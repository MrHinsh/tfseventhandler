Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client
Imports microsoft.TeamFoundation.Server

Namespace Services

    <ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
    Public Class QueuerService
        Implements Contracts.ITeamServerAdmin
        Implements Contracts.ISubscriptionAdmin

        Implements IDisposable

        Public ReadOnly Property ServiceSettings() As Config.ServiceItemElement
            Get
                Return Config.SettingsSection.Instance.Services.Item(Me.GetType.Name)
            End Get
        End Property

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

                Dim account As Net.NetworkCredential = New Net.NetworkCredential("srvteamsetup", "1Nst4ll4t10n", "EMEA")
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
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                Throw ex
            End Try
        End Function

        Public Function GetServerSubscriptions(ByVal TeamServerName As String) As Collection(Of Subscription)
            Try
                Dim Subscriptions As New Collection(Of Subscription)
                Dim ServerSubs() As Server.Subscription = GetServerSubs(TeamServerName)
                For Each serverSub As Server.Subscription In ServerSubs
                    Subscriptions.Add(New Subscription(serverSub))
                Next
                Return Subscriptions
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetSubscriptions for TFS server unsucessfull")
                Throw ex
            End Try
        End Function


#End Region

#Region " ITeamServerAdmin "

        Private _TeamServerAdminCallback As Contracts.ITeamServerAdminCallback

        Public ReadOnly Property TeamServerAdminCallback() As Contracts.ITeamServerAdminCallback
            Get
                If _TeamServerAdminCallback Is Nothing Then
                    _TeamServerAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ITeamServerAdminCallback)()
                End If
                Return _TeamServerAdminCallback
            End Get
        End Property

        Public Function ServceUrl() As System.Uri Implements Contracts.ITeamServerAdmin.ServceUrl
            Return OperationContext.EndpointDispatcher.EndpointAddress.Uri
        End Function

        Public Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements Contracts.ITeamServerAdmin.AddServer
            Try
                If RegisteredServers.GetUriForServer(TeamServerName) = Nothing Then
                    RegisteredServers.AddServer(TeamServerName, TeamServerUri)
                End If
                TeamServerAdminCallback.Updated(GetServers)
                If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry("Team Server Connected:" & TeamServerName)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
            End Try
        End Sub

        Public Sub RemoveServer(ByVal TeamServerName As String) Implements Contracts.ITeamServerAdmin.RemoveServer
            Try
                RegisteredServers.RemoveServer(TeamServerName)
                TeamServerAdminCallback.Updated(GetServers)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "disconnection to TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Function GetServers() As String() Implements Contracts.ITeamServerAdmin.GetServers
            Return RegisteredServers.GetServerNames
        End Function

#End Region

#Region " ISubscriptionAdmin"

        Private _SubscriptionAdminCallback As Contracts.ISubscriptionAdminCallback

        Public ReadOnly Property SubscriptionAdminCallback() As Contracts.ISubscriptionAdminCallback
            Get
                If _SubscriptionAdminCallback Is Nothing Then
                    _SubscriptionAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ISubscriptionAdminCallback)()
                End If
                Return _SubscriptionAdminCallback
            End Get
        End Property

        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As EventTypes) Implements Contracts.ISubscriptionAdmin.AddSubscriptions
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

        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements Contracts.ISubscriptionAdmin.RemoveSubscriptions
            Try
                For Each TeamServerName As String In Me.GetServers()
                    Dim tfs As TeamFoundationServer = Me.GetTeamServer(TeamServerName)
                    Dim EventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
                    For Each SubScription As Subscription In GetServerSubscriptions(TeamServerName)
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

        Public Function GetSubscriptions() As System.Collections.ObjectModel.Collection(Of DataContracts.Subscription) Implements Contracts.ISubscriptionAdmin.GetSubscriptions
            Dim Subscriptions As New Collection(Of Subscription)
            For Each TeamServerName As String In Me.GetServers()
                Dim ServerSubs() As Server.Subscription = GetServerSubs(TeamServerName)
                For Each serverSub As Server.Subscription In ServerSubs
                    Subscriptions.Add(New Subscription(serverSub))
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

    End Class

End Namespace