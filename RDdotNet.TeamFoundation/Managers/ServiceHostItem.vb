'Imports System.ServiceModel
'Imports System.ServiceModel.Description
'Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
'Imports MerrillLynch.TeamFoundation
'Imports MerrillLynch.TeamFoundation.Config

'Public Class ServiceHostItem
'    Inherits AItem(Of ServiceHost, EventItemElement)

'    Private _BaseAddress As Uri = Nothing
'    Private _EventType As EventTypes = EventTypes.Unknown

'    Public Property BaseAddress() As Uri
'        Get
'            Return _BaseAddress
'        End Get
'        Friend Set(ByVal value As Uri)
'            _BaseAddress = value
'        End Set
'    End Property

'    Public ReadOnly Property EventType() As EventTypes
'        Get
'            Return _EventType
'        End Get
'    End Property

'    Public Sub New(ByVal EventType As EventTypes, ByVal Subject As ServiceHost, ByVal ItemElement As EventItemElement)
'        MyBase.New(Subject, ItemElement)
'        _EventType = EventType
'    End Sub


'End Class
