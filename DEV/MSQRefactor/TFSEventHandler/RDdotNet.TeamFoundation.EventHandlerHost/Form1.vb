Imports System.Messaging
Imports RDdotNet.TeamFoundation

Public Class Form1

    Private EventHandlerHost As System.ServiceModel.ServiceHost
    Private QueuerHost As System.ServiceModel.ServiceHost

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim queueName As String = ".\private$\TFSEventHandler"
        ' Create the transacted MSMQ queue, if necessary.
        If Not MessageQueue.Exists(queueName) Then
            MessageQueue.Create(queueName, True)
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.ButtonQHStop.PerformClick()
        Me.ButtonEHHStop.PerformClick()
    End Sub

    Private Sub ButtonQHStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonQHStart.Click
        QueuerHost = ServiceFactory.GetQueuerServiceHost(6661)
        Try
            QueuerHost.Open()
            Me.ButtonQHStop.Enabled = True
            Me.ButtonQHStart.Enabled = False
            Me.CheckBoxQHEnabled.Checked = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonQHStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonQHStop.Click
        QueuerHost.Close()
        Me.ButtonQHStop.Enabled = False
        Me.ButtonQHStart.Enabled = True
        Me.CheckBoxQHEnabled.Checked = False
    End Sub

    Private Sub ButtonEHHStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEHHStart.Click
        EventHandlerHost = ServiceFactory.GetEventHandlerServiceHost(6661)
        EventHandlerHost.Open()
        Me.ButtonEHHStop.Enabled = True
        Me.ButtonEHHStart.Enabled = False
        Me.CheckBoxEHHEnabled.Checked = True
    End Sub

    Private Sub ButtonEHHStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEHHStop.Click
        EventHandlerHost.Close()
        Me.ButtonEHHStop.Enabled = False
        Me.ButtonEHHStart.Enabled = True
        Me.CheckBoxEHHEnabled.Checked = False
    End Sub

End Class
