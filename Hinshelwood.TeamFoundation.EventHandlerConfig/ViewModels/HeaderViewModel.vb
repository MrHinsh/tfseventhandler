Public Class HeaderViewModel
    Inherits ObservableObject

    Private m_Header As String

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

    Public Sub New(ByVal header As String)
        m_Header = header
    End Sub

End Class
