Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel


Namespace Clients

    Public MustInherit Class RDdotNetServerBase
        Implements IDisposable

        Private _Uri As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")
        Private _ClientServices As New Collection(Of IClientService)
        Private _ClientServicesLoaded As Boolean = False

        Public ReadOnly Property Uri() As Uri
            Get
                Return _Uri
            End Get
        End Property

        Public Sub New(Optional ByVal Url As Uri = Nothing)
            If Not Url Is Nothing Then
                _Uri = Url
            End If
        End Sub

        Friend Sub SetUri(Optional ByVal Url As Uri = Nothing)
            If Not Url Is Nothing Then
                _Uri = Url
            End If
        End Sub

        Public Function Authenticated() As Boolean
            Throw New NotImplementedException
        End Function

        Public Function GetService(Of TService As IClientService)() As TService
            If Not _ClientServicesLoaded Then LoadServices()
            For Each service As TService In _ClientServices
                Dim obj As Object = service
                If obj.GetType Is GetType(TService) Then
                    Return service
                End If
            Next
            Throw New NotImplementedException
        End Function

        Public Function GetService(ByVal Name As String) As IClientService
            If Not _ClientServicesLoaded Then LoadServices()
            For Each service As IClientService In _ClientServices
                If service.ServiceName = Name Then
                    Return service
                End If
            Next
            Throw New InvalidOperationException
        End Function

        Private Sub LoadServices()
            OnServicesPreLoad(_ClientServices)
            OnServicesLoad(_ClientServices)
            OnServicesPostLoad(_ClientServices)
        End Sub

        Protected Overridable Sub OnServicesPreLoad(ByRef ClientServices As Collection(Of IClientService))

        End Sub

        Protected MustOverride Sub OnServicesLoad(ByRef ClientServices As Collection(Of IClientService))

        Protected Overridable Sub OnServicesPostLoad(ByRef ClientServices As Collection(Of IClientService))
            For Each ClientService As IClientService In ClientServices
                ClientService.Start()
            Next
        End Sub

        Private Sub UnloadServices()
            OnServicesUnload(_ClientServices)
        End Sub

        Protected MustOverride Sub OnServicesUnload(ByRef ClientServices As Collection(Of IClientService))

#Region " IDisposable "

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    UnloadServices()
                End If
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

#End Region

    End Class


End Namespace