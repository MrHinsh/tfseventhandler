Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Config

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNodeItems(Of TreeNodeItem(Of TeamServerItem), TeamServerItem)


        Private SubNodeNameMap As String = "TeamServer:{0}"

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New("Team Servers", EventHandler, Delay)

            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.TeamServerUpdated, AddressOf OnStatusUpdate
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Team Server", Nothing, AddressOf AddTeamServer_Click))
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Refresh Server List", Nothing, AddressOf Refresh_Click))
            '-----------------------
            ' Initilise team server List
            Refresh()
        End Sub

        Protected Overrides Sub OnStatusUpdate(ByVal source As Clients.TFSEventHandlerClient, ByVal e As Clients.StatusChangeEventArgs(Of Services.DataContracts.TeamServerItem))
            Me.TreeNode_Inner_ExpandAll()
            Select Case e.ChangeType
                Case StatusChangeTypeEnum.Item_Added
                    ' Fired when a new item is added on the server
                    TreeNode_ServerAdd(e.Item)
                Case StatusChangeTypeEnum.Item_Removed
                    ' Fired when an Item is removed from the server
                    Dim x As TreeNode_TeamServer = TreeNode_ServerFind(e.Item)
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
                    MsgBox(String.Format(ServerExistsMessage, e.Item.Name))
                Case StatusChangeTypeEnum.Item_NotExists
                    ' Fired when an items is removed that does not exists

            End Select
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.ChangeNodeStatus(Status.Working)
            System.Threading.Thread.Sleep(2000)
            Try
                EventHandler.RefreshServers()
            Catch ex As Exception
                TreeNode_Inner_AddError("Error", ex)
                Me.ChangeNodeStatus(Status.Faulted)
            Finally
                'GenerateChildren(TeamServers)
                CheckChildren()
            End Try
        End Sub

        Public Overloads Sub CheckChildren()
            TreeNode_Inner_CheckEmpty(SubNodeNameMap, "team Servers")
            ' Then make sure that all nodes are expanded
            Me.TreeNode_Inner_ExpandAll()
            Me.ChangeNodeStatus(Status.Normal)
        End Sub

        Private Sub AddTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim frmConnectTo As New frmConnectTo("Team Foundation Server", Protocol:=TeamFoundation.frmConnectTo.Protocol.HTTP, Port:=8080, ServerName:=System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName)
            Dim DialogResult As DialogResult = frmConnectTo.ShowDialog()
            If DialogResult = Windows.Forms.DialogResult.OK Then
                '---------
                Dim ServerUri As Uri = frmConnectTo.ServerUri
                Dim ServerItemElement As New TeamServerItem(ServerUri.ToString, ServerUri)
                EventHandler.AddServer(ServerItemElement)
                '---------
                frmConnectTo.Close()
                frmConnectTo.Dispose()
            End If
        End Sub

        Private Sub Refresh_Click(ByVal sender As Object, ByVal e As EventArgs)
            EventHandler.RefreshServers()
        End Sub


#Region " TreeNode Manipulation "

        Private Function TreeNode_ContainsServer(ByVal TeamServerItem As TeamServerItem) As Boolean
            TreeNode_ContainsServer = False
            For Each n As TreeNode_TeamServer In Me.Nodes
                If n.Item.Uri Is TeamServerItem.Uri Then
                    TreeNode_ContainsServer = True
                End If
            Next
        End Function

        Private Sub TreeNode_ServerAdd(ByVal TeamServer As TeamServerItem)
            Dim key As String = String.Format(SubNodeNameMap, TeamServer.Name)
            SyncLock Me.Nodes
                If Not TreeNode_ServerExists(TeamServer) Then
                    ' Add Node
                    UpdateInnerNode(NodeUpdateEnum.Add, New TreeNode_TeamServer(key, EventHandler, TeamServer))
                End If
            End SyncLock
        End Sub

        Private Function TreeNode_ServerExists(ByVal TeamServer As TeamServerItem) As Boolean
            Dim key As String = String.Format(SubNodeNameMap, TeamServer.Name)
            If Me.Nodes.Find(key, False).Count > 0 Then
                Return True
            End If
        End Function

        Private Function TreeNode_ServerFind(ByVal TeamServer As TeamServerItem) As TreeNode_TeamServer
            Dim key As String = String.Format(SubNodeNameMap, TeamServer.Name)
            Dim FoundNodes = Me.Nodes.Find(key, False)
            If FoundNodes.Count > 0 Then
                Return CType(FoundNodes(0), TreeNode_TeamServer)
            End If
            Return Nothing
        End Function

#End Region

    End Class


End Namespace
