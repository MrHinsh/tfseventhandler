Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

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

        Private _Server As Uri = New Uri("http://localhost:6661/TFSEventHandler")

        Public Sub New(Optional ByVal Server As Uri = Nothing)
            If Not Server Is Nothing Then
                _Server = Server
            End If
        End Sub

#Region " IEvents "

        Public Event RecievedCheckInEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo)
        Public Event RecievedWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo)
        Public Event RecievedUnknownEvent(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo)

        Public Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseCheckinEvent
            RaiseEvent RecievedCheckInEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

        Public Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseUnknown
            RaiseEvent RecievedUnknownEvent(eventXml, tfsIdentityXml, SubscriptionInfo)
        End Sub

        Public Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As Services.DataContracts.SubscriptionInfo) Implements Services.Contracts.IEvents.RaiseWorkItemChangedEvent
            RaiseEvent RecievedWorkItemChangedEvent([Event], EventIdentity, SubscriptionInfo)
        End Sub

#End Region

#Region " IHandlers "

        Public Event HandlersUpdated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest)

        Public Sub AddAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) Implements Services.Contracts.IHandlers.AddAssembly

        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements Services.Contracts.IHandlers.AddAssemblyDirect

        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As Services.DataContracts.AssemblyItem Implements Services.Contracts.IHandlers.GetAssemblyItem

        End Function

        Public Function GetAssemblys() As Services.DataContracts.AssemblyManaifest Implements Services.Contracts.IHandlers.GetAssemblys

        End Function

        Public Sub RemoveAssembly(ByVal ID As Integer) Implements Services.Contracts.IHandlers.RemoveAssembly

        End Sub

        Public Function ValidateAssembly(ByVal AssemblyItem As Services.DataContracts.AssemblyItem) As Boolean Implements Services.Contracts.IHandlers.ValidateAssembly

        End Function

        Public Sub Updated(ByVal AssemblyManaifest As Services.DataContracts.AssemblyManaifest) Implements Services.Contracts.IHandlersCallback.Updated
            RaiseEvent HandlersUpdated(AssemblyManaifest)
        End Sub

#End Region

#Region " ISubscriptions  "

        Private _SubscriptionsProxy As Proxy.SubscriptionsClient

        Private ReadOnly Property SubscriptionsCallback() As InstanceContext
            Get
                Return New InstanceContext(Me)
            End Get
        End Property

        Private ReadOnly Property SubscriptionsClient() As Proxy.SubscriptionsClient
            Get
                If _SubscriptionsProxy Is Nothing Then
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/EventHandling/Subscriptions"))
                    _SubscriptionsProxy = New Proxy.SubscriptionsClient(SubscriptionsCallback, GetSecureWSDualHttpBinding, ep)
                End If
                Return _SubscriptionsProxy
            End Get
        End Property

        Public Event SubscriptionsUpdated(ByVal Subscriptions As Collection(Of DataContracts.Subscription))

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As Events.EventTypes) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            SubscriptionsClient.AddSubscriptions(ServiceUrl, EventType)
        End Sub

        Public Function GetSubscriptions() As System.Collections.ObjectModel.Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
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
                    Dim ep As New EndpointAddress(New Uri(_Server, "TFSEventHandler/EventHandling/TeamServers"))
                    _TeamServersClient = New Proxy.TeamServersClient(TeamServersCallback, GetSecureWSDualHttpBinding, ep)
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
            Return Nothing
        End Function

        Public Sub Updated(ByVal TeamServers() As String) Implements Services.Contracts.ITeamServersCallback.Updated
            RaiseEvent TeamServersUpdated(TeamServers)
        End Sub

#End Region

#Region " Bindings "

        Friend Shared Function GetSecureWSDualHttpBinding() As WSDualHttpBinding
            Dim Binding As New WSDualHttpBinding(WSDualHttpSecurityMode.Message)
            Binding.MaxReceivedMessageSize = 655360
            Binding.ReaderQuotas.MaxStringContentLength = 655360
            Binding.ReaderQuotas.MaxArrayLength = 655360
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Binding.Security.Message.NegotiateServiceCredential = True
            Return Binding
        End Function

        Friend Shared Function GetSecureWSHttpBinding() As WSHttpBinding
            Dim Binding As New WSHttpBinding(SecurityMode.Message, True)
            'Binding.MaxReceivedMessageSize = 655360
            'Binding.ReaderQuotas.MaxStringContentLength = 655360
            'Binding.ReaderQuotas.MaxArrayLength = 655360
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Binding.Security.Message.NegotiateServiceCredential = True
            Return Binding
        End Function

        Friend Shared Function GetSecureNetMsmqBinding() As NetMsmqBinding
            Dim Binding As New NetMsmqBinding(NetMsmqSecurityMode.Message)
            Binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows
            Return Binding
        End Function

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
                    If Not Me._SubscriptionsProxy Is Nothing Then
                        Me._SubscriptionsProxy.Close()
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