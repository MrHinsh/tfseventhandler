Public Class LoggingFasad

    Private Sub New()

    End Sub

    Public Shared Sub Log(ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent), ByVal Area As String, ByVal Message As String)
        If TeamServer.ItemElement.LogEvents Then
            LogData(TeamServer, e, Area, Message)
        End If
    End Sub

    Public Shared Sub LogException(ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent), ByVal Area As String, ByVal ex As Exception)
        LogData(TeamServer, e, Area, ex.ToString)
    End Sub

    Private Shared Sub LogData(ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent), ByVal Area As String, ByVal Message As String)
        My.Application.Log.WriteEntry(Message)
        Dim logname As String = String.Format("{0}.log", Area)
        Dim log As String = System.IO.Path.Combine(GetLogPath(TeamServer, e), logname)
        Message = String.Format("--{0}--{1}{2}", Now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff"), vbCrLf, Message)
        System.IO.File.AppendAllText(log, Message)
    End Sub

    Private Shared Function GetLogPath(ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As String
        Dim tfsName As String = TeamServer.Subject.Name
        Dim evntType As String = GetType(WorkItemChangedEvent).ToString
        Dim dt As String = e.EventRecieved.ToString("yyyy-MM-dd-HH-mm-ss-fffffff")
        Dim wiid As String = e.EventID.ToString
        Dim logPath As String = TeamServer.ItemElement.EventLogPath
        logPath = System.IO.Path.Combine(logPath, TeamServer.Subject.Name)
        logPath = System.IO.Path.Combine(logPath, GetType(WorkItemChangedEvent).ToString)
        logPath = System.IO.Path.Combine(logPath, String.Format("{0}-{1}", e.EventRecieved.ToString("yyyy-MM-dd-HH-mm-ss-fffffff"), e.EventID.ToString))
        System.IO.Directory.CreateDirectory(logPath)
        Return logPath
    End Function

End Class
