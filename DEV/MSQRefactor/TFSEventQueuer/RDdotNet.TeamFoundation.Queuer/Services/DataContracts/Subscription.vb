Imports System.Runtime.Serialization

Namespace Services.DataContracts


    <DataContract()> _
    Public Class Subscription

        Private _id As Integer
        Private _EventType As EventTypes
        Private _Address As String

        <DataMember()> _
        Public Property EventType() As EventTypes
            Get
                Return _EventType
            End Get
            Set(ByVal value As EventTypes)
                _EventType = value
            End Set
        End Property

        <DataMember()> _
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        <DataMember()> _
        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal value As String)
                _Address = value
            End Set
        End Property

        Public Sub New(ByVal Subscription As Microsoft.TeamFoundation.Server.Subscription)
            Me.ID = Subscription.ID
            Me.EventType = CType([Enum].Parse(GetType(EventTypes), Subscription.EventType), EventTypes)
            Me.Address = Subscription.DeliveryPreference.Address
        End Sub

    End Class

End Namespace