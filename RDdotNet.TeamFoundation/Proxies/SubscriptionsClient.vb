Namespace Proxies

    Friend Class SubscriptionsClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ISubscriptions)
        Implements Services.Contracts.ISubscriptions

        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Friend Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Channel.AddSubscriptions(ServiceUrl, EventType)
        End Sub

        Friend Function GetSubscriptions() As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Return MyBase.Channel.GetSubscriptions
        End Function

        Friend Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(ServiceUrl)
        End Sub

    End Class

End Namespace