'Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
'Imports MerrillLynch.TeamFoundation
'Imports MerrillLynch.TeamFoundation.Config
'Imports Microsoft.TeamFoundation.Client
'Imports Microsoft.TeamFoundation

'Public Class EventHandlersManager(Of TEvent)
'    Inherits AManager(Of EventHandlerItem(Of TEvent))

'    Private _EventHandlers As New Collection(Of EventHandlerItem(Of TEvent))
'    Private _EventType As EventTypes

'    Public Sub New(ByVal EventType As EventTypes, Optional ByVal Initialise As Boolean = True)
'        MyBase.New(Initialise)
'        _EventType = EventType
'    End Sub

'    Public Overrides Sub ManagerBeginCustom(ByVal state As Object)
'        ' Sleep while no Status is set.
'        While _EventType = EventTypes.Unknown
'            System.Threading.Thread.Sleep(100)
'        End While
'        _EventHandlers.Clear()
'        For Each EventItem As EventItemElement In Settings.EventItems
'            If EventItem.EventType = _EventType Then
'                '------------------
'                For Each HandlerItem As HandlerItemElement In EventItem.HandlerItems
'                    Dim EventHandlerBit As New EventHandlerItem(Of TEvent)(Nothing, HandlerItem)
'                    Dim assFile As String = System.IO.Path.Combine(HandlerItem.AssemblyFileLocation, HandlerItem.AssemblyFileName)
'                    If System.IO.File.Exists(assFile) Then
'                        Try
'                            EventHandlerBit.Subject = CType(System.Activator.CreateInstanceFrom(assFile, HandlerItem.Type.ToString).Unwrap, AEventHandler(Of TEvent))
'                            _EventHandlers.Add(EventHandlerBit)
'                            Me.OnStatusChange(EventHandlerBit, Status.ObjectCreated, _EventHandlers.Count)
'                        Catch ex As Exception
'                            Me.OnError(EventHandlerBit, Status.ObjectCreation, _EventHandlers.Count, ex)
'                        End Try
'                    Else
'                        Me.OnError(EventHandlerBit, Status.ObjectCreation, _EventHandlers.Count, New Exception("File " & assFile & " does not exist. The EventHandler will not be loaded"))
'                    End If
'                Next
'                '------------------
'            End If
'        Next
'    End Sub

'    Public Overrides Sub ManagerEndCustom()
'        Me.OnStatusChange(Nothing, Status.Closing, _EventHandlers.Count)
'        _EventHandlers.Clear()
'        _EventHandlers = Nothing
'        Me.OnStatusChange(Nothing, Status.Closed, 0)
'    End Sub

'    Public Sub RunEventHandlers(ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of TEvent))
'        '----------------
'        For Each EventHandlerItem As EventHandlerItem(Of TEvent) In _EventHandlers
'            '----------------
'            EventHandlerItem.Subject.Run(EventHandlerItem, ServiceHost, TeamServer, e)
'            '----------------
'        Next
'        '----------------
'    End Sub

'End Class
