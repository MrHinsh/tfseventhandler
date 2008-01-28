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

        Private m_TeamServerWidget As Widgets.TeamServerWidget
        Private m_SubscriptionWidgets As New Dictionary(Of String, Widgets.SubscriptionWidget)

        Public Sub New()
            Try

           
                '-------------------
                m_TeamServerWidget = New Widgets.TeamServerWidget
                AddHandler m_TeamServerWidget.StatusChange, AddressOf OnTeamServerWidgetStatusChange
                AddHandler m_TeamServerWidget.ErrorOccured, AddressOf OnTeamServerWidgetErrorOccured
                '-------------------
                For Each TSI As TeamServerItem In m_TeamServerWidget.Items
                    Dim SubscriptionWidget As New Widgets.SubscriptionWidget(TSI)
                    AddHandler SubscriptionWidget.StatusChange, AddressOf OnSubscriptionWidgetStatusChange
                    AddHandler SubscriptionWidget.ErrorOccured, AddressOf OnSubscriptionWidgetErrorOccured
                    ' Add to dictionary
                    m_SubscriptionWidgets.Add(TSI.Name, SubscriptionWidget)
                Next
                '-------------------
            Catch ex As Exception
                'My.Application.Log.WriteException(ex, TraceEventType.Critical, "Failed to create instance of Queuer Service")
                My.Application.Log.WriteEntry(ex.ToString, TraceEventType.Error, 2)
                If Not ex.InnerException Is Nothing Then
                    My.Application.Log.WriteException(ex.InnerException, TraceEventType.Critical, "Failed to create instance of Queuer Service")
                    My.Application.Log.WriteEntry(ex.InnerException.ToString, TraceEventType.Error, 2)
                End If
            End Try
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

#Region " CallBack "

        Private m_TeamServerAdminCallback As Contracts.ITeamServersCallback

        Public ReadOnly Property TeamServerAdminCallback() As Contracts.ITeamServersCallback
            Get
                If m_TeamServerAdminCallback Is Nothing Then
                    m_TeamServerAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ITeamServersCallback)()
                End If
                Return m_TeamServerAdminCallback
            End Get
        End Property

#End Region

#Region " Events  "

        Private Sub OnTeamServerWidgetStatusChange(ByVal Status As StatusChangeTypeEnum, ByVal TeamServerItem As TeamServerItem)
            Try
                ' Manage Subscription widgerts for each server
                Select Case Status
                    Case StatusChangeTypeEnum.Item_Added
                        Dim SubscriptionWidget As New Widgets.SubscriptionWidget(TeamServerItem)
                        'AddHandler m_SubscriptionWidget.StatusChange, AddressOf OnTeamServerWidgetStatusChange
                        'AddHandler m_SubscriptionWidget.ErrorOccured, AddressOf OnTeamServerWidgetErrorOccured
                        ' Add to dictionary
                        m_SubscriptionWidgets.Add(TeamServerItem.Name, SubscriptionWidget)
                    Case StatusChangeTypeEnum.Item_Removed
                        m_SubscriptionWidgets.Remove(TeamServerItem.Name)
                End Select
                ' Finally send status change to clients
                TeamServerAdminCallback.StatusChange(Status, TeamServerItem)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: OnTeamServerWidgetStatusChange")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: OnTeamServerWidgetStatusChange"))
            End Try
        End Sub

        Private Sub OnTeamServerWidgetErrorOccured(ByVal ex As Exception)
            Try
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: OnTeamServerWidgetErrorOccured")
                TeamServerAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex))
            Catch ex2 As Exception
                My.Application.Log.WriteException(ex2, TraceEventType.Error, "Queuer Service: OnTeamServerWidgetErrorOccured")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex2, "Queuer Service: OnTeamServerWidgetErrorOccured"))
            End Try
        End Sub

#End Region

        Public Function ServceUrl() As System.Uri Implements Contracts.ITeamServers.ServceUrl
            Return OperationContext.EndpointDispatcher.EndpointAddress.Uri
        End Function

        Public Sub AddServer(ByVal TeamServer As TeamServerItem) Implements Contracts.ITeamServers.AddServer
            m_TeamServerWidget.Add(TeamServer)
        End Sub

        Public Sub RemoveServer(ByVal TeamServer As TeamServerItem) Implements Contracts.ITeamServers.RemoveServer
            m_TeamServerWidget.Remove(TeamServer)
        End Sub

        Public Sub RefreshServers() Implements Contracts.ITeamServers.RefreshServers
            m_TeamServerWidget.Refresh()
        End Sub

        Public Sub RefreshServer(ByVal TeamServer As DataContracts.TeamServerItem) Implements Contracts.ITeamServers.RefreshServer
            m_TeamServerWidget.Refresh(TeamServer)
        End Sub

