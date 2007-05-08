Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

''' <summary>
''' This is the seervice contract for integrating with the Team Foundation Server notification events.
''' </summary>
''' <remarks></remarks>
<ServiceContract(CallbackContract:=GetType(INotificationAdminCallback), Namespace:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin")> _
Public Interface INotificationAdmin

    '<OperationContract(IsOneWay:=True)> _
    'Sub OneWayPaceholder()


    <OperationContract(IsOneWay:=False)> _
   Function GetEventType() As EventTypes

    <OperationContract(IsOneWay:=False)> _
      Function GetLocal() As String
End Interface

