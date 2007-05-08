Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls


    Friend Class TreeNode_Subscriptions
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
            'Try
            If subscriptions Is Nothing Then
                subscriptions = _EventHandler.GetSubscriptions()
            End If
            For Each s As Subscription In subscriptions
                Me.Nodes.Add(New TreeNode_Subscription(EventHandler, s))
            Next
            'Catch ex As Exception
            'Me.Nodes.Add("Error: " & ex.ToString)
            'End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Subscriptions found")
            End If
        End Sub

    End Class



End Namespace