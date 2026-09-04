Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Database
    ' =======================================================
    ' DATABASE CONFIGURATION 
    ' Change these values here for easy development setup.
    ' =======================================================
    Private Shared ReadOnly dbServer As String = "127.0.0.1"
    Private Shared ReadOnly dbUser As String = "root"
    Private Shared ReadOnly dbPassword As String = ""
    Private Shared ReadOnly dbName As String = "dpms_db"

    ' Use the builder to safely construct the string without syntax errors
    Private Shared ReadOnly Property ConnectionString As String
        Get
            Dim builder As New MySqlConnectionStringBuilder() With {
                .Server = dbServer,
                .UserID = dbUser,
                .Password = dbPassword,
                .Database = dbName,
                .Pooling = True,
                .MinimumPoolSize = 0,
                .MaximumPoolSize = 50
            }
            Return builder.ConnectionString
        End Get
    End Property
    ' =======================================================

    ''' <summary>
    ''' Instantiates and returns a brand-new, freshly opened connection object.
    ''' Wrap this inside a "Using" block in your forms to ensure it closes automatically.
    ''' </summary>
    Public Shared Function CreateOpenConnection() As MySqlConnection
        Dim conn As New MySqlConnection(ConnectionString)
        Try
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            Return conn
        Catch ex As MySqlException
            MessageBox.Show($"Database Connection Failure: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try
    End Function










End Class