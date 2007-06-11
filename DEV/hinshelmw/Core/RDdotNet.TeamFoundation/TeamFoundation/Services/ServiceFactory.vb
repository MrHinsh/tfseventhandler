Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace TeamFoundation.Services


    Public Class ServiceFactory

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
            Binding.ClientBaseAddress = New System.Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":660")
            Binding.BypassProxyOnLocal = True
            Return Binding
        End Function

        Friend Shared Function GetSecureWSHttpBinding() As WSHttpBinding
            Dim Binding As New WSHttpBinding(SecurityMode.Message, True)
            'Binding.MaxReceivedMessageSize = 655360
            'Binding.ReaderQuotas.MaxStringContentLength = 655360
            'Binding.ReaderQuotas.MaxArrayLength = 655360
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Binding.Security.Message.NegotiateServiceCredential = True
            Binding.BypassProxyOnLocal = True
            Return Binding
        End Function

        Friend Shared Function GetSecureNetMsmqBinding() As NetMsmqBinding
            Dim Binding As New NetMsmqBinding(NetMsmqSecurityMode.Message)
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Return Binding
        End Function

#End Region

#Region " Builders "

        Public Shared Function GetEventHandlerServiceHost(ByVal Port As Integer) As ServiceHost
            '---------------
            Dim baseAddresses() As Uri = { _
                            New Uri(String.Format("net.msmq://{0}/private/TFSEventHandler", System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName)), _
                            New Uri(String.Format("http://{0}:{1}/RDdotNet/TFSEventHandler/EventHandling", System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName, Port)) _
                            }
            '---------------
            Dim sh As New System.ServiceModel.ServiceHost(GetType(Services.EventHandlerService), baseAddresses)
            '----------------
            SetServiceMetadataBehavior(sh)
            SetServiceDebugBehavior(sh)
            '---------------
            sh.Description.Endpoints.Clear()
            sh.AddServiceEndpoint(GetType(Services.Contracts.IHandlers), GetSecureWSDualHttpBinding, "Handlers")
            sh.AddServiceEndpoint(GetType(Services.Contracts.IEvents), GetSecureWSDualHttpBinding, "Events")
            sh.AddServiceEndpoint(GetType(Description.IMetadataExchange), GetSecureWSHttpBinding, "mex")
            '----------------
            Return sh
        End Function

        Public Shared Function GetQueuerServiceHost(ByVal Port As Integer) As ServiceHost
            '---------------
            Dim baseAddresses() As Uri = { _
                            New Uri(String.Format("http://{0}:{1}/RDdotNet/TFSEventHandler/Queuer", My.Computer.Name, Port)) _
                            }
            '---------------
            Dim sh As New System.ServiceModel.ServiceHost(GetType(Services.QueuerService), baseAddresses)
            '---------------
            SetServiceMetadataBehavior(sh)
            SetServiceDebugBehavior(sh)
            '---------------
            sh.Description.Endpoints.Clear()
            For Each EventType As Events.EventTypes In [Enum].GetValues(GetType(Events.EventTypes))
                sh.AddServiceEndpoint(GetType(Services.Contracts.INotification), GetSecureWSHttpBinding, "Notification/" & EventType.ToString)
            Next
            'sh.AddServiceEndpoint(GetType(Services.Contracts.ISubscriptions), GetSecureWSDualHttpBinding, "Subscriptions")
            sh.AddServiceEndpoint(GetType(Services.Contracts.ITeamServers), GetSecureWSDualHttpBinding, "TeamServers")
            sh.AddServiceEndpoint(GetType(Description.IMetadataExchange), GetSecureWSHttpBinding, "mex")
            '----------------
            Return sh
        End Function '

#End Region

#Region " ServiceMetaData "

        Private Shared Sub SetServiceMetadataBehavior(ByRef sh As ServiceHost)
            Dim smb As ServiceMetadataBehavior = sh.Description.Behaviors.Find(Of ServiceMetadataBehavior)()
            If smb Is Nothing Then
                smb = New ServiceMetadataBehavior()
                smb.HttpGetEnabled = True
                sh.Description.Behaviors.Add(smb)
            Else
                smb.HttpGetEnabled = True
            End If
        End Sub

        Private Shared Sub SetServiceDebugBehavior(ByRef sh As ServiceHost)
            Dim sdb As ServiceDebugBehavior = sh.Description.Behaviors.Find(Of ServiceDebugBehavior)()
            If sdb Is Nothing Then
                sdb = New ServiceDebugBehavior()
                sdb.IncludeExceptionDetailInFaults = True
                sh.Description.Behaviors.Add(sdb)
            Else
                sdb.IncludeExceptionDetailInFaults = True
            End If
        End Sub


#End Region

    End Class


End Namespace