Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Public Class TFSEventHandlerControl

        Private _ConnectedEventHandler As New Collection(Of Servers.TFSEventHandlerServer)

        Private Sub TFSEventHandlerControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '--------------------


            '--------------------
        End Sub

        Private Sub uxToolStripButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonRefresh.Click
            RefershEventHandlers()
        End Sub

        Private Sub uxToolStripButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim frmConnectTo As New frmConnectTo("TFS Event Handler Server", Protocol:=TeamFoundation.frmConnectTo.Protocol.HTTP, Port:=6661, ServerName:=System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName)
            Dim DialogResult As DialogResult = frmConnectTo.ShowDialog(Me)
            If DialogResult = Windows.Forms.DialogResult.OK Then
                '---------
                frmConnectTo.Close()
                frmConnectTo.Dispose()
                '---------
                Dim ServerUri As Uri = frmConnectTo.ServerUri
                Dim EventHandler As New Servers.TFSEventHandlerServer(ServerUri)
                ' Start Services
                _ConnectedEventHandler.Add(EventHandler)
                RefershEventHandlers()
                ' Then make sure that all nodes are expanded
                Me.uxTreeView.ExpandAll()
            End If
        End Sub

        Private Sub RefershEventHandlers()
            Me.uxTreeView.Nodes.Clear()
            For Each EventHandler As Servers.TFSEventHandlerServer In _ConnectedEventHandler
                Me.uxTreeView.Nodes.Add(New TreeNode_EventHandler(EventHandler))
            Next
        End Sub



    End Class

End Namespace