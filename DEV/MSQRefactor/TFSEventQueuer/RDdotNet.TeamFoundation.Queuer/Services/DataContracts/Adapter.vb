Public Class Adapter

    Public Shared Function Convert(ByVal source As TFSIdentity) As Proxys.EventHandlerService.TFSIdentity
        Dim x As New Proxys.EventHandlerService.TFSIdentity
        x.url = source.Url
        Return x
    End Function

    Public Shared Function Convert(ByVal source As SubscriptionInfo) As Proxys.EventHandlerService.SubscriptionInfo
        Dim x As New Proxys.EventHandlerService.SubscriptionInfo
        x.ID = source.ID
        x.Classification = source.Classification
        x.Subscriber = source.Subscriber
        Return x
    End Function

End Class
