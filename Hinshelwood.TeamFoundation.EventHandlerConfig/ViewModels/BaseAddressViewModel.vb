Imports Hinshelwood.TeamFoundation.Config

Public Class BaseAddressViewModel
    Inherits SettingsViewModel


    Public Sub New(ByVal BaseAddress As BaseAddressItemElement)
        MyBase.New(New HeaderViewModel("Base Address", "Address"))

    End Sub


    
End Class
