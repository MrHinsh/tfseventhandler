Namespace TeamFoundation.Proxies

    Public Class SubscriptionsClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ISubscriptions)
        Implements RDdotNet.Proxies.IClientProxy
        Implements Services.Contracts.ISubscriptions

        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub


        Public Function GetSubscriptions(ByVal TeamServer As String) As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Return MyBase.Channel.GetSubscriptions(TeamServer)
        End Function

        Public Sub RemoveSubscriptions(ByVal TeamServer As String, ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(TeamServer, ServiceUrl)
        End Sub

        Public Sub AddSubscriptions(ByVal TeamServer As String, ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Channel.AddSubscriptions(TeamServer, ServiceUrl)
        End Sub

        Public Sub IsSubscribed(ByVal TeamServer As String, ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.IsSubscribed
            MyBase.Channel.IsSubscribed(TeamServer, ServiceUrl)
        End Sub

    End Class

End Namespace