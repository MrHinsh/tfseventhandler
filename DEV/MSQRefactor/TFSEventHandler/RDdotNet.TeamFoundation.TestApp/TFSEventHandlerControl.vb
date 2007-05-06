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
        Dim x As String = InputBox("Enter url")
        Dim url As New Uri(x)
        Dim EventHandler As New TFSEventHandlerClient
        ' TODO: Add event handlers
        'addhandler EventHandler.TeamServersUpdated, Addressof xxxxxxxxxxxx
        ' Start Services
        _ConnectedEventHandler.Add(EventHandler)
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
            Me.Text = EventHandler.Server
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            ' TODO: Add Event handlers
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Connect"))
            _ContextMenuStrip.Items.Add(New ToolStripButton("Refresh"))
            '-----------------------
            ' Add Team Servers
            _TeamServersNode = New TreeName_TeamServers(EventHandler)
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
            'TODO: Add Contect Menu Options
            '-----------------------
            ' Initilise team server List
            For Each s As String In _EventHandler.GetServers
                Me.Nodes.Add(s)
            Next
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnTeamServersUpdated(ByVal TeamServers() As String)
            Me.Nodes.Clear()
            For Each s As String In TeamServers
                Me.Nodes.Add(s)
            Next
        End Sub

    End Class

End Class
