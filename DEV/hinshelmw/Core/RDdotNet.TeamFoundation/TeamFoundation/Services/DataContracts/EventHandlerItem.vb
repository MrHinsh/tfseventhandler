Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Events.Handlers

Namespace TeamFoundation.Services.DataContracts


    <DataContract(), Serializable()> _
    Public Class EventHandlerItem

        Private _HandlerType As System.Type
        Private _EventType As EventTypes

        <DataMember()> _
        Public Property HandlerType() As System.Type
            Get
                Return _HandlerType
            End Get
            Set(ByVal value As System.Type)
                _HandlerType = value
            End Set
        End Property

        <DataMember()> _
       Public Property EventType() As EventTypes
            Get
                Return _EventType
            End Get
            Set(ByVal value As EventTypes)
                _EventType = value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal HandlerType As System.Type, ByVal EventType As EventTypes)
            _HandlerType = HandlerType
        End Sub


    End Class


End Namespace