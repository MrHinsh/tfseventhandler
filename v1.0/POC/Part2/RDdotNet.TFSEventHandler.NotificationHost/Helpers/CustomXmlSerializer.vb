Namespace Helpers

    ''' <summary>
    ''' Defines how to serialize class members (fields and properties)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Enum SerializationMethod
        ''' <summary>
        ''' Serialize public members
        ''' </summary>
        ''' <remarks></remarks>
        Shallow

        ''' <summary>
        ''' Serialize private, friend, and public members
        ''' </summary> 
        ''' <remarks></remarks>
        Deep
    End Enum

    ''' <summary>
    ''' Serialize and deserialize objects into and from Xml.
    ''' Write operations serialize the object into various target mediums.
    ''' Read operations deserialize the object from various source mediums.
    ''' </summary>
    ''' <remarks>
    ''' Designed and Created by Larry Steinle, 2006.
    ''' 
    ''' Deserializing structure data types is not supported.
    ''' Serialization/Deserialization of circular references is not supported.
    ''' Designed for use with System.Xml.Serialization.IXmlSerializable.
    ''' 
    ''' Standard FreeWare Licensing Applies. This software is to be used free of charge and may not be sold.
    '''
    ''' Resources:
    ''' http://www.programmersheaven.com/2/Dot-Net-Reflection-Part-1-Page2
    ''' Elements are used with inner text at all times. Attributes aren't supported. 
    ''' This ensures that we won't have any translation problems when loading the Xml into the target object.
    ''' Note: This class does not support deserializing structures.
    '''
    ''' Reason Structures Aren't Supported for Deserialization: http://www.dotnet247.com/247reference/msgs/31/158508.aspx
    ''' The SetValue method takes an object parameter, which causes a boxing
    ''' operation. SetValue ends up being called on the heap-based boxed copy
    ''' rather than the stack-based copy. You need to unbox the heap-based copy
    ''' back to the stack to see the end result of the SetValue call.
    ''' 
    ''' Conversion From VB.Net to C#: http://www.harding.edu/USER/fmccown/WWW/vbnet_csharp_comparison.html
    '''  To simplify conversion between VB.Net and C# the following rules have been applied:
    '''    Avoided use of Do...Until logic because it is not supported in the "real" programming language: C#.
    '''    Made all data types supportable in both C# and VB.Net, 2003 and 2005.
    '''    Used System.Convert instead of CType whenever possible.
    ''' 
    ''' FIX: December 9, 2006 - Code Changes to Correctly Manage IDictionary Object Types
    ''' When serializing/deserializing classes that inherit from IDictionary the property
    ''' IncludeClassNameAttribute must be set to a value of True. This is because the item
    ''' property for an IDictionary class cannot be interogated for it's data type. The item
    ''' property always returns a DictionaryEntry which has a value type of object.
    ''' </remarks>
    Public Class CustomXmlSerializer
#Region "Public Properties: Serialization Behavior"
        Private m_UseCData As Boolean = False
        Private m_IgnoreWarnings As Boolean = True
        Private m_IncludeClassNameAttribute As Boolean = False
        Private m_Method As SerializationMethod = SerializationMethod.Shallow

        ''' <summary>
        ''' Serialize string values into xml CData tags.
        ''' </summary>
        ''' <value>True to enable CData serialization, False to disable and store as string.</value>
        ''' <returns>Boolean value identifing property state.</returns>
        ''' <remarks>When enabled strings and enumerators are stored in CData tags.</remarks>
        Public Property CDataStorage() As Boolean
            Get
                Return m_UseCData
            End Get
            Set(ByVal Value As Boolean)
                m_UseCData = Value
            End Set
        End Property

        ''' <summary>
        ''' Ignore warnings and allow operation to continue.
        ''' </summary>
        ''' <value>True to ignore warning errors, False to throw warning errors.</value>
        ''' <returns>Boolean value identifing property state.</returns>
        ''' <remarks>Use with caution as deserialization can load objects with incomplete data.</remarks>
        Public Property IgnoreWarnings() As Boolean
            Get
                Return m_IgnoreWarnings
            End Get
            Set(ByVal Value As Boolean)
                m_IgnoreWarnings = Value
            End Set
        End Property

        ''' <summary>
        ''' Record the name of the class when serializing to ensure that the
        ''' class can be deserialized.
        ''' </summary>
        ''' <value>True to include the className, False to exclude it.</value>
        ''' <returns>The state of the property.</returns>
        ''' <remarks></remarks>
        Public Property IncludeClassNameAttribute() As Boolean
            Get
                Return m_IncludeClassNameAttribute
            End Get
            Set(ByVal value As Boolean)
                m_IncludeClassNameAttribute = value
            End Set
        End Property

        ''' <summary>
        ''' Identifies how the class should be serialized.
        ''' </summary>
        ''' <value>Shallow to serialize public fields and properties. Deep to serialize private, friend, and public fields and properties.</value>
        ''' <returns>The state of the property.</returns>
        ''' <remarks></remarks>
        Public Property Method() As SerializationMethod
            Get
                'FEATURE: June 22, 2006 - Added to support Deep Serialization in addition to default Shallow Serialization.
                Return m_Method
            End Get
            Set(ByVal Value As SerializationMethod)
                'FEATURE: June 22, 2006 - Added to support Deep Serialization in addition to default Shallow Serialization.
                m_Method = Value
            End Set
        End Property

        ''' <summary>
        ''' Defines the Reflection BindingFlags required to support the selected SerializationMethod.
        ''' </summary>
        ''' <returns>The state of the property.</returns>
        ''' <remarks></remarks>
        Private ReadOnly Property BindingCriteria() As System.Reflection.BindingFlags
            Get
                'FEATURE: June 22, 2006 - Added to support new Method property.
                Dim Flags As System.Reflection.BindingFlags = System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.Public
                If Method = SerializationMethod.Deep Then Flags = Flags Or System.Reflection.BindingFlags.NonPublic
                Return Flags
            End Get
        End Property
#End Region

