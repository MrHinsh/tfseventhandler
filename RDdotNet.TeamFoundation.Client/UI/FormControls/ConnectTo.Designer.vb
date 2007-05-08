<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConnectTo
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
        Me.uxLabelServerName = New System.Windows.Forms.Label
        Me.uxTextBoxServerName = New System.Windows.Forms.TextBox
        Me.uxGroupBoxConnection = New System.Windows.Forms.GroupBox
        Me.uxTextBoxPort = New System.Windows.Forms.TextBox
        Me.uxLabelProtocol = New System.Windows.Forms.Label
        Me.uxRadioButtonProtocolHttps = New System.Windows.Forms.RadioButton
        Me.uxRadioButtonProtocolHttp = New System.Windows.Forms.RadioButton
        Me.uxLabelPort = New System.Windows.Forms.Label
        Me.uxButtonCancel = New System.Windows.Forms.Button
        Me.uxButtonConnect = New System.Windows.Forms.Button
        Me.uxGroupBoxConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxLabelServerName
        '
        Me.uxLabelServerName.AutoSize = True
        Me.uxLabelServerName.Location = New System.Drawing.Point(12, 9)
        Me.uxLabelServerName.Name = "uxLabelServerName"
        Me.uxLabelServerName.Size = New System.Drawing.Size(167, 13)
        Me.uxLabelServerName.TabIndex = 0
        Me.uxLabelServerName.Text = "TFS Event Handler Server name: "
        '
        'uxTextBoxServerName
        '
        Me.uxTextBoxServerName.Location = New System.Drawing.Point(12, 25)
        Me.uxTextBoxServerName.Name = "uxTextBoxServerName"
        Me.uxTextBoxServerName.Size = New System.Drawing.Size(233, 20)
        Me.uxTextBoxServerName.TabIndex = 1
        '
        'uxGroupBoxConnection
        '
        Me.uxGroupBoxConnection.Controls.Add(Me.uxTextBoxPort)
        Me.uxGroupBoxConnection.Controls.Add(Me.uxLabelProtocol)
        Me.uxGroupBoxConnection.Controls.Add(Me.uxRadioButtonProtocolHttps)
        Me.uxGroupBoxConnection.Controls.Add(Me.uxRadioButtonProtocolHttp)
        Me.uxGroupBoxConnection.Controls.Add(Me.uxLabelPort)
        Me.uxGroupBoxConnection.Location = New System.Drawing.Point(15, 51)
        Me.uxGroupBoxConnection.Name = "uxGroupBoxConnection"
        Me.uxGroupBoxConnection.Size = New System.Drawing.Size(230, 69)
        Me.uxGroupBoxConnection.TabIndex = 2
        Me.uxGroupBoxConnection.TabStop = False
        Me.uxGroupBoxConnection.Text = "Connection Details"
        '
        'uxTextBoxPort
        '
        Me.uxTextBoxPort.Location = New System.Drawing.Point(74, 15)
        Me.uxTextBoxPort.Name = "uxTextBoxPort"
        Me.uxTextBoxPort.Size = New System.Drawing.Size(54, 20)
        Me.uxTextBoxPort.TabIndex = 2
        '
        'uxLabelProtocol
        '
        Me.uxLabelProtocol.AutoSize = True
        Me.uxLabelProtocol.Location = New System.Drawing.Point(8, 46)
        Me.uxLabelProtocol.Name = "uxLabelProtocol"
        Me.uxLabelProtocol.Size = New System.Drawing.Size(52, 13)
        Me.uxLabelProtocol.TabIndex = 4
        Me.uxLabelProtocol.Text = "Protocol: "
        '
        'uxRadioButtonProtocolHttps
        '
        Me.uxRadioButtonProtocolHttps.AutoSize = True
        Me.uxRadioButtonProtocolHttps.Location = New System.Drawing.Point(136, 42)
        Me.uxRadioButtonProtocolHttps.Name = "uxRadioButtonProtocolHttps"
        Me.uxRadioButtonProtocolHttps.Size = New System.Drawing.Size(61, 17)
        Me.uxRadioButtonProtocolHttps.TabIndex = 3
        Me.uxRadioButtonProtocolHttps.TabStop = True
        Me.uxRadioButtonProtocolHttps.Text = "HTTPS"
        Me.uxRadioButtonProtocolHttps.UseVisualStyleBackColor = True
        '
        'uxRadioButtonProtocolHttp
        '
        Me.uxRadioButtonProtocolHttp.AutoSize = True
        Me.uxRadioButtonProtocolHttp.Location = New System.Drawing.Point(74, 42)
        Me.uxRadioButtonProtocolHttp.Name = "uxRadioButtonProtocolHttp"
        Me.uxRadioButtonProtocolHttp.Size = New System.Drawing.Size(54, 17)
        Me.uxRadioButtonProtocolHttp.TabIndex = 3
        Me.uxRadioButtonProtocolHttp.TabStop = True
        Me.uxRadioButtonProtocolHttp.Text = "HTTP"
        Me.uxRadioButtonProtocolHttp.UseVisualStyleBackColor = True
        '
        'uxLabelPort
        '
        Me.uxLabelPort.AutoSize = True
        Me.uxLabelPort.Location = New System.Drawing.Point(28, 22)
        Me.uxLabelPort.Name = "uxLabelPort"
        Me.uxLabelPort.Size = New System.Drawing.Size(32, 13)
        Me.uxLabelPort.TabIndex = 1
        Me.uxLabelPort.Text = "Port: "
        '
        'uxButtonCancel
        '
        Me.uxButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.uxButtonCancel.Location = New System.Drawing.Point(170, 126)
        Me.uxButtonCancel.Name = "uxButtonCancel"
        Me.uxButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.uxButtonCancel.TabIndex = 5
        Me.uxButtonCancel.Text = "Cancel"
        Me.uxButtonCancel.UseVisualStyleBackColor = True
        '
        'uxButtonConnect
        '
        Me.uxButtonConnect.Enabled = False
        Me.uxButtonConnect.Location = New System.Drawing.Point(89, 126)
        Me.uxButtonConnect.Name = "uxButtonConnect"
        Me.uxButtonConnect.Size = New System.Drawing.Size(75, 23)
        Me.uxButtonConnect.TabIndex = 4
        Me.uxButtonConnect.Text = "Connect"
        Me.uxButtonConnect.UseVisualStyleBackColor = True
        '
        'frmConnectTo
        '
        Me.AcceptButton = Me.uxButtonConnect
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.uxButtonCancel
        Me.ClientSize = New System.Drawing.Size(260, 155)
        Me.Controls.Add(Me.uxButtonConnect)
        Me.Controls.Add(Me.uxButtonCancel)
        Me.Controls.Add(Me.uxGroupBoxConnection)
        Me.Controls.Add(Me.uxTextBoxServerName)
        Me.Controls.Add(Me.uxLabelServerName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConnectTo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Connect To..."
        Me.TopMost = True
        Me.uxGroupBoxConnection.ResumeLayout(False)
        Me.uxGroupBoxConnection.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents uxLabelServerName As System.Windows.Forms.Label
    Friend WithEvents uxTextBoxServerName As System.Windows.Forms.TextBox
    Friend WithEvents uxGroupBoxConnection As System.Windows.Forms.GroupBox
    Friend WithEvents uxLabelPort As System.Windows.Forms.Label
    Friend WithEvents uxTextBoxPort As System.Windows.Forms.TextBox
    Friend WithEvents uxLabelProtocol As System.Windows.Forms.Label
    Friend WithEvents uxRadioButtonProtocolHttps As System.Windows.Forms.RadioButton
    Friend WithEvents uxRadioButtonProtocolHttp As System.Windows.Forms.RadioButton
    Friend WithEvents uxButtonCancel As System.Windows.Forms.Button
    Friend WithEvents uxButtonConnect As System.Windows.Forms.Button
End Class
