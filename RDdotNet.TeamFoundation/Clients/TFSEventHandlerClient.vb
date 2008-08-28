Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Clients

    Public Class TFSEventHandlerClient
        Implements Contracts.ISubscriptions
        Implements Contracts.ISubscriptionsCallback
        Implements Contracts.ITeamServers
        Implements Contracts.ITeamServersCallback
        Implements Contracts.IHandlers
        Implements Contracts.IHandlersCallback
        Implements Contracts.IEvents
        Implements IDisposable

        Private _Server As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")

        Public ReadOnly Property Server() As Uri
            Get
                Return _Server
            End Get
        End Property

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            If Not Server Is Nothing Then
                _Server = Server
            End If
        End Sub

        Public Sub Open()
            EventsClient.Open()
            HandlersClient.Open()
            SubscriptionsClient.Open()
            TeamServersClient.Open()
        End Sub



#Region " IEvents "

        Private _EventsClient As Proxy.EventsClient

        Private ReadOnly Property EventsClient() As Proxy.EventsClient
            Get
                If _EventsClient Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/EventHandling/Events"))
                    _EventsClient = New Proxy.EventsClient(Services.ServiceFactory.GetSecureWSHttpBinding, ep)
                End If
                Return _EventsClient
            End Get
        End Property

        Friend Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseCheckinEvent
            EventsClient.RaiseCheckinEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

        Friend Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseUnknown
            EventsClient.RaiseUnknown(eventXml, tfsIdentityXml, SubscriptionInfo)
        End Sub

        Friend Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseWorkItemChangedEvent
            EventsClient.RaiseWorkItemChangedEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

#End Region

#Region " IHandlers "

        Private _HandlersClient As Proxy.HandlersClient

        Private ReadOnly Property HandlersCallback() As InstanceContext
            Get
                Return New InstanceContext(Me)
            End Get
        End Property

        Private ReadOnly Property HandlersClient() As Proxy.HandlersClient
            Get
                If _HandlersClient Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/EventHandling/Handlers"))
                    _HandlersClient = New Proxy.HandlersClient(SubscriptionsCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding("Handlers"), ep)
                End If
                Return _HandlersClient
            End Get
        End Property

        Public Event HandlersUpdated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest)

        Public Sub AddAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.AddAssembly
            HandlersClient.AddAssembly(AssemblyItem)
        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements Services.Contracts.IHandlers.AddAssemblyDirect
            HandlersClient.AddAssemblyDirect(AssemblyBytes)
        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As Services.DataContracts.AssemblyItem Implements Services.Contracts.IHandlers.GetAssemblyItem
            Return HandlersClient.GetAssemblyItem(AssemblyBytes)
        End Function

        Public Function GetAssemblys() As Collection(Of AssemblyItem) Implements Services.Contracts.IHandlers.GetAssemblys
            Return HandlersClient.GetAssemblys()
        End Function

        Public Sub RemoveAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.RemoveAssembly
            HandlersClient.RemoveAssembly(AssemblyItem)
        End Sub

        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly
            Return HandlersClient.ValidateAssembly(AssemblyItem)
        End Function

        Public Sub HandlersErrorOccured(ByVal ex As System.Exception) Implements Services.Contracts.IHandlersCallback.ErrorOccured
            MsgBox(ex.ToString)
        End Sub

#End Region

