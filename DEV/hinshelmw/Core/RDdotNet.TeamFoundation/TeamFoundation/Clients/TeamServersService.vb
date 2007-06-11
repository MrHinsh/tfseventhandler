Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports Microsoft.TeamFoundation.Client

Namespace TeamFoundation.Clients

    Public Delegate Sub TeamServersUpdated(ByVal TeamServers() As String)

    Public Class TeamServersService
        Inherits RDdotNet.Clients.WcfServiceBase(Of Proxies.TeamServersClient, WSDualHttpBinding)
        Implements Services.Contracts.ITeamServers
        Implements Services.Contracts.ITeamServersCallback

        Public Event TeamServersUpdated As TeamServersUpdated

#Region " WcfServiceBase "

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            MyBase.New(Server, "RDdotNet/TFSEventHandler/Queuer/TeamServers")
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

        Protected Overrides Function GetClient() As Proxies.TeamServersClient
            Return New Proxies.TeamServersClient(New InstanceContext(Me), GetBinding, Me.EndPoint)
        End Function

#End Region

#Region " ITeamServers "

        Public Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements Services.Contracts.ITeamServers.AddServer
            MyBase.Client.AddServer(TeamServerName, TeamServerUri)
        End Sub

        Public Function GetServers() As String() Implements Services.Contracts.ITeamServers.GetServers
            Return MyBase.Client.GetServers()
        End Function

        Public Sub RemoveServer(ByVal TeamServerName As String) Implements Services.Contracts.ITeamServers.RemoveServer
            MyBase.Client.RemoveServer(TeamServerName)
        End Sub

        Public Function ServceUrl() As System.Uri Implements Services.Contracts.ITeamServers.ServceUrl
            Return MyBase.Client.ServceUrl()
        End Function

#End Region

#Region " ITeamServersCallback "

        Public Sub Updated(ByVal TeamServers() As String) Implements Services.Contracts.ITeamServersCallback.Updated
            RaiseEvent TeamServersUpdated(TeamServers)
        End Sub

        Public Function GetCredentials(ByVal uri As System.Uri, ByVal failedCredentials As System.Net.ICredentials) As System.Net.ICredentials Implements Services.Contracts.ITeamServersCallback.GetCredentials
            Return New Net.NetworkCredential("hinshelwm", "3v4Ng3l1n4", "EMEA")

        End Function

        Public Sub NotifyCredentialsAuthenticated(ByVal uri As System.Uri) Implements Services.Contracts.ITeamServersCallback.NotifyCredentialsAuthenticated

        End Sub

#End Region


   
    End Class

End Namespace
