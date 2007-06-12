Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls.Tree

    Friend Enum FeatureOptions
        TeamServers = 1
        EventHandlers = 2
        All = TeamServers Or EventHandlers
    End Enum

    Friend Enum DisplayOptions
        User = 1
        Admin = 2
        Everone = User Or Admin
    End Enum

    Friend Class ServerTreeNode
        Inherits TreeNode

        Private _EventHandler As Servers.TFSEventHandlerServer
        Private _FeatureOption As FeatureOptions = FeatureOptions.All
        Private _DisplayOption As DisplayOptions = DisplayOptions.Everone
        Private _TeamServersNode As TreeNode_TeamServers
        Private _EventHandlersNode As TreeNode_EventHandlers
        Private _ContextMenuStrip As New ContextMenuStrip

        Public ReadOnly Property IsFeature(ByVal FeatureOption As FeatureOptions) As Boolean
            Get
                Return (_FeatureOption And FeatureOption) > 0
            End Get
        End Property


        Public ReadOnly Property IsDisplay(ByVal DisplayOption As DisplayOptions) As Boolean
            Get
                Return (_DisplayOption And DisplayOption) > 0
            End Get
        End Property



        Public Sub New(ByVal EventHandler As Servers.TFSEventHandlerServer, ByVal FeatureOption As FeatureOptions, ByVal DisplayOption As DisplayOptions)
            Me.Text = EventHandler.ServerUri.ToString
            _FeatureOption = FeatureOption
            _DisplayOption = DisplayOption
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            ' TODO: Add Event handlers
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Refresh", Nothing, AddressOf Refresh_Click))
            If IsDisplay(DisplayOptions.Admin) Then _ContextMenuStrip.Items.Add(New ToolStripButton("Remove", Nothing, AddressOf Remove_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            Dim AddedNodeID As Integer = 0
            ' Add Team Servers
            If IsFeature(FeatureOptions.TeamServers) Then AddedNodeID = Me.Nodes.Add(New TreeNode_TeamServers(EventHandler, 100))
            If IsFeature(FeatureOptions.TeamServers) Then _TeamServersNode = CType(Me.Nodes(AddedNodeID), TreeNode_TeamServers)
            If IsFeature(FeatureOptions.EventHandlers) Then AddedNodeID = Me.Nodes.Add(New TreeNode_EventHandlers(EventHandler, 100))
            If IsFeature(FeatureOptions.EventHandlers) Then _EventHandlersNode = CType(Me.Nodes(AddedNodeID), TreeNode_EventHandlers)
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As Servers.TFSEventHandlerServer
            Get
                Return _EventHandler
            End Get
        End Property

        Private Sub Refresh_Click(ByVal sender As Object, ByVal e As EventArgs)
            If IsFeature(FeatureOptions.TeamServers) Then _TeamServersNode.Refresh()
            If IsFeature(FeatureOptions.EventHandlers) Then _EventHandlersNode.Refresh()
        End Sub

        Private Sub Remove_Click(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

    End Class

End Namespace


