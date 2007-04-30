Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports merrilllynch.TeamFoundation
Imports merrilllynch.TeamFoundation.Config

<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
Public Class WorkItemChangedService
    Inherits AEventService(Of WorkItemChangedEvent)

    Public Overrides Sub OnStart()

    End Sub

    Public Overrides Sub OnEnd()

    End Sub

    Public Overrides Sub OnEventRecieved(ByVal sender As Microsoft.TeamFoundation.Client.TeamFoundationServer, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent))

    End Sub

End Class

