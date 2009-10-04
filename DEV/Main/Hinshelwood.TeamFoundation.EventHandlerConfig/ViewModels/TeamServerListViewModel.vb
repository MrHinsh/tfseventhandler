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
        MyBase.New(New CollectionHeaderViewModel("Team Servers", "Servers"))
        m_TeamServers = New ObservableCollection(Of TeamServerViewModel)

        For Each ts In TeamServerItems
            m_TeamServers.Add(New TeamServerViewModel(ts))
        Next
    End Sub


End Class
