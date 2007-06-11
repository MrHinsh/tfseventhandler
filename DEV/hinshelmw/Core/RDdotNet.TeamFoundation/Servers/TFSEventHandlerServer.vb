Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Servers

    Public Class TFSEventHandlerServer
        Inherits RDdotNet.Servers.RDdotNetServerBase

        Public Sub New(Optional ByVal uri As System.Uri = Nothing)
            MyBase.New(uri)
        End Sub

        Protected Overrides Sub OnServicesLoad()
            AddClientService(New TeamFoundation.Clients.EventsService(Me.ServerUri))
            AddClientService(New TeamFoundation.Clients.HandlersService(Me.ServerUri))
            AddClientService(New TeamFoundation.Clients.TeamServersService(Me.ServerUri))
            AddClientService(New TeamFoundation.Clients.SubscriptionsService())
            AddClientService(New ActiveDirectory.Clients.ActiveDirectoryService)
        End Sub

        Protected Overrides Sub OnServicesUnload(ByRef ClientServices As System.Collections.ObjectModel.Collection(Of Clients.IClientService))

        End Sub

        Public ReadOnly Property EventsService() As TeamFoundation.Clients.EventsService
            Get
                Return Me.GetService(Of TeamFoundation.Clients.EventsService)()
            End Get
        End Property

        Public ReadOnly Property HandlersService() As TeamFoundation.Clients.HandlersService
            Get
                Return Me.GetService(Of TeamFoundation.Clients.HandlersService)()
            End Get
        End Property

        Public ReadOnly Property TeamServersService() As TeamFoundation.Clients.TeamServersService
            Get
                Return Me.GetService(Of TeamFoundation.Clients.TeamServersService)()
            End Get
        End Property

        Public ReadOnly Property SubscriptionsService() As TeamFoundation.Clients.SubscriptionsService
            Get
                Return Me.GetService(Of TeamFoundation.Clients.SubscriptionsService)()
            End Get
        End Property

        Public ReadOnly Property ActiveDirectoryService() As ActiveDirectory.Clients.ActiveDirectoryService
            Get
                Return Me.GetService(Of ActiveDirectory.Clients.ActiveDirectoryService)()
            End Get
        End Property

    End Class


End Namespace