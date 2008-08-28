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
    Public Class AssemblyItemElement
        Inherits ConfigurationElement
        Implements IConfigurationElement

        Public ReadOnly Property Key() As String Implements IConfigurationElement.Key
            Get
                Return AssemblyFileName
            End Get
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("assemblyFileName", IsRequired:=True)> _
        Public Property AssemblyFileName() As String
            Get
                Return CType(Me("assemblyFileName"), String)
            End Get
            Set(ByVal value As String)
                Me("assemblyFileName") = value
            End Set
        End Property

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <ConfigurationProperty("assemblyFileLocation", IsRequired:=False)> _
        Public Property AssemblyFileLocation() As String
            Get
                Dim path As String = CType(Me("assemblyFileLocation"), String)
                If path.StartsWith("~\") Then
                    path = path.Replace("~\", "")
                    path = System.IO.Path.Combine(TeamFoundationSettingsSection.Instance.Repository.LocalPath, path)
                End If
                Return path
            End Get
            Set(ByVal value As String)
                Me("assemblyFileLocation") = value
            End Set
        End Property


    End Class


End Namespace