Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config


Public Class Form1

    Public WithEvents TeamServerManager As New TeamServerManager()
    Public WithEvents ServiceHostManager As New ServiceHostManager()
    Public WithEvents EventHandlerManager_WorkItemChanged As New EventHandlersManager(Of WorkItemChangedEvent)(EventTypes.WorkItemChangedEvent, False)
    Public WithEvents EventHandlerManager_CheckinEvent As New EventHandlersManager(Of CheckinEvent)(EventTypes.CheckinEvent, False)

#Region " Form1 "

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ServiceHostManager.Dispose()
        TeamServerManager.Dispose()
        EventHandlerManager_WorkItemChanged.Dispose()
        EventHandlerManager_CheckinEvent.Dispose()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '----------------------------

        '----------------------------

    End Sub

    Private Sub ButtonStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonStart.Click
        TeamServerManager.Initilise()
        ButtonEnd.Enabled = True
        ButtonStart.Enabled = False
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        ServiceHostManager.Dispose()
        TeamServerManager.Dispose()
        EventHandlerManager_WorkItemChanged.Dispose()
        EventHandlerManager_CheckinEvent.Dispose()
        ButtonEnd.Enabled = False
        ButtonStart.Enabled = True
    End Sub

#End Region

#Region " TeamServerManager "

    Private Delegate Sub TeamServerManager_StatusChangeDelegate(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub TeamServerManager_ErrorDelegate(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub TeamServerManager_InitiliseCompleteDelegate()

    Private Sub TeamServerManager_Error(ByVal ManagedType As TeamServerItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles TeamServerManager.Error
        If Me.InvokeRequired Then
            Me.Invoke(New TeamServerManager_ErrorDelegate(AddressOf TeamServerManager_Error), ManagedType, Status, Items, e)
        Else
            Me.uxListBoxTeamServer.Items.Add("Error: " & e.ToString)
            MsgBox(e.ToString)
        End If
    End Sub

    Private Sub TeamServerManager_StatusChange(ByVal TeamServer As TeamServerItem, ByVal Status As Status, ByVal Items As Integer) Handles TeamServerManager.StatusChange
        If Me.InvokeRequired Then
            Me.Invoke(New TeamServerManager_StatusChangeDelegate(AddressOf TeamServerManager_StatusChange), TeamServer, Status, Items)
        Else
            Me.uxListBoxTeamServer.Items.Add("StatusChange: " & Status.ToString)
            Select Case Status
                Case TeamFoundation.Status.Connected
                    ' MsgBox("TeamServerManager_TeamServerCreated: " & TeamServer.Subject.Name)
                Case TeamFoundation.Status.InitializationComplete
                    ServiceHostManager.Initilise()
            End Select
        End If
    End Sub

#End Region

#Region " ServiceHostManager "

    Private Delegate Sub ServiceHostManager_StatusChangeDelegate(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub ServiceHostManager_ErrorDelegate(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub ServiceHostManager_InitiliseCompleteDelegate()

  

    Private Sub ServiceHostManager_Error(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles ServiceHostManager.Error
        If Me.InvokeRequired Then
            Me.Invoke(New ServiceHostManager_ErrorDelegate(AddressOf ServiceHostManager_Error), ManagedType, Status, Items, e)
        Else
            Me.uxListBoxServiceHost.Items.Add("Error: " & Status.ToString & " " & e.ToString)
            MsgBox(e.ToString)
        End If
    End Sub

    Private Sub ServiceHostManager_StatusChange(ByVal ManagedType As ServiceHostItem, ByVal Status As Status, ByVal Items As Integer) Handles ServiceHostManager.StatusChange
        If Me.InvokeRequired Then
            Me.Invoke(New ServiceHostManager_StatusChangeDelegate(AddressOf ServiceHostManager_StatusChange), ManagedType, Status, Items)
        Else
            Me.uxListBoxServiceHost.Items.Add("StatusChange: " & Status.ToString)
            Select Case Status
                Case TeamFoundation.Status.Closed
                    'MsgBox("ServiceHostManager_ServiceHostClosed: " & ManagedType.EventType.ToString & ":" & ManagedType.BaseAddress.ToString)
                    TeamServerManager.UnregisterEvent(ManagedType.EventType)
                Case TeamFoundation.Status.Connected
                    'MsgBox("ServiceHostManager_ServiceHostCreated: " & ManagedType.EventType.ToString & ":" & ManagedType.BaseAddress.ToString)
                    TeamServerManager.RegisterEvent(ManagedType.EventType, ManagedType.BaseAddress)
                Case TeamFoundation.Status.InitializationComplete
                    EventHandlerManager_WorkItemChanged.Initilise()
                    EventHandlerManager_CheckinEvent.Initilise()
            End Select
        End If
    End Sub

    Private Delegate Sub ServiceHostManager_WorkItemChangedEventDelegate(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))

    Private Sub ServiceHostManager_WorkItemChangedEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Handles ServiceHostManager.WorkItemChangedEvent
        If Me.InvokeRequired Then
            Me.Invoke(New ServiceHostManager_WorkItemChangedEventDelegate(AddressOf ServiceHostManager_WorkItemChangedEvent), ServiceHost, e)
        Else
            Me.uxListBoxServiceHost.Items.Add("Event Recieved: " & e.EventID.ToString)

            e.Event.DisplayUrl = EndpointBase.ReformatServerURL(EventTypes.WorkItemChangedEvent, e.Event.DisplayUrl)

            EventHandlerManager_WorkItemChanged.RunEventHandlers(ServiceHost, TeamServerManager.GetTeamServer(e.Identity), e)
        End If
    End Sub

    Private Delegate Sub ServiceHostManager_CheckinEventDelegate(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of CheckinEvent))

    Private Sub ServiceHostManager_CheckinEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of CheckinEvent)) Handles ServiceHostManager.CheckinEvent
        If Me.InvokeRequired Then
            Me.Invoke(New ServiceHostManager_CheckinEventDelegate(AddressOf ServiceHostManager_CheckinEvent), ServiceHost, e)
        Else
            Me.uxListBoxServiceHost.Items.Add("Event Recieved: " & e.EventID.ToString)

            ' e.Event.DisplayUrl = EndpointBase.ReformatServerURL(EventTypes.WorkItemChangedEvent, e.Event.DisplayUrl)

            EventHandlerManager_CheckinEvent.RunEventHandlers(ServiceHost, TeamServerManager.GetTeamServer(e.Identity), e)
        End If
    End Sub

#End Region

#Region " EventHandlerManager_WorkItemChanged "

    Private Delegate Sub WorkItemChangedManager_StatusChangeDelegate(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub WorkItemChangedManager_ErrorDelegate(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
    Private Delegate Sub WorkItemChangedManager_InitiliseCompleteDelegate()


    Private Sub WorkItemChangedManager_Error(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles EventHandlerManager_WorkItemChanged.Error
        If Me.InvokeRequired Then
            Me.Invoke(New WorkItemChangedManager_ErrorDelegate(AddressOf WorkItemChangedManager_Error), ManagedType, Status, Items, e)
        Else
            Me.uxListBoxWorkItemChanged.Items.Add("Error: " & e.ToString)
            MsgBox(e.ToString)
        End If
    End Sub

    Private Sub WorkItemChangedManager_StatusChange(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer) Handles EventHandlerManager_WorkItemChanged.StatusChange
        If Me.InvokeRequired Then
            Me.Invoke(New WorkItemChangedManager_StatusChangeDelegate(AddressOf WorkItemChangedManager_StatusChange), ManagedType, Status, Items)
        Else
            Me.uxListBoxWorkItemChanged.Items.Add("StatusChange: " & Status.ToString)
        End If
    End Sub

#End Region


#Region " EventHandlerManager_CheckinEvent "

    Private Delegate Sub EventHandlerManager_CheckinEvent_StatusChangeDelegate(ByVal ManagedType As EventHandlerItem(Of CheckinEvent), ByVal Status As Status, ByVal Items As Integer)
    Private Delegate Sub EventHandlerManager_CheckinEvent_ErrorDelegate(ByVal ManagedType As EventHandlerItem(Of CheckinEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)


    Private Sub EventHandlerManager_CheckinEvent_Error(ByVal ManagedType As EventHandlerItem(Of CheckinEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception) Handles EventHandlerManager_CheckinEvent.Error
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandlerManager_CheckinEvent_ErrorDelegate(AddressOf EventHandlerManager_CheckinEvent_Error), ManagedType, Status, Items, e)
        Else
            uxListBoxCheckIn.Items.Add("Error: " & e.ToString)
        End If
    End Sub

    Private Sub EventHandlerManager_CheckinEvent_StatusChange(ByVal ManagedType As EventHandlerItem(Of CheckinEvent), ByVal Status As Status, ByVal Items As Integer) Handles EventHandlerManager_CheckinEvent.StatusChange
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandlerManager_CheckinEvent_StatusChangeDelegate(AddressOf EventHandlerManager_CheckinEvent_StatusChange), ManagedType, Status, Items)
        Else
            uxListBoxCheckIn.Items.Add("StatusChange: " & Status.ToString)
        End If
    End Sub

#End Region

#Region " uxListBox "

    Private Sub uxListBox_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles uxListBoxTeamServer.MouseClick, uxListBoxServiceHost.MouseClick, uxListBoxCheckIn.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim x As ListBox = CType(sender, ListBox)
            Me.uxContextMenuStripDetails.Show(e.X, e.Y)
        End If
    End Sub

#End Region





 
End Class
