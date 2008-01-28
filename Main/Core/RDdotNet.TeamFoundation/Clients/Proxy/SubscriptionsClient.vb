Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Clients.Proxy

    Friend Class SubscriptionsClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ISubscriptions)
        Implements Services.Contracts.ISubscriptions

        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Public Sub AddSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Channel.AddSubscriptions(TeamServer, EventType)
        End Sub

        Public Function EventServiceUrl(ByVal EventType As Events.EventTypes) As System.Uri Implements Services.Contracts.ISubscriptions.EventServiceUrl
            Return MyBase.Channel.EventServiceUrl(EventType)
        End Function

        Public Sub RefreshServerSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ISubscriptions.RefreshServerSubscriptions
            MyBase.Channel.RefreshServerSubscriptions(TeamServer)
        End Sub

        Public Sub RefreshSubscription(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal Subscription As Services.DataContracts.SubscriptionItem) Implements Services.Contracts.ISubscriptions.RefreshSubscription
            MyBase.Channel.RefreshSubscription(TeamServer, Subscription)
        End Sub

        Public Sub RefreshSubscriptions() Implements Services.Contracts.ISubscriptions.RefreshSubscriptions
            MyBase.Channel.RefreshSubscriptions()
        End Sub

        Public Sub RemoveSubscription(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal Subscription As Services.DataContracts.SubscriptionItem) Implements Services.Contracts.ISubscriptions.RemoveSubscription
            MyBase.Channel.RemoveSubscription(TeamServer, Subscription)
        End Sub

        Public Sub RemoveSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(TeamServer)
        End Sub
    End Class

End Namespace