'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Config

'Public Class SubscriptionAdminControl
'    Implements EventAdmin.ISubscriptionAdminCallback


'    Private SubscriptionAdminCallback As InstanceContext
'    Private SubscriptionAdminClient As EventAdmin.SubscriptionAdminClient

'    Public Sub Updated(ByVal Subscriptions As System.ComponentModel.BindingList(Of EventAdmin.Subscription)) Implements EventAdmin.ISubscriptionAdminCallback.Updated
'        BuildSubscriptionList(Subscriptions)
'    End Sub

'    Private Sub BuildSubscriptionList(Optional ByVal Subscriptions As System.ComponentModel.BindingList(Of EventAdmin.Subscription) = Nothing)
'        If Subscriptions Is Nothing Then
'            Subscriptions = SubscriptionAdminClient.GetSubscriptions
'        End If
'        Me.uxBindingSourceSubscriptions.DataSource = Subscriptions
'    End Sub

'    Private Sub uxToolStripButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonConnect.Click
'        Try
'            '----------------------------
'            SubscriptionAdminCallback = New InstanceContext(Me)

'            Dim binding As New System.ServiceModel.WSDualHttpBinding(WSDualHttpSecurityMode.Message)
'            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
'            binding.Security.Message.NegotiateServiceCredential = True
'            binding.ClientBaseAddress = New System.Uri("http://" & My.Computer.Name & ":4567/SubscriptionAdminCallback")
'            Dim ep As New System.ServiceModel.EndpointAddress("http://eglavm060.emea.win.ml.com/MerrillLynchTeamServices/v1.0/EventAdmin.svc/SubscriptionAdmin")
'            SubscriptionAdminClient = New EventAdmin.SubscriptionAdminClient(SubscriptionAdminCallback, binding, ep)
'            '----------------------------
'            SubscriptionAdminClient.Open()
'            '-------------------------
'            BuildSubscriptionList()
'            '-------------------------
'            Me.uxToolStripButtonConnect.Enabled = False
'            Me.uxToolStripButtonDisconnect.Enabled = True
'            Me.uxToolStripButtonAdd.Enabled = True
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'        End Try
'    End Sub

'    Private Sub uxToolStripButtonDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonDisconnect.Click
'        Try
'            SubscriptionAdminClient.Close()
'            Me.uxToolStripButtonConnect.Enabled = True
'            Me.uxToolStripButtonDisconnect.Enabled = False
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'        End Try
'    End Sub


'    Private Sub uxToolStripButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonAdd.Click
'        Dim x As New SubscriptionAddForm()
'        Dim Result As Windows.Forms.DialogResult = x.ShowDialog(Me)
'        Select Case Result
'            Case Windows.Forms.DialogResult.Cancel
'                'Do nothing, form canceled
'            Case Windows.Forms.DialogResult.OK
'                SubscriptionAdminClient.AddSubscriptions(x.ServiceUri.ToString, x.EventType)
'            Case Else
'                MsgBox("Result: " & Result.ToString)
'        End Select

'        '
'    End Sub

'End Class
