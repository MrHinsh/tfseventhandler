Imports Hinshelwood.TeamFoundation.Config

Public Class TeamServerViewModel
    Inherits SettingsViewModel

    Private m_TeamServerItem As TeamServerItemElement
    Private m_Name As String
    Private m_Url As Uri
    Private m_Subscriber As String
    Private m_MailAddressFrom As String
    Private m_MailFromName As String
    Private m_MailServer As String
    Private m_LogEvents As Boolean
    Private m_TestMode As Boolean
    Private m_TestEmail As String
    Private m_EventLogPath As String

    Public Property Name() As String
        Get
            Return m_TeamServerItem.Name
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.Name) Then
                m_TeamServerItem.Name = value
                OnPropertyChanged("Name")
            End If
        End Set
    End Property

    Public Property Url() As Uri
        Get
            Return m_TeamServerItem.Url
        End Get
        Set(ByVal value As Uri)
            If Not value.Equals(m_TeamServerItem.Url) Then
                m_TeamServerItem.Url = value
                OnPropertyChanged("Url")
            End If
        End Set
    End Property

    Public Property Subscriber() As String
        Get
            Return m_TeamServerItem.Subscriber
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.Subscriber) Then
                m_TeamServerItem.Subscriber = value
                OnPropertyChanged("Subscriber")
            End If
        End Set
    End Property

    Public Property MailAddressFrom() As String
        Get
            Return m_TeamServerItem.MailAddressFrom
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.MailAddressFrom) Then
                m_TeamServerItem.MailAddressFrom = value
                OnPropertyChanged("MailAddressFrom")
            End If
        End Set
    End Property

    Public Property MailFromName() As String
        Get
            Return m_TeamServerItem.MailFromName
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.MailFromName) Then
                m_TeamServerItem.MailFromName = value
                OnPropertyChanged("MailFromName")
            End If
        End Set
    End Property

    Public Property MailServer() As String
        Get
            Return m_TeamServerItem.MailServer
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.MailServer) Then
                m_TeamServerItem.MailServer = value
                OnPropertyChanged("MailServer")
            End If
        End Set
    End Property

    Public Property LogEvents() As Boolean
        Get
            Return m_TeamServerItem.LogEvents
        End Get
        Set(ByVal value As Boolean)
            If Not value.Equals(m_TeamServerItem.LogEvents) Then
                m_TeamServerItem.LogEvents = value
                OnPropertyChanged("LogEvents")
            End If
        End Set
    End Property

    Public Property TestMode() As Boolean
        Get
            Return m_TeamServerItem.TestMode
        End Get
        Set(ByVal value As Boolean)
            If Not value.Equals(m_TeamServerItem.TestMode) Then
                m_TeamServerItem.TestMode = value
                OnPropertyChanged("TestMode")
            End If
        End Set
    End Property


    Public Property TestEmail() As String
        Get
            Return m_TeamServerItem.TestEmail
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.TestEmail) Then
                m_TeamServerItem.TestEmail = value
                OnPropertyChanged("TestEmail")
            End If
        End Set
    End Property

    Public Property EventLogPath() As String
        Get
            Return m_TeamServerItem.EventLogPath
        End Get
        Set(ByVal value As String)
            If Not value.Equals(m_TeamServerItem.EventLogPath) Then
                m_TeamServerItem.EventLogPath = value
                OnPropertyChanged("EventLogPath")
            End If
        End Set
    End Property

    Public Sub New(ByVal teamServer As TeamServerItemElement)
        MyBase.New(New HeaderViewModel(teamServer.Name))
        m_TeamServerItem = teamServer

    End Sub

End Class
