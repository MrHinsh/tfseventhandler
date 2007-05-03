Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Events


    Public Class NotifyEventArgs(Of TEvent)
        Inherits EventArgs

        Private _EventID As Guid = Guid.NewGuid
        Private _EventType As EventTypes
        Private _Event As TEvent
        Private _Identity As TFSIdentity
        Private _SubscriptionInfo As SubscriptionInfo

        Public ReadOnly Property EventID() As Guid
            Get
                Return _EventID
            End Get
        End Property

        Public ReadOnly Property EventType() As EventTypes
            Get
                Return _EventType
            End Get
        End Property

        Public ReadOnly Property [Event]() As TEvent
            Get
                Return _Event
            End Get
        End Property

        Public ReadOnly Property Identity() As TFSIdentity
            Get
                Return _Identity
            End Get
        End Property

        Public ReadOnly Property SubscriptionInfo() As SubscriptionInfo
            Get
                Return _SubscriptionInfo
            End Get
        End Property

        Public Sub New(ByVal EventType As EventTypes, ByVal EventObject As TEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo)
            _EventType = EventType
            _Event = EventObject
            _Identity = EventIdentity
            _SubscriptionInfo = SubscriptionInfo
        End Sub

    End Class


End Namespace