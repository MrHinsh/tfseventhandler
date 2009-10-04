Imports Hinshelwood.TeamFoundation.Config

Public Class UrlReplacementViewModel
    Inherits SettingsViewModel

    Public Sub New(ByVal urlReplacement As UrlReplacementItemElement)
        MyBase.New(New HeaderViewModel(urlReplacement.Old))
    End Sub

End Class
