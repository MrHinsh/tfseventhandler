
Namespace Config


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

    End Class

End Namespace