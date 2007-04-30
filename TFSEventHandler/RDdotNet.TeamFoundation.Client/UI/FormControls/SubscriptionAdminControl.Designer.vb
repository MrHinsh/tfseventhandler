<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubscriptionAdminControl
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SubscriptionAdminControl))
        Me.uxGroupBoxSubscriptionAdmin = New System.Windows.Forms.GroupBox
        Me.uxTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.uxToolStripSubscription = New System.Windows.Forms.ToolStrip
        Me.uxToolStripButtonConnect = New System.Windows.Forms.ToolStripButton
        Me.uxToolStripButtonDisconnect = New System.Windows.Forms.ToolStripButton
        Me.uxToolStripButtonAdd = New System.Windows.Forms.ToolStripButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EventTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AddressDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.uxBindingSourceSubscriptions = New System.Windows.Forms.BindingSource(Me.components)
        Me.uxContextMenuStripData = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.uxGroupBoxSubscriptionAdmin.SuspendLayout()
        Me.uxTableLayoutPanel.SuspendLayout()
        Me.uxToolStripSubscription.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxBindingSourceSubscriptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'uxGroupBoxSubscriptionAdmin
        '
        Me.uxGroupBoxSubscriptionAdmin.Controls.Add(Me.uxTableLayoutPanel)
        Me.uxGroupBoxSubscriptionAdmin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxGroupBoxSubscriptionAdmin.Location = New System.Drawing.Point(0, 0)
        Me.uxGroupBoxSubscriptionAdmin.Name = "uxGroupBoxSubscriptionAdmin"
        Me.uxGroupBoxSubscriptionAdmin.Size = New System.Drawing.Size(457, 247)
        Me.uxGroupBoxSubscriptionAdmin.TabIndex = 0
        Me.uxGroupBoxSubscriptionAdmin.TabStop = False
        Me.uxGroupBoxSubscriptionAdmin.Text = "Subscriptions Admin"
        '
        'uxTableLayoutPanel
        '
        Me.uxTableLayoutPanel.ColumnCount = 2
        Me.uxTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.uxTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.uxTableLayoutPanel.Controls.Add(Me.uxToolStripSubscription, 0, 0)
        Me.uxTableLayoutPanel.Controls.Add(Me.DataGridView1, 0, 1)
        Me.uxTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTableLayoutPanel.Location = New System.Drawing.Point(3, 16)
        Me.uxTableLayoutPanel.Name = "uxTableLayoutPanel"
        Me.uxTableLayoutPanel.RowCount = 2
        Me.uxTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.uxTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.uxTableLayoutPanel.Size = New System.Drawing.Size(451, 228)
        Me.uxTableLayoutPanel.TabIndex = 0
        '
        'uxToolStripSubscription
        '
        Me.uxTableLayoutPanel.SetColumnSpan(Me.uxToolStripSubscription, 2)
        Me.uxToolStripSubscription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxToolStripSubscription.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.uxToolStripSubscription.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxToolStripButtonConnect, Me.uxToolStripButtonDisconnect, Me.uxToolStripButtonAdd})
        Me.uxToolStripSubscription.Location = New System.Drawing.Point(0, 0)
        Me.uxToolStripSubscription.Name = "uxToolStripSubscription"
        Me.uxToolStripSubscription.Size = New System.Drawing.Size(451, 25)
        Me.uxToolStripSubscription.TabIndex = 1
        Me.uxToolStripSubscription.Text = "ToolStrip1"
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
        'uxToolStripButtonAdd
        '
        Me.uxToolStripButtonAdd.Enabled = False
        Me.uxToolStripButtonAdd.Image = CType(resources.GetObject("uxToolStripButtonAdd.Image"), System.Drawing.Image)
        Me.uxToolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.uxToolStripButtonAdd.Name = "uxToolStripButtonAdd"
        Me.uxToolStripButtonAdd.Size = New System.Drawing.Size(46, 22)
        Me.uxToolStripButtonAdd.Text = "Add"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.EventTypeDataGridViewTextBoxColumn, Me.AddressDataGridViewTextBoxColumn})
        Me.uxTableLayoutPanel.SetColumnSpan(Me.DataGridView1, 2)
        Me.DataGridView1.DataSource = Me.uxBindingSourceSubscriptions
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 28)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.Size = New System.Drawing.Size(445, 197)
        Me.DataGridView1.TabIndex = 2
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.Width = 43
        '
        'EventTypeDataGridViewTextBoxColumn
        '
        Me.EventTypeDataGridViewTextBoxColumn.DataPropertyName = "EventType"
        Me.EventTypeDataGridViewTextBoxColumn.HeaderText = "EventType"
        Me.EventTypeDataGridViewTextBoxColumn.Name = "EventTypeDataGridViewTextBoxColumn"
        Me.EventTypeDataGridViewTextBoxColumn.Width = 84
        '
        'AddressDataGridViewTextBoxColumn
        '
        Me.AddressDataGridViewTextBoxColumn.DataPropertyName = "Address"
        Me.AddressDataGridViewTextBoxColumn.HeaderText = "Address"
        Me.AddressDataGridViewTextBoxColumn.Name = "AddressDataGridViewTextBoxColumn"
        Me.AddressDataGridViewTextBoxColumn.Width = 70
        '
        'uxBindingSourceSubscriptions
        '
        Me.uxBindingSourceSubscriptions.DataSource = GetType(MerrillLynch.TeamFoundation.Client.EventAdmin.Subscription)
        '
        'uxContextMenuStripData
        '
        Me.uxContextMenuStripData.Name = "uxContextMenuStripData"
        Me.uxContextMenuStripData.Size = New System.Drawing.Size(61, 4)
        '
        'SubscriptionAdminControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.uxGroupBoxSubscriptionAdmin)
        Me.Name = "SubscriptionAdminControl"
        Me.Size = New System.Drawing.Size(457, 247)
        Me.uxGroupBoxSubscriptionAdmin.ResumeLayout(False)
        Me.uxTableLayoutPanel.ResumeLayout(False)
        Me.uxTableLayoutPanel.PerformLayout()
        Me.uxToolStripSubscription.ResumeLayout(False)
        Me.uxToolStripSubscription.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxBindingSourceSubscriptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxGroupBoxSubscriptionAdmin As System.Windows.Forms.GroupBox
    Friend WithEvents uxTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents uxToolStripSubscription As System.Windows.Forms.ToolStrip
    Friend WithEvents uxToolStripButtonConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents uxToolStripButtonDisconnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents uxToolStripButtonAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents uxBindingSourceSubscriptions As System.Windows.Forms.BindingSource
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AddressDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uxContextMenuStripData As System.Windows.Forms.ContextMenuStrip

End Class
