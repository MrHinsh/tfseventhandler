Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client

Namespace UI.FormControls.Tree

    Friend Class TreeNode_TeamServer
        Inherits TreeNode

        Private _EventHandler As Servers.TFSEventHandlerServer
        Private _ContextMenuStrip As New ContextMenuStrip

        Private _ServerName As String = ""

        Public ReadOnly Property ServerName() As String
            Get
                Return Me._ServerName
            End Get
        End Property

        Public Sub New(ByVal EventHandler As Servers.TFSEventHandlerServer, ByVal ServerName As String)
            Me.Text = ServerName
            _ServerName = ServerName
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            'AddHandler _EventHandler.TeamServersUpdate, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove", Nothing, AddressOf RemoveTeamServer_Click))
            _ContextMenuStrip.Items.Add(New ToolStripSeparator())
            _ContextMenuStrip.Items.Add(New ToolStripButton("Subscribe", Nothing, AddressOf SubscribeTeamServer_Click))
            _ContextMenuStrip.Items.Add(New ToolStripButton("Unsubscribe", Nothing, AddressOf UnsubscribeTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As Servers.TFSEventHandlerServer
            Get
                Return _EventHandler
            End Get
        End Property

        Private Sub RemoveTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Call remove
            _EventHandler.TeamServersService.RemoveServer(ServerName)
        End Sub

        Private Sub SubscribeTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            EventHandler.SubscriptionsService.AddSubscriptions(ServerName, _EventHandler.ServerUri.ToString)
        End Sub

        Private Sub UnsubscribeTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            EventHandler.SubscriptionsService.RemoveSubscriptions(ServerName, _EventHandler.ServerUri.ToString)
        End Sub

    End Class


End Namespace