#Region "Public Methods: Deserialization Routines"
        ''' <summary>
        ''' Deserialize Xml into the target object.
        ''' </summary>
        ''' <param name="reader">The source of the Xml to load.</param>
        ''' <param name="target">The destination for the Xml.</param>
        ''' <remarks>
        ''' The target must be passed in ByVal and returned to support data type variable serialization.
        ''' </remarks>
        Public Function ReadXml(ByVal reader As System.Xml.XmlReader, ByVal target As Object) As Object
            'Get Attributes to Identify Type of Object to Save
            Dim _MetaInstructions As System.Collections.SortedList
            _MetaInstructions = GetAttributes(reader)

            MoveToRootNode(reader)

            'Attempt to Identify Target Type
            If reader.IsEmptyElement Then
                'No data to work with for the current element. Continue processing.
            ElseIf TypeOf target Is System.Array Then
                'Feature: Added Support for System.Array
                target = ReadArray(reader, target)
            ElseIf IsDataType(target) _
            OrElse IsDataType(reader) Then
                'Since all values are stored as string in Xml we 
                'have to cast the variable back to its original type.
                'Data types are stored with their type as the node name.
                Dim _DataType As String = reader.Name
                MoveToValueNode(reader)
                SaveValue(target, _DataType, reader.Value)
                MoveToNextTag(reader)
            ElseIf Not _MetaInstructions Is Nothing _
            AndAlso IsDataType(_MetaInstructions.Item("className"), True) Then
                'FIX: December 9, 2006 - Add support when data type is recorded in XML.
                'Since all values are stored as string in Xml we 
                'have to cast the variable back to its original type.
                'Data types are stored with their type as the node name.
                Dim _DataType As String = System.Convert.ToString(_MetaInstructions.Item("className"))
                MoveToValueNode(reader)
                SaveValue(target, _DataType, reader.Value)
                MoveToNextTag(reader)
            ElseIf TypeOf target Is System.Enum Then
                MoveToValueNode(reader)
                target = System.Enum.Parse(target.GetType, reader.Value)
                MoveToNextTag(reader)
            ElseIf target Is Nothing Then
                'FIX: December 9, 2006 - Removed reference to the non-instantiated target object.
                Throw New System.InvalidOperationException("Unable to deserialize Xml. The target parameter must be initialized.")
            Else
                MoveToNextNode(reader)

                Do While reader.NodeType <> System.Xml.XmlNodeType.EndElement _
                AndAlso String.Compare(reader.Name, "System.Collections.IEnumerable", True) <> 0
                    If reader.IsEmptyElement Then
                        'No data to work with for the current element. Continue processing.
                    Else
                        target = ReadFields(reader, target)
                        target = ReadProperties(reader, target)
                    End If

                    MoveToNextTag(reader)
                Loop

                'FIX: July 13, 2006 - Support the scenario where the Item of an IEnumerable is itself an IEnumerable.
                If TypeOf target Is System.Collections.IEnumerable Then
                    Do While reader.NodeType <> System.Xml.XmlNodeType.EndElement
                        target = ReadChildren(reader, target, target)
                        MoveToNextTag(reader)
                    Loop
                End If
            End If

            Return target
        End Function

        ''' <summary>
        ''' Deserialize Xml into the target object.
        ''' </summary>
        ''' <param name="node">The Xml to load into the object.</param>
        ''' <param name="target">The destination for the Xml.</param>
        ''' <remarks></remarks>
        Public Function ReadXml(ByVal node As System.Xml.XmlNode, ByVal target As Object) As Object
            Dim _xmlReader As New System.Xml.XmlNodeReader(node)
            target = ReadXml(_xmlReader, target)
            Return target
        End Function

        ''' <summary>
        ''' Deserialize Xml into the target object.
        ''' </summary>
        ''' <param name="document">The Xml to load into the object.</param>
        ''' <param name="target">The destination for the Xml.</param>
        ''' <remarks></remarks>
        Public Function ReadXml(ByVal document As System.Xml.XmlDocument, ByVal target As Object) As Object
            Dim _xmlReader As New System.Xml.XmlNodeReader(document)
            target = ReadXml(_xmlReader, target)
            Return target
        End Function

        ''' <summary>
        ''' Deserialize Xml into the target object.
        ''' </summary>
        ''' <param name="path">A path to the file with the Xml to load into the object.</param>
        ''' <param name="target">The destination for the Xml.</param>
        ''' <remarks></remarks>
        Public Function ReadXml(ByVal path As String, ByVal target As Object) As Object
            If System.IO.File.Exists(path) Then
                Dim _File As New System.IO.StreamReader(path)
                Dim _xmlText As New System.Text.StringBuilder

                _xmlText.Append(_File.ReadToEnd())
                _File.Close()

                target = ReadXml(_xmlText, target)
                Return target
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Deserialize Xml into the target object.
        ''' </summary>
        ''' <param name="text">The Xml to load into the object.</param>
        ''' <param name="target">The destination for the Xml.</param>
        ''' <remarks></remarks>
        Public Function ReadXml(ByVal text As System.Text.StringBuilder, ByVal target As Object) As Object
            Dim _textStreamReader As New System.IO.StringReader(text.ToString)
            Dim _xmlReader As New System.Xml.XmlTextReader(_textStreamReader)
            target = ReadXml(_xmlReader, target)
            Return target
        End Function
#End Region

