Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports Hinshelwood.TeamFoundation
Imports Hinshelwood.TeamFoundation.Config

<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single, AddressFilterMode:=AddressFilterMode.Any)> _
Public Class NotificationService(Of TEvent As {New})
    Implements INotification
    Implements IDisposable

    Private _EventHandler As EventHandlerDelegate(Of TEvent)
    Private _EventType As EventTypes

    Public Sub New(ByVal EventType As EventTypes, ByVal EventHandler As EventHandlerDelegate(Of TEvent))
        _EventType = EventType
        _EventHandler = EventHandler
    End Sub

    Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As TeamFoundation.SubscriptionInfo) Implements TeamFoundation.INotification.Notify
        Dim IdentityObject As TFSIdentity = EndpointBase.CreateInstance(Of TFSIdentity)(tfsIdentityXml)
        Dim EventObject As TEvent = EndpointBase.CreateInstance(Of TEvent)(eventXml)
        Dim NotifyEventArgs As New NotifyEventArgs(Of TEvent)(_EventType, EventObject, IdentityObject, SubscriptionInfo)
        _EventHandler(NotifyEventArgs)
    End Sub

#Region " IDisposable "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

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
