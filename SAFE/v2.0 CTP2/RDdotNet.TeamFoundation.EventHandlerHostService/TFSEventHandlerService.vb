Imports System.Messaging
Imports RDdotNet.TeamFoundation.Services

Public Class TFSEventHandlerService

    Private EventHandlerHost As System.ServiceModel.ServiceHost
    Private QueuerHost As System.ServiceModel.ServiceHost

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            Dim queueName As String = ".\private$\TFSEventHandler"
            ' Create the transacted MSMQ queue, if necessary.
            If Not MessageQueue.Exists(queueName) Then
                MessageQueue.Create(queueName, True)
            End If
            ' Add code here to start your service. This method should set things
            ' in motion so your service can do its work.
            QueuerHost = ServiceFactory.GetQueuerServiceHost(6661)
            EventHandlerHost = ServiceFactory.GetEventHandlerServiceHost(6661)
            QueuerHost.Open()
            EventHandlerHost.Open()
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Start, "OnStart Error")
        End Try
       
    End Sub

    Protected Overrides Sub OnStop()
        Try
            ' Add code here to perform any tear-down necessary to stop your service.
            EventHandlerHost.Close()
            QueuerHost.Close()
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Stop, "OnStop Error")
        End Try
       
    End Sub

End Class
