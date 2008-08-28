Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_TFSEventHandlerClient
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _TeamServersNode As TreeNode_TeamServers

        Private _EventHandlersNode As TreeNode_EventHandlers
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = EventHandler.Server.ToString
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            ' TODO: Add Event handlers
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Refresh", Nothing, AddressOf Refresh_Click))
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove", Nothing, AddressOf Remove_Click))
            _ContextMenuStrip.Items.Add(New ToolStripButton("Server Settings", Nothing, AddressOf Settings_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            Dim AddedNodeID As Integer = 0
            ' Add Team Servers
            AddedNodeID = Me.Nodes.Add(New TreeNode_TeamServers(EventHandler, 100))
            _TeamServersNode = Me.Nodes(AddedNodeID)
            AddedNodeID = Me.Nodes.Add(New TreeNode_EventHandlers(EventHandler, 100))
            _EventHandlersNode = Me.Nodes(AddedNodeID)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Private Sub Refresh_Click(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private Sub Remove_Click(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private Sub Settings_Click(ByVal sender As Object, ByVal e As EventArgs)
            FormServerSettings.ShowSettings()
        End Sub

    End Class

End Namespace


