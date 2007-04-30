'Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
'Imports MerrillLynch.TeamFoundation
'Imports MerrillLynch.TeamFoundation.Config
'Imports Microsoft.TeamFoundation.Client
'Imports Microsoft.TeamFoundation

'Public Class TeamServerManager
'    Inherits AManager(Of TeamServerItem)

'    Public Sub New()
'        MyBase.New(False)
'    End Sub

'    Private _TeamServers As New Collection(Of TeamServerItem)

'    Public ReadOnly Property TeamServers() As Collection(Of TeamServerItem)
'        Get
'            Return _TeamServers
'        End Get
'    End Property

'    Public Overrides Sub ManagerBeginCustom(ByVal state As Object)
'        ' Connect to all team servers
'        For Each TeamServerItem As TeamServerItemElement In Settings.TeamServerItems
'            Dim TeamServer As TeamServerItem = New TeamServerItem(Nothing, TeamServerItem)
'            Dim Status As Status = Status.Connecting
'            Try
'                Me.OnStatusChange(TeamServer, Status, TeamServers.Count)
'                TeamServer.Subject = GetTeamServer(TeamServerItem)
'                Status = Status.Connected
'                TeamServers.Add(TeamServer)
'                Me.OnStatusChange(TeamServer, Status, TeamServers.Count)
'            Catch ex As Exception
'                Me.OnError(TeamServer, Status, TeamServers.Count, ex)
'            End Try
'        Next
'    End Sub

'    Public Overrides Sub ManagerEndCustom()
'        '--------------
'        ' remove all events
'        For Each EventItem As Config.EventItemElement In Settings.EventItems
'            UnregisterEvent(EventItem.EventType)
'        Next
'        '--------------
'        ' kill all team server commentsions
'        For Each TeamServerItem As TeamServerItem In TeamServers
'            Me.OnStatusChange(TeamServerItem, Status.Closing, TeamServers.Count)
'            TeamServerItem.Subject.Dispose()
'            Me.OnStatusChange(TeamServerItem, Status.Closed, TeamServers.Count)
'        Next
'        '--------------
'        TeamServers.Clear()
'        '--------------
'    End Sub

'    Private Function GetTeamServer(ByVal TeamServerItem As Config.TeamServerItemElement) As TeamFoundationServer
'        Try
'            If RegisteredServers.GetUriForServer(TeamServerItem.Name) = Nothing Then
'                RegisteredServers.AddServer(TeamServerItem.Name, TeamServerItem.Url.ToString)
'            End If
'            GetTeamServer = TeamFoundationServerFactory.GetServer(TeamServerItem.Name)
'        Catch ex As Exception
'            Me.OnError(Nothing, Status.None, TeamServers.Count, ex)
'            My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
'            Return Nothing
'        End Try
'    End Function

'    Public Function GetTeamServer(ByVal EventIdentity As TFSIdentity) As TeamServerItem
'        For Each TeamServerItem As TeamServerItem In TeamServers
'            If EventIdentity.Url.ToLower.IndexOf(TeamServerItem.Subject.Name.ToLower) > -1 Then
'                Return TeamServerItem
'            End If
'        Next
'        Return Nothing
'    End Function

'    Public Sub RegisterEvent(ByVal EventType As EventTypes, ByVal BaseAddress As System.Uri)
'        Dim SubscriptionID As Integer = 0
'        For Each TeamServerItem As TeamServerItem In TeamServers
'            Try
'                Dim ID As Integer = 0
'                ID = SubscriptionServices.SubscribeEvent(TeamServerItem.Subject, TeamServerItem.ItemElement.Subscriber, BaseAddress.ToString, Server.DeliveryType.Soap, Server.DeliverySchedule.Immediate, EventType)
'                TeamServerItem.CollectSubscriptionID(EventType, ID)
'            Catch ex As Exception
'                Me.OnError(TeamServerItem, Status.External, TeamServers.Count, ex)
'            End Try
'        Next
'    End Sub

'    Public Sub UnregisterEvent(ByVal EventType As EventTypes)
'        Dim SubscriptionID As Integer = 0
'        For Each TeamServerItem As TeamServerItem In TeamServers
'            Dim ID As Integer = TeamServerItem.GetSubscription(EventType)
'            If Not ID = 0 Then
'                Try
'                    SubscriptionServices.UnSubscribeEvent(TeamServerItem.Subject, TeamServerItem.GetSubscription(EventType))
'                Catch ex As Exception
'                    Me.OnError(TeamServerItem, Status.External, TeamServers.Count, ex)
'                End Try
'            End If
'        Next
'    End Sub

'End Class
