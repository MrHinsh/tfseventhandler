Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Public Enum Status
        Normal = 0
        Faulted = 1
        Working = 2
    End Enum

    Public MustInherit Class TreeNodeItem(Of TItem)
        Inherits TreeNode

        Private m_EventHandler As TFSEventHandlerClient
        Private m_Item As TItem
        Private m_Status As Status = Status.Normal

        Public Property Status() As Status
            Get
                Return m_Status
            End Get
            Protected Set(ByVal value As Status)
                m_Status = value
            End Set
        End Property

        Public ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return m_EventHandler
            End Get
        End Property

        Public Property Item() As TItem
            Get
                Return m_Item
            End Get
            Protected Set(ByVal value As TItem)
                m_Item = value
            End Set
        End Property

        Public Shadows ReadOnly Property ContextMenuStrip() As ContextMenuStrip
            Get
                If MyBase.ContextMenuStrip Is Nothing Then
                    MyBase.ContextMenuStrip = New ContextMenuStrip
                End If
                Return MyBase.ContextMenuStrip
            End Get
        End Property

        Public Sub New(ByVal KEY As String, ByVal EventHandler As TFSEventHandlerClient, ByVal Item As TItem)
            m_EventHandler = EventHandler
            m_Item = Item
            Me.Text = KEY
            Me.Name = KEY
        End Sub

        Protected MustOverride Sub OnStatusUpdate(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of TItem))


#Region " Thread Safe "

        Public Delegate Sub SimpleDelegate()
        Public Delegate Sub MessageDelegate(ByVal Key As String, ByVal Message As String)
        Public Delegate Sub AddCatagoryErrorDelegate(ByVal Catagory As String, ByVal ex As Exception)
        Public Delegate Sub UpdateStatusDelegate(ByVal Status As Status)

        Protected Sub TreeNode_Inner_ClearNodes()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf TreeNode_Inner_ClearNodes))
                System.Threading.Thread.Sleep(100)
            Else
                MyBase.Nodes.Clear()
            End If
        End Sub

        Protected Overloads Sub TreeNode_Inner_ExpandAll()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf TreeNode_Inner_ExpandAll))
                System.Threading.Thread.Sleep(100)
            Else
                MyBase.ExpandAll()
            End If
        End Sub

        Protected Sub TreeNode_Inner_AddError(ByVal Catagory As String, ByVal ex As Exception)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New AddCatagoryErrorDelegate(AddressOf TreeNode_Inner_AddError), Catagory, ex)
                System.Threading.Thread.Sleep(100)
            Else
                Me.Nodes.Add(Catagory & ": " & ex.ToString)
            End If
        End Sub

        Protected Sub TreeNode_Inner_AddMessage(ByVal Key As String, ByVal Message As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New MessageDelegate(AddressOf TreeNode_Inner_AddMessage), Key, Message)
                System.Threading.Thread.Sleep(100)
            Else
                Me.Nodes.Add(Key, Message)
            End If
        End Sub

        Protected Sub TreeNode_Inner_RemoveMessage(ByVal Key As String, ByVal Message As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New MessageDelegate(AddressOf TreeNode_Inner_AddMessage), Key, Message)
                System.Threading.Thread.Sleep(100)
            Else
                Me.Nodes.RemoveByKey(Key)
            End If
        End Sub

        Protected Sub ChangeNodeStatus(ByVal Status As Status, Optional ByVal Message As String = "")
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New UpdateStatusDelegate(AddressOf ChangeNodeStatus), Status)
                System.Threading.Thread.Sleep(100)
            Else
                Dim MessageMask As String = "{0} ({1}{2}{3})"
                Dim Spacer As String = ""
                If Message <> "" Then Spacer = " - "
                Select Case Status
                    Case FormControls.Status.Normal
                        Me.Text = Me.Name
                    Case FormControls.Status.Working, FormControls.Status.Faulted
                        Me.Text = String.Format(MessageMask, Me.Name, Status.ToString, Spacer, Message)
                End Select
            End If
        End Sub




#End Region


    End Class

End Namespace