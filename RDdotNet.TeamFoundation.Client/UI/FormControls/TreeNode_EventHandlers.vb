Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandlers
        Inherits TreeNodeCustom(Of TreeNode_AssemblyItem)

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            Me.Text = "Event Handlers"
            Me.Delay = Delay
            '-----------------------
            ' Create Handler and attach Events
            _EventHandler = EventHandler
            AddHandler _EventHandler.HandlersUpdated, AddressOf OnHandlersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            _ContextMenuStrip = New ContextMenuStrip
            _ContextMenuStrip.Items.Add(New ToolStripButton("Add Assembly")) 'TODO: , Nothing, AddressOf AddTeamServer_Click))
            Me.ContextMenuStrip = _ContextMenuStrip
            '-----------------------
            ' Initilise Assembly List
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Public Sub GenerateChildren(ByVal state As Object)
            Me.UpdateStatus("Event Handlers", Status.Working)
            Dim AssemblyManaifest As AssemblyManaifest = Nothing
            Try
                AssemblyManaifest = _EventHandler.GetAssemblys()
            Catch ex As Exception
                AddError("Error", ex)
                Me.UpdateStatus("Event Handler", Status.Faulted)
            Finally
                Me.GenerateChildren(AssemblyManaifest)
            End Try
        End Sub

        Public Sub GenerateChildren(ByVal AssemblyManaifest As AssemblyManaifest)
            ClearNodes()
            If Not AssemblyManaifest.Assemblys Is Nothing Then
                For Each AI As AssemblyItem In AssemblyManaifest.Assemblys
                    AddNode(New TreeNode_AssemblyItem(EventHandler, AI))
                Next
            End If
            If Me.Nodes.Count = 0 Then
                AddMessage("No Assemblies found")
            End If
            Me.UpdateStatus("Event Handlers", Status.Normal)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub OnHandlersUpdated(ByVal AssemblyManaifest As RDdotNet.TeamFoundation.Services.DataContracts.AssemblyManaifest)
            GenerateChildren(AssemblyManaifest)
        End Sub

    End Class


End Namespace