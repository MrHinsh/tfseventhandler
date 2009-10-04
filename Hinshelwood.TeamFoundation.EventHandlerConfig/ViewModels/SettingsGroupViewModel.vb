Imports System.Collections.ObjectModel

Public Class SettingsGroupViewModel
    Inherits ObservableObject

    Private m_Settings As ObservableCollection(Of SettingsViewModel)
    Private m_Title As String

    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_Title) Then
                m_Title = value
                OnPropertyChanged("Title")
            End If
        End Set
    End Property

    Public ReadOnly Property Settings() As ObservableCollection(Of SettingsViewModel)
        Get
            Return m_Settings
        End Get
    End Property

    Public Sub New(ByVal title As String, ByVal ParamArray settings() As SettingsViewModel)
        m_Title = title
        m_Settings = New ObservableCollection(Of SettingsViewModel)
        For Each setting In settings
            m_Settings.Add(setting)
        Next
    End Sub

    Public Sub AddSettings(ByVal settings As SettingsViewModel)
        m_Settings.Add(settings)
    End Sub

End Class
