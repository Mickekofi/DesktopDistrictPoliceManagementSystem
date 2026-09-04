Option Strict On
Option Explicit On

Public Class SessionManager
    ' =================================================================
    ' GLOBAL SESSION STATE
    ' Holds the current authenticated account's details across all forms.
    ' =================================================================

    Public Shared Property CurrentAccountId As Integer = 0
    Public Shared Property CurrentAccountName As String = String.Empty
    Public Shared Property CurrentUsername As String = String.Empty
    Public Shared Property CurrentRole As String = String.Empty
    Public Shared Property CurrentZoneId As Nullable(Of Integer) = Nothing

    ''' <summary>
    ''' Checks if a user is currently authenticated in the system.
    ''' </summary>
    Public Shared ReadOnly Property IsLoggedIn As Boolean
        Get
            Return CurrentAccountId > 0
        End Get
    End Property

    ''' <summary>
    ''' Initializes the session after a successful database login.
    ''' </summary>
    Public Shared Sub StartSession(accountId As Integer, accountName As String, username As String, role As String, Optional zoneId As Nullable(Of Integer) = Nothing)
        CurrentAccountId = accountId
        CurrentAccountName = accountName
        CurrentUsername = username
        CurrentRole = role
        CurrentZoneId = zoneId
    End Sub

    ''' <summary>
    ''' Wipes the session data. Call this when the user clicks "Logout".
    ''' </summary>
    Public Shared Sub EndSession()
        CurrentAccountId = 0
        CurrentAccountName = String.Empty
        CurrentUsername = String.Empty
        CurrentRole = String.Empty
        CurrentZoneId = Nothing
    End Sub

    ''' <summary>
    ''' Authorization Helper: Use this to lock down specific forms or buttons.
    ''' </summary>
    Public Shared Function HasRole(requiredRole As String) As Boolean
        Return String.Equals(CurrentRole, requiredRole, StringComparison.OrdinalIgnoreCase)
    End Function
End Class