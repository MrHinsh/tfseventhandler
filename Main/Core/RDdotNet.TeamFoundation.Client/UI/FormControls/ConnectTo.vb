Public Class frmConnectTo

#Region " Event Handlers "

    Private Sub uxTextBoxServerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxTextBoxServerName.TextChanged
        CheckData()
    End Sub

    Private Sub uxTextBoxPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxTextBoxPort.TextChanged
        CheckData()
    End Sub

    Private Sub uxRadioButtonProtocolHttp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxRadioButtonProtocolHttp.CheckedChanged
        uxRadioButtonProtocolHttps.Checked = False
        CheckData()
    End Sub

    Private Sub uxRadioButtonProtocolHttps_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxRadioButtonProtocolHttps.CheckedChanged
        uxRadioButtonProtocolHttp.Checked = False
        CheckData()
    End Sub

#End Region

    Private Sub CheckData()
        Dim Valid As Boolean = True
        ' Check Server Name
        If uxTextBoxServerName.Text = "" Then
            Valid = False
        End If
        ' Check Port Number
        If uxTextBoxPort.Text = "" Then
            Valid = False
        End If
        ' Check Protocol
        If Not uxRadioButtonProtocolHttp.Checked And Not uxRadioButtonProtocolHttps.Checked Then
            Valid = False
        End If
        ' If Valid then do stuff
        If Valid Then
            Me.uxButtonConnect.Enabled = True
        End If
    End Sub


    Private Sub uxButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxButtonCancel.Click
        Me.Hide()

    End Sub

    Private _ServerUri As Uri = Nothing

    Public ReadOnly Property ServerUri() As Uri
        Get
            Return _ServerUri
        End Get
    End Property

    Private Sub uxButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxButtonConnect.Click
        Dim ServerName As String = Me.uxTextBoxServerName.Text
        Dim Port As Integer = 0
        Try
            Port = Me.uxTextBoxPort.Text
        Catch ex As Exception
            MsgBox("The port number you entered is not valid!", MsgBoxStyle.Critical, "Port Error")
            Me.uxButtonConnect.Enabled = False
            Exit Sub
        End Try
        Dim Protocal As String = "http"
        If Me.uxRadioButtonProtocolHttps.Checked Then
            Protocal = "https"
        End If
        _ServerUri = New Uri(String.Format("{0}://{1}:{2}", Protocal, ServerName, Port))
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

End Class