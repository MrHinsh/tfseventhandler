Imports System.ServiceModel

<ServiceContract()> _
Public Interface INotificationService

    <OperationContract()> _
    Sub DoWork()

End Interface
