Imports Microsoft.TeamFoundation
Imports Microsoft.TeamFoundation.Client
Imports Microsoft.TeamFoundation.Server
Imports RDdotNet.TeamFoundation.Events

Namespace Helpers


    ''' <summary>
    ''' Helper methods for subscribing to and from events
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SubscriptionHelper

        Public Shared Function SubscribeEvent(ByRef tfs As TeamFoundationServer, ByVal userName As String, ByVal deliveryAddress As String, ByVal Type As Microsoft.TeamFoundation.Server.DeliveryType, ByVal Schedule As Microsoft.TeamFoundation.Server.DeliverySchedule, ByVal EventType As EventTypes, Optional ByVal Filter As String = "") As Integer
            Dim eventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
            Dim delivery As DeliveryPreference = New DeliveryPreference()
            delivery.Type = Type
            delivery.Schedule = Schedule
            delivery.Address = deliveryAddress
            Return eventService.SubscribeEvent(userName, EventType.ToString, Filter, delivery)
        End Function


        Public Shared Sub UnSubscribeEvent(ByRef tfs As TeamFoundationServer, ByVal subscriptionId As Integer)
            Dim eventService As IEventService = CType(tfs.GetService(GetType(IEventService)), IEventService)
            eventService.UnsubscribeEvent(subscriptionId)
        End Sub

    End Class

End Namespace