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
    Public Class UrlReplacementItemElement
        Inherits ConfigurationElement

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("eventType", IsRequired:=True)> _
        Public Property EventType() As EventTypes
            Get
                Return CType(Me("eventType"), EventTypes)
            End Get
            Set(ByVal value As EventTypes)
                Me("eventType") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("old", IsRequired:=True)> _
        Public Property Old() As String
            Get
                Return CType(Me("old"), String)
            End Get
            Set(ByVal value As String)
                Me("old") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("new", IsRequired:=True)> _
        Public Property [New]() As String
            Get
                Return CType(Me("new"), String)
            End Get
            Set(ByVal value As String)
                Me("new") = value
            End Set
        End Property

    End Class


End Namespace