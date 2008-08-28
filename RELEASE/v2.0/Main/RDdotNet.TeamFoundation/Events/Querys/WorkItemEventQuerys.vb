Imports System.Xml

Namespace Events.Querys


    ''' <summary>
    ''' This class holds methods for pulling data from a WorkItemTrackingEvent
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WorkItemEventQuerys

        ''' <summary>
        ''' Prevents the instanciation of this class
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub

        '' <summary>
        '' Returns work item type of this event
        '' </summary>
        Public Shared Function GetWorkItemType(ByVal eventData As WorkItemChangedEvent) As String
            Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindWorkItemType)).NewValue
        End Function

        Public Shared Function FindWorkItemType(ByVal obj As StringField) As Boolean
            If obj.ReferenceName = "System.WorkItemType" Then Return True
        End Function

        '' <summary>
        '' Returns work item type of this event
        '' </summary>
        Public Shared Function GetWorkItemID(ByVal eventData As WorkItemChangedEvent) As Integer
            Return eventData.CoreFields.IntegerFields.Find(New Predicate(Of IntegerField)(AddressOf FindID)).NewValue
        End Function

        Public Shared Function FindID(ByVal obj As IntegerField) As Boolean
            If obj.ReferenceName = "System.Id" Then Return True
        End Function

        '' <summary>
        '' Returns changed by user name for this Team Foundation event
        '' </summary>
        Public Shared Function GetChangedByName(ByVal eventData As WorkItemChangedEvent) As String
            Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindChangedBy)).NewValue
        End Function

        Public Shared Function FindChangedBy(ByVal obj As StringField) As Boolean
            If obj.ReferenceName = "System.ChangedBy" Then Return True
        End Function

        '' <summary>
        '' Returns assigned to name for this Team Foundation event
        '' </summary>
        Public Shared Function GetAssignedToName(ByVal eventData As WorkItemChangedEvent) As String
            Return eventData.CoreFields.StringFields.Find(New Predicate(Of StringField)(AddressOf FindAssignedTo)).NewValue
        End Function

        Public Shared Function FindAssignedTo(ByVal obj As StringField) As Boolean
            If obj.ReferenceName = "System.AssignedTo" Then Return True
        End Function

    End Class

End Namespace