Imports System.Runtime.Serialization
Imports RDdotNet.TeamFoundation.Events

Namespace Services.DataContracts


    <DataContract()> _
    Public Class DeliveryPreferenceItem

        Private m_Address As String
        Private m_Schedule As Microsoft.TeamFoundation.Server.DeliverySchedule
        Private m_Type As Microsoft.TeamFoundation.Server.DeliveryType

        <DataMember()> _
        Public Property Address() As String
            Get
                Return m_Address
            End Get
            Set(ByVal value As String)
                m_Address = value
            End Set
        End Property

        <DataMember()> _
        Public Property Schedule() As Microsoft.TeamFoundation.Server.DeliverySchedule
            Get
                Return m_Schedule
            End Get
            Set(ByVal value As Microsoft.TeamFoundation.Server.DeliverySchedule)
                m_Schedule = value
            End Set
        End Property

        <DataMember()> _
        Public Property Type() As Microsoft.TeamFoundation.Server.DeliveryType
            Get
                Return m_Type
            End Get
            Set(ByVal value As Microsoft.TeamFoundation.Server.DeliveryType)
                m_Type = value
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal DeliveryPreference As Microsoft.TeamFoundation.Server.DeliveryPreference)
            Me.Address = DeliveryPreference.Address
            Me.Schedule = DeliveryPreference.Schedule
            Me.Type = DeliveryPreference.Type
        End Sub

        Public Sub New(ByVal Address As String, ByVal Schedule As Microsoft.TeamFoundation.Server.DeliverySchedule, ByVal Type As Microsoft.TeamFoundation.Server.DeliveryType)
            Me.Address = Address
            Me.Schedule = Schedule
            Me.Type = Type
        End Sub

    End Class

End Namespace