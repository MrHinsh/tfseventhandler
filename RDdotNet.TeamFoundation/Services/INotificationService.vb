Imports System.ServiceModel
Imports System.Runtime.Serialization

''' <summary>
''' This is the seervice contract for integrating with the Team Foundation Server notification events.
''' </summary>
''' <remarks></remarks>
<ServiceContract(Namespace:="http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/03")> _
Public Interface INotification

    ''' <summary>
    ''' The Notify method if fired whenever a subscribed event arrives.
    ''' </summary>
    ''' <param name="eventXml">This XML defignes the data that was changed on the event.</param>
    ''' <param name="tfsIdentityXml">This xml identifies the Team Foundation Server the event came from.</param>
    ''' <param name="SubscriptionInfo">Information about the subscriber</param>
    ''' <remarks></remarks>
    <OperationContract(Action:="http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/03/Notify")> _
    Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo)


End Interface

