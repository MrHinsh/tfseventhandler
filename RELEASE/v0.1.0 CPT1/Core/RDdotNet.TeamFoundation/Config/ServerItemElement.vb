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
    <DataContract(), Serializable()> _
    Public Class ServerItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

        Public Sub New()
            MyBase.New()
        End Sub


        Public ReadOnly Property Key() As String Implements IConfigurationElement.Key
            Get
                Return Me.Name
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("name", IsRequired:=True)> _
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
        <DataMember(), ConfigurationProperty("uri", IsRequired:=True)> _
        Public Property Uri() As Uri
            Get
                Return CType(Me("uri"), Uri)
            End Get
            Set(ByVal value As Uri)
                Me("uri") = value
            End Set
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Credentials", IsRequired:=False)> _
        Public Property Credentials() As CredentialsItemElement
            Get
                Return CType(Me("Credentials"), CredentialsItemElement)
            End Get
            Set(ByVal value As CredentialsItemElement)
                Me("Credentials") = value
            End Set
        End Property



    End Class

End Namespace
