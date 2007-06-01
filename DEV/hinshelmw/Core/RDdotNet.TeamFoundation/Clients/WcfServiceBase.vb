Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Clients

    Public MustInherit Class WcfServiceBase(Of TClient As {Class}, TBinding As Channels.Binding)
        Implements IService

        Public ReadOnly Property ServiceType() As ServiceTypes Implements IService.ServiceType
            Get
                Return ServiceTypes.Wcf
            End Get
        End Property

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

        Public ReadOnly Property Contracts() As System.Type() Implements IService.Contracts
            Get
                Dim a As New ArrayList
                For Each t As Type In Me.GetType.GetInterfaces
                    If t.IsInterface And t.GetCustomAttributes(GetType(RDdotNetServiceContractAttribute), True).Length > 0 Then
                        a.Add(t)
                    End If
                Next
                Return a.ToArray(GetType(System.Type))
            End Get
        End Property

        Public ReadOnly Property ServiceName() As String Implements IService.ServiceName
            Get
                Return Me.GetType.Name
            End Get
        End Property

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

        Public Function Authenticated() As Boolean Implements IService.Authenticated
            Throw New NotImplementedException
        End Function


    End Class

End Namespace
