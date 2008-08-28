Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Contracts


    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract(CallbackContract:=GetType(ITeamServersCallback), Namespace:="http://schemas.rddotnet.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03")> _
    Public Interface ITeamServers

        <OperationContract(IsOneWay:=True)> _
        Sub AddServer(ByVal TeamServer As TeamServerItem)

        <OperationContract(IsOneWay:=True)> _
        Sub RemoveServer(ByVal TeamServer As TeamServerItem)

        <OperationContract(IsOneWay:=False)> _
        Function ServceUrl() As Uri

        <OperationContract(IsOneWay:=True)> _
        Sub RefreshServers()

        <OperationContract(IsOneWay:=True)> _
        Sub RefreshServer(ByVal TeamServer As TeamServerItem)

    End Interface


End Namespace
