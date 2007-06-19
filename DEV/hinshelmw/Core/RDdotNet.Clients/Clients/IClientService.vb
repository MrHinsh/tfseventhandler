Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Clients

    Public Interface IClientService

        ReadOnly Property ServiceType() As ClientServiceTypes
        ReadOnly Property ServiceName() As String
        ReadOnly Property Server() As Servers.IClientServer
        ReadOnly Property Contracts() As Collection(Of Type)
        Function Authenticated() As Boolean
        Sub Start()
        Sub [Stop]()

    End Interface

    Public Enum ClientServiceTypes
        Local
        Remote
    End Enum

End Namespace
