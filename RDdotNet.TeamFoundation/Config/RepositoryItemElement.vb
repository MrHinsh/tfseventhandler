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
    <DataContract()> _
    Public Class RepositoryItemElement
        Inherits ConfigurationElement

        ''' <summary>
        ''' The type of the System to use.
        ''' </summary>
        <DataMember(), ConfigurationProperty("localPath", IsRequired:=True)> _
        Public Property LocalPath() As String
            Get
                Dim path As String = CType(Me("localPath"), String)
                If path.StartsWith("~\") Then
                    path = path.Replace("~\", "")
                    path = System.IO.Path.Combine(My.Application.Info.DirectoryPath, path)
                End If
                Return path
            End Get
            Set(ByVal value As String)
                Me("localPath") = value
            End Set
        End Property


    End Class


End Namespace