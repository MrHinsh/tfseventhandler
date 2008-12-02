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
    Public Class EventItemElement
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
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <ConfigurationProperty("Handlers", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(HandlerItemCollection), AddItemName:="Handler")> _
        Public ReadOnly Property HandlerItems() As HandlerItemCollection
            Get
                Return CType(Me("Handlers"), HandlerItemCollection)
            End Get
        End Property

    End Class


End Namespace