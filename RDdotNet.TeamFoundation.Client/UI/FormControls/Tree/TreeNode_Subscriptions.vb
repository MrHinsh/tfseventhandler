'Imports system.Collections.ObjectModel
'Imports RDdotNet.TeamFoundation.Clients
'Imports RDdotNet.TeamFoundation.Services.DataContracts
'Imports RDdotNet.TeamFoundation.Services.FaultContracts
'Imports system.ServiceModel
'Imports System.Windows.Forms

'Namespace UI.FormControls.Tree

'    Friend Class TreeNode_Subscriptions
'        Inherits TreeNodeCustom(Of TreeNode_Subscription)

'        Public Sub New(ByVal EventHandler As Servers.TFSEventHandlerServer, Optional ByVal Delay As Integer = 0)
'            MyBase.New("Subscriptions", EventHandler, Delay)
'            '-----------------------
'            ' Create Handler and attach Events
'            AddHandler EventHandler.SubscriptionsService.SubscriptionsUpdated, AddressOf OnSubscriptionsUpdated
'            '-----------------------
'            ' Create Contect Menu as Add events
'            For Each EventType As Events.EventTypes In [Enum].GetValues(GetType(Events.EventTypes))
'                If Not EventType = Events.EventTypes.Unknown Then
'                    Dim tsb As New ToolStripButton
'                    tsb.Text = String.Format("Subscribe to {0}", EventType.ToString)
'                    tsb.Tag = EventType
'                    tsb.Name = EventType.ToString
'                    tsb.CheckState = CheckState.Unchecked
'                    AddHandler tsb.Click, AddressOf SubscribeToEvent_Click
'                    ContextMenuStrip.Items.Add(tsb)
'                End If
'            Next
'            '-----------------------
'            ' Initilise team server List
'            Refresh()
'        End Sub

'        Public Sub OnSubscriptionsUpdated(ByVal subscriptions As Collection(Of Subscription))
'            GenerateChildren(subscriptions)
'        End Sub

'        Protected Overrides Sub GenerateChildren(ByVal State As Object)
'            Me.ChangeStatus(Status.Working)
'            Dim subscriptions As Collection(Of Subscription) = Nothing
'            Try
'                subscriptions = EventHandler.SubscriptionsService.GetSubscriptions()
'            Catch ex As FaultException(Of TeamFoundationServerUnauthorizedException)
'                AddError("TFS Denied", ex)
'                Me.ChangeStatus(Status.Faulted)
'            Catch ex As ServiceModel.FaultException
'                AddError("Error", ex)
'                Me.ChangeStatus(Status.Faulted)
'            Finally
'                GenerateChildren(subscriptions)
'            End Try
'        End Sub

'        Public Overloads Sub GenerateChildren(ByVal subscriptions As Collection(Of Subscription))
'            ClearNodes()
'            If Not subscriptions Is Nothing Then
'                For Each s As Subscription In subscriptions
'                    AddNode(New TreeNode_Subscription(EventHandler, s))
'                    If ContextMenuStrip.Items.ContainsKey(s.EventType.ToString) Then
'                        Dim tsb As ToolStripButton = CType(ContextMenuStrip.Items(s.EventType.ToString), ToolStripButton)
'                        tsb.CheckState = CheckState.Checked
'                        tsb.Text = String.Format("Unsubscribe from {0}", s.EventType.ToString)
'                    End If
'                Next
'            End If
'            If Me.Nodes.Count = 0 Then
'                AddMessage("No Subscriptions found")
'            End If
'            ' Then make sure that all nodes are expanded
'            Me.ExpandAll()
'            Me.ChangeStatus(Status.Normal)
'        End Sub

'        Private Sub SubscribeToEvent_Click(ByVal sender As Object, ByVal e As EventArgs)
'            Dim tsb As ToolStripButton = CType(sender, ToolStripButton)
'            Dim eventtype As Events.EventTypes = CType(tsb.Tag, Events.EventTypes)
'            Select Case tsb.CheckState
'                Case CheckState.Checked
'                    EventHandler.SubscriptionsService.RemoveSubscriptions(EventHandler.SubscriptionsService.EndPoint.ToString)
'                Case CheckState.Unchecked
'                    EventHandler.SubscriptionsService.AddSubscriptions(EventHandler.ServerUri.ToString, eventtype)
'                Case CheckState.Indeterminate
'                    Me.Refresh()
'            End Select
'        End Sub

'    End Class

'End Namespace