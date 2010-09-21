Imports Microsoft.TeamFoundation.Framework.Server
Imports Microsoft.TeamFoundation.WorkItemTracking.Server

Public Class TfsEventHandlerEntryPoint
    Implements ISubscriber

    Public ReadOnly Property Name As String Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.Name
        Get
            Return My.Application.Info.ProductName
        End Get
    End Property

    Public ReadOnly Property Priority As Microsoft.TeamFoundation.Framework.Server.SubscriberPriority Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.Priority
        Get
            Return SubscriberPriority.Normal
        End Get
    End Property

    Public Function ProcessEvent(ByVal requestContext As Microsoft.TeamFoundation.Framework.Server.TeamFoundationRequestContext, ByVal notificationType As Microsoft.TeamFoundation.Framework.Server.NotificationType, ByVal notificationEventArgs As Object, ByRef statusCode As Integer, ByRef statusMessage As String, ByRef properties As Microsoft.TeamFoundation.Common.ExceptionPropertyCollection) As Microsoft.TeamFoundation.Framework.Server.EventNotificationStatus Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.ProcessEvent

        Dim objWriter As System.IO.StreamWriter = System.IO.File.CreateText(String.Format("c:\temp\ServerEntryPoint-{0}.txt", Now.Ticks))
        objWriter.WriteLine("-----------------------------------------------")
        Try
            statusCode = 0
            properties = Nothing
            statusMessage = String.Empty

            ' 

            objWriter.WriteLine("Recieved WorkItemChangedEvent ")
            objWriter.WriteLine(String.Format("notificationType: {0}", notificationType.ToString))
            objWriter.WriteLine(String.Format("notificationEventArgs: {0}", notificationEventArgs.ToString))

            objWriter.WriteLine(String.Format("requestContext:"))
            objWriter.WriteLine(String.Format("    AuthenticatedUserName: {0}", requestContext.AuthenticatedUserName))
            objWriter.WriteLine(String.Format("    Command: {0}", requestContext.Command))
            objWriter.WriteLine(String.Format("    ContextId: {0}", requestContext.ContextId))
            objWriter.WriteLine(String.Format("    DomainUserName: {0}", requestContext.DomainUserName))
            objWriter.WriteLine(String.Format("    EndTime: {0}", requestContext.EndTime))
            objWriter.WriteLine(String.Format("    IsServicingContext: {0}", requestContext.IsServicingContext))
            objWriter.WriteLine(String.Format("    IsSystemContext: {0}", requestContext.IsSystemContext))

            For Each i In requestContext.Items
                objWriter.WriteLine(String.Format("        {0}: {1}", i.Key, i.Value.ToString))
            Next
            If Not requestContext.Method Is Nothing Then
                objWriter.WriteLine(String.Format("    Method: {0}", requestContext.Method.ToString))
            End If

            objWriter.WriteLine(String.Format("    Queued: {0}", requestContext.Queued))
            objWriter.WriteLine(String.Format("    QueuedTime: {0}", requestContext.QueuedTime))
            objWriter.WriteLine(String.Format("    ServiceName: {0}", requestContext.ServiceName))
            objWriter.WriteLine(String.Format("    StartTime: {0}", requestContext.StartTime))
            If Not requestContext.Status Is Nothing Then
                objWriter.WriteLine(String.Format("    Status: {0}", requestContext.Status.ToString))
            End If
            objWriter.WriteLine(String.Format("    UniqueIdentifier: {0}", requestContext.UniqueIdentifier.ToString))
            objWriter.WriteLine(String.Format("    UserContext: {0}", requestContext.UserContext.ToString))

            objWriter.WriteLine(String.Format("    ServiceHost.Name: {0}", requestContext.ServiceHost.Name.ToString))
            objWriter.WriteLine(String.Format("    ServiceHost.InstanceId: {0}", requestContext.ServiceHost.InstanceId.ToString))
            objWriter.WriteLine(String.Format("    ServiceHost.DataDirectory: {0}", requestContext.ServiceHost.DataDirectory.ToString))
            objWriter.WriteLine(String.Format("    ServiceHost.Culture: {0}", requestContext.ServiceHost.Culture.ToString))
            objWriter.WriteLine(String.Format("    ServiceHost.VirtualDirectory: {0}", requestContext.ServiceHost.VirtualDirectory.ToString))



            Dim WITS As WorkItemTrackingService = requestContext.GetService(Of WorkItemTrackingService)()
            If (notificationType = notificationType.Notification) AndAlso (TypeOf notificationEventArgs Is WorkItemChangedEvent) Then
                Dim wice As WorkItemChangedEvent = CType(notificationEventArgs, WorkItemChangedEvent)

            End If


            '  End If
        Catch ex As Exception
            objWriter.WriteLine(ex.ToString)
        End Try
        objWriter.WriteLine("-----------------------------------------------")
        objWriter.Close()
        Return EventNotificationStatus.ActionPermitted
    End Function

    Public Function SubscribedTypes() As System.Type() Implements Microsoft.TeamFoundation.Framework.Server.ISubscriber.SubscribedTypes
        Return New Type() {GetType(WorkItemChangedEvent)}
    End Function


End Class
