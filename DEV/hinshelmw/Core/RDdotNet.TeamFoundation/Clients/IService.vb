Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Clients

    Public Interface IService

        ReadOnly Property ServiceType() As ServiceTypes
        ReadOnly Property ServiceName() As String
        ReadOnly Property Contracts() As Type()
        Function Authenticated() As Boolean

    End Interface

    Public Enum ServiceTypes
        Wcf
        Storage
        Logic
    End Enum

End Namespace
