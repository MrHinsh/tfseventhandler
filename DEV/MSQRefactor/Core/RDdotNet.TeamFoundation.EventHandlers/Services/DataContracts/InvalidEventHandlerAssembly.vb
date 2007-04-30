Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization

Namespace Services.DataContracts


    <DataContract()> _
    Public Class InvalidEventHandlerAssembly
        Inherits System.ServiceModel.FaultException

        Public Sub New()

        End Sub

    End Class


End Namespace