#Region " ISubscriptions  "

        Private _SubscriptionsClient As Proxy.SubscriptionsClient

        Private ReadOnly Property SubscriptionsCallback() As InstanceContext
            Get
                Return New InstanceContext(Me)
            End Get
        End Property


        Public Function EventServiceUrl(ByVal EventType As Events.EventTypes) As System.Uri Implements Services.Contracts.ISubscriptions.EventServiceUrl
            Return SubscriptionsClient.EventServiceUrl(EventType)
        End Function

        Private ReadOnly Property SubscriptionsClient() As Proxy.SubscriptionsClient
            Get
                If _SubscriptionsClient Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/Queuer/Subscriptions"))
                    _SubscriptionsClient = New Proxy.SubscriptionsClient(SubscriptionsCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding("Subscriptions"), ep)
                End If
                Return _SubscriptionsClient
            End Get
        End Property

        Public Event SubscriptionsStatusChange(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of SubscriptionItem))


        Public Sub AddSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            SubscriptionsClient.AddSubscriptions(TeamServer, EventType)
        End Sub

        Public Sub RefreshServerSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ISubscriptions.RefreshServerSubscriptions
            SubscriptionsClient.RefreshServerSubscriptions(TeamServer)
        End Sub

        Public Sub RefreshSubscription(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal Subscription As Services.DataContracts.SubscriptionItem) Implements Services.Contracts.ISubscriptions.RefreshSubscription
            SubscriptionsClient.RefreshSubscription(TeamServer, Subscription)
        End Sub

        Public Sub RefreshSubscriptions() Implements Services.Contracts.ISubscriptions.RefreshSubscriptions
            SubscriptionsClient.RefreshSubscriptions()
        End Sub

        Public Sub RemoveSubscription(ByVal TeamServer As Services.DataContracts.TeamServerItem, ByVal Subscription As Services.DataContracts.SubscriptionItem) Implements Services.Contracts.ISubscriptions.RemoveSubscription
            SubscriptionsClient.RemoveSubscription(TeamServer, Subscription)
        End Sub

        Public Sub RemoveSubscriptions(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            SubscriptionsClient.RemoveSubscriptions(TeamServer)
        End Sub

        Public Sub StatusChange(ByVal StatusChangeType As Services.DataContracts.StatusChangeTypeEnum, ByVal SubscriptionItem As Services.DataContracts.SubscriptionItem) Implements Services.Contracts.ISubscriptionsCallback.StatusChange
            RaiseEvent SubscriptionsStatusChange(Me, New StatusChangeEventArgs(Of SubscriptionItem)(StatusChangeType, SubscriptionItem))
        End Sub

        Public Sub SubscriptionErrorOccured(ByVal ex As System.Exception) Implements Services.Contracts.ISubscriptionsCallback.ErrorOccured
            MsgBox(ex.ToString)
        End Sub

#End Region

#Region " ITeamServers "

        Private _TeamServersClient As Proxy.TeamServersClient

        Private ReadOnly Property TeamServersCallback() As InstanceContext
            Get
                Return New InstanceContext(Me)
            End Get
        End Property

        Private ReadOnly Property TeamServersClient() As Proxy.TeamServersClient
            Get
                If _TeamServersClient Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/Queuer/TeamServers"))
                    _TeamServersClient = New Proxy.TeamServersClient(TeamServersCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding("TeamServers"), ep)
                End If
                Return _TeamServersClient
            End Get
        End Property

        Public Event TeamServerUpdated(ByVal source As TFSEventHandlerClient, ByVal e As StatusChangeEventArgs(Of TeamServerItem))

        Public Sub AddServer(ByVal TeamServer As TeamServerItem) Implements Services.Contracts.ITeamServers.AddServer
            TeamServersClient.AddServer(TeamServer)
        End Sub

        Public Sub RefreshServers() Implements Services.Contracts.ITeamServers.RefreshServers
            TeamServersClient.RefreshServers()
        End Sub

        Public Sub RefreshServer(ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ITeamServers.RefreshServer
            TeamServersClient.RefreshServer(TeamServer)
        End Sub

        Public Sub RemoveServer(ByVal TeamServer As TeamServerItem) Implements Services.Contracts.ITeamServers.RemoveServer
            TeamServersClient.RemoveServer(TeamServer)
        End Sub

        Public Function ServceUrl() As System.Uri Implements Services.Contracts.ITeamServers.ServceUrl
            Return TeamServersClient.ServceUrl
        End Function

        Public Sub StatusChange(ByVal StatusChangeType As Services.DataContracts.StatusChangeTypeEnum, ByVal TeamServer As Services.DataContracts.TeamServerItem) Implements Services.Contracts.ITeamServersCallback.StatusChange
            RaiseEvent TeamServerUpdated(Me, New StatusChangeEventArgs(Of TeamServerItem)(StatusChangeType, TeamServer))
        End Sub

        Public Sub ErrorOccured(ByVal ex As System.Exception) Implements Services.Contracts.ITeamServersCallback.ErrorOccured
            MsgBox(ex.ToString)
        End Sub


#End Region

#Region " IDisposable "

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    If Not _TeamServersClient Is Nothing Then
                        _TeamServersClient.Close()
                    End If
                    If Not Me._SubscriptionsClient Is Nothing Then
                        Me._SubscriptionsClient.Close()
                    End If
                    If Not HandlersClient Is Nothing Then
                        HandlersClient.Close()
                    End If
                    If Not EventsClient Is Nothing Then
                        EventsClient.Close()
                    End If
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




    End Class


End Namespace
