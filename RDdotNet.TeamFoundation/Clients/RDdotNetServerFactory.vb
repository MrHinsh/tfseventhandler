Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Clients

    Public Class RDdotNetServerFactory

#Region " Singleton "

        Private Shared _Instance As RDdotNetServerFactory

        Private Shared ReadOnly Property Instance() As RDdotNetServerFactory
            Get
                If _Instance Is Nothing Then
                    _Instance = New RDdotNetServerFactory
                End If
                Return _Instance
            End Get
        End Property

        Private Sub New()

        End Sub

#End Region

        Public ConnectedServers As New System.Collections.Generic.Dictionary(Of Uri, RDdotNetServer)

        Public Shared Function GetServer(ByVal Server As Uri) As RDdotNetServer
            If Not Instance.ConnectedServers.ContainsKey(Server) Then
                Instance.ConnectedServers.Add(Server, New RDdotNetServer(Server))
            End If
            Return Instance.ConnectedServers(Server)
        End Function

    End Class


End Namespace