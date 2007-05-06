<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.uxContextMenuStripDetails = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.uxToolStripMenuItemViewasText = New System.Windows.Forms.ToolStripMenuItem
        Me.uxContextMenuStripDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxContextMenuStripDetails
        '
        Me.uxContextMenuStripDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripMenuItemViewasText})
        Me.uxContextMenuStripDetails.Name = "uxContextMenuStripDetails"
        Me.uxContextMenuStripDetails.Size = New System.Drawing.Size(139, 26)
        '
        'uxToolStripMenuItemViewasText
        '
        Me.uxToolStripMenuItemViewasText.Name = "uxToolStripMenuItemViewasText"
        Me.uxToolStripMenuItemViewasText.Size = New System.Drawing.Size(138, 22)
        Me.uxToolStripMenuItemViewasText.Text = "View as Text"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 421)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.uxContextMenuStripDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxContextMenuStripDetails As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents uxToolStripMenuItemViewasText As System.Windows.Forms.ToolStripMenuItem

End Class
