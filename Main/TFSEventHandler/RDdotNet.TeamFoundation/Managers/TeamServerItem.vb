'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
'Imports MerrillLynch.TeamFoundation
'Imports MerrillLynch.TeamFoundation.Config
'Imports Microsoft.TeamFoundation.Client
'Imports Microsoft.TeamFoundation

'Public Class TeamServerItem
'    Inherits AItem(Of TeamFoundationServer, TeamServerItemElement)

'    Private _SubscriptionIDs As New Generic.Dictionary(Of EventTypes, Integer)

'    Public Sub New(ByVal Subject As TeamFoundationServer, ByVal ItemElement As TeamServerItemElement)
'        MyBase.New(Subject, ItemElement)
'    End Sub

'    Public Sub CollectSubscriptionID(ByVal EventType As EventTypes, ByVal SubscriptionID As Integer)
'        If Not _SubscriptionIDs.ContainsKey(EventType) Then
'            _SubscriptionIDs.Add(EventType, SubscriptionID)
'        End If
'    End Sub

'    Public Function GetSubscription(ByVal EventType As EventTypes) As Integer
'        If _SubscriptionIDs.ContainsKey(EventType) Then
'            Return _SubscriptionIDs(EventType)
'        End If
'        Return 0
'    End Function

'    Public Sub RemoveSubscriptionID(ByVal EventType As EventTypes, ByVal SubscriptionID As Integer)
'        If _SubscriptionIDs.ContainsKey(EventType) Then
'            _SubscriptionIDs.Remove(EventType)
'        End If
'    End Sub

'End Class
