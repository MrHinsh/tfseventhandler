Imports Microsoft.TeamFoundation.Client

Public Interface IEventHandler(Of TEvent, THandlerConfig As AEventHandlerConfig)

    Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of TEvent, THandlerConfig), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of TEvent))
    Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of TEvent, THandlerConfig), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of TEvent)) As Boolean

End Interface
