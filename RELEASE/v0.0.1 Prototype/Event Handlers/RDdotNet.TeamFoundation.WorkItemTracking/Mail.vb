Imports System.Net.Mail
Imports RDdotNet.TeamFoundation.Helpers

Public Class Mail

    Private m_EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent)
    Private m_TeamServer As TeamServerItem
    Private m_NotifyEvent As NotifyEventArgs(Of WorkItemChangedEvent)

    Public Sub New(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal TeamServer As TeamServerItem, ByVal NotifyEvent As NotifyEventArgs(Of WorkItemChangedEvent))
        m_EventHandlerItem = EventHandlerItem
        m_TeamServer = TeamServer
        m_NotifyEvent = NotifyEvent
    End Sub

    Private Function GetReplaceomatic() As Hashtable
        Dim replacers As New Hashtable
        '---------------------------
        replacers.Add("##ChangedByName##", WorkItemEventQuerys.GetChangedByName(m_NotifyEvent.Event))
        replacers.Add("##WorkItemID##", WorkItemEventQuerys.GetWorkItemID(m_NotifyEvent.Event))
        replacers.Add("##WorkItemType##", WorkItemEventQuerys.GetWorkItemType(m_NotifyEvent.Event))
        replacers.Add("##ChangedByEmail##", RDdotNet.ActiveDirectory.Querys.GetEmailAddress(CStr(replacers("##ChangedByName##"))))
        replacers.Add("##PortfolioProject##", m_NotifyEvent.Event.PortfolioProject)
        replacers.Add("##WorkItemTitle##", m_NotifyEvent.Event.WorkItemTitle)
        replacers.Add("##DisplayURL##", m_NotifyEvent.Event.DisplayUrl)
        replacers.Add("##WorkItemDescription##", m_NotifyEvent.Event.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindDescription)))
        replacers.Add("##AssignedToName##", WorkItemEventQuerys.GetAssignedToName(m_NotifyEvent.Event))
        '---------------------------
        replacers.Add("##TFSURL##", m_TeamServer.Subject.Uri.ToString)
        replacers.Add("##TFSName##", m_TeamServer.Subject.Name)
        '---------------------------
        replacers.Add("##RDdotNetStart##", "2007")
        replacers.Add("##RDdotNetEnd##", Now.Year.ToString)
        '---------------------------
        '---------------------------
        Return replacers
    End Function

    Private Function FindDescription(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.Description" Then Return True
    End Function

    Public Function PerformReplace(ByVal Source As String) As String
        Try
            Dim replacers As Hashtable = GetReplaceomatic()
            For Each x As String In replacers.Keys
                Source = Source.Replace(x, CStr(replacers(x)))
            Next
            Return Source
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "XSL Issues")
            Return ex.ToString
        End Try
    End Function

    '' <summary>
    '' Retrieves email body based on XSL transform of XML event
    '' </summary>
    Public Function GetBody(ByVal EmailType As EmailTypes) As String
        Return GetBody(EmailType.ToString)
    End Function

    '' <summary>
    '' Retrieves email body based on XSL transform of XML event
    '' </summary>
    Public Function GetInnerBody(ByVal EmailType As String) As String
        Try
            Dim x As New System.Text.RegularExpressions.Regex("<body\b[^>]*>(.*?)</body>", Text.RegularExpressions.RegexOptions.IgnoreCase)
            Return x.Match(GetBody(EmailType)).Value
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "GetInnerBody")
            Return ex.ToString
        End Try
    End Function

    '' <summary>
    '' Retrieves email body based on XSL transform of XML event
    '' </summary>
    Public Function GetBody(ByVal EmailType As String) As String
        Try
            Dim TemplatePath As String = System.IO.Path.Combine(m_EventHandlerItem.ItemElement.AssemblyFileLocation, EmailType.ToString & ".htm")
            Dim Template As String = System.IO.File.ReadAllText(TemplatePath)
            Return PerformReplace(Template)
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Critical, "XSL Issues")
            Return ex.ToString
        End Try
    End Function

    '' <summary>
    '' Sends email to assignee with details of the work item they've been assigned
    '' </summary>
    Public Sub SendMail(ByVal EmailType As EmailTypes, ByVal [to] As Net.Mail.MailAddress, ByVal from As Net.Mail.MailAddress, ByVal Subject As String)
        SendMail(EmailType.ToString, [to], from, Subject, False)
    End Sub

    '' <summary>
    '' Sends email to assignee with details of the work item they've been assigned
    '' </summary>
    Public Sub SendMail(ByVal EmailType As String, ByVal [to] As Net.Mail.MailAddress, ByVal from As Net.Mail.MailAddress, ByVal Subject As String, ByVal TypeIsBody As Boolean)
        'Logger.Log("entering SendMail")
        Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage(New MailAddress(m_TeamServer.ItemElement.MailAddressFrom, m_TeamServer.ItemElement.MailFromName), [to])
        If TypeIsBody Then
            mail.Body = EmailType
        Else
            mail.Body = GetBody(EmailType)
        End If
        mail.IsBodyHtml = True
        ''Logger.Log(body)
        If Not String.IsNullOrEmpty(from.Address) Then
            mail.ReplyTo = from
        End If
        mail.Subject = PerformReplace(Subject)
        '-----------------
        Dim smtp As SmtpClient = New SmtpClient(m_TeamServer.ItemElement.MailServer)
        '-----------------
        'If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("SMTPPassword")) Then
        '    smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("SMTPUser"), ConfigurationManager.AppSettings("SMTPPassword"))
        'End If
        'Logger.Log("sending mail..")
        '-----------------
        If m_TeamServer.ItemElement.LogEvents Then
            Dim PathSafeSubhject As String = Now.Ticks & "-" & mail.Subject & ".htm"
            For Each x As Char In System.IO.Path.GetInvalidPathChars
                PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
            Next
            For Each x As Char In System.IO.Path.GetInvalidFileNameChars
                PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
            Next
            Dim PathsafeFile As String = System.IO.Path.Combine(m_TeamServer.ItemElement.EventLogPath, "Logs")
            PathsafeFile = System.IO.Path.Combine(PathsafeFile, PathSafeSubhject)
            '-----------------
            System.IO.File.AppendAllText(PathsafeFile, mail.Body)
        End If
        smtp.Send(mail)
    End Sub

    Public Enum EmailTypes
        AssignedTo
        ReAssigned
        NotifyCreator
    End Enum

End Class
