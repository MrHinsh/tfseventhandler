Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel


''' <summary>
''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
''' </summary>
''' <remarks></remarks>
Public Interface INotificationAdminCallback

    <OperationContract(IsOneWay:=True)> _
    Sub ForNoReason()

End Interface
