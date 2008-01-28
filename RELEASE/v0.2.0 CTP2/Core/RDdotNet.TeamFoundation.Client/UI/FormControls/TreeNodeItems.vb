Imports system.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Windows.Forms

Namespace UI.FormControls

    Public MustInherit Class TreeNodeItems(Of TSubNodes As {TreeNodeItem(Of TItem)}, TItem)
        Inherits TreeNodeItem(Of TItem)

        Private _EventHandler As TFSEventHandlerClient
        Private _Delay As Integer = 0

        Public Property Delay() As Integer
            Get
                Return _Delay
            End Get
            Set(ByVal value As Integer)
                _Delay = value
            End Set
        End Property

        Public Sub New(ByVal KEY As String, ByVal EventHandler As TFSEventHandlerClient, Optional ByVal Delay As Integer = 0)
            MyBase.New(KEY, EventHandler, Nothing)
            _EventHandler = EventHandler
            _Delay = Delay
        End Sub

        Public Sub Refresh()
            ' Initilise worker thread
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Protected MustOverride Sub GenerateChildren(ByVal state As Object)

#Region " Thread Safe "

        Protected Delegate Sub ModifyNodeDelegate(ByVal NodeUpdate As NodeUpdateEnum, ByVal TreeNode As TSubNodes)

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

        Protected Sub UpdateInnerNode(ByVal NodeUpdate As NodeUpdateEnum, ByVal TreeNode As TSubNodes)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New ModifyNodeDelegate(AddressOf UpdateInnerNode), NodeUpdate, TreeNode)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Select Case NodeUpdate
                    Case NodeUpdateEnum.Add
                        Me.Nodes.Add(TreeNode)
                    Case NodeUpdateEnum.Delete
                        Me.Nodes.Remove(TreeNode)
                    Case NodeUpdateEnum.Modify, NodeUpdateEnum.None
                        Throw New Exception("Not yet implemented")
                End Select
            End If
        End Sub

    

#End Region


    End Class

    Public Enum NodeUpdateEnum
        None
        Add
        Modify
        Delete
    End Enum

End Namespace