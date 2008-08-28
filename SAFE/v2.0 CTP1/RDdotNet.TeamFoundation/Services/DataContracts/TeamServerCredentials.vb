Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Events.Handlers

Namespace Services.DataContracts


    <DataContract(), Serializable()> _
    Public Class TeamServerCredentials


        Private m_Username As String
        Private m_Password As String
        Private m_Domain As String

        <DataMember()> _
        Public Property Username() As String
            Get
                Return m_Username
            End Get
            Set(ByVal value As String)
                m_Username = value
            End Set
        End Property

        <DataMember()> _
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(ByVal value As String)
                m_Password = value
            End Set
        End Property

        <DataMember()> _
        Public Property Domain() As String
            Get
                Return m_Domain
            End Get
            Set(ByVal value As String)
                m_Domain = value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal Username As String, ByVal Password As String, ByVal Domain As String)
            m_Username = Username
            m_password = Password
            m_Domain = Domain
        End Sub

        Public Function GetCredential() As System.Net.NetworkCredential
            Return New System.Net.NetworkCredential(m_Username, m_Password, m_Domain)
        End Function

    End Class

End Namespace
