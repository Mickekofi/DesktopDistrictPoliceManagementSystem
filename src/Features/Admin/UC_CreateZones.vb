Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class UC_CreateZones

    ' Local state tracking
    Private dtZones As New DataTable()
    Private selectedZoneId As Integer = 0
    Private isUpdateMode As Boolean = False
    Private zoneToolTip As New ToolTip()

    'ErrorProvider for validation feedback
    Private ErrorProvider1 As New ErrorProvider()

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_CreateZones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply shared styling
        RadiusButton(btnAddZone, 1.5F)
        RadiusButton(btnDeleteZone, 1.5F)

        DataGridViewHelper.ApplyBeautifulStyle(dgvZones)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvZones, 40)

        ' UX GUIDANCE: Setup Tooltips
        zoneToolTip.ToolTipIcon = ToolTipIcon.Info
        zoneToolTip.IsBalloon = True
        zoneToolTip.AutoPopDelay = 5000

        zoneToolTip.SetToolTip(txtZoneName, "Enter the official name of the jurisdiction or town. Letters, spaces, and hyphens only.")
        zoneToolTip.SetToolTip(txtZoneSearch, "Type here to instantly filter the list of zones.")
        zoneToolTip.SetToolTip(btnAddZone, "Save this zone to the database.")
        zoneToolTip.SetToolTip(btnDeleteZone, "Permanently remove this zone. (Will fail if cases are tied to it).")
        zoneToolTip.SetToolTip(dgvZones, "Double-click a row to edit its name.")

        LoadZones()
    End Sub

    ' =================================================================
    ' DATA RETRIEVAL & LIVE SEARCH
    ' =================================================================
    Private Sub LoadZones()
        Dim query As String = "SELECT zone_id, zone_name, created_at FROM zones ORDER BY zone_name ASC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        dtZones.Clear()
                        adapter.Fill(dtZones)
                    End Using
                End Using
            End Using

            dgvZones.DataSource = dtZones

            ' Format Grid Columns
            dgvZones.Columns("zone_id").Visible = False
            dgvZones.Columns("zone_name").HeaderText = "Jurisdiction / Zone Name"
            dgvZones.Columns("created_at").HeaderText = "Date Added"
            dgvZones.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy"

            ClearForm()

        Catch ex As MySqlException
            MessageBox.Show($"Failed to load zones: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtZoneSearch_TextChanged(sender As Object, e As EventArgs) Handles txtZoneSearch.TextChanged
        ' Live search without hitting the database repeatedly
        If dtZones IsNot Nothing AndAlso dtZones.Rows.Count > 0 Then
            Dim searchStr As String = txtZoneSearch.Text.Trim().Replace("'", "''") ' Escape single quotes for DataView filter
            dtZones.DefaultView.RowFilter = $"zone_name LIKE '%{searchStr}%'"
        End If
    End Sub

    ' =================================================================
    ' VALIDATION & CRUD OPERATIONS
    ' =================================================================
    Private Sub btnAddZone_Click(sender As Object, e As EventArgs) Handles btnAddZone.Click
        ErrorProvider1.Clear()
        Dim zoneName As String = txtZoneName.Text.Trim()

        ' 1. Regex Validation
        ' Allows letters, numbers, spaces, hyphens, and apostrophes (e.g., "St. John's", "Teshie-Nungua")
        Dim regexPattern As String = "^[A-Za-z0-9\s\-\.\']{2,100}$"
        If Not Regex.IsMatch(zoneName, regexPattern) Then
            ErrorProvider1.SetError(txtZoneName, "Invalid format. Use 2-100 characters. Letters, numbers, spaces, and hyphens only.")
            txtZoneName.Focus()
            Return
        End If

        ' 2. Database Execution
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                If isUpdateMode AndAlso selectedZoneId > 0 Then
                    ' UPDATE
                    Dim updateQuery As String = "UPDATE zones SET zone_name = @name WHERE zone_id = @id"
                    Using cmd As New MySqlCommand(updateQuery, conn)
                        cmd.Parameters.AddWithValue("@name", zoneName)
                        cmd.Parameters.AddWithValue("@id", selectedZoneId)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Zone updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ' INSERT
                    Dim insertQuery As String = "INSERT INTO zones (zone_name) VALUES (@name)"
                    Using cmd As New MySqlCommand(insertQuery, conn)
                        cmd.Parameters.AddWithValue("@name", zoneName)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Zone added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            LoadZones() ' Refresh grid and clear form

        Catch ex As MySqlException
            If ex.Number = 1062 Then ' Error code for duplicate entry
                ErrorProvider1.SetError(txtZoneName, "This zone already exists in the system.")
            Else
                MessageBox.Show($"Database Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try



    End Sub

    Private Sub btnDeleteZone_Click(sender As Object, e As EventArgs) Handles btnDeleteZone.Click
        If selectedZoneId = 0 Then
            MessageBox.Show("Please select a zone from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to permanently delete this zone?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Dim deleteQuery As String = "DELETE FROM zones WHERE zone_id = @id"
                    Using cmd As New MySqlCommand(deleteQuery, conn)
                        cmd.Parameters.AddWithValue("@id", selectedZoneId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Zone deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadZones()

            Catch ex As MySqlException
                If ex.Number = 1451 Then ' Foreign Key Constraint Error
                    MessageBox.Show("Cannot delete this zone because there are registered accounts or active cases tied to it. You must reassign those first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Else
                    MessageBox.Show($"Failed to delete zone: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
        End If
    End Sub

    ' =================================================================
    ' GRID INTERACTION (Select / Double-Click to Update)
    ' =================================================================
    Private Sub dgvZones_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZones.CellClick
        If e.RowIndex >= 0 Then
            ' Just select the ID for potential deletion
            selectedZoneId = Convert.ToInt32(dgvZones.Rows(e.RowIndex).Cells("zone_id").Value)
        End If
    End Sub

    Private Sub dgvZones_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZones.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvZones.Rows(e.RowIndex)
            selectedZoneId = Convert.ToInt32(row.Cells("zone_id").Value)
            txtZoneName.Text = row.Cells("zone_name").Value.ToString()

            ' Morph into Update Mode
            isUpdateMode = True
            btnAddZone.Text = "Update Zone"
            btnAddZone.BackColor = Drawing.Color.DarkOrange ' Visual cue for edit mode
            txtZoneName.Focus()
        End If
    End Sub

    Private Sub txtZoneName_TextChanged(sender As Object, e As EventArgs) Handles txtZoneName.TextChanged
        ' If the user clears the text while in update mode, reset back to Add mode
        If isUpdateMode AndAlso String.IsNullOrWhiteSpace(txtZoneName.Text) Then
            ClearForm()
        End If
    End Sub

    Private Sub ClearForm()
        txtZoneName.Clear()
        selectedZoneId = 0
        isUpdateMode = False
        btnAddZone.Text = "Add Zone"
        btnAddZone.BackColor = Drawing.Color.FromArgb(0, 0, 64) ' Reset to standard button color
        ErrorProvider1.Clear()
    End Sub

End Class