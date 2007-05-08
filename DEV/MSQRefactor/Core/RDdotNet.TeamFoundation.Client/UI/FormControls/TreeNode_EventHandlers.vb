Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandlers
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _ContextMenuStrip As New ContextMenuStrip

        Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient)
            Me.Text = "Event Handlers"
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
            GenerateChildren()
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub GenerateChildren(Optional ByVal AssemblyManaifest As AssemblyManaifest = Nothing)
            Me.Nodes.Clear()
            Try
                If AssemblyManaifest Is Nothing Then
                    AssemblyManaifest = _EventHandler.GetAssemblys()
                End If
                If Not AssemblyManaifest.Assemblys Is Nothing Then
                    For Each AI As AssemblyItem In AssemblyManaifest.Assemblys
                        Me.Nodes.Add(New TreeNode_AssemblyItem(EventHandler, AI))
                    Next
                End If
            Catch ex As Exception
                Me.Nodes.Add("Error: " & ex.ToString)
            End Try
            If Me.Nodes.Count = 0 Then
                Me.Nodes.Add("No Assemblies found")
            End If
        End Sub

        Public Sub OnHandlersUpdated(ByVal AssemblyManaifest As RDdotNet.TeamFoundation.Services.DataContracts.AssemblyManaifest)
            GenerateChildren(AssemblyManaifest)
        End Sub

    End Class


End Namespace