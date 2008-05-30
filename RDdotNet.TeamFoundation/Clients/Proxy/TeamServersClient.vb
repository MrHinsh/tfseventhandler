Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Clients.Proxy

    Friend Class TeamServersClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ITeamServers)
        Implements Services.Contracts.ITeamServers

        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Friend Sub AddServer(ByVal TeamServer As TeamServerItem) Implements Services.Contracts.ITeamServers.AddServer
            MyBase.Channel.AddServer(TeamServer)
        End Sub

        Friend Sub RefreshServers() Implements Services.Contracts.ITeamServers.RefreshServers
            MyBase.Channel.RefreshServers()
        End Sub

        Friend Sub RemoveServer(ByVal TeamServer As TeamServerItem) Implements Services.Contracts.ITeamServers.RemoveServer
            MyBase.Channel.RemoveServer(TeamServer)
        End Sub

        Friend Function ServceUrl() As System.Uri Implements Services.Contracts.ITeamServers.ServceUrl
            Return MyBase.Channel.ServceUrl()
        End Function

        Public Sub RefreshServer(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ITeamServers.RefreshServer
            MyBase.Channel.RefreshServer(TeamServer)
        End Sub

    End Class

End Namespace
