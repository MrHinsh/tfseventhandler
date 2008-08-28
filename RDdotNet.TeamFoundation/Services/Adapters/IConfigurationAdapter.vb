Namespace Adapters

    Public Interface IConfigurationAdapter(Of TObject, TConfigItem)

        Function Convert(ByVal source As TObject) As TConfigItem
        Function Convert(ByVal source As TConfigItem) As TObject

    End Interface

End Namespace