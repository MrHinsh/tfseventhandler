Namespace Adapters

    Public Interface IAdapter(Of TObject, TProxy)

        Function Convert(ByVal source As TObject) As TProxy
        Function Convert(ByVal source As TProxy) As TObject

    End Interface

End Namespace