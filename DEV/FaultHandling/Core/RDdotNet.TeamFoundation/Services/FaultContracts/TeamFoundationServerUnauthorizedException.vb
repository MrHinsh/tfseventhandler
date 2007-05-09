Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization

Namespace Services.FaultContracts


    <DataContract()> _
    Public Class TeamFoundationServerUnauthorizedException
        Inherits System.ServiceModel.FaultException

        Public Sub New()
            MyBase.New("You do not have permission to perform this action")
        End Sub

    End Class


End Namespace