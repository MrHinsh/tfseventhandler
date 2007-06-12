Public Class ExplorerAdminControl

    Public Shadows ReadOnly Property Parent() As UI.FormControls.ExplorerControl
        Get
            Return CType(MyBase.Parent, UI.FormControls.ExplorerControl)
        End Get
    End Property

End Class
