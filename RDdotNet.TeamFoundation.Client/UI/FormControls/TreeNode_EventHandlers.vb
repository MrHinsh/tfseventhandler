Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandlers
        Inherits TreeNodeCustom(Of TreeNode_AssemblyItem)

        Public Sub New(ByVal EventHandler As Servers.TFSEventHandlerServer, Optional ByVal Delay As Integer = 0)
            MyBase.New("Event Handlers", EventHandler, Delay)
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.HandlersService.HandlersUpdated, AddressOf OnHandlersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Assembly")) 'TODO: , Nothing, AddressOf AddTeamServer_Click))
            '-----------------------
            ' Initilise Assembly List
            Refresh()
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.ChangeStatus(Status.Working)
            Dim AssemblyManaifest As AssemblyManaifest = Nothing
            Try
                AssemblyManaifest = EventHandler.HandlersService.GetAssemblys()
            Catch ex As Exception
                AddError("Error", ex)
                Me.ChangeStatus(Status.Faulted)
            Finally
                Me.GenerateChildren(AssemblyManaifest)
            End Try
        End Sub

        Public Overloads Sub GenerateChildren(ByVal AssemblyManaifest As AssemblyManaifest)
            ClearNodes()
            If AssemblyManaifest Is Nothing Then
                AddMessage("AssemblyManaifest not retrieved")
            Else
                If Not AssemblyManaifest.Assemblys Is Nothing Then
                    For Each AI As AssemblyItem In AssemblyManaifest.Assemblys
                        AddNode(New TreeNode_AssemblyItem(EventHandler, AI))
                    Next
                End If
                If Me.Nodes.Count = 0 Then
                    AddMessage("No Assemblies found")
                End If
            End If
            Me.ChangeStatus(Status.Faulted)
            ' Then make sure that all nodes are expanded
            Me.ExpandAll()
        End Sub

        Public Sub OnHandlersUpdated(ByVal AssemblyManaifest As RDdotNet.TeamFoundation.Services.DataContracts.AssemblyManaifest)
            GenerateChildren(AssemblyManaifest)
        End Sub

    End Class


End Namespace