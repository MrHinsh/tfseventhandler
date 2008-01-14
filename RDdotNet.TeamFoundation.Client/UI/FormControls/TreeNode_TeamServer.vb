Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms
Imports RDdotNet.TeamFoundation.Config

Namespace UI.FormControls

    Friend Class TreeNode_TeamServer
        Inherits TreeNode

        Private m_EventHandler As TFSEventHandlerClient
        Private m_TeamServer As TeamServerItem
        Private m_ContextMenuStrip As New ContextMenuStrip
        Private _SubscriptionsNode As TreeNode_Subscriptions

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal TeamServer As TeamServerItem)
            Me.Text = TeamServer.Name
            m_TeamServer = TeamServer
            '-----------------------
            ' Create Handler and attach Events
            m_EventHandler = EventHandler
            AddHandler m_EventHandler.TeamServerUpdated, AddressOf OnTeamServersUpdated
            '-----------------------
            RunChecks()
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return m_EventHandler
            End Get
        End Property

        Friend ReadOnly Property TeamServer() As TeamServerItem
            Get
                Return m_TeamServer
            End Get
        End Property

        Private Sub RunChecks()
            '------------------------
            ' Create Contect Menu as Add events
            m_ContextMenuStrip = New ContextMenuStrip
            m_ContextMenuStrip.Items.Add(New ToolStripButton("Remove Team Server", Nothing, AddressOf RemoveTeamServer_Click))
            If Not Me.TeamServer.HasAuthenticated Then
                m_ContextMenuStrip.Items.Add(New ToolStripButton("Authenticate Team Server", Nothing, AddressOf RemoveTeamServer_Click))
            End If
            Me.ContextMenuStrip = m_ContextMenuStrip
            '---------------------
            If Not Me.TeamServer.IsValid Then
                Me.ForeColor = Drawing.Color.Red
            Else
                Me.ForeColor = Drawing.Color.DarkGreen
            End If
            '---------------------
        End Sub

        Private Sub RemoveTeamServer_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Get Selected Team Server
            Dim TeamServer As TreeNode_TeamServer = CType(Me.TreeView.SelectedNode, TreeNode_TeamServer)
            ' Call remove
            m_EventHandler.RemoveServer(m_TeamServer)
        End Sub

        Public Sub OnTeamServersUpdated(ByVal source As TFSEventHandlerClient, ByVal e As TeamServerEventArgs)
            ' Only run for own Team Serfver details
            Select Case e.ChangeType
                Case StatusChangeTypeEnum.ServerCheck, StatusChangeTypeEnum.ServerAuthenticated, StatusChangeTypeEnum.ServerAuthenticationFailed
                    If e.TeamServer.Uri.ToString = m_TeamServer.Uri.ToString Then
                        m_TeamServer = e.TeamServer
                        RunChecks()
                    End If
            End Select
            If e.ChangeType = StatusChangeTypeEnum.ServerAuthenticated Then
                StartSubscriptions()
            End If
      
        End Sub

        Private Delegate Sub delStartSubscriptions()

        Private Sub StartSubscriptions()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New delStartSubscriptions(AddressOf StartSubscriptions))
            Else
                Dim nodeID As Integer = Me.Nodes.Add(New TreeNode_Subscriptions(EventHandler, TeamServer, 100))
                _SubscriptionsNode = Me.Nodes(nodeID)
            End If
        End Sub

    End Class


End Namespace