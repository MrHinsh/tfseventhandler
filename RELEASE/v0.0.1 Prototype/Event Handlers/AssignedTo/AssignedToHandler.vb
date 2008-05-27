Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.io
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports RDdotNet.TeamFoundation.Helpers
Imports RDdotNet.TeamFoundation

''' <summary>
''' Send an email to a user who is assigned an email unless they are the one that assigned it to themselves
''' </summary>
''' <remarks></remarks>
Public Class AssignedToHandler
    Implements IEventHandler(Of WorkItemChangedEvent)

#Region " IEventHandler "

    '' <summary>
    '' Returns true if the event contains a assignment to a user other than the assigner
    '' but not if the changer and the assignee are the sdame person,
    '' and not if the old assignee and new assignee are the same.
    '' </summary>
    Public Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) As Boolean Implements IEventHandler(Of WorkItemChangedEvent).IsValid
        If e.Event Is Nothing Then
            Return False
        End If
        Dim assignedName As String = WorkItemEventQuerys.GetAssignedToName(e.Event)
        Dim ChangedByName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        If String.IsNullOrEmpty(assignedName) Then
            Return False
        Else
            Return Not assignedName = WorkItemEventQuerys.GetChangedByName(e.Event) _
                    And Not ChangedByName = Querys.GetAssignedToName(e.Event).OldValue _
                    And Not Querys.GetAssignedToName(e.Event).OldValue = Querys.GetAssignedToName(e.Event).NewValue
        End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If

        Dim toName As String = WorkItemEventQuerys.GetAssignedToName(e.Event)
        Dim toAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(toName)
        Dim fromName As String = WorkItemEventQuerys.GetChangedByName(e.Event)
        Dim fromAddress As String = RDdotNet.ActiveDirectory.Querys.GetEmailAddress(fromName)
        If String.IsNullOrEmpty(toAddress) Then
            'Logger.Log("Can't send email because no email address was found for " + toName)
        Else
            Dim [to] As New MailAddress(toAddress, toName)
            Dim from As New MailAddress(fromAddress, fromName)
            If TeamServer.ItemElement.TestMode Then
                [to] = New Net.Mail.MailAddress(TeamServer.ItemElement.TestEmail)
            End If
            Dim Subject As String = "##PortfolioProject##:##WorkItemType## Assigned - ##WorkItemID##: ##WorkItemTitle##"
            Dim x As New Mail(EventHandlerItem, TeamServer, e)
            x.SendMail(Mail.EmailTypes.AssignedTo, [to], from, Subject)
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Complete ")
    End Sub


#End Region

#Region " EmailSend "


    'Private Function GetReplaceomatic(ByVal EventObject As WorkItemChangedEvent) As Hashtable
    '    Dim replacers As New Hashtable
    '    replacers.Add("##ChangedByName##", WorkItemEventQuerys.GetChangedByName(EventObject))
    '    replacers.Add("##WorkItemID##", WorkItemEventQuerys.GetWorkItemID(EventObject))
    '    replacers.Add("##WorkItemType##", WorkItemEventQuerys.GetWorkItemType(EventObject))
    '    replacers.Add("##ChangedByEmail##", RDdotNet.ActiveDirectory.Querys.GetEmailAddress(CStr(replacers("##ChangedByName##"))))
    '    replacers.Add("##PortfolioProject##", EventObject.PortfolioProject)
    '    replacers.Add("##WorkItemTitle##", EventObject.WorkItemTitle)
    '    replacers.Add("##DisplayURL##", EventObject.DisplayUrl)
    '    Return replacers
    'End Function

    ' '' <summary>
    ' '' Retrieves email body based on XSL transform of XML event
    ' '' </summary>
    'Public Function GetBody(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal EventObject As WorkItemChangedEvent, ByVal EventIdentity As TFSIdentity, ByVal SubscriptionInfo As SubscriptionInfo) As String
    '    Try
    '        Dim replacers As Hashtable = GetReplaceomatic(EventObject)
    '        Dim TemplatePath As String = System.IO.Path.Combine(EventHandlerItem.ItemElement.AssemblyFileLocation, "AssignedTo.htm")
    '        Dim Template As String = System.IO.File.ReadAllText(TemplatePath)
    '        For Each x As String In replacers.Keys
    '            Template = Template.Replace(x, CStr(replacers(x)))
    '        Next
    '        Return Template
    '    Catch ex As Exception
    '        My.Application.Log.WriteException(ex, TraceEventType.Critical, "XSL Issues")
    '        Return ex.ToString
    '    End Try
    'End Function

    ' '' <summary>
    ' '' Sends email to assignee with details of the work item they've been assigned
    ' '' </summary>
    'Public Sub SendMail(ByVal [to] As Net.Mail.MailAddress, ByVal from As Net.Mail.MailAddress, ByVal Subject As String, ByVal Body As String, ByVal TeamServer As TeamServerItem)
    '    'Logger.Log("entering SendMail")
    '    Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage(New MailAddress(TeamServer.ItemElement.MailAddressFrom, TeamServer.ItemElement.MailFromName), [to])
    '    mail.Body = Body
    '    mail.IsBodyHtml = True
    '    ''Logger.Log(body)
    '    If Not String.IsNullOrEmpty(from.Address) Then
    '        mail.ReplyTo = from
    '    End If
    '    mail.Subject = Subject
    '    '-----------------
    '    Dim smtp As SmtpClient = New SmtpClient(TeamServer.ItemElement.MailServer)
    '    '-----------------
    '    'If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("SMTPPassword")) Then
    '    '    smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("SMTPUser"), ConfigurationManager.AppSettings("SMTPPassword"))
    '    'End If
    '    'Logger.Log("sending mail..")
    '    '-----------------
    '    Dim PathSafeSubhject As String = mail.Subject & ".htm"
    '    For Each x As Char In System.IO.Path.GetInvalidPathChars
    '        PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
    '    Next
    '    For Each x As Char In System.IO.Path.GetInvalidFileNameChars
    '        PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
    '    Next
    '    Dim PathsafeFile As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "Logs")
    '    PathsafeFile = System.IO.Path.Combine(PathsafeFile, PathSafeSubhject)
    '    '-----------------
    '    'System.IO.File.AppendAllText(PathsafeFile, mail.Body)
    '    smtp.Send(mail)
    'End Sub

#End Region





End Class