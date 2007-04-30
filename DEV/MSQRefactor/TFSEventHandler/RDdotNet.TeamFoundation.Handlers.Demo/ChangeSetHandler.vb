'Imports System.Xml
'Imports System.Xml.Xsl
'Imports System.Xml.XPath
'Imports System.io
'Imports System.Text
'Imports System.Net.Mail
'Imports System.Configuration
'Imports System.Reflection
'Imports Microsoft.TeamFoundation.Client
'Imports MerrillLynch.TeamFoundation.EventResources
'Imports MerrillLynch.TeamFoundation
'Imports Microsoft.TeamFoundation.WorkItemTracking.Client
'Imports Microsoft.TeamFoundation.Common
'Imports Microsoft.TeamFoundation.Server
'Imports System.Collections.ObjectModel

'Public Class ChangeSetHandler
'    Inherits AEventHandler(Of CheckinEvent)


'    Public Enum EmailTeamplates
'        ChangeSet
'        PolicyFailerOverride
'    End Enum


'    Public Overrides Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of CheckinEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of CheckinEvent))
'        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
'            Return
'        End If
'        'Dim wistore As WorkItemStore = DirectCast(TeamServer.Subject.GetService(GetType(WorkItemStore)), WorkItemStore)
'        Dim CommonStructureService As ICommonStructureService = DirectCast(TeamServer.Subject.GetService(GetType(ICommonStructureService)), ICommonStructureService)
'        Dim GroupSecurityService As IGroupSecurityService = DirectCast(TeamServer.Subject.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
'        Dim project As ProjectInfo = CommonStructureService.GetProjectFromName(e.Event.TeamProject)
'        Dim users As Collection(Of String) = GetAllUsersToEmail(TeamServer.Subject, project.Name)
'        For Each user As String In users
'            Dim toName As String = user
'            Dim toAddress As String = MerrillLynch.ActiveDirectory.Querys.GetEmailAddress(user)
'            Dim [to] As New MailAddress(toAddress, toName)
'            Dim fromName As String = e.Event.Owner
'            Dim fromAddress As String = MerrillLynch.ActiveDirectory.Querys.GetEmailAddress(e.Event.Owner)
'            Dim memberInfo As Identity = GroupSecurityService.ReadIdentity(SearchFactor.AccountName, e.Event.Owner, QueryMembership.None)
'            fromAddress = MerrillLynch.ActiveDirectory.Querys.GetEmailAddress(memberInfo.DisplayName)
'            Dim from As New MailAddress(fromAddress, fromName)
'            If String.IsNullOrEmpty(toAddress) Then
'                'Logger.Log("Can't send email because no email address was found for " + toName)
'            Else
'                If TeamServer.ItemElement.TestMode Then
'                    [to] = New Net.Mail.MailAddress(TeamServer.ItemElement.TestEmail)
'                End If
'                Dim Subject As String = e.Event.Title
'                Dim Body As String = GetBody(EmailTeamplates.ChangeSet, EventHandlerItem, ServiceHost, TeamServer, e)
'                'TODO: SendMail([to], from, Subject, Body, TeamServer)
'            End If
'        Next
'    End Sub

'    Private Function GetReplaceomatic(ByVal EventObject As CheckinEvent) As Hashtable
'        Dim replacers As New Hashtable
'        'replacers.Add("##ChangedByName##", WorkItemEventQuerys.GetChangedByName(EventObject))
'        'replacers.Add("##WorkItemID##", WorkItemEventQuerys.GetWorkItemID(EventObject))
'        'replacers.Add("##WorkItemType##", WorkItemEventQuerys.GetWorkItemType(EventObject))
'        'replacers.Add("##ChangedByEmail##", MerrillLynch.ActiveDirectory.Querys.GetEmailAddress(CStr(replacers("##ChangedByName##"))))
'        'replacers.Add("##PortfolioProject##", EventObject.PortfolioProject)
'        'replacers.Add("##WorkItemTitle##", EventObject.WorkItemTitle)
'        'replacers.Add("##DisplayURL##", EventObject.DisplayUrl)
'        Return replacers
'    End Function

'    '' <summary>
'    '' Retrieves email body based on XSL transform of XML event
'    '' </summary>
'    Public Function GetBody(ByVal eTemplate As EmailTeamplates, ByVal EventHandlerItem As EventHandlerItem(Of CheckinEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of CheckinEvent)) As String
'        Try
'            Dim replacers As Hashtable = GetReplaceomatic(e.Event)
'            Dim TemplatePath As String = System.IO.Path.Combine(EventHandlerItem.ItemElement.AssemblyFileLocation, eTemplate.ToString & ".htm")
'            Dim Template As String = System.IO.File.ReadAllText(TemplatePath)
'            For Each x As String In replacers.Keys
'                Template = Template.Replace(x, CStr(replacers(x)))
'            Next
'            Return Template
'        Catch ex As Exception
'            My.Application.Log.WriteException(ex, TraceEventType.Critical, "XSL Issues")
'            Return ex.ToString
'        End Try
'    End Function

