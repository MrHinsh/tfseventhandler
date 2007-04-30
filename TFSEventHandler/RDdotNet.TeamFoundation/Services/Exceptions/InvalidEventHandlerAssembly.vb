Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports MerrillLynch.TeamFoundation
Imports MerrillLynch.TeamFoundation.Config
Imports System.Runtime.Serialization

<DataContract()> _
Public Class InvalidEventHandlerAssembly
    Inherits System.ServiceModel.FaultException

    Public Sub New()

    End Sub

End Class
