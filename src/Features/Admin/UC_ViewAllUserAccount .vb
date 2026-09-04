Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class UC_AllUserAccount

    Private dtAccounts As New DataTable()
    Private accountToolTip As New ToolTip()

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_AllUserAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Styling
        RadiusButton(btnStatus, 1.5F) ' Re-purposed as Toggle Status
        btnStatus.Text = "Toggle Status (Deactivate)"

        DataGridViewHelper.ApplyBeautifulStyle(dgvAccounts)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvAccounts, 40)

        ' 2. Tooltips
        accountToolTip.ToolTipIcon = ToolTipIcon.Info
        accountToolTip.IsBalloon = True
        accountToolTip.AutoPopDelay = 5000

        accountToolTip.SetToolTip(cmbAccountRole, "Filter accounts by system role.")
        accountToolTip.SetToolTip(cmbFilterZones, "Filter Officer Groups by jurisdiction.")
        accountToolTip.SetToolTip(txtSearch, "Live search by username or account name.")
        accountToolTip.SetToolTip(btnStatus, "Deactivate or Reactivate access. (Hard deletions are blocked for audit integrity).")
        accountToolTip.SetToolTip(dgvAccounts, "Double-click a row to reset their username or password.")

        ' 3. Load Comboboxes and Grid
        LoadFilters()
        LoadAccountsGrid()
    End Sub

    Private Sub LoadFilters()
        ' Roles
        cmbAccountRole.Items.Clear()
        cmbAccountRole.Items.Add("ALL ROLES")
        cmbAccountRole.Items.Add("ADMIN")
        cmbAccountRole.Items.Add("INSPECTOR")
        cmbAccountRole.Items.Add("OFFICER_GROUP")
        cmbAccountRole.SelectedIndex = 0

        ' Zones
        Dim zoneQuery As String = "SELECT zone_id, zone_name FROM zones ORDER BY zone_name ASC"
        Dim dtZones As New DataTable()
        dtZones.Columns.Add("zone_id", GetType(Integer))
        dtZones.Columns.Add("zone_name", GetType(String))
        dtZones.Rows.Add(-1, "ALL ZONES") ' Default option

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(zoneQuery, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            dtZones.Rows.Add(reader("zone_id"), reader("zone_name"))
                        End While
                    End Using
                End Using
            End Using

            cmbFilterZones.DataSource = dtZones
            cmbFilterZones.DisplayMember = "zone_name"
            cmbFilterZones.ValueMember = "zone_id"
            cmbFilterZones.SelectedIndex = 0

        Catch ex As MySqlException
            MessageBox.Show($"Failed to load zones for filter: {ex.Message}")
        End Try
    End Sub

    ' =================================================================
    ' DATA RETRIEVAL & HEAVY SQL QUERY
    ' =================================================================
    Private Sub LoadAccountsGrid()
        ' The query leverages Subqueries to count assigned and completed cases per Officer Group.
        ' FIXED: Mapped explicitly to 'assigned_to_group' per the locked schema.
        Dim query As String = "
            SELECT 
                a.account_id, 
                a.account_name, 
                a.username, 
                a.role, 
                COALESCE(z.zone_name, 'N/A') AS zone_name,
                a.account_status,
                (SELECT COUNT(*) FROM case_assignments ca JOIN cases c ON ca.case_id = c.case_id WHERE ca.assigned_to_group = a.account_id AND c.status = 'COMPLETED') AS cases_completed,
                (SELECT COUNT(*) FROM case_assignments ca JOIN cases c ON ca.case_id = c.case_id WHERE ca.assigned_to_group = a.account_id AND c.status IN ('ASSIGNED', 'IN_PROGRESS', 'PENDING')) AS cases_pending,
                a.zone_id
            FROM accounts a
            LEFT JOIN zones z ON a.zone_id = z.zone_id
            ORDER BY a.role ASC, a.account_name ASC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        dtAccounts.Clear()
                        adapter.Fill(dtAccounts)
                    End Using
                End Using
            End Using

            dgvAccounts.DataSource = dtAccounts

            ' Formatting the Grid
            dgvAccounts.Columns("account_id").Visible = False
            dgvAccounts.Columns("zone_id").Visible = False
            dgvAccounts.Columns("account_name").HeaderText = "Account Name"
            dgvAccounts.Columns("username").HeaderText = "Username"
            dgvAccounts.Columns("role").HeaderText = "Role"
            dgvAccounts.Columns("zone_name").HeaderText = "Zone"
            dgvAccounts.Columns("account_status").HeaderText = "Status"
            dgvAccounts.Columns("cases_completed").HeaderText = "Completed"
            dgvAccounts.Columns("cases_pending").HeaderText = "Pending"

        Catch ex As MySqlException
            MessageBox.Show($"Failed to load accounts: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' LIVE FILTERING LOGIC
    ' =================================================================
    Private Sub ApplyFilters()
        If dtAccounts Is Nothing OrElse dtAccounts.Rows.Count = 0 Then Return

        Dim filterParts As New List(Of String)

        ' 1. Search Text
        Dim searchTxt As String = txtSearch.Text.Trim().Replace("'", "''")
        If Not String.IsNullOrEmpty(searchTxt) Then
            filterParts.Add($"(username LIKE '%{searchTxt}%' OR account_name LIKE '%{searchTxt}%')")
        End If

        ' 2. Role Filter
        If cmbAccountRole.SelectedIndex > 0 Then
            filterParts.Add($"role = '{cmbAccountRole.SelectedItem.ToString()}'")
        End If

        ' 3. Zone Filter
        If cmbFilterZones.SelectedIndex > 0 Then
            Dim zId As Integer = Convert.ToInt32(cmbFilterZones.SelectedValue)
            filterParts.Add($"zone_id = {zId}")
        End If

        ' Apply to DataView
        If filterParts.Count > 0 Then
            dtAccounts.DefaultView.RowFilter = String.Join(" AND ", filterParts)
        Else
            dtAccounts.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ApplyFilters()
    End Sub

    Private Sub cmbAccountRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccountRole.SelectedIndexChanged
        ApplyFilters()
    End Sub

    Private Sub cmbFilterZones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterZones.SelectedIndexChanged
        ApplyFilters()
    End Sub

    ' =================================================================
    ' STATUS TOGGLE (INSTEAD OF HARD DELETE)
    ' =================================================================
    Private Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        If dgvAccounts.SelectedRows.Count = 0 AndAlso dgvAccounts.SelectedCells.Count = 0 Then
            MessageBox.Show("Select an account first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim rowIndex = If(dgvAccounts.SelectedRows.Count > 0, dgvAccounts.SelectedRows(0).Index, dgvAccounts.SelectedCells(0).RowIndex)
        Dim accId = Convert.ToInt32(dgvAccounts.Rows(rowIndex).Cells("account_id").Value)
        Dim currentStatus = dgvAccounts.Rows(rowIndex).Cells("account_status").Value.ToString
        Dim accRole = dgvAccounts.Rows(rowIndex).Cells("role").Value.ToString

        ' Prevent Admin from locking themselves out
        If accId = SessionManager.CurrentAccountId Then
            MessageBox.Show("You cannot deactivate your own active session.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        Dim newStatus = If(currentStatus = "ACTIVE", "INACTIVE", "ACTIVE")
        Dim msg = If(newStatus = "INACTIVE", "Deactivate this account? They will be immediately blocked from logging in.", "Reactivate this account? They will regain system access.")

        If MessageBox.Show(msg, "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using conn = Database.CreateOpenConnection
                    Dim updateQuery = "UPDATE accounts SET account_status = @status WHERE account_id = @id"
                    Using cmd As New MySqlCommand(updateQuery, conn)
                        cmd.Parameters.AddWithValue("@status", newStatus)
                        cmd.Parameters.AddWithValue("@id", accId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show($"Account is now {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadAccountsGrid()

            Catch ex As MySqlException
                MessageBox.Show($"Failed to update status: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub



    ' =================================================================
    ' DYNAMIC EDIT DIALOG (DOUBLE CLICK EVENT)
    ' =================================================================
    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim accId As Integer = Convert.ToInt32(dgvAccounts.Rows(e.RowIndex).Cells("account_id").Value)
        Dim currentUsername As String = dgvAccounts.Rows(e.RowIndex).Cells("username").Value.ToString()

        ' Build a dynamic dialog form in memory so you don't have to create a new UI file
        Using editForm As New Form()
            editForm.Text = "Update Account Credentials"
            editForm.Size = New Size(350, 250)
            editForm.StartPosition = FormStartPosition.CenterParent
            editForm.FormBorderStyle = FormBorderStyle.FixedDialog
            editForm.MaximizeBox = False
            editForm.MinimizeBox = False

            Dim lblUser As New Label() With {.Text = "New Username:", .Location = New Point(20, 20), .AutoSize = True}
            Dim txtUser As New TextBox() With {.Text = currentUsername, .Location = New Point(20, 45), .Width = 280}

            Dim lblPass As New Label() With {.Text = "New Password (Leave blank to keep current):", .Location = New Point(20, 85), .AutoSize = True}
            Dim txtPass As New TextBox() With {.Location = New Point(20, 110), .Width = 280, .UseSystemPasswordChar = True}

            Dim btnSave As New Button() With {.Text = "Save Updates", .Location = New Point(20, 160), .Width = 130, .BackColor = Color.DarkBlue, .ForeColor = Color.White}
            Dim btnCancel As New Button() With {.Text = "Cancel", .Location = New Point(170, 160), .Width = 130}

            editForm.Controls.AddRange(New Control() {lblUser, txtUser, lblPass, txtPass, btnSave, btnCancel})

            ' Add click handlers via lambda expressions
            AddHandler btnCancel.Click, Sub(s, args) editForm.DialogResult = DialogResult.Cancel

            AddHandler btnSave.Click, Sub(s, args)
                                          ' Inline Regex Validations
                                          If Not Regex.IsMatch(txtUser.Text.Trim(), "^[A-Za-z0-9_]{4,50}$") Then
                                              MessageBox.Show("Username must be 4-50 chars. Letters, numbers, underscores only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          Try
                                              Using conn As MySqlConnection = Database.CreateOpenConnection()
                                                  If String.IsNullOrWhiteSpace(txtPass.Text) Then
                                                      ' Update username only
                                                      Dim cmd As New MySqlCommand("UPDATE accounts SET username = @user WHERE account_id = @id", conn)
                                                      cmd.Parameters.AddWithValue("@user", txtUser.Text.Trim())
                                                      cmd.Parameters.AddWithValue("@id", accId)
                                                      cmd.ExecuteNonQuery()
                                                  Else
                                                      ' Update username AND password
                                                      Dim cmd As New MySqlCommand("UPDATE accounts SET username = @user, password_hash = @pass WHERE account_id = @id", conn)
                                                      cmd.Parameters.AddWithValue("@user", txtUser.Text.Trim())
                                                      cmd.Parameters.AddWithValue("@pass", HashPassword(txtPass.Text.Trim()))
                                                      cmd.Parameters.AddWithValue("@id", accId)
                                                      cmd.ExecuteNonQuery()
                                                  End If
                                              End Using
                                              editForm.DialogResult = DialogResult.OK
                                          Catch ex As MySqlException
                                              If ex.Number = 1062 Then
                                                  MessageBox.Show("This username is already taken.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Else
                                                  MessageBox.Show($"Update failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End If
                                          End Try
                                      End Sub

            If editForm.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Credentials updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadAccountsGrid()
            End If
        End Using
    End Sub

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function



    ' =================================================================
    ' HARD DELETE (DANGEROUS ACTION)
    ' =================================================================

    Private Sub btnDeleteAccount_Click_1(sender As Object, e As EventArgs) Handles btnDeleteAccount.Click
        If dgvAccounts.SelectedRows.Count = 0 AndAlso dgvAccounts.SelectedCells.Count = 0 Then
            MessageBox.Show("Select an account first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim rowIndex = If(dgvAccounts.SelectedRows.Count > 0, dgvAccounts.SelectedRows(0).Index, dgvAccounts.SelectedCells(0).RowIndex)
        Dim accId = Convert.ToInt32(dgvAccounts.Rows(rowIndex).Cells("account_id").Value)
        Dim currentUsername = dgvAccounts.Rows(rowIndex).Cells("username").Value.ToString

        ' 1. Prevent suicide
        If accId = SessionManager.CurrentAccountId Then
            MessageBox.Show("You cannot delete your own active session. That is system suicide.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' 2. Final Warning
        Dim warningMsg = $"Are you absolutely sure you want to PERMANENTLY DELETE '{currentUsername}'? " & vbCrLf & vbCrLf &
                                   "WARNING: If this account has ANY ties to existing cases, assignments, or evidence, the system will block the deletion to protect the integrity of police records."

        If MessageBox.Show(warningMsg, "CRITICAL: Confirm Hard Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Try
                Using conn = Database.CreateOpenConnection
                    Dim deleteQuery = "DELETE FROM accounts WHERE account_id = @id"
                    Using cmd As New MySqlCommand(deleteQuery, conn)
                        cmd.Parameters.AddWithValue("@id", accId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show($"Account '{currentUsername}' was totally deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadAccountsGrid()

            Catch ex As MySqlException
                If ex.Number = 1451 Then
                    ' The database did its job and stopped you.
                    MessageBox.Show("DELETION BLOCKED." & vbCrLf & vbCrLf &
                                    "This account cannot be deleted because they are tied to official cases, evidence, or audit logs. " &
                                    "Erasing them would corrupt police records. You must use the 'Toggle Status (Deactivate)' function instead.",
                                    "Database Integrity Constraint", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Else
                    MessageBox.Show($"System Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
        End If
    End Sub


End Class