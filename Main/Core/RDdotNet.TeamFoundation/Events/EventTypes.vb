Imports System.Runtime.Serialization

Namespace Events

    <DataContract()> _
    Public Enum EventTypes
        <EnumMember()> Unknown
        <EnumMember()> AclChangedEven
        <EnumMember()> Branchmovedevent
        <EnumMember()> BuildCompletionEvent
        <EnumMember()> BuildStatusChangeEvent
        <EnumMember()> CommonStructureChangedEvent
        <EnumMember()> DataChangedEvent
        <EnumMember()> IdentityChangedEvent
        <EnumMember()> IdentityCreatedEvent
        <EnumMember()> IdentityDeletedEvent
        <EnumMember()> MembershipChangedEvent
        <EnumMember()> WorkItemChangedEvent
        <EnumMember()> CheckinEvent
    End Enum

End Namespace