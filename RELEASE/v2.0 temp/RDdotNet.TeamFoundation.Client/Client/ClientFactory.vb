Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Public Class ClientFactory

    Private Sub New()

    End Sub

#Region " Bindings "

    Friend Shared Function GetSecureWSDualHttpBinding() As WSDualHttpBinding
        Dim Binding As New WSDualHttpBinding(WSDualHttpSecurityMode.Message)
        Binding.MaxReceivedMessageSize = 655360
        Binding.ReaderQuotas.MaxStringContentLength = 655360
        Binding.ReaderQuotas.MaxArrayLength = 655360
        Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
        Binding.Security.Message.NegotiateServiceCredential = True
        Return Binding
    End Function

    Friend Shared Function GetSecureWSHttpBinding() As WSHttpBinding
        Dim Binding As New WSHttpBinding(SecurityMode.Message, True)
        'Binding.MaxReceivedMessageSize = 655360
        'Binding.ReaderQuotas.MaxStringContentLength = 655360
        'Binding.ReaderQuotas.MaxArrayLength = 655360
        Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
        Binding.Security.Message.NegotiateServiceCredential = True
        Return Binding
    End Function

    Friend Shared Function GetSecureNetMsmqBinding() As NetMsmqBinding
        Dim Binding As New NetMsmqBinding(NetMsmqSecurityMode.Message)
        Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
        Return Binding
    End Function

#End Region

#Region " Builders "

    Public Shared Function GetHandlersClient(ByVal CallbackInstance As InstanceContext) As Proxys.EventHandlerService.HandlersClient
        '---------------
        Dim epa As New EndpointAddress(String.Format("http://{0}:6661/TFSEventHandler/Handlers", My.Computer.Name))
        '---------------
        Dim sc As New Proxys.EventHandlerService.HandlersClient(CallbackInstance, GetSecureWSDualHttpBinding, epa)
        '----------------
        Return sc
    End Function

    Public Shared Function GetEventsClient() As Proxys.EventHandlerService.EventsClient
        '---------------
        Dim epa As New EndpointAddress(String.Format("net.msmq://{0}/private/TFSEventHandler", My.Computer.Name))
        '---------------
        Dim sc As New Proxys.EventHandlerService.EventsClient(GetSecureNetMsmqBinding, epa)
        '----------------
        Return sc
    End Function

#End Region

End Class
