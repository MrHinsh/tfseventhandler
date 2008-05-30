'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports RDdotNet.TeamFoundation
'Imports RDdotNet.TeamFoundation.Config

'Public Class SubscriptionAddForm


'    Friend Event TestResult(ByVal Status As StatusList)
'    Friend Event TestError(ByVal Status As StatusList, ByVal ex As Exception)
'    ' public event 

'    Private _EventType As EventAdmin.EventTypes = EventAdmin.EventTypes.Unknown
'    Private _ServiceUri As Uri = Nothing

'    Public ReadOnly Property EventType() As EventAdmin.EventTypes
'        Get
'            Return _EventType
'        End Get
'    End Property

'    Public ReadOnly Property ServiceUri() As Uri
'        Get
'            Return _ServiceUri
'        End Get
'    End Property

'    Private Sub uxButtonTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxButtonTest.Click
'        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf RunTest, Me.uxTextBoxSeriveUri.Text)
'    End Sub

'    Private Sub uxButtonAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxButtonAccept.Click
'        Me.DialogResult = Windows.Forms.DialogResult.OK
'        Me.Close()
'    End Sub

'#Region " Tests "

'    Public Sub RunTest(ByVal state As Object)
'        RaiseEvent TestResult(StatusList.TestBegin)
'        Dim ServiceUriText As String = CStr(state)
'        Dim ServiceUri As Uri = TestUri(ServiceUriText)
'        If ServiceUri Is Nothing Then
'            ' Failed
'        Else
'            Dim NotificationAdminClient As EventAdmin.NotificationAdminClient = TestServiceConnection(ServiceUri)
'            If NotificationAdminClient Is Nothing Then
'                ' Failed
'            Else
'                If NotificationAdminClient.State = CommunicationState.Faulted Then

'                Else
'                    _EventType = NotificationAdminClient.GetEventType()
'                    NotificationAdminClient.Close()

'                    Me._ServiceUri = New Uri(ServiceUri.ToString.Replace("/NotificationAdmin", ""))
'                End If
'            End If
'        End If
'        RaiseEvent TestResult(StatusList.TestEnd)
'    End Sub

'    Private Function TestServiceConnection(ByVal ServiceUri As Uri) As EventAdmin.NotificationAdminClient
'        RaiseEvent TestResult(StatusList.ServiceTest)
'        Dim NotificationAdminClient As EventAdmin.NotificationAdminClient = Nothing
'        Dim NotificationAdminCallbackContext As InstanceContext
'        Dim NotificationAdminCallback As New NotificationAdminCallback
'        '/NotificationAdmin
'        Try
'            NotificationAdminCallbackContext = New InstanceContext(NotificationAdminCallback)
'            Dim binding As New System.ServiceModel.WSDualHttpBinding(WSDualHttpSecurityMode.Message)
'            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
'            binding.Security.Message.NegotiateServiceCredential = True
'            binding.ClientBaseAddress = New System.Uri("http://" & My.Computer.Name & ":4567/NotificationAdminCallback")
'            Dim ep As New System.ServiceModel.EndpointAddress(ServiceUri)
'            NotificationAdminClient = New EventAdmin.NotificationAdminClient(NotificationAdminCallbackContext, binding, ep)
'            '----------------------------
'            NotificationAdminClient.Open()
'            '-------------------------
'        Catch ex As Exception
'            RaiseEvent TestError(StatusList.ServiceInvalid, ex)
'            Return Nothing
'        End Try
'        RaiseEvent TestResult(StatusList.ServicePass)
'        Return NotificationAdminClient
'    End Function

'    Private Function TestUri(ByVal ServiceUriText As String) As Uri
'        RaiseEvent TestResult(StatusList.UriTest)
'        Dim ServiceString As String = Me.uxTextBoxSeriveUri.Text
'        Dim ServiceUri As Uri = Nothing
'        If ServiceString Is String.Empty Then
'            RaiseEvent TestResult(StatusList.UriEmpty)
'        End If
'        If ServiceString.ToLower.EndsWith(".svc") Then
'            ServiceString = ServiceString & "/NotificationAdmin"
'            RaiseEvent TestResult(StatusList.UriModified)
'        End If
'        Try
'            ServiceUri = New Uri(ServiceString)
'        Catch ex As Exception
'            RaiseEvent TestError(StatusList.UriInvalid, ex)
'        End Try
'        RaiseEvent TestResult(StatusList.UriPass)
'        Return ServiceUri
'    End Function

'    Public Enum StatusList
'        TestBegin
'        UriTest
'        UriEmpty
'        UriModified
'        UriInvalid
'        UriPass
'        ServiceTest
'        ServiceInvalid
'        ServicePass
'        TestEnd
'    End Enum


'#End Region

'    Public Delegate Sub TestResultDelegate(ByVal Status As StatusList)
'    Public Delegate Sub TestErrorDelegate(ByVal Status As StatusList, ByVal ex As Exception)

'    Private Sub SubscriptionAddForm_TestError(ByVal Status As StatusList, ByVal ex As System.Exception) Handles Me.TestError
'        If Me.InvokeRequired Then
'            Me.Invoke(New TestErrorDelegate(AddressOf SubscriptionAddForm_TestError), Status, ex)
'        Else
'            Me.uxListBoxTestResults.Items.Add(Status.ToString)
'            MsgBox(Status.ToString & ": " & ex.ToString)
'        End If
'    End Sub

'    Private Sub SubscriptionAddForm_TestUpdate(ByVal Status As StatusList) Handles Me.TestResult
'        If Me.InvokeRequired Then
'            Me.Invoke(New TestResultDelegate(AddressOf SubscriptionAddForm_TestUpdate), Status)
'        Else
'            Select Case Status
'                Case StatusList.TestBegin
'                    Me.uxButtonCancel.Enabled = False
'                    Me.uxButtonTest.Enabled = False
'                    Me.uxButtonAccept.Enabled = False
'                Case StatusList.TestEnd
'                    Me.uxButtonCancel.Enabled = True
'                    Me.uxButtonTest.Enabled = True
'                    If Not EventType = EventAdmin.EventTypes.Unknown Then Me.uxButtonAccept.Enabled = True
'            End Select
'            Me.uxListBoxTestResults.Items.Add(Status.ToString)
'        End If
'    End Sub

'    Private Class NotificationAdminCallback
'        Implements EventAdmin.INotificationAdminCallback

'        Public Sub ForNoReason() Implements EventAdmin.INotificationAdminCallback.ForNoReason

'        End Sub

'    End Class



'End Class

