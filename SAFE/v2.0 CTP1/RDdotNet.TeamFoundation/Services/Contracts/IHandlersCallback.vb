Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Contracts

    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IHandlersCallback

        <OperationContract(IsOneWay:=True)> _
      Sub Updated(ByVal AssemblyManaifest As AssemblyManaifest)
        <OperationContract(IsOneWay:=True)> _
      Sub ErrorOccured(ByVal ex As Exception)

        '       <OperationContract(IsOneWay:=True)> _
        'Sub StatusChange(ByVal ex As Exception)

    End Interface


End Namespace