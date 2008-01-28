Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports Microsoft.TeamFoundation.Server
Imports Microsoft.TeamFoundation
Imports RDdotNet.TeamFoundation.Events

Namespace Services.Widgets

    Public Class SubscriptionWidget
        Inherits ItemElementWidget(Of SubscriptionItem, String, Config.SubscriptionItemElement)

        Private m_TeamServerItem As TeamServerItem

        Public Sub New(ByVal TeamServerItem As TeamServerItem)
            MyBase.New(TeamServerItem)
            m_TeamServerItem = TeamServerItem
        End Sub

        Public ReadOnly Property ServerItemElement() As Config.ServerItemElement
            Get
                Return Config.TeamFoundationSettingsSection.Instance.Servers.Item(m_TeamServerItem.Name)
            End Get
        End Property

#Region " Public Overloads "

        Public Overrides Sub Add(ByVal Subscription As SubscriptionItem)
            Try
                Dim subID As Integer = TFSSubscribeToEvent(Subscription)
                Subscription = TFSGetSubscription(m_TeamServerItem, subID)
                InnerAdd(Subscription)
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to add subscription")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to add subscription", New FaultCode("TFS:EH:TS:0001")))
            End Try
        End Sub

        Public Overrides Sub Remove(ByVal Subscription As SubscriptionItem)
            Try
                InnerRemove(Subscription)
                Me.TFSDeleteSubscribe(Subscription)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to remove subscription")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to remove subscription", New FaultCode("TFS:EH:TS:0003")))
            End Try
        End Sub

        'Public Sub RemoveAll()
        '    Try
        '        For Each Subscription In Me.GetItemElements.ToList
        '            InnerRemove(Subscription)
        '            Me.TFSDeleteSubscribe(Subscription)
        '        Next
        '    Catch ex As System.ServiceModel.FaultException
        '        My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to remove subscription")
        '        OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to remove subscription", New FaultCode("TFS:EH:TS:0003")))
        '    End Try
        'End Sub

        Public Overrides Sub Refresh()
            Try
                InnerRefresh()
                TFSSyncSubscriptions(m_TeamServerItem)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to refresh subscriptions")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to refresh subscriptions", New FaultCode("TFS:EH:TS:0002")))
            End Try
        End Sub

        Public Overloads Overrides Sub Refresh(ByVal Item As DataContracts.SubscriptionItem)
            Try
                InnerRefresh(Item)
                TFSSyncSubscriptions(m_TeamServerItem)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to refresh subscriptions")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to refresh subscriptions", New FaultCode("TFS:EH:TS:0002")))
            End Try
        End Sub

        Public Overrides Function Find(ByVal SearchTerm As String) As DataContracts.SubscriptionItem
            Dim exTSI = (From tsi As SubscriptionItem In Items Where tsi.ID = CInt(SearchTerm)).SingleOrDefault
            Return exTSI
        End Function

        Public Overrides Function Exists(ByVal Subscription As DataContracts.SubscriptionItem) As Boolean
            Return Not Find(Subscription.ID.ToString) Is Nothing
        End Function

#End Region

