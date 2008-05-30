Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.ComponentModel
Imports System.Runtime.Serialization

Namespace Events.Handlers


    <AttributeUsage(AttributeTargets.Class)> _
        Public Class MailSettingsAttribute
        Inherits Attribute

        Private _MailFromName As String = "Team Foundation Server"

        Public Property MailFromName() As String
            Get
                Return _MailFromName
            End Get
            Set(ByVal value As String)
                _MailFromName = value
            End Set
        End Property

        Private _MailServer As String = "relay.uk.ml.com"

        Public Property MailServer() As String
            Get
                Return _MailServer
            End Get
            Set(ByVal value As String)
                _MailServer = value
            End Set
        End Property

        Private _MailAddressFrom As String = "teamsystem@ml.com"

        Public Property MailAddressFrom() As String
            Get
                Return _MailAddressFrom
            End Get
            Set(ByVal value As String)
                _MailAddressFrom = value
            End Set
        End Property

    End Class

End Namespace