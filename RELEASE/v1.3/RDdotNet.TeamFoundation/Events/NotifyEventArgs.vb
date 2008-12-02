Public Class NotifyEventArgs(Of TEvent)
    Inherits EventArgs

    Private m_EventID As Guid = Guid.NewGuid
    Private m_EventRecieved As DateTime = Now
    Private m_EventType As EventTypes
    Private m_Event As TEvent
    Private m_Identity As TFSIdentity
    Private m_SubscriptionInfo As TeamFoundation.SubscriptionInfo

    Public ReadOnly Property EventID() As Guid
        Get
            Return m_EventID
        End Get
    End Property

    Public ReadOnly Property EventRecieved() As DateTime
        Get
            Return m_EventRecieved
        End Get
    End Property

    Public ReadOnly Property EventType() As EventTypes
        Get
            Return m_EventType
        End Get
    End Property

    Public ReadOnly Property [Event]() As TEvent
        Get
            Return m_Event
        End Get
    End Property

    Public ReadOnly Property Identity() As TFSIdentity
        Get
            Return m_Identity
        End Get
    End Property

    Public ReadOnly Property SubscriptionInfo() As TeamFoundation.SubscriptionInfo
        Get
            Return m_SubscriptionInfo
        End Get
    End Property

    Public Sub New(ByVal EventType As EventTypes, ByVal EventObject As TEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As TeamFoundation.SubscriptionInfo)
        m_EventType = EventType
        m_Event = EventObject
        m_Identity = EventIdentity
        m_SubscriptionInfo = SubscriptionInfo
    End Sub

End Class
