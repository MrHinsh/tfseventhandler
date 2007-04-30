Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports MerrillLynch.TeamFoundation
Imports MerrillLynch.TeamFoundation.Config


Public Class FaultGen

    Public Enum Faults
        Unknown = 0
    End Enum

    Public Shared Function GetFault(ByVal Fault As Faults, ByVal ReasonString As String) As System.ServiceModel.FaultException
        Dim code As New System.ServiceModel.FaultCode(Format(Fault, "MLTFSdddddd"))
        Dim reason As New System.ServiceModel.FaultReason(ReasonString)
        Return New System.ServiceModel.FaultException(reason, code, "")
    End Function


    Public Shared Function GetFault(Of T)(ByVal Fault As Faults, ByVal Detail As T, ByVal ReasonString As String) As System.ServiceModel.FaultException(Of T)
        Dim code As New System.ServiceModel.FaultCode(Format(Fault, "MLTFSdddddd"))
        Dim reason As New System.ServiceModel.FaultReason(ReasonString)
        Return New System.ServiceModel.FaultException(Of T)(Detail, reason, code, "")
    End Function

End Class