Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Events
Imports Microsoft.TeamFoundation
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Contracts



    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract(CallbackContract:=GetType(ISubscriptionsCallback), Namespace:="http://schemas.rddotnet.com/TeamFoundation/2005/06/Services/SubscriptionAdmin")> _
    Public Interface ISubscriptions

        <OperationContract(isOneWay:=True)> _
        Sub AddSubscriptions(ByVal TeamServer As TeamServerItem, ByVal EventType As EventTypes)

        <OperationContract(isOneWay:=True)> _
        Sub RemoveSubscription(ByVal TeamServer As TeamServerItem, ByVal Subscription As SubscriptionItem)

        <OperationContract(isOneWay:=True)> _
        Sub RemoveSubscriptions(ByVal TeamServer As TeamServerItem)

        <OperationContract(IsOneWay:=True)> _
        Sub RefreshSubscriptions()

        <OperationContract(IsOneWay:=True)> _
        Sub RefreshSubscription(ByVal TeamServer As TeamServerItem, ByVal Subscription As SubscriptionItem)

        <OperationContract(IsOneWay:=True)> _
        Sub RefreshServerSubscriptions(ByVal TeamServer As TeamServerItem)

        <OperationContract(IsOneWay:=False)> _
      Function EventServiceUrl(ByVal EventType As EventTypes) As Uri

    End Interface

End Namespace

