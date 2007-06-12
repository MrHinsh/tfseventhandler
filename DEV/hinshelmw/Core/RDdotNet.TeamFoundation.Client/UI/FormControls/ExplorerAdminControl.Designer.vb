<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExplorerAdminControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExplorerAdminControl))
        Me.uxToolStrip = New System.Windows.Forms.ToolStrip
        Me.uxToolStripButtonRefresh = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.uxTreeView = New System.Windows.Forms.TreeView
        Me.uxToolStripButtonConnect = New System.Windows.Forms.ToolStripButton
        Me.uxToolStrip.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxToolStrip
        '
        Me.uxToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.uxToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.uxToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripButtonConnect, Me.uxToolStripButtonRefresh})
        Me.uxToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.uxToolStrip.Name = "uxToolStrip"
        Me.uxToolStrip.Padding = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.uxToolStrip.Size = New System.Drawing.Size(255, 25)
        Me.uxToolStrip.Stretch = True
        Me.uxToolStrip.TabIndex = 0
        '
        'uxToolStripButtonRefresh
        '
        Me.uxToolStripButtonRefresh.Image = CType(resources.GetObject("uxToolStripButtonRefresh.Image"), System.Drawing.Image)
        Me.uxToolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonRefresh.Name = "uxToolStripButtonRefresh"
        Me.uxToolStripButtonRefresh.Size = New System.Drawing.Size(65, 21)
        Me.uxToolStripButtonRefresh.Text = "Refresh"
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.AutoScroll = True
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.uxTreeView)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(255, 336)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(255, 361)
        Me.ToolStripContainer1.TabIndex = 2
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.uxToolStrip)
        '
        'uxTreeView
        '
        Me.uxTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTreeView.Location = New System.Drawing.Point(0, 0)
        Me.uxTreeView.Name = "uxTreeView"
        Me.uxTreeView.Size = New System.Drawing.Size(255, 336)
        Me.uxTreeView.TabIndex = 0
        '
        'uxToolStripButtonConnect
        '
        Me.uxToolStripButtonConnect.Image = CType(resources.GetObject("uxToolStripButtonConnect.Image"), System.Drawing.Image)
        Me.uxToolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonConnect.Name = "uxToolStripButtonConnect"
        Me.uxToolStripButtonConnect.Size = New System.Drawing.Size(67, 21)
        Me.uxToolStripButtonConnect.Text = "Connect"
        '
        'ExplorerAdminControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Name = "ExplorerAdminControl"
        Me.Size = New System.Drawing.Size(255, 361)
        Me.uxToolStrip.ResumeLayout(False)
        Me.uxToolStrip.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents uxToolStripButtonRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents uxTreeView As System.Windows.Forms.TreeView
    Friend WithEvents uxToolStripButtonConnect As System.Windows.Forms.ToolStripButton

End Class
