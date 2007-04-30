<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubscriptionAddForm
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
        Me.uxTableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.uxTextBoxSeriveUri = New System.Windows.Forms.TextBox
        Me.uxLabelServiceUri = New System.Windows.Forms.Label
        Me.uxTableLayoutPanelButtons = New System.Windows.Forms.TableLayoutPanel
        Me.uxButtonTest = New System.Windows.Forms.Button
        Me.uxButtonCancel = New System.Windows.Forms.Button
        Me.uxButtonAccept = New System.Windows.Forms.Button
        Me.uxListBoxTestResults = New System.Windows.Forms.ListBox
        Me.uxTableLayoutPanel1.SuspendLayout()
        Me.uxTableLayoutPanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxTableLayoutPanel1
        '
        Me.uxTableLayoutPanel1.ColumnCount = 2
        Me.uxTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.uxTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.uxTableLayoutPanel1.Controls.Add(Me.uxTextBoxSeriveUri, 1, 0)
        Me.uxTableLayoutPanel1.Controls.Add(Me.uxLabelServiceUri, 0, 0)
        Me.uxTableLayoutPanel1.Controls.Add(Me.uxTableLayoutPanelButtons, 1, 2)
        Me.uxTableLayoutPanel1.Controls.Add(Me.uxListBoxTestResults, 1, 1)
        Me.uxTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.uxTableLayoutPanel1.Name = "uxTableLayoutPanel1"
        Me.uxTableLayoutPanel1.RowCount = 3
        Me.uxTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.uxTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.uxTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.uxTableLayoutPanel1.Size = New System.Drawing.Size(520, 176)
        Me.uxTableLayoutPanel1.TabIndex = 0
        '
        'uxTextBoxSeriveUri
        '
        Me.uxTextBoxSeriveUri.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTextBoxSeriveUri.Location = New System.Drawing.Point(176, 3)
        Me.uxTextBoxSeriveUri.Name = "uxTextBoxSeriveUri"
        Me.uxTextBoxSeriveUri.Size = New System.Drawing.Size(341, 20)
        Me.uxTextBoxSeriveUri.TabIndex = 0
        '
        'uxLabelServiceUri
        '
        Me.uxLabelServiceUri.AutoSize = True
        Me.uxLabelServiceUri.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxLabelServiceUri.Location = New System.Drawing.Point(3, 0)
        Me.uxLabelServiceUri.Name = "uxLabelServiceUri"
        Me.uxLabelServiceUri.Size = New System.Drawing.Size(167, 25)
        Me.uxLabelServiceUri.TabIndex = 1
        Me.uxLabelServiceUri.Text = "Service Uri: "
        Me.uxLabelServiceUri.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'uxTableLayoutPanelButtons
        '
        Me.uxTableLayoutPanelButtons.ColumnCount = 3
        Me.uxTableLayoutPanelButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.uxTableLayoutPanelButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.uxTableLayoutPanelButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.uxTableLayoutPanelButtons.Controls.Add(Me.uxButtonTest, 0, 0)
        Me.uxTableLayoutPanelButtons.Controls.Add(Me.uxButtonCancel, 1, 0)
        Me.uxTableLayoutPanelButtons.Controls.Add(Me.uxButtonAccept, 2, 0)
        Me.uxTableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTableLayoutPanelButtons.Location = New System.Drawing.Point(176, 149)
        Me.uxTableLayoutPanelButtons.Name = "uxTableLayoutPanelButtons"
        Me.uxTableLayoutPanelButtons.RowCount = 1
        Me.uxTableLayoutPanelButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.uxTableLayoutPanelButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.uxTableLayoutPanelButtons.Size = New System.Drawing.Size(341, 24)
        Me.uxTableLayoutPanelButtons.TabIndex = 2
        '
        'uxButtonTest
        '
        Me.uxButtonTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxButtonTest.Location = New System.Drawing.Point(0, 0)
        Me.uxButtonTest.Margin = New System.Windows.Forms.Padding(0)
        Me.uxButtonTest.Name = "uxButtonTest"
        Me.uxButtonTest.Size = New System.Drawing.Size(113, 24)
        Me.uxButtonTest.TabIndex = 0
        Me.uxButtonTest.Text = "Test"
        Me.uxButtonTest.UseVisualStyleBackColor = True
        '
        'uxButtonCancel
        '
        Me.uxButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.uxButtonCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxButtonCancel.Location = New System.Drawing.Point(113, 0)
        Me.uxButtonCancel.Margin = New System.Windows.Forms.Padding(0)
        Me.uxButtonCancel.Name = "uxButtonCancel"
        Me.uxButtonCancel.Size = New System.Drawing.Size(113, 24)
        Me.uxButtonCancel.TabIndex = 1
        Me.uxButtonCancel.Text = "Cancel"
        Me.uxButtonCancel.UseVisualStyleBackColor = True
        '
        'uxButtonAccept
        '
        Me.uxButtonAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxButtonAccept.Enabled = False
        Me.uxButtonAccept.Location = New System.Drawing.Point(226, 0)
        Me.uxButtonAccept.Margin = New System.Windows.Forms.Padding(0)
        Me.uxButtonAccept.Name = "uxButtonAccept"
        Me.uxButtonAccept.Size = New System.Drawing.Size(115, 24)
        Me.uxButtonAccept.TabIndex = 2
        Me.uxButtonAccept.Text = "Accept"
        Me.uxButtonAccept.UseVisualStyleBackColor = True
        '
        'uxListBoxTestResults
        '
        Me.uxListBoxTestResults.BackColor = System.Drawing.SystemColors.Control
        Me.uxListBoxTestResults.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.uxListBoxTestResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxListBoxTestResults.FormattingEnabled = True
        Me.uxListBoxTestResults.Location = New System.Drawing.Point(176, 28)
        Me.uxListBoxTestResults.Name = "uxListBoxTestResults"
        Me.uxListBoxTestResults.Size = New System.Drawing.Size(341, 104)
        Me.uxListBoxTestResults.TabIndex = 3
        '
        'SubscriptionAddForm
        '
        Me.AcceptButton = Me.uxButtonAccept
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.uxButtonCancel
        Me.ClientSize = New System.Drawing.Size(520, 176)
        Me.Controls.Add(Me.uxTableLayoutPanel1)
        Me.Name = "SubscriptionAddForm"
        Me.Text = "SubscriptionAddForm"
        Me.uxTableLayoutPanel1.ResumeLayout(False)
        Me.uxTableLayoutPanel1.PerformLayout()
        Me.uxTableLayoutPanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents uxTableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents uxTextBoxSeriveUri As System.Windows.Forms.TextBox
    Friend WithEvents uxLabelServiceUri As System.Windows.Forms.Label
    Friend WithEvents uxTableLayoutPanelButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents uxButtonTest As System.Windows.Forms.Button
    Friend WithEvents uxButtonCancel As System.Windows.Forms.Button
    Friend WithEvents uxButtonAccept As System.Windows.Forms.Button
    Friend WithEvents uxListBoxTestResults As System.Windows.Forms.ListBox
End Class
