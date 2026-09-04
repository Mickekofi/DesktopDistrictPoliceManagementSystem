Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Login

    ' =================================================================
    ' UI ANIMATIONS & EFFECTS
    ' =================================================================
    Private Sub FadeIn(sender As Object, e As EventArgs)
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            tmrFade.Stop()
            RemoveHandler tmrFade.Tick, AddressOf FadeIn
        End If
    End Sub



    'Initialize error provider for validation feedback
    Dim ErrorProvider1 As New ErrorProvider()

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "System Login"
        Me.WindowState = FormWindowState.Maximized
        Me.Opacity = 0

        ' Start Fade
        tmrFade.Start()
        AddHandler tmrFade.Tick, AddressOf FadeIn

        txtUsername.Focus()

        ' Ensure password is masked by default
        txtPassword.UseSystemPasswordChar = True

        ' Apply UI Styles (Assuming RadiusButton is in your Shared Module)
        RadiusButton(btnLogin, 1.5F)

        Dim navButtons As New List(Of Button) From {btnLogin, btnNoUse}
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnNoUse)

        ' UX GUIDANCE: Setup Tooltips for Login Instructions
        Dim loginToolTip As New ToolTip() With {
            .ToolTipIcon = ToolTipIcon.Info,
            .IsBalloon = True,
            .ToolTipTitle = "Login Instructions",
            .AutoPopDelay = 5000,
            .InitialDelay = 500
        }

        loginToolTip.SetToolTip(txtUsername, "Enter your assigned desk or group username.")
        loginToolTip.SetToolTip(txtPassword, "Enter your secure password.")
    End Sub

    ' =================================================================
    ' KEYDOWN EVENTS (Press Enter to Login)
    ' =================================================================
    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnLogin.PerformClick()
        End If
    End Sub



    'HoverOver the txtPassword to show the password
    Private Sub txtPassword_MouseHover(sender As Object, e As EventArgs) Handles txtPassword.MouseHover
        txtPassword.UseSystemPasswordChar = False
    End Sub




    ' =================================================================
    ' CORE AUTHENTICATION LOGIC
    ' =================================================================
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' 1. Clear previous errors
        ErrorProvider1.Clear()

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim hasErrors As Boolean = False

        ' 2. Validation
        If String.IsNullOrEmpty(username) Then
            ErrorProvider1.SetError(txtUsername, "Username is required.")
            hasErrors = True
        End If

        If String.IsNullOrEmpty(password) Then
            ErrorProvider1.SetError(txtPassword, "Password is required.")
            hasErrors = True
        End If

        If hasErrors Then Return

        ' 3. Hash Password (Base64 matching the PasswordHasher tool)
        Dim hashedPassword As String = HashPassword(password)

        ' 4. Secure Database Query using your Database.vb class
        Dim query As String = "SELECT account_id, account_name, role, zone_id, account_status FROM accounts WHERE username = @user AND password_hash = @pass"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", hashedPassword)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then

                            ' 5. Verify Account Status
                            If reader("account_status").ToString() = "INACTIVE" Then
                                MessageBox.Show("This account has been deactivated. Contact the Administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                Return
                            End If

                            ' 6. Extract Role and Zone
                            Dim dbRole As String = reader("role").ToString().ToUpper()
                            Dim zoneId As Integer? = Nothing
                            If Not IsDBNull(reader("zone_id")) Then
                                zoneId = Convert.ToInt32(reader("zone_id"))
                            End If

                            ' 7. Initialize Global Session
                            SessionManager.StartSession(
                                Convert.ToInt32(reader("account_id")),
                                reader("account_name").ToString(),
                                username,
                                dbRole,
                                zoneId
                            )

                            ' 8. Role-Based Routing
                            Me.Hide() ' Hide login immediately upon success

                            Select Case dbRole
                                Case "ADMIN"
                                    Dim adminDash As New AdminDashboard()
                                    adminDash.Show()

                                Case "INSPECTOR"
                                    Dim inspectorDash As New InspectorDashboard()
                                    inspectorDash.Show()

                                Case "OFFICER_GROUP"
                                    Dim officerDash As New OfficerDashboard()
                                    officerDash.Show()

                                Case Else
                                    MessageBox.Show("Unknown role assigned to this account. Cannot route to a dashboard.", "Routing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Me.Show()
                                    SessionManager.EndSession()
                            End Select

                        Else
                            ' Invalid credentials
                            ErrorProvider1.SetError(txtPassword, "Invalid username or password.")
                            txtPassword.Clear()
                            txtPassword.Focus()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Database Connection Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' SECURITY HASHING ALGORITHM
    ' =================================================================
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function

    ' Ensure application terminates if Login form is closed
    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class