'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright © Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.ComponentModel

Namespace Config


    ''' <summary>
    ''' Contains the definition of a visualization.
    ''' </summary>
    Public Class HandlerItemElement(Of THandlerConfig)
        Inherits ConfigurationElement

        '<ConfigurationProperty("Name", IsRequired:=True, iskey:=True, DefaultValue:="No Name.")> _
        'Public Property Name() As String
        '    Get
        '        Return CStr(Me("Name"))
        '    End Get
        '    Set(ByVal value As String)
        '        Me("Name") = value
        '    End Set
        'End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("type", IsRequired:=True)> _
        Public Property Type() As String
            Get
                Return CType(Me("type"), String)
            End Get
            Set(ByVal value As String)
                Me("type") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("assemblyFileName", IsRequired:=True)> _
        Public Property AssemblyFileName() As String
            Get
                Return CType(Me("assemblyFileName"), String)
            End Get
            Set(ByVal value As String)
                Me("assemblyFileName") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("assemblyFileLocation", IsRequired:=False)> _
        Public Property AssemblyFileLocation() As String
            Get
                Dim x As String = CType(Me("assemblyFileLocation"), String)
                If x.StartsWith("~") Then
                    x = x.Replace("~", My.Application.Info.DirectoryPath)
                End If
                Return x
            End Get
            Set(ByVal value As String)
                Me("assemblyFileLocation") = value
            End Set
        End Property

        '''' <summary>
        '''' The type of the System to use.
        '''' </summary>
        '<ConfigurationProperty("HandlerConfig", IsRequired:=False)> _
        'Public Property HandlerConfig() As THandlerConfig
        '    Get
        '        Return CType(Me("HandlerConfig"), THandlerConfig)
        '    End Get
        '    Set(ByVal value As THandlerConfig)
        '        Me("HandlerConfig") = value
        '    End Set
        'End Property

    End Class


End Namespace