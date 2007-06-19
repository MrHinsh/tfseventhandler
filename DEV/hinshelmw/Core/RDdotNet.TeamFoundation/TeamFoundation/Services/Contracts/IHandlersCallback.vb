Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace TeamFoundation.Services.Contracts

    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    <ClientServiceContractAttribute()> _
    Public Interface IHandlersCallback

        <OperationContract(IsOneWay:=True)> _
      Sub Updated(ByVal AssemblyManaifest As AssemblyManaifest)

    End Interface


End Namespace