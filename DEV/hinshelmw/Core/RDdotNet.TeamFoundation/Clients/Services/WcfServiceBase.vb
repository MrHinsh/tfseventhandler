Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Clients

    Public MustInherit Class WcfServiceBase(Of TClient As {Class}, TBinding As Channels.Binding)
        Inherits ServiceBase

        Private _Server As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")
        Private _Location As String = "WcfServiceBase"

        Public ReadOnly Property Server() As Uri
            Get
                Return _Server
            End Get
        End Property

        Public ReadOnly Property Location() As String
            Get
                Return _Location
            End Get
        End Property

        Public ReadOnly Property EndPoint() As EndpointAddress
            Get
                Return New EndpointAddress(New Uri(_Server, _Location))
            End Get
        End Property

        Private _Client As TClient

        Friend ReadOnly Property Client() As TClient
            Get
                If _Client Is Nothing Then
                    _Client = GetClient()
                End If
                Return _Client
            End Get
        End Property

        Friend MustOverride Function GetBinding() As TBinding
        Friend MustOverride Function GetClient() As TClient

        Public Sub New(ByVal Server As Uri, ByVal Location As String)
            If Not Server Is Nothing Then
                _Server = Server
            End If
            If Not Location = "" Then
                _Location = Location
            Else
                Throw New InvalidOperationException("You must provide a valid location")
            End If
        End Sub

    End Class

End Namespace
