﻿Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace NotificationService

    <System.Web.Services.WebServiceBinding(Name:="CheckInEventNotification", ConformsTo:=System.Web.Services.WsiProfiles.BasicProfile1_1, EmitConformanceClaims:=True, Namespace:="http://schema.rddotnet.com/2007/08/TFSEventHandler/CheckInEvent")> _
    <System.Web.Services.Protocols.SoapDocumentService()> _
    Public Class CheckInEventNotification
        Inherits System.Web.Services.WebService

    End Class

End Namespace