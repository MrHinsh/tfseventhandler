Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.Clients

Namespace ActiveDirectory.Clients

    Public Class ActiveDirectoryService
        Implements IClientService

        Private _Server As Servers.IServer

        Public Sub New(ByVal Server As Servers.IServer)
            _Server = Server
        End Sub

        Public ReadOnly Property Server() As Servers.IServer Implements IClientService.Server
            Get
                Return Me._Server
            End Get
        End Property

        Public Function Authenticated() As Boolean Implements IClientService.Authenticated
            Return True
        End Function

        Public ReadOnly Property Contracts() As System.Collections.ObjectModel.Collection(Of System.Type) Implements IClientService.Contracts
            Get
                Dim ServiceContracts As New Collection(Of Type)
                For Each t As Type In Me.GetType.GetInterfaces
                    If t.IsInterface And t.GetCustomAttributes(GetType(RDdotNetServiceContractAttribute), True).Length > 0 Then
                        ServiceContracts.Add(t)
                    End If
                Next
                Return ServiceContracts
            End Get
        End Property

        Public ReadOnly Property ServiceName() As String Implements IClientService.ServiceName
            Get
                Return Me.GetType.Name
            End Get
        End Property

        Public ReadOnly Property ServiceType() As ClientServiceTypes Implements IClientService.ServiceType
            Get
                Return ClientServiceTypes.Local
            End Get
        End Property

        Public Sub Start() Implements IClientService.Start

        End Sub

        Public Sub [Stop]() Implements IClientService.Stop

        End Sub

        '' <summary>
        '' Retrieves a user's email address from Active Directory based on their display name
        '' </summary>
        Public Function GetEmailAddress(ByVal userDisplayName As String) As String
            Dim ds As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher()
            ds.PropertiesToLoad.Add("mail")
            ds.Filter = String.Format("(&(displayName={0})(objectCategory=person)((objectClass=user)))", userDisplayName)

            Dim results As DirectoryServices.SearchResultCollection = ds.FindAll()
            If results.Count = 0 Then
                Return String.Empty
            End If
            Dim values As DirectoryServices.ResultPropertyValueCollection = results(0).Properties("mail")
            If values.Count = 0 Then
                Return String.Empty
            End If
            Return values(0).ToString()
        End Function


      
    End Class

End Namespace
