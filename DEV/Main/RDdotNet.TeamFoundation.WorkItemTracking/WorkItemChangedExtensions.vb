Module WorkItemChangedExtensions

    <System.Runtime.CompilerServices.Extension()> _
    Public Function GetTextField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As TextField
        Return (From f In Value.TextFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    <System.Runtime.CompilerServices.Extension()> _
   Public Function GetCoreStringField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As StringField
        Return (From f In Value.CoreFields.StringFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function

    <System.Runtime.CompilerServices.Extension()> _
Public Function GetCoreIntegerField(ByVal Value As WorkItemChangedEvent, ByVal ReferenceName As String) As IntegerField
        Return (From f In Value.CoreFields.IntegerFields Where f.ReferenceName = ReferenceName).SingleOrDefault
    End Function


End Module