#End Region

#Region " ISubscription "

#Region " Callback "

        Private m_SubscriptionAdminCallback As Contracts.ISubscriptionsCallback

        Public ReadOnly Property SubscriptionAdminCallback() As Contracts.ISubscriptionsCallback
            Get
                If m_SubscriptionAdminCallback Is Nothing Then
                    m_SubscriptionAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.ISubscriptionsCallback)()
                End If
                Return m_SubscriptionAdminCallback
            End Get
        End Property

#End Region

#Region " Events  "

        Private Sub OnSubscriptionWidgetStatusChange(ByVal Status As StatusChangeTypeEnum, ByVal SubscriptionItem As SubscriptionItem)
            Try
                ' Manage Subscription widgerts for each server
                ' Finally send status change to clients
                SubscriptionAdminCallback.StatusChange(Status, SubscriptionItem)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: OnSubscriptionWidgetStatusChange")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: OnSubscriptionWidgetStatusChange"))
            End Try
          
        End Sub

        Private Sub OnSubscriptionWidgetErrorOccured(ByVal ex As Exception)
            Try
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: OnSubscriptionWidgetErrorOccured")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex))
            Catch ex2 As Exception
                My.Application.Log.WriteException(ex2, TraceEventType.Error, "Queuer Service: OnSubscriptionWidgetErrorOccured")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex2, "Queuer Service: OnSubscriptionWidgetErrorOccured"))
            End Try
        End Sub

#End Region

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

        Public Sub AddSubscriptions(ByVal TeamServer As TeamServerItem, ByVal EventType As EventTypes) Implements Contracts.ISubscriptions.AddSubscriptions
            Try
                ' Find Server
                Dim si = m_SubscriptionWidgets(TeamServer.Name)
                Dim ds As New DeliveryPreferenceItem(EventServiceUrl(EventType).ToString, DeliverySchedule.Immediate, DeliveryType.Soap)
                Dim subsct As New SubscriptionItem(TeamServer, 0, EventType, "", "", "", "TFSEventHandler", ds)
                si.Add(subsct)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: AddSubscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: AddSubscriptions"))
            End Try
        End Sub

        Public Sub RefreshSubscription(ByVal TeamServer As DataContracts.TeamServerItem, ByVal Subscription As SubscriptionItem) Implements Contracts.ISubscriptions.RefreshSubscription
            Try
                ' Find Server
                Dim si = m_SubscriptionWidgets(TeamServer.Name)
                si.Refresh(Subscription)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: RefreshServerSubscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: RefreshServerSubscriptions"))
            End Try
        End Sub

        Public Sub RefreshServerSubscriptions(ByVal TeamServer As DataContracts.TeamServerItem) Implements Contracts.ISubscriptions.RefreshServerSubscriptions
            Try
                ' Find Server
                Dim si = m_SubscriptionWidgets(TeamServer.Name)
                si.Refresh()
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: RefreshServerSubscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: RefreshServerSubscriptions"))
            End Try

        End Sub

        Public Sub RefreshSubscriptions() Implements Contracts.ISubscriptions.RefreshSubscriptions
            Try
                For Each subscriptionwidget In m_SubscriptionWidgets.ToList
                    subscriptionwidget.Value.Refresh()
                Next
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: RefreshSubscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: RefreshSubscriptions"))
            End Try
        End Sub


        Public Sub RemoveSubscriptions(ByVal TeamServer As DataContracts.TeamServerItem) Implements Contracts.ISubscriptions.RemoveSubscriptions
            Try
                SubscriptionAdminCallback.ErrorOccured(New FaultException("Remove All not implemented"))
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: RemoveSubscriptions")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: RemoveSubscriptions"))
            End Try
        End Sub

        Public Sub RemoveSubscription(ByVal TeamServer As TeamServerItem, ByVal Subscription As DataContracts.SubscriptionItem) Implements Contracts.ISubscriptions.RemoveSubscription
            Try
                ' Find Server
                Dim si = m_SubscriptionWidgets(TeamServer.Name)
                si.Remove(Subscription)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: RemoveSubscription")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: RemoveSubscription"))
            End Try
        End Sub

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
            Try
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
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Queuer Service: Notify")
                SubscriptionAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex, "Queuer Service: Notify"))
            End Try
           
        End Sub

#End Region


    End Class

End Namespace
