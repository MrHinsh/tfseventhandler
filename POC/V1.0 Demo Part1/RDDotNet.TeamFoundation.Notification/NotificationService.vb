' NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
Public Class NotificationService
    Implements INotification

    Public Function GetData(ByVal value As Integer) As String Implements INotification.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements INotification.GetDataUsingDataContract
        If composite.BoolValue Then
            composite.StringValue = (composite.StringValue & "Suffix")
        End If
        Return composite
    End Function

End Class
