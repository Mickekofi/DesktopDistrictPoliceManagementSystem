Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.IO
Imports System.Text

Public Class UC_CaseFeeds

    Private adminToolTip As New ToolTip()
    Private isLoading As Boolean = True

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_CaseFeeds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Styling & UX Tooltips
        adminToolTip.IsBalloon = True
        adminToolTip.ToolTipIcon = ToolTipIcon.Info

        adminToolTip.SetToolTip(cmbFilterStatus, "Filter the master case list by operational status.")
        adminToolTip.SetToolTip(cmbFilterCrimeCategory, "Filter the master case list by crime classification.")
        adminToolTip.SetToolTip(cmbFilterZone, "Filter the master case list by jurisdiction.")
        adminToolTip.SetToolTip(dgvCrimeCases, "Select a specific case to view details and assign.")
        adminToolTip.SetToolTip(cmbAssignOfficer, "Select an available Officer Group to deploy to this case.")
        adminToolTip.SetToolTip(btnAssignOfficerGroup, "Officially dispatch the selected Officer Group. This updates the case status to 'ASSIGNED'.")
        adminToolTip.SetToolTip(btnExport, "Export the currently filtered case list to a secure CSV file.")

        ' 2. Enforce UI Lockdowns requested
        cmbCrimeCategory.DropDownStyle = ComboBoxStyle.Simple
        cmbZone.DropDownStyle = ComboBoxStyle.Simple

        ' Grid UI Lockdown for Tight Spaces
        dgvCrimeCases.AllowUserToAddRows = False
        dgvCrimeCases.AllowUserToDeleteRows = False
        dgvCrimeCases.ReadOnly = True
        dgvCrimeCases.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCrimeCases.MultiSelect = False
        dgvCrimeCases.RowHeadersVisible = False
        dgvCrimeCases.BackgroundColor = Drawing.Color.White

        'Apply consistent styling to the DataGridView
        DataGridViewHelper.ApplyBeautifulStyle(dgvCrimeCases)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvCrimeCases, 50)


        ' 3. Load Base Filters
        LoadFilters()

        isLoading = False
        LoadCasesIntoGrid()
    End Sub

    Private Sub LoadFilters()
        ' Load Status
        cmbFilterStatus.Items.Clear()
        cmbFilterStatus.Items.AddRange(New String() {"ALL", "NEW", "ASSIGNED", "IN_PROGRESS", "COMPLETED", "CANCELLED"})
        cmbFilterStatus.SelectedIndex = 1 ' Default to 'NEW'

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                ' Load Categories Filter
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

                ' Load Zones Filter
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
    ' MASTER CASE LOADING & FILTERING
    ' =================================================================
    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterStatus.SelectedIndexChanged, cmbFilterCrimeCategory.SelectedIndexChanged, cmbFilterZone.SelectedIndexChanged
        If Not isLoading Then
            LoadCasesIntoGrid()
        End If
    End Sub

    Private Sub LoadCasesIntoGrid()
        If cmbFilterStatus.SelectedIndex = -1 OrElse cmbFilterCrimeCategory.SelectedIndex = -1 OrElse cmbFilterZone.SelectedIndex = -1 Then Return

        Dim statusFilter As String = cmbFilterStatus.SelectedItem.ToString()
        Dim catId As Integer = Convert.ToInt32(cmbFilterCrimeCategory.SelectedValue)
        Dim zoneId As Integer = Convert.ToInt32(cmbFilterZone.SelectedValue)

        Dim query As String = "SELECT case_id, case_title AS 'Case Title', victim_name AS 'Victim' FROM cases WHERE 1=1"

        If statusFilter <> "ALL" Then
            query &= $" AND status = '{statusFilter}'"
        End If
        If catId <> -1 Then
            query &= $" AND category_id = {catId}"
        End If
        If zoneId <> -1 Then
            query &= $" AND zone_id = {zoneId}"
        End If

        query &= " ORDER BY created_at DESC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Dim dtCases As New DataTable()
                    dtCases.Load(cmd.ExecuteReader())

                    dgvCrimeCases.DataSource = Nothing

                    If dtCases.Rows.Count > 0 Then
                        dgvCrimeCases.DataSource = dtCases

                        ' Visual formatting
                        dgvCrimeCases.Columns("case_id").Visible = False
                        dgvCrimeCases.Columns("Case Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        dgvCrimeCases.Columns("Victim").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                        ' Force text color so it is always readable
                        dgvCrimeCases.DefaultCellStyle.ForeColor = Drawing.Color.Black
                        dgvCrimeCases.DefaultCellStyle.SelectionBackColor = Drawing.Color.DodgerBlue
                        dgvCrimeCases.DefaultCellStyle.SelectionForeColor = Drawing.Color.White

                        ' Allow WinForms to automatically fire SelectionChanged and load the first item
                    Else
                        ClearCaseDetails()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading cases: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                c.case_title, c.incident_description, c.victim_name, c.suspect_name, c.incident_date, c.zone_id,
                cat.category_name, z.zone_name, a.account_name AS inspector_name,
                e.file_path
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

                            ' Process Image
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

                            ' Load Available Officers based on Zone
                            Dim zoneId As Integer = Convert.ToInt32(reader("zone_id"))
                            LoadAvailableOfficers(zoneId)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to fetch case details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAvailableOfficers(zoneId As Integer)
        Dim query As String = "SELECT account_id, account_name FROM accounts WHERE role = 'OFFICER_GROUP' AND account_status = 'ACTIVE' AND zone_id = @zoneId"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@zoneId", zoneId)
                    Dim dtOfficers As New DataTable()
                    dtOfficers.Load(cmd.ExecuteReader())

                    cmbAssignOfficer.DataSource = dtOfficers
                    cmbAssignOfficer.DisplayMember = "account_name"
                    cmbAssignOfficer.ValueMember = "account_id"
                    cmbAssignOfficer.SelectedIndex = -1
                End Using
            End Using
        Catch ex As Exception
            ' Silent fail
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

        cmbAssignOfficer.DataSource = Nothing
        cmbAssignOfficer.Items.Clear()
    End Sub

    ' =================================================================
    ' ASSIGNMENT LOGIC (TRANSACTIONAL)
    ' =================================================================
    Private Sub btnAssignOfficerGroup_Click(sender As Object, e As EventArgs) Handles btnAssignOfficerGroup.Click
        If dgvCrimeCases.CurrentRow Is Nothing Then
            MessageBox.Show("Select a valid case first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmbAssignOfficer.SelectedValue Is Nothing Then
            MessageBox.Show("Select an Officer Group to assign.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim caseId As Integer = Convert.ToInt32(dgvCrimeCases.CurrentRow.Cells("case_id").Value)
        Dim officerGroupId As Integer = Convert.ToInt32(cmbAssignOfficer.SelectedValue)

        If MessageBox.Show("Officially deploy this Officer Group to the selected case?", "Confirm Dispatch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Using transaction = conn.BeginTransaction()
                        Try
                            ' 1. Insert into case_assignments
                            ' FIXED: Corrected 'assigned_at' to 'assigned_date' per schema
                            Dim assignQuery As String = "INSERT INTO case_assignments (case_id, assigned_to_group, assigned_by_account, assigned_date) VALUES (@cId, @gId, @aId, NOW())"
                            Using cmdAssign As New MySqlCommand(assignQuery, conn, transaction)
                                cmdAssign.Parameters.AddWithValue("@cId", caseId)
                                cmdAssign.Parameters.AddWithValue("@gId", officerGroupId)
                                cmdAssign.Parameters.AddWithValue("@aId", SessionManager.CurrentAccountId)
                                cmdAssign.ExecuteNonQuery()
                            End Using

                            ' 2. Update cases status
                            Dim updateQuery As String = "UPDATE cases SET status = 'ASSIGNED' WHERE case_id = @cId"
                            Using cmdUpdate As New MySqlCommand(updateQuery, conn, transaction)
                                cmdUpdate.Parameters.AddWithValue("@cId", caseId)
                                cmdUpdate.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Case successfully dispatched.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            LoadCasesIntoGrid()

                        Catch ex As Exception
                            transaction.Rollback()
                            Throw New Exception($"Assignment failed. Database rolled back. {ex.Message}")
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
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim statusFilter As String = If(cmbFilterStatus.SelectedIndex <> -1, cmbFilterStatus.SelectedItem.ToString(), "ALL")
        Dim catId As Integer = If(cmbFilterCrimeCategory.SelectedIndex <> -1, Convert.ToInt32(cmbFilterCrimeCategory.SelectedValue), -1)
        Dim zoneId As Integer = If(cmbFilterZone.SelectedIndex <> -1, Convert.ToInt32(cmbFilterZone.SelectedValue), -1)

        Dim query As String = "
            SELECT 
                c.case_id AS 'Case ID', 
                c.case_number AS 'Case Number',
                c.case_title AS 'Case Title', 
                cat.category_name AS 'Category', 
                z.zone_name AS 'Zone', 
                c.status AS 'Status',
                c.victim_name AS 'Victim',
                c.suspect_name AS 'Suspect',
                DATE_FORMAT(c.incident_date, '%Y-%m-%d') AS 'Incident Date',
                a.account_name AS 'Reporting Inspector',
                COALESCE(e.file_path, 'No Evidence Attached') AS 'Evidence File Name'
            FROM cases c
            LEFT JOIN crime_categories cat ON c.category_id = cat.category_id
            LEFT JOIN zones z ON c.zone_id = z.zone_id
            LEFT JOIN accounts a ON c.reported_by_account = a.account_id
            LEFT JOIN evidence_records e ON c.case_id = e.case_id
            WHERE 1=1"

        If statusFilter <> "ALL" Then query &= $" AND c.status = '{statusFilter}'"
        If catId <> -1 Then query &= $" AND c.category_id = {catId}"
        If zoneId <> -1 Then query &= $" AND c.zone_id = {zoneId}"

        query &= " ORDER BY c.created_at DESC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    Dim dtExport As New DataTable()
                    dtExport.Load(cmd.ExecuteReader())

                    If dtExport.Rows.Count = 0 Then
                        MessageBox.Show("No data available to export under current filters.", "Empty Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    Using sfd As New SaveFileDialog()
                        sfd.Filter = "CSV files (*.csv)|*.csv"
                        sfd.Title = "Export Filtered Cases Report"
                        sfd.FileName = $"Police_Case_Report_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv"

                        If sfd.ShowDialog() = DialogResult.OK Then
                            WriteDataTableToCSV(dtExport, sfd.FileName)
                            MessageBox.Show("Report exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        ' Write Headers
        Dim columnNames As IEnumerable(Of String) = dt.Columns.Cast(Of DataColumn)().Select(Function(column) column.ColumnName)
        sb.AppendLine(String.Join(",", columnNames))

        ' Write Rows
        For Each row As DataRow In dt.Rows
            Dim fields As IEnumerable(Of String) = row.ItemArray.Select(Function(field)
                                                                            Dim fieldString As String = If(field IsNot Nothing, field.ToString().Replace("""", """'"), "")
                                                                            Return $"""{fieldString}"""
                                                                        End Function)
            sb.AppendLine(String.Join(",", fields))
        Next

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    End Sub

    Private Sub PanelInputBundle_Paint(sender As Object, e As PaintEventArgs) Handles PanelInputBundle.Paint
    End Sub

    Private Sub dgvCrimeCases_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCrimeCases.CellContentClick
    End Sub

End Class