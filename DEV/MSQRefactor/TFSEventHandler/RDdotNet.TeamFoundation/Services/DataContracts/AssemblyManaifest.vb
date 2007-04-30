Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

<DataContract(), Serializable()> _
Public Class AssemblyManaifest

    Private _Version As Integer
    Private _Assemblys As Collection(Of AssemblyItem)


    <DataMember()> _
    Public Property Version() As Integer
        Get
            Return _Version
        End Get
        Set(ByVal value As Integer)
            _Version = value
        End Set
    End Property

    <DataMember()> _
    Public Property Assemblys() As Collection(Of AssemblyItem)
        Get
            Return _Assemblys
        End Get
        Set(ByVal value As Collection(Of AssemblyItem))
            _Assemblys = value
        End Set
    End Property

End Class
