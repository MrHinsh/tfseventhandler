Imports System.Collections.Generic
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Clients


Public Class Form1


    Private WithEvents EventHandler As New RDdotNet.TeamFoundation.Clients.TFSEventHandlerClient

#Region " Form1 "


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

#End Region


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EventHandler.GetServers()
    End Sub

End Class
