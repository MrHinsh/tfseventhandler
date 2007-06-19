Imports Microsoft.TeamFoundation.Client

Public MustInherit Class AEventHandler(Of TEvent, THandlerConfig As AEventHandlerConfig)

    Public MustOverride Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of TEvent, THandlerConfig), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of TEvent))
    Public MustOverride Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of TEvent, THandlerConfig), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of TEvent)) As Boolean

End Class
