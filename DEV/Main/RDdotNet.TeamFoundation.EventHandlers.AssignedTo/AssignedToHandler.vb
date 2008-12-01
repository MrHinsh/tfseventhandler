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

            Dim assignedIdentity As Identity = TeamServer.GetIdentityFromDisplayName(assignedName, e.Event)
            If assignedIdentity.SecurityGroup Then
                Return Not Querys.GetAssignedToName(e.Event).OldValue = Querys.GetAssignedToName(e.Event).NewValue
            Else
                Return Not assignedName = ChangedByName _
                            And Not Querys.GetAssignedToName(e.Event).OldValue = Querys.GetAssignedToName(e.Event).NewValue
            End If
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Is Not Valid ", TraceEventType.Warning)
            Return
        End If
        Dim assignedName As String = WorkItemTracking.Querys.GetAssignedToName(e.Event).NewValue
        Dim assignedIdentity As Identity = TeamServer.GetIdentityFromDisplayName(assignedName, e.Event)
        Dim ChangedByName As String = WorkItemTracking.Querys.GetChangedByName(e.Event).NewValue
        Dim ChangedIdentity As Identity = TeamServer.GetIdentityFromDisplayName(ChangedByName, e.Event)


        Dim Subject As String = "##PortfolioProject##:##WorkItemType## Assigned - ##WorkItemID##: ##WorkItemTitle##"

        Dim x As New UserNotificationService(EventHandlerItem, TeamServer, e)
        x.SendNotification("AssignedTo", ChangedIdentity, assignedIdentity, Subject)

        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Complete ")
    End Sub


#End Region

End Class