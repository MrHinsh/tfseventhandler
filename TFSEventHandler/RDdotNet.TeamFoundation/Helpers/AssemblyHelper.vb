Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports MerrillLynch.TeamFoundation
Imports MerrillLynch.TeamFoundation.Config

Public Class AssemblyHelper

    Private Sub New()

    End Sub

    Private Shared _EventHandlerTestDomain As AppDomain

    Public Shared ReadOnly Property EventHandlerTestDomain(ByVal LocalPath As String) As AppDomain
        Get
            If _EventHandlerTestDomain Is Nothing Then
                _EventHandlerTestDomain = AppDomain.CreateDomain("EventHandlerTest", Nothing, LocalPath, "", True)
            End If
            Return _EventHandlerTestDomain
        End Get
    End Property

    Friend Shared Function GetEventHandlers(ByVal Assembly As System.Reflection.Assembly) As Collection(Of EventHandlerItem)
        GetEventHandlers = New Collection(Of EventHandlerItem)
        For Each objType As System.Type In Assembly.GetTypes
            For Each objAttribute As EventHandlerAttribute In objType.GetCustomAttributes(GetType(EventHandlerAttribute), False)
                GetEventHandlers.Add(New EventHandlerItem(objType, objAttribute.EventType))
                Exit For
            Next
        Next
    End Function

    Friend Shared Function GetAssemblyItem(ByVal LocalPath As String, ByVal AssemblyBytes As Byte()) As AssemblyItem

        Try
            Dim tempAssI As New AssemblyItem
            Dim Assembly As System.Reflection.Assembly = EventHandlerTestDomain(LocalPath).Load(AssemblyBytes)
            tempAssI.Name = Assembly.GetName
            tempAssI.EventHandlers = GetEventHandlers(Assembly)
            If tempAssI.EventHandlers.Count > 0 Then
                ' Is OK - 
                Dim objFstream As IO.FileStream = Nothing
                Try
                    tempAssI.ID = Guid.NewGuid
                    tempAssI.Location = LocalPath
                    tempAssI.Location = System.IO.Path.Combine(tempAssI.Location, tempAssI.ID.ToString & ".dll")
                    objFstream = IO.File.Open(tempAssI.Location, IO.FileMode.Create, IO.FileAccess.Write)
                    Dim lngLen As Long = AssemblyBytes.Length
                    objFstream.Write(AssemblyBytes, 0, CInt(lngLen))
                    objFstream.Flush()
                    objFstream.Close()
                    '---------------------
                Catch exc As System.UnauthorizedAccessException
                    If System.IO.File.Exists(tempAssI.Location) Then
                        System.IO.File.Delete(tempAssI.Location)
                    End If
                    Throw New System.ServiceModel.FaultException(Of System.UnauthorizedAccessException)(exc)
                Catch exc As Exception
                    If System.IO.File.Exists(tempAssI.Location) Then
                        System.IO.File.Delete(tempAssI.Location)
                    End If
                    Throw New System.ServiceModel.FaultException(Of System.Exception)(exc)
                Finally
                    If Not objFstream Is Nothing Then
                        objFstream.Close()
                    End If
                End Try
                Return tempAssI
            Else
                Throw New System.ServiceModel.FaultException(String.Format("The assembly {0} does not contain any.", Assembly.FullName))
            End If
        Catch ex As Exception
            Throw New System.ServiceModel.FaultException(Of System.Exception)(ex)
        End Try
        '--------------
        Return Nothing
    End Function

    Friend Shared Sub Bin(ByVal AssemblyItem As AssemblyItem)
        System.IO.File.Delete(AssemblyItem.Location)
    End Sub

End Class
