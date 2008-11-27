Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Hinshelwood.TeamFoundation
Imports Hinshelwood.TeamFoundation.Config
Imports Microsoft.TeamFoundation.Client
Imports Microsoft.TeamFoundation

Public Enum Status
    None
    Initializing
    Connecting
    Connected
    Closing
    Closed
    InitializationComplete
    StopError
    External
    ObjectCreated
    ObjectCreation
End Enum

Public MustInherit Class AManager(Of TManagedType As {Item})
    Implements IDisposable

    Private _Initilised As Boolean = False

    Public Event [Error](ByVal ManagedType As TManagedType, ByVal Status As Status, ByVal Items As Integer, ByVal e As Exception)
    Public Event StatusChange(ByVal ManagedType As TManagedType, ByVal Status As Status, ByVal Items As Integer)

    Public ReadOnly Property Settings() As Config.SettingsSection
        Get
            Return SettingsSection.Instance
        End Get
    End Property

    Private Sub New()

    End Sub

    Public Sub New(ByVal Initialise As Boolean)
        If Initialise Then Initilise()
    End Sub

    Public Sub Initilise()
        Me.OnStatusChange(Nothing, Status.Initializing, -1)
        If _Initilised Then
            Me.OnError(Nothing, Status.Initializing, -1, New Exception("This TeamServerManager has already been initilised."))
            Me.OnStatusChange(Nothing, Status.StopError, -1)
        End If
        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf ManagerBegin)
    End Sub

    Private Sub ManagerBegin(ByVal state As Object)
        _Initilised = True
        ManagerBeginCustom(state)
        Me.OnStatusChange(Nothing, Status.InitializationComplete, -1)
    End Sub

    MustOverride Sub ManagerBeginCustom(ByVal state As Object)

    Private Sub ManagerEnd()
        ManagerEndCustom()
    End Sub

    MustOverride Sub ManagerEndCustom()

    Public Sub OnError(ByVal ManagedType As TManagedType, ByVal Status As Status, ByVal ConnectionCount As Integer, ByVal e As Exception)
        RaiseEvent [Error](ManagedType, Status, ConnectionCount, e)
    End Sub

    Public Sub OnStatusChange(ByRef ManagedType As TManagedType, ByVal Status As Status, ByVal ConnectionCount As Integer)
        If Not ManagedType Is Nothing Then
            ManagedType.Status = Status
        End If
        RaiseEvent StatusChange(ManagedType, Status, ConnectionCount)
    End Sub

#Region " IDisposable "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If
            ManagerEnd()
            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

End Class
