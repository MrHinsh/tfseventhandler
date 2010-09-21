Imports System.ServiceModel
Imports System.ServiceModel.Activation

<ServiceContract(Namespace:="")>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class RegistrationService

    <OperationContract()>
    Public Function GetEventHandlers()
        ' Add your operation implementation here
    End Function

    ' Add more operations here and mark them with <OperationContract()>

End Class
