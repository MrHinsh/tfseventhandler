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
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
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
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode12})
        Me.TreeView1.Size = New System.Drawing.Size(255, 256)
        Me.TreeView1.TabIndex = 0
        '
        'TFSEventHandlerControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "TFSEventHandlerControl"
        Me.Size = New System.Drawing.Size(255, 256)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView

End Class
