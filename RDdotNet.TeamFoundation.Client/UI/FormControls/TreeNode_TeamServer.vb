Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Config

Namespace UI.FormControls

    Friend Class TreeNode_TeamServer
        Inherits TreeNodeItem(Of TeamServerItem)

        Private _SubscriptionsNode As TreeNode_Subscriptions

        Public Sub New(ByVal Key As String, ByVal EventHandler As TFSEventHandlerClient, ByVal TeamServer As TeamServerItem)
            MyBase.New(Key, EventHandler, TeamServer)
            Me.Text = TeamServer.Name
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.TeamServerUpdated, AddressOf OnStatusUpdate
            '-----------------------
            Me.ContextMenuStrip.Items.Add(New ToolStripButton("Remove Team Server", Nothing, AddressOf RemoveTeamServer_Click))

            RunChecks()
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Private Sub RunChecks()
            '------------------------
            ' Create Contect Menu as Add events
            If Not Me.Item.HasAuthenticated Then
                Me.ContextMenuStrip.Items.Add(New ToolStripButton("Authenticate Team Server", Nothing, AddressOf RemoveTeamServer_Click))
            End If
            '---------------------
            If Not Me.Item.IsValid Then
                Me.ForeColor = Drawing.Color.Red
            Else
                Me.ForeColor = Drawing.Color.DarkGreen
            End If
            '---------------------
        End Sub

        Private Sub RemoveTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Get Selected Team Server
            'Dim TeamServer As TreeNode_TeamServer = CType(Me.TreeView.SelectedNode, TreeNode_TeamServer)
            ' Call remove
            MyBase.EventHandler.RemoveServer(Me.Item)
        End Sub

        ' Edit this for functionality
        Protected Overrides Sub OnStatusUpdate(ByVal source As Clients.TFSEventHandlerClient, ByVal e As Clients.StatusChangeEventArgs(Of Services.DataContracts.TeamServerItem))
            ' Only run for own Team Serfver details
            If Not e.Item Is Nothing AndAlso e.Item.Uri.ToString = e.Item.Uri.ToString Then
                Select Case e.ChangeType
                    Case StatusChangeTypeEnum.Item_Check, StatusChangeTypeEnum.Item_Check_OK, StatusChangeTypeEnum.Item_Check_Failed, StatusChangeTypeEnum.Item_Added
                        Item = e.Item
                        RunChecks()
                End Select
                If e.ChangeType = StatusChangeTypeEnum.Item_Check_OK Then
                    StartSubscriptions()
                End If
            End If
        End Sub



        Private Delegate Sub delStartSubscriptions()

        Private Sub StartSubscriptions()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New delStartSubscriptions(AddressOf StartSubscriptions))
            Else
                Dim nodeID As Integer = Me.Nodes.Add(New TreeNode_Subscriptions(EventHandler, Item, 100))
                _SubscriptionsNode = Me.Nodes(nodeID)
            End If
        End Sub


    End Class


End Namespace