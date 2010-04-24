Imports Hinshelwood.TeamFoundation.Config

Public Class HandlerViewModel
    Inherits SettingsViewModel

    Public Sub New(ByVal eventHandler As HandlerItemElement)
        MyBase.New(New HeaderViewModel(eventHandler.Type.ToString))
    End Sub

End Class
