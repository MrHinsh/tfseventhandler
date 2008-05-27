Public Class frmConnectTo

    Private _Protocol As Protocol = Protocol.None
    Private _ServerName As String = ""
    Private _Port As Integer = 0
    Private _ConnectTo As String = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName

    Public ReadOnly Property ServerName() As String
        Get
            Return _ServerName
        End Get
    End Property

    Public Sub New(ByVal ConectTo As String, Optional ByVal Protocol As Protocol = Protocol.None, Optional ByVal ServerName As String = "", Optional ByVal Port As Integer = 0)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Protocol = Protocol
        _ServerName = ServerName
        _Port = Port
        _ConnectTo = ConectTo
        Me.Text = "Connect to " & ConectTo
    End Sub


    Private Sub frmConnectTo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Set defaults for Protocol
        Select Case _Protocol
            Case Protocol.HTTP
                uxRadioButtonProtocolHttp.Checked = True
                uxRadioButtonProtocolHttp.Enabled = False
                uxRadioButtonProtocolHttps.Enabled = False
            Case Protocol.HTTPS
                uxRadioButtonProtocolHttps.Checked = True
                uxRadioButtonProtocolHttps.Enabled = False
                uxRadioButtonProtocolHttp.Enabled = False
            Case Protocol.None
                'Do nothing
        End Select
        ' Set Defaults for Port
        If _Port > 0 Then
            Me.uxTextBoxPort.Text = _Port
            Me.uxTextBoxPort.Enabled = False
        End If
        ' Set server name stuff
        Me.uxLabelServerName.Text = _ConnectTo
        If Not _ServerName = "" Then
            Me.uxTextBoxServerName.Text = _ServerName
        End If
    End Sub

    Public Enum Protocol
        None
        HTTP
        HTTPS
    End Enum

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
        _ServerName = Me.uxTextBoxServerName.Text
        Try
            _Port = Me.uxTextBoxPort.Text
        Catch ex As Exception
            MsgBox("The port number you entered is not valid!", MsgBoxStyle.Critical, "Port Error")
            Me.uxButtonConnect.Enabled = False
            Exit Sub
        End Try
        If Me.uxRadioButtonProtocolHttps.Checked Then
            _Protocol = Protocol.HTTPS
        End If
        If Me.uxRadioButtonProtocolHttp.Checked Then
            _Protocol = Protocol.HTTP
        End If
        Try
            _ServerUri = New Uri(String.Format("{0}://{1}:{2}", _Protocol.ToString.ToLower, _ServerName, _Port))
        Catch ex As Exception
            MsgBox("The server name you entered is not valid!", MsgBoxStyle.Critical, "Server Name Error")
            Exit Sub
        End Try
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

End Class