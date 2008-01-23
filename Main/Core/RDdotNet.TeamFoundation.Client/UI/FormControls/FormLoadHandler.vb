Imports RDdotNet.TeamFoundation.Clients

Public Class FormLoadHandler

    Private m_SelectedFile As String
    Private m_Eventhandler As TFSEventHandlerClient

    Public ReadOnly Property SelectedFile() As String
        Get
            Return m_SelectedFile
        End Get
    End Property

    Public Sub New(ByVal Eventhandler As TFSEventHandlerClient)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_Eventhandler = Eventhandler
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.ux_OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            m_SelectedFile = ux_OpenFileDialog.FileName

            Dim bites() As Byte = System.IO.File.ReadAllBytes(m_SelectedFile)

            m_Eventhandler.AddAssemblyDirect(bites)

        End If
    End Sub

End Class