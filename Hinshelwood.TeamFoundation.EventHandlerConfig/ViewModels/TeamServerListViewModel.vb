Imports Hinshelwood.TeamFoundation.Config
Imports System.Collections.ObjectModel

Public Class TeamServerListViewModel
    Inherits SettingsViewModel

    Private m_TeamServers As ObservableCollection(Of TeamServerViewModel)

    Public ReadOnly Property TeamServers() As ObservableCollection(Of TeamServerViewModel)
        Get
            Return m_TeamServers
        End Get
    End Property

    Public Sub New(ByVal TeamServerItems As TeamServerItemCollection)
        MyBase.New(New CollectionHeaderViewModel("Team Servers"))
        m_TeamServers = New ObservableCollection(Of TeamServerViewModel)

        For Each ts In TeamServerItems
            m_TeamServers.Add(New TeamServerViewModel(ts))
        Next
    End Sub

    Friend Overrides Sub SavedReset()
        MyBase.SavedReset()
        For Each i In TeamServers
            i.SavedReset()
        Next
    End Sub


End Class
