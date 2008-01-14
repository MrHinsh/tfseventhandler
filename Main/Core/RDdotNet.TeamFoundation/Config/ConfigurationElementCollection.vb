
Public Class ConfigurationElementCollection(Of TConfigurationElement As {New, System.Configuration.ConfigurationElement, IConfigurationElement})
    Inherits System.Configuration.ConfigurationElementCollection

    Protected Overloads Overrides Function CreateNewElement() As System.Configuration.ConfigurationElement
        Return New TConfigurationElement
    End Function

    Protected Overrides Function GetElementKey(ByVal element As System.Configuration.ConfigurationElement) As Object
        Return CType(element, TConfigurationElement).Key
    End Function

    Default Public Overloads ReadOnly Property Item(ByVal Key As String) As TConfigurationElement
        Get
            Return CType(Me.BaseGet(Key), TConfigurationElement)
        End Get
    End Property

    Public Function CreateNew() As TConfigurationElement
        Return CreateNewElement()
    End Function

    Public Sub Add(ByVal ConfigurationElement As TConfigurationElement)
        Me.BaseAdd(ConfigurationElement, True)
    End Sub

    Public Sub Remove(ByVal ConfigurationElement As TConfigurationElement)
        Me.BaseRemove(ConfigurationElement.Key)
    End Sub

    Public Sub Remove(ByVal Key As String)
        Me.BaseRemove(Key)
    End Sub

    Public Sub Clear()
        Me.BaseClear()
    End Sub

    Public Function Contains(ByVal element As TConfigurationElement) As Boolean
        For c As Integer = 0 To MyBase.Count
            If element.Key = Me.BaseGetKey(c) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function Contains(ByVal key As String) As Boolean
        For c As Integer = 0 To MyBase.Count
            If key = Me.BaseGetKey(c) Then
                Return True
            End If
        Next
        Return False
    End Function

End Class
