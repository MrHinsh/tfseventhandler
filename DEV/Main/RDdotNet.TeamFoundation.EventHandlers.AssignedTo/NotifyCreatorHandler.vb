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
''' Sends an email to the Owner of a Work Item when it is changed unless they are the one that changed it
''' </summary>
''' <remarks></remarks>
Public Class NotifyCreatorHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a new assignment to a user other than the assigner
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        If e.Event Is Nothing Then
            Return False
        End If
        Dim createdName As String = Querys.GetCreatedByName(e.Event).OldValue
        Dim ChangedByName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        If String.IsNullOrEmpty(createdName) Then
            Return False
        Else
            Return Not createdName = ChangedByName And Not ChangedByName = Querys.GetAssignedToName(e.Event).OldValue
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("NotifyCreatorHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("NotifyCreatorHandler: Is not valid ", TraceEventType.Warning)
            Return
        End If
        Dim toName As String = Querys.GetCreatedByName(e.Event).OldValue
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
            Dim Subject As String = "##PortfolioProject##:##WorkItemType## Owner Notification - ##WorkItemID##: ##WorkItemTitle##"
            Dim x As New Mail(EventHandlerItem, TeamServer, e)
            x.SendMail("NotifyCreator", [to], from, Subject)
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("NotifyCreatorHandler: Complete ")
    End Sub


#End Region

End Class