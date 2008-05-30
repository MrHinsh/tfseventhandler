Imports Microsoft.TeamFoundation.Server

Public Class Notification
    Implements INotification

    Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo) Implements INotification.Notify
        Dim IdentityObject As TFSIdentity = EndpointBase.CreateInstance(Of TFSIdentity)(tfsIdentityXml)
        '---------------
        Dim UriString As String = OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri.AbsoluteUri
        Dim SlashIndex As Integer = UriString.LastIndexOf("/")
        Dim EndieBit As String = UriString.Substring(SlashIndex, (UriString.Length - (UriString.Length - SlashIndex)))
        Dim EventType As EventTypes = CType([Enum].Parse(GetType(EventTypes), EndieBit), EventTypes)
        '---------------
            Select EventType
            Case EventTypes.WorkItemChangedEvent
                Dim EventObject As WorkItemChangedEvent = EndpointBase.CreateInstance(Of WorkItemChangedEvent)(eventXml)
                EventHandlerClient.EventsService.RaiseWorkItemChangedEvent(EventObject, IdentityObject, New DataContracts.SubscriptionInfo(SubscriptionInfo))
            Case EventTypes.CheckinEvent
                Dim EventObject As CheckinEvent = EndpointBase.CreateInstance(Of CheckinEvent)(eventXml)
                EventHandlerClient.EventsService.RaiseCheckinEvent(EventObject, IdentityObject, New DataContracts.SubscriptionInfo(SubscriptionInfo))
            Case Else
                EventHandlerClient.EventsService.RaiseUnknown(eventXml, tfsIdentityXml, New DataContracts.SubscriptionInfo(SubscriptionInfo))
    End Sub

End Class
