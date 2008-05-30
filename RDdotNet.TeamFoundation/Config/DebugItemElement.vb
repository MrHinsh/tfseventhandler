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
    Public Class DebugItemElement
        Inherits ConfigurationElement

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("name", IsRequired:=True)> _
        Public Property Name() As String
            Get
                Return CType(Me("name"), String)
            End Get
            Set(ByVal value As String)
                Me("name") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("eventLogPath", IsRequired:=True)> _
        Public Property EventLogPath() As String
            Get
                Return CType(Me("eventLogPath"), String)
            End Get
            Set(ByVal value As String)
                Me("eventLogPath") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("logEvents", IsRequired:=True)> _
        Public Property LogEvents() As Boolean
            Get
                Return CType(Me("logEvents"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("logEvents") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("verbose", IsRequired:=True)> _
        Public Property Verbose() As Boolean
            Get
                Return CType(Me("verbose"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("verbose") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("testEmail", IsRequired:=True)> _
        Public Property TestEmail() As String
            Get
                Return CType(Me("testEmail"), String)
            End Get
            Set(ByVal value As String)
                Me("testEmail") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("testMode", IsRequired:=True)> _
        Public Property TestMode() As Boolean
            Get
                Return CType(Me("testMode"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("testMode") = value
            End Set
        End Property


    End Class


End Namespace