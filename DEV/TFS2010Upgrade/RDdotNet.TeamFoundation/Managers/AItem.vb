Imports System.ServiceModel
Imports System.Runtime.Serialization


Public Class AItem(Of TSubject, TItemElement)
    Inherits Item

    Private _Subject As TSubject = Nothing
    Private _ItemElement As TItemElement = Nothing

    Public Property Subject() As TSubject
        Get
            Return _Subject
        End Get
        Friend Set(ByVal value As TSubject)
            _Subject = value
        End Set
    End Property

    Public ReadOnly Property ItemElement() As TItemElement
        Get
            Return _ItemElement
        End Get
    End Property

    Public Sub New(ByVal Subject As TSubject, ByVal ItemElement As TItemElement)
        _Subject = Subject
        _ItemElement = ItemElement
    End Sub

End Class
