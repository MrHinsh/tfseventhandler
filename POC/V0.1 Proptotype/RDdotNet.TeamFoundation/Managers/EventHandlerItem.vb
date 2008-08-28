Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation.Config

Public Class EventHandlerItem(Of TEvent, THandlerConfig As AEventHandlerConfig)
    Inherits AItem(Of IEventHandler(Of TEvent, THandlerConfig), HandlerItemElement(Of THandlerConfig))

    Public Sub New(ByVal Subject As IEventHandler(Of TEvent, THandlerConfig), ByVal ItemElement As HandlerItemElement(Of THandlerConfig))
        MyBase.New(Subject, ItemElement)
    End Sub


End Class
