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
Imports System.Configuration
Imports System.Runtime.Serialization


Namespace Config

    ''' <summary>
    ''' Definition of the configuration section for the block.
    ''' </summary>
    <DataContract()> _
    Public Class SettingsSection
        Inherits ConfigurationSection

        Private Shared innerConfiguration As SettingsSection

        Public Shared ReadOnly Property Instance() As SettingsSection
            Get
                If innerConfiguration Is Nothing Then
                    innerConfiguration = LoadConfiguration()
                End If
                Return innerConfiguration
            End Get
        End Property

        Private Shared Function LoadConfiguration() As SettingsSection
            Dim section As Object = ConfigurationManager.GetSection(SettingsSection.SectionName)
            Dim configSection As SettingsSection = TryCast(section, SettingsSection)

            If Not section Is Nothing AndAlso configSection Is Nothing Then
                Throw New ConfigurationErrorsException
            End If

            If configSection Is Nothing Then
                configSection = New SettingsSection()
            End If
            Return configSection
        End Function

        ''' <summary>
        ''' The configuration section name for this section.
        ''' </summary>
        Public Const SectionName As String = "MerrillLynch.TeamFoundation"

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Services", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(ConfigurationElementCollection(Of ServiceItemElement)), AddItemName:="Service")> _
        Public ReadOnly Property Services() As ConfigurationElementCollection(Of ServiceItemElement)
            Get
                Return CType(Me("Services"), ConfigurationElementCollection(Of ServiceItemElement))
            End Get
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Repository", IsRequired:=True)> _
        Public ReadOnly Property Repository() As RepositoryItemElement
            Get
                Return CType(Me("Repository"), RepositoryItemElement)
            End Get
        End Property

    End Class


End Namespace
