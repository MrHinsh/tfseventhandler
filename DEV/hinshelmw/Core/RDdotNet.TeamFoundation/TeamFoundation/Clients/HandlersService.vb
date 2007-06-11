Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace TeamFoundation.Clients

    Public Delegate Sub HandlersUpdated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest)

    Public Class HandlersService
        Inherits RDdotNet.Clients.WcfServiceBase(Of Proxies.HandlersClient, WSDualHttpBinding)
        Implements Services.Contracts.IHandlers
        Implements Services.Contracts.IHandlersCallback

        Public Event HandlersUpdated As HandlersUpdated

#Region " WcfServiceBase "

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            MyBase.New(Server, "RDdotNet/TFSEventHandler/EventHandling/Handlers")
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

        Protected Overrides Function GetClient() As Proxies.HandlersClient
            Return New Proxies.HandlersClient(New InstanceContext(Me), GetBinding, Me.EndPoint)
        End Function

#End Region

#Region " IHandlers "

        Public Sub AddAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.AddAssembly
            Me.Client.AddAssembly(AssemblyItem)
        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements Services.Contracts.IHandlers.AddAssemblyDirect
            Me.Client.AddAssemblyDirect(AssemblyBytes)
        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As Services.DataContracts.AssemblyItem Implements Services.Contracts.IHandlers.GetAssemblyItem
            Return Me.Client.GetAssemblyItem(AssemblyBytes)
        End Function

        Public Function GetAssemblys() As Services.DataContracts.AssemblyManaifest Implements Services.Contracts.IHandlers.GetAssemblys
            Return Me.Client.GetAssemblys()
        End Function

        Public Sub RemoveAssembly(ByVal ID As Integer) Implements Services.Contracts.IHandlers.RemoveAssembly
            Me.Client.RemoveAssembly(ID)
        End Sub

        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly
            Me.Client.ValidateAssembly(AssemblyItem)
        End Function

#End Region

#Region " IHandlersCallback "

        Public Sub Updated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest) Implements Services.Contracts.IHandlersCallback.Updated
            RaiseEvent HandlersUpdated(AssemblyManaifest)
        End Sub

#End Region


    End Class

End Namespace
