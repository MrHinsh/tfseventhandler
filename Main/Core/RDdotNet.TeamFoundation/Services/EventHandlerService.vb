Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services.DataContracts
'Imports microsoft.TeamFoundation
'Imports microsoft.TeamFoundation.Client
'Imports microsoft.TeamFoundation.Server

Namespace Services

    <ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)> _
    Public Class EventHandlerService
        Implements Contracts.IHandlers
        Implements Contracts.IEvents
        Implements IDisposable

        Public ReadOnly Property ServiceSettings() As Config.ServiceItemElement
            Get
                Return Config.SettingsSection.Instance.Services.Item(Me.GetType.Name)
            End Get
        End Property

        Public ReadOnly Property RepositorySettings() As Config.RepositoryItemElement
            Get
                Return Config.SettingsSection.Instance.Repository
            End Get
        End Property

        Public ReadOnly Property OperationContext() As OperationContext
            Get
                Return OperationContext.Current
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

#Region " IHandlerAdmin "

#Region " Bits "

        Public Enum XmlFiles
            Manifest
        End Enum

        Private Function GetObject(Of T As {New})(ByVal XmlFile As XmlFiles) As T
            Dim XmlFileLocation As String = RepositorySettings.LocalPath
            XmlFileLocation = System.IO.Path.Combine(XmlFileLocation, XmlFile.ToString & ".xml")
            Dim x As New CustomXmlSerializer()
            SyncLock x
                GetObject = New T
                If System.IO.File.Exists(XmlFileLocation) Then
                    GetObject = CType(x.ReadXml(XmlFileLocation, GetObject), T)
                Else
                    x.WriteFile(GetObject, XmlFileLocation, True)
                End If
            End SyncLock
            Return GetObject
        End Function

        Private Sub SetObject(Of T)(ByVal XmlFile As XmlFiles, ByVal target As T)
            Dim XmlFileLocation As String = RepositorySettings.LocalPath
            XmlFileLocation = System.IO.Path.Combine(XmlFileLocation, XmlFile.ToString & ".xml")
            Dim x As New CustomXmlSerializer()
            SyncLock x
                x.WriteFile(target, XmlFileLocation, True)
            End SyncLock
        End Sub

#End Region

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
            If ValidateAssembly(AssemblyItem) Then
                Dim AssemblyManaifest As AssemblyManaifest = Nothing
                AssemblyManaifest = GetObject(Of AssemblyManaifest)(XmlFiles.Manifest)
                AssemblyManaifest.Assemblys.Add(AssemblyItem)
                AssemblyManaifest.Version = AssemblyManaifest.Version + 1
                SetObject(Of AssemblyManaifest)(XmlFiles.Manifest, AssemblyManaifest)
                HandlerAdminCallback.Updated(AssemblyManaifest)
            End If
        End Sub

        Public Function GetAssemblys() As AssemblyManaifest Implements Contracts.IHandlers.GetAssemblys
            Return GetObject(Of AssemblyManaifest)(XmlFiles.Manifest)
        End Function

        Public Sub RemoveAssembly(ByVal ID As Integer) Implements Contracts.IHandlers.RemoveAssembly

        End Sub

        Public Sub AddAssemblyDirect(ByVal AssemblyBytes As Byte()) Implements Contracts.IHandlers.AddAssemblyDirect
            AddAssembly(GetAssemblyItem(AssemblyBytes))
        End Sub

        Public Function GetAssemblyItem(ByVal AssemblyBytes As Byte()) As AssemblyItem Implements Contracts.IHandlers.GetAssemblyItem
            Try
                '------------------
                Dim AssemblyItem As AssemblyItem = Nothing
                AssemblyItem = AssemblyHelper.GetAssemblyItem(Config.SettingsSection.Instance.Repository.LocalPath, AssemblyBytes)
                Return AssemblyItem
            Catch ex As Exception
                Throw New System.ServiceModel.FaultException(Of Exception)(ex, "An error occurerd. That Assembly may not be of the correct type.")
            End Try
        End Function

        Public Function ValidateAssembly(ByVal AssemblyItem As AssemblyItem) As Boolean Implements Contracts.IHandlers.ValidateAssembly
            If Not AssemblyItem.EventHandlers.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region " IEvent "

        Public Sub RaiseCheckinEvent(ByVal [Event] As CheckinEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseCheckinEvent

        End Sub

        Public Sub RaiseWorkItemChangedEvent(ByVal [Event] As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseWorkItemChangedEvent

        End Sub

        Public Sub RaiseUnknown(ByVal eventXml As String, ByVal tfsIdentityXml As String, ByVal SubscriptionInfo As SubscriptionInfo) Implements Contracts.IEvents.RaiseUnknown

        End Sub

#End Region


    End Class


End Namespace