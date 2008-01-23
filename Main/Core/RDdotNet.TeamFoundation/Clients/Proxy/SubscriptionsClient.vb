Namespace Clients.Proxy

    Friend Class SubscriptionsClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ISubscriptions)
        Implements Services.Contracts.ISubscriptions


        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Friend Sub AddSubscriptions(ByVal TeamServerName As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Channel.AddSubscriptions(TeamServerName, EventType)
        End Sub

        Friend Function GetSubscriptions(ByVal TeamServerName As String) As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Return MyBase.Channel.GetSubscriptions(TeamServerName)
        End Function

        Friend Sub RemoveSubscriptions(ByVal TeamServerName As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(TeamServerName)
        End Sub

        Public Function EventServiceUrl(ByVal EventType As Events.EventTypes) As System.Uri Implements Services.Contracts.ISubscriptions.EventServiceUrl
            Return MyBase.Channel.EventServiceUrl(EventType)
        End Function

    End Class

End Namespace