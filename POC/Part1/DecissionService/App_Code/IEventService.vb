Imports System.ServiceModel

<ServiceContract()> _
Public Interface IEventService

    <OperationContract()> _
    Sub DoWork()

End Interface
