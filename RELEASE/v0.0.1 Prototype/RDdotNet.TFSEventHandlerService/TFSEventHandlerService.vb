Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config

Public Class TFSEventHandlerService

    Public WithEvents TeamServerManager As New TeamServerManager()
    Public WithEvents ServiceHostManager As New ServiceHostManager()
    Public WithEvents WorkItemChangedManager As New EventHandlersManager(Of WorkItemChangedEvent)(EventTypes.WorkItemChangedEvent, False)


    Protected Overrides Sub OnStart(ByVal args() As String)
        TeamServerManager.Initilise()
    End Sub

    Protected Overrides Sub OnStop()
        ServiceHostManager.Dispose()
        TeamServerManager.Dispose()
        WorkItemChangedManager.Dispose()
    End Sub

#Region " TeamServerManager "

    Private Delegate Sub TeamServerManager_StatusChangeDelegate(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub TeamServerManager_ErrorDelegate(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub TeamServerManager_InitiliseCompleteDelegate()

    Private Sub TeamServerManager_Error(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles TeamServerManager.Error
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.Name
        End If
        My.Application.Log.WriteException(e, TraceEventType.Warning, String.Format("TeamServerManager: There was an error with {0} in a stsus of {1}", name, Status.ToString))
    End Sub

    Private Sub TeamServerManager_StatusChange(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer) Handles TeamServerManager.StatusChange
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.Name
        End If
        My.Application.Log.WriteEntry(String.Format("TeamServerManager: Status of {0} is now {1}", name, Status.ToString), TraceEventType.Information)
        Select Case Status
            Case RDdotNet.TeamFoundation.Status.InitializationComplete
                ServiceHostManager.Initilise()
        End Select
    End Sub

#End Region

#Region " ServiceHostManager "

    Private Delegate Sub ServiceHostManager_StatusChangeDelegate(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub ServiceHostManager_ErrorDelegate(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub ServiceHostManager_InitiliseCompleteDelegate()

    Private Sub ServiceHostManager_Error(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles ServiceHostManager.Error
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.EventType.ToString
        End If
        My.Application.Log.WriteException(e, TraceEventType.Warning, String.Format("ServiceHostManager: There was an error with {0} in a stsus of {1}", name, Status.ToString))
    End Sub

    Private Sub ServiceHostManager_StatusChange(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer) Handles ServiceHostManager.StatusChange
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.EventType.ToString
        End If
        My.Application.Log.WriteEntry(String.Format("ServiceHostManager: Status of {0} is now {1}", name, Status.ToString), TraceEventType.Information)
        Select Case Status
            Case RDdotNet.TeamFoundation.Status.Closed
                TeamServerManager.UnregisterEvent(ManagedType.EventType)
            Case RDdotNet.TeamFoundation.Status.Connected
                TeamServerManager.RegisterEvent(ManagedType.EventType, ManagedType.BaseAddress)
            Case RDdotNet.TeamFoundation.Status.InitializationComplete
                WorkItemChangedManager.Initilise()
        End Select
    End Sub

    Private Delegate Sub ServiceHostManager_WorkItemChangedEventDelegate(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))

    Private Sub ServiceHostManager_WorkItemChangedEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Handles ServiceHostManager.WorkItemChangedEvent
        My.Application.Log.WriteEntry(String.Format("EventRecieved: a {0} event from {1} has been essigned the id {2}", ServiceHost.ItemElement.EventType.ToString, e.Identity.Url, e.EventID), TraceEventType.Information)
        e.Event.DisplayUrl = EndpointBase.ReformatServerURL(EventTypes.WorkItemChangedEvent, e.Event.DisplayUrl)
        WorkItemChangedManager.RunEventHandlers(ServiceHost, TeamServerManager.GetTeamServer(e.Identity), e)
    End Sub

#End Region

#Region " WorkItemChangedEventHandlerManager "

    Private Delegate Sub WorkItemChangedManager_StatusChangeDelegate(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent, AEventHandlerConfig), ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub WorkItemChangedManager_ErrorDelegate(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent, AEventHandlerConfig), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub WorkItemChangedManager_InitiliseCompleteDelegate()


    Private Sub WorkItemChangedManager_Error(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent, AEventHandlerConfig), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles WorkItemChangedManager.Error
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.AssemblyFileName.ToString
        End If
        My.Application.Log.WriteException(e, TraceEventType.Warning, String.Format("WorkItemChangedManager: There was an error with {0} in a status of {1}", name, Status.ToString))
    End Sub

    Private Sub WorkItemChangedManager_StatusChange(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent, AEventHandlerConfig), ByVal Status As Status, ByVal Items As Integer) Handles WorkItemChangedManager.StatusChange
        Dim name As String = "Server"
        If Not ManagedType Is Nothing Then
            name = ManagedType.ItemElement.AssemblyFileName.ToString
        End If
        My.Application.Log.WriteEntry(String.Format("WorkItemChangedManager: Status of {0} is now {1}", name, Status.ToString), TraceEventType.Information)
    End Sub

#End Region


End Class
