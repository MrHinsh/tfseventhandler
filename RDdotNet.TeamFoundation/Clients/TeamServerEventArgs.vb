Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Clients


    Public Class TeamServerEventArgs
        Inherits EventArgs

        Private m_ChangeType As Services.DataContracts.StatusChangeTypeEnum = StatusChangeTypeEnum.Unknown
        Private m_TeamServer As TeamServerItem

        Public ReadOnly Property ChangeType() As Services.DataContracts.StatusChangeTypeEnum
            Get
                Return m_ChangeType
            End Get
        End Property

        Public ReadOnly Property TeamServer() As TeamServerItem
            Get
                Return m_TeamServer
            End Get
        End Property

        Public Sub New(ByVal ChangeType As Services.DataContracts.StatusChangeTypeEnum, ByVal TeamServer As TeamServerItem)
            m_ChangeType = ChangeType
            m_TeamServer = TeamServer
        End Sub

    End Class

End Namespace