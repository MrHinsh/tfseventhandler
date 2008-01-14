Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports Microsoft.TeamFoundation
Imports Microsoft.TeamFoundation.Client
Imports Microsoft.TeamFoundation.Server
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Services.DataContracts

Module ServiceExtensionModule

    <System.Runtime.CompilerServices.Extension()> _
Friend Function ToServerNameCollection(ByVal x As Collection(Of Microsoft.TeamFoundation.Client.TeamFoundationServer)) As Collection(Of TeamServerItem)
        Dim servers As New Collection(Of TeamServerItem)
        For Each server As Microsoft.TeamFoundation.Client.TeamFoundationServer In x
            servers.Add(New TeamServerItem(server))
        Next
        Return servers
    End Function

End Module
