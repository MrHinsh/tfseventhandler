﻿Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports RDdotNet.TeamFoundation
Imports Microsoft.TeamFoundation.Common
Imports Microsoft.TeamFoundation
Imports RDdotNet.TeamFoundation.Events
Imports RDdotNet.TeamFoundation.Events.Handlers
Imports RDdotNet.TeamFoundation.Services
Imports RDdotNet.TeamFoundation.Events.Querys

<EventHandler(EventTypes.WorkItemChangedEvent, "martin@hinshelwood.com", testMode:=True)> _
<MailSettings(mailserver:="", mailAddressFrom:="", mailfromname:="")> _
Public Class AssignedToHandler
    Inherits AEventHandler(Of WorkItemChangedEvent)

    Public Overrides Function IsValid(ByVal e As Events.NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean
        If e.Event Is Nothing Then
            Return False
        End If
        Dim assignedName As String = WorkItemEventQuerys.GetAssignedToName(e.Event)
        If String.IsNullOrEmpty(assignedName) Then
            Return False
        Else
            Return Not assignedName = WorkItemEventQuerys.GetChangedByName(e.Event)
        End If
    End Function

    Public Overrides Sub Run(ByVal e As Events.NotifyEventArgs(Of WorkItemChangedEvent))
        If Not IsValid(e) Then
            Return
        End If

        'Dim MailSettings As Config.MailItemElement = EventService.ServiceSettings.Mail
        'Dim TeamServer As New TeamFoundationServer(e.Identity.Url)
        'Dim GroupSecurityService As IGroupSecurityService = DirectCast(TeamServer.Subject.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
        'Dim CommonStructureService As ICommonStructureService = DirectCast(TeamServer.Subject.GetService(GetType(ICommonStructureService)), ICommonStructureService)

        Dim toName As String = WorkItemEventQuerys.GetAssignedToName(e.Event)
        Dim toAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(toName)
        Dim [to] As New MailAddress(toAddress, toName)
        Dim fromName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        Dim fromAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(fromName)
        Dim from As New MailAddress(fromAddress, fromName)
        If String.IsNullOrEmpty(toAddress) Then
            'Logger.Log("Can't send email because no email address was found for " + toName)
        Else
            If EventHandlerSettings.TestMode Then
                [to] = New Net.Mail.MailAddress(EventHandlerSettings.MailTo)
            End If
            Dim Subject As String = e.Event.Title
            Dim Body As String = GetBody(e)
            SendMail([to], from, Subject, Body)
        End If
    End Sub

    Public Enum EmailSendTypes
        AssignedTo
    End Enum


#Region " Extras "


    Private Function GetReplaceomatic(ByVal e As WorkItemChangedEvent) As Hashtable
        Dim replacers As New Hashtable
        replacers.Add("##ChangedByName##", WorkItemEventQuerys.GetChangedByName(e))
        replacers.Add("##WorkItemID##", WorkItemEventQuerys.GetWorkItemID(e))
        replacers.Add("##WorkItemType##", WorkItemEventQuerys.GetWorkItemType(e))
        replacers.Add("##ChangedByEmail##", RDdotNet.ActiveDirectory.Querys.GetEmailAddress(CStr(replacers("##ChangedByName##"))))
        replacers.Add("##PortfolioProject##", e.PortfolioProject)
        replacers.Add("##WorkItemTitle##", e.WorkItemTitle)
        replacers.Add("##DisplayURL##", e.DisplayUrl)
        Return replacers
    End Function

    '' <summary>
    '' Retrieves email body based on XSL transform of XML event
    '' </summary>
    Public Function GetBody(ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As String
        Try
            Dim replacers As Hashtable = GetReplaceomatic(e.Event)
            Dim TemplatePath As String = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly.Location, "AssignedTo.htm")

            Dim Template As String = System.IO.File.ReadAllText(TemplatePath)
            For Each x As String In replacers.Keys
                Template = Template.Replace(x, CStr(replacers(x)))
            Next
            Return Template
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "XSL Issues")
            Return ex.ToString
        End Try
    End Function

    '' <summary>
    '' Sends email to assignee with details of the work item they've been assigned
    '' </summary>
    Public Sub SendMail(ByVal [to] As Net.Mail.MailAddress, ByVal from As Net.Mail.MailAddress, ByVal Subject As String, ByVal Body As String)
        'Logger.Log("entering SendMail")
        Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage(New MailAddress(MailSettings.MailAddressFrom, MailSettings.MailFromName), [to])
        mail.Body = Body
        mail.IsBodyHtml = True
        ''Logger.Log(body)
        If Not String.IsNullOrEmpty(from.Address) Then
            mail.ReplyTo = from
        End If
        mail.Subject = Subject
        '-----------------
        Dim smtp As SmtpClient = New SmtpClient(MailSettings.MailServer)
        '-----------------
        'If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("SMTPPassword")) Then
        '    smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("SMTPUser"), ConfigurationManager.AppSettings("SMTPPassword"))
        'End If
        'Logger.Log("sending mail..")
        '-----------------
        Dim PathSafeSubhject As String = mail.Subject & ".htm"
        For Each x As Char In System.IO.Path.GetInvalidPathChars
            PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
        Next
        For Each x As Char In System.IO.Path.GetInvalidFileNameChars
            PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
        Next
        Dim PathsafeFile As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "Logs")
        PathsafeFile = System.IO.Path.Combine(PathsafeFile, PathSafeSubhject)
        '-----------------
        'System.IO.File.AppendAllText(PathsafeFile, mail.Body)
        smtp.Send(mail)
    End Sub

#End Region



End Class