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
Imports RDdotNet.TeamFoundation.Services.DataContracts
Imports System.Collections.ObjectModel


Namespace Config

    ''' <summary>
    ''' Definition of the configuration section for the block.
    ''' </summary>
    <DataContract()> _
    Public Class TeamFoundationSettingsSection
        Inherits ConfigurationSection

#Region " Singleton "

        Private Shared innerSection As TeamFoundationSettingsSection
        Private Shared innerConfig As System.Configuration.Configuration

        ''' <summary>
        ''' The configuration section name for this section.
        ''' </summary>
        Public Const SectionName As String = "RDdotNet.TeamFoundation"

        Public Shared ReadOnly Property Instance() As TeamFoundationSettingsSection
            Get
                If innerSection Is Nothing Then
                    innerSection = LoadConfiguration()
                End If
                Return innerSection
            End Get
        End Property

        Private Shared Function LoadConfiguration() As TeamFoundationSettingsSection
            innerConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration("RDdotNet.TeamFoundation.dll")
            Dim section As Object = innerConfig.GetSection(TeamFoundationSettingsSection.SectionName)
            Dim configSection As TeamFoundationSettingsSection = TryCast(section, TeamFoundationSettingsSection)
            If Not section Is Nothing AndAlso configSection Is Nothing Then
                Throw New ConfigurationErrorsException
            End If
            If configSection Is Nothing Then
                configSection = New TeamFoundationSettingsSection()
            End If
            Return configSection
        End Function

        Public Shared Sub Save()
            innerConfig.Save()
        End Sub

#End Region

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("Servers", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(ConfigurationElementCollection(Of ServerItemElement)), AddItemName:="Server")> _
        Public ReadOnly Property Servers() As ConfigurationElementCollection(Of ServerItemElement)
            Get
                Return CType(Me("Servers"), ConfigurationElementCollection(Of ServerItemElement))
            End Get
        End Property

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

        ''' <summary>
        ''' List of  services that will be initialized on the host.
        ''' </summary>
        <DataMember(), ConfigurationProperty("HandlerAssemblies", IsRequired:=True, IsDefaultCollection:=False), ConfigurationCollection(GetType(ConfigurationElementCollection(Of AssemblyItemElement)), AddItemName:="Assembly")> _
        Public ReadOnly Property HandlerAssemblies() As ConfigurationElementCollection(Of AssemblyItemElement)
            Get
                Return CType(Me("HandlerAssemblies"), ConfigurationElementCollection(Of AssemblyItemElement))
            End Get
        End Property

    End Class


End Namespace
