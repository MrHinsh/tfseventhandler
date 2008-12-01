Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Hinshelwood.TeamFoundation
Imports Hinshelwood.TeamFoundation.Config
Imports Microsoft.TeamFoundation.Client
Imports Microsoft.TeamFoundation
Imports Microsoft.TeamFoundation.Server

Public Class TeamServerItem
    Inherits AItem(Of TeamFoundationServer, TeamServerItemElement)

    Private _SubscriptionIDs As New Generic.Dictionary(Of EventTypes, Integer)
    Private m_GroupSecurityService As IGroupSecurityService


    Public Sub New(ByVal Subject As TeamFoundationServer, ByVal ItemElement As TeamServerItemElement)
        MyBase.New(Subject, ItemElement)
        m_GroupSecurityService = CType(Subject.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
    End Sub

    Public ReadOnly Property GroupSecurityService() As IGroupSecurityService
        Get
            Return m_GroupSecurityService
        End Get
    End Property


    Public Sub CollectSubscriptionID(ByVal EventType As EventTypes, ByVal SubscriptionID As Integer)
        If Not _SubscriptionIDs.ContainsKey(EventType) Then
            _SubscriptionIDs.Add(EventType, SubscriptionID)
        End If
    End Sub

    Public Function GetSubscription(ByVal EventType As EventTypes) As Integer
        If _SubscriptionIDs.ContainsKey(EventType) Then
            Return _SubscriptionIDs(EventType)
        End If
        Return 0
    End Function

    Public Sub RemoveSubscriptionID(ByVal EventType As EventTypes, ByVal SubscriptionID As Integer)
        If _SubscriptionIDs.ContainsKey(EventType) Then
            _SubscriptionIDs.Remove(EventType)
        End If
    End Sub

End Class
