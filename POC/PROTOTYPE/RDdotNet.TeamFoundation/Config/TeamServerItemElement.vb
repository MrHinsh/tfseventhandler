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
    Public Class TeamServerItemElement
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
        <ConfigurationProperty("subscriber", IsRequired:=True)> _
        Public Property Subscriber() As String
            Get
                Return CType(Me("subscriber"), String)
            End Get
            Set(ByVal value As String)
                Me("subscriber") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("mailFromName", IsRequired:=True)> _
        Public Property MailFromName() As String
            Get
                Return CType(Me("mailFromName"), String)
            End Get
            Set(ByVal value As String)
                Me("mailFromName") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("mailServer", IsRequired:=True)> _
        Public Property MailServer() As String
            Get
                Return CType(Me("mailServer"), String)
            End Get
            Set(ByVal value As String)
                Me("mailServer") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("mailAddressFrom", IsRequired:=True)> _
        Public Property MailAddressFrom() As String
            Get
                Return CType(Me("mailAddressFrom"), String)
            End Get
            Set(ByVal value As String)
                Me("mailAddressFrom") = value
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
        <ConfigurationProperty("url", IsRequired:=True)> _
        Public Property Url() As System.Uri
            Get
                Return CType(Me("url"), System.Uri)
            End Get
            Set(ByVal value As System.Uri)
                Me("url") = value
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