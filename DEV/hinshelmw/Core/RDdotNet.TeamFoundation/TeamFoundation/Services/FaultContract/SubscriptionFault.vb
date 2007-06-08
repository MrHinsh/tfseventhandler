Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization

Namespace TeamFoundation.Services.FaultContracts


    <DataContract()> _
    Public Class SubscriptionFault(Of T)
        Inherits System.ServiceModel.FaultException(Of T)

        Public Sub New(ByVal detail As T)
            MyBase.New(detail)
        End Sub

    End Class

End Namespace
