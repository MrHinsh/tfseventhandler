Public Class HeaderViewModel
    Inherits ObservableObject

    Private m_Header As String
    Private m_ShortHeader As String

    Public Property Header() As String
        Get
            Return m_header
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_header) Then
                m_header = value
                OnPropertyChanged("Header")
            End If
        End Set
    End Property

    Public Property ShortHeader() As String
        Get
            Return m_ShortHeader
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_ShortHeader) Then
                m_ShortHeader = value
                OnPropertyChanged("ShortHeader")
            End If
        End Set
    End Property

    Public Sub New(ByVal header As String, ByVal shortHeader As String)
        m_Header = header
        m_ShortHeader = shortHeader
    End Sub

End Class
