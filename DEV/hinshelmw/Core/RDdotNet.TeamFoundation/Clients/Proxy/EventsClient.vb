Namespace Clients.Proxy

    Public Class EventsClient
        Inherits System.ServiceModel.ClientBase(Of Services.Contracts.IEvents)
        Implements Services.Contracts.IEvents

        Friend Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub

        Public Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseCheckinEvent
            MyBase.Channel.RaiseCheckinEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

        Public Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseUnknown
            MyBase.Channel.RaiseUnknown(eventXml, tfsIdentityXml, SubscriptionInfo)
        End Sub

        Public Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseWorkItemChangedEvent
            MyBase.Channel.RaiseWorkItemChangedEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

    End Class

End Namespace