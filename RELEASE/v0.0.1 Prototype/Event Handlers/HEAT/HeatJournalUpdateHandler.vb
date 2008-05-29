Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.io
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports RDdotNet.TeamFoundation.Helpers
Imports RDdotNet.TeamFoundation

''' <summary>
''' Send an email to a user who is assigned an email unless they are the one that assigned it to themselves
''' </summary>
''' <remarks></remarks>
Public Class HeatJournalUpdateHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a assignment to a user other than the assigner
    '' but not if the changer and the assignee are the sdame person,
    '' and not if the old assignee and new assignee are the same.
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        If e.Event Is Nothing Then
            Return False
        End If
        ' Get Heat field value
        Dim HeatReference As StringField = Querys.GetHeatReference(e.Event)
        ' If there is no field then return false
        If HeatReference Is Nothing Then
            Return False
        End If
        ' If there is no heat value then exit
        If Not String.IsNullOrEmpty(HeatReference.NewValue) Then
            Return True
        End If
        Return False

    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If
        ' Get Heat field value
        Dim HeatReference As StringField = Querys.GetHeatReference(e.Event)
        Dim HeatRef As Integer = HeatReference.OldValue
        If Not String.IsNullOrEmpty(HeatReference.NewValue) Then
            HeatRef = HeatReference.NewValue
        End If
        Dim ChangedByName As StringField = Querys.GetChangedByName(e.Event)
        Dim UserName As String = RDdotNet.ActiveDirectory.Querys.GetUsername(ChangedByName.NewValue)
        Dim Duration As Integer = 1
        Dim CompleatedWork As IntegerField = Querys.GetCoreIntegerField(e.Event, "Microsoft.VSTS.Scheduling.CompletedWork")
        If Not CompleatedWork Is Nothing AndAlso CompleatedWork.NewValue > CompleatedWork.OldValue Then
            Duration = (CompleatedWork.NewValue - CompleatedWork.OldValue) * 60
        End If
        'Microsoft.VSTS.CMMI.Estimate
        'Microsoft.VSTS.Scheduling.RemainingWork
        'Microsoft.VSTS.Scheduling.CompletedWork
        Dim objMail As New Mail(EventHandlerItem, TeamServer, e)
        Dim message As String = "From TFS:" & vbCrLf & objMail.GetInnerBody("HeatJournalUpdate")

        Try
            Dim dc As New DataAccess.HeatDataContext
            dc.UpdateJournal(HeatRef, message, UserName, Duration)
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "HeatJournalUpdateHandler: Complete ")
            Dim toName As String = "Test Dudes" 'WorkItemEventQuerys.GetAssignedToName(e.Event)
            Dim toAddress As String = "Martin.hinshelwood@aggreko.co.uk;roddy.crossin@aggreko.co.uk" 'RDdotNet.ActiveDirectory.Querys.GetEmailAddress(toName)
            Dim fromName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
            Dim fromAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(fromName)
            If String.IsNullOrEmpty(toAddress) Then
                'Logger.Log("Can't send email because no email address was found for " + toName)
            Else
                Dim [to] As New MailAddress(toAddress, toName)
                Dim from As New MailAddress(fromAddress, fromName)
                If TeamServer.ItemElement.TestMode Then
                    [to] = New Net.Mail.MailAddress(TeamServer.ItemElement.TestEmail)
                End If
                Dim Subject As String = "##PortfolioProject##:##WorkItemType## Heat Journal Update - ##WorkItemID##: ##WorkItemTitle##"
                Dim x As New Mail(EventHandlerItem, TeamServer, e)
                x.SendMail("HeatJournalUpdate", [to], from, Subject, False)
            End If
        End Try
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Complete ")
    End Sub


#End Region




End Class