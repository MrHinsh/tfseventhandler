Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel


''' <summary>
''' This is the service for hosting and manipulating the ML EventHandler applciation for team foundation server
''' </summary>
''' <remarks></remarks>
Public Interface IEventHandlerAdminCallback

    <OperationContract(IsOneWay:=True)> _
  Sub Updated(ByVal AssemblyManaifest As AssemblyManaifest)

End Interface
