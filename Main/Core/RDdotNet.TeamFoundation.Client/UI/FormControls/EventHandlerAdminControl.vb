'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Config

'Public Class EventHandlerAdminControl
'    Implements Proxys.EventHandlerService.IHandlersCallback


'    Private EventHandlerAdminCallback As InstanceContext
'    Private EventHandlerAdminClient As Proxys.EventHandlerService.EventsClient

'    Private Sub BuildEventHandlerList(Optional ByVal AssemblyManaifest As Proxys.EventHandlerService.AssemblyManaifest = Nothing)
'        If AssemblyManaifest Is Nothing Then
'            Try
'                AssemblyManaifest = EventHandlerAdminClient.GetAssemblys
'            Catch ex As Exception
'                MsgBox(ex)
'            End Try
'        End If
'        Me.uxBindingSourceEventHandlers.DataSource = AssemblyManaifest.Assemblys
'    End Sub

'    Private Sub uxToolStripButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonConnect.Click
'        Try
'            '----------------------------
'            EventHandlerAdminCallback = New InstanceContext(Me)

'            Dim binding As New System.ServiceModel.WSDualHttpBinding(WSDualHttpSecurityMode.Message)
'            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
'            binding.Security.Message.NegotiateServiceCredential = True
'            binding.ClientBaseAddress = New System.Uri("http://" & My.Computer.Name & ":6666/EventHandlerAdminCallback")
'            binding.MaxReceivedMessageSize = 655360
'            binding.ReaderQuotas.MaxStringContentLength = 655360
'            binding.ReaderQuotas.MaxArrayLength = 655360
'            Dim ep As New System.ServiceModel.EndpointAddress("http://Localhost/TFSEveventHandler")
'            EventHandlerAdminClient = New Proxys.EventHandlerService.HandlersClient(EventHandlerAdminCallback, binding, ep)
'            '----------------------------
'            EventHandlerAdminClient.Open()
'            '-------------------------
'            BuildEventHandlerList()
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
'            EventHandlerAdminClient.Close()
'            Me.uxToolStripButtonConnect.Enabled = True
'            Me.uxToolStripButtonDisconnect.Enabled = False
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'        End Try
'    End Sub

'    Private Sub uxToolStripButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonAdd.Click
'        If Me.uxOpenFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
'            Dim FileStream As IO.FileStream
'            Try
'                ' Read file and return contents
'                FileStream = IO.File.Open(Me.uxOpenFileDialog.FileName, IO.FileMode.Open, IO.FileAccess.Read)
'                Dim abytBuffer(CInt(FileStream.Length - 1)) As Byte
'                FileStream.Read(abytBuffer, 0, CInt(FileStream.Length))
'                '-------------------
'                Dim AssemblyItem As Proxys.EventHandlerService.AssemblyItem = EventHandlerAdminClient.GetAssemblyItem(abytBuffer)
'                If EventHandlerAdminClient.ValidateAssembly(AssemblyItem) Then
'                    EventHandlerAdminClient.AddAssembly(AssemblyItem)
'                Else
'                    MsgBox("That assembly is not valid.")
'                End If
'                '-------------------
'            Catch exp As Exception
'                MsgBox(exp.ToString)
'            Finally
'                If Not FileStream Is Nothing Then
'                    FileStream.Close()
'                End If
'            End Try
'        End If
'    End Sub

'    Public Sub Updated(ByVal AssemblyManaifest As Proxys.EventHandlerService.AssemblyManaifest) Implements Proxys.EventHandlerService.IHandlersCallback.Updated
'        BuildEventHandlerList(AssemblyManaifest)
'    End Sub

'    Private Sub uxToolStripButtonCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonCheck.Click
'        Dim str As New System.Text.StringBuilder
'        str.AppendLine(String.Format("State: {0}", EventHandlerAdminClient.State.ToString))
'        str.AppendLine("--------------------------")
'        MsgBox(str.ToString)
'    End Sub

'End Class