#Region "Public Methods: Serialization Routines"
        ''' <summary>
        ''' Serialize the source object into an XmlDocument following "Shallow Copy" business logic.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <returns>The serialized object.</returns>
        ''' <remarks></remarks>
        Public Function WriteDocument(ByVal source As Object) As System.Xml.XmlDocument
            Dim _xmlDoc As New System.Xml.XmlDocument
            _xmlDoc.LoadXml(WriteString(source))
            Return _xmlDoc
        End Function

        ''' <summary>
        ''' Serialize the source object into a file following "Shallow Copy" business logic.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <param name="path">The file to save the Xml into.</param>
        ''' <param name="replaceFile">
        ''' If true the file is deleted before the contents are saved.
        ''' If false and the file exists serialization is terminated.
        ''' </param>
        ''' <remarks></remarks>
        Public Sub WriteFile(ByVal source As Object, ByVal path As String, Optional ByVal replaceFile As Boolean = False)
            If replaceFile AndAlso System.IO.File.Exists(path) Then
                System.IO.File.Delete(path)
            End If

            If Not System.IO.File.Exists(path) Then
                Dim _xmlText As String = WriteString(source)
                Dim _File As New System.IO.StreamWriter(path)
                _File.Write(_xmlText)
                _File.Close()
            End If
        End Sub

        ''' <summary>
        ''' Serialize the source object into a string following "Shallow Copy" business logic.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <returns>The serialized object.</returns>
        ''' <remarks></remarks>
        Public Function WriteString(ByVal source As Object) As String
            Return WriteText(source).ToString
        End Function

        ''' <summary>
        ''' Serialize the source object into an StringBuilder following "Shallow Copy" business logic.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <returns>The serialized object.</returns>
        ''' <remarks></remarks>
        Public Function WriteText(ByVal source As Object) As System.Text.StringBuilder
            Dim _xmlText As New System.Text.StringBuilder

            'Serialize the class to Xml
            Dim _TextStreamWriter As New System.IO.StringWriter(_xmlText)
            Dim _xmlWriter As New System.Xml.XmlTextWriter(_TextStreamWriter)
            WriteXml(source, _xmlWriter)

            Return _xmlText
        End Function

        ''' <summary>
        ''' Serialize the source object into an XmlWriter following "Shallow Copy" business logic.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <param name="writer">The destination for the xml.</param>
        ''' <param name="propertyName">If serializing a class property provide the name of the property. If serializing a class then set to nothing.</param>
        ''' <remarks>
        ''' Shallow Copy means that only the exposed properties are serialized. 
        ''' Hidden fields, properties, or protected properties are ignored.
        ''' </remarks>
        Public Sub WriteXml(ByVal source As Object, ByVal writer As System.Xml.XmlWriter, Optional ByVal propertyName As String = Nothing)
            Dim _ElementName As String

            If source Is Nothing Then
                Exit Sub
            ElseIf propertyName Is Nothing Then
                _ElementName = source.GetType.Name
            Else
                'Use the pre-defined name for the Xml Node Name
                _ElementName = propertyName
            End If

            'Feature: Added Support for System.Array
            If _ElementName.IndexOf("[") > 0 Then
                _ElementName = _ElementName.Substring(0, _ElementName.IndexOf("["))
            End If

            writer.WriteStartElement(_ElementName)

            'Feature: Added Support for System.Array
            If TypeOf source Is System.Array Then
                writer.WriteAttributeString("size", Nothing, GetArraySize(CType(source, System.Array)))
                writer.WriteAttributeString("className", Nothing, "System.Array")
                writer.WriteAttributeString("type", Nothing, GetArrayType(CType(source, System.Array)))
            End If

            WriteKey(source, writer)
            WriteClassName(source, writer)

            If IsDataType(source) Then
                If CDataStorage AndAlso (TypeOf source Is Char OrElse TypeOf source Is String) Then
                    writer.WriteCData(source.ToString.Trim)
                Else
                    writer.WriteString(source.ToString.Trim)
                End If
            ElseIf TypeOf source Is System.Array Then
                'Feature: Added Support for System.Array
                WriteArray(CType(source, System.Array), writer)
            ElseIf TypeOf source Is System.Enum Then
                writer.WriteString(source.ToString.Trim)
            Else
                WriteFields(source, writer)
                WriteProperties(source, writer)

                'FIX: July 13, 2006 - Support the scenario where the Item of an IEnumerable is itself an IEnumerable.
                If TypeOf source Is System.Collections.IEnumerable Then
                    writer.WriteStartElement("System.Collections.IEnumerable")
                    For Each _Item As Object In CType(source, System.Collections.IEnumerable)
                        WriteXml(_Item, writer)
                    Next
                    writer.WriteEndElement()
                End If
            End If

            writer.WriteEndElement()
        End Sub
#End Region

#Region "Private Methods: Deserialization Helpers"
        ''' <summary>
        ''' Assings the Xml values to the fields.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <param name="target">The class containing the fields to update.</param>
        ''' <returns>The updated class.</returns>
        ''' <remarks></remarks>
        Private Function ReadFields(ByVal reader As System.Xml.XmlReader, ByVal target As Object) As Object
            Dim _FieldValue As Object

            For Each _Field As System.Reflection.FieldInfo In target.GetType.GetFields(BindingCriteria)
                If String.Compare(reader.Name, _Field.Name, True) = 0 Then
                    'BEGIN: FIX: July 13, 2006 - Check for Array Data Type
                    Dim _MetaInstructions As System.Collections.SortedList = GetAttributes(reader)

                    If Not _MetaInstructions Is Nothing _
                    AndAlso _MetaInstructions.ContainsKey("className") _
                    AndAlso _MetaInstructions.ContainsKey("size") _
                    AndAlso _MetaInstructions.ContainsKey("type") Then
                        _FieldValue = CreateArray(target, System.Convert.ToString(_MetaInstructions.Item("size")), System.Convert.ToString(_MetaInstructions.Item("type")))
                    Else
                        _FieldValue = CreateClass(_MetaInstructions, target, _Field)
                    End If
                    'END: FIX: July 13, 2006

                    If TypeOf _FieldValue Is System.Enum Then
                        MoveToValueNode(reader)

                        'Translate String Value to Enumerator Value and Assign It to the Property
                        Dim _EnumValue As Object = System.Enum.Parse(_FieldValue.GetType, reader.Value)
                        _Field.SetValue(target, _EnumValue)

                        MoveToNextTag(reader)
                    ElseIf IsDataType(_FieldValue) OrElse IsDataType(_Field) Then
                        MoveToValueNode(reader)
                        SaveValue(target, _Field, reader.Value)
                        MoveToNextTag(reader)
                    ElseIf TypeOf _FieldValue Is System.Array Then
                        'Feature: Added Support for System.Array.
                        Dim _Arr As System.Array = CType(ReadXml(reader, _FieldValue), System.Array)
                        _Field.SetValue(target, _Arr)
                    ElseIf TypeOf _FieldValue Is System.Collections.IEnumerable Then
                        target = ReadChildren(reader, target, _FieldValue)
                    Else
                        _FieldValue = ReadXml(reader, _FieldValue)
                    End If

                    Exit For
                End If
            Next

            Return target
        End Function

        ''' <summary>
        ''' Assings the Xml values to the properties.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <param name="target">The class containing the fields to update.</param>
        ''' <returns>The updated class.</returns>
        ''' <remarks></remarks>
        Private Function ReadProperties(ByVal reader As System.Xml.XmlReader, ByVal target As Object) As Object
            Dim _PropertyValue As Object

            For Each _Property As System.Reflection.PropertyInfo In target.GetType.GetProperties(BindingCriteria)
                If String.Compare(reader.Name, _Property.Name, True) = 0 Then
                    'BEGIN: FIX: July 13, 2006 - Check for Array Data Type
                    Dim _MetaInstructions As System.Collections.SortedList = GetAttributes(reader)

                    If Not _MetaInstructions Is Nothing _
                    AndAlso _MetaInstructions.ContainsKey("className") _
                    AndAlso _MetaInstructions.ContainsKey("size") _
                    AndAlso _MetaInstructions.ContainsKey("type") Then
                        _PropertyValue = CreateArray(target, System.Convert.ToString(_MetaInstructions.Item("size")), System.Convert.ToString(_MetaInstructions.Item("type")))
                    Else
                        _PropertyValue = CreateClass(_MetaInstructions, target, _Property)
                    End If
                    'END: FIX: July 13, 2006

                    If TypeOf _PropertyValue Is System.Enum Then
                        MoveToValueNode(reader)

                        'Translate String Value to Enumerator Value and Assign It to the Property
                        Dim _EnumValue As Object = System.Enum.Parse(_PropertyValue.GetType, reader.Value)
                        _Property.SetValue(target, _EnumValue, Nothing)

                        MoveToNextTag(reader)
                    ElseIf IsDataType(_PropertyValue) OrElse IsDataType(_Property) Then
                        MoveToValueNode(reader)
                        SaveValue(target, _Property, reader.Value)
                        MoveToNextTag(reader)
                    ElseIf TypeOf _PropertyValue Is System.Array Then
                        'Feature: Added Support for System.Array.
                        Dim _Arr As System.Array = CType(ReadXml(reader, _PropertyValue), System.Array)
                        _Property.SetValue(target, _Arr, Nothing)
                    ElseIf TypeOf _PropertyValue Is System.Collections.IEnumerable Then
                        target = ReadChildren(reader, target, _PropertyValue)
                    Else
                        _PropertyValue = ReadXml(reader, _PropertyValue)
                    End If

                    Exit For
                End If
            Next

            Return target
        End Function

        ''' <summary>
        ''' Analyzes the Xml to build the child objects adding them to the list property.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <param name="target">The class containing the list field to update.</param>
        ''' <param name="propertyMember">The IEnumerable property.</param>
        ''' <returns>The updated class.</returns>
        ''' <remarks></remarks>
        Private Function ReadChildren(ByVal reader As System.Xml.XmlReader, ByVal target As Object, ByVal propertyMember As Object) As Object
            Dim _MetaInstructions As System.Collections.SortedList
            Dim _NewValue As Object

            'Initialize the class
            ExecuteClearMethod(propertyMember)
            MoveToNextNode(reader)

            Do While reader.NodeType <> System.Xml.XmlNodeType.EndElement
                'Get Attributes to Identify Type of Object to Create
                _MetaInstructions = GetAttributes(reader)

                'Instantiate New Object: Identify the type of object to add to the list and create it
                'BEGIN: FIX: July 13, 2006 - Check for Array Data Type
                If Not _MetaInstructions Is Nothing _
                AndAlso _MetaInstructions.ContainsKey("className") _
                AndAlso _MetaInstructions.ContainsKey("size") _
                AndAlso _MetaInstructions.ContainsKey("type") Then
                    _NewValue = CreateArray(target, System.Convert.ToString(_MetaInstructions.Item("size")), System.Convert.ToString(_MetaInstructions.Item("type")))
                ElseIf Not _MetaInstructions Is Nothing _
                AndAlso IsDataType(_MetaInstructions.Item("className"), True) Then
                    _NewValue = InstantiateMember(target.GetType.Assembly, System.Convert.ToString(_MetaInstructions.Item("className")))
                    'END: FIX: July 13, 2006
                ElseIf Not _MetaInstructions Is Nothing _
                AndAlso _MetaInstructions.ContainsKey("className") Then
                    'FIX: December 12, 2006 - Support for Queue
                    _NewValue = InstantiateMember(target.GetType.Assembly, _MetaInstructions.Item("className").ToString)
                Else
                    'In the event the attributes don't record the className attempt to find the item 
                    'property which will tell us what type to instantiate.
                    'With this approach we must assume that all items in the list are of the same type.
                    _NewValue = Nothing

                    'Search Members for Item Property
                    For Each _ItemMember As System.Reflection.PropertyInfo In propertyMember.GetType.GetProperties
                        If String.Compare(_ItemMember.Name, "Item", True) = 0 Then
                            _NewValue = InstantiateMember(propertyMember.GetType.Assembly, _ItemMember.PropertyType.FullName)
                            Exit For
                        End If
                    Next
                End If

                'Populate the Object
                _NewValue = ReadXml(reader, _NewValue)

                'Add the Object to the List
                'FIX: June 22, 2006 - Restrict value assignment to values set to something (instead of Nothing).
                'FIX: July 1, 2006 - Assignment to _Added variable on ExecuteEnqueueMethod and ExecutePushMethod.
                Dim _Added As Boolean = False
                Dim _KeyValue As Object = _MetaInstructions.Item("key")
                If Not _NewValue Is Nothing AndAlso Not _Added Then _Added = ExecuteAddMethod(propertyMember, _KeyValue, _NewValue)
                If Not _NewValue Is Nothing AndAlso Not _Added Then _Added = ExecuteEnqueueMethod(propertyMember, _KeyValue, _NewValue)
                If Not _NewValue Is Nothing AndAlso Not _Added Then _Added = ExecutePushMethod(propertyMember, _KeyValue, _NewValue)

                If Not _NewValue Is Nothing AndAlso Not _Added AndAlso Not m_IgnoreWarnings Then
                    Throw New System.NotSupportedException("The class, " & target.GetType.Name & ", does not support deserialization. Missing the Add, Enqueue, or Push method.")
                End If

                MoveToNextTag(reader)
            Loop

            Return target
        End Function
#End Region

        'Feature: Added Support for System.Array
