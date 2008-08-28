﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Runtime.Serialization

Namespace EventAdmin
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/RDdotNet.TeamFoundation")>  _
    Public Enum EventTypes As Integer
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Unknown = 0
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        AclChangedEven = 1
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Branchmovedevent = 2
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        BuildCompletionEvent = 3
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        BuildStatusChangeEvent = 4
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        CommonStructureChangedEvent = 5
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        DataChangedEvent = 6
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        IdentityChangedEvent = 7
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        IdentityCreatedEvent = 8
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        IdentityDeletedEvent = 9
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        MembershipChangedEvent = 10
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        WorkItemChangedEvent = 11
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        CheckinEvent = 12
    End Enum
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/RDdotNet.TeamFoundation"),  _
     System.SerializableAttribute()>  _
    Partial Public Class Subscription
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private AddressField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private EventTypeField As EventAdmin.EventTypes
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private IDField As Integer
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Address() As String
            Get
                Return Me.AddressField
            End Get
            Set
                Me.AddressField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property EventType() As EventAdmin.EventTypes
            Get
                Return Me.EventTypeField
            End Get
            Set
                Me.EventTypeField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property ID() As Integer
            Get
                Return Me.IDField
            End Get
            Set
                Me.IDField = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/RDdotNet.TeamFoundation"),  _
     System.SerializableAttribute()>  _
    Partial Public Class AssemblyItem
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private EventHandlersField As System.ComponentModel.BindingList(Of EventAdmin.EventHandlerItem)
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private IDField As System.Guid
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private LocationField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private NameField As System.Reflection.AssemblyName
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property EventHandlers() As System.ComponentModel.BindingList(Of EventAdmin.EventHandlerItem)
            Get
                Return Me.EventHandlersField
            End Get
            Set
                Me.EventHandlersField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property ID() As System.Guid
            Get
                Return Me.IDField
            End Get
            Set
                Me.IDField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Location() As String
            Get
                Return Me.LocationField
            End Get
            Set
                Me.LocationField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Name() As System.Reflection.AssemblyName
            Get
                Return Me.NameField
            End Get
            Set
                Me.NameField = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/RDdotNet.TeamFoundation"),  _
     System.SerializableAttribute()>  _
    Partial Public Class EventHandlerItem
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private EventTypeField As EventAdmin.EventTypes
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private HandlerTypeField As System.Type
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property EventType() As EventAdmin.EventTypes
            Get
                Return Me.EventTypeField
            End Get
            Set
                Me.EventTypeField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property HandlerType() As System.Type
            Get
                Return Me.HandlerTypeField
            End Get
            Set
                Me.HandlerTypeField = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/System.Reflection.Cache"),  _
     System.SerializableAttribute()>  _
    Partial Public Class InternalCache
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        Private m_cacheField As System.ComponentModel.BindingList(Of EventAdmin.InternalCacheItem)
        
        Private m_numItemsField As Integer
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property m_cache() As System.ComponentModel.BindingList(Of EventAdmin.InternalCacheItem)
            Get
                Return Me.m_cacheField
            End Get
            Set
                Me.m_cacheField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property m_numItems() As Integer
            Get
                Return Me.m_numItemsField
            End Get
            Set
                Me.m_numItemsField = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/System.Reflection.Cache"),  _
     System.SerializableAttribute(),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.Reflection.MemberInfo)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.Reflection.AssemblyName)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.InternalCache)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.ComponentModel.BindingList(Of EventAdmin.InternalCacheItem))),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.CacheObjType)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.EventTypes)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.ComponentModel.BindingList(Of EventAdmin.Subscription))),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.Subscription)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.AssemblyItem)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.ComponentModel.BindingList(Of EventAdmin.EventHandlerItem))),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.EventHandlerItem)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(EventAdmin.AssemblyManaifest)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.ComponentModel.BindingList(Of EventAdmin.AssemblyItem))),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.Type)),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(System.ComponentModel.BindingList(Of String)))>  _
    Partial Public Structure InternalCacheItem
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        Private KeyField As EventAdmin.CacheObjType
        
        Private ValueField As Object
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property Key() As EventAdmin.CacheObjType
            Get
                Return Me.KeyField
            End Get
            Set
                Me.KeyField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property Value() As Object
            Get
                Return Me.ValueField
            End Get
            Set
                Me.ValueField = value
            End Set
        End Property
    End Structure
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/System.Reflection.Cache")>  _
    Public Enum CacheObjType As Integer
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        EmptyElement = 0
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        ParameterInfo = 1
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        TypeName = 2
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        RemotingData = 3
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        SerializableAttribute = 4
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        AssemblyName = 5
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        ConstructorInfo = 6
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        FieldType = 7
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        FieldName = 8
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        DefaultMember = 9
    End Enum
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://schemas.datacontract.org/2004/07/RDdotNet.TeamFoundation"),  _
     System.SerializableAttribute()>  _
    Partial Public Class AssemblyManaifest
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private AssemblysField As System.ComponentModel.BindingList(Of EventAdmin.AssemblyItem)
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private VersionField As Integer
        
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Assemblys() As System.ComponentModel.BindingList(Of EventAdmin.AssemblyItem)
            Get
                Return Me.AssemblysField
            End Get
            Set
                Me.AssemblysField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Version() As Integer
            Get
                Return Me.VersionField
            End Get
            Set
                Me.VersionField = value
            End Set
        End Property
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin", ConfigurationName:="EventAdmin.INotificationAdmin", CallbackContract:=GetType(EventAdmin.INotificationAdminCallback))>  _
    Public Interface INotificationAdmin
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin/INotifica"& _ 
            "tionAdmin/GetEventType", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin/INotifica"& _ 
            "tionAdmin/GetEventTypeResponse")>  _
        Function GetEventType() As EventAdmin.EventTypes
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin/INotifica"& _ 
            "tionAdmin/GetLocal", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin/INotifica"& _ 
            "tionAdmin/GetLocalResponse")>  _
        Function GetLocal() As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface INotificationAdminCallback
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/NotificationAdmin/INotifica"& _ 
            "tionAdmin/ForNoReason")>  _
        Sub ForNoReason()
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface INotificationAdminChannel
        Inherits EventAdmin.INotificationAdmin, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Partial Public Class NotificationAdminClient
        Inherits System.ServiceModel.DuplexClientBase(Of EventAdmin.INotificationAdmin)
        Implements EventAdmin.INotificationAdmin
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext)
            MyBase.New(callbackInstance)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String)
            MyBase.New(callbackInstance, endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub
        
        Public Function GetEventType() As EventAdmin.EventTypes Implements EventAdmin.INotificationAdmin.GetEventType
            Return MyBase.Channel.GetEventType
        End Function
        
        Public Function GetLocal() As String Implements EventAdmin.INotificationAdmin.GetLocal
            Return MyBase.Channel.GetLocal
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03", ConfigurationName:="EventAdmin.ITeamServerAdmin", CallbackContract:=GetType(EventAdmin.ITeamServerAdminCallback))>  _
    Public Interface ITeamServerAdmin
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/AddServer")>  _
        Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String)
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/RemoveServer")>  _
        Sub RemoveServer(ByVal TeamServerName As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/ServceUrl", ReplyAction:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/ServceUrlResponse")>  _
        Function ServceUrl() As System.Uri
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/GetServers", ReplyAction:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/GetServersResponse")>  _
        Function GetServers() As System.ComponentModel.BindingList(Of String)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface ITeamServerAdminCallback
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2007/04/Services/TeamServerAdmin/03/ITeamSer"& _ 
            "verAdmin/Updated")>  _
        Sub Updated(ByVal TeamServers As System.ComponentModel.BindingList(Of String))
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface ITeamServerAdminChannel
        Inherits EventAdmin.ITeamServerAdmin, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Partial Public Class TeamServerAdminClient
        Inherits System.ServiceModel.DuplexClientBase(Of EventAdmin.ITeamServerAdmin)
        Implements EventAdmin.ITeamServerAdmin
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext)
            MyBase.New(callbackInstance)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String)
            MyBase.New(callbackInstance, endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub
        
        Public Sub AddServer(ByVal TeamServerName As String, ByVal TeamServerUri As String) Implements EventAdmin.ITeamServerAdmin.AddServer
            MyBase.Channel.AddServer(TeamServerName, TeamServerUri)
        End Sub
        
        Public Sub RemoveServer(ByVal TeamServerName As String) Implements EventAdmin.ITeamServerAdmin.RemoveServer
            MyBase.Channel.RemoveServer(TeamServerName)
        End Sub
        
        Public Function ServceUrl() As System.Uri Implements EventAdmin.ITeamServerAdmin.ServceUrl
            Return MyBase.Channel.ServceUrl
        End Function
        
        Public Function GetServers() As System.ComponentModel.BindingList(Of String) Implements EventAdmin.ITeamServerAdmin.GetServers
            Return MyBase.Channel.GetServers
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin", ConfigurationName:="EventAdmin.ISubscriptionAdmin", CallbackContract:=GetType(EventAdmin.ISubscriptionAdminCallback))>  _
    Public Interface ISubscriptionAdmin
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin/ISubscrip"& _ 
            "tionAdmin/AddSubscriptions")>  _
        Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As EventAdmin.EventTypes)
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin/ISubscrip"& _ 
            "tionAdmin/RemoveSubscriptions")>  _
        Sub RemoveSubscriptions(ByVal ServiceUrl As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin/ISubscrip"& _ 
            "tionAdmin/GetSubscriptions", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin/ISubscrip"& _ 
            "tionAdmin/GetSubscriptionsResponse")>  _
        Function GetSubscriptions() As System.ComponentModel.BindingList(Of EventAdmin.Subscription)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface ISubscriptionAdminCallback
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/SubscriptionAdmin/ISubscrip"& _ 
            "tionAdmin/Updated")>  _
        Sub Updated(ByVal Subscriptions As System.ComponentModel.BindingList(Of EventAdmin.Subscription))
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface ISubscriptionAdminChannel
        Inherits EventAdmin.ISubscriptionAdmin, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Partial Public Class SubscriptionAdminClient
        Inherits System.ServiceModel.DuplexClientBase(Of EventAdmin.ISubscriptionAdmin)
        Implements EventAdmin.ISubscriptionAdmin
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext)
            MyBase.New(callbackInstance)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String)
            MyBase.New(callbackInstance, endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub
        
        Public Sub AddSubscriptions(ByVal ServiceUrl As String, ByVal EventType As EventAdmin.EventTypes) Implements EventAdmin.ISubscriptionAdmin.AddSubscriptions
            MyBase.Channel.AddSubscriptions(ServiceUrl, EventType)
        End Sub
        
        Public Sub RemoveSubscriptions(ByVal ServiceUrl As String) Implements EventAdmin.ISubscriptionAdmin.RemoveSubscriptions
            MyBase.Channel.RemoveSubscriptions(ServiceUrl)
        End Sub
        
        Public Function GetSubscriptions() As System.ComponentModel.BindingList(Of EventAdmin.Subscription) Implements EventAdmin.ISubscriptionAdmin.GetSubscriptions
            Return MyBase.Channel.GetSubscriptions
        End Function
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin", ConfigurationName:="EventAdmin.IEventHandlerAdmin", CallbackContract:=GetType(EventAdmin.IEventHandlerAdminCallback))>  _
    Public Interface IEventHandlerAdmin
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/AddAssembly")>  _
        Sub AddAssembly(ByVal AssemblyItem As EventAdmin.AssemblyItem)
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/AddAssemblyDirect")>  _
        Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte)
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/RemoveAssembly")>  _
        Sub RemoveAssembly(ByVal ID As Integer)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/GetAssemblys", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/GetAssemblysResponse")>  _
        Function GetAssemblys() As EventAdmin.AssemblyManaifest
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/GetAssemblyItem", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/GetAssemblyItemResponse")>  _
        Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As EventAdmin.AssemblyItem
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/ValidateAssembly", ReplyAction:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/ValidateAssemblyResponse")>  _
        Function ValidateAssembly(ByVal AssemblyItem As EventAdmin.AssemblyItem) As Boolean
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface IEventHandlerAdminCallback
        
        <System.ServiceModel.OperationContractAttribute(IsOneWay:=true, Action:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin/IEventHan"& _ 
            "dlerAdmin/Updated")>  _
        Sub Updated(ByVal AssemblyManaifest As EventAdmin.AssemblyManaifest)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Public Interface IEventHandlerAdminChannel
        Inherits EventAdmin.IEventHandlerAdmin, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")>  _
    Partial Public Class EventHandlerAdminClient
        Inherits System.ServiceModel.DuplexClientBase(Of EventAdmin.IEventHandlerAdmin)
        Implements EventAdmin.IEventHandlerAdmin
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext)
            MyBase.New(callbackInstance)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String)
            MyBase.New(callbackInstance, endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal callbackInstance As System.ServiceModel.InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(callbackInstance, binding, remoteAddress)
        End Sub
        
        Public Sub AddAssembly(ByVal AssemblyItem As EventAdmin.AssemblyItem) Implements EventAdmin.IEventHandlerAdmin.AddAssembly
            MyBase.Channel.AddAssembly(AssemblyItem)
        End Sub
        
        Public Sub AddAssemblyDirect(ByVal AssemblyBytes() As Byte) Implements EventAdmin.IEventHandlerAdmin.AddAssemblyDirect
            MyBase.Channel.AddAssemblyDirect(AssemblyBytes)
        End Sub
        
        Public Sub RemoveAssembly(ByVal ID As Integer) Implements EventAdmin.IEventHandlerAdmin.RemoveAssembly
            MyBase.Channel.RemoveAssembly(ID)
        End Sub
        
        Public Function GetAssemblys() As EventAdmin.AssemblyManaifest Implements EventAdmin.IEventHandlerAdmin.GetAssemblys
            Return MyBase.Channel.GetAssemblys
        End Function
        
        Public Function GetAssemblyItem(ByVal AssemblyBytes() As Byte) As EventAdmin.AssemblyItem Implements EventAdmin.IEventHandlerAdmin.GetAssemblyItem
            Return MyBase.Channel.GetAssemblyItem(AssemblyBytes)
        End Function
        
        Public Function ValidateAssembly(ByVal AssemblyItem As EventAdmin.AssemblyItem) As Boolean Implements EventAdmin.IEventHandlerAdmin.ValidateAssembly
            Return MyBase.Channel.ValidateAssembly(AssemblyItem)
        End Function
    End Class
End Namespace
