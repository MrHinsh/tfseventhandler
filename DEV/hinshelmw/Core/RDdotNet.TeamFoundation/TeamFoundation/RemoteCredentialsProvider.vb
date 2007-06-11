Namespace TeamFoundation


    Public Class RemoteCredentialsProvider
        Implements Microsoft.TeamFoundation.Client.ICredentialsProvider

        Private _EventHandlerClient As Servers.TFSEventHandlerServer

        Public ReadOnly Property EventHandlerClient() As Servers.TFSEventHandlerServer
            Get
                If _EventHandlerClient Is Nothing Then
                    _EventHandlerClient = New Servers.TFSEventHandlerServer()
                End If
                Return _EventHandlerClient
            End Get
        End Property

        Public Function GetCredentials(ByVal uri As System.Uri, ByVal failedCredentials As System.Net.ICredentials) As System.Net.ICredentials Implements Microsoft.TeamFoundation.Client.ICredentialsProvider.GetCredentials
            Return EventHandlerClient.TeamServersService.GetCredentials(uri, failedCredentials)
        End Function

        Public Sub NotifyCredentialsAuthenticated(ByVal uri As System.Uri) Implements Microsoft.TeamFoundation.Client.ICredentialsProvider.NotifyCredentialsAuthenticated

        End Sub

    End Class


End Namespace