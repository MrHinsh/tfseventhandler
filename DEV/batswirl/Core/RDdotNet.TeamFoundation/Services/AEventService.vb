Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client

Namespace Services

    Public MustInherit Class AEventService(Of TEvent As {New})
        Implements IDisposable


        Public ReadOnly Property ServiceSettings() As Config.ServiceItemElement
            Get
                Return Config.SettingsSection.Instance.Services.Item(Me.GetType.Name)
            End Get
        End Property

        Public ReadOnly Property OperationContext() As OperationContext
            Get
                Return OperationContext.Current
            End Get
        End Property

        Public Sub New()
            OnStart()
        End Sub

        Public MustOverride Sub OnStart()
        Public MustOverride Sub OnEnd()
        Public MustOverride Sub Run(ByVal sender As Microsoft.TeamFoundation.Client.TeamFoundationServer, ByVal e As NotifyEventArgs(Of TEvent))
        Public MustOverride Sub IsValid(ByVal sender As Microsoft.TeamFoundation.Client.TeamFoundationServer, ByVal e As NotifyEventArgs(Of TEvent))

#Region " IDisposable "

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    OnEnd()
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