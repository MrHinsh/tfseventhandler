Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Services.DataContracts

    <DataContract(), Serializable()> _
    Public Class AssemblyItem

        Private m_EventHandlers As Collection(Of EventHandlerItem) = New Collection(Of EventHandlerItem)
        Private m_Name As String
        Private m_Location As String
        Private m_State As AssemblyItemStates = AssemblyItemStates.Unknown
        Private m_StateReason As System.ServiceModel.FaultException = Nothing

        <DataMember()> _
        Public Property EventHandlers() As Collection(Of EventHandlerItem)
            Get
                Return m_EventHandlers
            End Get
            Set(ByVal value As Collection(Of EventHandlerItem))
                m_EventHandlers = value
            End Set
        End Property

        <DataMember()> _
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property

        <DataMember()> _
        Public Property Location() As String
            Get
                Return m_Location
            End Get
            Set(ByVal value As String)
                m_Location = value
            End Set
        End Property

        <DataMember()> _
        Public Property State() As AssemblyItemStates
            Get
                Return m_State
            End Get
            Set(ByVal value As AssemblyItemStates)
                m_State = value
                m_StateReason = Nothing
            End Set
        End Property

        <DataMember()> _
        Public Property StateReason() As System.ServiceModel.FaultException
            Get
                Return m_StateReason
            End Get
            Set(ByVal value As System.ServiceModel.FaultException)
                m_StateReason = value
            End Set
        End Property

    End Class

    <DataContract()> _
    Public Enum AssemblyItemStates
        <EnumMember()> Unknown
        <EnumMember()> Valid
        <EnumMember()> Invalid
        <EnumMember()> NotFound
    End Enum

End Namespace