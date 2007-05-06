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
        Me.TFSEventHandlerControl = New RDdotNet.TeamFoundation.TestApp.TFSEventHandlerControl
        Me.SuspendLayout()
        '
        'TFSEventHandlerControl
        '
        Me.TFSEventHandlerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TFSEventHandlerControl.Location = New System.Drawing.Point(0, 0)
        Me.TFSEventHandlerControl.Name = "TFSEventHandlerControl"
        Me.TFSEventHandlerControl.Size = New System.Drawing.Size(554, 421)
        Me.TFSEventHandlerControl.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 421)
        Me.Controls.Add(Me.TFSEventHandlerControl)
        Me.Name = "Form1"
        Me.Text = "TFS Event Handler Explorer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TFSEventHandlerControl As TFSEventHandlerControl

End Class
