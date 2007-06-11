Namespace TeamFoundation.Proxies

    Public Class SubscriptionsClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ISubscriptions)
        Implements Services.Contracts.ISubscriptions

        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Channel.AddSubscriptions(ServiceUrl, EventType)
        End Sub

        Public Function GetSubscriptions() As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            MsgBox(Me.ClientCredentials.UserName.UserName)
            Return MyBase.Channel.GetSubscriptions
        End Function

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(ServiceUrl)
        End Sub

    End Class

End Namespace