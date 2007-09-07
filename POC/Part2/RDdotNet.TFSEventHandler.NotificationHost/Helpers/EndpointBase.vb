Imports system.Xml
Imports system.Xml.Serialization

Namespace TeamFoundation.Events


    Public Class EndpointBase

        ''' <summary>
        ''' Deserializes a Type
        ''' </summary>
        ''' <typeparam name="T">Type to deserialize</typeparam>
        ''' <param name="serializedType">serialized version of the type</param>
        ''' <returns>DeSerialized version of the type</returns>
        Public Shared Function CreateInstance(Of T As {New})(ByVal serializedType As String) As T
            Dim customType As T = New T()

            Dim serializer As Xml.Serialization.XmlSerializer = New XmlSerializer(GetType(T))

            Dim xmlDocument As XmlDocument = New XmlDocument()
            xmlDocument.LoadXml(serializedType)
            Dim xmlNodeReader As XmlNodeReader = New XmlNodeReader(xmlDocument)
            Try
                customType = CType(serializer.Deserialize(xmlNodeReader), T)
            Catch ex As Exception
                ''TODO: Add Logging etc here...
                Throw
            End Try
            Return customType
        End Function

        Public Shared Function ReformatServerURL(ByVal EventType As EventTypes, ByVal CurrentUrl As String) As String
            Select Case EventType
                Case EventTypes.WorkItemChangedEvent
                    CurrentUrl = CurrentUrl.Replace(":8080/WorkItemTracking/WorkItem.aspx?artifactMoniker=", "/workitem.aspx?id=")
            End Select
            Return CurrentUrl
        End Function


    End Class

End Namespace
