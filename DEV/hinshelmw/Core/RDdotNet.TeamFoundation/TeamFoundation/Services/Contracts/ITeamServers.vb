Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace TeamFoundation.Services.Contracts


    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <RDdotNetServiceContract()> _
    <ServiceContract(CallbackContract:=GetType(ITeamServersCallback), Namespace:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03")> _
    Public Interface ITeamServers

        <OperationContract(IsOneWay:=True)> _
        Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String)

        <OperationContract(IsOneWay:=True)> _
        Sub RemoveServer(ByVal TeamServerName As String)

        <OperationContract(IsOneWay:=False)> _
        Function ServceUrl() As Uri

        <OperationContract(IsOneWay:=False)> _
        Function GetServers() As String()

    End Interface


End Namespace