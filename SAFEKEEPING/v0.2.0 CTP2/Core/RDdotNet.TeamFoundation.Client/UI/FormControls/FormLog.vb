Imports RDdotNet.TeamFoundation.Clients
Imports RDdotNet.TeamFoundation.Services.DataContracts

Public Class FormLog

    Private Shared m_innerForm As FormLog
    Private m_EventHandler As TFSEventHandlerClient

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Shared Sub ShowLog(ByVal EventHandler As TFSEventHandlerClient)
        If m_innerForm Is Nothing Then
            m_innerForm = New FormLog
        End If
        m_innerForm.m_EventHandler = EventHandler
        m_innerForm.Show()
        AddHandler m_innerForm.m_EventHandler.TeamServerUpdated, AddressOf m_innerForm.TeamServerUpdated
        AddHandler m_innerForm.m_EventHandler.SubscriptionsStatusChange, AddressOf m_innerForm.SubscriptionsUpdated
        'AddHandler m_innerForm.m_EventHandler.HandlersUpdated, AddressOf m_innerForm.TeamServerUpdate
    End Sub


    Private Sub FormLog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If Not e.CloseReason = Windows.Forms.CloseReason.WindowsShutDown Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Delegate Sub del_TeamServerUpdated(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of TeamServerItem))
    Private Delegate Sub del_SubscriptionUpdated(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of SubscriptionItem))

    Private Sub TeamServerUpdated(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of TeamServerItem))
        If Me.InvokeRequired Then
            Me.Invoke(New del_TeamServerUpdated(AddressOf TeamServerUpdated), source, e)
        Else
            If e.Item Is Nothing Then
                Me.ListBox1.Items.Add(e.ChangeType.ToString)
            Else
                Me.ListBox1.Items.Add(String.Format("{0} {1}", e.ChangeType.ToString, e.Item.Name))
            End If
        End If
    End Sub

    Private Sub SubscriptionsUpdated(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of SubscriptionItem))
        If Me.InvokeRequired Then
            Me.Invoke(New del_SubscriptionUpdated(AddressOf SubscriptionsUpdated), source, e)
        Else
            If e.Item Is Nothing Then
                Me.ListBox2.Items.Add(e.ChangeType.ToString)
            Else
                Me.ListBox2.Items.Add(String.Format("{0} {1} {2}", e.ChangeType.ToString, e.Item.TeamServerItem.Name, e.Item.ID))
            End If
        End If
    End Sub


End Class