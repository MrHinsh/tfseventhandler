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
    Public Class DeliveryPreferenceItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

        Public Sub New()
            MyBase.New()
        End Sub


        Public ReadOnly Property Key() As String Implements IConfigurationElement.Key
            Get
                Return Me.Address
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("address", IsRequired:=True)> _
        Public Property Address() As String
            Get
                Return CType(Me("address"), String)
            End Get
            Set(ByVal value As String)
                Me("address") = value
            End Set
        End Property


        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("schedule", IsRequired:=True)> _
        Public Property Schedule() As String
            Get
                Return CType(Me("schedule"), String)
            End Get
            Set(ByVal value As String)
                Me("schedule") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("type", IsRequired:=True)> _
        Public Property Type() As String
            Get
                Return CType(Me("type"), String)
            End Get
            Set(ByVal value As String)
                Me("type") = value
            End Set
        End Property

    End Class

End Namespace
