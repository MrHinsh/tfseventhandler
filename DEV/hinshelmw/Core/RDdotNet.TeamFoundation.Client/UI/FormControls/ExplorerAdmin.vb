Public Class ExplorerAdmin

    Private _ExplorerControl As UI.FormControls.ExplorerControl

    Public ReadOnly Property ExplorerControl() As UI.FormControls.ExplorerControl
        Get
            Return _ExplorerControl
        End Get
    End Property

    Public Sub New(ByVal ExplorerControl As UI.FormControls.ExplorerControl)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ExplorerControl = ExplorerControl
    End Sub


End Class