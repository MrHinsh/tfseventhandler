Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events

Namespace Clients

    Public Class RDdotNetServer
        Implements IDisposable

        Private _Uri As Uri = New Uri("http://" & System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).HostName & ":6661")
        Private _Services As New Collection(Of IService)

        Public ReadOnly Property Uri() As Uri
            Get
                Return _Uri
            End Get
        End Property

        Public Sub New(Optional ByVal Url As Uri = Nothing)
            If Not Url Is Nothing Then
                _Uri = Url
            End If
            ' Load all teh services
            LoadServices(_Services)
        End Sub

        Public Function Authenticated() As Boolean
            Throw New NotImplementedException
        End Function

        Public Function GetService(Of TService As IService)() As TService
            For Each service As IService In _Services
                Dim obj As Object = service
                If obj.GetType Is GetType(TService) Then
                    Return service
                End If
            Next
            Throw New NotImplementedException
        End Function

        Public Function GetService(ByVal Name As String) As IService
            For Each service As IService In _Services
                If service.ServiceName = Name Then
                    Return service
                End If
            Next
            Throw New InvalidOperationException
        End Function

        Protected Overridable Sub LoadServices(ByRef Services As Collection(Of IService))
            Services.Add(New EventsService(Uri))
            Services.Add(New HandlersService(Uri))
        End Sub

#Region " IDisposable "

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called

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