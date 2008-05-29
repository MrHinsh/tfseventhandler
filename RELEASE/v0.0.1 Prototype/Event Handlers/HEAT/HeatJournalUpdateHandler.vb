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
Public Class HeatJournalUpdateHandler
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
        ' Get Heat field value
        Dim HeatReference As StringField = Querys.GetHeatReference(e.Event)
        ' If there is no field then return false
        If HeatReference Is Nothing Then
            Return False
        End If
        ' If there is no heat value then exit
        Dim HasValue As Boolean = String.IsNullOrEmpty(HeatReference.OldValue) And String.IsNullOrEmpty(HeatReference.OldValue)
        Return HasValue

        '' If the head id has just been filleed out the return true
        'Dim IsNew As Boolean = String.IsNullOrEmpty(HeatReference.OldValue) And Not String.IsNullOrEmpty(HeatReference.OldValue)
        'If IsNew Then
        '    Return True
        'End If

        'If String.IsNullOrEmpty(HeatReference.OldValue) Then
        '    Return False
        'Else
        '    Return Not String.IsNullOrEmpty(HeatReference.OldValue)
        'End If
    End Function

    Public Sub Run(ByVal EventHandlerItem As EventHandlerItem(Of WorkItemChangedEvent), ByVal ServiceHost As ServiceHostItem, ByVal TeamServer As TeamServerItem, ByVal e As NotifyEventArgs(Of WorkItemChangedEvent)) Implements IEventHandler(Of WorkItemChangedEvent).Run
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("HeatJournalUpdateHandler: Running ")
        If Not IsValid(EventHandlerItem, ServiceHost, TeamServer, e) Then
            Return
        End If

        Dim toName As String = "Test Dudes" 'WorkItemEventQuerys.GetAssignedToName(e.Event)
        Dim toAddress As String = "Martin.hinshelwood@aggreko.co.uk;roddy.crossin@aggreko.co.uk" 'RDdotNet.ActiveDirectory.Querys.GetEmailAddress(toName)
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
            Dim Subject As String = "##PortfolioProject##:##WorkItemType## HeatJournalUpdateHandler - ##WorkItemID##: ##WorkItemTitle##"
            Dim x As New Mail(EventHandlerItem, TeamServer, e)
            x.SendMail("HeatJournalUpdate", [to], from, Subject, False)
        End If
        If TeamServer.ItemElement.LogEvents Then My.Application.Log.WriteEntry("AssignedToHandler: Complete ")
    End Sub


#End Region


End Class