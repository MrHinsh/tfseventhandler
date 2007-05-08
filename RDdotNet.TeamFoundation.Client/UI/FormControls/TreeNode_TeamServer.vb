Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_TeamServer
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal ServerName As String)
            Me.Text = ServerName
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            'AddHandler _EventHandler.TeamServersUpdate, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove", Nothing, AddressOf RemoveTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Private Sub RemoveTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Get Selected Team Server
            Dim TeamServer As TreeNode_TeamServer = CType(Me.TreeView.SelectedNode, TreeNode_TeamServer)
            ' Call remove
            _EventHandler.RemoveServer(TeamServer.Name)
        End Sub

    End Class


End Namespace