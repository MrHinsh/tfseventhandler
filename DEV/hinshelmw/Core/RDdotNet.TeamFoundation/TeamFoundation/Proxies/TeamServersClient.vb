Namespace TeamFoundation.Proxies

    Friend Class TeamServersClient
        Inherits System.ServiceModel.DuplexClientBase(Of Services.Contracts.ITeamServers)
        Implements Services.Contracts.ITeamServers

        Friend Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub

        Friend Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements Services.Contracts.ITeamServers.AddServer
            MyBase.Channel.AddServer(TeamServerName, TeamServerUri)
        End Sub

        Friend Function GetServers() As String() Implements Services.Contracts.ITeamServers.GetServers
            Return MyBase.Channel.GetServers
        End Function

        Friend Sub RemoveServer(ByVal TeamServerName As String) Implements Services.Contracts.ITeamServers.RemoveServer
            MyBase.Channel.RemoveServer(TeamServerName)
        End Sub

        Friend Function ServceUrl() As System.Uri Implements Services.Contracts.ITeamServers.ServceUrl
            Return MyBase.Channel.ServceUrl()
        End Function

    End Class

End Namespace