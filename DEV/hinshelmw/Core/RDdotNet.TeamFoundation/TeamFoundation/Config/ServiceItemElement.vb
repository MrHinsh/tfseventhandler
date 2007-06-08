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
Imports RDdotNet.Config

Namespace TeamFoundation.Config


    ''' <summary>
    ''' Contains the definition of a visualization.
    ''' </summary>
    <DataContract()> _
    Public Class ServiceItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

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
        <DataMember(), ConfigurationProperty("subscriber", IsRequired:=True)> _
        Public Property Subscriber() As String
            Get
                Return CType(Me("subscriber"), String)
            End Get
            Set(ByVal value As String)
                Me("subscriber") = value
            End Set
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Debug", IsRequired:=True)> _
        Public ReadOnly Property Debug() As DebugItemElement
            Get
                Return CType(Me("Debug"), DebugItemElement)
            End Get
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Mail", IsRequired:=True)> _
        Public ReadOnly Property Mail() As MailItemElement
            Get
                Return CType(Me("Mail"), MailItemElement)
            End Get
        End Property

    End Class


End Namespace