﻿Imports System.Net.Mail
Imports Hinshelwood.TeamFoundation.Helpers
Imports System.Text.RegularExpressions
Imports Hinshelwood.TeamFoundation
Imports Microsoft.TeamFoundation.Server

Public Class UserNotificationService

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
        replacers.Add("##ChangedByEmail##", Hinshelwood.ActiveDirectory.Querys.GetEmailAddress(CStr(replacers("##ChangedByName##"))))
        replacers.Add("##PortfolioProject##", m_NotifyEvent.Event.PortfolioProject)
        replacers.Add("##WorkItemTitle##", m_NotifyEvent.Event.WorkItemTitle)
        replacers.Add("##DisplayURL##", m_NotifyEvent.Event.DisplayUrl)
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

    Private m_replacers As Hashtable

    Private Function FindDescription(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.Description" Then Return True
    End Function

    Public Function PerformReplace(ByVal Source As String) As String
        Try
            If m_replacers Is Nothing Then
                m_replacers = GetReplaceomatic()
            End If
            Dim x As New System.Text.RegularExpressions.Regex("##(.*?)##")
            For Each y As Match In x.Matches(Source)
                If m_replacers.ContainsKey(y.Value) Then
                    Try
                        Source = Source.Replace(y.Value, m_replacers(y.Value).ToString)
                    Catch ex As Exception
                        Source = Source.Replace(y.Value, String.Format("?{0} Can't replace?", y.Value))
                    End Try

                Else
                    ' Not yet implemented
                    Source = Source.Replace(y.Value, String.Format("?{0} is not implemented?", y.Value))
                End If
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
    Public Sub SendNotification(ByVal EmailType As String, ByVal [to] As Identity, ByVal from As Identity, ByVal Subject As String)
        SendNotification(EmailType.ToString, [to], from, Subject, False)
        '  If TeamServer.ItemElement.TestMode Then
        '[to] = New Net.Mail.MailAddress(TeamServer.ItemElement.TestEmail)
        '    End If
    End Sub

    '' <summary>
    '' Sends email to assignee with details of the work item they've been assigned
    '' </summary>
    Public Sub SendNotification(ByVal EmailType As String, ByVal toIdentity As Identity, ByVal fromIdentity As Identity, ByVal Subject As String, ByVal TypeIsBody As Boolean)
        'Logger.Log("entering SendMail")
        Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage
        ' add from address
        mail.From = New MailAddress(m_TeamServer.ItemElement.MailAddressFrom, m_TeamServer.ItemElement.MailFromName)
        ' Add to addresses
        If toIdentity.SecurityGroup Then
            ' Add all to's
            Dim toIdentitiesLessFrom = From i In toIdentity.Members Where Not i = fromIdentity.Sid
            If toIdentitiesLessFrom.Count = 0 Then
                ' Ifg there is nobody to send an email to, then exit.
                Exit Sub
            End If
            Dim toIdentities() As Identity = m_TeamServer.GroupSecurityService.ReadIdentities(SearchFactor.Sid, toIdentitiesLessFrom.ToArray, QueryMembership.None)
            For Each i In toIdentities
                If m_TeamServer.ItemElement.TestMode Then
                    ' add fake to
                    mail.To.Add(New MailAddress(m_TeamServer.ItemElement.TestEmail, i.DisplayName))
                Else
                    ' add to
                    mail.To.Add(New MailAddress(i.MailAddress, i.DisplayName))
                End If
            Next
        Else
            If m_TeamServer.ItemElement.TestMode Then
                ' add fake to
                mail.To.Add(New MailAddress(m_TeamServer.ItemElement.TestEmail, toIdentity.DisplayName))
            Else
                ' add to
                mail.To.Add(New MailAddress(toIdentity.MailAddress, toIdentity.DisplayName))
            End If
        End If
        ' Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage(New MailAddress(m_TeamServer.ItemElement.MailAddressFrom, m_TeamServer.ItemElement.MailFromName), [to])

        If TypeIsBody Then
            mail.Body = EmailType
        Else
            mail.Body = GetBody(EmailType)
        End If

        mail.IsBodyHtml = True
        ''Logger.Log(body)
        If fromIdentity.SecurityGroup Then
            ' Add all to's
            Dim toIdentitiesLessFrom = From i In toIdentity.Members Where Not i = fromIdentity.Sid
            If toIdentitiesLessFrom.Count > 0 Then
                ' Ifg there is nobody to send an email to, then exit.
                Dim toIdentities() As Identity = m_TeamServer.GroupSecurityService.ReadIdentities(SearchFactor.Sid, toIdentitiesLessFrom.ToArray, QueryMembership.None)
                Dim x As New System.Text.StringBuilder
                For Each i In toIdentities
                    If m_TeamServer.ItemElement.TestMode Then
                        ' add fake reply to
                        x.AppendFormat("{0};", (New MailAddress(m_TeamServer.ItemElement.TestEmail, i.DisplayName).ToString))
                    Else
                        ' add reply to
                        x.AppendFormat("{0};", (New MailAddress(i.MailAddress, i.DisplayName).ToString))
                    End If
                Next
                mail.ReplyTo = New MailAddress(x.ToString)
            End If
        Else
            If Not String.IsNullOrEmpty(fromIdentity.MailAddress) Then 'TODO: Add reply to multi
                mail.ReplyTo = New MailAddress(fromIdentity.MailAddress)
            End If
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
            Try

                Dim Filename As String = String.Format("{0}{1}.htm", EmailType, Now.Ticks)
                'For Each x As Char In System.IO.Path.GetInvalidPathChars
                '    PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
                'Next
                'For Each x As Char In System.IO.Path.GetInvalidFileNameChars
                '    PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
                'Next
                Dim EventPath As String = System.IO.Path.Combine(m_TeamServer.ItemElement.EventLogPath, m_NotifyEvent.EventID.ToString)
                System.IO.Directory.CreateDirectory(EventPath)
                Dim FullPath As String = System.IO.Path.Combine(EventPath, Filename)
                '-----------------
                System.IO.File.AppendAllText(FullPath, mail.Body)

            Catch ex As Exception
                My.Application.Log.WriteException(ex, TraceEventType.Critical, "Email Log Issues")
            End Try
           
        End If
        smtp.Send(mail)
    End Sub

End Class
