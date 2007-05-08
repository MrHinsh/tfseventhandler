Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Proxys

Namespace Adapters

    Partial Public Class DataContracts
        Implements IAdapter(Of TFSIdentity, EventHandlerService.TFSIdentity)
        Implements IAdapter(Of SubscriptionInfo, EventHandlerService.SubscriptionInfo)

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

        Public Function Convert(ByVal source As EventHandlerService.SubscriptionInfo) As SubscriptionInfo Implements IAdapter(Of Services.DataContracts.SubscriptionInfo, Proxys.EventHandlerService.SubscriptionInfo).Convert
            Dim x As New SubscriptionInfo
            x.ID = source.ID
            x.Classification = source.Classification
            x.Subscriber = source.Subscriber
            Return x
        End Function

    End Class

End Namespace
