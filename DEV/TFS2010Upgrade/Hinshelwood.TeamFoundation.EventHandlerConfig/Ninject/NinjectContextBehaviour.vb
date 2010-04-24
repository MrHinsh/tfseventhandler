Public Class NinjectContextBehaviour

    Public Shared Function GetViewModel(ByVal element As DependencyObject) As Type
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        Return element.GetValue(ViewModelProperty)
    End Function

    Public Shared Sub SetViewModel(ByVal element As DependencyObject, ByVal value As Type)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        element.SetValue(ViewModelProperty, value)
    End Sub

    Public Shared ReadOnly ViewModelProperty As  _
                           DependencyProperty = DependencyProperty.RegisterAttached("ViewModel", _
                           GetType(Type), GetType(NinjectContextBehaviour), _
                           New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf NinjectContextBehaviour.ViewModelChanged)))

    Private Shared Sub ViewModelChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim frameworkElement As FrameworkElement = TryCast(obj, FrameworkElement)
        If (Not frameworkElement Is Nothing) Then
            Dim viewModelType As Type = GetViewModel(frameworkElement)
            frameworkElement.DataContext = My.Kernel.Get(viewModelType)
        Else
            Throw New ArgumentOutOfRangeException("NinjectContextBehaviour can only be applied to FrameworkElements")
        End If
    End Sub

End Class
