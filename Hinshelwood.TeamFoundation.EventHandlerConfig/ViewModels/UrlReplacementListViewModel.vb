﻿Imports Hinshelwood.TeamFoundation.Config
Imports System.Collections.ObjectModel

Public Class UrlReplacementListViewModel
    Inherits SettingsViewModel

    Private m_UrlReplacements As ObservableCollection(Of UrlReplacementViewModel)

    Public ReadOnly Property UrlReplacements() As ObservableCollection(Of UrlReplacementViewModel)
        Get
            Return m_UrlReplacements
        End Get
    End Property

    Public Sub New(ByVal UrlReplacementItems As UrlReplacementItemCollection)
        MyBase.New(New CollectionHeaderViewModel("Url Replacements"))
        m_UrlReplacements = New ObservableCollection(Of UrlReplacementViewModel)

        For Each i In UrlReplacementItems
            m_UrlReplacements.Add(New UrlReplacementViewModel(i))
        Next
    End Sub

    Friend Overrides Sub SavedReset()
        MyBase.SavedReset()
        For Each i In UrlReplacements
            i.SavedReset()
        Next
    End Sub
   


End Class
