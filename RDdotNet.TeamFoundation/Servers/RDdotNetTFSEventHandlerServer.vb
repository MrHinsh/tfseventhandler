Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Servers

    Public Class RDdotNetTFSEventHandlerServer
        Inherits RDdotNet.Servers.RDdotNetServerBase


        Protected Overrides Sub OnServicesLoad(ByRef ClientServices As System.Collections.ObjectModel.Collection(Of Clients.IClientService))
            ClientServices.Add(New TeamFoundation.Clients.EventsService(Me.Uri))
            ClientServices.Add(New TeamFoundation.Clients.HandlersService(Me.Uri))
        End Sub

        Protected Overrides Sub OnServicesUnload(ByRef ClientServices As System.Collections.ObjectModel.Collection(Of Clients.IClientService))

        End Sub
    End Class


End Namespace