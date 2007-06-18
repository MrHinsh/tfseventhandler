'Imports System.ServiceModel
'Imports System.Runtime.Serialization


'''' <summary>
'''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
'''' </summary>
'''' <remarks></remarks>
'Public Interface IEventHandlerCallback

'    <OperationContract(IsOneWay:=True)> _
'    Sub TeamServerManager_StatusChange(ByVal TeamServer As String, ByVal Status As Status, ByVal Items As Integer)
'    <OperationContract(IsOneWay:=True)> _
'    Sub TeamServerManager_Error(ByVal TeamServer As String, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
'    <OperationContract(IsOneWay:=True)> _
'    Sub ServiceHostManager_StatusChange(ByVal ServiceHost As String, ByVal Status As Status, ByVal Items As Integer)
'    <OperationContract(IsOneWay:=True)> _
'    Sub ServiceHostManager_Error(ByVal ServiceHost As String, ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
'    <OperationContract(IsOneWay:=True)> _
'    Sub WorkItemChangedManager_StatusChange(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer)
'    <OperationContract(IsOneWay:=True)> _
'    Sub WorkItemChangedManager_Error(ByVal ManagedType As EventHandlerItem(Of WorkItemChangedEvent), ByVal Status As Status, ByVal Items As Integer, ByVal e As System.Exception)
'    <OperationContract(IsOneWay:=True)> _
'    Sub WorkItemChangedEvent(ByVal ServiceHost As String, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))


'End Interface
