Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Services.FaultContracts
Imports system.ServiceModel
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Events

Namespace UI.FormControls


    Friend Class TreeNode_Subscriptions
        Inherits TreeNodeItems(Of TreeNode_Subscription, SubscriptionItem)

        Private SubNodeNameMap As String = "Subscription:{0}"

        Private m_TeamServer As TeamServerItem

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal TeamServer As TeamServerItem, Optional ByVal Delay As Integer = 0)
            MyBase.New("Subscriptions", EventHandler, Delay)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.SubscriptionsStatusChange, AddressOf OnStatusUpdate
            m_TeamServer = TeamServer
            '------------------------
            '-----------------------
            Dim EvntSubNode As New ToolStripMenuItem("Add Event Type")
            ContextMenuStrip.Items.Add(EvntSubNode)
            ' Create Contect Menu as Add events
            For Each EventType As EventTypes In [Enum].GetValues(GetType(EventTypes))
                If Not EventType = EventTypes.Unknown Then
                    Dim tsb As New ToolStripButton(EventType.ToString, Nothing, AddressOf AddSubScription_Click)
                    EvntSubNode.DropDownItems.Add(tsb)
                End If
            Next
            '-----------------------
            ' Initilise team server List
            Refresh()
        End Sub

        Protected Overrides Sub OnStatusUpdate(ByVal source As Clients.TFSEventHandlerClient, ByVal e As Clients.StatusChangeEventArgs(Of Services.DataContracts.SubscriptionItem))
            Me.TreeNode_Inner_ExpandAll()
            Select Case e.ChangeType
                Case StatusChangeTypeEnum.Item_Added
                    ' Fired when a new item is added on the server
                    TreeNode_ServerAdd(e.Item)
                Case StatusChangeTypeEnum.Item_Removed
                    ' Fired when an Item is removed from the server
                    Dim x As TreeNode_Subscription = TreeNode_SubscriptionFind(e.Item)
                    If Not x Is Nothing Then UpdateInnerNode(NodeUpdateEnum.Delete, x)
                Case StatusChangeTypeEnum.Item_CheckAll_Started
                    ' Fired when a Refresh All is started
                    Me.ChangeNodeStatus(Status.Working)
                Case StatusChangeTypeEnum.Item_Check_Started
                    ' Fired when a single item is being checked
                    TreeNode_ServerAdd(e.Item)
                Case StatusChangeTypeEnum.Item_Check_OK
                    ' Fired when a check suceeds
                Case StatusChangeTypeEnum.Item_Check_Failed
                    ' Fired when a check fails
                Case StatusChangeTypeEnum.Item_Check_Ended
                    ' Fired when a single item check is finished
                    TreeNode_ServerAdd(e.Item)
                Case StatusChangeTypeEnum.Item_CheckAll_Ended
                    ' Fired when a Refresh All is finished
                    Me.ChangeNodeStatus(Status.Normal)
                    CheckChildren()
                Case StatusChangeTypeEnum.Item_Exists
                    ' Fired when an items is added that already exists
                    Dim ServerExistsMessage As String = "The server {0} already exists and can not be added"
                    MsgBox(String.Format(ServerExistsMessage, e.Item.ID))
                Case StatusChangeTypeEnum.Item_NotExists
                    ' Fired when an items is removed that does not exists

            End Select
        End Sub


        Public Sub OnSubscriptionsUpdated(ByVal Name As String, ByVal subscriptions As Collection(Of SubscriptionItem))
            GenerateChildren(subscriptions)
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.ChangeNodeStatus(Status.Working)
            System.Threading.Thread.Sleep(2000)
            Try
                EventHandler.RefreshSubscriptions()
            Catch ex As Exception
                TreeNode_Inner_AddError("Error", ex)
                Me.ChangeNodeStatus(Status.Faulted)
            Finally
                'GenerateChildren(TeamServers)
                CheckChildren()
            End Try
        End Sub

        Public Overloads Sub CheckChildren()
            TreeNode_Inner_CheckEmpty(SubNodeNameMap, "subscriptions")
            ' Then make sure that all nodes are expanded
            Me.TreeNode_Inner_ExpandAll()
            Me.ChangeNodeStatus(Status.Normal)
        End Sub

        Private Sub AddSubScription_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim text As String = CType(sender, ToolStripButton).Text
            Dim EventType As EventTypes = [Enum].Parse(GetType(EventTypes), text, True)
            '---------
            'Dim ServerUri As Uri = EventHandler.ServceUrl
            EventHandler.AddSubscriptions(m_TeamServer, EventType)
        End Sub

#Region " TreeNode Manipulation "

        Private Function TreeNode_ContainsServer(ByVal Subscription As SubscriptionItem) As Boolean
            TreeNode_ContainsServer = False
            For Each n As TreeNode_Subscription In Me.Nodes
                If n.Item.ID = Subscription.ID Then
                    TreeNode_ContainsServer = True
                End If
            Next
        End Function

        Private Sub TreeNode_ServerAdd(ByVal Subscription As SubscriptionItem)
            Dim key As String = String.Format(SubNodeNameMap, Subscription.ID)
            SyncLock Me.Nodes
                If Not TreeNode_ServerExists(Subscription) Then
                    ' Add Node
                    UpdateInnerNode(NodeUpdateEnum.Add, New TreeNode_Subscription(key, EventHandler, m_TeamServer, Subscription))
                End If
            End SyncLock
        End Sub

        Private Function TreeNode_ServerExists(ByVal Subscription As SubscriptionItem) As Boolean
            Dim key As String = String.Format(SubNodeNameMap, Subscription.ID)
            If Me.Nodes.Find(key, False).Count > 0 Then
                Return True
            End If
        End Function

        Private Function TreeNode_SubscriptionFind(ByVal Subscription As SubscriptionItem) As TreeNode_Subscription
            Dim key As String = String.Format(SubNodeNameMap, Subscription.ID)
            Dim FoundNodes = Me.Nodes.Find(key, False)
            If FoundNodes.Count > 0 Then
                Return CType(FoundNodes(0), TreeNode_Subscription)
            End If
            Return Nothing
        End Function

#End Region

    End Class

End Namespace
