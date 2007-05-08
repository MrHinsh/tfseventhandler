Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNodeCustom(Of TreeNode_TeamServer)

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip
        Private _DataThread As System.Threading.Thread

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            Me.Text = "Team Servers"
            Me.Delay = Delay
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            AddHandler _EventHandler.TeamServersUpdated, AddressOf OnTeamServersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Add Team Server", Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise team server List
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnTeamServersUpdated(ByVal TeamServers() As String)
            GenerateChildren(TeamServers)
        End Sub

        Public Sub GenerateChildren(ByVal state As Object)
            Me.UpdateStatus("Team Servers", Status.Working)
            Dim TeamServers() As String = Nothing
            Try
                TeamServers = _EventHandler.GetServers()
            Catch ex As Exception
                AddError("Error", ex)
                Me.UpdateStatus("Team Servers", Status.Faulted)
            Finally
                GenerateChildren(TeamServers)
            End Try
        End Sub

        Public Sub GenerateChildren(ByVal TeamServers() As String)
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
            Me.UpdateStatus("Team Servers", Status.Normal)
        End Sub

        Private Sub AddTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim frmConnectTo As New frmConnectTo("Team Foundation Server", Protocol:=TeamFoundation.frmConnectTo.Protocol.HTTP, Port:=8080, ServerName:=My.Computer.Name)
            Dim DialogResult As DialogResult = frmConnectTo.ShowDialog(Me)
            If DialogResult = Windows.Forms.DialogResult.OK Then
                '---------
                frmConnectTo.Close()
                frmConnectTo.Dispose()
                '---------
                Dim ServerUri As Uri = frmConnectTo.ServerUri
                _EventHandler.AddServer(ServerUri.ToString, ServerUri.ToString)
            End If
        End Sub


    End Class


End Namespace