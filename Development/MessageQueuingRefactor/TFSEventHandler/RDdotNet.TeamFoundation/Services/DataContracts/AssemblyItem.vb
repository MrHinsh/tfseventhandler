Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

<DataContract(), Serializable()> _
Public Class AssemblyItem

    Private _EventHandlers As Collection(Of EventHandlerItem)
    Private _Name As Reflection.AssemblyName
    Private _Location As String
    Private _ID As Guid

    <DataMember()> _
    Public Property ID() As Guid
        Get
            Return _ID
        End Get
        Set(ByVal value As Guid)
            _ID = value
        End Set
    End Property

    <DataMember()> _
    Public Property EventHandlers() As Collection(Of EventHandlerItem)
        Get
            Return _EventHandlers
        End Get
        Set(ByVal value As Collection(Of EventHandlerItem))
            _EventHandlers = value
        End Set
    End Property

    <DataMember()> _
    Public Property Name() As Reflection.AssemblyName
        Get
            Return _Name
        End Get
        Set(ByVal value As Reflection.AssemblyName)
            _Name = value
        End Set
    End Property

    <DataMember()> _
    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Sub New()
        _ID = Guid.NewGuid
    End Sub



End Class
