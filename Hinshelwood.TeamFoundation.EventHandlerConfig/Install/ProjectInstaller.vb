Imports System.ComponentModel
Imports System.IO
Imports System.Reflection

<RunInstaller(True)> _
Public Class ProjectInstaller
    Inherits System.Configuration.Install.Installer


    ' Override the 'Install' method.
    Public Overloads Overrides Sub Install(ByVal savedState As IDictionary)
        MyBase.Install(savedState)
    End Sub

    ' Override the 'Commit' method.
    Public Overloads Overrides Sub Commit(ByVal savedState As IDictionary)
        MyBase.Commit(savedState)
    End Sub

    ' Override the 'Rollback' method.
    Public Overloads Overrides Sub Rollback(ByVal savedState As IDictionary)
        MyBase.Rollback(savedState)
    End Sub

    Private Sub ProjectInstaller_Committed(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.Committed
        Try
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\EventHandlerConfig.exe")
        Catch

        End Try

    End Sub
End Class
