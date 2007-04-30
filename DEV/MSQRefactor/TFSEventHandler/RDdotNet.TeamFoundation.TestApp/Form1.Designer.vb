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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.TeamServerAdminControl1 = New RDdotNet.TeamFoundation.Client.TeamServerAdminControl
        Me.SubscriptionAdminControl1 = New RDdotNet.TeamFoundation.Client.SubscriptionAdminControl
        Me.EventHandlerAdminControl1 = New RDdotNet.TeamFoundation.Client.EventHandlerAdminControl
        Me.uxContextMenuStripDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxContextMenuStripDetails
        '
        Me.uxContextMenuStripDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripMenuItemViewasText})
        Me.uxContextMenuStripDetails.Name = "uxContextMenuStripDetails"
        Me.uxContextMenuStripDetails.Size = New System.Drawing.Size(136, 26)
        '
        'uxToolStripMenuItemViewasText
        '
        Me.uxToolStripMenuItemViewasText.Name = "uxToolStripMenuItemViewasText"
        Me.uxToolStripMenuItemViewasText.Size = New System.Drawing.Size(135, 22)
        Me.uxToolStripMenuItemViewasText.Text = "View as Text"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TeamServerAdminControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SubscriptionAdminControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EventHandlerAdminControl1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(554, 421)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'TeamServerAdminControl1
        '
        Me.TeamServerAdminControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TeamServerAdminControl1.Location = New System.Drawing.Point(3, 3)
        Me.TeamServerAdminControl1.Name = "TeamServerAdminControl1"
        Me.TeamServerAdminControl1.Size = New System.Drawing.Size(548, 134)
        Me.TeamServerAdminControl1.TabIndex = 0
        '
        'SubscriptionAdminControl1
        '
        Me.SubscriptionAdminControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubscriptionAdminControl1.Location = New System.Drawing.Point(3, 143)
        Me.SubscriptionAdminControl1.Name = "SubscriptionAdminControl1"
        Me.SubscriptionAdminControl1.Size = New System.Drawing.Size(548, 134)
        Me.SubscriptionAdminControl1.TabIndex = 1
        '
        'EventHandlerAdminControl1
        '
        Me.EventHandlerAdminControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EventHandlerAdminControl1.Location = New System.Drawing.Point(3, 283)
        Me.EventHandlerAdminControl1.Name = "EventHandlerAdminControl1"
        Me.EventHandlerAdminControl1.Size = New System.Drawing.Size(548, 135)
        Me.EventHandlerAdminControl1.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 421)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.uxContextMenuStripDetails.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents uxContextMenuStripDetails As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents uxToolStripMenuItemViewasText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TeamServerAdminControl1 As RDdotNet.TeamFoundation.Client.TeamServerAdminControl
    Friend WithEvents SubscriptionAdminControl1 As RDdotNet.TeamFoundation.Client.SubscriptionAdminControl
    Friend WithEvents EventHandlerAdminControl1 As RDdotNet.TeamFoundation.Client.EventHandlerAdminControl


End Class
