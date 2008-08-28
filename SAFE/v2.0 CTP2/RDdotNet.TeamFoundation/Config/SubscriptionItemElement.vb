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
    Public Class SubscriptionItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

        Public Sub New()
            MyBase.New()
        End Sub


        Public ReadOnly Property Key() As String Implements IConfigurationElement.Key
            Get
                Return Me.ID.ToString
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("id", IsRequired:=True)> _
        Public Property ID() As Integer
            Get
                Return CType(Me("id"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("id") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("eventType", IsRequired:=True)> _
        Public Property EventType() As String
            Get
                Return CType(Me("eventType"), String)
            End Get
            Set(ByVal value As String)
                Me("eventType") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("conditionString", IsRequired:=True)> _
        Public Property ConditionString() As String
            Get
                Return CType(Me("conditionString"), String)
            End Get
            Set(ByVal value As String)
                Me("conditionString") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("device", IsRequired:=True)> _
        Public Property Device() As String
            Get
                Return CType(Me("device"), String)
            End Get
            Set(ByVal value As String)
                Me("device") = value
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
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("tag", IsRequired:=True)> _
        Public Property Tag() As String
            Get
                Return CType(Me("tag"), String)
            End Get
            Set(ByVal value As String)
                Me("tag") = value
            End Set
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("DeliveryPreference", IsRequired:=True)> _
        Public Property DeliveryPreference() As DeliveryPreferenceItemElement
            Get
                Return CType(Me("DeliveryPreference"), DeliveryPreferenceItemElement)
            End Get
            Set(ByVal value As DeliveryPreferenceItemElement)
                Me("DeliveryPreference") = value
            End Set
        End Property



    End Class

End Namespace
