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
                ' get existiong item
                Dim exTSI = (From tsi As TeamServerItem In Servers Where tsi.Uri.ToString = TeamServer.Uri.ToString).SingleOrDefault
                If exTSI Is Nothing Then
                    Servers.Add(TeamServer)
                    TeamFoundationSettingsSection.Instance.SaveChanges(m_TeamServers)
                    m_TeamServers = Nothing
                    TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerAdded, TeamServer)
                Else
                    TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerExists, exTSI)
                End If
                If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Team Server Connected:" & TeamServer.Name)
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
                TeamServerAdminCallback.ErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to add team server", New FaultCode("TFS:EH:TS:0001")))
            End Try
        End Sub

        Public Sub RemoveServer(ByVal TeamServer As TeamServerItem) Implements Contracts.ITeamServers.RemoveServer
            Try
                Servers.Remove(TeamServer)
                TeamFoundationSettingsSection.Instance.SaveChanges(m_TeamServers)
                m_TeamServers = Nothing
                TeamServerAdminCallback.StatusChange(StatusChangeTypeEnum.ServerRemoved, TeamServer)
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

        Public Function GetServers() As System.Collections.ObjectModel.Collection(Of DataContracts.TeamServerItem) Implements Contracts.ITeamServers.GetServers
            Return Servers
        End Function

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
        End Sub



#End Region

#Region " ISubscription "

        Private _SubscriptionAdminCallback As Contracts.ISubscriptionsCallback

        Public ReadOnly Property SubscriptionAdminCallback() As Contracts.ISubscriptionsCallback
            Get
                If _SubscriptionAdminCallback Is Nothing Then
                    _SubscriptionAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ISubscriptionsCallback)()
                End If
                Return _SubscriptionAdminCallback
            End Get
        End Property


        Public Function EventServiceUrl(ByVal EventType As EventTypes) As System.Uri Implements Contracts.ISubscriptions.EventServiceUrl
            Dim curernturi As Uri = OperationContext.EndpointDispatcher.EndpointAddress.Uri
            Dim Port As Integer = curernturi.Port
            Dim DnsAddress As String = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName
            Dim eventString As String = EventType.ToString
            If EventType = EventTypes.Unknown Then
                eventString = ""
            End If
            Dim urlmap As String = "http://{0}:{1}/TFSEventHandler/Queuer/Notification/{2}"
            Return New Uri(String.Format(urlmap, DnsAddress, Port, eventString))
        End Function

        Public Sub AddSubscriptions(ByVal TeamServerName As String, ByVal EventType As EventTypes) Implements Contracts.ISubscriptions.AddSubscriptions
            Try
                ' Find Server
                Dim tsi = (From TeamServerItem In Servers Where TeamServerItem.Name = TeamServerName).SingleOrDefault
                If tsi Is Nothing Then
                    SubscriptionAdminCallback.ErrorOccured(New Exception("Team Server does not exist."))
                    Exit Sub
                End If
                ' With Server add subscritpions...
                Dim EventService As IEventService = CType(tsi.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
                Dim delivery As DeliveryPreference = New DeliveryPreference()
                delivery.Type = DeliveryType.Soap
                delivery.Schedule = DeliverySchedule.Immediate
                delivery.Address = EventServiceUrl(EventType).ToString
                Dim subId As Integer = EventService.SubscribeEvent(My.User.Name, EventType.ToString, "", delivery, "TFSEventHandler")
                ' Calback with an updated subscription list.
                SubscriptionAdminCallback.Updated(TeamServerName, GetSubscriptions(TeamServerName))
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "AddSubscription to TFS server unsucessfull")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "AddSubscription Failed"))
            End Try
        End Sub

        Public Sub RemoveSubscriptions(ByVal TeamServerName As String) Implements Contracts.ISubscriptions.RemoveSubscriptions
            Try
                ' Find Server
                Dim tsi = (From TeamServerItem In Servers Where TeamServerItem.Name = TeamServerName).SingleOrDefault
                If tsi Is Nothing Then
                    SubscriptionAdminCallback.ErrorOccured(New Exception("Team Server does not exist."))
                    Exit Sub
                End If
                ' Collect Eventing Bit
                Dim EventService As IEventService = CType(tsi.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
                For Each SubScription As DataContracts.Subscription In GetSubscriptions(tsi.Name)
                    If SubScription.Address.Contains(EventServiceUrl(EventTypes.Unknown).ToString) Then
                        EventService.UnsubscribeEvent(SubScription.ID)
                        SubscriptionAdminCallback.Updated(tsi.Name, GetSubscriptions(tsi.Name))
                    End If
                Next
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "RemoveSubscription for TFS server unsucessfull")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "RemoveSubscription Failed"))
            End Try
        End Sub

        Public Function GetSubscriptions(ByVal TeamServerName As String) As System.Collections.ObjectModel.Collection(Of DataContracts.Subscription) Implements Contracts.ISubscriptions.GetSubscriptions
            Dim Subscriptions As New Collection(Of DataContracts.Subscription)
            Try
                Dim RDSubs As New Collection(Of DataContracts.Subscription)
                ' Find Server
                Dim tsi = (From TeamServerItem In Servers Where TeamServerItem.Name = TeamServerName).SingleOrDefault
                If tsi Is Nothing Then
                    SubscriptionAdminCallback.ErrorOccured(New Exception("Team Server does not exist."))
                Else
                    ' Collect Eventing Bit
                    Dim EventService As IEventService = CType(tsi.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
                    ' Convert TFS Subscriptuions to RDdotNet Subscriptions

                    For Each serverSub As Server.Subscription In EventService.EventSubscriptions(My.User.Name, "TFSEventHandler")
                        Subscriptions.Add(New DataContracts.Subscription(serverSub))
                    Next
                End If
            Catch ex As TeamFoundationServerUnauthorizedException
                Return Subscriptions
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException("FaultDemoFaultSimple()", New FaultCode("FDFS Fault Code"), "FDFS Action"))
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001")))
            End Try
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

        Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Microsoft.TeamFoundation.Server.SubscriptionInfo) Implements Contracts.INotification.Notify
            Dim IdentityObject As TFSIdentity = EndpointBase.CreateInstance(Of TFSIdentity)(tfsIdentityXml)
            '---------------
            Dim UriString As String = OperationContext.EndpointDispatcher.EndpointAddress.Uri.AbsoluteUri
            Dim SlashIndex As Integer = UriString.LastIndexOf("/") + 1
            Dim LengthOfEventText As Integer = UriString.Length - SlashIndex
            Dim EndieBit As String = UriString.Substring(SlashIndex, LengthOfEventText)
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
