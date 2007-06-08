Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Servers

    Public Class TFSEventHandlerServer
        Implements Contracts.ISubscriptions
        Implements Contracts.ISubscriptionsCallback
        Implements Contracts.ITeamServers
        Implements Contracts.ITeamServersCallback
        Implements Contracts.IHandlers
        Implements Contracts.IHandlersCallback
        Implements Contracts.IEvents
        Implements IDisposable

        Private _Server As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")
        Private _ConnectedServices As New Collection

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
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/EventHandling/Handlers"))
                    _EventsClient = New Proxy.EventsClient(Services.ServiceFactory.GetSecureWSDualHttpBinding, ep)
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
                    _HandlersClient = New Proxy.HandlersClient(SubscriptionsCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding, ep)
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

        Public Function GetAssemblys() As Services.DataContracts.AssemblyManaifest Implements Services.Contracts.IHandlers.GetAssemblys
            Return HandlersClient.GetAssemblys()
        End Function

        Public Sub RemoveAssembly(ByVal ID As Integer) Implements Services.Contracts.IHandlers.RemoveAssembly
            HandlersClient.RemoveAssembly(ID)
        End Sub

        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly
            Return HandlersClient.ValidateAssembly(AssemblyItem)
        End Function

        Public Sub Updated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest) Implements Services.Contracts.IHandlersCallback.Updated
            RaiseEvent HandlersUpdated(AssemblyManaifest)
        End Sub

#End Region

#Region " ISubscriptions  "

        Private _SubscriptionsClient As Proxy.SubscriptionsClient

        Private ReadOnly Property SubscriptionsCallback() As InstanceContext
            Get
                Return New InstanceContext(Me)
            End Get
        End Property

        Private ReadOnly Property SubscriptionsClient() As Proxy.SubscriptionsClient
            Get
                If _SubscriptionsClient Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/Queuer/Subscriptions"))
                    _SubscriptionsClient = New Proxy.SubscriptionsClient(SubscriptionsCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding, ep)
                End If
                Return _SubscriptionsClient
            End Get
        End Property

        Public Event SubscriptionsUpdated(ByVal Subscriptions As Collection(Of DataContracts.Subscription))

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            SubscriptionsClient.AddSubscriptions(ServiceUrl, EventType)
        End Sub

        Public Function GetSubscriptions() As Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Return SubscriptionsClient.GetSubscriptions()
        End Function

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Contracts.ISubscriptions.RemoveSubscriptions
            SubscriptionsClient.RemoveSubscriptions(ServiceUrl)
        End Sub

        Public Sub Updated(ByVal Subscriptions As Collection(Of DataContracts.Subscription)) Implements Services.Contracts.ISubscriptionsCallback.Updated
            RaiseEvent SubscriptionsUpdated(Subscriptions)
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
                    _TeamServersClient = New Proxy.TeamServersClient(TeamServersCallback, Services.ServiceFactory.GetSecureWSDualHttpBinding, ep)
                End If
                Return _TeamServersClient
            End Get
        End Property

        Public Event TeamServersUpdated(ByVal TeamServers() As String)

        Public Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements Services.Contracts.ITeamServers.AddServer
            TeamServersClient.AddServer(TeamServerName, TeamServerUri)
        End Sub

        Public Function GetServers() As String() Implements Services.Contracts.ITeamServers.GetServers
            Return TeamServersClient.GetServers()
        End Function

        Public Sub RemoveServer(ByVal TeamServerName As String) Implements Services.Contracts.ITeamServers.RemoveServer
            TeamServersClient.RemoveServer(TeamServerName)
        End Sub

        Public Function ServceUrl() As System.Uri Implements Services.Contracts.ITeamServers.ServceUrl
            Return TeamServersClient.ServceUrl()
        End Function

        Public Sub Updated(ByVal TeamServers() As String) Implements Services.Contracts.ITeamServersCallback.Updated
            RaiseEvent TeamServersUpdated(TeamServers)
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