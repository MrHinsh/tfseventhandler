Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports merrilllynch.TeamFoundation
Imports merrilllynch.TeamFoundation.Config
Imports microsoft.TeamFoundation
Imports microsoft.TeamFoundation.Client

<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
Public MustInherit Class AEventService(Of TEvent As {New})
    Implements MerrillLynch.TeamFoundation.INotification
    Implements MerrillLynch.TeamFoundation.INotificationAdmin
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
    Public MustOverride Sub OnEventRecieved(ByVal sender As Microsoft.TeamFoundation.Client.TeamFoundationServer, ByVal e As NotifyEventArgs(Of TEvent))

#Region " INotification "

    Public Sub Notify(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo) Implements INotification.Notify
        Dim IdentityObject As TFSIdentity = EndpointBase.CreateInstance(Of TFSIdentity)(tfsIdentityXml)
        Dim EventObject As TEvent = EndpointBase.CreateInstance(Of TEvent)(eventXml)
        Dim EventType As EventTypes = CType([Enum].Parse(GetType(EventTypes), GetType(TEvent).Name), EventTypes)
        Dim NotifyEventArgs As New NotifyEventArgs(Of TEvent)(EventType, EventObject, IdentityObject, SubscriptionInfo)
        '------------------------
        If Me.ServiceSettings.Debug.Verbose Then My.Application.Log.WriteEntry(String.Format("Event of type {0} recieven and assigned id of {1}:", EventType.ToString, NotifyEventArgs.EventID.ToString))
        '------------------------
        Dim ServerName As String = RegisteredServers.GetServerForUri(New Uri(IdentityObject.Url))
        Dim tfs As TeamFoundationServer = TeamFoundationServerFactory.GetServer(ServerName)
        OnEventRecieved(tfs, NotifyEventArgs)
    End Sub

#End Region

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

#Region " INotificationAdmin "

    Public Function GetEventType() As EventTypes Implements INotificationAdmin.GetEventType
        Return CType([Enum].Parse(GetType(EventTypes), GetType(TEvent).Name), EventTypes)
    End Function

    Public Function GetLocal() As String Implements INotificationAdmin.GetLocal
        Return Me.OperationContext.EndpointDispatcher.EndpointAddress.Uri.LocalPath
    End Function

#End Region


   
End Class
