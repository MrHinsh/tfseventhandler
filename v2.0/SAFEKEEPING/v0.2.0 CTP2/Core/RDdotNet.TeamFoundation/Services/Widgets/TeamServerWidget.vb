Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Widgets

    Public Class TeamServerWidget
        Inherits ItemElementWidget(Of TeamServerItem, String, Config.ServerItemElement)

#Region " Public Overloads "

        Public Overrides Sub Add(ByVal TeamServer As TeamServerItem)
            Try
                InnerAdd(TeamServer)
            Catch ex As System.Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Connection to TFS server unsucessfull")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to add team server", New FaultCode("TFS:EH:TS:0001")))
            End Try
        End Sub

        Public Overrides Sub Remove(ByVal TeamServer As TeamServerItem)
            Try
                InnerRemove(TeamServer)
            Catch ex As System.ServiceModel.FaultException
                My.Application.Log.WriteException(ex, TraceEventType.Error, "disconnection to TFS server unsucessfull")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to remove team server", New FaultCode("TFS:EH:TS:0003")))
            End Try
        End Sub

        Public Overrides Sub Refresh()
            Try
                InnerRefresh()
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to refresh team servers")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to refresh team servers", New FaultCode("TFS:EH:TS:0002")))
            End Try
        End Sub

        Public Overloads Overrides Sub Refresh(ByVal Item As DataContracts.TeamServerItem)
            Try
                InnerRefresh(Item)
            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Error, "Failed to refresh team servers")
                OnErrorOccured(New FaultException(Of System.Exception)(ex, "Failed to refresh team servers", New FaultCode("TFS:EH:TS:0002")))
            End Try
        End Sub

        Public Overrides Function Find(ByVal SearchTerm As String) As DataContracts.TeamServerItem
            Dim exTSI = (From tsi As TeamServerItem In Items Where tsi.Name = SearchTerm).SingleOrDefault
            Return exTSI
        End Function

        Public Overrides Function Exists(ByVal TeamServer As DataContracts.TeamServerItem) As Boolean
            Return Not Find(TeamServer.Name) Is Nothing
        End Function

#End Region

#Region " Protected Overloads "

        Protected Overloads Overrides Function Convert(ByVal source As Config.ServerItemElement) As DataContracts.TeamServerItem
            Dim tsc As TeamServerCredentials = Nothing
            If Not source.Credentials Is Nothing AndAlso source.Credentials.Username <> "" Then
                tsc = New TeamServerCredentials(source.Credentials.Username, source.Credentials.Password, source.Credentials.Domain)
            End If
            Return New TeamServerItem(source.Name, source.Uri, tsc)
        End Function

        Protected Overloads Overrides Function Convert(ByVal source As DataContracts.TeamServerItem) As Config.ServerItemElement
            Dim NewItem As ServerItemElement = Config.TeamFoundationSettingsSection.Instance.Servers.CreateNew()
            NewItem.Name = source.Name
            NewItem.Uri = source.Uri
            If Not source.Credentials Is Nothing Then
                NewItem.Credentials = New CredentialsItemElement
                NewItem.Credentials.Username = source.Credentials.Username
                NewItem.Credentials.Password = source.Credentials.Password
                NewItem.Credentials.Domain = source.Credentials.Domain
            End If
            Return NewItem
        End Function

        Protected Overrides Function GetItemElements(Optional ByVal initilise As Object = Nothing) As Collection(Of ServerItemElement)
            Dim ServerItemElements As New Collection(Of Config.ServerItemElement)
            For Each ServerItemElement As ServerItemElement In Config.TeamFoundationSettingsSection.Instance.Servers
                ServerItemElements.Add(ServerItemElement)
            Next
            Return ServerItemElements
        End Function

        Protected Overrides Sub SetItemElements(ByVal ItemElements As Collection(Of Config.ServerItemElement))
            TeamFoundationSettingsSection.Instance.Servers.Clear()
            For Each ItemElement As Config.ServerItemElement In ItemElements
                TeamFoundationSettingsSection.Instance.Servers.Add(ItemElement)
            Next
            TeamFoundationSettingsSection.Save()
        End Sub



        Protected Overrides Function ItemCheck(ByVal Item As DataContracts.TeamServerItem) As Boolean
            Try
                Item.TeamFoundationServer.Authenticate()
                Item.HasAuthenticated = True
                Return True
            Catch ex As Exception
                OnErrorOccured(ex)
                Return False
            End Try
        End Function

#End Region




    End Class

End Namespace