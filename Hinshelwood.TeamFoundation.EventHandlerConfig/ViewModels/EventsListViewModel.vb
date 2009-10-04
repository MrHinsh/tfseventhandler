Imports Hinshelwood.TeamFoundation.Config
Imports System.Collections.ObjectModel

Public Class EventsListViewModel
    Inherits SettingsViewModel

    Private m_EventHandlers As ObservableCollection(Of TeamServerViewModel)

    Public ReadOnly Property EventHandlers() As ObservableCollection(Of TeamServerViewModel)
        Get
            Return m_EventHandlers
        End Get
    End Property

    Public Sub New(ByVal EventHandlers As HandlerItemCollection)
        MyBase.New(New CollectionHeaderViewModel("Team Servers"))
        m_EventHandlers = New ObservableCollection(Of TeamServerViewModel)

        For Each eh In EventHandlers
            m_EventHandlers.Add(New TeamServerViewModel(eh))
        Next
    End Sub


End Class
