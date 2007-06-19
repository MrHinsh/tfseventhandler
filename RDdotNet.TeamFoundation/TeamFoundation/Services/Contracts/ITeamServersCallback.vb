Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace TeamFoundation.Services.Contracts


    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    <ClientServiceContractAttribute()> _
    Public Interface ITeamServersCallback

        <OperationContract(IsOneWay:=True)> _
      Sub Updated(ByVal TeamServers() As String)

        <OperationContract(IsOneWay:=False)> _
        Function GetCredentials(ByVal uri As System.Uri, ByVal failedCredentials As System.Net.ICredentials) As System.Net.ICredentials

        <OperationContract(IsOneWay:=True)> _
        Sub NotifyCredentialsAuthenticated(ByVal uri As System.Uri)

    End Interface

End Namespace