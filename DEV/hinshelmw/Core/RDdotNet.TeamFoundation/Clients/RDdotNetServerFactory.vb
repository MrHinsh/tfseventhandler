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

        Private Servers As New System.Collections.Generic.Dictionary(Of Uri, RDdotNetServer)

        Public Shared Function GetServer(ByVal Server As Uri) As RDdotNetServer
            If Not Instance.Servers.ContainsKey(Server) Then
                Try
                    Dim RDdotNetServer As New RDdotNetServer(Server)
                    If RDdotNetServer.Authenticated Then
                        Instance.Servers.Add(Server, RDdotNetServer)
                    Else
                        Throw New InvalidOperationException
                    End If
                Catch ex As Exception
                    Throw New InvalidOperationException
                End Try
            End If
            Return Instance.Servers(Server)
        End Function

    End Class


End Namespace