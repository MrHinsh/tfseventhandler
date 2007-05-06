<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TFSEventHandlerControl
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("https://tfs03.codeplex.com:443")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("http://team.worldnet.ml.com")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Team Servers", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node9")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node10")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node11")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Subscriptions", New System.Windows.Forms.TreeNode() {TreeNode4, TreeNode5, TreeNode6})
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node14")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Demo1", New System.Windows.Forms.TreeNode() {TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Demo2")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Assemblies", New System.Windows.Forms.TreeNode() {TreeNode9, TreeNode10})
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Handler Services", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode7, TreeNode11})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TFSEventHandlerControl))
        Me.uxTreeView = New System.Windows.Forms.TreeView
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.uxToolStrip = New System.Windows.Forms.ToolStrip
        Me.uxToolStripButtonRefresh = New System.Windows.Forms.ToolStripButton
        Me.uxToolStripButtonAdd = New System.Windows.Forms.ToolStripButton
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
        TreeNode1.Name = "Node7"
        TreeNode1.Text = "https://tfs03.codeplex.com:443"
        TreeNode2.Name = "Node8"
        TreeNode2.Text = "http://team.worldnet.ml.com"
        TreeNode3.Name = "Node1"
        TreeNode3.Text = "Team Servers"
        TreeNode4.Name = "Node9"
        TreeNode4.Text = "Node9"
        TreeNode5.Name = "Node10"
        TreeNode5.Text = "Node10"
        TreeNode6.Name = "Node11"
        TreeNode6.Text = "Node11"
        TreeNode7.Name = "Node2"
        TreeNode7.Text = "Subscriptions"
        TreeNode8.Name = "Node14"
        TreeNode8.Text = "Node14"
        TreeNode9.Name = "Node12"
        TreeNode9.Text = "Demo1"
        TreeNode10.Name = "Node13"
        TreeNode10.Text = "Demo2"
        TreeNode11.Name = "Node3"
        TreeNode11.Text = "Assemblies"
        TreeNode12.Name = "Handler Services"
        TreeNode12.Text = "Handler Services"
        Me.uxTreeView.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode12})
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
        Me.uxToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripButtonAdd, Me.uxToolStripButtonRefresh})
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
        Me.uxToolStripButtonRefresh.Size = New System.Drawing.Size(66, 21)
        Me.uxToolStripButtonRefresh.Text = "Refresh"
        '
        'uxToolStripButtonAdd
        '
        Me.uxToolStripButtonAdd.Image = CType(resources.GetObject("uxToolStripButtonAdd.Image"), System.Drawing.Image)
        Me.uxToolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonAdd.Name = "uxToolStripButtonAdd"
        Me.uxToolStripButtonAdd.Size = New System.Drawing.Size(49, 21)
        Me.uxToolStripButtonAdd.Text = "Add"
        '
        'TFSEventHandlerControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Name = "TFSEventHandlerControl"
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
    Friend WithEvents uxToolStripButtonAdd As System.Windows.Forms.ToolStripButton

End Class
