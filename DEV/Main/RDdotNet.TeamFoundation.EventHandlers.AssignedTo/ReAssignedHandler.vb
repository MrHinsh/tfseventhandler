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
''' Send an email to a user when a work item that they are assigned is re assigend to someone else unless they are the one that made the change.
''' </summary>
''' <remarks></remarks>
Public Class ReAssignedHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a new assignment to a user other than the assigner
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        If e.Event Is Nothing Then
            Return False
        End If
        Dim NewAssignedName As String = Querys.GetAssignedToName(e.Event).NewValue
        Dim OldAssignedName As String = Querys.GetAssignedToName(e.Event).OldValue
        Dim ChangedByName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        If String.IsNullOrEmpty(NewAssignedName) Or String.IsNullOrEmpty(OldAssignedName) Then
            Return False
        Else
            Return Not NewAssignedName = OldAssignedName And Not OldAssignedName = ChangedByName
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("ReAssignedHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("ReAssignedHandler: Is not valid ", TraceEventType.Warning)
            Return
        End If

        Dim toName As String = Querys.GetAssignedToName(e.Event).OldValue
        Dim toIdentity As Identity = TeamServer.GroupSecurityService.ReadIdentity(SearchFactor.DistinguishedName, toName, QueryMembership.Expanded)

        Dim fromName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        Dim fromIdentity As Identity = TeamServer.GroupSecurityService.ReadIdentity(SearchFactor.DistinguishedName, fromName, QueryMembership.Expanded)

        Dim Subject As String = "##PortfolioProject##:##WorkItemType## Re-Assigned - ##WorkItemID##: ##WorkItemTitle##"
        Dim x As New UserNotificationService(EventHandlerItem, TeamServer, e)
        x.SendNotification("ReAssigned", toIdentity, fromIdentity, Subject)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("ReAssignedHandler: Complete ")
    End Sub


#End Region





End Class