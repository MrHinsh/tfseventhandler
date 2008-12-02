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
    Private m_CommonStructureService As ICommonStructureService

    Public Sub New(ByVal Subject As TeamFoundationServer, ByVal ItemElement As TeamServerItemElement)
        MyBase.New(Subject, ItemElement)
    End Sub

    Public ReadOnly Property GroupSecurityService() As IGroupSecurityService
        Get
            If m_GroupSecurityService Is Nothing Then
                m_GroupSecurityService = CType(Subject.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
            End If
            Return m_GroupSecurityService
        End Get
    End Property

    Public ReadOnly Property CommonStructureService() As ICommonStructureService
        Get
            If m_CommonStructureService Is Nothing Then
                m_CommonStructureService = CType(Subject.GetService(GetType(ICommonStructureService)), ICommonStructureService)
            End If
            Return m_CommonStructureService
        End Get
    End Property

    Public Function GetIdentityFromDisplayName(ByVal displayName As String, ByVal e As WorkItemChangedEvent, Optional ByVal QueryMembership As QueryMembership = QueryMembership.None) As Identity
        ' Return App Group if you can
        Dim grpIdent As Identity = GetGroupIdentityFromDisplayName(displayName, e.PortfolioProject, QueryMembership)
        If Not grpIdent Is Nothing Then
            Return grpIdent
        End If
        ' Not app group. Then return user is you can
        Return GetUserIdentityFromDisplayName(displayName, QueryMembership)
    End Function

    Public Function GetUserIdentityFromDisplayName(ByVal displayName As String, Optional ByVal QueryMembership As QueryMembership = QueryMembership.None) As Identity
        Dim username As String = Hinshelwood.ActiveDirectory.Querys.GetUsername(displayName) ' TODO: Fix for username is nothing
        If String.IsNullOrEmpty(username) Then
            Return Nothing
        End If
        Return GroupSecurityService.ReadIdentity(SearchFactor.AccountName, username, QueryMembership)
    End Function

    Public Function GetGroupIdentityFromDisplayName(ByVal displayName As String, ByVal projectName As String, Optional ByVal QueryMembership As QueryMembership = QueryMembership.None) As Identity
        ' Return App Group if you can
        Dim pi As ProjectInfo = CommonStructureService.GetProjectFromName(projectName)
        Dim appGroup As Identity = (From i In GroupSecurityService.ListApplicationGroups(pi.Uri) Where i.DisplayName = displayName).SingleOrDefault
        If Not appGroup Is Nothing Then
            If QueryMembership = Server.QueryMembership.None Then
                Return appGroup
            Else
                Return GroupSecurityService.ReadIdentity(SearchFactor.Sid, appGroup.Sid, QueryMembership)
            End If
        End If
        Return Nothing
    End Function

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
