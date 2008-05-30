Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Clients


    Public Class StatusChangeEventArgs(Of TItem)
        Inherits EventArgs

        Private m_ChangeType As Services.DataContracts.StatusChangeTypeEnum = StatusChangeTypeEnum.Unknown
        Private m_Item As TItem

        Public ReadOnly Property ChangeType() As Services.DataContracts.StatusChangeTypeEnum
            Get
                Return m_ChangeType
            End Get
        End Property

        Public ReadOnly Property Item() As TItem
            Get
                Return m_Item
            End Get
        End Property

        Public Sub New(ByVal ChangeType As Services.DataContracts.StatusChangeTypeEnum, ByVal Item As TItem)
            m_ChangeType = ChangeType
            m_Item = Item
        End Sub

    End Class

End Namespace