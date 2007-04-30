Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports System.Collections.ObjectModel

Namespace Services.Contracts


    ''' <summary>
    ''' This is the seervice contract for integrating with the Team Foundation Server notification events.
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract(CallbackContract:=GetType(IEventHandlerAdminCallback), Namespace:="http://schemas.ml.com/TeamFoundation/2005/06/Services/EventHandlerAdmin")> _
    Public Interface IEventHandlerAdmin

        <OperationContract(IsOneWay:=True)> _
        Sub AddAssembly(ByVal AssemblyItem As AssemblyItem)

        <OperationContract(IsOneWay:=True)> _
        Sub AddAssemblyDirect(ByVal AssemblyBytes As Byte())

        <OperationContract(IsOneWay:=True)> _
        Sub RemoveAssembly(ByVal ID As Integer)

        <OperationContract(IsOneWay:=False)> _
        Function GetAssemblys() As AssemblyManaifest

        <OperationContract(IsOneWay:=False)> _
        Function GetAssemblyItem(ByVal AssemblyBytes As Byte()) As AssemblyItem

        <OperationContract(IsOneWay:=False)> _
        Function ValidateAssembly(ByVal AssemblyItem As AssemblyItem) As Boolean

    End Interface



End Namespace