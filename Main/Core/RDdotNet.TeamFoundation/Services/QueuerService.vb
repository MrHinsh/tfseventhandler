Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client
Imports microsoft.TeamFoundation.Server
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Configuration

Namespace Services

    <ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
    Public Class QueuerService
        Implements Contracts.ITeamServers
        Implements Contracts.ISubscriptions
        Implements Contracts.INotification
        Implements IDisposable

        Public Sub New()

        End Sub

        Public ReadOnly Property ServiceSettings() As Config.ServiceItemElement
            Get
                Return Config.TeamFoundationSettingsSection.Instance.Services.Item(Me.GetType.Name)
            End Get
        End Property

        'Public ReadOnly Property Adapters() As Adapters.DataContract
        '    Get
        '        Return New Adapters.DataContract
        '    End Get
        'End Property

        Public ReadOnly Property OperationContext() As OperationContext
            Get
                Return OperationContext.Current
            End Get
        End Property

#Region " Team Server Bits "

        Public Function GetTeamServer(ByVal TeamServerName As String) As TeamFoundationServer
            Dim tfs As TeamFoundationServer = Nothing
            Try
                Dim ui As ICredentialsProvider = New UICredentialsProvider

                'Dim account As Net.NetworkCredential = New Net.NetworkCredential("xxhinshelmw_cp", "xxxxx", "snd")
                tfs = New TeamFoundationServer(TeamServerName, ui)
            Catch ex As System.ServiceModel.FaultException
                Throw ex
            End Try
            If tfs Is Nothing Then
                Throw New System.ServiceModel.FaultException("Team Server not found")
            End If
            Return tfs
        End Function

        Public Function GetTeamServer(ByVal TeamServerUri As Uri) As Microsoft.TeamFoundation.Client.TeamFoundationServer
            Dim serverName As String = Nothing
            Try
                serverName = RegisteredServers.GetServerForUri(TeamServerUri)
            Catch ex As System.ServiceModel.FaultException
                Throw ex
            End Try
            If serverName Is Nothing Then
                Throw New System.ServiceModel.FaultException("Team Server not found")
            End If
            Return GetTeamServer(serverName)
        End Function

        Public Function GetServerSubs(ByVal TeamServerName As String) As Server.Subscription()
            Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServerName)
            Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
            Return EventService.EventSubscriptions("TFSEventHandler")
        End Function

        Public Function GetServerSubscriptions(ByVal TeamServerName As String) As Collection(Of DataContracts.Subscription)
            Try
                Dim Subscriptions As New Collection(Of DataContracts.Subscription)
                Dim ServerSubs() As Server.Subscription = GetServerSubs(TeamServerName)
                For Each serverSub As Server.Subscription In ServerSubs
                    Subscriptions.Add(New DataContracts.Subscription(serverSub))
                Next
                Return Subscriptions
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetSubscriptions for TFS server unsucessfull")
                Throw ex
            End Try
        End Function


#End Region

#Region " ITeamServers "

        Private _TeamServerAdminCallback As Contracts.ITeamServersCallback

        Public ReadOnly Property TeamServerAdminCallback() As Contracts.ITeamServersCallback
            Get
                If _TeamServerAdminCallback Is Nothing Then
                    _TeamServerAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ITeamServersCallback)()
                End If
                Return _TeamServerAdminCallback
            End Get
        End Property

        Public Function ServceUrl() As System.Uri Implements Contracts.ITeamServers.ServceUrl
            Return OperationContext.EndpointDispatcher.EndpointAddress.Uri
        End Function

        Public Sub AddServer(ByVal TeamServer As TeamServerItem) Implements Contracts.ITeamServers.AddServer
            Try
                Servers.Add(TeamServer)
                TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerAdded, TeamServer)
                RefreshServers()
                If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Team Server Connected:" & TeamServer.Name)
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
                TeamServerAdminCallback.ErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to add team server", New FaultCode("TFS:EH:TS:0001")))
            End Try
        End Sub

        Public Sub RemoveServer(ByVal TeamServer As TeamServerItem) Implements Contracts.ITeamServers.RemoveServer
            Try
                Servers.Remove(TeamServer)
                TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerRemoved, TeamServer)
                RefreshServers()
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "disconnection to TFS server unsucessfull")
                TeamServerAdminCallback.ErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to remove team server", New FaultCode("TFS:EH:TS:0001")))
            End Try
        End Sub

        Private m_TeamServers As Collection(Of TeamServerItem)

        Public ReadOnly Property Servers() As Collection(Of TeamServerItem)
            Get
                If m_TeamServers Is Nothing Then
                    m_TeamServers = Config.TeamFoundationSettingsSection.Instance.LoadServers
                End If
                Return m_TeamServers
            End Get
        End Property

        Public Sub RefreshServers() Implements Contracts.ITeamServers.RefreshServers
            TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerCheckStarted, Nothing)
            ' Check for removed servers
            For Each TSI In Servers
                Try
                    TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerCheck, TSI)
                    TSI.TeamFoundationServer.Authenticate()
                    TSI.HasAuthenticated = True
                    TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerAuthenticated, TSI)
                Catch ex As Exception
                    TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerAuthenticationFailed, TSI)
                End Try
            Next
            TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerCheckEnded, Nothing)
            '-----------
            TeamFoundationSettingsSection.Instance.SaveChanges(m_TeamServers)
        End Sub

