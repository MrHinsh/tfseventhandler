Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.io
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports Microsoft.TeamFoundation.WorkItemTracking.Client
Imports RDdotNet.TeamFoundation.Helpers
Imports RDdotNet.TeamFoundation
Imports System.Xml.Serialization

''' <summary>
''' Updates a Heat Journal Entry with the duration of work and a not with a link to the TFS Artifact
''' </summary>
''' <remarks></remarks>
Public Class HeatJournalUpdateHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

    Private m_WorkItemStore As New Dictionary(Of String, WorkItemStore)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a assignment to a user other than the assigner
    '' but not if the changer and the assignee are the sdame person,
    '' and not if the old assignee and new assignee are the same.
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Activated ")
        If e.Event Is Nothing Then
            Return False
        End If
        If TeamServer.ItemElement.LogEvents Then WriteOutEvent(EventHandlerItem, ServiceHost, TeamServer, e)
        Dim wi As WorkItem = GetHeatWorkItem(EventHandlerItem, ServiceHost, TeamServer, e)
        Dim HeatRef As Field = GetField(wi, My.Settings.HeatFieldFef, TeamServer)
        ' If there is no heat value then exit
        If HeatRef Is Nothing OrElse HeatRef.Value = Nothing Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Heatref nor present")
            Return False
        End If

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Has Heat Ref")
        Return True

    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running")
        Dim wi As WorkItem = GetHeatWorkItem(EventHandlerItem, ServiceHost, TeamServer, e)
        ' Get Heat field value
        Dim HeatRef As Field = GetField(wi, My.Settings.HeatFieldFef, TeamServer)
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: HeatRef:" & HeatRef.Value)
        Dim ChangedByName As StringField = Querys.GetChangedByName(e.Event)
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: ChangedByName:" & ChangedByName.NewValue)
        Dim UserName As String = RDdotNet.ActiveDirectory.Querys.GetUsername(ChangedByName.NewValue)
        If String.IsNullOrEmpty(UserName) Then UserName = My.User.Name
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: UserName:" & UserName)
        Dim Duration As Integer = 1
        Try
            Dim CompleatedWork As StringField = Querys.GetChangedStringField(e.Event, "Microsoft.VSTS.Scheduling.CompletedWork")
            If Not CompleatedWork Is Nothing AndAlso CInt(CompleatedWork.NewValue) > CInt(CompleatedWork.OldValue) Then
                Duration = (CInt(CompleatedWork.NewValue) - CInt(CompleatedWork.OldValue)) * 60
            End If
            If CompleatedWork Is Nothing Then
                If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: CompleatedWork is nothing")
            Else
                If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: CompleatedWork:NewValue" & CompleatedWork.NewValue)
                If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: CompleatedWork:OldValue" & CompleatedWork.OldValue)
            End If
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "HeatJournalUpdateHandler: CompleatedWork Failed ")
        End Try
        
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Duration:" & Duration)
        'Microsoft.VSTS.CMMI.Estimate
        'Microsoft.VSTS.Scheduling.RemainingWork
        'Microsoft.VSTS.Scheduling.CompletedWork
        Dim objMail As New Mail(EventHandlerItem, TeamServer, e)

        Dim message As New System.Text.StringBuilder

        message.AppendFormat("Updated by {0} in Team Foundation Server", ChangedByName.NewValue)
        message.AppendLine()
        message.AppendFormat("View the changes at {0}", e.Event.DisplayUrl)
        message.AppendLine()
        message.AppendFormat("Work Item: <a href='{0}'>View {1}</a>", e.Event.DisplayUrl, e.Event.WorkItemTitle)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: message:" & message.ToString)
        Try
            Dim dc As New DataAccess.HeatDataContext
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Updating Database:")
            dc.UpdateJournal(HeatRef.Value, message.ToString, UserName, Duration)
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "HeatJournalUpdateHandler: Failed ")
        End Try
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Complete ")
    End Sub


#End Region

    Private Function GetHeatWorkItem(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As WorkItem
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatWorkItem: ")

        Dim witID As IntegerField = Querys.GetWorkItemID(e.Event)
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatWorkItem: witID: " & witID.NewValue)
        If Not m_WorkItemStore.ContainsKey(e.Identity.Url) Then
            m_WorkItemStore.Add(e.Identity.Url, DirectCast(TeamServer.Subject.GetService(GetType(WorkItemStore)), WorkItemStore))
        End If
        If m_WorkItemStore Is Nothing Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatWorkItem: m_WorkItemStore is nothihng ")
            Return Nothing
        End If
        ' Get Heat field value
        Dim wi As WorkItem = m_WorkItemStore(e.Identity.Url).GetWorkItem(witID.NewValue)
        Return wi
    End Function

    Private Function GetField(ByVal wi As WorkItem, ByVal ReferenceName As String, ByVal TeamServer As TeamServerItem) As Field
        Return (From f As Field In wi.Fields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    Private Sub WriteOutEvent(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))
        If TeamServer.ItemElement.LogEvents Then
            Dim s As New XmlSerializer(GetType(WorkItemChangedEvent))

            Dim PathSafeSubhject As String = Now.Ticks & "-" & e.EventID.ToString & ".xml"
            For Each x As Char In System.IO.Path.GetInvalidPathChars
                PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
            Next
            For Each x As Char In System.IO.Path.GetInvalidFileNameChars
                PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
            Next
            Dim PathsafeFile As String = System.IO.Path.Combine(TeamServer.ItemElement.EventLogPath, "Events\WorkItemTracking\")
            System.IO.Directory.CreateDirectory(PathsafeFile)
            PathsafeFile = System.IO.Path.Combine(PathsafeFile, PathSafeSubhject)
            '-----------------
            s.Serialize(System.IO.File.AppendText(PathsafeFile), e.Event)
        End If


    End Sub




End Class