Imports System.Messaging

Public Class Form1

    Private Host As System.ServiceModel.ServiceHost

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim queueName As String = ".\private$\TFSEventHandler"
        ' Create the transacted MSMQ queue, if necessary.
        If Not MessageQueue.Exists(queueName) Then
            MessageQueue.Create(queueName, True)
        End If

        Host = New System.ServiceModel.ServiceHost(GetType(Services.EventHandlerService))
        Host.Open()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Host.Close()
    End Sub


End Class
