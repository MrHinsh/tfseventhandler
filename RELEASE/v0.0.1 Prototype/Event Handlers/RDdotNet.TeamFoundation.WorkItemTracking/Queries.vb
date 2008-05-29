Imports System.Xml


''' <summary>
''' This class holds methods for pulling data from a WorkItemTrackingEvent
''' </summary>
''' <remarks></remarks>
Public Class Querys

    ''' <summary>
    ''' Prevents the instanciation of this class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

    'Microsoft.VSTS.CMMI.Estimate
    'Microsoft.VSTS.Scheduling.RemainingWork
    'Microsoft.VSTS.Scheduling.CompletedWork
    Public Shared Function GetTextField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As TextField
        Return (From f In Value.TextFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    Public Shared Function GetCoreStringField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As StringField
        Return (From f In Value.CoreFields.StringFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    Public Shared Function GetCoreIntegerField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As IntegerField
        Return (From f In Value.CoreFields.IntegerFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    '' <summary>
    '' Returns work item type of this event
    '' </summary>
    Public Shared Function GetWorkItemType(ByVal eventData As WorkItemChangedEvent) As StringField
        Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindWorkItemType))
    End Function

    Public Shared Function FindWorkItemType(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.WorkItemType" Then Return True
    End Function

    '' <summary>
    '' Returns work item type of this event
    '' </summary>
    Public Shared Function GetWorkItemID(ByVal eventData As WorkItemChangedEvent) As IntegerField
        Return eventData.CoreFields.IntegerFields.Find(New Predicate(Of IntegerField)(AddressOf FindID))
    End Function

    Public Shared Function FindID(ByVal obj As IntegerField) As Boolean
        If obj.ReferenceName = "System.Id" Then Return True
    End Function

    '' <summary>
    '' Returns changed by user name for this Team Foundation event
    '' </summary>
    Public Shared Function GetChangedByName(ByVal eventData As WorkItemChangedEvent) As StringField
        Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindChangedBy))
    End Function

    Public Shared Function FindChangedBy(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.ChangedBy" Then Return True
    End Function

    '' <summary>
    '' Returns changed by user name for this Team Foundation event
    '' </summary>
    Public Shared Function GetHeatReference(ByVal eventData As WorkItemChangedEvent) As StringField
        Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindHeatReference))
    End Function

    Public Shared Function FindHeatReference(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "Aggreko.Heat.Reference" Then Return True
    End Function

    '' <summary>
    '' Returns created by user name for this Team Foundation event
    '' </summary>
    Public Shared Function GetCreatedByName(ByVal eventData As WorkItemChangedEvent) As StringField
        Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindCreatedBy))
    End Function

    Public Shared Function FindCreatedBy(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.CreatedBy" Then Return True
    End Function

    '' <summary>
    '' Returns assigned to name for this Team Foundation event
    '' </summary>
    Public Shared Function GetAssignedToName(ByVal eventData As WorkItemChangedEvent) As StringField
        Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindAssignedTo))
    End Function

    Public Shared Function FindAssignedTo(ByVal obj As StringField) As Boolean
        If obj.ReferenceName = "System.AssignedTo" Then Return True
    End Function

End Class