'    '' <summary>
'    '' Sends email to assignee with details of the work item they've been assigned
'    '' </summary>
'    Public Sub SendMail(ByVal [to] As Net.Mail.MailAddress, ByVal from As Net.Mail.MailAddress, ByVal Subject As String, ByVal Body As String, ByVal TeamServer As TeamServerItem)
'        'Logger.Log("entering SendMail")
'        Dim mail As Net.Mail.MailMessage = New Net.Mail.MailMessage(New MailAddress(TeamServer.ItemElement.MailAddressFrom, TeamServer.ItemElement.MailFromName), [to])
'        mail.Body = Body
'        mail.IsBodyHtml = True
'        ''Logger.Log(body)
'        If Not String.IsNullOrEmpty(from.Address) Then
'            mail.ReplyTo = from
'        End If
'        mail.Subject = Subject
'        '-----------------
'        Dim smtp As SmtpClient = New SmtpClient(TeamServer.ItemElement.MailServer)
'        '-----------------
'        'If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("SMTPPassword")) Then
'        '    smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("SMTPUser"), ConfigurationManager.AppSettings("SMTPPassword"))
'        'End If
'        'Logger.Log("sending mail..")
'        '-----------------
'        Dim PathSafeSubhject As String = mail.Subject & ".htm"
'        For Each x As Char In System.IO.Path.GetInvalidPathChars
'            PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
'        Next
'        For Each x As Char In System.IO.Path.GetInvalidFileNameChars
'            PathSafeSubhject = PathSafeSubhject.Replace(x, CChar("_"))
'        Next
'        Dim PathsafeFile As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "Logs")
'        PathsafeFile = System.IO.Path.Combine(PathsafeFile, PathSafeSubhject)
'        '-----------------
'        'System.IO.File.AppendAllText(PathsafeFile, mail.Body)
'        smtp.Send(mail)
'    End Sub

'    '' <summary>
'    '' Returns true if the event contains a new assignment to a user other than the assigner
'    '' </summary>
'    Public Overrides Function IsValid(ByVal EventHandlerItem As EventHandlerItem(Of CheckinEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of CheckinEvent)) As Boolean
'        If e.Event Is Nothing Then
'            Return False
'        End If
'        Return True
'    End Function


'    ' <summary>
'    '   Gets a collection of TFS Users along with their roles.
'    ' </summary>
'    ' <param name="projectName">Name of the team project</param>
'    ' <returns>Returns a collection of TFSUser structure. It has tfsUserName and associated TFS roles</returns>
'    Public Function GetAllUsersToEmail(ByVal TFS As TeamFoundationServer, ByVal projectName As String) As Collection(Of String)
'        Dim users As New Collection(Of String)
'        Dim CommonStructureService As ICommonStructureService = DirectCast(TFS.GetService(GetType(ICommonStructureService)), ICommonStructureService)
'        Dim GroupSecurityService As IGroupSecurityService = DirectCast(TFS.GetService(GetType(IGroupSecurityService)), IGroupSecurityService)
'        Dim projInfo As ProjectInfo = CommonStructureService.GetProjectFromName(projectName)
'        Dim appGroups As Identity() = GroupSecurityService.ListApplicationGroups(projInfo.Uri)
'        For Each Group As Identity In appGroups
'            RecurseIdentity(Group, users, GroupSecurityService)
'        Next
'        Return users
'    End Function

'    Public Sub RecurseIdentity(ByVal Identity As Identity, ByRef users As Collection(Of String), ByVal GSS As IGroupSecurityService)
'        Dim IdentityMembers() As Identity = GSS.ReadIdentities(SearchFactor.Sid, New String() {Identity.Sid}, QueryMembership.Expanded)
'        For Each member As Identity In IdentityMembers
'            Select Case member.Type
'                Case IdentityType.WindowsUser
'                    If Not users.Contains(member.DisplayName) Then users.Add(member.DisplayName)
'                Case IdentityType.ApplicationGroup, IdentityType.WindowsGroup
'                    If Not member.Members Is Nothing Then
'                        For Each memberSid As String In member.Members
'                            Dim memberInfo As Identity = GSS.ReadIdentity(SearchFactor.Sid, memberSid, QueryMembership.None)
'                            Select Case memberInfo.Type
'                                Case IdentityType.WindowsUser
'                                    If Not users.Contains(memberInfo.DisplayName) Then users.Add(memberInfo.DisplayName)
'                                Case IdentityType.ApplicationGroup, IdentityType.WindowsGroup
'                                    RecurseIdentity(memberInfo, users, GSS)
'                            End Select
'                        Next
'                    End If
'            End Select
'        Next
'        Return
'    End Sub

'End Class