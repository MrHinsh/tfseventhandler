Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Collections.ObjectModel

Namespace Clients.Proxy

    Friend Class HandlersClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.IHandlers)
        Implements Services.Contracts.IHandlers

        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Public Sub AddAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.AddAssembly
            MyBase.Channel.AddAssembly(AssemblyItem)
        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements Services.Contracts.IHandlers.AddAssemblyDirect
            MyBase.Channel.AddAssemblyDirect(AssemblyBytes)
        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As Services.DataContracts.AssemblyItem Implements Services.Contracts.IHandlers.GetAssemblyItem
            Return MyBase.Channel.GetAssemblyItem(AssemblyBytes)
        End Function

        Public Function GetAssemblys() As Collection(Of AssemblyItem) Implements Services.Contracts.IHandlers.GetAssemblys
            Return MyBase.Channel.GetAssemblys()
        End Function

        Public Sub RemoveAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.RemoveAssembly
            MyBase.Channel.RemoveAssembly(AssemblyItem)
        End Sub

        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly
            Return MyBase.Channel.ValidateAssembly(AssemblyItem)
        End Function
    End Class

End Namespace