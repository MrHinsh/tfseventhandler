Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports Microsoft.TeamFoundation
Imports Microsoft.TeamFoundation.Client

Namespace TeamFoundation.Clients

    Public Delegate Sub SubscriptionsUpdated(ByVal TeamServer As String, ByVal Subscriptions As Collection(Of Services.DataContracts.Subscription))

    Public Class SubscriptionsService
        'Inherits RDdotNet.Clients.WcfServiceBase(Of Proxies.SubscriptionsClient, WSDualHttpBinding)
        Implements RDdotNet.Clients.IClientService
        Implements Services.Contracts.ISubscriptions
        Implements Services.Contracts.ISubscriptionsCallback

        Public Event SubscriptionsUpdated As SubscriptionsUpdated

#Region " IClientService "

        Public Function Authenticated() As Boolean Implements RDdotNet.Clients.IClientService.Authenticated
            Return True
        End Function

        Public ReadOnly Property Contracts() As System.Collections.ObjectModel.Collection(Of System.Type) Implements RDdotNet.Clients.IClientService.Contracts
            Get
                Dim ServiceContracts As New Collection(Of Type)
                For Each t As Type In Me.GetType.GetInterfaces
                    If t.IsInterface And t.GetCustomAttributes(GetType(RDdotNetServiceContractAttribute), True).Length > 0 Then
                        ServiceContracts.Add(t)
                    End If
                Next
                Return ServiceContracts
            End Get
        End Property

        Public ReadOnly Property ServiceName() As String Implements RDdotNet.Clients.IClientService.ServiceName
            Get
                Return Me.GetType.Name
            End Get
        End Property

        Public ReadOnly Property ServiceType() As RDdotNet.Clients.ClientServiceTypes Implements RDdotNet.Clients.IClientService.ServiceType
            Get
                Return RDdotNet.Clients.ClientServiceTypes.Logic
            End Get
        End Property

        Public Sub Start() Implements RDdotNet.Clients.IClientService.Start

        End Sub

        Public Sub [Stop]() Implements RDdotNet.Clients.IClientService.Stop

        End Sub

#End Region

#Region " ISubscriptions "

        Public Sub AddSubscriptions(ByVal TeamServer As String, ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.AddSubscriptions
            Try
                For Each EventType As Events.EventTypes In [Enum].GetValues(GetType(Events.EventTypes))
                    If Not EventType = Events.EventTypes.Unknown Then
                        Dim NotifyUrl As String = ServiceUrl & "RDdotNet/TFSEventHandler/Queuer/Notification/" & EventType.ToString
                        Dim delivery As Microsoft.TeamFoundation.Server.DeliveryPreference = New Microsoft.TeamFoundation.Server.DeliveryPreference()
                        delivery.Type = Microsoft.TeamFoundation.Server.DeliveryType.Soap
                        delivery.Schedule = Microsoft.TeamFoundation.Server.DeliverySchedule.Immediate
                        delivery.Address = NotifyUrl
                        Dim subId As Integer = EventService(TeamServer).SubscribeEvent("TFSEventHandler", EventType.ToString, "", delivery, "EventAdminService")
                    End If
                Next
                Updated(TeamServer, GetSubscriptions(TeamServer))
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "AddSubscription to TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

        Public Function GetSubscriptions(ByVal TeamServer As String) As Collection(Of Services.DataContracts.Subscription) Implements Services.Contracts.ISubscriptions.GetSubscriptions
            Dim Subscriptions As New Collection(Of DataContracts.Subscription)
            Try
                Dim ServerSubs() As Microsoft.TeamFoundation.Server.Subscription = EventService(TeamServer).EventSubscriptions("TFSEventHandler", "EventAdminService")
                For Each serverSub As Microsoft.TeamFoundation.Server.Subscription In ServerSubs
                    Subscriptions.Add(New DataContracts.Subscription(serverSub))
                Next
            Catch ex As Microsoft.TeamFoundation.TeamFoundationServerUnauthorizedException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
            End Try
            Return Subscriptions
        End Function

        Public Sub RemoveSubscriptions(ByVal TeamServer As String, ByVal ServiceUrl As String) Implements Services.Contracts.ISubscriptions.RemoveSubscriptions
            Try
                For Each SubScription As DataContracts.Subscription In GetSubscriptions(TeamServer)
                    If SubScription.Address = ServiceUrl Then
                        EventService(TeamServer).UnsubscribeEvent(SubScription.ID)
                    End If
                Next
                Updated(TeamServer, GetSubscriptions(TeamServer))
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "RemoveSubscription for TFS server unsucessfull")
                Throw ex
            End Try
        End Sub

#End Region

#Region " ISubscriptionsCallback "


        Public Sub Updated(ByVal TeamServer As String, ByVal Subscriptions As Collection(Of Services.DataContracts.Subscription)) Implements Services.Contracts.ISubscriptionsCallback.Updated
            RaiseEvent SubscriptionsUpdated(TeamServer, Subscriptions)
        End Sub

#End Region

        Public ReadOnly Property TeamServer(ByVal TeamServerName As String) As Microsoft.TeamFoundation.Client.TeamFoundationServer
            Get
                Try
                    Dim tfs As Microsoft.TeamFoundation.Client.TeamFoundationServer = Nothing
                    tfs = New Microsoft.TeamFoundation.Client.TeamFoundationServer(TeamServerName)
                    tfs.Authenticate()
                    If tfs Is Nothing Then
                        Throw New System.ServiceModel.FaultException("Team Server not found")
                    End If
                    Return tfs
                Catch ex As Microsoft.TeamFoundation.TeamFoundationServerUnauthorizedException
                    My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
                Catch ex As System.Exception
                    My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                    Throw New FaultException(Of System.Exception)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001"))
                End Try
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property EventService(ByVal TeamServerName As String) As Microsoft.TeamFoundation.Server.IEventService
            Get
                Try
                    Dim tfs As Microsoft.TeamFoundation.Client.TeamFoundationServer = Me.TeamServer(TeamServerName)
                    Return CType(tfs.GetService(GetType(Microsoft.TeamFoundation.Server.IEventService)), Microsoft.TeamFoundation.Server.IEventService)
                Catch ex As Microsoft.TeamFoundation.TeamFoundationServerUnauthorizedException
                    My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to get subscriptions")
                Catch ex As System.Exception
                    My.Application.Log.WriteException(ex, TraceEventType.Error, "GetServerSubs for TFS server unsucessfull")
                    Throw New FaultException(Of System.Exception)(ex, "Failed to get subscriptions", New FaultCode("TFS:EH:S:0001"))
                End Try
            End Get
        End Property

    End Class

End Namespace
