Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts

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
            _TeamServersNode = New TreeNode_TeamServers(EventHandler)
            Me.Nodes.Add(_TeamServersNode)
            _EventHandlersNode = New TreeNode_EventHandlers(EventHandler)
            Me.Nodes.Add(_EventHandlersNode)
            _SubscriptionsNode = New TreeNode_Subscriptions(EventHandler)
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

#Region " Team Server "

    Private Class TreeNode_TeamServers
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
            GenerateChildren()
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

        Public Sub GenerateChildren(Optional ByVal TeamServers() As String = Nothing)
            Me.Nodes.Clear()
            Try
                If TeamServers Is Nothing Then
                    TeamServers = _EventHandler.GetServers()
                End If
                For Each s As String In TeamServers
                    Me.Nodes.Add(New TreeNode_TeamServer(EventHandler, s))
                Next
            Catch ex As Exception
                Me.Nodes.Add("Error:" & ex.ToString)
            End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Servers Found")
            End If
        End Sub

        Private Sub AddTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As String = InputBox("Enter url", DefaultResponse:="http://localhost:8080")
            Dim url As New Uri(x)
            _EventHandler.AddServer(x.ToString, x.ToString)
        End Sub

    End Class

    Private Class TreeNode_TeamServer
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

#End Region

#Region " Event Handlers "

    Private Class TreeNode_EventHandlers
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = "Event Handlers"
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            AddHandler _EventHandler.HandlersUpdated, AddressOf OnHandlersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Add Assembly")) 'TODO: , Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise Assembly List
            GenerateChildren()
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub GenerateChildren(Optional ByVal AssemblyManaifest As AssemblyManaifest = Nothing)
            Me.Nodes.Clear()
            Try
                If AssemblyManaifest Is Nothing Then
                    AssemblyManaifest = _EventHandler.GetAssemblys()
                End If
                If Not AssemblyManaifest.Assemblys Is Nothing Then
                    For Each AI As AssemblyItem In AssemblyManaifest.Assemblys
                        Me.Nodes.Add(New TreeNode_AssemblyItem(EventHandler, AI))
                    Next
                End If
            Catch ex As Exception
                Me.Nodes.Add("Error: " & ex.ToString)
            End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Assemblies found")
            End If
        End Sub

        Public Sub OnHandlersUpdated(ByVal AssemblyManaifest As RDdotNet.TeamFoundation.Services.DataContracts.AssemblyManaifest)
            GenerateChildren(AssemblyManaifest)
        End Sub

    End Class

    Private Class TreeNode_AssemblyItem
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _AssemblyItem As AssemblyItem
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal AssemblyItem As AssemblyItem)
            Me.Text = "Assembly: " & AssemblyItem.Name.FullName
            _AssemblyItem = AssemblyItem
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove")) 'TODO: , Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise team server List
            GenerateChildren(_AssemblyItem.EventHandlers)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub GenerateChildren(ByVal EventHandlers As Collection(Of EventHandlerItem))
            Me.Nodes.Clear()
            For Each EHI As EventHandlerItem In EventHandlers
                Me.Nodes.Add(New TreeNode_EventHandlerItem(_EventHandler, EHI))
            Next
        End Sub

    End Class

    Private Class TreeNode_EventHandlerItem
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _EventHandlerItem As EventHandlerItem
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal EventHandlerItem As EventHandlerItem)
            Me.Text = String.Format("{0} ({1})", EventHandlerItem.HandlerType.FullName, EventHandlerItem.EventType.ToString)
            _EventHandlerItem = EventHandlerItem
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

    End Class

#End Region

#Region " Subscriptions "

    Private Class TreeNode_Subscriptions
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = "Subscriptions"
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            AddHandler _EventHandler.SubscriptionsUpdated, AddressOf OnSubscriptionsUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Add Subscription")) 'TODO:, Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise team server List
            GenerateChildren()
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnSubscriptionsUpdated(ByVal subscriptions As Collection(Of Subscription))
            GenerateChildren(subscriptions)
        End Sub

        Public Sub GenerateChildren(Optional ByVal subscriptions As Collection(Of Subscription) = Nothing)
            Me.Nodes.Clear()
            Try
                If subscriptions Is Nothing Then
                    subscriptions = _EventHandler.GetSubscriptions()
                End If
                For Each s As Subscription In subscriptions
                    Me.Nodes.Add(New TreeNode_Subscription(EventHandler, s))
                Next
            Catch ex As Exception
                Me.Nodes.Add("Error: " & ex.ToString)
            End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Subscriptions found")
            End If
        End Sub

    End Class

    Private Class TreeNode_Subscription
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal Subscription As Subscription)
            Me.Text = String.Format("{0} {1} {2}", Subscription.ID, Subscription.EventType.ToString, Subscription.Address)
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove")) ' TODO:, Nothing, AddressOf RemoveTeamServer_Click))
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

    End Class

#End Region



End Class