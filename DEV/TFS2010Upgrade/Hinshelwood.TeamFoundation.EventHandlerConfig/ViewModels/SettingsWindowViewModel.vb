Imports System.Collections.ObjectModel

Imports System.ComponentModel
Imports System.Configuration
Imports System.Net.Configuration

Public Class SettingsWindowViewModel
    Inherits ObservableObject

    Private m_SettingsGroups As ObservableCollection(Of SettingsGroupViewModel)
    Private m_SelectedSettingsGroup As SettingsGroupViewModel
    Private m_SaveRequired As Boolean
    Private m_Model As TeamFoundation.Config.SettingsSection

    Public ReadOnly Property Title() As String
        Get
            Return "TFS Event Handler: Settings"
        End Get
    End Property

    Public ReadOnly Property SettingsGroups() As ObservableCollection(Of SettingsGroupViewModel)
        Get
            Return m_SettingsGroups
        End Get
    End Property

    Public Property SelectedSettingsGroup() As SettingsGroupViewModel
        Get
            Return m_SelectedSettingsGroup
        End Get
        Set(ByVal value As SettingsGroupViewModel)
            If Not value.Equals(m_SelectedSettingsGroup) Then
                m_SelectedSettingsGroup = value
                OnPropertyChanged("SelectedSettingsGroup")
            End If
        End Set
    End Property

    Public Property SaveRequired() As Boolean
        Get
            Return m_SaveRequired
        End Get
        Set(ByVal value As Boolean)
            If value.Equals(m_SaveRequired) Then
                m_SaveRequired = value
                OnPropertyChanged("SaveRequired")
            End If
        End Set
    End Property

    Public Sub New()
        m_Model = TeamFoundation.Config.SettingsSection.ServiceInstance


        m_SettingsGroups = New ObservableCollection(Of SettingsGroupViewModel)

        m_SettingsGroups.Add(New SettingsGroupViewModel("General", _
                            New SettingsViewModel(New HeaderViewModel("General")), _
                            New BaseAddressViewModel(m_Model.BaseAddress) _
                            ))
        m_SettingsGroups.Add(New SettingsGroupViewModel("Events", _
                                   New EventListViewModel(m_Model.EventItems) _
                                   ))

        m_SettingsGroups.Add(New SettingsGroupViewModel("Servers", _
                                   New TeamServerListViewModel(m_Model.TeamServerItems) _
                                   ))

        m_SettingsGroups.Add(New SettingsGroupViewModel("Replace", _
                                  New UrlReplacementListViewModel(m_Model.UrlReplacementItems) _
                                  ))


        SelectedSettingsGroup = SettingsGroups(0)
    End Sub



End Class
