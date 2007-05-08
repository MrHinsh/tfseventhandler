Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_TeamServers
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = "Team Servers"
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
            GenerateChildren()
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub OnTeamServersUpdated(ByVal TeamServers() As String)
            GenerateChildren(TeamServers)
        End Sub

        Public Sub GenerateChildren(Optional ByVal TeamServers() As String = Nothing)
            Me.Nodes.Clear()
            Try
                If TeamServers Is Nothing Then
                    TeamServers = _EventHandler.GetServers()
                End If
                For Each s As String In TeamServers
                    Me.Nodes.Add(New TreeNode_TeamServer(EventHandler, s))
                Next
            Catch ex As Exception
                Me.Nodes.Add("Error:" & ex.ToString)
            End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Servers Found")
            End If
        End Sub

        Private Sub AddTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As String = InputBox("Enter url", DefaultResponse:="http://localhost:8080")
            Dim url As New Uri(x)
            _EventHandler.AddServer(x.ToString, x.ToString)
        End Sub

    End Class


End Namespace