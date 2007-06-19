Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace TeamFoundation.Services.Contracts


    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ClientServiceContractAttribute()> _
    <ServiceContract()> _
    Public Interface IEvents

        <OperationContract(IsOneWay:=True)> _
        Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo)

        <OperationContract(IsOneWay:=True)> _
        Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo)

        <OperationContract(IsOneWay:=True)> _
        Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo)

    End Interface



End Namespace