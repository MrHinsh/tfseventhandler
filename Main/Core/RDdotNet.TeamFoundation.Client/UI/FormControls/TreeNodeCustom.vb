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
            Me.Name = NodeName
        End Sub

        Public Sub Refresh()
            ' Initilise worker thread
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf GenerateChildren)
        End Sub

        Protected MustOverride Sub GenerateChildren(ByVal state As Object)

#Region " Thread Safe "

        Public Delegate Sub SimpleDelegate()
        Public Delegate Sub AddMessageDelegate(ByVal Message As String)
        Public Delegate Sub AddCatagoryErrorDelegate(ByVal Catagory As String, ByVal ex As Exception)
        Public Delegate Sub AddNodeDelegate(ByVal TreeNode As T)
        Public Delegate Sub UpdateStatusDelegate(ByVal Status As Status)

        Protected Sub ClearNodes()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf ClearNodes))
                System.Threading.Thread.Sleep(_Delay)
            Else
                MyBase.Nodes.Clear()
            End If
        End Sub

        Protected Overloads Sub ExpandAll()
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New SimpleDelegate(AddressOf ExpandAll))
                System.Threading.Thread.Sleep(_Delay)
            Else
                MyBase.ExpandAll()
            End If
        End Sub

        Protected Sub AddError(ByVal Catagory As String, ByVal ex As Exception)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New AddCatagoryErrorDelegate(AddressOf AddError), Catagory, ex)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(Catagory & ": " & ex.ToString)
            End If
        End Sub

        Protected Sub AddMessage(ByVal Message As String)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New AddMessageDelegate(AddressOf AddMessage), Message)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(Message)
            End If
        End Sub

        Protected Sub AddNode(ByVal TreeNode As T)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New AddNodeDelegate(AddressOf AddNode), TreeNode)
                System.Threading.Thread.Sleep(_Delay)
            Else
                Me.Nodes.Add(TreeNode)
            End If
        End Sub

        Protected Sub ChangeStatus(ByVal Status As Status)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New UpdateStatusDelegate(AddressOf ChangeStatus), Status)
                System.Threading.Thread.Sleep(_Delay)
            Else
                _Status = Status
                Select Case Status
                    Case FormControls.Status.Normal
                        Me.Text = Me.NodeName
                    Case FormControls.Status.Working
                        Me.Text = String.Format("{0} ({1}...)", Me.NodeName, Status.ToString)
                    Case FormControls.Status.Faulted
                        Me.Text = String.Format("{0} ({1})", Me.NodeName, Status.ToString)
                End Select
            End If
        End Sub


#End Region

  
    End Class

End Namespace