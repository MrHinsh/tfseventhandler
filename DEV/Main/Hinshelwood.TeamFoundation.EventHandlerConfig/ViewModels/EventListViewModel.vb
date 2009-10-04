Imports Hinshelwood.TeamFoundation.Config
Imports System.Collections.ObjectModel

Public Class EventListViewModel
    Inherits SettingsViewModel

    Private m_Events As ObservableCollection(Of EventViewModel)

    Public ReadOnly Property Events() As ObservableCollection(Of EventViewModel)
        Get
            Return m_Events
        End Get
    End Property

    Public Sub New(ByVal EventHandlers As EventItemCollection)
        MyBase.New(New CollectionHeaderViewModel("Events"))
        m_Events = New ObservableCollection(Of EventViewModel)

        For Each e As EventItemElement In EventHandlers
            m_Events.Add(New EventViewModel(e))
        Next
    End Sub

    Friend Overrides Sub SavedReset()
        MyBase.SavedReset()
        For Each i In Events
            i.SavedReset()
        Next
    End Sub


End Class
