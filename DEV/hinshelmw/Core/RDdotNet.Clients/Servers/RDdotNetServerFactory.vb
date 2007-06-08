Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Namespace Servers

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

        Private Servers As New System.Collections.Generic.Dictionary(Of Uri, RDdotNetServerBase)

        Public Shared Function GetServer(Of TRDdotNetServer As {New, RDdotNetServerBase})(ByVal Server As Uri) As TRDdotNetServer
            If Not Instance.Servers.ContainsKey(Server) Then
                Try
                    Dim RDdotNetServer As New TRDdotNetServer()
                    RDdotNetServer.SetUri(Server)
                    If RDdotNetServer.Authenticated Then
                        Instance.Servers.Add(Server, RDdotNetServer)
                    Else
                        Throw New InvalidOperationException
                    End If
                Catch ex As Exception
                    Throw New InvalidOperationException
                End Try
            End If
            Return CType(Instance.Servers(Server), TRDdotNetServer)
        End Function

    End Class


End Namespace