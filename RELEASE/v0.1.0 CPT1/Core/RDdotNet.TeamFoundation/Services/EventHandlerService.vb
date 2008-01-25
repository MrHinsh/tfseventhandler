Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events.Handlers
Imports RDdotNet.TeamFoundation.Events

'Imports microsoft.TeamFoundation
'Imports microsoft.TeamFoundation.Client
'Imports microsoft.TeamFoundation.Server

Namespace Services

    <ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
    Public Class EventHandlerService
        Implements Contracts.IHandlers
        Implements Contracts.IEvents
        Implements IDisposable

        Public Sub New()
            Initilise()

        End Sub

        'Public ReadOnly Property ServiceSettings() As Config.ServiceItemElement
        '    Get
        '        Return Config.TeamFoundationSettingsSection.Instance.Services.Item(Me.GetType.Name)
        '    End Get
        'End Property

        'Public ReadOnly Property RepositorySettings() As Config.RepositoryItemElement
        '    Get
        '        Return Config.TeamFoundationSettingsSection.Instance.Repository
        '    End Get
        'End Property

        Public ReadOnly Property OperationContext() As OperationContext
            Get
                Return OperationContext.Current
            End Get
        End Property

        Public ReadOnly Property SettingsSection() As Config.TeamFoundationSettingsSection
            Get
                Return Config.TeamFoundationSettingsSection.Instance
            End Get
        End Property




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

#Region " Handler "

#Region " Handler Processing "

        Private m_AssemblyItems As New Collection(Of AssemblyItem)

        Private Sub Initilise()
           LoadFromConfig
        End Sub

        Private Sub LoadFromConfig()
            m_AssemblyItems.Clear()
            For Each AIE As AssemblyItemElement In SettingsSection.HandlerAssemblies
                Try
                    Dim AssemblyItem As AssemblyItem = AssemblyHelper.GetAssemblyItem(AIE)
                    If Not AssemblyItem Is Nothing Then
                        m_AssemblyItems.Add(AssemblyItem)
                    End If
                Catch ex As Exception
                    HandlerAdminCallback.ErrorOccured(New FaultException(Of Exception)(ex))
                End Try
            Next
        End Sub

#End Region

#Region "  IHandlers  "

        Private _HandlerAdminCallback As Contracts.IHandlersCallback

        Public ReadOnly Property HandlerAdminCallback() As Contracts.IHandlersCallback
            Get
                If _HandlerAdminCallback Is Nothing Then
                    _HandlerAdminCallback = OperationContext.GetCallbackChannel(Of Contracts.IHandlersCallback)()
                End If
                Return _HandlerAdminCallback
            End Get
        End Property

        Public Sub AddAssembly(ByVal AssemblyItem As AssemblyItem) Implements Contracts.IHandlers.AddAssembly

        End Sub

        Public Function GetAssemblys() As Collection(Of AssemblyItem) Implements Contracts.IHandlers.GetAssemblys
            Return m_AssemblyItems
        End Function

        Public Sub RemoveAssembly(ByVal AssemblyItem As AssemblyItem) Implements Contracts.IHandlers.RemoveAssembly

        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes As Byte()) Implements Contracts.IHandlers.AddAssemblyDirect

        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes As Byte()) As AssemblyItem Implements Contracts.IHandlers.GetAssemblyItem
            'Try
            '    '------------------
            '    Dim AssemblyItem As AssemblyItem = Nothing
            '    AssemblyItem = AssemblyHelper.GetAssemblyItem(Config.TeamFoundationSettingsSection.Instance.Repository.LocalPath, AssemblyBytes)
            '    Return AssemblyItem
            'Catch ex As Exception
            '    Throw New System.ServiceModel.FaultException(Of Exception)(ex, "An error occurerd. That Assembly may not be of the correct type.")
            'End Try
        End Function

        Public Function ValidateAssembly(ByVal AssemblyItem As AssemblyItem) As Boolean Implements Contracts.IHandlers.ValidateAssembly
            If Not AssemblyItem.EventHandlers.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#End Region

#Region " Event Processing "

        Friend Sub RunAllValidEventHandlers(Of TEventType As {New})(ByVal EventType As EventTypes, ByVal [Event] As TEventType, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo)
            Dim NotifyEventArgs As New Events.NotifyEventArgs(Of TEventType)(EventType, [Event], EventIdentity, SubscriptionInfo)
            For Each AssemblyItem As AssemblyItem In m_AssemblyItems
                If AssemblyItem.State = AssemblyItemStates.Valid Then
                    For Each EventHandler In AssemblyItem.EventHandlers
                        If EventHandler.EventType = EventType Then
                            Try
                                Dim x As AEventHandler(Of TEventType) = Activator.CreateInstance(EventHandler.HandlerType)
                                If x.IsValid(NotifyEventArgs) Then
                                    x.Run(NotifyEventArgs)
                                End If
                            Catch ex As Exception
                                'TODO: Some sort of error message
                            End Try
                        End If
                    Next
                End If
            Next
        End Sub

#Region " IEvents "

        Public Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseCheckinEvent
            RunAllValidEventHandlers(Of CheckinEvent)(Events.EventTypes.CheckinEvent, [Event], EventIdentity, SubscriptionInfo)
        End Sub

        Public Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseWorkItemChangedEvent
            RunAllValidEventHandlers(Of WorkItemChangedEvent)(Events.EventTypes.WorkItemChangedEvent, [Event], EventIdentity, SubscriptionInfo)
        End Sub

        Public Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseUnknown
            'TODO: Deal with unknown Events
        End Sub

#End Region

#End Region


    End Class


End Namespace