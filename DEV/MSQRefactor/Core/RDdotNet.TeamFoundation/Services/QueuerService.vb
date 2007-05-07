Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client
Imports microsoft.TeamFoundation.Server
Imports RDdotNet.TeamFoundation.Events

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
                Return Config.SettingsSection.Instance.Services.Item(Me.GetType.Name)
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

                Dim account As Net.NetworkCredential = New Net.NetworkCredential("xxhinshelmw_cp", "mjh260178", "snd")
                tfs = New TeamFoundationServer(TeamServerName, account)
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
            Try
                Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServerName)
                Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
                Return EventService.EventSubscriptions("EMEA\srvteamsetup", "EventAdminService")
            Catch ex As TeamFoundationServerUnauthorizedException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
                Throw New FaultException(Of TeamFoundationServerUnauthorizedException)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001"))
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                Throw New FaultException(Of System.Exception)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001"))
            End Try
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

        Public Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements Contracts.ITeamServers.AddServer
            Try
                If RegisteredServers.GetUriForServer(TeamServerName) = Nothing Then
                    RegisteredServers.AddServer(TeamServerName, TeamServerUri)
                End If
                TeamServerAdminCallback.Updated(GetServers)
                If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Team Server Connected:" & TeamServerName)
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
                Throw New FaultException(Of System.Exception)(ex, "Failed to add team server", New FaultCode("TFS:EH:TS:0001"))
            End Try
        End Sub

        Public Sub RemoveServer(ByVal TeamServerName As String) Implements Contracts.ITeamServers.RemoveServer
            Try
                RegisteredServers.RemoveServer(TeamServerName)
                TeamServerAdminCallback.Updated(GetServers)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "disconnection to TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Function GetServers() As String() Implements Contracts.ITeamServers.GetServers
            Return RegisteredServers.GetServerNames
        End Function

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
                For Each TeamServerName As String In Me.GetServers()
                    Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServerName)
                    Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
                    Dim delivery As DeliveryPreference = New DeliveryPreference()
                    delivery.Type = DeliveryType.Soap
                    delivery.Schedule = DeliverySchedule.Immediate
                    delivery.Address = ServiceUrl
                    Dim subId As Integer = EventService.SubscribeEvent("EMEA\srvteamsetup", EventType.ToString, "", delivery, "EventAdminService")
                    If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Event Subscribed:" & TeamServerName)
                    SubscriptionAdminCallback.Updated(GetSubscriptions)
                Next
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "AddSubscription to TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Contracts.ISubscriptions.RemoveSubscriptions
            Try
                For Each TeamServerName As String In Me.GetServers()
                    Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServerName)
                    Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
                    For Each SubScription As DataContracts.Subscription In GetServerSubscriptions(TeamServerName)
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
            For Each TeamServerName As String In Me.GetServers()
                Dim ServerSubs() As Server.Subscription = GetServerSubs(TeamServerName)
                For Each serverSub As Server.Subscription In ServerSubs
                    Subscriptions.Add(New DataContracts.Subscription(serverSub))
                Next
            Next
            Return Subscriptions
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

        Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.INotification.Notify
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