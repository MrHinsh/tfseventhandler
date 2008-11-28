Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Hinshelwood.TeamFoundation.Config

Public Class EventHandlerItem(Of TEvent)
    Inherits AItem(Of IEventHandler(Of TEvent), HandlerItemElement)

    Public Sub New(ByVal Subject As IEventHandler(Of TEvent), ByVal ItemElement As HandlerItemElement)
        MyBase.New(Subject, ItemElement)
    End Sub


End Class
