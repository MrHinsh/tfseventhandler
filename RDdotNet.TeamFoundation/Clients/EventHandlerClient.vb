'Imports System.ServiceModel
'Imports System.Runtime.Serialization
'Imports System.Collections.ObjectModel
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Services
'Imports RDdotNet.TeamFoundation.Config
'Imports RDdotNet.TeamFoundation.Events

'Namespace Clients


'    Public Class EventHandlerClient
'        Implements Contracts.IHandlers
'        Implements Contracts.IHandlersCallback
'        Implements Contracts.IEvents
'        Implements IDisposable

'#Region " IHandlers "

'        Private _HandlersClient As Proxys.EventHandlerServiceProxy.HandlersClient

'        Private ReadOnly Property HandlersClient() As Proxys.EventHandlerServiceProxy.HandlersClient
'            Get
'                If _HandlersClient Is Nothing Then
'                    _HandlersClient = New Proxys.EventHandlerServiceProxy.HandlersClient
'                    _HandlersClient.Open()
'                End If
'                Return _HandlersClient
'            End Get
'        End Property

'        Public Event HandlersUpdated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest)

'        Public Sub AddAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.AddAssembly
'            HandlersClient.AddAssembly(AssemblyItem)
'        End Sub

'        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements Services.Contracts.IHandlers.AddAssemblyDirect
'            HandlersClient.AddAssemblyDirect(AssemblyBytes)
'        End Sub

'        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As Services.DataContracts.AssemblyItem Implements Services.Contracts.IHandlers.GetAssemblyItem
'            Return HandlersClient.GetAssemblyItem(AssemblyBytes)
'        End Function

'        Public Function GetAssemblys() As Services.DataContracts.AssemblyManaifest Implements Services.Contracts.IHandlers.GetAssemblys
'            Return HandlersClient.GetAssemblys()
'        End Function

'        Public Sub RemoveAssembly(ByVal ID As Integer) Implements Services.Contracts.IHandlers.RemoveAssembly
'            HandlersClient.RemoveAssembly(ID)
'        End Sub

'        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly
'            Return HandlersClient.ValidateAssembly(AssemblyItem)
'        End Function

'        Private Sub Updated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest) Implements Services.Contracts.IHandlersCallback.Updated
'            RaiseEvent HandlersUpdated(AssemblyManaifest)
'        End Sub

'#End Region

'#Region " IEvents "

'        Private _EventsClient As Proxys.EventHandlerServiceProxy.EventsClient

'        Private ReadOnly Property EventsClient() As Proxys.EventHandlerServiceProxy.EventsClient
'            Get
'                If _EventsClient Is Nothing Then
'                    _EventsClient = New Proxys.EventHandlerServiceProxy.EventsClient
'                End If
'                Return _EventsClient
'            End Get
'        End Property

'        Public Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseCheckinEvent
'            EventsClient.RaiseCheckinEvent([Event], EventIdentity, SubscriptionInfo)
'        End Sub

'        Public Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseUnknown
'            EventsClient.RaiseUnknown(eventXml, tfsIdentityXml, SubscriptionInfo)
'        End Sub

'        Public Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseWorkItemChangedEvent
'            EventsClient.RaiseWorkItemChangedEvent([Event], EventIdentity, SubscriptionInfo)
'        End Sub

'#End Region

'#Region " IDisposable "

'        Private disposedValue As Boolean = False        ' To detect redundant calls

'        ' IDisposable
'        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'            If Not Me.disposedValue Then
'                If disposing Then
'                    ' TODO: free managed resources when explicitly called
'                    If Not _EventsClient Is Nothing Then
'                        _EventsClient.Close()
'                    End If
'                    If Not Me._HandlersClient Is Nothing Then
'                        Me._HandlersClient.Close()
'                    End If
'                End If
'                ' TODO: free shared unmanaged resources
'            End If
'            Me.disposedValue = True
'        End Sub

'#Region " IDisposable Support "
'        ' This code added by Visual Basic to correctly implement the disposable pattern.
'        Public Sub Dispose() Implements IDisposable.Dispose
'            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
'            Dispose(True)
'            GC.SuppressFinalize(Me)
'        End Sub
'#End Region

'#End Region

'    End Class


'End Namespace