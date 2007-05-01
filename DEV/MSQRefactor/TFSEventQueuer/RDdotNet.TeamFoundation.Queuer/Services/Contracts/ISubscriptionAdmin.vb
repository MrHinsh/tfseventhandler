Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Services.Contracts



    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract(CallbackContract:=GetType(ISubscriptionAdminCallback), Namespace:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin")> _
    Public Interface ISubscriptionAdmin

        <OperationContract(IsOneWay:=True)> _
        Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As EventTypes)

        <OperationContract(IsOneWay:=True)> _
        Sub RemoveSubscriptions(ByVal ServiceUrl As String)

        <OperationContract(IsOneWay:=False)> _
        Function GetSubscriptions() As Collection(Of DataContracts.Subscription)


    End Interface

End Namespace

