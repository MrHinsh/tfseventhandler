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
Imports System.Runtime.Serialization

Namespace Config


    ''' <summary>
    ''' Contains the definition of a visualization.
    ''' </summary>
    Public Class MailItemElement
        Inherits ConfigurationElement

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("mailFromName", IsRequired:=True)> _
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
        <DataMember(), ConfigurationProperty("mailServer", IsRequired:=True)> _
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
        <DataMember(), ConfigurationProperty("mailAddressFrom", IsRequired:=True)> _
        Public Property MailAddressFrom() As String
            Get
                Return CType(Me("mailAddressFrom"), String)
            End Get
            Set(ByVal value As String)
                Me("mailAddressFrom") = value
            End Set
        End Property

    End Class


End Namespace