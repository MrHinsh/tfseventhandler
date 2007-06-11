Imports System.Collections.Generic
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports RDdotNet.TeamFoundation.Clients


Public Class Form1

#Region " Form1 "


#End Region


    Private Sub uxToolStripButtonAuthCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uxToolStripButtonAuthCheck.Click
        Dim DispForm As String = "{1}{0}Username: {2}{0}Password: {3}{0}Doamin: {4}{0}{0}"
        Dim x As System.Net.NetworkCredential
        Dim s As New System.Text.StringBuilder
        '-----------------
        Dim id As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent
        Dim p As System.Security.Principal.WindowsPrincipal = New System.Security.Principal.WindowsPrincipal(id)
        s.AppendFormat(DispForm, vbCrLf, "WindowsPrincipal", p.Identity.Name, "****", "****")
        id.Impersonate()
        '-----------------
        x = CType(System.Net.CredentialCache.DefaultCredentials, Net.NetworkCredential)
        s.AppendFormat(DispForm, vbCrLf, "DefaultCredentials", x.UserName, x.Password, x.Domain)
        '-----------------
        x = System.Net.CredentialCache.DefaultNetworkCredentials
        s.AppendFormat(DispForm, vbCrLf, "DefaultNetworkCredentials", x.UserName, x.Password, x.Domain)
        '-----------------

        '-----------------
        MsgBox(s.ToString)
    End Sub

End Class
