<AttributeUsage(AttributeTargets.Class)> _
Public Class EventHandlerAttribute
    Inherits Attribute

    Private _EventType As EventTypes

    Public ReadOnly Property EventType() As EventTypes
        Get
            Return _EventType
        End Get
    End Property

    Sub New(ByVal EventType As EventTypes, ByVal MailTo As String)
        _EventType = EventType
    End Sub

    Private _LogEvents As Boolean = True

    Public Property LogEvents() As Boolean
        Get
            Return _LogEvents
        End Get
        Set(ByVal value As Boolean)
            _LogEvents = value
        End Set
    End Property

    Private _LogVerbose As Boolean = True

    Public Property LogVerbose() As Boolean
        Get
            Return _LogVerbose
        End Get
        Set(ByVal value As Boolean)
            _LogVerbose = value
        End Set
    End Property

    Public _MailTo As String

    Public Property MailTo() As String
        Get
            Return _MailTo
        End Get
        Set(ByVal value As String)
            _MailTo = value
        End Set
    End Property

    Private _TestMode As Boolean = True
    ''' <summary>
    ''' The type of the System to use.
    ''' </summary>
    Public Property TestMode() As Boolean
        Get
            Return _TestMode
        End Get
        Set(ByVal value As Boolean)
            _TestMode = value
        End Set
    End Property

End Class
