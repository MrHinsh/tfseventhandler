Public Class SettingsViewModel
    Inherits ObservableObject

    Private m_IsDirty As Boolean
    Private m_Header As HeaderViewModel

    Public Property IsDirty() As Boolean
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
    'ISettingsHeaderViewModel
    Public Property Header() As HeaderViewModel
        Get
            Return m_Header
        End Get
        Set(ByVal value As HeaderViewModel)
            If Not value.Equals(m_Header) Then
                m_Header = value
                OnPropertyChanged("Header")
            End If
        End Set
    End Property

    Public Sub New(ByVal headerViewModel As HeaderViewModel)
        m_Header = headerViewModel
    End Sub

End Class
