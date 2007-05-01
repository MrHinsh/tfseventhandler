Public Class Form1

    Private Host As System.ServiceModel.ServiceHost

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Host = New System.ServiceModel.ServiceHost(GetType(Services.DataContracts.EventHandlerService))
        Host.Open()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Host.Close()
    End Sub


End Class
