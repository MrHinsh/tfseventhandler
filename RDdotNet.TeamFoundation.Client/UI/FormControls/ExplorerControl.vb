Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Public Class TFSEventHandlerControl

        Private _ConnectedEventHandler As New Collection(Of TFSEventHandlerClient)

        Private Sub TFSEventHandlerControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '--------------------


            '--------------------
        End Sub

        Private Sub uxToolStripButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonRefresh.Click
            RefershEventHandlers()
        End Sub

        Private Sub uxToolStripButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonAdd.Click
            Dim x As String = InputBox("Enter url", DefaultResponse:="http://localhost:6661")
            Dim url As New Uri(x)
            Dim EventHandler As New TFSEventHandlerClient(url)
            ' TODO: Add event handlers
            'addhandler EventHandler.TeamServersUpdated, Addressof xxxxxxxxxxxx
            ' Start Services
            _ConnectedEventHandler.Add(EventHandler)
            RefershEventHandlers()
            ' Then make sure that all nodes are expanded
            Me.uxTreeView.ExpandAll()
        End Sub

        Private Sub RefershEventHandlers()
            Me.uxTreeView.Nodes.Clear()
            For Each EventHandler As TFSEventHandlerClient In _ConnectedEventHandler
                Me.uxTreeView.Nodes.Add(New TreeNode_EventHandler(EventHandler))
            Next
        End Sub



    End Class

End Namespace