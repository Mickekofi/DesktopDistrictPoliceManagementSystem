Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic

Public Class UC_HandleCrimeCase

    Private officerToolTip As New ToolTip()
    Private isLoading As Boolean = True

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_HandleCrimeCase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Styling & UX Tooltips
        officerToolTip.IsBalloon = True
        officerToolTip.ToolTipIcon = ToolTipIcon.Info

        officerToolTip.SetToolTip(dgvCrimeCases, "Your active and historical case assignments.")
        officerToolTip.SetToolTip(cmbSetTaskStatus, "Update the operational status of the selected case.")
        officerToolTip.SetToolTip(cmbFilterCrimeCategory, "Filter your assigned workload by crime category.")
        officerToolTip.SetToolTip(cmbFilterZone, "Filter your assigned workload by zone.")
        officerToolTip.SetToolTip(btnConfirmStatus, "Commit the status change. Requires an official field report note.")
        officerToolTip.SetToolTip(btnExport, "Export your unit's filtered assignment history.")

        ' 2. UI Lockdowns 
        cmbSetTaskStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbFilterCrimeCategory.DropDownStyle = ComboBoxStyle.Simple
        cmbFilterZone.DropDownStyle = ComboBoxStyle.Simple

        ' Grid UI Lockdown (Using your Global Helper Module)
        dgvCrimeCases.AllowUserToAddRows = False
        dgvCrimeCases.AllowUserToDeleteRows = False
        dgvCrimeCases.ReadOnly = True
        dgvCrimeCases.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCrimeCases.MultiSelect = False
        dgvCrimeCases.RowHeadersVisible = False

        DataGridViewHelper.ApplyBeautifulStyle(dgvCrimeCases)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvCrimeCases, 40)

        ' 3. Load Available Statuses for Officers
        cmbSetTaskStatus.Items.Clear()
        cmbSetTaskStatus.Items.AddRange(New String() {"IN_PROGRESS", "COMPLETED"})

        ' 4. Load Database Filters
        LoadFilters()

        isLoading = False
        LoadOfficerWorkload()
    End Sub

    Private Sub LoadFilters()
        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' Load Categories
                Dim catQuery As String = "SELECT category_id, category_name FROM crime_categories ORDER BY category_name ASC"
                Using cmd As New MySqlCommand(catQuery, conn)
                    Dim dtCat As New DataTable()
                    dtCat.Columns.Add("category_id", GetType(Integer))
                    dtCat.Columns.Add("category_name", GetType(String))
                    dtCat.Rows.Add(-1, "ALL CATEGORIES")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            dtCat.Rows.Add(reader("category_id"), reader("category_name"))
                        End While
                    End Using

                    cmbFilterCrimeCategory.DataSource = dtCat
                    cmbFilterCrimeCategory.DisplayMember = "category_name"
                    cmbFilterCrimeCategory.ValueMember = "category_id"
                    cmbFilterCrimeCategory.SelectedIndex = 0
                End Using

                ' Load Zones
                Dim zoneQuery As String = "SELECT zone_id, zone_name FROM zones ORDER BY zone_name ASC"
                Using cmdZone As New MySqlCommand(zoneQuery, conn)
                    Dim dtZone As New DataTable()
                    dtZone.Columns.Add("zone_id", GetType(Integer))
                    dtZone.Columns.Add("zone_name", GetType(String))
                    dtZone.Rows.Add(-1, "ALL ZONES")

                    Using reader As MySqlDataReader = cmdZone.ExecuteReader()
                        While reader.Read()
                            dtZone.Rows.Add(reader("zone_id"), reader("zone_name"))
                        End While
                    End Using

                    cmbFilterZone.DataSource = dtZone
                    cmbFilterZone.DisplayMember = "zone_name"
                    cmbFilterZone.ValueMember = "zone_id"
                    cmbFilterZone.SelectedIndex = 0
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show($"Failed to load filters: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' WORKLOAD RETRIEVAL
    ' =================================================================
    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterCrimeCategory.SelectedIndexChanged, cmbFilterZone.SelectedIndexChanged
        If Not isLoading Then
            LoadOfficerWorkload()
        End If
    End Sub

    Private Sub LoadOfficerWorkload()
        If cmbFilterCrimeCategory.SelectedIndex = -1 OrElse cmbFilterZone.SelectedIndex = -1 Then Return

        Dim catId As Integer = Convert.ToInt32(cmbFilterCrimeCategory.SelectedValue)
        Dim zoneId As Integer = Convert.ToInt32(cmbFilterZone.SelectedValue)

        Dim query As String = "
            SELECT 
                c.case_id, 
                c.case_title AS 'Case Title', 
                c.victim_name AS 'Victim',
                c.status AS 'Case Status'
            FROM cases c
            INNER JOIN case_assignments ca ON c.case_id = ca.case_id
            WHERE ca.assigned_to_group = @myGroupId"

        If catId <> -1 Then query &= " AND c.category_id = @catId"
        If zoneId <> -1 Then query &= " AND c.zone_id = @zoneId"

        query &= " ORDER BY ca.assigned_date DESC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@myGroupId", SessionManager.CurrentAccountId)
                    If catId <> -1 Then cmd.Parameters.AddWithValue("@catId", catId)
                    If zoneId <> -1 Then cmd.Parameters.AddWithValue("@zoneId", zoneId)

                    Dim dtCases As New DataTable()
                    dtCases.Load(cmd.ExecuteReader())

                    dgvCrimeCases.DataSource = Nothing

                    If dtCases.Rows.Count > 0 Then
                        dgvCrimeCases.DataSource = dtCases

                        dgvCrimeCases.Columns("case_id").Visible = False
                        dgvCrimeCases.Columns("Case Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        dgvCrimeCases.Columns("Victim").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                        dgvCrimeCases.Columns("Case Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                        dgvCrimeCases.ClearSelection()
                        ClearCaseDetails()
                    Else
                        dgvCrimeCases.Rows.Clear()
                        ClearCaseDetails()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading your workload: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' CASE SELECTION & DETAILS POPULATION
    ' =================================================================
    Private Sub dgvCrimeCases_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCrimeCases.SelectionChanged
        If dgvCrimeCases.CurrentRow Is Nothing OrElse dgvCrimeCases.CurrentRow.Index < 0 Then
            ClearCaseDetails()
            Return
        End If

        Dim caseId As Integer
        If dgvCrimeCases.CurrentRow.Cells("case_id").Value IsNot Nothing AndAlso Integer.TryParse(dgvCrimeCases.CurrentRow.Cells("case_id").Value.ToString(), caseId) Then
            FetchCaseDetails(caseId)
        Else
            ClearCaseDetails()
        End If
    End Sub

    Private Sub FetchCaseDetails(caseId As Integer)
        Dim query As String = "
            SELECT 
                c.case_title, c.incident_description, c.victim_name, c.suspect_name, c.incident_date,
                cat.category_name, z.zone_name, a.account_name AS inspector_name,
                e.file_path, c.status AS current_status
            FROM cases c
            LEFT JOIN crime_categories cat ON c.category_id = cat.category_id
            LEFT JOIN zones z ON c.zone_id = z.zone_id
            LEFT JOIN accounts a ON c.reported_by_account = a.account_id
            LEFT JOIN evidence_records e ON c.case_id = e.case_id
            WHERE c.case_id = @id LIMIT 1"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", caseId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtTitle.Text = reader("case_title").ToString()
                            txtCaseDescription.Text = reader("incident_description").ToString()
                            txtVictimName.Text = reader("victim_name").ToString()
                            txtSuspectName.Text = reader("suspect_name").ToString()

                            Dim incDate As DateTime = Convert.ToDateTime(reader("incident_date"))
                            uiii.Text = incDate.ToString("dd MMMM yyyy")

                            cmbCrimeCategory.Text = reader("category_name").ToString()
                            cmbZone.Text = reader("zone_name").ToString()
                            lblReportedByInspector.Text = "Inspector: " & reader("inspector_name").ToString()

                            Dim currentStatus As String = reader("current_status").ToString()
                            If cmbSetTaskStatus.Items.Contains(currentStatus) Then
                                cmbSetTaskStatus.SelectedItem = currentStatus
                            Else
                                cmbSetTaskStatus.SelectedIndex = -1
                            End If

                            pbxImageEvidence.Image = Nothing
                            If Not IsDBNull(reader("file_path")) Then
                                Dim fileName As String = reader("file_path").ToString()
                                Dim fullPath As String = Path.Combine(Application.StartupPath, "EvidenceVault", fileName)
                                If File.Exists(fullPath) Then
                                    pbxImageEvidence.ImageLocation = fullPath
                                    pbxImageEvidence.SizeMode = PictureBoxSizeMode.Zoom
                                Else
                                    pbxImageEvidence.Image = Nothing
                                End If
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to fetch case details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearCaseDetails()
        txtTitle.Clear()
        txtCaseDescription.Clear()
        txtVictimName.Clear()
        txtSuspectName.Clear()
        uiii.Text = "---"
        cmbCrimeCategory.Text = ""
        cmbZone.Text = ""
        lblReportedByInspector.Text = "Inspector: ---"

        pbxImageEvidence.Image = Nothing
        pbxImageEvidence.ImageLocation = Nothing
        cmbSetTaskStatus.SelectedIndex = -1
    End Sub

    ' =================================================================
    ' INVESTIGATION UPDATE LOGIC (TRANSACTIONAL)
    ' =================================================================
    Private Sub btnConfirmStatus_Click(sender As Object, e As EventArgs) Handles btnConfirmStatus.Click
        If dgvCrimeCases.CurrentRow Is Nothing Then
            MessageBox.Show("Select a valid case from your workload first.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmbSetTaskStatus.SelectedIndex = -1 Then
            MessageBox.Show("Select a valid status to apply to this case.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim caseId As Integer = Convert.ToInt32(dgvCrimeCases.CurrentRow.Cells("case_id").Value)
        Dim newStatus As String = cmbSetTaskStatus.SelectedItem.ToString()
        Dim currentStatus As String = dgvCrimeCases.CurrentRow.Cells("Case Status").Value.ToString()

        If newStatus = currentStatus Then
            MessageBox.Show($"Case is already marked as {newStatus}.", "Redundant Action", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim fieldNote As String = InputBox($"Provide a required field report detailing why this case is being moved to {newStatus}:", "Official Investigation Update").Trim()

        If String.IsNullOrWhiteSpace(fieldNote) Then
            MessageBox.Show("Status change aborted. An official operational note is legally required.", "Compliance Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        If MessageBox.Show($"Are you sure you want to officially update this case to {newStatus}?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Using transaction = conn.BeginTransaction()
                        Try
                            Dim updateCaseQuery As String = "UPDATE cases SET status = @status WHERE case_id = @cId"
                            Using cmdCase As New MySqlCommand(updateCaseQuery, conn, transaction)
                                cmdCase.Parameters.AddWithValue("@status", newStatus)
                                cmdCase.Parameters.AddWithValue("@cId", caseId)
                                cmdCase.ExecuteNonQuery()
                            End Using

                            Dim insertLogQuery As String = "INSERT INTO investigation_updates (case_id, updated_by_account, description, update_date, created_at) VALUES (@cId, @accountId, @desc, CURDATE(), NOW())"
                            Using cmdLog As New MySqlCommand(insertLogQuery, conn, transaction)
                                cmdLog.Parameters.AddWithValue("@cId", caseId)
                                cmdLog.Parameters.AddWithValue("@accountId", SessionManager.CurrentAccountId)
                                cmdLog.Parameters.AddWithValue("@desc", $"Status changed to {newStatus}: {fieldNote}")
                                cmdLog.ExecuteNonQuery()
                            End Using

                            If newStatus = "COMPLETED" Then
                                Dim closeAssignQuery As String = "UPDATE case_assignments SET status = 'COMPLETED' WHERE case_id = @cId AND assigned_to_group = @accountId"
                                Using cmdAssign As New MySqlCommand(closeAssignQuery, conn, transaction)
                                    cmdAssign.Parameters.AddWithValue("@cId", caseId)
                                    cmdAssign.Parameters.AddWithValue("@accountId", SessionManager.CurrentAccountId)
                                    cmdAssign.ExecuteNonQuery()
                                End Using
                            End If

                            transaction.Commit()
                            MessageBox.Show("Case successfully updated and logged in the operational audit trail.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            LoadOfficerWorkload()

                        Catch ex As Exception
                            transaction.Rollback()
                            Throw New Exception($"Status update failed. Database rolled back. {ex.Message}")
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' =================================================================
    ' SMART EXPORT LOGIC
    ' =================================================================
    Private Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim catId = If(cmbFilterCrimeCategory.SelectedIndex <> -1, Convert.ToInt32(cmbFilterCrimeCategory.SelectedValue), -1)
        Dim zoneId = If(cmbFilterZone.SelectedIndex <> -1, Convert.ToInt32(cmbFilterZone.SelectedValue), -1)

        Dim query = "
            SELECT 
                c.case_number AS 'Case Number',
                c.case_title AS 'Case Title', 
                cat.category_name AS 'Category', 
                z.zone_name AS 'Zone', 
                c.status AS 'Current Status',
                c.victim_name AS 'Victim',
                c.suspect_name AS 'Suspect',
                DATE_FORMAT(c.incident_date, '%Y-%m-%d') AS 'Incident Date',
                a.account_name AS 'Reporting Inspector',
                DATE_FORMAT(ca.assigned_date, '%Y-%m-%d %H:%i') AS 'Assigned To Us',
                COALESCE(e.file_path, 'No Evidence Attached') AS 'Evidence File Name'
            FROM cases c
            INNER JOIN case_assignments ca ON c.case_id = ca.case_id
            LEFT JOIN crime_categories cat ON c.category_id = cat.category_id
            LEFT JOIN zones z ON c.zone_id = z.zone_id
            LEFT JOIN accounts a ON c.reported_by_account = a.account_id
            LEFT JOIN evidence_records e ON c.case_id = e.case_id
            WHERE ca.assigned_to_group = @myGroupId"

        If catId <> -1 Then query &= " AND c.category_id = @catId"
        If zoneId <> -1 Then query &= " AND c.zone_id = @zoneId"

        query &= " ORDER BY ca.assigned_date DESC"

        Try
            Using conn = Database.CreateOpenConnection
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@myGroupId", SessionManager.CurrentAccountId)
                    If catId <> -1 Then cmd.Parameters.AddWithValue("@catId", catId)
                    If zoneId <> -1 Then cmd.Parameters.AddWithValue("@zoneId", zoneId)

                    Dim dtExport As New DataTable
                    dtExport.Load(cmd.ExecuteReader)

                    If dtExport.Rows.Count = 0 Then
                        MessageBox.Show("No workload data available to export under current filters.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    Using sfd As New SaveFileDialog
                        sfd.Filter = "CSV files (*.csv)|*.csv"
                        sfd.Title = "Export Unit Workload Report"
                        sfd.FileName = $"Unit_Workload_Report_{Date.Now.ToString("yyyyMMdd_HHmmss")}.csv"

                        If sfd.ShowDialog = DialogResult.OK Then
                            WriteDataTableToCSV(dtExport, sfd.FileName)
                            MessageBox.Show("Workload exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WriteDataTableToCSV(dt As DataTable, filePath As String)
        Dim sb As New StringBuilder()
        Dim columnNames As IEnumerable(Of String) = dt.Columns.Cast(Of DataColumn)().Select(Function(column) column.ColumnName)
        sb.AppendLine(String.Join(",", columnNames))

        For Each row As DataRow In dt.Rows
            Dim fields As IEnumerable(Of String) = row.ItemArray.Select(Function(field)
                                                                            Dim fieldString As String = If(field IsNot Nothing, field.ToString().Replace("""", """'"), "")
                                                                            Return $"""{fieldString}"""
                                                                        End Function)
            sb.AppendLine(String.Join(",", fields))
        Next

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub
End Class