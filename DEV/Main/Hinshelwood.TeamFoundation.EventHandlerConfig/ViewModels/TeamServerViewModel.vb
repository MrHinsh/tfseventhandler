Imports Hinshelwood.TeamFoundation.Config

Public Class TeamServerViewModel
    Inherits SettingsViewModel

    Public Sub New(ByVal teamServer As TeamServerItemElement)
        MyBase.New(New HeaderViewModel(teamServer.Url.ToString, teamServer.Name))
    End Sub

End Class
