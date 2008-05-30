Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_Subscription
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal Subscription As Subscription)
            Me.Text = String.Format("{0} {1} {2}", Subscription.ID, Subscription.EventType.ToString, Subscription.Address)
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove")) ' TODO:, Nothing, AddressOf RemoveTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

    End Class


End Namespace