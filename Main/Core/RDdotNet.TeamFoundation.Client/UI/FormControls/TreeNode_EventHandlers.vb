Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Friend Class TreeNode_EventHandlers
        Inherits TreeNodeCustom(Of TreeNode_AssemblyItem)

        Private m_Eventhandler As TFSEventHandlerClient

        Private SubNodeNameMap As String = "EventHandler:{0}"

        Public Sub New(ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New("Event Handlers", EventHandler, Delay)
            '-----------------------
            m_Eventhandler = EventHandler
            '-----------------------
            ' Create Handler and attach Events
            AddHandler EventHandler.HandlersUpdated, AddressOf OnHandlersUpdated
            '-----------------------
            ' Create Contect Menu as Add events
            ContextMenuStrip.Items.Add(New ToolStripButton("Add Assembly", Nothing, AddressOf AddAssembly_Click))
            '-----------------------
            ' Initilise Assembly List
            Refresh()
        End Sub

        Protected Overrides Sub GenerateChildren(ByVal state As Object)
            Me.TreeNode_Inner_ChangeStatus(Status.Working)
            Dim AssemblyItems As Collection(Of AssemblyItem) = Nothing
            Try
                AssemblyItems = EventHandler.GetAssemblys()
            Catch ex As Exception
                TreeNode_Inner_AddError("Error", ex)
                Me.TreeNode_Inner_ChangeStatus(Status.Faulted)
            Finally
                Me.GenerateChildren(AssemblyItems)
            End Try
        End Sub

        Public Overloads Sub GenerateChildren(ByVal AssemblyItems As Collection(Of AssemblyItem))
            TreeNode_Inner_ClearNodes()
            If Not AssemblyItems Is Nothing Then
                For Each AI As AssemblyItem In AssemblyItems
                    TreeNode_Inner_AddNode(New TreeNode_AssemblyItem(EventHandler, AI))
                Next
            End If
            TreeNode_Inner_CheckEmpty(SubNodeNameMap, "Event Handlers")
            Me.TreeNode_Inner_ChangeStatus(Status.Normal)
            ' Then make sure that all nodes are expanded
            Me.TreeNode_Inner_ExpandAll()
        End Sub

        Public Sub OnHandlersUpdated(ByVal AssemblyManaifest As RDdotNet.TeamFoundation.Services.DataContracts.AssemblyManaifest)
            GenerateChildren(AssemblyManaifest)
        End Sub

        Public Sub AddAssembly_Click(ByVal source As Object, ByVal e As EventArgs)
            Dim x As New FormLoadHandler(m_Eventhandler)
            x.ShowDialog()
        End Sub

    End Class


End Namespace