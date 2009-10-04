Imports System.Collections.ObjectModel

Imports System.ComponentModel
Imports System.Configuration
Imports System.Net.Configuration

Public Class SettingsWindowViewModel
    Inherits ObservableObject

    Private m_Settings As ObservableCollection(Of ISettingsViewModel)
    Private m_SelectedSetting As ISettingsViewModel
    Private m_SaveRequired As Boolean

    Public ReadOnly Property Title() As String
        Get
            Return "TFS Event Handler: Settings"
        End Get
    End Property

    Public ReadOnly Property Settings() As ObservableCollection(Of ISettingsViewModel)
        Get
            Return m_Settings
        End Get
    End Property

    Public Property SelectedSetting() As ISettingsViewModel
        Get
            Return m_SelectedSetting
        End Get
        Set(ByVal value As ISettingsViewModel)
            If Not value.Equals(m_SelectedSetting) Then
                m_SelectedSetting = value
                OnPropertyChanged("SelectedSetting")
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
        m_Settings = New ObservableCollection(Of ISettingsViewModel)

        m_Settings.Add(New SettingsViewModel("General", "General"))

        Dim x As TeamFoundation.Config.SettingsSection = TeamFoundation.Config.SettingsSection.ServiceInstance

        m_Settings.Add(New BaseAddressViewModel(x.BaseAddress))
        m_Settings.Add(New EventHandlerListViewModel(x.EventItems))
        m_Settings.Add(New TeamServerListViewModel(x.TeamServerItems))
        m_Settings.Add(New UrlReplacementListViewModel(x.UrlReplacementItems))
        SelectedSetting = Settings(0)
    End Sub



End Class
