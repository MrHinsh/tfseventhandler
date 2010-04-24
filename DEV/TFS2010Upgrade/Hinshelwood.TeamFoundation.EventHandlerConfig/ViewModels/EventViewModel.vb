Imports Hinshelwood.TeamFoundation.Config
Imports System.Collections.ObjectModel

Public Class EventViewModel
    Inherits SettingsViewModel

    Private m_Handlers As ObservableCollection(Of HandlerViewModel)

    Public ReadOnly Property Handlers() As ObservableCollection(Of HandlerViewModel)
        Get
            Return m_Handlers
        End Get
    End Property

    Public Sub New(ByVal EventItem As EventItemElement)
        MyBase.New(New CollectionHeaderViewModel(EventItem.EventType.ToString))
        m_Handlers = New ObservableCollection(Of HandlerViewModel)

        For Each eh In EventItem.HandlerItems
            m_Handlers.Add(New HandlerViewModel(eh))
        Next
    End Sub

    Friend Overrides Sub SavedReset()
        MyBase.SavedReset()
        For Each i In Handlers
            i.SavedReset()
        Next
    End Sub


End Class
