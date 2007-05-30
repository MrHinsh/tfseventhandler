Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNodeCustom(Of TreeNode_TeamServer)

        Public Sub New(ByVal EventHandler As TFSEventHandlerServer, Optional ByVal Delay As Integer = 0)
            MyBase.New("Team Servers", EventHandler, Delay)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.TeamServersUpdated, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Team Server", Nothing, AddressOf AddTeamServer_Click))
            '-----------------------
            ' Initilise team server List
            Refresh()
        End Sub

        Public Sub OnTeamServersUpdated(ByVal TeamServers() As String)
            GenerateChildren(TeamServers)
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.ChangeStatus(Status.Working)
            Dim TeamServers() As String = Nothing
            Try
                TeamServers = EventHandler.GetServers()
            Catch ex As Exception
                AddError("Error", ex)
                Me.ChangeStatus(Status.Faulted)
            Finally
                GenerateChildren(TeamServers)
            End Try
        End Sub

        Public Overloads Sub GenerateChildren(ByVal TeamServers() As String)
            ClearNodes()
            If Not TeamServers Is Nothing Then
                For Each s As String In TeamServers
                    AddNode(New TreeNode_TeamServer(EventHandler, s))
                Next
            End If
            If Me.Nodes.Count = 0 Then
                AddMessage("No Servers Found")
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
                frmConnectTo.Close()
                frmConnectTo.Dispose()
                '---------
                Dim ServerUri As Uri = frmConnectTo.ServerUri
                EventHandler.AddServer(ServerUri.ToString, ServerUri.ToString)
            End If
        End Sub


    End Class


End Namespace