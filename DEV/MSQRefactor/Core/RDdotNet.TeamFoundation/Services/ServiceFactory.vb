Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

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

    Public Shared Function GetEventHandlerServiceHost() As ServiceHost
        '---------------
        Dim baseAddresses() As Uri = { _
                        New Uri(String.Format("net.msmq://{0}/private/TFSEventHandler", My.Computer.Name)), _
                        New Uri(String.Format("http://{0}:6661/TFSEventHandler", My.Computer.Name)) _
                        }
        '---------------
        Dim sh As New System.ServiceModel.ServiceHost(GetType(Services.EventHandlerService), baseAddresses)
        '----------------
        SetServiceMetadataBehavior(sh)
        SetServiceDebugBehavior(sh)
        '---------------
        sh.AddServiceEndpoint(GetType(Services.Contracts.IHandlers), GetSecureWSDualHttpBinding, "Handlers")
        sh.AddServiceEndpoint(GetType(Services.Contracts.IEvents), GetSecureWSDualHttpBinding, "Events")
        sh.AddServiceEndpoint(GetType(Description.IMetadataExchange), GetSecureWSHttpBinding, "mex")
        '----------------
        Return sh
    End Function

    Public Shared Function GetQueuerServiceHost() As ServiceHost
        '---------------
        Dim baseAddresses() As Uri = { _
                        New Uri(String.Format("http://{0}:6661/TFSQueuer", My.Computer.Name)) _
                        }
        '---------------
        Dim sh As New System.ServiceModel.ServiceHost(GetType(Services.EventHandlerService), baseAddresses)
        '---------------
        SetServiceMetadataBehavior(sh)
        SetServiceDebugBehavior(sh)
        '---------------
        sh.AddServiceEndpoint(GetType(Services.Contracts.INotification), GetSecureWSHttpBinding, "Notification")
        sh.AddServiceEndpoint(GetType(Services.Contracts.ISubscriptions), GetSecureWSDualHttpBinding, "Subscriptions")
        sh.AddServiceEndpoint(GetType(Services.Contracts.ITeamServers), GetSecureWSDualHttpBinding, "Subscriptions")
        sh.AddServiceEndpoint(GetType(Description.IMetadataExchange), GetSecureWSHttpBinding, "mex")
        '----------------
        Return sh
    End Function

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
