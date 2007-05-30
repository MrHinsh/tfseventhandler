Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandler
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerServer
        Private _TeamServersNode As TreeNode_TeamServers
        Private _SubscriptionsNode As TreeNode_Subscriptions
        Private _EventHandlersNode As TreeNode_EventHandlers
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerServer)
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
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            Dim AddedNodeID As Integer = 0
            ' Add Team Servers
            AddedNodeID = Me.Nodes.Add(New TreeNode_TeamServers(EventHandler, 100))
            _TeamServersNode = CType(Me.Nodes(AddedNodeID), TreeNode_TeamServers)
            AddedNodeID = Me.Nodes.Add(New TreeNode_EventHandlers(EventHandler, 100))
            _EventHandlersNode = CType(Me.Nodes(AddedNodeID), TreeNode_EventHandlers)
            AddedNodeID = Me.Nodes.Add(New TreeNode_Subscriptions(EventHandler, 100))
            _SubscriptionsNode = CType(Me.Nodes(AddedNodeID), TreeNode_Subscriptions)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerServer
            Get
                Return _EventHandler
            End Get
        End Property

        Private Sub Refresh_Click(ByVal sender As Object, ByVal e As EventArgs)
            _TeamServersNode.Refresh()
            _EventHandlersNode.Refresh()
            _SubscriptionsNode.Refresh()
        End Sub

        Private Sub Remove_Click(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

    End Class

End Namespace


