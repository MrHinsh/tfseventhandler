'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Config

'Public Class TeamServerAdminControl
'    Implements EventAdmin.ITeamServerAdminCallback


'    Private TeamServerAdminCallback As InstanceContext
'    Private TeamServerAdminClient As EventAdmin.TeamServerAdminClient

'    Public Sub Updated(ByVal TeamServers As System.ComponentModel.BindingList(Of String)) Implements EventAdmin.ITeamServerAdminCallback.Updated
'        BuildTeamServerList(TeamServers)
'    End Sub

'    Private Sub BuildTeamServerList(Optional ByVal TeamServers As System.ComponentModel.BindingList(Of String) = Nothing)
'        If TeamServers Is Nothing Then
'            TeamServers = TeamServerAdminClient.GetServers
'        End If
'        Me.uxListBoxTeamServers.DataSource = TeamServers
'    End Sub

'    Private Sub uxToolStripButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonConnect.Click
'        Try
'            '----------------------------
'            TeamServerAdminCallback = New InstanceContext(Me)

'            Dim binding As New System.ServiceModel.WSDualHttpBinding(WSDualHttpSecurityMode.Message)
'            binding.Name = "WSDualHttpBinding_ITeamServerAdmin"
'            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
'            binding.Security.Message.NegotiateServiceCredential = True
'            binding.ClientBaseAddress = New System.Uri("http://" & My.Computer.Name & ":4567/TeamServerAdminCallback")

'            Dim ep As New System.ServiceModel.EndpointAddress("http://eglavm060.emea.win.ml.com/MerrillLynchTeamServices/v1.0/EventAdmin.svc/TeamServerAdmin")

'            TeamServerAdminClient = New EventAdmin.TeamServerAdminClient(TeamServerAdminCallback, binding, ep)
'            '----------------------------
'            TeamServerAdminClient.Open()
'            '-------------------------
'            BuildTeamServerList()
'            '-------------------------
'            Me.uxToolStripButtonConnect.Enabled = False
'            Me.uxToolStripButtonDisconnect.Enabled = True
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'        End Try
'    End Sub

'    Private Sub uxToolStripButtonDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonDisconnect.Click
'        Try
'            TeamServerAdminClient.Close()
'            Me.uxToolStripButtonConnect.Enabled = True
'            Me.uxToolStripButtonDisconnect.Enabled = False
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'        End Try
'    End Sub


'End Class
