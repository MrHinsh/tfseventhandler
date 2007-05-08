Public Class Form1

    Private QueuerHost As System.ServiceModel.ServiceHost

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        QueuerHost = New System.ServiceModel.ServiceHost(GetType(Services.QueuerService))
        QueuerHost.Open()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        QueuerHost.Close()

    End Sub

End Class
