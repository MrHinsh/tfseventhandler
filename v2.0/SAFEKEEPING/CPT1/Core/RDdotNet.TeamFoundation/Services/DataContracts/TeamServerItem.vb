Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Events.Handlers

Namespace Services.DataContracts


    <DataContract(), Serializable()> _
    Public Class TeamServerItem

        Public Sub New()

        End Sub

        Public Sub New(ByVal TFS As Microsoft.TeamFoundation.Client.TeamFoundationServer)

            m_TFS = TFS
            Name = TFS.Name
            Uri = TFS.Uri
            m_HasAuthenticated = TFS.HasAuthenticated
            'm_Credentials = TFS.Credentials
            m_Validated = True
        End Sub

        Public Sub New(ByVal Name As String, ByVal Uri As Uri, ByVal Credentials As TeamServerCredentials)
            MyBase.New()
            Me.Name = Name
            Me.Uri = Uri
            Me.Credentials = Credentials
        End Sub

        Public Sub New(ByVal Name As String, ByVal Uri As Uri)
            MyBase.New()
            Me.Name = Name
            Me.Uri = Uri
        End Sub

        Private m_Name As String
        Private m_Uri As Uri
        Private m_TFS As Microsoft.TeamFoundation.Client.TeamFoundationServer
        Private m_Credentials As TeamServerCredentials
        Private m_HasAuthenticated As Boolean = False
        <DataMember()> _
        Private m_Validated As Boolean = False

        Public ReadOnly Property TeamFoundationServer() As Microsoft.TeamFoundation.Client.TeamFoundationServer
            Get
                If m_TFS Is Nothing Then
                    If Credentials Is Nothing Then
                        m_TFS = New Microsoft.TeamFoundation.Client.TeamFoundationServer(Name)
                    Else
                        m_TFS = New Microsoft.TeamFoundation.Client.TeamFoundationServer(Name, New System.Net.NetworkCredential(Credentials.Username, Credentials.Password, Credentials.Domain))
                    End If
                End If
                Return m_TFS
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property


        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember()> _
        Public Property Uri() As Uri
            Get
                Return m_Uri
            End Get
            Set(ByVal value As Uri)
                m_Uri = value
            End Set
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember()> _
        Public Property Credentials() As TeamServerCredentials
            Get
                Return m_Credentials
            End Get
            Set(ByVal value As TeamServerCredentials)
                m_Credentials = value
            End Set
        End Property

        <DataMember()> _
Public Property HasAuthenticated() As Boolean
            Get
                Return m_HasAuthenticated
            End Get
            Set(ByVal value As Boolean)
                m_HasAuthenticated = value
                If value Then
                    m_Validated = value
                End If
            End Set
        End Property

        Public ReadOnly Property IsValid() As Boolean
            Get
                Return m_Validated
            End Get
        End Property

    End Class

    Public Class ServerItemElementCompair
        Inherits Collections.Generic.Comparer(Of ServerItemElement)

        Public Overrides Function Compare(ByVal x As ServerItemElement, ByVal y As ServerItemElement) As Integer
            If x.Uri Is y.Uri Then Return 1
        End Function

    End Class

    <DataContract(), Serializable()> _
    Public Enum StatusChangeTypeEnum
        <EnumMember()> Unknown
        <EnumMember()> ServerAdded
        <EnumMember()> ServerExists
        <EnumMember()> ServerRemoved
        <EnumMember()> ServerCheck
        <EnumMember()> ServerCheckStarted
        <EnumMember()> ServerCheckEnded
        <EnumMember()> ServerAuthenticated
        <EnumMember()> ServerAuthenticationFailed
    End Enum

End Namespace
