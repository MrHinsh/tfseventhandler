Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Config

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNodeCustom(Of TreeNode_TeamServer)

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New("Team Servers", EventHandler, Delay)
            '-----------------------
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

                    AddNode(New TreeNode_TeamServer(EventHandler, e.TeamServer))
                Case StatusChangeTypeEnum.ServerRemoved
                    For Each n As TreeNode_TeamServer In Me.Nodes
                        If n.TeamServer.Uri.ToString = e.TeamServer.Uri.ToString Then
                            RemoveNode(n)

                        End If
                    Next
                Case StatusChangeTypeEnum.ServerCheck
                    SyncLock Me.Nodes
                        If Not ContainsServer(e.TeamServer) Then
                            AddNode(New TreeNode_TeamServer(EventHandler, e.TeamServer))
                        End If
                    End SyncLock
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
            Dim TeamServers As Collection(Of ServerItemElement) = Nothing
            Try
                EventHandler.GetServers()
            Catch ex As Exception
                AddError("Error", ex)
                Me.ChangeStatus(Status.Faulted)
            Finally
                'GenerateChildren(TeamServers)
            End Try
        End Sub

        Public Overloads Sub CheckChildren()
            If Me.Nodes.Count = 0 Then
                'AddMessage("No Servers Found")
            Else
                'RemoveMessage("No Servers Found")
            End If
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


    End Class


End Namespace