Imports Microsoft.TeamFoundation.Framework.Server
Imports Microsoft.TeamFoundation.WorkItemTracking.Server

Public Class AssignedToHandler
    Implements ISubscriber

    Public ReadOnly Property Name As String Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.Name
        Get
            Return "test handler"
        End Get
    End Property

    Public ReadOnly Property Priority As Microsoft.TeamFoundation.Framework.Server.SubscriberPriority Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.Priority
        Get
            Return SubscriberPriority.Normal
        End Get
    End Property

    Public Function ProcessEvent(ByVal requestContext As Microsoft.TeamFoundation.Framework.Server.TeamFoundationRequestContext, ByVal notificationType As Microsoft.TeamFoundation.Framework.Server.NotificationType, ByVal notificationEventArgs As Object, ByRef statusCode As Integer, ByRef statusMessage As String, ByRef properties As Microsoft.TeamFoundation.Common.ExceptionPropertyCollection) As Microsoft.TeamFoundation.Framework.Server.EventNotificationStatus Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.ProcessEvent
        Try
            statusCode = 0
            properties = Nothing
            statusMessage = String.Empty
            If (notificationType = notificationType.Notification) AndAlso (TypeOf notificationEventArgs Is WorkItemChangedEvent) Then
                Dim objWriter As New System.IO.StreamWriter("c:\temp\AssignedToHandler.txt")
                objWriter.WriteLine("Recieved WorkItemChangedEvent ")

                objWriter.Close()
            End If
        Catch ex As Exception
            My.Application.Log.WriteException(ex)
        End Try
        Return EventNotificationStatus.ActionPermitted
    End Function

    Public Function SubscribedTypes() As System.Type() Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.SubscribedTypes
        Return New Type() {GetType(WorkItemChangedEvent)}
    End Function


End Class
