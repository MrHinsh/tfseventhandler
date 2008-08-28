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

Public Class AssignedFromHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a new assignment to a user other than the assigner
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        If e.Event Is Nothing Then
            Return False
        End If
        Dim assignedName As String = Querys.GetAssignedToName(e.Event).NewValue
        If String.IsNullOrEmpty(assignedName) Then
            Return False
        Else
            Return Not assignedName = Querys.GetAssignedToName(e.Event).OldValue
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If
        Dim toName As String = Querys.GetAssignedToName(e.Event).OldValue
        Dim toAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(toName)
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
            Dim Subject As String = "##PortfolioProject##:##WorkItemType## Assigned - ##WorkItemID##: ##WorkItemTitle##"
            Dim x As New Mail(EventHandlerItem, TeamServer, e)
            x.SendMail(Mail.EmailTypes.ReAssigned, [to], from, Subject)
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Complete ")
    End Sub


#End Region

End Class