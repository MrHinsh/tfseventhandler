Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_Subscription
        Inherits TreeNodeItem(Of SubscriptionItem)

        Private m_TeamServerItem As TeamServerItem
        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal Key As String, ByVal EventHandler As TFSEventHandlerClient, ByVal TeamServer As TeamServerItem, ByVal Subscription As SubscriptionItem)
            MyBase.New(Key, EventHandler, Subscription)
            m_TeamServerItem = TeamServer
            Me.Text = String.Format("{0} ({1})", Subscription.EventType.ToString, Subscription.ID)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.SubscriptionsStatusChange, AddressOf OnStatusUpdate
            '-----------------------
            Me.ContextMenuStrip.Items.Add(New ToolStripButton("Remove Subscription", Nothing, AddressOf RemoveSubscription_Click))

            RunChecks()
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Private Sub RunChecks()
            '------------------------
           
            '---------------------
        End Sub

        Protected Overrides Sub OnStatusUpdate(ByVal source As Clients.TFSEventHandlerClient, ByVal e As Clients.StatusChangeEventArgs(Of Services.DataContracts.SubscriptionItem))
            ' Only run for own Team Serfver details
            If Not e.Item Is Nothing AndAlso e.Item.ID = Me.Item.ID Then
                Select Case e.ChangeType
                    Case StatusChangeTypeEnum.Item_Check, StatusChangeTypeEnum.Item_Check_OK, StatusChangeTypeEnum.Item_Check_Failed, StatusChangeTypeEnum.Item_Added
                        Item = e.Item
                        RunChecks()
                End Select
            End If
        End Sub

        Private Sub RemoveSubscription_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Get Selected Team Server
            'Dim TeamServer As TreeNode_TeamServer = CType(Me.TreeView.SelectedNode, TreeNode_TeamServer)
            ' Call remove
            MyBase.EventHandler.RemoveSubscription(m_TeamServerItem, Me.Item)
        End Sub

    End Class


End Namespace