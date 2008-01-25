<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.ButtonQHStart = New System.Windows.Forms.Button
        Me.ButtonQHStop = New System.Windows.Forms.Button
        Me.CheckBoxQHEnabled = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.ButtonEHHStart = New System.Windows.Forms.Button
        Me.ButtonEHHStop = New System.Windows.Forms.Button
        Me.CheckBoxEHHEnabled = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(334, 126)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(328, 57)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Queuer Host"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonQHStart, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonQHStop, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CheckBoxQHEnabled, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(322, 38)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'ButtonQHStart
        '
        Me.ButtonQHStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonQHStart.Location = New System.Drawing.Point(3, 3)
        Me.ButtonQHStart.Name = "ButtonQHStart"
        Me.ButtonQHStart.Size = New System.Drawing.Size(101, 32)
        Me.ButtonQHStart.TabIndex = 0
        Me.ButtonQHStart.Text = "Start"
        Me.ButtonQHStart.UseVisualStyleBackColor = True
        '
        'ButtonQHStop
        '
        Me.ButtonQHStop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonQHStop.Enabled = False
        Me.ButtonQHStop.Location = New System.Drawing.Point(110, 3)
        Me.ButtonQHStop.Name = "ButtonQHStop"
        Me.ButtonQHStop.Size = New System.Drawing.Size(101, 32)
        Me.ButtonQHStop.TabIndex = 1
        Me.ButtonQHStop.Text = "Stop"
        Me.ButtonQHStop.UseVisualStyleBackColor = True
        '
        'CheckBoxQHEnabled
        '
        Me.CheckBoxQHEnabled.AutoSize = True
        Me.CheckBoxQHEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxQHEnabled.Enabled = False
        Me.CheckBoxQHEnabled.Location = New System.Drawing.Point(217, 3)
        Me.CheckBoxQHEnabled.Name = "CheckBoxQHEnabled"
        Me.CheckBoxQHEnabled.Size = New System.Drawing.Size(102, 32)
        Me.CheckBoxQHEnabled.TabIndex = 2
        Me.CheckBoxQHEnabled.Text = "Enabled"
        Me.CheckBoxQHEnabled.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 66)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(328, 57)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Event Handler Host"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonEHHStart, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonEHHStop, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.CheckBoxEHHEnabled, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(322, 38)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'ButtonEHHStart
        '
        Me.ButtonEHHStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonEHHStart.Location = New System.Drawing.Point(3, 3)
        Me.ButtonEHHStart.Name = "ButtonEHHStart"
        Me.ButtonEHHStart.Size = New System.Drawing.Size(101, 32)
        Me.ButtonEHHStart.TabIndex = 0
        Me.ButtonEHHStart.Text = "Start"
        Me.ButtonEHHStart.UseVisualStyleBackColor = True
        '
        'ButtonEHHStop
        '
        Me.ButtonEHHStop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonEHHStop.Enabled = False
        Me.ButtonEHHStop.Location = New System.Drawing.Point(110, 3)
        Me.ButtonEHHStop.Name = "ButtonEHHStop"
        Me.ButtonEHHStop.Size = New System.Drawing.Size(101, 32)
        Me.ButtonEHHStop.TabIndex = 1
        Me.ButtonEHHStop.Text = "Stop"
        Me.ButtonEHHStop.UseVisualStyleBackColor = True
        '
        'CheckBoxEHHEnabled
        '
        Me.CheckBoxEHHEnabled.AutoSize = True
        Me.CheckBoxEHHEnabled.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxEHHEnabled.Enabled = False
        Me.CheckBoxEHHEnabled.Location = New System.Drawing.Point(217, 3)
        Me.CheckBoxEHHEnabled.Name = "CheckBoxEHHEnabled"
        Me.CheckBoxEHHEnabled.Size = New System.Drawing.Size(102, 32)
        Me.CheckBoxEHHEnabled.TabIndex = 2
        Me.CheckBoxEHHEnabled.Text = "Enabled"
        Me.CheckBoxEHHEnabled.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 126)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(342, 160)
        Me.MinimumSize = New System.Drawing.Size(342, 160)
        Me.Name = "Form1"
        Me.Opacity = 0.7
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TFS Event Handlers"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonQHStart As System.Windows.Forms.Button
    Friend WithEvents ButtonQHStop As System.Windows.Forms.Button
    Friend WithEvents CheckBoxQHEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonEHHStart As System.Windows.Forms.Button
    Friend WithEvents ButtonEHHStop As System.Windows.Forms.Button
    Friend WithEvents CheckBoxEHHEnabled As System.Windows.Forms.CheckBox

End Class
