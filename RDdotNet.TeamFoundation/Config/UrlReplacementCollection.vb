'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright � Microsoft Corporation.  All rights reserved.
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
    Public Class UrlReplacementItemCollection
        Inherits ConfigurationElementCollection

        Protected Overloads Overrides Function CreateNewElement() As System.Configuration.ConfigurationElement
            Return New UrlReplacementItemElement
        End Function

        Protected Overrides Function GetElementKey(ByVal element As System.Configuration.ConfigurationElement) As Object
            Return CType(element, UrlReplacementItemElement).EventType.ToString & CType(element, UrlReplacementItemElement).Old
        End Function

    End Class


End Namespace