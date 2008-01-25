Imports RDdotNet.TeamFoundation.Clients

Public Class FormServerSettings

    Private Shared m_Instance As FormServerSettings
    Private _EventHandler As TFSEventHandlerClient

    Friend ReadOnly Property EventHandler() As TFSEventHandlerClient
        Get
            Return _EventHandler
        End Get
    End Property

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Shared Sub ShowSettings()
        m_Instance = New FormServerSettings
        ' Set variables here

        ' Show the form
        m_Instance.ShowDialog()
    End Sub

End Class