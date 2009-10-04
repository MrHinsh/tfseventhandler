Public Class SettingsViewModel
    Inherits ObservableObject
    Implements ISettingsViewModel

    Private m_IsDirty As Boolean
    Private m_Header As String
    Private m_TabHeader As String

    Public Property IsDirty() As Boolean Implements ISettingsViewModel.IsDirty
        Get
            Return m_IsDirty
        End Get
        Set(ByVal value As Boolean)
            If Not value.Equals(m_IsDirty) Then
                m_IsDirty = value
                OnPropertyChanged("IsDirty")
            End If
        End Set
    End Property

    Public Property Header() As String Implements ISettingsViewModel.Header
        Get
            Return m_Header
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_Header) Then
                m_Header = value
                OnPropertyChanged("Header")
            End If
        End Set
    End Property

    Public Property TabHeader() As String Implements ISettingsViewModel.TabHeader
        Get
            Return m_TabHeader
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TabHeader) Then
                m_TabHeader = value
                OnPropertyChanged("TabHeader")
            End If
        End Set
    End Property

    Public Sub New(ByVal header As String, ByVal tabHeader As String)
        m_Header = header
        m_TabHeader = tabHeader
    End Sub
End Class
