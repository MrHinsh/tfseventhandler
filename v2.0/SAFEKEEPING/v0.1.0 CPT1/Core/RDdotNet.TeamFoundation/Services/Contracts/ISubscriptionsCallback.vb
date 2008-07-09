Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Services.Contracts

    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ISubscriptionsCallback

        <OperationContract(IsOneWay:=True)> _
      Sub Updated(ByVal TeamServerName As String, ByVal Subscriptions As Collection(Of DataContracts.Subscription))
        <OperationContract(IsOneWay:=True)> _
      Sub ErrorOccured(ByVal ex As Exception)

    End Interface

End Namespace