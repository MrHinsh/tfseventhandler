Imports Hinshelwood.TeamFoundation.Config

Public Class UrlReplacementListViewModel
     Inherits SettingsViewModel

    Public Sub New(ByVal UrlReplacementItems As UrlReplacementItemCollection)
        MyBase.New(New HeaderViewModel("Url Replacements", "Replace"))

    End Sub


End Class
