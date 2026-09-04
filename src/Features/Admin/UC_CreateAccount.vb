Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms

Public Class UC_CreateAccount

    Private accountToolTip As New ToolTip()
    Private dtZones As New DataTable()
    'ErrorProvider for validation feedback
    Private ErrorProvider1 As New ErrorProvider()

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_CreateAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Apply shared styling
        RadiusButton(btnCreate, 1.5F)

        ' 2. Load Static Roles
        cmbSelectRole.Items.Clear()
        cmbSelectRole.Items.Add("ADMIN")
        cmbSelectRole.Items.Add("INSPECTOR")
        cmbSelectRole.Items.Add("OFFICER_GROUP")
        cmbSelectRole.SelectedIndex = -1

        ' 3. Load Dynamic Zones
        LoadZones()

        ' 4. UX GUIDANCE: Setup Tooltips
        accountToolTip.ToolTipIcon = ToolTipIcon.Info
        accountToolTip.IsBalloon = True
        accountToolTip.AutoPopDelay = 5000

        accountToolTip.SetToolTip(txtAccountName, "Enter the official name (e.g., 'Alpha Group' or 'Inspector John Doe').")
        accountToolTip.SetToolTip(txtAccountUsername, "Create a unique login username. No spaces allowed.")
        accountToolTip.SetToolTip(txtPassword, "Set a secure default password.")
        accountToolTip.SetToolTip(txtPhoneNumber, "Must be exactly 10 digits starting with 02 or 05.")
        accountToolTip.SetToolTip(cmbSelectRole, "Select the system access level.")
        accountToolTip.SetToolTip(cmbSelectZone, "Only required for Officer Groups. Admins and Inspectors are system-wide.")
        accountToolTip.SetToolTip(btnCreate, "Save this account to the database.")
    End Sub

    Private Sub LoadZones()
        Dim query As String = "SELECT zone_id, zone_name FROM zones ORDER BY zone_name ASC"
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        dtZones.Clear()
                        adapter.Fill(dtZones)
                    End Using
                End Using
            End Using

            ' Bind to ComboBox safely
            cmbSelectZone.DataSource = dtZones
            cmbSelectZone.DisplayMember = "zone_name"
            cmbSelectZone.ValueMember = "zone_id"
            cmbSelectZone.SelectedIndex = -1
            cmbSelectZone.Enabled = False ' Disabled by default until role is chosen

        Catch ex As MySqlException
            MessageBox.Show($"Failed to load zones: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    ' =================================================================
    ' UI LOGIC: ROLE SELECTION DICTATES ZONE SELECTION
    ' =================================================================
    Private Sub cmbSelectRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelectRole.SelectedIndexChanged
        If cmbSelectRole.SelectedItem IsNot Nothing AndAlso cmbSelectRole.SelectedItem.ToString() = "OFFICER_GROUP" Then
            cmbSelectZone.Enabled = True
        Else
            cmbSelectZone.Enabled = False
            cmbSelectZone.SelectedIndex = -1 ' Clear selection for Admins/Inspectors
            ErrorProvider1.SetError(cmbSelectZone, "")
        End If
    End Sub

    ' =================================================================
    ' LIVE & ON-LEAVE VALIDATIONS
    ' =================================================================
    Private Sub txtAccountName_TextChanged(sender As Object, e As EventArgs) Handles txtAccountName.TextChanged
        Dim pattern As String = "^[A-Za-z0-9\s\-\.\']{2,150}$"
        If Not Regex.IsMatch(txtAccountName.Text, pattern) AndAlso txtAccountName.Text.Length > 0 Then
            ErrorProvider1.SetError(txtAccountName, "Invalid characters. Use letters, numbers, spaces, and hyphens.")
        Else
            ErrorProvider1.SetError(txtAccountName, "")
        End If
    End Sub

    Private Sub txtAccountUsername_TextChanged(sender As Object, e As EventArgs) Handles txtAccountUsername.TextChanged
        ' Usernames: No spaces, alphanumeric + underscores, 4-50 chars
        Dim pattern As String = "^[A-Za-z0-9_]{4,50}$"
        If Not Regex.IsMatch(txtAccountUsername.Text, pattern) AndAlso txtAccountUsername.Text.Length > 0 Then
            ErrorProvider1.SetError(txtAccountUsername, "Must be 4-50 characters. No spaces. Letters, numbers, underscores only.")
        Else
            ErrorProvider1.SetError(txtAccountUsername, "")
        End If
    End Sub

    Private Sub txtPhoneNumber_Leave(sender As Object, e As EventArgs) Handles txtPhoneNumber.Leave
        Dim phoneStr As String = txtPhoneNumber.Text.Trim()
        If phoneStr.Length > 0 Then
            Dim pattern As String = "^(02|05)\d{8}$"
            If Not Regex.IsMatch(phoneStr, pattern) Then
                ErrorProvider1.SetError(txtPhoneNumber, "Must be exactly 10 digits starting with 02 or 05.")
                txtPhoneNumber.Focus() ' Force them to fix it
            Else
                ErrorProvider1.SetError(txtPhoneNumber, "")
            End If
        End If
    End Sub



    'Key Down event for All Controls to allow Enter key to move to next control
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccountName.KeyDown, txtAccountUsername.KeyDown, txtPassword.KeyDown, txtPhoneNumber.KeyDown, cmbSelectRole.KeyDown, cmbSelectZone.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.SelectNextControl(DirectCast(sender, Control), True, True, True, True)
            e.SuppressKeyPress = True ' Prevent ding sound
        End If
    End Sub



    ' =================================================================
    ' DATABASE EXECUTION
    ' =================================================================
    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        ' 1. Final Hard Validation Check before hitting DB
        If Not ValidateForm() Then Return

        ' 2. Prepare Data
        Dim accName As String = txtAccountName.Text.Trim()
        Dim username As String = txtAccountUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim phone As String = txtPhoneNumber.Text.Trim()
        Dim role As String = cmbSelectRole.SelectedItem.ToString()

        ' Hash the password using Base64
        Dim hashedPassword As String = HashPassword(password)

        ' Handle Zone Nullability safely
        Dim zoneId As Object = DBNull.Value
        If cmbSelectZone.Enabled AndAlso cmbSelectZone.SelectedValue IsNot Nothing Then
            zoneId = Convert.ToInt32(cmbSelectZone.SelectedValue)
        End If

        ' 3. Execute Insert
        Dim query As String = "INSERT INTO accounts (account_name, username, password_hash, phone_number, role, zone_id, account_status) " &
                              "VALUES (@name, @user, @pass, @phone, @role, @zoneId, 'ACTIVE')"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", accName)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", hashedPassword)

                    ' Handles empty string as DBNull if they skip the phone number
                    If String.IsNullOrEmpty(phone) Then
                        cmd.Parameters.AddWithValue("@phone", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@phone", phone)
                    End If

                    cmd.Parameters.AddWithValue("@role", role)
                    cmd.Parameters.AddWithValue("@zoneId", zoneId)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Account successfully created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()

        Catch ex As MySqlException
            If ex.Number = 1062 Then ' Duplicate entry constraint
                MessageBox.Show("This username is already taken. Please choose a different username.", "Username Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtAccountUsername.Focus()
                txtAccountUsername.SelectAll()
            Else
                MessageBox.Show($"Database Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ' =================================================================
    ' HELPER FUNCTIONS
    ' =================================================================
    Private Function ValidateForm() As Boolean
        ErrorProvider1.Clear()
        Dim isValid As Boolean = True

        If Not Regex.IsMatch(txtAccountName.Text, "^[A-Za-z0-9\s\-\.\']{2,150}$") Then
            ErrorProvider1.SetError(txtAccountName, "Invalid account name.")
            isValid = False
        End If

        If Not Regex.IsMatch(txtAccountUsername.Text, "^[A-Za-z0-9_]{4,50}$") Then
            ErrorProvider1.SetError(txtAccountUsername, "Invalid username format.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            ErrorProvider1.SetError(txtPassword, "Password is required.")
            isValid = False
        End If

        If txtPhoneNumber.Text.Trim().Length > 0 AndAlso Not Regex.IsMatch(txtPhoneNumber.Text.Trim(), "^(02|05)\d{8}$") Then
            ErrorProvider1.SetError(txtPhoneNumber, "Invalid phone number format.")
            isValid = False
        End If

        If cmbSelectRole.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cmbSelectRole, "You must select a role.")
            isValid = False
        End If

        If cmbSelectZone.Enabled AndAlso cmbSelectZone.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cmbSelectZone, "Officer Groups require an assigned zone.")
            isValid = False
        End If

        If Not isValid Then
            MessageBox.Show("Please correct the errors indicated by the red icons before saving.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        Return isValid
    End Function

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function

    Private Sub ClearForm()
        txtAccountName.Clear()
        txtAccountUsername.Clear()
        txtPassword.Clear()
        txtPhoneNumber.Clear()
        cmbSelectRole.SelectedIndex = -1
        cmbSelectZone.SelectedIndex = -1
        cmbSelectZone.Enabled = False
        ErrorProvider1.Clear()
        txtAccountName.Focus()
    End Sub

End Class