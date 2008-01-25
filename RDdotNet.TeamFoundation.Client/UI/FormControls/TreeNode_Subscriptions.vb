Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Services.FaultContracts
Imports system.ServiceModel
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Events

Namespace UI.FormControls


    Friend Class TreeNode_Subscriptions
        Inherits TreeNodeCustom(Of TreeNode_Subscription)

        Private SubNodeNameMap As String = "Subscription:{0}"

        Private m_TeamServer As TeamServerItem

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal TeamServer As TeamServerItem, Optional ByVal Delay As Integer = 0)
            MyBase.New("Subscriptions", EventHandler, Delay)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.SubscriptionsUpdated, AddressOf OnSubscriptionsUpdated
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

        Public Sub OnSubscriptionsUpdated(ByVal Name As String, ByVal subscriptions As Collection(Of Subscription))
            GenerateChildren(subscriptions)
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal State As Object)
            Me.TreeNode_Inner_ChangeStatus(Status.Working)
            Dim subscriptions As Collection(Of Subscription) = Nothing
            Try
                subscriptions = EventHandler.GetSubscriptions(m_TeamServer.Name)
            Catch ex As ServiceModel.FaultException
                TreeNode_Inner_AddError("Error", ex)
                Me.TreeNode_Inner_ChangeStatus(Status.Faulted)
            Finally
                GenerateChildren(subscriptions)
            End Try
        End Sub

        Public Overloads Sub GenerateChildren(ByVal subscriptions As Collection(Of Subscription))
            TreeNode_Inner_ClearNodes()
            If Not subscriptions Is Nothing Then
                For Each s As Subscription In subscriptions
                    TreeNode_Inner_AddNode(New TreeNode_Subscription(EventHandler, s))
                Next
            End If
            TreeNode_Inner_CheckEmpty(SubNodeNameMap, "Subscriptions")
            ' Then make sure that all nodes are expanded
            Me.TreeNode_Inner_ExpandAll()
            Me.TreeNode_Inner_ChangeStatus(Status.Normal)
        End Sub

        Private Sub AddSubScription_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim text As String = CType(sender, ToolStripButton).Text
            Dim EventType As EventTypes = [Enum].Parse(GetType(EventTypes), text, True)
            '---------
            'Dim ServerUri As Uri = EventHandler.ServceUrl
            EventHandler.AddSubscriptions(m_TeamServer.Name, EventType)
        End Sub

    End Class

End Namespace
