Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_AssemblyItem
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _AssemblyItem As AssemblyItem
        Private _ContextMenuStrip As New ContextMenuStrip

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, ByVal AssemblyItem As AssemblyItem)
            If AssemblyItem.State = AssemblyItemStates.NotFound Then
                Me.Text = "Assembly NotFound: " & AssemblyItem.Location
            Else
                Me.Text = "Assembly: " & AssemblyItem.Name
            End If

            _AssemblyItem = AssemblyItem
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Remove")) 'TODO: , Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise team server List
            GenerateChildren(_AssemblyItem.EventHandlers)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub GenerateChildren(ByVal EventHandlers As Collection(Of EventHandlerItem))
            Me.Nodes.Clear()
            If Not EventHandlers Is Nothing Then
                For Each EHI As EventHandlerItem In EventHandlers
                    Me.Nodes.Add(New TreeNode_EventHandlerItem(_EventHandler, EHI))
                Next
            End If
           
        End Sub

    End Class

End Namespace