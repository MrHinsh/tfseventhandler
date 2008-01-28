Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports RDdotNet.TeamFoundation
Imports RDdotNet.TeamFoundation.Config
Imports System.Runtime.Serialization
Imports RDdotNet.TeamFoundation.Services.DataContracts

Namespace Services.Widgets


    Public MustInherit Class ItemElementWidget(Of TItem, TSearchTerm, TItemElement As {New, System.Configuration.ConfigurationElement, IConfigurationElement})

        Public Sub New(Optional ByVal initilise As Object = Nothing)
            m_Items = LoadItemElements(initilise)
        End Sub

        Private m_Items As New Collection(Of TItem)

        Friend ReadOnly Property Items() As Collection(Of TItem)
            Get
                Return m_Items
            End Get
        End Property

#Region " Events "

        Friend Event StatusChange(ByVal Status As StatusChangeTypeEnum, ByVal Item As TItem)

        Protected Sub OnStatusChange(ByVal Status As StatusChangeTypeEnum, ByVal Item As TItem)
            RaiseEvent StatusChange(Status, Item)
        End Sub

        Friend Event ErrorOccured(ByVal ex As Exception)

        Protected Sub OnErrorOccured(ByVal ex As Exception)
            RaiseEvent ErrorOccured(ex)
        End Sub

#End Region

#Region " manipulation "


        'Public Function GetItems() As Collection(Of TItem)
        '    Return m_Items
        'End Function

        Public MustOverride Sub Add(ByVal Item As TItem)

        Protected Sub InnerAdd(ByVal Item As TItem)
            If Not Exists(Item) Then
                m_Items.Add(Item)
                Me.SaveItemElements()
                OnStatusChange(StatusChangeTypeEnum.Item_Added, Item)
                InnerRefresh(Item)
            Else
                OnStatusChange(StatusChangeTypeEnum.Item_Exists, Item)
            End If
        End Sub

        Public MustOverride Sub Remove(ByVal Item As TItem)

        Protected Sub InnerRemove(ByVal Item As TItem)
            If Exists(Item) Then
                m_Items.Remove(Item)
                Me.SaveItemElements()
                OnStatusChange(StatusChangeTypeEnum.Item_Removed, Item)
            Else
                OnStatusChange(StatusChangeTypeEnum.Item_NotExists, Item)
            End If
        End Sub

        Public MustOverride Sub Refresh(ByVal Item As TItem)

        Public MustOverride Sub Refresh()

        Protected Sub InnerRefresh()
            OnStatusChange(StatusChangeTypeEnum.Item_CheckAll_Started, Nothing)
            For Each Item In m_Items
                InnerRefresh(Item)
            Next
            OnStatusChange(StatusChangeTypeEnum.Item_CheckAll_Ended, Nothing)
        End Sub

        Protected Sub InnerRefresh(ByVal Item As TItem)
            OnStatusChange(StatusChangeTypeEnum.Item_Check_Started, Item)
            If ItemCheck(Item) Then
                OnStatusChange(StatusChangeTypeEnum.Item_Check_OK, Item)
            Else
                OnStatusChange(StatusChangeTypeEnum.Item_Check_Failed, Item)
            End If
            OnStatusChange(StatusChangeTypeEnum.Item_Check_Ended, Item)
        End Sub

        ''' <summary>
        ''' Checks a specific item for validity. Can be used to run extra setup against an item before it is passed to the UI.
        ''' </summary>
        ''' <param name="Item"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected MustOverride Function ItemCheck(ByVal Item As TItem) As Boolean

#End Region

#Region " Loading, Saving and Converting "

        Protected MustOverride Function Convert(ByVal source As TItem) As TItemElement
        Protected MustOverride Function Convert(ByVal source As TItemElement) As TItem

        Protected Function LoadItemElements(Optional ByVal initilise As Object = Nothing) As Collection(Of TItem)
            Dim Items As New Collection(Of TItem)
            For Each ServerItemElement As TItemElement In GetItemElements(initilise)
                Items.Add(Convert(ServerItemElement))
            Next
            Return Items
        End Function

        Protected Sub SaveItemElements()
            Dim ItemElements As New Collection(Of TItemElement)
            For Each Item As TItem In m_Items
                ItemElements.Add(Convert(Item))
            Next
            SetItemElements(ItemElements)
        End Sub

        ''' <summary>
        ''' Get all elements from the Data store. This is a Must Override function.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected MustOverride Function GetItemElements(Optional ByVal initilise As Object = Nothing) As Collection(Of TItemElement)

        ''' <summary>
        ''' Saves the Items to the data store. This is a Must Override function.
        ''' </summary>
        ''' <param name="ItemElements"></param>
        ''' <remarks></remarks>
        Protected MustOverride Sub SetItemElements(ByVal ItemElements As Collection(Of TItemElement))


#End Region

        ''' <summary>
        ''' Checks to see if a specified Data Contract Item exists. Returns True if it does and False if it does not.
        ''' </summary>
        ''' <param name="Item"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function Exists(ByVal Item As TItem) As Boolean

        ''' <summary>
        ''' Returns a specified data contract Item. Returns nothing if it does not exist.
        ''' </summary>
        ''' <param name="SearchTerm"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function Find(ByVal SearchTerm As TSearchTerm) As TItem






    End Class


End Namespace