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
    Public Class BaseAddressItemElement
        Inherits ConfigurationElement


        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("url", IsRequired:=True)> _
        Public Property Url() As String
            Get
                Return CType(Me("url"), String)
            End Get
            Set(ByVal value As String)
                Me("url") = value
            End Set
        End Property

    End Class


End Namespace