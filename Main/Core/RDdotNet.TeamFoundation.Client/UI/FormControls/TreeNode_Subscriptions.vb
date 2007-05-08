Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls


    Friend Class TreeNode_Subscriptions
        Inherits TreeNodeCustom(Of TreeNode_Subscription)

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
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnSubscriptionsUpdated(ByVal subscriptions As Collection(Of Subscription))
            GenerateChildren(subscriptions)
        End Sub

        Public Sub GenerateChildren(ByVal State As Object)
            Dim subscriptions As Collection(Of Subscription) = Nothing
            Try
                subscriptions = _EventHandler.GetSubscriptions()
            Catch ex As Services.FaultContracts.TeamFoundationServerUnauthorizedException
                AddError("TFS Denied", ex)
            Catch ex As ServiceModel.FaultException
                AddError("Error", ex)
            Finally
                GenerateChildren(subscriptions)
            End Try
        End Sub

        Public Sub GenerateChildren(ByVal subscriptions As Collection(Of Subscription))
            ClearNodes()
            If Not subscriptions Is Nothing Then
                For Each s As Subscription In subscriptions
                    AddNode(New TreeNode_Subscription(EventHandler, s))
                Next
            End If
            If Me.Nodes.Count = 0 Then
                AddMessage("No Subscriptions found")
            End If
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

    End Class

End Namespace