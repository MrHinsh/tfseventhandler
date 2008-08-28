<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TeamServerAdminControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TeamServerAdminControl))
        Me.uxGroupBoxTeamServerAdmin = New System.Windows.Forms.GroupBox
        Me.uxTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.uxListBoxTeamServers = New System.Windows.Forms.ListBox
        Me.uxToolStripTeamServer = New System.Windows.Forms.ToolStrip
        Me.uxToolStripButtonConnect = New System.Windows.Forms.ToolStripButton
        Me.uxToolStripButtonDisconnect = New System.Windows.Forms.ToolStripButton
        Me.uxGroupBoxTeamServerAdmin.SuspendLayout()
        Me.uxTableLayoutPanel.SuspendLayout()
        Me.uxToolStripTeamServer.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxGroupBoxTeamServerAdmin
        '
        Me.uxGroupBoxTeamServerAdmin.Controls.Add(Me.uxTableLayoutPanel)
        Me.uxGroupBoxTeamServerAdmin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxTeamServerAdmin.Location = New System.Drawing.Point(0, 0)
        Me.uxGroupBoxTeamServerAdmin.Name = "uxGroupBoxTeamServerAdmin"
        Me.uxGroupBoxTeamServerAdmin.Size = New System.Drawing.Size(378, 247)
        Me.uxGroupBoxTeamServerAdmin.TabIndex = 0
        Me.uxGroupBoxTeamServerAdmin.TabStop = False
        Me.uxGroupBoxTeamServerAdmin.Text = "Team Server Admin"
        '
        'uxTableLayoutPanel
        '
        Me.uxTableLayoutPanel.ColumnCount = 2
        Me.uxTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.uxTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.uxTableLayoutPanel.Controls.Add(Me.uxListBoxTeamServers, 0, 1)
        Me.uxTableLayoutPanel.Controls.Add(Me.uxToolStripTeamServer, 0, 0)
        Me.uxTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTableLayoutPanel.Location = New System.Drawing.Point(3, 16)
        Me.uxTableLayoutPanel.Name = "uxTableLayoutPanel"
        Me.uxTableLayoutPanel.RowCount = 2
        Me.uxTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.uxTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.uxTableLayoutPanel.Size = New System.Drawing.Size(372, 228)
        Me.uxTableLayoutPanel.TabIndex = 0
        '
        'uxListBoxTeamServers
        '
        Me.uxListBoxTeamServers.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxTeamServers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxTableLayoutPanel.SetColumnSpan(Me.uxListBoxTeamServers, 2)
        Me.uxListBoxTeamServers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxTeamServers.FormattingEnabled = True
        Me.uxListBoxTeamServers.Location = New System.Drawing.Point(3, 28)
        Me.uxListBoxTeamServers.Name = "uxListBoxTeamServers"
        Me.uxListBoxTeamServers.Size = New System.Drawing.Size(366, 195)
        Me.uxListBoxTeamServers.TabIndex = 0
        '
        'uxToolStripTeamServer
        '
        Me.uxTableLayoutPanel.SetColumnSpan(Me.uxToolStripTeamServer, 2)
        Me.uxToolStripTeamServer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxToolStripTeamServer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.uxToolStripTeamServer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripButtonConnect, Me.uxToolStripButtonDisconnect})
        Me.uxToolStripTeamServer.Location = New System.Drawing.Point(0, 0)
        Me.uxToolStripTeamServer.Name = "uxToolStripTeamServer"
        Me.uxToolStripTeamServer.Size = New System.Drawing.Size(372, 25)
        Me.uxToolStripTeamServer.TabIndex = 1
        Me.uxToolStripTeamServer.Text = "ToolStrip1"
        '
        'uxToolStripButtonConnect
        '
        Me.uxToolStripButtonConnect.Image = CType(resources.GetObject("uxToolStripButtonConnect.Image"), System.Drawing.Image)
        Me.uxToolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonConnect.Name = "uxToolStripButtonConnect"
        Me.uxToolStripButtonConnect.Size = New System.Drawing.Size(67, 22)
        Me.uxToolStripButtonConnect.Text = "Connect"
        '
        'uxToolStripButtonDisconnect
        '
        Me.uxToolStripButtonDisconnect.Enabled = False
        Me.uxToolStripButtonDisconnect.Image = CType(resources.GetObject("uxToolStripButtonDisconnect.Image"), System.Drawing.Image)
        Me.uxToolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonDisconnect.Name = "uxToolStripButtonDisconnect"
        Me.uxToolStripButtonDisconnect.Size = New System.Drawing.Size(79, 22)
        Me.uxToolStripButtonDisconnect.Text = "Disconnect"
        '
        'TeamServerAdminControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.uxGroupBoxTeamServerAdmin)
        Me.Name = "TeamServerAdminControl"
        Me.Size = New System.Drawing.Size(378, 247)
        Me.uxGroupBoxTeamServerAdmin.ResumeLayout(False)
        Me.uxTableLayoutPanel.ResumeLayout(False)
        Me.uxTableLayoutPanel.PerformLayout()
        Me.uxToolStripTeamServer.ResumeLayout(False)
        Me.uxToolStripTeamServer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxGroupBoxTeamServerAdmin As System.Windows.Forms.GroupBox
    Friend WithEvents uxTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents uxListBoxTeamServers As System.Windows.Forms.ListBox
    Friend WithEvents uxToolStripTeamServer As System.Windows.Forms.ToolStrip
    Friend WithEvents uxToolStripButtonConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents uxToolStripButtonDisconnect As System.Windows.Forms.ToolStripButton

End Class