#Region " Protected Overloads "

        Protected Overloads Overrides Function Convert(ByVal source As Config.SubscriptionItemElement) As DataContracts.SubscriptionItem
            Dim DeliverySchedule As DeliverySchedule = CType([Enum].Parse(GetType(DeliverySchedule), source.DeliveryPreference.Schedule), DeliverySchedule)
            Dim DeliveryType As DeliveryType = CType([Enum].Parse(GetType(DeliveryType), source.DeliveryPreference.Type), DeliveryType)
            Dim dpi As New DeliveryPreferenceItem(source.DeliveryPreference.Address, DeliverySchedule, DeliveryType)
            Dim EventType As EventTypes = CType([Enum].Parse(GetType(EventTypes), source.EventType), EventTypes)
            Return New SubscriptionItem(m_TeamServerItem, source.ID, EventType, source.ConditionString, source.Device, source.Subscriber, source.Tag, dpi)
        End Function

        Protected Overloads Overrides Function Convert(ByVal source As DataContracts.SubscriptionItem) As Config.SubscriptionItemElement
            Dim NewItem As SubscriptionItemElement = ServerItemElement.Subscriptions.CreateNew()
            NewItem.ID = source.ID
            NewItem.EventType = source.EventType.ToString
            NewItem.Device = source.Device
            NewItem.Subscriber = source.Subscriber
            NewItem.Tag = source.Tag

            NewItem.DeliveryPreference = New DeliveryPreferenceItemElement
            NewItem.DeliveryPreference.Address = source.DeliveryPreference.Address
            NewItem.DeliveryPreference.Schedule = source.DeliveryPreference.Schedule.ToString
            NewItem.DeliveryPreference.Type = source.DeliveryPreference.Type.ToString

            Return NewItem
        End Function

        Protected Overrides Function GetItemElements(Optional ByVal initilise As Object = Nothing) As Collection(Of SubscriptionItemElement)
            m_TeamServerItem = CType(initilise, TeamServerItem)
            TFSSyncSubscriptions(m_TeamServerItem)
            Dim SubscriptionItemElements As New Collection(Of Config.SubscriptionItemElement)
            For Each SubscriptionItemElement As SubscriptionItemElement In ServerItemElement.Subscriptions
                SubscriptionItemElements.Add(SubscriptionItemElement)
            Next
            Return SubscriptionItemElements
        End Function

        Protected Overrides Sub SetItemElements(ByVal ItemElements As Collection(Of Config.SubscriptionItemElement))
            ServerItemElement.Subscriptions.Clear()
            For Each ItemElement As Config.SubscriptionItemElement In ItemElements
                ServerItemElement.Subscriptions.Add(ItemElement)
            Next
            TeamFoundationSettingsSection.Save()
        End Sub

        Protected Overrides Function ItemCheck(ByVal Item As DataContracts.SubscriptionItem) As Boolean
            Try
                'Item.TeamFoundationServer.Authenticate()
                'Item.HasAuthenticated = True
                Return True
            Catch ex As Exception
                OnErrorOccured(ex)
                Return False
            End Try
        End Function

#End Region

        'Private Sub AddSubscripiotnFromTFS(ByVal ID As Integer)

        'End Sub

        Private Sub TFSSyncSubscriptions(ByVal tsi As TeamServerItem)
            ' Collect Eventing Bit
            Dim EventService As IEventService = CType(tsi.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
            ' Convert TFS Subscriptuions to RDdotNet Subscriptions
            For Each serverSub As Server.Subscription In EventService.EventSubscriptions(My.User.Name, "TFSEventHandler")
                Dim SubscriptionItem As New DataContracts.SubscriptionItem(m_TeamServerItem, serverSub)
                If Not Me.Exists(SubscriptionItem) Then
                    Me.InnerAdd(SubscriptionItem)
                End If
            Next
        End Sub

        Private Function TFSGetSubscription(ByVal tsi As TeamServerItem, ByVal ID As Integer) As SubscriptionItem
            ' Collect Eventing Bit
            Dim EventService As IEventService = CType(tsi.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
            ' Convert TFS Subscriptuions to RDdotNet Subscriptions
            For Each serverSub As Server.Subscription In EventService.EventSubscriptions(My.User.Name, "TFSEventHandler")
                Dim SubscriptionItem As New DataContracts.SubscriptionItem(m_TeamServerItem, serverSub)
                If ID = SubscriptionItem.ID Then
                    Return SubscriptionItem
                End If
            Next
            Return Nothing
        End Function

        Private Function TFSSubscribeToEvent(ByVal Subscription As SubscriptionItem) As Integer
            ' With Server add subscritpions...
            Dim EventService As IEventService = CType(m_TeamServerItem.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
            Dim delivery As DeliveryPreference = New DeliveryPreference()
            delivery.Type = DeliveryType.Soap
            delivery.Schedule = DeliverySchedule.Immediate
            delivery.Address = Subscription.DeliveryPreference.Address
            Dim subId As Integer = EventService.SubscribeEvent(My.User.Name, Subscription.EventType.ToString, "", delivery, "TFSEventHandler")
            ' Calback with an updated subscription list.
            Return subId
        End Function

        Private Sub TFSDeleteSubscribe(ByVal Subscription As SubscriptionItem)
            ' Collect Eventing Bit
            Dim EventService As IEventService = CType(m_TeamServerItem.TeamFoundationServer.GetService(GetType(IEventService)), IEventService)
            EventService.UnsubscribeEvent(Subscription.ID)
        End Sub

    End Class

End Namespace