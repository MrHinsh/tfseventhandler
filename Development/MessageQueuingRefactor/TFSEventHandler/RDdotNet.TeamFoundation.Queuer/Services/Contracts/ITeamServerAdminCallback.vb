Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Services.Contracts


    ''' <summary>
    ''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITeamServerAdminCallback

        <OperationContract(IsOneWay:=True)> _
      Sub Updated(ByVal TeamServers() As String)

    End Interface

End Namespace