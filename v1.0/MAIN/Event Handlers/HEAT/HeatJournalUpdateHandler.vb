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
''' Send an email to a user who is assigned an email unless they are the one that assigned it to themselves
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
        Dim HeatRef As Integer = GetHeatRef(EventHandlerItem, ServiceHost, TeamServer, e)
        ' If there is no heat value then exit
        If HeatRef > 0 Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Has Ref 2")
            Return True
        End If

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: No Ref 2")
        Return False

    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running")
        ' Get Heat field value
        Dim HeatRef As Integer = GetHeatRef(EventHandlerItem, ServiceHost, TeamServer, e)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: HeatRef:" & HeatRef)
        Dim ChangedByName As StringField = Querys.GetChangedByName(e.Event)
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: ChangedByName:" & ChangedByName.NewValue)
        Dim UserName As String = RDdotNet.ActiveDirectory.Querys.GetUsername(ChangedByName.NewValue)
        If String.IsNullOrEmpty(UserName) Then UserName = "svc_TFSServices"
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: UserName:" & UserName)
        Dim Duration As Integer = 1
        Dim CompleatedWork As IntegerField = Querys.GetCoreIntegerField(e.Event, "Microsoft.VSTS.Scheduling.CompletedWork")
        If Not CompleatedWork Is Nothing AndAlso CompleatedWork.NewValue > CompleatedWork.OldValue Then
            Duration = (CompleatedWork.NewValue - CompleatedWork.OldValue) * 60
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Duration:" & Duration)
        'Microsoft.VSTS.CMMI.Estimate
        'Microsoft.VSTS.Scheduling.RemainingWork
        'Microsoft.VSTS.Scheduling.CompletedWork
        Dim objMail As New Mail(EventHandlerItem, TeamServer, e)

        Dim message As New System.Text.StringBuilder

        message.AppendFormat("Updated by {0} in Team Foundation Server", ChangedByName)
        message.AppendLine()
        message.AppendFormat("View the changes at {0}", e.Event.DisplayUrl)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: message:" & message.ToString)
        Try
            Dim dc As New DataAccess.HeatDataContext
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Updating Database:")
            dc.UpdateJournal(HeatRef, message.ToString, UserName, Duration)
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "HeatJournalUpdateHandler: Failed ")
        End Try
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Complete ")
    End Sub


#End Region

    Private Function GetHeatRef(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Integer
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatRef: ")

        Dim witID As IntegerField = Querys.GetWorkItemID(e.Event)
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatRef: witID: " & witID.NewValue)
        If Not m_WorkItemStore.ContainsKey(e.Identity.Url) Then
            m_WorkItemStore.Add(e.Identity.Url, DirectCast(TeamServer.Subject.GetService(GetType(WorkItemStore)), WorkItemStore))
        End If
        If m_WorkItemStore Is Nothing Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatRef: m_WorkItemStore is nothihng ")
            Return 0
        End If
        ' Get Heat field value
        Dim wi As WorkItem = m_WorkItemStore(e.Identity.Url).GetWorkItem(witID.NewValue)
        Dim HeatReference = (From f As Field In wi.Fields Where f.ReferenceName = "Aggreko.Heat.Reference").SingleOrDefault
        ' If there is no field then return false
        If HeatReference Is Nothing Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatRef: Heat ref field not loaded")
            Return 0
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: GetHeatRef: Heat ref field loaded")
        Dim HeatRef As Integer = HeatReference.Value
        ' If there is no heat value then exit
        If HeatRef > 0 Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Returning heat ref: " & HeatRef)
            Return HeatRef
        End If
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