Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandler
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _TeamServersNode As TreeNode_TeamServers
        Private _SubscriptionsNode As TreeNode_Subscriptions
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
            _ContextMenuStrip.Items.Add(New ToolStripButton("Refresh"))
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove"))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Add Team Servers
            _TeamServersNode = New TreeNode_TeamServers(EventHandler, 100)
            Me.Nodes.Add(_TeamServersNode)
            _EventHandlersNode = New TreeNode_EventHandlers(EventHandler, 100)
            Me.Nodes.Add(_EventHandlersNode)
            _SubscriptionsNode = New TreeNode_Subscriptions(EventHandler, 100)
            Me.Nodes.Add(_SubscriptionsNode)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

    End Class

End Namespace


