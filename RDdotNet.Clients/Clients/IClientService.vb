Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Clients

    Public Interface IClientService

        ReadOnly Property ServiceType() As ClientServiceTypes
        ReadOnly Property ServiceName() As String
        ReadOnly Property Contracts() As Type()
        Function Authenticated() As Boolean
        Sub Start()
        Sub [Stop]()

    End Interface

    Public Enum ClientServiceTypes
        Wcf
        Storage
        Logic
    End Enum

End Namespace
