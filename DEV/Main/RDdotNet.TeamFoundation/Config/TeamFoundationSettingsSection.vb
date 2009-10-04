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


Namespace Config

    ''' <summary>
    ''' Definition of the configuration section for the block.
    ''' </summary>
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

        Public Shared ReadOnly Property ServiceInstance() As SettingsSection
            Get
                If innerConfiguration Is Nothing Then
                    innerConfiguration = LoadServiceConfiguration()
                End If
                Return innerConfiguration
            End Get
        End Property

        Private Shared Function LoadServiceConfiguration() As SettingsSection
            ' Open App.Config of executable
            Dim filemap As New ExeConfigurationFileMap
            filemap.ExeConfigFilename = "Hinshelwood.TFSEventHandler.exe"
            Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None)

            Dim section As Object = config.GetSection("Hinshelwood.TFSEventHandler")
            Dim configSection As SettingsSection = TryCast(section, SettingsSection)

            If Not section Is Nothing AndAlso configSection Is Nothing Then
                Throw New ConfigurationErrorsException
            End If

            If configSection Is Nothing Then
                configSection = New SettingsSection()
            End If
            Return configSection
        End Function

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
        Public Const SectionName As String = "Hinshelwood.TeamFoundation"

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <ConfigurationProperty("TeamServers", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(TeamServerItemCollection), AddItemName:="TeamServer")> _
        Public ReadOnly Property TeamServerItems() As TeamServerItemCollection
            Get
                Return CType(Me("TeamServers"), TeamServerItemCollection)
            End Get
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <ConfigurationProperty("Events", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(EventItemCollection), AddItemName:="Event")> _
        Public ReadOnly Property EventItems() As EventItemCollection
            Get
                Return CType(Me("Events"), EventItemCollection)
            End Get
        End Property

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <ConfigurationProperty("BaseAddress", IsRequired:=True)> _
        Public ReadOnly Property BaseAddress() As BaseAddressItemElement
            Get
                Return CType(Me("BaseAddress"), BaseAddressItemElement)
            End Get
        End Property

        ''' <summary>
        ''' List of  url repalcements
        ''' </summary>
        <ConfigurationProperty("UrlReplacements", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(UrlReplacementItemCollection), AddItemName:="Replace")> _
        Public ReadOnly Property UrlReplacementItems() As UrlReplacementItemCollection
            Get
                Return CType(Me("UrlReplacements"), UrlReplacementItemCollection)
            End Get
        End Property

    End Class


End Namespace
