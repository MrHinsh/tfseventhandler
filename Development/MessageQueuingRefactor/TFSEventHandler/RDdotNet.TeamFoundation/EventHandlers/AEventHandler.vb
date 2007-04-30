Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.io
Imports System.Text
Imports System.Net.Mail
Imports System.Configuration
Imports System.Reflection
Imports Microsoft.TeamFoundation.Client
Imports RDdotNet.TeamFoundation.EventResources
Imports RDdotNet.TeamFoundation
Imports Microsoft.TeamFoundation.Common
Imports Microsoft.TeamFoundation.Server
Imports Microsoft.TeamFoundation

Public MustInherit Class AEventHandler(Of TEvent As {New})

    Public Sub New()
        If EventHandlerSettings Is Nothing Then Throw New System.ServiceModel.FaultException("You must have a EventHandlerAttribute on you class.")
    End Sub

    Public ReadOnly Property EventHandlerSettings() As EventHandlerAttribute
        Get
            Dim attObj As Attribute = Attribute.GetCustomAttribute(Me.GetType, GetType(EventHandlerAttribute))
            If attObj Is Nothing Then
                Throw New System.ServiceModel.FaultException("To use the EventHandlerSettings you must have a EventHandlerAttribute added to yoru handler.")
            End If
            Return CType(attObj, EventHandlerAttribute)
        End Get
    End Property

    Public ReadOnly Property MailSettings() As MailSettingsAttribute
        Get
            Dim attObj As Attribute = Attribute.GetCustomAttribute(Me.GetType, GetType(MailSettingsAttribute))
            If attObj Is Nothing Then
                Throw New System.ServiceModel.FaultException("To use the MailSettings you must have a MailSettingsAttribute added to your handler.")
            End If
            Return CType(attObj, MailSettingsAttribute)
        End Get
    End Property

    Public MustOverride Sub Run(ByVal e As NotifyEventArgs(Of TEvent))
    Public MustOverride Function IsValid(ByVal e As NotifyEventArgs(Of TEvent)) As Boolean

End Class
