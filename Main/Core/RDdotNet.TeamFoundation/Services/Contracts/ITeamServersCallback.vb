Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Contracts


    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITeamServersCallback

        <OperationContract(IsOneWay:=True)> _
        Sub StatusChange(ByVal StatusChangeType As StatusChangeTypeEnum, ByVal TeamServer As TeamServerItem)
        <OperationContract(IsOneWay:=True)> _
        Sub ErrorOccured(ByVal ex As Exception)

    End Interface

End Namespace