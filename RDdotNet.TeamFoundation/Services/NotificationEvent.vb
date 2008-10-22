Public Class NotificationEvent(Of TEvent)

    Private _Identity As TFSIdentity
    Private _Event As TEvent
    Private _SubscriptionInfo As TeamFoundation.SubscriptionInfo

    Public ReadOnly Property Identity() As TFSIdentity
        Get
            Return _Identity
        End Get
    End Property

    Public ReadOnly Property [Event]() As TEvent
        Get
            Return _Event
        End Get
    End Property

    Public ReadOnly Property SubscriptionInfo() As TeamFoundation.SubscriptionInfo
        Get
            Return _SubscriptionInfo
        End Get
    End Property

    Public Sub New(ByVal EventObject As TEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As TeamFoundation.SubscriptionInfo)
        _Identity = EventIdentity
        _Event = EventObject
        _SubscriptionInfo = SubscriptionInfo
    End Sub

End Class
