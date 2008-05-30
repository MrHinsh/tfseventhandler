'Imports System.ServiceModel
'Imports System.Runtime.Serialization
'Imports System.Collections.ObjectModel
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Config

'<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
'Public Class EventHandlerService
'    Implements IEventHandler
'    Implements IDisposable

'    Dim callback As IEventHandlerCallback

'    Public WithEvents TeamServerManager As New TeamServerManager()
'    Public WithEvents ServiceHostManager As New ServiceHostManager()
'    Public WithEvents WorkItemChangedManager As New EventHandlersManager(Of WorkItemChangedEvent)(EventTypes.WorkItemChangedEvent, False)

'    Public Sub New()
'        'Retrieve Callback system
'        'callback = OperationContext.Current.GetCallbackChannel(Of IEventHandlerCallback)()
'    End Sub

'    Public Sub InitiliseServices() Implements IEventHandler.InitiliseServices
'        InitiliseCallBack()
'        TeamServerManager.Initilise()
'    End Sub

'    Public Sub InitiliseCallBack()
'        If callback Is Nothing Then
'            callback = OperationContext.Current.GetCallbackChannel(Of IEventHandlerCallback)()
'        End If
'    End Sub


'#Region " TeamServerManager "

'    Private Sub TeamServerManager_Error(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles TeamServerManager.Error
'        callback.TeamServerManager_Error(ManagedType.ItemElement.Name, Status, Items, e)
'    End Sub

'    Private Sub TeamServerManager_StatusChange(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer) Handles TeamServerManager.StatusChange
'        callback.TeamServerManager_StatusChange(ManagedType.ItemElement.Name, Status, Items)
'        Select Case Status
'            Case TeamFoundation.Status.InitializationComplete
'                ServiceHostManager.Initilise()
'        End Select
'    End Sub

'#End Region

'#Region " ServiceHostManager "


'    Private Sub ServiceHostManager_Error(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles ServiceHostManager.Error
'        callback.ServiceHostManager_Error(ManagedType.ItemElement.EventType.ToString, Status, Items, e)
'    End Sub

'    Private Sub ServiceHostManager_StatusChange(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer) Handles ServiceHostManager.StatusChange
'        callback.ServiceHostManager_StatusChange(ManagedType.ItemElement.EventType.ToString, Status, Items)
'        Select Case Status
'            Case TeamFoundation.Status.Closed
'                'MsgBox("ServiceHostManager_ServiceHostClosed: " & ManagedType.EventType.ToString & ":" & ManagedType.BaseAddress.ToString)
'                TeamServerManager.UnregisterEvent(ManagedType.EventType)
'            Case TeamFoundation.Status.Connected
'                'MsgBox("ServiceHostManager_ServiceHostCreated: " & ManagedType.EventType.ToString & ":" & ManagedType.BaseAddress.ToString)
'                TeamServerManager.RegisterEvent(ManagedType.EventType, ManagedType.BaseAddress)
'            Case TeamFoundation.Status.InitializationComplete
'                WorkItemChangedManager.Initilise()
'        End Select
'    End Sub

'    Private Sub ServiceHostManager_WorkItemChangedEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Handles ServiceHostManager.WorkItemChangedEvent
'        callback.WorkItemChangedEvent(ServiceHost.ItemElement.EventType.ToString, e)
'        e.Event.DisplayUrl = EndpointBase.ReformatServerURL(EventTypes.WorkItemChangedEvent, e.Event.DisplayUrl)
'        WorkItemChangedManager.RunEventHandlers(ServiceHost, TeamServerManager.GetTeamServer(e.Identity), e)
'    End Sub

'#End Region

'#Region " WorkItemChangedEventHandlerManager "

'    Private Sub WorkItemChangedManager_Error(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles WorkItemChangedManager.Error
'        callback.WorkItemChangedManager_Error(ManagedType, Status, Items, e)
'    End Sub

'    Private Sub WorkItemChangedManager_StatusChange(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer) Handles WorkItemChangedManager.StatusChange
'        callback.WorkItemChangedManager_StatusChange(ManagedType, Status, Items)
'    End Sub

'#End Region

'#Region " IDisposable "

'    Private disposedValue As Boolean = False        ' To detect redundant calls

'    ' IDisposable
'    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'        If Not Me.disposedValue Then
'            If disposing Then
'                ' TODO: free managed resources when explicitly called
'                ServiceHostManager.Dispose()
'                TeamServerManager.Dispose()
'                WorkItemChangedManager.Dispose()
'            End If
'            ' TODO: free shared unmanaged resources
'        End If
'        Me.disposedValue = True
'    End Sub

'#Region " IDisposable Support "
'    ' This code added by Visual Basic to correctly implement the disposable pattern.
'    Public Sub Dispose() Implements IDisposable.Dispose
'        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
'        Dispose(True)
'        GC.SuppressFinalize(Me)
'    End Sub
'#End Region

'#End Region


'End Class