#Region "Private Methods: Deserialization Helpers: System.Array"
        Private Function ReadArray(ByVal reader As System.Xml.XmlReader, ByVal target As Object) As Object
            Dim _MetaInstructions As System.Collections.SortedList = GetAttributes(reader)
            Dim _ArraySize As String = Nothing
            Dim _ArrayType As String = Nothing
            Dim _ArrayPoint As String = Nothing
            Dim _NewValue As Object = Nothing

            If _MetaInstructions.ContainsKey("size") Then
                _ArraySize = System.Convert.ToString(_MetaInstructions.Item("size"))
            Else
                'Attempt to size array assuming that array is a Fixed Size Array
                _ArraySize = GetArraySize(CType(target, System.Array))
            End If

            If _MetaInstructions.ContainsKey("type") Then
                _ArrayType = System.Convert.ToString(_MetaInstructions.Item("type"))
            Else
                _ArrayType = GetArrayType(CType(target, System.Array))
            End If

            target = CreateArray(target, _ArraySize, _ArrayType)
            MoveToNextNode(reader)

            Do While reader.NodeType <> System.Xml.XmlNodeType.EndElement
                _MetaInstructions = GetAttributes(reader)

                If _MetaInstructions.ContainsKey("point") Then
                    _ArrayPoint = System.Convert.ToString(_MetaInstructions.Item("point"))
                End If

                MoveToNextNode(reader)
                _MetaInstructions = GetAttributes(reader)

                'Instantiate New Object: Identify the type of object to add to the list and create it
                If _MetaInstructions.ContainsKey("className") Then
                    _NewValue = InstantiateMember(target.GetType.Assembly, System.Convert.ToString(_MetaInstructions.Item("className")))
                ElseIf IsDataType(reader) Then
                    'Do Nothing
                Else
                    _NewValue = InstantiateMember(target.GetType.Assembly, _ArrayType)
                End If

                'Get Element Value
                _NewValue = ReadXml(reader, _NewValue)

                'Move Past Value
                MoveToNextTag(reader)

                'Add Element to Array
                CType(target, System.Array).SetValue(_NewValue, ReadIndexes(_ArrayPoint))

                'Move Past Closing Array Item Tag
                MoveToNextTag(reader)
            Loop

            Return target
        End Function

        Private Function CreateArray(ByVal target As Object, ByVal arraySize As String, ByVal arrayType As String) As System.Array
            'We have to increase the array size by a factor of 1 to account for zero-indexing.
            Dim _Indexes() As Integer = ReadIndexes(arraySize)
            Dim _ArrayType As System.Type

            Select Case arrayType.ToUpper.Trim
                Case "SYSTEM.OBJECT", "OBJECT" : _ArrayType = GetType(System.Object)
                Case "SYSTEM.BOOLEAN", "BOOLEAN", "BOOL" : _ArrayType = GetType(System.Boolean)
                Case "SYSTEM.BYTE", "BYTE" : _ArrayType = GetType(System.Byte)
                Case "SYSTEM.CHAR", "CHAR" : _ArrayType = GetType(System.Char)
                Case "SYSTEM.DATE", "DATE" : _ArrayType = GetType(System.DateTime)
                Case "SYSTEM.DATETIME", "DATETIME" : _ArrayType = GetType(System.DateTime)
                Case "SYSTEM.DECIMAL", "DECIMAL" : _ArrayType = GetType(System.Decimal)
                Case "SYSTEM.DOUBLE", "DOUBLE" : _ArrayType = GetType(System.Double)
                Case "SYSTEM.INT16", "INT16" : _ArrayType = GetType(System.Int16)
                Case "SYSTEM.INT32", "INT32" : _ArrayType = GetType(System.Int32)
                Case "SYSTEM.INT64", "INT64" : _ArrayType = GetType(System.Int64)
                Case "SYSTEM.INTEGER", "INTEGER", "INT" : _ArrayType = GetType(System.Int32)
                Case "SYSTEM.LONG", "LONG" : _ArrayType = GetType(System.Int64)
                Case "SYSTEM.SBYTE", "SBYTE" : _ArrayType = GetType(System.SByte)
                Case "SYSTEM.SHORT", "SHORT" : _ArrayType = GetType(System.Int16)
                Case "SYSTEM.SINGLE", "SINGLE", "FLOAT" : _ArrayType = GetType(System.Single)
                Case "SYSTEM.STRING", "STRING" : _ArrayType = GetType(System.String)
                Case "SYSTEM.UINT16", "UINT16" : _ArrayType = GetType(System.UInt16)
                Case "SYSTEM.UINT32", "UINT32" : _ArrayType = GetType(System.UInt32)
                Case "SYSTEM.UINT64", "UINT64" : _ArrayType = GetType(System.UInt64)
                Case "SYSTEM.UINTPTR", "UINTPTR" : _ArrayType = GetType(System.UIntPtr)
                Case "SYSTEM.INTPTR", "INTPTR" : _ArrayType = GetType(System.IntPtr)
                Case "SYSTEM.UINTEGER", "UINTEGER", "UINT" : _ArrayType = GetType(System.UInt32)
                Case "SYSTEM.ULONG", "ULONG" : _ArrayType = GetType(System.UInt64)
                Case "SYSTEM.USHORT", "USHORT" : _ArrayType = GetType(System.UInt16)
                Case Else
                    Try
                        _ArrayType = target.GetType.Module.GetType(arrayType, True)
                    Catch ex As System.Exception
                        Throw New System.InvalidCastException("An error occurred while creating an array. Casting to type, " & arrayType & ", is not supported.", ex)
                    End Try
            End Select

            Return System.Array.CreateInstance(_ArrayType, _Indexes)
        End Function

        Private Function ReadIndexes(ByVal indexes As String) As Integer()
            Dim _Sizes() As String = indexes.Split(System.Convert.ToChar(","))
            'C# Is 0 Based While VB Is 1 Based By Default
            Dim _Lengths(_Sizes.Length - 1) As Integer
            Dim _Index As Integer = 0

            For Each _Size As String In _Sizes
                If IsNumeric(_Size) Then
                    _Lengths(_Index) = System.Convert.ToInt32(_Size)
                End If

                _Index += 1
            Next

            Return _Lengths
        End Function
#End Region

