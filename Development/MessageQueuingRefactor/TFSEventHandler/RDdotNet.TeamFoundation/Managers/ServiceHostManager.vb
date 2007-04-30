'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
'Imports MerrillLynch.TeamFoundation
'Imports MerrillLynch.TeamFoundation.Config

'Public Delegate Sub EventHandlerDelegate(Of TEvent)(ByVal e As NotifyEventArgs(Of TEvent))

'Public Class ServiceHostManager
'    Inherits AManager(Of ServiceHostItem)

'    Private _BaseAddress As String = Nothing
'    Public Event WorkItemChangedEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))
'    Public Event CheckinEvent(ByVal ServiceHost As ServiceHostItem, ByVal e As NotifyEventArgs(Of CheckinEvent))
'    Private _ServiceHosts As New Generic.Dictionary(Of EventTypes, ServiceHostItem)

'    Public ReadOnly Property ServiceHosts() As Generic.Dictionary(Of EventTypes, ServiceHostItem)
'        Get
'            Return _ServiceHosts
'        End Get
'    End Property

'    Public Sub New()
'        MyBase.New(False)
'    End Sub

'    Public Overrides Sub ManagerBeginCustom(ByVal state As Object)
'        For Each EventItem As Config.EventItemElement In Settings.EventItems
'            '-------------------
'            Dim ServiceItem As New ServiceHostItem(EventItem.EventType, Nothing, EventItem)
'            Me.OnStatusChange(ServiceItem, Status.Connecting, _ServiceHosts.Count)
'            '-------------------
'            Dim Success As Boolean = True
'            '-------------------
'            Try
'                ServiceItem.BaseAddress = GetBaseAddress(EventItem.EventType)
'            Catch ex As Exception
'                Me.OnError(ServiceItem, Status.Connecting, _ServiceHosts.Count, ex)
'            End Try
'            '-------------------
'            Try
'                Select Case EventItem.EventType
'                    Case EventTypes.WorkItemChangedEvent
'                        ServiceItem.Subject = GetServiceHost(Of WorkItemChangedEvent)(EventItem.EventType, ServiceItem.BaseAddress, AddressOf ServiceHost_EventRecieved)
'                    Case EventTypes.CheckinEvent
'                        ServiceItem.Subject = GetServiceHost(Of CheckinEvent)(EventItem.EventType, ServiceItem.BaseAddress, AddressOf ServiceHost_EventRecieved)
'                End Select
'            Catch ex As Exception
'                Success = False
'                Me.OnError(ServiceItem, Status.Connecting, _ServiceHosts.Count, ex)
'            End Try
'            '-------------------
'            If Not ServiceItem.Subject Is Nothing Then
'                Try
'                    ServiceItem.Subject.Open()
'                    _ServiceHosts.Add(EventItem.EventType, ServiceItem)
'                Catch ex As Exception
'                    Success = False
'                    Me.OnError(ServiceItem, Status.Connecting, _ServiceHosts.Count, ex)
'                End Try
'            End If
'            '-------------------
'            If Success Then
'                Me.OnStatusChange(ServiceItem, Status.Connected, _ServiceHosts.Count)
'            End If
'            '-------------------
'        Next
'    End Sub

'    Public Overrides Sub ManagerEndCustom()
'        For Each EventItem As Config.EventItemElement In Settings.EventItems
'            If _ServiceHosts.ContainsKey(EventItem.EventType) Then
'                Dim ServiceItem As ServiceHostItem = _ServiceHosts(EventItem.EventType)
'                Me.OnStatusChange(ServiceItem, Status.Closing, _ServiceHosts.Count)
'                ServiceItem.Subject.Close()
'                ServiceItem.Subject = Nothing
'                Me.OnStatusChange(ServiceItem, Status.Closed, _ServiceHosts.Count)
'            End If
'        Next
'    End Sub

'    Private Function GetServiceHost(Of TEvent As {New})(ByVal EventType As EventTypes, ByVal BaseAddress As System.Uri, ByVal EventHandler As EventHandlerDelegate(Of TEvent)) As System.ServiceModel.ServiceHost
'        'Dim NotificationService As New NotificationService(Of TEvent)(EventType, EventHandler)
'        'Dim ServiceHost As New System.ServiceModel.ServiceHost(NotificationService, BaseAddress)
'        'Dim smb As ServiceMetadataBehavior = ServiceHost.Description.Behaviors.Find(Of ServiceMetadataBehavior)()
'        'If Not smb Is Nothing Then
'        '    smb.HttpGetEnabled = True
'        'Else
'        '    smb = New ServiceMetadataBehavior()
'        '    smb.HttpGetEnabled = True
'        '    ServiceHost.Description.Behaviors.Add(smb)
'        'End If
'        'ServiceHost.AddServiceEndpoint(GetType(INotification), New BasicHttpBinding, "")
'        'ServiceHost.AddServiceEndpoint(GetType(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding, "/mex")
'        'Return ServiceHost
'    End Function

'    Private Function GetBaseAddress(ByVal EventType As EventTypes) As Uri
'        Dim BaseAddressString As String = Settings.BaseAddress.Url
'        If Not BaseAddressString.EndsWith("\") Then
'            BaseAddressString = BaseAddressString & "\"
'        End If
'        BaseAddressString = BaseAddressString & EventType.ToString
'        Return New System.Uri(BaseAddressString)
'    End Function

'    Public Sub ServiceHost_EventRecieved(ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))
'        RaiseEvent WorkItemChangedEvent(ServiceHosts(EventTypes.WorkItemChangedEvent), e)
'    End Sub

'    Public Sub ServiceHost_EventRecieved(ByVal e As NotifyEventArgs(Of CheckinEvent))
'        RaiseEvent CheckinEvent(ServiceHosts(EventTypes.CheckinEvent), e)
'    End Sub

'End Class