Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.io
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports Hinshelwood.TeamFoundation.Helpers
Imports Hinshelwood.TeamFoundation

''' <summary>
''' Send an email to a user who is assigned a work item unless they are the one that assigned it to themselves
''' </summary>
''' <remarks></remarks>
Public Class AssignedToHandler
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
        Dim assignedName As String = WorkItemEventQuerys.GetAssignedToName(e.Event)
        Dim ChangedByName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        If String.IsNullOrEmpty(assignedName) Then
            Return False
        Else
            Return Not assignedName = ChangedByName _
                    And Not Querys.GetAssignedToName(e.Event).OldValue = Querys.GetAssignedToName(e.Event).NewValue
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Is Not Valid ", TraceEventType.Warning)
            Return
        End If

        Dim toName As String = WorkItemTracking.Querys.GetAssignedToName(e.Event).NewValue
        Dim toAddress As String = Hinshelwood.ActiveDirectory.Querys.GetEmailAddress(toName)
        Dim fromName As String = WorkItemTracking.Querys.GetChangedByName(e.Event).NewValue
        Dim fromAddress As String = Hinshelwood.ActiveDirectory.Querys.GetEmailAddress(fromName)
        If String.IsNullOrEmpty(toAddress) Then
            'Logger.Log("Can't send email because no email address was found for " + toName)
        Else
            Dim [to] As New MailAddress(toAddress, toName)
            Dim from As New MailAddress(fromAddress, fromName)
            If TeamServer.ItemElement.TestMode Then
                [to] = New Net.Mail.MailAddress(TeamServer.ItemElement.TestEmail)
            End If
            Dim Subject As String = "##PortfolioProject##:##WorkItemType## Assigned - ##WorkItemID##: ##WorkItemTitle##"
            Dim x As New Mail(EventHandlerItem, TeamServer, e)
            x.SendMail("AssignedTo", [to], from, Subject)
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Complete ")
    End Sub


#End Region

End Class