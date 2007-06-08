Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel


Namespace Clients

    Public MustInherit Class RDdotNetServerBase
        Implements IDisposable

        Private _Uri As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")
        Private _Services As New Collection(Of IService)
        Private _ServicesLoaded As Boolean = False

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

        Public Function GetService(Of TService As IService)() As TService
            If Not _ServicesLoaded Then LoadServices()
            For Each service As TService In _Services
                Dim obj As Object = service
                If obj.GetType Is GetType(TService) Then
                    Return service
                End If
            Next
            Throw New NotImplementedException
        End Function

        Public Function GetService(ByVal Name As String) As IService
            If Not _ServicesLoaded Then LoadServices()
            For Each service As IService In _Services
                If service.ServiceName = Name Then
                    Return service
                End If
            Next
            Throw New InvalidOperationException
        End Function

        Private Sub LoadServices()
            LoadServices(_Services)
        End Sub

        Protected MustOverride Sub LoadServices(ByRef Services As Collection(Of IService))

        Private Sub UnloadServices()
            UnloadServices(_Services)
        End Sub

        Protected MustOverride Sub UnloadServices(ByRef Services As Collection(Of IService))

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