#End Region

#Region " ISubscriptionAdmin"

        Private _SubscriptionAdminCallback As Contracts.ISubscriptionsCallback

        Public ReadOnly Property SubscriptionAdminCallback() As Contracts.ISubscriptionsCallback
            Get
                If _SubscriptionAdminCallback Is Nothing Then
                    _SubscriptionAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ISubscriptionsCallback)()
                End If
                Return _SubscriptionAdminCallback
            End Get
        End Property

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As EventTypes) Implements Contracts.ISubscriptions.AddSubscriptions
            Try
                For Each TeamServer As TeamServerItem In Servers
                    Dim EventService As IEventService = CType(TeamServer.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
                    Dim delivery As DeliveryPreference = New DeliveryPreference()
                    delivery.Type = DeliveryType.Soap
                    delivery.Schedule = DeliverySchedule.Immediate
                    delivery.Address = ServiceUrl
                    Dim subId As Integer = EventService.SubscribeEvent("TFSEventHandler", EventType.ToString, "", delivery, "EventAdminService")
                    'If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Event Subscribed:" & TeamServerName)
                    SubscriptionAdminCallback.Updated(GetSubscriptions)
                Next
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "AddSubscription to TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Contracts.ISubscriptions.RemoveSubscriptions
            Try
                For Each TeamServer As TeamServerItem In Servers
                    Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServer.Name)
                    Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
                    For Each SubScription As DataContracts.Subscription In GetServerSubscriptions(TeamServer.Name)
                        If SubScription.Address = ServiceUrl Then
                            EventService.UnsubscribeEvent(SubScription.ID)
                            SubscriptionAdminCallback.Updated(GetSubscriptions)
                        End If
                    Next
                Next
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "RemoveSubscription for TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Function GetSubscriptions() As System.Collections.ObjectModel.Collection(Of DataContracts.Subscription) Implements Contracts.ISubscriptions.GetSubscriptions
            Dim Subscriptions As New Collection(Of DataContracts.Subscription)
            Try

                For Each TeamServer As TeamServerItem In Servers
                    Dim ServerSubs() As Server.Subscription = GetServerSubs(TeamServer.Name)
                    For Each serverSub As Server.Subscription In ServerSubs
                        Subscriptions.Add(New DataContracts.Subscription(serverSub))
                    Next
                Next
                Return Subscriptions
            Catch ex As TeamFoundationServerUnauthorizedException
                Return Subscriptions
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
                Throw New FaultException("FaultDemoFaultSimple()", New FaultCode("FDFS Fault Code"), "FDFS Action")
                'Throw New FaultException(Of FaultContracts.TeamFoundationServerUnauthorizedException)(New FaultContracts.TeamFoundationServerUnauthorizedException(), "Unauthorized")
            Catch ex As System.Exception
                Return Subscriptions
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                Throw New FaultException(Of System.Exception)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001"))
            End Try
        End Function

#End Region

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

#Region " INotification "

        Private _EventHandlerClient As Clients.TFSEventHandlerClient

        Public ReadOnly Property EventHandlerClient() As Clients.TFSEventHandlerClient
            Get
                If _EventHandlerClient Is Nothing Then
                    _EventHandlerClient = New Clients.TFSEventHandlerClient()
                End If
                Return _EventHandlerClient
            End Get
        End Property

        Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Microsoft.TeamFoundation.Server.SubscriptionInfo) Implements Contracts.INotification.Notify
            Dim IdentityObject As TFSIdentity = EndpointBase.CreateInstance(Of TFSIdentity)(tfsIdentityXml)
            '---------------
            Dim UriString As String = OperationContext.EndpointDispatcher.EndpointAddress.Uri.AbsoluteUri
            Dim SlashIndex As Integer = UriString.LastIndexOf("/")
            Dim EndieBit As String = UriString.Substring(SlashIndex, (UriString.Length - (UriString.Length - SlashIndex)))
            Dim EventType As EventTypes = CType([Enum].Parse(GetType(EventTypes), EndieBit), EventTypes)
            '---------------
            Select Case EventType
                Case EventTypes.WorkItemChangedEvent
                    Dim EventObject As WorkItemChangedEvent = EndpointBase.CreateInstance(Of WorkItemChangedEvent)(eventXml)
                    EventHandlerClient.RaiseWorkItemChangedEvent(EventObject, IdentityObject, New DataContracts.SubscriptionInfo(SubscriptionInfo))
                Case EventTypes.CheckinEvent
                    Dim EventObject As CheckinEvent = EndpointBase.CreateInstance(Of CheckinEvent)(eventXml)
                    EventHandlerClient.RaiseCheckinEvent(EventObject, IdentityObject, New DataContracts.SubscriptionInfo(SubscriptionInfo))
                Case Else
                    EventHandlerClient.RaiseUnknown(eventXml, tfsIdentityXml, New DataContracts.SubscriptionInfo(SubscriptionInfo))
            End Select
            '---------------
        End Sub

#End Region

    End Class

End Namespace