Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Config

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNodeCustom(Of TreeNode_TeamServer)

        Private SubNodeNameMap As String = "TeamServer:{0}"

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New("Team Servers", EventHandler, Delay)
            '-----------------------
            Me.Name = "TreeNode_TeamServers"
            ' Create Handler and attach Events
            AddHandler EventHandler.TeamServerUpdated, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Team Server", Nothing, AddressOf AddTeamServer_Click))
            '-----------------------
            ' Initilise team server List
            Refresh()
        End Sub

        Public Sub OnTeamServersUpdated(ByVal source As TFSEventHandlerClient, ByVal e As TeamServerEventArgs)
            Me.ExpandAll()
            Select Case e.ChangeType
                Case StatusChangeTypeEnum.ServerAdded
                    AddServer(e.TeamServer)
                Case StatusChangeTypeEnum.ServerRemoved
                    For Each n As TreeNode_TeamServer In Me.Nodes
                        If n.TeamServer.Uri.ToString = e.TeamServer.Uri.ToString Then
                            RemoveNode(n)
                        End If
                    Next
                Case StatusChangeTypeEnum.ServerCheck
                    AddServer(e.TeamServer)
                Case StatusChangeTypeEnum.ServerCheckEnded
                    CheckChildren()
            End Select
        End Sub

        Private Function ContainsServer(ByVal TeamServerItem As TeamServerItem) As Boolean
            ContainsServer = False
            For Each n As TreeNode_TeamServer In Me.Nodes
                If n.TeamServer.Uri Is TeamServerItem.Uri Then
                    ContainsServer = True
                End If
            Next
        End Function

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.ChangeStatus(Status.Working)
            Dim TeamServers As Collection(Of TeamServerItem) = Nothing
            Try
                TeamServers = EventHandler.GetServers()
                For Each tsi As TeamServerItem In TeamServers
                    AddServer(tsi)
                Next
            Catch ex As Exception
                AddError("Error", ex)
                Me.ChangeStatus(Status.Faulted)
            Finally
                'GenerateChildren(TeamServers)
            End Try
        End Sub

        Public Overloads Sub CheckChildren()
            CheckEmpty(SubNodeNameMap, "team Servers")
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
            Me.ChangeStatus(Status.Normal)
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

        Private Sub AddServer(ByVal TeamServer As TeamServerItem)
            Dim key As String = String.Format(SubNodeNameMap, TeamServer.Name)
            SyncLock Me.Nodes
                If Not ServerExists(TeamServer) Then
                    ' Add Node
                    AddNode(New TreeNode_TeamServer(key, EventHandler, TeamServer))
                    EventHandler.RefreshServers()
                End If
            End SyncLock
        End Sub

        Private Function ServerExists(ByVal TeamServer As TeamServerItem) As Boolean
            Dim key As String = String.Format(SubNodeNameMap, TeamServer.Name)
            If Me.Nodes.Find(key, False).Count > 0 Then
                Return True
            End If
        End Function


    End Class


End Namespace
