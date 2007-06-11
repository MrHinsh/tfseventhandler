Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Clients

    Public MustInherit Class WcfServiceBase(Of TClient As {Class}, TBinding As Channels.Binding)
        Implements IClientService


        Public ReadOnly Property ServiceType() As ClientServiceTypes Implements IClientService.ServiceType
            Get
                Return ClientServiceTypes.Wcf
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

        Protected ReadOnly Property Client() As TClient
            Get
                If _Client Is Nothing Then
                    _Client = GetClient()
                End If
                Return _Client
            End Get
        End Property

        Protected MustOverride Function GetBinding() As TBinding
        Protected MustOverride Function GetClient() As TClient



        Public ReadOnly Property Contracts() As System.Collections.ObjectModel.Collection(Of System.Type) Implements IClientService.Contracts
            Get
                Dim ServiceContracts As New Collection(Of Type)
                For Each t As Type In Me.GetType.GetInterfaces
                    If t.IsInterface And t.GetCustomAttributes(GetType(RDdotNetServiceContractAttribute), True).Length > 0 Then
                        ServiceContracts.Add(t)
                    End If
                Next
                Return ServiceContracts
            End Get
        End Property

        Public ReadOnly Property ServiceName() As String Implements IClientService.ServiceName
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

        Public Function Authenticated() As Boolean Implements IClientService.Authenticated
            Throw New NotImplementedException
        End Function


        Public Sub Start() Implements IClientService.Start

        End Sub

        Public Sub [Stop]() Implements IClientService.Stop

        End Sub

   
    End Class

End Namespace
