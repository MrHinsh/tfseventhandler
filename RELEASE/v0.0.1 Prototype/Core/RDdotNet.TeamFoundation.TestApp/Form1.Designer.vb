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
        Dim uxListBoxEventing As System.Windows.Forms.ListBox
        Me.uxContextMenuStripDetails = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.uxToolStripMenuItemViewasText = New System.Windows.Forms.ToolStripMenuItem
        Me.ButtonStart = New System.Windows.Forms.Button
        Me.ButtonEnd = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.uxListBoxWorkItemChanged = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.uxGroupBoxTeamServer = New System.Windows.Forms.GroupBox
        Me.uxListBoxTeamServer = New System.Windows.Forms.ListBox
        Me.uxGroupBoxServiceHost = New System.Windows.Forms.GroupBox
        Me.uxListBoxServiceHost = New System.Windows.Forms.ListBox
        Me.uxGroupBoxCheckinHandlers = New System.Windows.Forms.GroupBox
        Me.uxListBoxCheckIn = New System.Windows.Forms.ListBox
        Me.uxGroupBoxEvents = New System.Windows.Forms.GroupBox
        Me.EndPointsGroupBox = New System.Windows.Forms.GroupBox
        Me.ux_ListBoxEndPoints = New System.Windows.Forms.ListBox
        uxListBoxEventing = New System.Windows.Forms.ListBox
        Me.uxContextMenuStripDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.uxGroupBoxTeamServer.SuspendLayout()
        Me.uxGroupBoxServiceHost.SuspendLayout()
        Me.uxGroupBoxCheckinHandlers.SuspendLayout()
        Me.uxGroupBoxEvents.SuspendLayout()
        Me.EndPointsGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxListBoxEventing
        '
        uxListBoxEventing.BackColor = System.Drawing.SystemColors.Control
        uxListBoxEventing.BorderStyle = System.Windows.Forms.BorderStyle.None
        uxListBoxEventing.ContextMenuStrip = Me.uxContextMenuStripDetails
        uxListBoxEventing.Dock = System.Windows.Forms.DockStyle.Fill
        uxListBoxEventing.FormattingEnabled = True
        uxListBoxEventing.HorizontalScrollbar = True
        uxListBoxEventing.Location = New System.Drawing.Point(3, 16)
        uxListBoxEventing.Name = "uxListBoxEventing"
        uxListBoxEventing.Size = New System.Drawing.Size(265, 182)
        uxListBoxEventing.TabIndex = 6
        '
        'uxContextMenuStripDetails
        '
        Me.uxContextMenuStripDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripMenuItemViewasText})
        Me.uxContextMenuStripDetails.Name = "uxContextMenuStripDetails"
        Me.uxContextMenuStripDetails.Size = New System.Drawing.Size(147, 26)
        '
        'uxToolStripMenuItemViewasText
        '
        Me.uxToolStripMenuItemViewasText.Name = "uxToolStripMenuItemViewasText"
        Me.uxToolStripMenuItemViewasText.Size = New System.Drawing.Size(146, 22)
        Me.uxToolStripMenuItemViewasText.Text = "View as Text"
        '
        'ButtonStart
        '
        Me.ButtonStart.Location = New System.Drawing.Point(9, 19)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(75, 23)
        Me.ButtonStart.TabIndex = 0
        Me.ButtonStart.Text = "Start"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(90, 19)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonEnd.TabIndex = 1
        Me.ButtonEnd.Text = "End"
        Me.ButtonEnd.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.uxGroupBoxTeamServer, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.uxGroupBoxServiceHost, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.uxGroupBoxCheckinHandlers, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.uxGroupBoxEvents, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EndPointsGroupBox, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(554, 421)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.uxListBoxWorkItemChanged)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 315)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(277, 106)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Handlers: WorkItemChanged"
        Me.GroupBox2.UseWaitCursor = True
        '
        'uxListBoxWorkItemChanged
        '
        Me.uxListBoxWorkItemChanged.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxWorkItemChanged.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxListBoxWorkItemChanged.ContextMenuStrip = Me.uxContextMenuStripDetails
        Me.uxListBoxWorkItemChanged.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxWorkItemChanged.FormattingEnabled = True
        Me.uxListBoxWorkItemChanged.HorizontalScrollbar = True
        Me.uxListBoxWorkItemChanged.Location = New System.Drawing.Point(3, 16)
        Me.uxListBoxWorkItemChanged.Name = "uxListBoxWorkItemChanged"
        Me.uxListBoxWorkItemChanged.Size = New System.Drawing.Size(271, 78)
        Me.uxListBoxWorkItemChanged.TabIndex = 5
        Me.uxListBoxWorkItemChanged.UseWaitCursor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonStart)
        Me.GroupBox1.Controls.Add(Me.ButtonEnd)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(271, 99)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'uxGroupBoxTeamServer
        '
        Me.uxGroupBoxTeamServer.Controls.Add(Me.uxListBoxTeamServer)
        Me.uxGroupBoxTeamServer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxTeamServer.Location = New System.Drawing.Point(3, 108)
        Me.uxGroupBoxTeamServer.Name = "uxGroupBoxTeamServer"
        Me.uxGroupBoxTeamServer.Padding = New System.Windows.Forms.Padding(0)
        Me.uxGroupBoxTeamServer.Size = New System.Drawing.Size(271, 99)
        Me.uxGroupBoxTeamServer.TabIndex = 7
        Me.uxGroupBoxTeamServer.TabStop = False
        Me.uxGroupBoxTeamServer.Text = "Team Server"
        Me.uxGroupBoxTeamServer.UseWaitCursor = True
        '
        'uxListBoxTeamServer
        '
        Me.uxListBoxTeamServer.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxTeamServer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxListBoxTeamServer.ContextMenuStrip = Me.uxContextMenuStripDetails
        Me.uxListBoxTeamServer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxTeamServer.FormattingEnabled = True
        Me.uxListBoxTeamServer.HorizontalScrollbar = True
        Me.uxListBoxTeamServer.Location = New System.Drawing.Point(0, 13)
        Me.uxListBoxTeamServer.Name = "uxListBoxTeamServer"
        Me.uxListBoxTeamServer.Size = New System.Drawing.Size(271, 78)
        Me.uxListBoxTeamServer.TabIndex = 3
        Me.uxListBoxTeamServer.UseWaitCursor = True
        '
        'uxGroupBoxServiceHost
        '
        Me.uxGroupBoxServiceHost.Controls.Add(Me.uxListBoxServiceHost)
        Me.uxGroupBoxServiceHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxServiceHost.Location = New System.Drawing.Point(3, 213)
        Me.uxGroupBoxServiceHost.Name = "uxGroupBoxServiceHost"
        Me.uxGroupBoxServiceHost.Padding = New System.Windows.Forms.Padding(0)
        Me.uxGroupBoxServiceHost.Size = New System.Drawing.Size(271, 99)
        Me.uxGroupBoxServiceHost.TabIndex = 8
        Me.uxGroupBoxServiceHost.TabStop = False
        Me.uxGroupBoxServiceHost.Text = "Service Host"
        '
        'uxListBoxServiceHost
        '
        Me.uxListBoxServiceHost.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxServiceHost.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxListBoxServiceHost.ContextMenuStrip = Me.uxContextMenuStripDetails
        Me.uxListBoxServiceHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxServiceHost.FormattingEnabled = True
        Me.uxListBoxServiceHost.HorizontalScrollbar = True
        Me.uxListBoxServiceHost.Location = New System.Drawing.Point(0, 13)
        Me.uxListBoxServiceHost.Name = "uxListBoxServiceHost"
        Me.uxListBoxServiceHost.Size = New System.Drawing.Size(271, 78)
        Me.uxListBoxServiceHost.TabIndex = 4
        '
        'uxGroupBoxCheckinHandlers
        '
        Me.uxGroupBoxCheckinHandlers.Controls.Add(Me.uxListBoxCheckIn)
        Me.uxGroupBoxCheckinHandlers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxCheckinHandlers.Location = New System.Drawing.Point(277, 315)
        Me.uxGroupBoxCheckinHandlers.Margin = New System.Windows.Forms.Padding(0)
        Me.uxGroupBoxCheckinHandlers.Name = "uxGroupBoxCheckinHandlers"
        Me.uxGroupBoxCheckinHandlers.Size = New System.Drawing.Size(277, 106)
        Me.uxGroupBoxCheckinHandlers.TabIndex = 9
        Me.uxGroupBoxCheckinHandlers.TabStop = False
        Me.uxGroupBoxCheckinHandlers.Text = "Handlers: CheckIn"
        Me.uxGroupBoxCheckinHandlers.UseWaitCursor = True
        '
        'uxListBoxCheckIn
        '
        Me.uxListBoxCheckIn.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxCheckIn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxListBoxCheckIn.ContextMenuStrip = Me.uxContextMenuStripDetails
        Me.uxListBoxCheckIn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxCheckIn.FormattingEnabled = True
        Me.uxListBoxCheckIn.HorizontalScrollbar = True
        Me.uxListBoxCheckIn.Location = New System.Drawing.Point(3, 16)
        Me.uxListBoxCheckIn.Name = "uxListBoxCheckIn"
        Me.uxListBoxCheckIn.Size = New System.Drawing.Size(271, 78)
        Me.uxListBoxCheckIn.TabIndex = 5
        Me.uxListBoxCheckIn.UseWaitCursor = True
        '
        'uxGroupBoxEvents
        '
        Me.uxGroupBoxEvents.Controls.Add(uxListBoxEventing)
        Me.uxGroupBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxEvents.Location = New System.Drawing.Point(280, 108)
        Me.uxGroupBoxEvents.Name = "uxGroupBoxEvents"
        Me.TableLayoutPanel1.SetRowSpan(Me.uxGroupBoxEvents, 2)
        Me.uxGroupBoxEvents.Size = New System.Drawing.Size(271, 204)
        Me.uxGroupBoxEvents.TabIndex = 10
        Me.uxGroupBoxEvents.TabStop = False
        Me.uxGroupBoxEvents.Text = "Eventing"
        '
        'EndPointsGroupBox
        '
        Me.EndPointsGroupBox.Controls.Add(Me.ux_ListBoxEndPoints)
        Me.EndPointsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EndPointsGroupBox.Location = New System.Drawing.Point(280, 3)
        Me.EndPointsGroupBox.Name = "EndPointsGroupBox"
        Me.EndPointsGroupBox.Size = New System.Drawing.Size(271, 99)
        Me.EndPointsGroupBox.TabIndex = 12
        Me.EndPointsGroupBox.TabStop = False
        Me.EndPointsGroupBox.Text = "End Points"
        '
        'ux_ListBoxEndPoints
        '
        Me.ux_ListBoxEndPoints.BackColor = System.Drawing.SystemColors.Control
        Me.ux_ListBoxEndPoints.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ux_ListBoxEndPoints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ux_ListBoxEndPoints.FormattingEnabled = True
        Me.ux_ListBoxEndPoints.Location = New System.Drawing.Point(3, 16)
        Me.ux_ListBoxEndPoints.Name = "ux_ListBoxEndPoints"
        Me.ux_ListBoxEndPoints.Size = New System.Drawing.Size(265, 78)
        Me.ux_ListBoxEndPoints.TabIndex = 0
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
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.uxGroupBoxTeamServer.ResumeLayout(False)
        Me.uxGroupBoxServiceHost.ResumeLayout(False)
        Me.uxGroupBoxCheckinHandlers.ResumeLayout(False)
        Me.uxGroupBoxEvents.ResumeLayout(False)
        Me.EndPointsGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonStart As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents uxListBoxTeamServer As System.Windows.Forms.ListBox
    Friend WithEvents uxListBoxServiceHost As System.Windows.Forms.ListBox
    Friend WithEvents uxListBoxCheckIn As System.Windows.Forms.ListBox
    Friend WithEvents uxContextMenuStripDetails As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents uxToolStripMenuItemViewasText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents uxGroupBoxTeamServer As System.Windows.Forms.GroupBox
    Friend WithEvents uxGroupBoxServiceHost As System.Windows.Forms.GroupBox
    Friend WithEvents uxGroupBoxCheckinHandlers As System.Windows.Forms.GroupBox
    Friend WithEvents uxGroupBoxEvents As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents uxListBoxWorkItemChanged As System.Windows.Forms.ListBox
    Friend WithEvents EndPointsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ux_ListBoxEndPoints As System.Windows.Forms.ListBox


End Class
