Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace TeamFoundation.Clients

    Public Class EventsService
        Inherits RDdotNet.Clients.WcfServiceBase(Of Proxies.EventsClient, WSDualHttpBinding)
        Implements Services.Contracts.IEvents

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            MyBase.New(Server, "RDdotNet/TFSEventHandler/EventHandling/Handlers")
        End Sub

        Friend Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseCheckinEvent
            Client.RaiseCheckinEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

        Friend Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseUnknown
            Client.RaiseUnknown(eventXml, tfsIdentityXml, SubscriptionInfo)
        End Sub

        Friend Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseWorkItemChangedEvent
            Client.RaiseWorkItemChangedEvent([Event], EventIdentity, SubscriptionInfo)
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

        Protected Overrides Function GetClient() As Proxies.EventsClient
            Return New Proxies.EventsClient(GetBinding, Me.EndPoint)
        End Function


    End Class

End Namespace
