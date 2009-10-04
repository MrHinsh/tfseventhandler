Imports Hinshelwood.TeamFoundation.Config

Public Class EventHandlerListViewModel
    Inherits SettingsViewModel

    Public Sub New(ByVal EventItems As EventItemCollection)
        MyBase.New(New HeaderViewModel("Event Handlers"))

    End Sub


End Class
