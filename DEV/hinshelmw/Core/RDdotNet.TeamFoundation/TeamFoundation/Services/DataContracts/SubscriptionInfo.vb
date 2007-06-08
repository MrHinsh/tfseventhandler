Imports System.ServiceModel
Imports System.Runtime.Serialization

Namespace TeamFoundation.Services.DataContracts

    ''' <summary>
    ''' The SubscriptionInfo holds the data for the subscription info
    ''' </summary>
    ''' <remarks></remarks>
    <DataContract(Namespace:="http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/03")> _
    Public Class SubscriptionInfo

        ''' <summary>
        ''' This member is the ID of the subscription on the relevent server. It can be used to unsubscribe.
        ''' </summary>
        ''' <remarks></remarks>
        <DataMember()> _
        Public ID As String

        ''' <summary>
        ''' The SID of the subscriber
        ''' </summary>
        ''' <remarks></remarks>
        <DataMember()> _
     Public Subscriber As String

        ''' <summary>
        ''' Reserved for future use.
        ''' </summary>
        ''' <remarks></remarks>
        <DataMember()> _
     Public Classification As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal SubscriptionInfo As Microsoft.TeamFoundation.Server.SubscriptionInfo)
            Me.Classification = SubscriptionInfo.Classification
            Me.ID = SubscriptionInfo.ID
            Me.Subscriber = SubscriptionInfo.Subscriber
        End Sub

    End Class

End Namespace
