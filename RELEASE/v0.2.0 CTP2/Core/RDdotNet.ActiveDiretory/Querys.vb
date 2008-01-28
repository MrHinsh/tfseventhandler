''' <summary>
''' A numberof usefull queries for Active Directory
''' </summary>
''' <remarks></remarks>
Public Class Querys

    ''' <summary>
    ''' Prevents the instanciation of this class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub


    '' <summary>
    '' Retrieves a user's email address from Active Directory based on their display name
    '' </summary>
    Public Shared Function GetEmailAddress(ByVal userDisplayName As String) As String
        Dim ds As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher()
        ds.PropertiesToLoad.Add("mail")
        ds.Filter = String.Format("(&(displayName={0})(objectCategory=person)((objectClass=user)))", userDisplayName)

        Dim results As DirectoryServices.SearchResultCollection = ds.FindAll()
        If results.Count = 0 Then
            Return String.Empty
        End If
        Dim values As DirectoryServices.ResultPropertyValueCollection = results(0).Properties("mail")
        If values.Count = 0 Then
            Return String.Empty
        End If
        Return values(0).ToString()
    End Function

End Class
