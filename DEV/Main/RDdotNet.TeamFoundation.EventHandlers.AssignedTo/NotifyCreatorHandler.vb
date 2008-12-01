Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports Hinshelwood.TeamFoundation.Helpers
Imports Hinshelwood.TeamFoundation
Imports Microsoft.TeamFoundation.Server


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
            Dim GroupSecurityService As IGroupSecurityService = CType(TeamServer.Subject.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
            Dim assignedIdentity As Identity = GroupSecurityService.ReadIdentity(SearchFactor.DistinguishedName, Querys.GetAssignedToName(e.Event).OldValue, QueryMembership.Expanded)
            If assignedIdentity.SecurityGroup Then
                ' If group, don't check 

            End If

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
        Dim toIdentity As Identity = TeamServer.GroupSecurityService.ReadIdentity(SearchFactor.DistinguishedName, toName, QueryMembership.Expanded)
        Dim fromName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        Dim fromIdentity As Identity = TeamServer.GroupSecurityService.ReadIdentity(SearchFactor.DistinguishedName, fromName, QueryMembership.Expanded)

        Dim Subject As String = "##PortfolioProject##:##WorkItemType## Owner Notification - ##WorkItemID##: ##WorkItemTitle##"
        Dim x As New UserNotificationService(EventHandlerItem, TeamServer, e)
        x.SendNotification("NotifyCreator", toIdentity, fromIdentity, Subject)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("NotifyCreatorHandler: Complete ")
    End Sub


#End Region

End Class