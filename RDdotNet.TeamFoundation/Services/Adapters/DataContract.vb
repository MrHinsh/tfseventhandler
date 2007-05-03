Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Proxys

Namespace Adapters

    Partial Public Class DataContract
        Implements IAdapter(Of TFSIdentity, EventHandlerService.TFSIdentity)
        Implements IAdapter(Of SubscriptionInfo, EventHandlerService.SubscriptionInfo)
        Implements IAdapter(Of Microsoft.TeamFoundation.Server.SubscriptionInfo, EventHandlerService.SubscriptionInfo)

        Public Function Convert(ByVal source As TFSIdentity) As EventHandlerService.TFSIdentity Implements IAdapter(Of TFSIdentity, Proxys.EventHandlerService.TFSIdentity).Convert
            Dim x As New Proxys.EventHandlerService.TFSIdentity
            x.url = source.Url
            Return x
        End Function

        Public Function Convert(ByVal source As EventHandlerService.TFSIdentity) As TFSIdentity Implements IAdapter(Of TFSIdentity, Proxys.EventHandlerService.TFSIdentity).Convert
            Dim x As New TFSIdentity
            x.Url = source.url
            Return x
        End Function

        Public Function Convert(ByVal source As SubscriptionInfo) As EventHandlerService.SubscriptionInfo Implements IAdapter(Of Services.DataContracts.SubscriptionInfo, Proxys.EventHandlerService.SubscriptionInfo).Convert
            Dim x As New Proxys.EventHandlerService.SubscriptionInfo
            x.ID = source.ID
            x.Classification = source.Classification
            x.Subscriber = source.Subscriber
            Return x
        End Function

        Public Function Convert(ByVal source As Microsoft.TeamFoundation.Server.SubscriptionInfo) As Proxys.EventHandlerService.SubscriptionInfo Implements IAdapter(Of Microsoft.TeamFoundation.Server.SubscriptionInfo, Proxys.EventHandlerService.SubscriptionInfo).Convert
            Dim x As New Proxys.EventHandlerService.SubscriptionInfo
            x.ID = source.ID
            x.Classification = source.Classification
            x.Subscriber = source.Subscriber
            Return x
        End Function

        Public Function Convert(ByVal source As Proxys.EventHandlerService.SubscriptionInfo) As Microsoft.TeamFoundation.Server.SubscriptionInfo Implements IAdapter(Of Microsoft.TeamFoundation.Server.SubscriptionInfo, Proxys.EventHandlerService.SubscriptionInfo).Convert
            Dim x As New Microsoft.TeamFoundation.Server.SubscriptionInfo
            x.ID = source.ID
            x.Classification = source.Classification
            x.Subscriber = source.Subscriber
            Return x
        End Function

        Public Function Convert1(ByVal source As Proxys.EventHandlerService.SubscriptionInfo) As Services.DataContracts.SubscriptionInfo Implements IAdapter(Of Services.DataContracts.SubscriptionInfo, Proxys.EventHandlerService.SubscriptionInfo).Convert
            Dim x As New SubscriptionInfo
            x.ID = source.ID
            x.Classification = source.Classification
            x.Subscriber = source.Subscriber
            Return x
        End Function

    End Class

End Namespace
