Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Public MustInherit Class TreeNodeCustom(Of T As {TreeNode})
        Inherits TreeNode

        Private _EventHandler As TFSEventHandlerClient
        Private _Delay As Integer = 0
        Private _NodeName As String = "[Enter Node Name]"
        Private _Status As Status = Status.Normal

        Public ReadOnly Property EventHandler() As TFSEventHandlerClient
            Get
                Return _EventHandler
            End Get
        End Property

        Public ReadOnly Property Status() As Status
            Get
                Return _Status
            End Get
        End Property

        Public Shadows ReadOnly Property ContextMenuStrip() As ContextMenuStrip
            Get
                If MyBase.ContextMenuStrip Is Nothing Then
                    MyBase.ContextMenuStrip = New ContextMenuStrip
                End If
                Return MyBase.ContextMenuStrip
            End Get
        End Property

        Public ReadOnly Property NodeName() As String
            Get
                Return _NodeName
            End Get
        End Property

        Public Property Delay() As Integer
            Get
                Return _Delay
            End Get
            Set(ByVal value As Integer)
                _Delay = value
            End Set
        End Property

        Public Sub New(ByVal NodeName As String, ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            _EventHandler = EventHandler
            _Delay = Delay
            _NodeName = NodeName
            Me.Text = NodeName
        End Sub

        Public Sub Refresh()
            ' Initilise worker thread
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Protected MustOverride Sub GenerateChildren(ByVal state As Object)

#Region " Thread Safe "

        Public Delegate Sub SimpleDelegate()
        Public Delegate Sub MessageDelegate(ByVal Key As String, ByVal Message As String)
        Public Delegate Sub AddCatagoryErrorDelegate(ByVal Catagory As String, ByVal ex As Exception)
        Public Delegate Sub ModifyNodeDelegate(ByVal TreeNode As T)
        Public Delegate Sub UpdateStatusDelegate(ByVal Status As Status)

        Protected Sub TreeNode_Inner_ClearNodes()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf TreeNode_Inner_ClearNodes))
                System.Threading.Thread.Sleep(_Delay)
            Else
                MyBase.Nodes.Clear()
            End If
        End Sub

        Protected Overloads Sub TreeNode_Inner_ExpandAll()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf TreeNode_Inner_ExpandAll))
                System.Threading.Thread.Sleep(_Delay)
            Else
                MyBase.ExpandAll()
            End If
        End Sub

        Protected Sub TreeNode_Inner_AddError(ByVal Catagory As String, ByVal ex As Exception)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New AddCatagoryErrorDelegate(AddressOf TreeNode_Inner_AddError), Catagory, ex)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(Catagory & ": " & ex.ToString)
            End If
        End Sub

        Protected Sub TreeNode_Inner_CheckEmpty(ByVal KeyMask As String, ByVal ItemTypeName As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New MessageDelegate(AddressOf TreeNode_Inner_CheckEmpty), KeyMask, ItemTypeName)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Dim key As String = String.Format(KeyMask, "NoItems")
                Dim messagemask As String = "No {0} Found"

                If Me.Nodes.Count > 0 Then
                    Dim matches As TreeNode() = Me.Nodes.Find(key, False)
                    If matches.Count > 0 Then
                        Me.Nodes.RemoveByKey(key)
                    End If
                Else
                    Me.Nodes.Add(key, String.Format(messagemask, ItemTypeName))
                End If
            End If
        End Sub

        Protected Sub TreeNode_Inner_AddMessage(ByVal Key As String, ByVal Message As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New MessageDelegate(AddressOf TreeNode_Inner_AddMessage), Key, Message)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(Key, Message)
            End If
        End Sub

        Protected Sub TreeNode_Inner_RemoveMessage(ByVal Key As String, ByVal Message As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New MessageDelegate(AddressOf TreeNode_Inner_AddMessage), Key, Message)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.RemoveByKey(Key)
            End If
        End Sub

        Protected Sub TreeNode_Inner_AddNode(ByVal TreeNode As T)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New ModifyNodeDelegate(AddressOf TreeNode_Inner_AddNode), TreeNode)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(TreeNode)
            End If
        End Sub

        Protected Sub TreeNode_Inner_RemoveNode(ByVal TreeNode As T)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New ModifyNodeDelegate(AddressOf TreeNode_Inner_RemoveNode), TreeNode)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Remove(TreeNode)
            End If
        End Sub

        Protected Sub TreeNode_Inner_ChangeStatus(ByVal Status As Status, Optional ByVal Message As String = "")
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New UpdateStatusDelegate(AddressOf TreeNode_Inner_ChangeStatus), Status)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Dim MessageMask As String = "{0} ({1}{2}{3})"
                Dim Spacer As String = ""
                If Message <> "" Then Spacer = " - "
                _Status = Status
                Select Case Status
                    Case FormControls.Status.Normal
                        Me.Text = Me.NodeName
                    Case FormControls.Status.Working, FormControls.Status.Faulted
                        Me.Text = String.Format(MessageMask, Me.NodeName, Status.ToString, Spacer, Message)
                End Select
            End If
        End Sub


#End Region


    End Class

End Namespace