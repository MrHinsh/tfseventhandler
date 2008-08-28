﻿'===============================================================================
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
    <DataContract()> _
    Public Class CredentialsItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

        Public ReadOnly Property Key() As String Implements IConfigurationElement.Key
            Get
                Return Me.Username
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("username", IsRequired:=True)> _
        Public Property Username() As String
            Get
                Return CType(Me("username"), String)
            End Get
            Set(ByVal value As String)
                Me("username") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("password", IsRequired:=True)> _
        Public Property Password() As String
            Get
                Return CType(Me("password"), String)
            End Get
            Set(ByVal value As String)
                Me("password") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("domain", IsRequired:=True)> _
        Public Property Domain() As String
            Get
                Return CType(Me("domain"), String)
            End Get
            Set(ByVal value As String)
                Me("domain") = value
            End Set
        End Property


    End Class

End Namespace
