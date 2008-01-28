Imports System.Runtime.Serialization
Imports RDdotNet.TeamFoundation.Events

Namespace Services.DataContracts


    <DataContract()> _
    Public Class SubscriptionItem

        Private m_id As Integer
        Private m_TeamServerItem As TeamServerItem
        Private m_EventType As EventTypes
        Private m_ConditionString As String
        Private m_Device As String
        Private m_Subscriber As String
        Private m_Tag As String
        Private m_DeliveryPreferenceItem As DeliveryPreferenceItem

        <DataMember()> _
        Public Property EventType() As EventTypes
            Get
                Return m_EventType
            End Get
            Set(ByVal value As EventTypes)
                m_EventType = value
            End Set
        End Property

        <DataMember()> _
        Public Property ID() As Integer
            Get
                Return m_id
            End Get
            Set(ByVal value As Integer)
                m_id = value
            End Set
        End Property

        <DataMember()> _
        Public Property TeamServerItem() As TeamServerItem
            Get
                Return m_TeamServerItem
            End Get
            Set(ByVal value As TeamServerItem)
                m_TeamServerItem = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property ConditionString() As String
            Get
                Return m_ConditionString
            End Get
            Set(ByVal value As String)
                m_ConditionString = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property Device() As String
            Get
                Return m_Device
            End Get
            Set(ByVal value As String)
                m_Device = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property Subscriber() As String
            Get
                Return m_Subscriber
            End Get
            Set(ByVal value As String)
                m_Subscriber = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property Tag() As String
            Get
                Return m_Tag
            End Get
            Set(ByVal value As String)
                m_Tag = value
            End Set
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember()> _
        Public Property DeliveryPreference() As DeliveryPreferenceItem
            Get
                Return m_DeliveryPreferenceItem
            End Get
            Set(ByVal value As DeliveryPreferenceItem)
                m_DeliveryPreferenceItem = value
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal TeamServerItem As TeamServerItem, ByVal id As Integer, ByVal EventType As EventTypes, ByVal ConditionString As String, ByVal Device As String, ByVal Subscriber As String, ByVal Tag As String, ByVal DeliveryPreferenceItem As DeliveryPreferenceItem)
            Me.TeamServerItem = TeamServerItem
            Me.ID = id
            Me.EventType = EventType 'Me.EventType = CType([Enum].Parse(GetType(EventTypes), EventType), EventTypes)
            Me.ConditionString = ConditionString
            Me.Device = Device
            Me.Subscriber = Subscriber
            Me.Tag = Tag
            Me.DeliveryPreference = DeliveryPreferenceItem
        End Sub

        Public Sub New(ByVal TeamServerItem As TeamServerItem, ByVal Subscription As Microsoft.TeamFoundation.Server.Subscription)
            Me.TeamServerItem = TeamServerItem
            Me.ID = Subscription.ID
            Me.EventType = CType([Enum].Parse(GetType(EventTypes), Subscription.EventType), EventTypes)
            Me.ConditionString = Subscription.ConditionString
            Me.Device = Subscription.Device
            Me.Subscriber = Subscription.Subscriber
            Me.Tag = Subscription.Tag
            Me.DeliveryPreference = New DeliveryPreferenceItem(Subscription.DeliveryPreference)
        End Sub

    End Class

End Namespace