#Region "Private Methods: Serialization Helpers"
        Private Sub WriteKey(ByRef source As Object, ByVal writer As System.Xml.XmlWriter)
            If TypeOf source Is System.Collections.DictionaryEntry Then
                'Record the key value so we can re-assign the key/value pair when deserializing
                writer.WriteAttributeString("key", Nothing, CType(source, System.Collections.DictionaryEntry).Key.ToString)

                'Advance to the object to serialize
                source = CType(source, System.Collections.DictionaryEntry).Value
            End If
        End Sub

        Private Sub WriteClassName(ByVal sourceClass As Object, ByVal writer As System.Xml.XmlWriter)
            'Record the class name so we can instantiate the object when deserializing
            If IncludeClassNameAttribute _
            AndAlso TypeOf sourceClass Is Object Then
                writer.WriteAttributeString("className", Nothing, sourceClass.GetType.FullName.Trim)
            End If
        End Sub

        Private Sub WriteClassName(ByVal sourceType As System.Type, ByVal writer As System.Xml.XmlWriter)
            'Record the class name so we can instantiate the object when deserializing
            writer.WriteAttributeString("className", Nothing, sourceType.FullName.Trim)
        End Sub

        Private Sub WriteString(ByVal dataValue As Object, ByVal writer As System.Xml.XmlWriter)
            If CDataStorage AndAlso _
            (TypeOf dataValue Is Char _
            OrElse TypeOf dataValue Is String _
            OrElse TypeOf dataValue Is System.Enum) Then
                writer.WriteCData(dataValue.ToString.Trim)
            Else
                writer.WriteString(dataValue.ToString.Trim)
            End If
        End Sub

        ''' <summary>
        ''' Translates the fields of object, target, into Xml Elements.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <param name="writer">The destination for the xml.</param>
        ''' <remarks></remarks>
        Private Sub WriteFields(ByVal source As Object, ByVal writer As System.Xml.XmlWriter)
            For Each _Field As System.Reflection.FieldInfo In source.GetType.GetFields(BindingCriteria)
                Dim _Value As Object = _Field.GetValue(source)

                If IsDataType(_Value) _
                OrElse TypeOf _Value Is System.Enum Then
                    writer.WriteStartElement(_Field.Name)
                    WriteString(_Value, writer)
                    writer.WriteEndElement()
                ElseIf TypeOf _Value Is System.Array Then
                    'Feature: Added Support for System.Array
                    WriteXml(_Value, writer, _Field.Name)
                ElseIf TypeOf _Value Is System.Collections.IEnumerable Then
                    writer.WriteStartElement(_Field.Name)

                    'Record the Class Name so we can instantiate Abstract Classes when deserializing.
                    'An Abstract class is a class that inherits from a base class like System.Collections.DictionaryBase.
                    WriteClassName(_Field.FieldType, writer)

                    For Each _Item As Object In CType(_Value, System.Collections.IEnumerable)
                        WriteXml(_Item, writer)
                    Next

                    writer.WriteEndElement()
                Else
                    WriteXml(_Value, writer, _Field.Name)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Translates the properties of object, target, into Xml Elements.
        ''' </summary>
        ''' <param name="source">The object to serialize.</param>
        ''' <param name="writer">The destination for the xml.</param>
        ''' <remarks></remarks>
        Private Sub WriteProperties(ByVal source As Object, ByVal writer As System.Xml.XmlWriter)
            For Each _Property As System.Reflection.PropertyInfo In source.GetType.GetProperties(BindingCriteria)
                'FEATURE: December 9, 2006 - Check for XmlIgnore Provided by Jason Vetter
                If _Property.GetCustomAttributes(GetType(System.Xml.Serialization.XmlIgnoreAttribute), True).Length = 0 Then
                    Dim _Value As Object

                    Try
                        _Value = _Property.GetValue(source, Nothing)
                    Catch ex1 As System.Reflection.TargetParameterCountException
                        'This error may happen on a property like Item(ByVal Index As Integer)
                        'because the deserializer doesn't support properties with parameters.
                        If IgnoreWarnings Then
                            _Value = Nothing
                            'FIX: July 13, 2006 - Don't report valid error scenarios
                        ElseIf String.Compare(_Property.Name, "Item", True) = 0 Then
                            _Value = Nothing
                        Else
                            'FEATURE: July 13, 2006 - Make error message more meaningful
                            Throw New System.NotSupportedException("The property, " & _Property.Name & ", expects parameters. Property parameters are not supported.", ex1)
                        End If
                    Catch ex2 As System.Exception
                        Throw ex2
                    End Try

                    If IsDataType(_Value) _
                    OrElse TypeOf _Value Is System.Enum Then
                        writer.WriteStartElement(_Property.Name)
                        WriteString(_Value, writer)
                        writer.WriteEndElement()
                    ElseIf TypeOf _Value Is System.Array Then
                        'Feature: Added Support for System.Array
                        WriteXml(_Value, writer, _Property.Name)
                    ElseIf TypeOf _Value Is System.Collections.IEnumerable Then
                        writer.WriteStartElement(_Property.Name)

                        'Record the Class Name so we can instantiate Abstract Classes when deserializing.
                        'An Abstract class is a class that inherits from a base class like System.Collections.DictionaryBase.
                        WriteClassName(_Property.PropertyType, writer)

                        For Each _Item As Object In CType(_Value, System.Collections.IEnumerable)
                            WriteXml(_Item, writer)
                        Next

                        writer.WriteEndElement()
                    Else
                        WriteXml(_Value, writer, _Property.Name)
                    End If
                End If
            Next
        End Sub
#End Region

        'Feature: Added Support for System.Array
#Region "Private Methods: Serialization Helpers: System.Array"
        'Identifies the size of each dimension in the array
        Private Function GetArraySize(ByVal arr As System.Array) As String
            Dim _Size As String = String.Empty

            For _Index As Integer = 1 To arr.Rank
                _Size &= "," & UBound(arr, _Index) - LBound(arr, _Index) + 1
            Next

            If _Size.Length = 0 Then _Size = ","

            Return _Size.Substring(1)
        End Function

        Private Function GetArrayType(ByVal arr As System.Array) As String
            Return arr.GetType.FullName.Substring(0, arr.GetType.FullName.IndexOf("["))
        End Function

        'Translate current point into Xml friendly text
        Private Function GetArrayPoint(ByVal indices() As Integer) As String
            Dim _Indexes As String = String.Empty

            For Each _Index As String In indices
                _Indexes &= "," & _Index
            Next

            If _Indexes.Length = 0 Then _Indexes = ","

            Return _Indexes.Substring(1)
        End Function

        Private Sub WriteArray(ByVal arr As System.Array, ByVal writer As System.Xml.XmlWriter)
            'C# Is 0 Based While VB Is 1 Based By Default
            WriteArray(arr, writer, 1, Nothing)
        End Sub

        Private Sub WriteArray(ByVal arr As System.Array, ByVal writer As System.Xml.XmlWriter, ByVal rank As Integer, ByVal Indices() As Integer)
            Dim _Index As Integer

            'C# Is 0 Based While VB Is 1 Based By Default
            Dim _Indexes(arr.Rank - 1) As Integer

            If Not Indices Is Nothing Then
                System.Array.Copy(Indices, _Indexes, Indices.Length - 1)
            End If

            For _Index = LBound(arr, rank) To UBound(arr, rank)
                'C# Is 0 Based While VB Is 1 Based By Default
                _Indexes.SetValue(_Index, rank - 1)

                If Not arr.GetValue(_Indexes) Is Nothing Then
                    writer.WriteStartElement("System.Array.Item")
                    writer.WriteAttributeString("point", Nothing, GetArrayPoint(_Indexes))
                    WriteXml(arr.GetValue(_Indexes), writer)
                    writer.WriteEndElement()
                End If

                If arr.Rank > rank Then WriteArray(arr, writer, rank + 1, _Indexes)
            Next
        End Sub
#End Region

#Region "Private Methods: Reader Helpers"
        ''' <summary>
        ''' Advances to the first node
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <remarks></remarks>
        Private Sub MoveToRootNode(ByVal reader As System.Xml.XmlReader)
            Do While reader.NodeType = System.Xml.XmlNodeType.None _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.Comment _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.Notation _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.ProcessingInstruction _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.SignificantWhitespace _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.Whitespace _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.XmlDeclaration
                reader.Read()
            Loop
        End Sub

        ''' <summary>
        ''' Advance to the inner node, the tag between the open and closing element tags to access the element's value.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <remarks></remarks>
        Private Sub MoveToValueNode(ByVal reader As System.Xml.XmlReader)
            'The name will contain an empty string when we have landed on a value tag.
            Do
                reader.Read()
            Loop While Not reader.EOF _
            AndAlso (reader.Name.Trim.Length > 0 _
             OrElse reader.NodeType = System.Xml.XmlNodeType.Comment _
             OrElse reader.NodeType = System.Xml.XmlNodeType.Notation _
             OrElse reader.NodeType = System.Xml.XmlNodeType.ProcessingInstruction _
             OrElse reader.NodeType = System.Xml.XmlNodeType.SignificantWhitespace _
             OrElse reader.NodeType = System.Xml.XmlNodeType.Whitespace _
             OrElse reader.NodeType = System.Xml.XmlNodeType.XmlDeclaration)
        End Sub

        ''' <summary>
        ''' Advance to the next xml element.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <remarks></remarks>
        Private Sub MoveToNextNode(ByVal reader As System.Xml.XmlReader)
            Do
                reader.Read()
            Loop While Not reader.EOF _
            AndAlso reader.NodeType <> System.Xml.XmlNodeType.Element
        End Sub

        ''' <summary>
        ''' Advance to the next xml tag.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <remarks></remarks>
        Private Sub MoveToNextTag(ByVal reader As System.Xml.XmlReader)
            Do
                reader.Read()
            Loop While Not reader.EOF _
            AndAlso (reader.NodeType = System.Xml.XmlNodeType.Comment _
             OrElse reader.NodeType = System.Xml.XmlNodeType.Notation _
             OrElse reader.NodeType = System.Xml.XmlNodeType.ProcessingInstruction _
             OrElse reader.NodeType = System.Xml.XmlNodeType.SignificantWhitespace _
             OrElse reader.NodeType = System.Xml.XmlNodeType.Whitespace _
             OrElse reader.NodeType = System.Xml.XmlNodeType.XmlDeclaration)
        End Sub

        ''' <summary>
        ''' Returns a key/value pair representing the attributes in the element.
        ''' </summary>
        ''' <param name="reader">The bufferred xml to analyze.</param>
        ''' <returns>A sorted list of the attributes with the name as the key and the value as the value.</returns>
        ''' <remarks>Executing this method querries the current node for the attributes without advancing to the next node.</remarks>
        Private Function GetAttributes(ByVal reader As System.Xml.XmlReader) As System.Collections.SortedList
            Dim _List As New System.Collections.SortedList

            'FIX: December 9, 2005 - Fix to handle reading attributes from ReadXml routine
            If reader.NodeType <> System.Xml.XmlNodeType.None _
            AndAlso reader.MoveToFirstAttribute() Then
                Do
                    If reader.NodeType = System.Xml.XmlNodeType.Attribute Then
                        _List.Add(reader.Name, reader.Value)
                    End If
                Loop While reader.MoveToNextAttribute() OrElse reader.NodeType <> System.Xml.XmlNodeType.Attribute
            End If

            If reader.NodeType = System.Xml.XmlNodeType.Attribute Then reader.MoveToElement()

            Return _List
        End Function
#End Region

#Region "Private Methods: Reflection Helpers"
        ''' <summary>
        ''' Identifies if the object is a data type.
        ''' </summary>
        ''' <param name="dataValue">The object to test.</param>
        ''' <returns>True if the value is a data type.</returns>
        ''' <remarks></remarks>
        Private Function IsDataType(ByVal dataValue As Object, Optional ByVal valueIsTypeName As Boolean = False) As Boolean
            Dim _ValueType As String

            If valueIsTypeName Then
                'FIX: December 9, 2005 - Fix to handle reading attributes from ReadXml routine
                If dataValue Is Nothing Then
                    _ValueType = String.Empty
                Else
                    _ValueType = dataValue.ToString
                End If
            ElseIf TypeOf dataValue Is System.Xml.XmlReader Then
                _ValueType = CType(dataValue, System.Xml.XmlReader).Name
            ElseIf TypeOf dataValue Is System.Reflection.FieldInfo Then
                _ValueType = CType(dataValue, System.Reflection.FieldInfo).FieldType.FullName
            ElseIf TypeOf dataValue Is System.Reflection.PropertyInfo Then
                _ValueType = CType(dataValue, System.Reflection.PropertyInfo).PropertyType.FullName
            ElseIf TypeOf dataValue Is System.Type Then
                _ValueType = CType(dataValue, System.Type).FullName
            ElseIf Not dataValue Is Nothing Then
                _ValueType = dataValue.GetType.FullName
            Else
                _ValueType = String.Empty
            End If

            Select Case _ValueType.Trim.ToUpper
                Case "SYSTEM.BOOLEAN", "BOOLEAN", "BOOL" : Return True
                Case "SYSTEM.BYTE", "BYTE" : Return True
                Case "SYSTEM.CHAR", "CHAR" : Return True
                Case "SYSTEM.DATE", "DATE" : Return True
                Case "SYSTEM.DATETIME", "DATETIME" : Return True
                Case "SYSTEM.DECIMAL", "DECIMAL" : Return True
                Case "SYSTEM.DOUBLE", "DOUBLE" : Return True
                Case "SYSTEM.INT16", "INT16" : Return True
                Case "SYSTEM.INT32", "INT32" : Return True
                Case "SYSTEM.INT64", "INT64" : Return True
                Case "SYSTEM.INTEGER", "INTEGER", "INT" : Return True
                Case "SYSTEM.LONG", "LONG" : Return True
                Case "SYSTEM.SBYTE", "SBYTE" : Return True
                Case "SYSTEM.SHORT", "SHORT" : Return True
                Case "SYSTEM.SINGLE", "SINGLE", "FLOAT" : Return True
                Case "SYSTEM.STRING", "STRING" : Return True
                Case "SYSTEM.UINT16", "UINT16" : Return True
                Case "SYSTEM.UINT32", "UINT32" : Return True
                Case "SYSTEM.UINT64", "UINT64" : Return True
                Case "SYSTEM.UINTPTR", "UINTPTR" : Return True
                Case "SYSTEM.INTPTR", "INTPTR" : Return True
                Case "SYSTEM.UINTEGER", "UINTEGER", "UINT" : Return True
                Case "SYSTEM.ULONG", "ULONG" : Return True
                Case "SYSTEM.USHORT", "USHORT" : Return True
                Case Else : Return False
            End Select
        End Function

        ''' <summary>
        ''' Instantiates a new class.
        ''' </summary>
        ''' <param name="attributes">The attributes from the Xml used to identify the class type.</param>
        ''' <param name="target">The class containing the reference to the member to create.</param>
        ''' <param name="member">Information about the member to create.</param>
        ''' <returns>The instantiated class.</returns>
        ''' <remarks></remarks>
        Private Function CreateClass(ByVal attributes As System.Collections.SortedList, ByVal target As Object, ByVal member As System.Reflection.MemberInfo) As Object
            Dim _MemberValue As Object

            If TypeOf member Is System.Reflection.FieldInfo Then
                _MemberValue = CType(member, System.Reflection.FieldInfo).GetValue(target)
            ElseIf TypeOf member Is System.Reflection.PropertyInfo Then
                _MemberValue = CType(member, System.Reflection.PropertyInfo).GetValue(target, Nothing)
            Else
                Throw New System.NotSupportedException("MemberInfo type, " & member.GetType.Name & ", not supported.")
            End If

            'Instantiate Object Types
            If _MemberValue Is Nothing AndAlso Not IsDataType(member) Then
                Dim _MemberType As String = Nothing

                'Create a New Instance of the Object 
                If Not attributes Is Nothing AndAlso attributes.ContainsKey("className") Then
                    _MemberType = System.Convert.ToString(attributes.Item("className"))
                    _MemberValue = InstantiateMember(target.GetType.Assembly, _MemberType)
                End If

                If _MemberValue Is Nothing Then
                    'Attempt to create the class based on the field type.
                    'This won't work if the field is type System.Object.
                    If TypeOf member Is System.Reflection.FieldInfo Then
                        _MemberType = CType(member, System.Reflection.FieldInfo).FieldType.FullName
                    ElseIf TypeOf member Is System.Reflection.PropertyInfo Then
                        _MemberType = CType(member, System.Reflection.PropertyInfo).PropertyType.FullName
                    End If

                    _MemberValue = InstantiateMember(target.GetType.Assembly, _MemberType)
                End If

                'Assign the new class to the target object's field
                If TypeOf member Is System.Reflection.FieldInfo Then
                    CType(member, System.Reflection.FieldInfo).SetValue(target, _MemberValue)
                ElseIf TypeOf member Is System.Reflection.PropertyInfo Then
                    CType(member, System.Reflection.PropertyInfo).SetValue(target, _MemberValue, Nothing)
                End If
            End If

            Return _MemberValue
        End Function

        ''' <summary>
        ''' Creates an instance of the class.
        ''' </summary>
        ''' <param name="classAssembly">The assembly where the class to create is stored.</param>
        ''' <param name="className">The fullname of the class to create.</param>
        ''' <returns>If successful the instantiated class, otherwise nothing.</returns>
        ''' <remarks></remarks>
        Private Function InstantiateMember(ByRef classAssembly As System.Reflection.Assembly, ByVal className As String) As Object
            Dim _Value As Object = Nothing

            'Create a New Instance of the Object
            Try
                _Value = classAssembly.CreateInstance(className)
            Catch missingDefaultConstructorException As System.MissingMethodException
                'This error is generated by System.String.
                'It can also occur if the class is missing a default constructor.
            Catch unhandledException As System.Exception
                Throw unhandledException
            End Try

            'We have to verify that the value returned isn't a data type.
            'For example, string values require a parameter for the constructor
            'but work just fine without initialization.
            If _Value Is Nothing _
            AndAlso Not IsDataType(_Value) _
            AndAlso Not IsDataType(className, True) Then
                'The object to instantiate did not exist in the same assembly namespace.
                'Attempt to find the class and instantiate it.
                'This scenario happens with System namespace objects like System.Collections.ArrayList.
                _Value = System.Type.GetType(className).Assembly.CreateInstance(className)
            End If

            If _Value Is Nothing _
            AndAlso Not IsDataType(_Value) _
            AndAlso Not IsDataType(className, True) Then
                'The member did not have a value and we were unable to instantiate a new class to assign to it.
                Throw New System.MissingMethodException("Unable to deserialize Xml into " & className & ". Failed to initialize the object. Verify that the member's type supports a default constructor or that the member is automatically instantiated when the parent class is instantiated.")
            End If

            Return _Value
        End Function

        ''' <summary>
        ''' Assigns a casted value to the target object.
        ''' </summary>
        ''' <param name="target">The object to update.</param>
        ''' <param name="valueType">The type to cast the value to.</param>
        ''' <param name="dataValue">The value to cast and assign.</param>
        ''' <remarks>
        ''' This has to be done to support assignment to values of type object.
        ''' Without this code all values would be assigned as strings.
        ''' </remarks>
        Private Sub SaveValue(ByRef target As Object, ByRef valueType As String, ByVal dataValue As Object)
            Select Case valueType.ToUpper.Trim
                Case "SYSTEM.BOOLEAN", "BOOLEAN", "BOOL" : target = System.Convert.ToBoolean(dataValue)
                Case "SYSTEM.BYTE", "BYTE" : target = System.Convert.ToByte(dataValue)
                Case "SYSTEM.CHAR", "CHAR" : target = System.Convert.ToChar(dataValue)
                Case "SYSTEM.DATE", "DATE" : target = System.Convert.ToDateTime(dataValue)
                Case "SYSTEM.DATETIME", "DATETIME" : target = System.Convert.ToDateTime(dataValue)
                Case "SYSTEM.DECIMAL", "DECIMAL" : target = System.Convert.ToDecimal(dataValue)
                Case "SYSTEM.DOUBLE", "DOUBLE" : target = System.Convert.ToDouble(dataValue)
                Case "SYSTEM.INT16", "INT16" : target = System.Convert.ToInt16(dataValue)
                Case "SYSTEM.INT32", "INT32" : target = System.Convert.ToInt32(dataValue)
                Case "SYSTEM.INT64", "INT64" : target = System.Convert.ToInt64(dataValue)
                Case "SYSTEM.INTEGER", "INTEGER", "INT" : target = System.Convert.ToInt32(dataValue)
                Case "SYSTEM.LONG", "LONG" : target = System.Convert.ToInt64(dataValue)
                Case "SYSTEM.SBYTE", "SBYTE" : target = System.Convert.ToSByte(dataValue)
                Case "SYSTEM.SHORT", "SHORT" : target = System.Convert.ToInt16(dataValue)
                Case "SYSTEM.SINGLE", "SINGLE", "FLOAT" : target = System.Convert.ToSingle(dataValue)
                Case "SYSTEM.STRING", "STRING" : target = System.Convert.ToString(dataValue)
                Case "SYSTEM.UINT16", "UINT16" : target = System.Convert.ToUInt16(dataValue)
                Case "SYSTEM.UINT32", "UINT32" : target = System.Convert.ToUInt32(dataValue)
                Case "SYSTEM.UINT64", "UINT64" : target = System.Convert.ToUInt64(dataValue)
                Case "SYSTEM.UINTPTR", "UINTPTR" : target = CType(dataValue, System.UIntPtr)
                Case "SYSTEM.INTPTR", "INTPTR" : target = CType(dataValue, System.IntPtr)
                Case "SYSTEM.UINTEGER", "UINTEGER", "UINT" : target = System.Convert.ToUInt32(dataValue)
                Case "SYSTEM.ULONG", "ULONG" : target = System.Convert.ToUInt64(dataValue)
                Case "SYSTEM.USHORT", "USHORT" : target = System.Convert.ToUInt16(dataValue)
                Case "SYSTEM.OBJECT", "OBJECT"
                    'FIX: July 13, 2006 - System.Object Support
                    target = dataValue
                Case Else
                    Throw New System.InvalidCastException("An error occurred while assigning a value to " & target.GetType.FullName & ". Casting to type, " & valueType.ToUpper.Trim & ", is not supported.")
            End Select
        End Sub

        ''' <summary>
        ''' Assigns a strongly-typed data type to the member.
        ''' </summary>
        ''' <param name="target">The object containing the field.</param>
        ''' <param name="targetField">A FieldInfo object representing the field to update.</param>
        ''' <param name="dataValue">The value to assign to the field.</param>
        ''' <remarks>
        ''' This has to be done to support assignment to fields of type object.
        ''' Without this code all values would be assigned as strings.
        ''' </remarks>
        Private Sub SaveValue(ByRef target As Object, ByRef targetField As System.Reflection.FieldInfo, ByRef dataValue As System.Object)
            Select Case targetField.FieldType.FullName.ToUpper
                Case "SYSTEM.BOOLEAN", "BOOLEAN", "BOOL" : targetField.SetValue(target, System.Convert.ToBoolean(dataValue))
                Case "SYSTEM.BYTE", "BYTE" : targetField.SetValue(target, System.Convert.ToByte(dataValue))
                Case "SYSTEM.CHAR", "CHAR" : targetField.SetValue(target, System.Convert.ToChar(dataValue))
                Case "SYSTEM.DATE", "DATE" : targetField.SetValue(target, System.Convert.ToDateTime(dataValue))
                Case "SYSTEM.DATETIME", "DATETIME" : targetField.SetValue(target, System.Convert.ToDateTime(dataValue))
                Case "SYSTEM.DECIMAL", "DECIMAL" : targetField.SetValue(target, System.Convert.ToDecimal(dataValue))
                Case "SYSTEM.DOUBLE", "DOUBLE" : targetField.SetValue(target, System.Convert.ToDouble(dataValue))
                Case "SYSTEM.INT16", "INT16" : targetField.SetValue(target, System.Convert.ToInt16(dataValue))
                Case "SYSTEM.INT32", "INT32" : targetField.SetValue(target, System.Convert.ToInt32(dataValue))
                Case "SYSTEM.INT64", "INT64" : targetField.SetValue(target, System.Convert.ToInt64(dataValue))
                Case "SYSTEM.INTEGER", "INTEGER", "INT" : targetField.SetValue(target, System.Convert.ToInt32(dataValue))
                Case "SYSTEM.LONG", "LONG" : targetField.SetValue(target, System.Convert.ToInt64(dataValue))
                Case "SYSTEM.SBYTE", "SBYTE" : targetField.SetValue(target, System.Convert.ToSByte(dataValue))
                Case "SYSTEM.SHORT", "SHORT" : targetField.SetValue(target, System.Convert.ToInt16(dataValue))
                Case "SYSTEM.SINGLE", "SINGLE", "FLOAT" : targetField.SetValue(target, System.Convert.ToSingle(dataValue))
                Case "SYSTEM.STRING", "STRING" : targetField.SetValue(target, System.Convert.ToString(dataValue))
                Case "SYSTEM.UINT16", "UINT16" : targetField.SetValue(target, System.Convert.ToUInt16(dataValue))
                Case "SYSTEM.UINT32", "UINT32" : targetField.SetValue(target, System.Convert.ToUInt32(dataValue))
                Case "SYSTEM.UINT64", "UINT64" : targetField.SetValue(target, System.Convert.ToUInt64(dataValue))
                Case "SYSTEM.UINTPTR", "UINTPTR" : targetField.SetValue(target, CType(dataValue, System.UIntPtr))
                Case "SYSTEM.INTPTR", "INTPTR" : targetField.SetValue(target, CType(dataValue, System.IntPtr))
                Case "SYSTEM.UINTEGER", "UINTEGER", "UINT" : targetField.SetValue(target, System.Convert.ToUInt32(dataValue))
                Case "SYSTEM.ULONG", "ULONG" : targetField.SetValue(target, System.Convert.ToUInt64(dataValue))
                Case "SYSTEM.USHORT", "USHORT" : targetField.SetValue(target, System.Convert.ToUInt16(dataValue))
                Case "SYSTEM.OBJECT", "OBJECT"
                    'FIX: July 13, 2006 - System.Object Support
                    targetField.SetValue(target, dataValue)
                Case Else
                    Throw New System.InvalidCastException("An error occurred while assigning a value to " & target.GetType.FullName & ". Casting to type, " & targetField.FieldType.FullName.ToUpper & ", is not supported.")
            End Select
        End Sub

        ''' <summary>
        ''' Assigns a strongly-typed data type to the member.
        ''' </summary>
        ''' <param name="target">The object containing the property.</param>
        ''' <param name="targetProperty">A PropertyInfo object representing the property to update.</param>
        ''' <param name="dataValue">The value to assign to the property.</param>
        ''' <remarks>
        ''' This has to be done to support assignment to properties of type object.
        ''' Without this code all values would be assigned as strings.
        ''' </remarks>
        Private Sub SaveValue(ByRef target As Object, ByRef targetProperty As System.Reflection.PropertyInfo, ByRef dataValue As System.Object)
            If targetProperty.CanWrite Then
                Select Case targetProperty.PropertyType.FullName.ToUpper
                    Case "SYSTEM.BOOLEAN", "BOOLEAN", "BOOL" : targetProperty.SetValue(target, System.Convert.ToBoolean(dataValue), Nothing)
                    Case "SYSTEM.BYTE", "BYTE" : targetProperty.SetValue(target, System.Convert.ToByte(dataValue), Nothing)
                    Case "SYSTEM.CHAR", "CHAR" : targetProperty.SetValue(target, System.Convert.ToChar(dataValue), Nothing)
                    Case "SYSTEM.DATE", "DATE" : targetProperty.SetValue(target, System.Convert.ToDateTime(dataValue), Nothing)
                    Case "SYSTEM.DATETIME", "DATETIME" : targetProperty.SetValue(target, System.Convert.ToDateTime(dataValue), Nothing)
                    Case "SYSTEM.DECIMAL", "DECIMAL" : targetProperty.SetValue(target, System.Convert.ToDecimal(dataValue), Nothing)
                    Case "SYSTEM.DOUBLE", "DOUBLE" : targetProperty.SetValue(target, System.Convert.ToDouble(dataValue), Nothing)
                    Case "SYSTEM.INT16", "INT16" : targetProperty.SetValue(target, System.Convert.ToInt16(dataValue), Nothing)
                    Case "SYSTEM.INT32", "INT32" : targetProperty.SetValue(target, System.Convert.ToInt32(dataValue), Nothing)
                    Case "SYSTEM.INT64", "INT64" : targetProperty.SetValue(target, System.Convert.ToInt64(dataValue), Nothing)
                    Case "SYSTEM.INTEGER", "INTEGER", "INT" : targetProperty.SetValue(target, System.Convert.ToInt32(dataValue), Nothing)
                    Case "SYSTEM.LONG", "LONG" : targetProperty.SetValue(target, System.Convert.ToInt64(dataValue), Nothing)
                    Case "SYSTEM.SBYTE", "SBYTE" : targetProperty.SetValue(target, System.Convert.ToSByte(dataValue), Nothing)
                    Case "SYSTEM.SHORT", "SHORT" : targetProperty.SetValue(target, System.Convert.ToInt16(dataValue), Nothing)
                    Case "SYSTEM.SINGLE", "SINGLE", "FLOAT" : targetProperty.SetValue(target, System.Convert.ToSingle(dataValue), Nothing)
                    Case "SYSTEM.STRING", "STRING" : targetProperty.SetValue(target, System.Convert.ToString(dataValue), Nothing)
                    Case "SYSTEM.UINT16", "UINT16" : targetProperty.SetValue(target, System.Convert.ToUInt16(dataValue), Nothing)
                    Case "SYSTEM.UINT32", "UINT32" : targetProperty.SetValue(target, System.Convert.ToUInt32(dataValue), Nothing)
                    Case "SYSTEM.UINT64", "UINT64" : targetProperty.SetValue(target, System.Convert.ToUInt64(dataValue), Nothing)
                    Case "SYSTEM.UINTPTR", "UINTPTR" : targetProperty.SetValue(target, CType(dataValue, System.UIntPtr), Nothing)
                    Case "SYSTEM.INTPTR", "INTPTR" : targetProperty.SetValue(target, CType(dataValue, System.IntPtr), Nothing)
                    Case "SYSTEM.UINTEGER", "UINTEGER", "UINT" : targetProperty.SetValue(target, System.Convert.ToUInt32(dataValue), Nothing)
                    Case "SYSTEM.ULONG", "ULONG" : targetProperty.SetValue(target, System.Convert.ToUInt64(dataValue), Nothing)
                    Case "SYSTEM.USHORT", "USHORT" : targetProperty.SetValue(target, System.Convert.ToUInt16(dataValue), Nothing)
                    Case "SYSTEM.OBJECT", "OBJECT"
                        'FIX: July 13, 2006 - System.Object Support
                        targetProperty.SetValue(target, dataValue, Nothing)
                    Case Else
                        Throw New System.InvalidCastException("An error occurred while assigning a value to " & target.GetType.FullName & ". Casting to type, " & targetProperty.PropertyType.FullName.ToUpper & ", is not supported.")
                End Select
            Else
                'Although we serialized the value we can not deserialize it because the property is marked ReadOnly.
            End If
        End Sub

        ''' <summary>
        ''' Executes the add method of the list object when supported.
        ''' </summary>
        ''' <param name="target">The class containing the method to execute.</param>
        ''' <param name="key">The key value for DictionaryEntries. Use Nothing if a type other than DictionaryEntry.</param>
        ''' <param name="dataValue">The value to assign to the class.</param>
        ''' <remarks></remarks>
        Private Function ExecuteAddMethod(ByVal target As Object, ByVal key As Object, ByVal dataValue As Object) As Boolean
            Dim _FoundMethod As Boolean = False

            For Each _ItemMethod As System.Reflection.MemberInfo In target.GetType.GetMethods
                If String.Compare(_ItemMethod.Name, "Add", True) = 0 Then
                    If key Is Nothing Then
                        target.GetType.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {dataValue})
                    Else
                        target.GetType.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {key, dataValue})
                    End If

                    _FoundMethod = True
                    Exit For
                End If
            Next

            Return _FoundMethod
        End Function

        ''' <summary>
        ''' Executes the Enqueue method of the list object when supported.
        ''' </summary>
        ''' <param name="target">The class containing the method to execute.</param>
        ''' <param name="key">The key value for DictionaryEntries. Use Nothing if a type other than DictionaryEntry.</param>
        ''' <param name="dataValue">The value to assign to the class.</param>
        ''' <remarks></remarks>
        Private Function ExecuteEnqueueMethod(ByVal target As Object, ByVal key As Object, ByVal dataValue As Object) As Boolean
            Dim _FoundMethod As Boolean = False

            For Each _ItemMethod As System.Reflection.MemberInfo In target.GetType.GetMethods
                If String.Compare(_ItemMethod.Name, "Enqueue", True) = 0 Then
                    If key Is Nothing Then
                        target.GetType.InvokeMember("Enqueue", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {dataValue})
                    Else
                        target.GetType.InvokeMember("Enqueue", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {key, dataValue})
                    End If

                    _FoundMethod = True
                    Exit For
                End If
            Next

            Return _FoundMethod
        End Function

        ''' <summary>
        ''' Executes the Push method of the list object when supported.
        ''' </summary>
        ''' <param name="target">The class containing the method to execute.</param>
        ''' <param name="key">The key value for DictionaryEntries. Use Nothing if a type other than DictionaryEntry.</param>
        ''' <param name="dataValue">The value to assign to the class.</param>
        ''' <remarks></remarks>
        Private Function ExecutePushMethod(ByVal target As Object, ByVal key As Object, ByVal dataValue As Object) As Boolean
            Dim _FoundMethod As Boolean = False

            For Each _ItemMethod As System.Reflection.MemberInfo In target.GetType.GetMethods
                If String.Compare(_ItemMethod.Name, "Push", True) = 0 Then
                    If key Is Nothing Then
                        target.GetType.InvokeMember("Push", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {dataValue})
                    Else
                        target.GetType.InvokeMember("Push", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, New Object() {key, dataValue})
                    End If

                    _FoundMethod = True
                    Exit For
                End If
            Next

            Return _FoundMethod
        End Function

        ''' <summary>
        ''' Executes the clear method of the list object when supported.
        ''' </summary>
        ''' <param name="target">The class containing the method to execute.</param>
        ''' <remarks></remarks>
        Private Sub ExecuteClearMethod(ByVal target As Object)
            For Each _ItemMethod As System.Reflection.MemberInfo In target.GetType.GetMethods
                If String.Compare(_ItemMethod.Name, "Clear", True) = 0 Then
                    target.GetType.InvokeMember("Clear", System.Reflection.BindingFlags.InvokeMethod, Nothing, target, Nothing)
                    'FIX: July 13, 2006 - Efficiency Change
                    Exit For
                End If
            Next
        End Sub
#End Region
    End Class



End Namespace