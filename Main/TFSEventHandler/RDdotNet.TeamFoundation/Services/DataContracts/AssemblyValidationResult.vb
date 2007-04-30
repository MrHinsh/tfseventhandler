Imports System.Runtime.Serialization

<DataContract()> _
Public Class AssemblyValidationResult

    Private _AssemblyName As String
    Private _EventHandlerTypes As New System.Collections.ObjectModel.Collection(Of System.Type)
    Private _ValidAssembly As Boolean = False
    Private _Fault As System.ServiceModel.FaultException = Nothing


    <DataMember()> _
    Public Property EventHandlerTypes() As System.Collections.ObjectModel.Collection(Of System.Type)
        Get
            Return _EventHandlerTypes
        End Get
        Set(ByVal value As System.Collections.ObjectModel.Collection(Of System.Type))
            _EventHandlerTypes = value
        End Set
    End Property

    <DataMember()> _
    Public Property AssemblyName() As String
        Get
            Return _AssemblyName
        End Get
        Set(ByVal value As String)
            _AssemblyName = value
        End Set
    End Property

    <DataMember()> _
    Public Property ValidAssembly() As Boolean
        Get
            Return _ValidAssembly
        End Get
        Set(ByVal value As Boolean)
            _ValidAssembly = value
        End Set
    End Property

    <DataMember()> _
   Public Property Fault() As System.ServiceModel.FaultException
        Get
            Return _Fault
        End Get
        Set(ByVal value As System.ServiceModel.FaultException)
            _Fault = value
        End Set
    End Property


End Class
