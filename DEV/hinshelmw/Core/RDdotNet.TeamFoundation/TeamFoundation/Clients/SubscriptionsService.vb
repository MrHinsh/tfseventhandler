Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace TeamFoundation.Clients

    Public Delegate Sub SubscriptionsUpdated(ByVal Subscriptions As Collection(Of Services.DataContracts.Subscription))

    Public Class SubscriptionsService
        Inherits RDdotNet.Clients.WcfServiceBase(Of Proxies.SubscriptionsClient, WSDualHttpBinding)
        Implements Services.Contracts.ISubscriptions
        Implements Services.Contracts.ISubscriptionsCallback

        Public Event SubscriptionsUpdated As SubscriptionsUpdated

#Region " WcfServiceBase "

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            MyBase.New(Server, "RDdotNet/TFSEventHandler/Queuer/Subscriptions")
            Client.ChannelFactory.Credentials.Windows.ClientCredential = System.Net.CredentialCache.DefaultCredentials
        End Sub

        Protected Overrides Function GetBinding() As System.ServiceModel.WSDualHttpBinding
            Dim Binding As New WSDualHttpBinding(WSDualHttpSecurityMode.Message)
            Binding.MaxReceivedMessageSize = 655360
            Binding.ReaderQuotas.MaxStringContentLength = 655360
            Binding.ReaderQuotas.MaxArrayLength = 655360
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Binding.Security.Message.NegotiateServiceCredential = True
            Binding.ClientBaseAddress = New System.Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":660")
            Binding.BypassProxyOnLocal = True
            Return Binding
        End Function

        Protected Overrides Function GetClient() As Proxies.SubscriptionsClient
            Return New Proxies.SubscriptionsClient(New InstanceContext(Me), GetBinding, Me.EndPoint)
        End Function

#End Region

#Region " ISubscriptions "

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            MyBase.Client.AddSubscriptions(ServiceUrl, EventType)
        End Sub

        Public Function GetSubscriptions() As Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Return MyBase.Client.GetSubscriptions()
        End Function

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            MyBase.Client.RemoveSubscriptions(ServiceUrl)
        End Sub

#End Region

#Region " ISubscriptionsCallback "


        Public Sub Updated(ByVal Subscriptions As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription)) Implements Services.Contracts.ISubscriptionsCallback.Updated
            RaiseEvent SubscriptionsUpdated(Subscriptions)
        End Sub

#End Region






    End Class

End Namespace
