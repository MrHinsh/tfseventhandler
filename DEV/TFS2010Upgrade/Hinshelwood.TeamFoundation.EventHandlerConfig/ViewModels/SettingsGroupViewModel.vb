Imports System.Collections.ObjectModel

Public Class SettingsGroupViewModel
    Inherits SettingsViewModel

    Private m_Settings As ObservableCollection(Of SettingsViewModel)

    Public ReadOnly Property Settings() As ObservableCollection(Of SettingsViewModel)
        Get
            Return m_Settings
        End Get
    End Property

    Public Sub New(ByVal header As String, ByVal ParamArray settings() As SettingsViewModel)
        MyBase.New(New HeaderViewModel(header))
        m_Settings = New ObservableCollection(Of SettingsViewModel)
        For Each setting In settings
            m_Settings.Add(setting)
        Next
    End Sub

    Public Sub AddSettings(ByVal settings As SettingsViewModel)
        m_Settings.Add(settings)
    End Sub

    Friend Overrides Sub SavedReset()
        MyBase.SavedReset()
        For Each i In Settings
            i.SavedReset()
        Next
    End Sub

End Class
