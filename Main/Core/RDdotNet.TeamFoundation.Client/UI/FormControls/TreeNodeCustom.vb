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

        Private _Delay As Integer = 0

        Public Property Delay() As Integer
            Get
                Return _Delay
            End Get
            Set(ByVal value As Integer)
                _Delay = value
            End Set
        End Property

        Public Delegate Sub SimpleDelegate()
        Public Delegate Sub AddMessageDelegate(ByVal Message As String)
        Public Delegate Sub AddCatagoryErrorDelegate(ByVal Catagory As String, ByVal ex As Exception)
        Public Delegate Sub AddNodeDelegate(ByVal TreeNode As T)
        Public Delegate Sub UpdateStatusDelegate(ByVal Header As String, ByVal Status As Status)

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

        Protected Sub UpdateStatus(ByVal Header As String, ByVal Status As Status)
            If Me.TreeView.InvokeRequired Then
                Me.TreeView.Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), Header, Status)
                System.Threading.Thread.Sleep(_Delay)
            Else
                If Status = FormControls.Status.Normal Then
                    Me.Text = Header
                Else
                    Me.Text = String.Format("{0} ({1})", Header, Status.ToString)
                End If
            End If
        End Sub

    End Class

End Namespace