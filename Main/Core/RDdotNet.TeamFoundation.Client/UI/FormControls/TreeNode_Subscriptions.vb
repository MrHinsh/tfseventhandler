Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Services.FaultContracts
Imports system.ServiceModel
Imports System.Windows.Forms

Namespace UI.FormControls


    Friend Class TreeNode_Subscriptions
        Inherits TreeNodeCustom(Of TreeNode_Subscription)

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New("Subscriptions", EventHandler, Delay)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.SubscriptionsUpdated, AddressOf OnSubscriptionsUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Subscription", Nothing, AddressOf AddSubScription_Click))
            '-----------------------
            ' Initilise team server List
            Refresh()
        End Sub

        Public Sub OnSubscriptionsUpdated(ByVal subscriptions As Collection(Of Subscription))
            GenerateChildren(subscriptions)
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal State As Object)
            Me.ChangeStatus(Status.Working)
            Dim subscriptions As Collection(Of Subscription) = Nothing
            Try
                subscriptions = EventHandler.GetSubscriptions()
            Catch ex As ServiceModel.FaultException
                AddError("Error", ex)
                Me.ChangeStatus(Status.Faulted)
            Finally
                GenerateChildren(subscriptions)
            End Try
        End Sub

        Public Overloads Sub GenerateChildren(ByVal subscriptions As Collection(Of Subscription))
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
            Me.ChangeStatus(Status.Normal)
        End Sub

        Private Sub AddSubScription_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim frmConnectTo As New frmConnectTo("Team Foundation Server", Protocol:=TeamFoundation.frmConnectTo.Protocol.HTTP, Port:=8080, ServerName:=System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName)
            Dim DialogResult As DialogResult = frmConnectTo.ShowDialog()
            If DialogResult = Windows.Forms.DialogResult.OK Then
                '---------
                frmConnectTo.Close()
                frmConnectTo.Dispose()
                '---------
                Dim ServerUri As Uri = EventHandler.ServceUrl
                EventHandler.AddSubscriptions("http://Moo", Events.EventTypes.WorkItemChangedEvent)
            End If
        End Sub

    End Class

End Namespace