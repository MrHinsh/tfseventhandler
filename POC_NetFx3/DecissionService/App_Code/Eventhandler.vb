Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace DecissionService

    <System.Web.Services.WebServiceBinding(Name:="Eventhandler", ConformsTo:=System.Web.Services.WsiProfiles.BasicProfile1_1, EmitConformanceClaims:=True, Namespace:="http://schema.rddotnet.com/2007/08/TFSEventHandler/Eventhandler")> _
    <System.Web.Services.Protocols.SoapDocumentService()> _
    Public Class Eventhandler
        Inherits System.Web.Services.WebService

    End Class

End Namespace
