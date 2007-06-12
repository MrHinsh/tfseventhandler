<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExplorerAdmin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.uxExplorerAdminControl = New RDdotNet.TeamFoundation.ExplorerAdminControl
        Me.SuspendLayout()
        '
        'uxExplorerAdminControl
        '
        Me.uxExplorerAdminControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxExplorerAdminControl.Location = New System.Drawing.Point(0, 0)
        Me.uxExplorerAdminControl.Name = "uxExplorerAdminControl"
        Me.uxExplorerAdminControl.Size = New System.Drawing.Size(337, 267)
        Me.uxExplorerAdminControl.TabIndex = 0
        Me.uxExplorerAdminControl.UseWaitCursor = True
        '
        'ExplorerAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 267)
        Me.Controls.Add(Me.uxExplorerAdminControl)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExplorerAdmin"
        Me.Text = "TFS Event Explorer Admin"
        Me.UseWaitCursor = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxExplorerAdminControl As ExplorerAdminControl
End Class
