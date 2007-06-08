Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandlerItem
        Inherits TreeNode

        Private _EventHandler As Servers.TFSEventHandlerServer
        Private _EventHandlerItem As EventHandlerItem
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As Servers.TFSEventHandlerServer, ByVal EventHandlerItem As EventHandlerItem)
            Me.Text = String.Format("{0} ({1})", EventHandlerItem.HandlerType.FullName, EventHandlerItem.EventType.ToString)
            _EventHandlerItem = EventHandlerItem
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

    End Class

End Namespace