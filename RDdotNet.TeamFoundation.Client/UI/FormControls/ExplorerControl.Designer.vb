Namespace UI.FormControls



    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ExplorerControl
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExplorerControl))
            Me.uxTreeView = New System.Windows.Forms.TreeView
            Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
            Me.uxToolStrip = New System.Windows.Forms.ToolStrip
            Me.uxToolStripButtonRefresh = New System.Windows.Forms.ToolStripButton
            Me.uxToolStripButtonConfig = New System.Windows.Forms.ToolStripButton
            Me.uxToolStripButtonConnect = New System.Windows.Forms.ToolStripButton
            Me.ToolStripContainer1.ContentPanel.SuspendLayout()
            Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
            Me.ToolStripContainer1.SuspendLayout()
            Me.uxToolStrip.SuspendLayout()
            Me.SuspendLayout()
            '
            'uxTreeView
            '
            Me.uxTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.uxTreeView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.uxTreeView.Location = New System.Drawing.Point(0, 0)
            Me.uxTreeView.Name = "uxTreeView"
            Me.uxTreeView.Size = New System.Drawing.Size(255, 231)
            Me.uxTreeView.TabIndex = 0
            '
            'ToolStripContainer1
            '
            '
            'ToolStripContainer1.ContentPanel
            '
            Me.ToolStripContainer1.ContentPanel.AutoScroll = True
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.uxTreeView)
            Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(255, 231)
            Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStripContainer1.Name = "ToolStripContainer1"
            Me.ToolStripContainer1.Size = New System.Drawing.Size(255, 256)
            Me.ToolStripContainer1.TabIndex = 1
            Me.ToolStripContainer1.Text = "ToolStripContainer1"
            '
            'ToolStripContainer1.TopToolStripPanel
            '
            Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.uxToolStrip)
            '
            'uxToolStrip
            '
            Me.uxToolStrip.Dock = System.Windows.Forms.DockStyle.None
            Me.uxToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.uxToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripButtonRefresh, Me.uxToolStripButtonConfig, Me.uxToolStripButtonConnect})
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
            'uxToolStripButtonConfig
            '
            Me.uxToolStripButtonConfig.Image = CType(resources.GetObject("uxToolStripButtonConfig.Image"), System.Drawing.Image)
            Me.uxToolStripButtonConfig.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.uxToolStripButtonConfig.Name = "uxToolStripButtonConfig"
            Me.uxToolStripButtonConfig.Size = New System.Drawing.Size(58, 21)
            Me.uxToolStripButtonConfig.Text = "Config"
            '
            'uxToolStripButtonConnect
            '
            Me.uxToolStripButtonConnect.Image = CType(resources.GetObject("uxToolStripButtonConnect.Image"), System.Drawing.Image)
            Me.uxToolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.uxToolStripButtonConnect.Name = "uxToolStripButtonConnect"
            Me.uxToolStripButtonConnect.Size = New System.Drawing.Size(67, 21)
            Me.uxToolStripButtonConnect.Text = "Connect"
            '
            'ExplorerControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.ToolStripContainer1)
            Me.Name = "ExplorerControl"
            Me.Size = New System.Drawing.Size(255, 256)
            Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
            Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
            Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
            Me.ToolStripContainer1.ResumeLayout(False)
            Me.ToolStripContainer1.PerformLayout()
            Me.uxToolStrip.ResumeLayout(False)
            Me.uxToolStrip.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents uxTreeView As System.Windows.Forms.TreeView
        Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
        Friend WithEvents uxToolStrip As System.Windows.Forms.ToolStrip
        Friend WithEvents uxToolStripButtonRefresh As System.Windows.Forms.ToolStripButton
        Friend WithEvents uxToolStripButtonConfig As System.Windows.Forms.ToolStripButton
        Friend WithEvents uxToolStripButtonConnect As System.Windows.Forms.ToolStripButton

    End Class

End Namespace