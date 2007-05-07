Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients

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

    Private Class TreeNode_EventHandler
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _TeamServersNode As TreeNode
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
            _TeamServersNode = New TreeName_TeamServers(EventHandler)
            Me.Nodes.Add(_TeamServersNode)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

    End Class

    Private Class TreeName_TeamServers
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = "Team Servers"
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            AddHandler _EventHandler.TeamServersUpdated, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Add Team Server", Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise team server List
            GenerateChildren(_EventHandler.GetServers)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnTeamServersUpdated(ByVal TeamServers() As String)
            GenerateChildren(TeamServers)
        End Sub

        Public Sub GenerateChildren(ByVal TeamServers() As String)
            Me.Nodes.Clear()
            For Each s As String In TeamServers
                Me.Nodes.Add(New TreeName_TeamServer(EventHandler, s))
            Next
        End Sub

        Private Sub AddTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As String = InputBox("Enter url", DefaultResponse:="http://localhost:8080")
            Dim url As New Uri(x)
            _EventHandler.AddServer(x.ToString, x.ToString)
        End Sub

    End Class

    Private Class TreeName_TeamServer
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
            Dim TeamServer As TreeName_TeamServer = CType(Me.TreeView.SelectedNode, TreeName_TeamServer)
            ' Call remove
            _EventHandler.RemoveServer(TeamServer.Name)
        End Sub

    End Class

End Class
