Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Events
Imports Microsoft.TeamFoundation

Namespace Services.Contracts



    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract(CallbackContract:=GetType(ISubscriptionsCallback), Namespace:="http://schemas.rddotnet.com/TeamFoundation/2005/06/Services/SubscriptionAdmin")> _
    Public Interface ISubscriptions

        <OperationContract(isOneWay:=True)> _
        Sub AddSubscriptions(ByVal TeamServerName As String, ByVal EventType As EventTypes)

        <OperationContract(isOneWay:=True)> _
        Sub RemoveSubscriptions(ByVal TeamServerName As String)

        <OperationContract()> _
        Function GetSubscriptions(ByVal TeamServerName As String) As Collection(Of DataContracts.Subscription)

        <OperationContract(IsOneWay:=False)> _
      Function EventServiceUrl(ByVal EventType As EventTypes) As Uri

    End Interface

End Namespace

