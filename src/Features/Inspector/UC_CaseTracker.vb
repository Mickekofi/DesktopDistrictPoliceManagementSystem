Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class UC_CaseTracker

    Private trackerToolTip As New ToolTip()
    Private isLoading As Boolean = True

    ' Cache for images to prevent continuous disk I/O lag while scrolling
    Private imageCache As New Dictionary(Of String, Image)

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_CaseTracker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. UX Tooltips
        trackerToolTip.IsBalloon = True
        trackerToolTip.ToolTipIcon = ToolTipIcon.Info
        trackerToolTip.SetToolTip(cmbFilterStatus, "Filter your reported cases by their current status.")
        trackerToolTip.SetToolTip(cmbFilterByVictim, "Search your cases by the victim's name.")
        trackerToolTip.SetToolTip(dgvTrackCases, "Displays the case ledger. Images are dynamically loaded to save memory.")
        trackerToolTip.SetToolTip(btnPrint, "Generate and open a printable official HTML report for the selected case.")

        ' 2. UI Lockdowns & Styling
        cmbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbFilterByVictim.DropDownStyle = ComboBoxStyle.DropDownList

        dgvTrackCases.AllowUserToAddRows = False
        dgvTrackCases.AllowUserToDeleteRows = False
        dgvTrackCases.ReadOnly = True
        dgvTrackCases.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTrackCases.MultiSelect = False
        dgvTrackCases.RowHeadersVisible = False

        DataGridViewHelper.ApplyBeautifulStyle(dgvTrackCases)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvTrackCases, 60) ' Taller rows for thumbnails

        ' 3. Load Dropdowns & Calculate Stats
        LoadDropdowns()
        CalculateStatistics()

        isLoading = False
        LoadTrackingGrid()
    End Sub

    Private Sub LoadDropdowns()
        cmbFilterStatus.Items.Clear()
        cmbFilterStatus.Items.AddRange(New String() {"ALL", "NEW", "ASSIGNED", "IN_PROGRESS", "COMPLETED", "CANCELLED"})
        cmbFilterStatus.SelectedIndex = 0

        ' Load ONLY victims associated with cases filed by THIS Inspector
        Dim victimQuery As String = "SELECT DISTINCT victim_name FROM cases WHERE reported_by_account = @myId ORDER BY victim_name ASC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(victimQuery, conn)
                    cmd.Parameters.AddWithValue("@myId", SessionManager.CurrentAccountId)

                    Dim dtVictims As New DataTable()
                    dtVictims.Columns.Add("victim_name", GetType(String))
                    dtVictims.Rows.Add("ALL VICTIMS")

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            dtVictims.Rows.Add(reader("victim_name"))
                        End While
                    End Using

                    cmbFilterByVictim.DataSource = dtVictims
                    cmbFilterByVictim.DisplayMember = "victim_name"
                    cmbFilterByVictim.ValueMember = "victim_name"
                    cmbFilterByVictim.SelectedIndex = 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Filter loading failed: {ex.Message}")
        End Try
    End Sub

    ' =================================================================
    ' STATISTICS ENGINE
    ' =================================================================
    Private Sub CalculateStatistics()
        ' Calculates total cases and cases mathematically marked as COMPLETED
        Dim query As String = "
            SELECT 
                COUNT(case_id) AS TotalCases,
                SUM(CASE WHEN status = 'COMPLETED' THEN 1 ELSE 0 END) AS CompletedCases
            FROM cases 
            WHERE reported_by_account = @myId"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@myId", SessionManager.CurrentAccountId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim total As Integer = Convert.ToInt32(If(IsDBNull(reader("TotalCases")), 0, reader("TotalCases")))
                            Dim completed As Integer = Convert.ToInt32(If(IsDBNull(reader("CompletedCases")), 0, reader("CompletedCases")))

                            Dim percentage As Double = 0
                            If total > 0 Then
                                percentage = Math.Round((completed / total) * 100, 1)
                            End If

                            ' Populating purely the digits as requested
                            lblTotalCasesFiled.Text = total.ToString()
                            lblTotalCasesCompleted.Text = completed.ToString()
                            lblTotalPercentage.Text = percentage.ToString("0.0")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Fallback to zero if the database connection drops to prevent UI layout breaking
            lblTotalCasesFiled.Text = "0"
            lblTotalCasesCompleted.Text = "0"
            lblTotalPercentage.Text = "0.0"
        End Try
    End Sub

    ' =================================================================
    ' MASTER GRID LOADING (WITH VIRTUALIZED IMAGE PATHS)
    ' =================================================================
    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterStatus.SelectedIndexChanged, cmbFilterByVictim.SelectedIndexChanged
        If Not isLoading Then
            LoadTrackingGrid()
        End If
    End Sub

    Private Sub LoadTrackingGrid()
        If cmbFilterStatus.SelectedIndex = -1 OrElse cmbFilterByVictim.SelectedIndex = -1 Then Return

        Dim statusFilter As String = cmbFilterStatus.SelectedItem.ToString()
        Dim victimFilter As String = cmbFilterByVictim.SelectedValue.ToString()

        ' We pull the file_path but hide it. We will use CellFormatting to render the image.
        Dim query As String = "
            SELECT 
                c.case_id, 
                c.case_number AS 'Case No.',
                c.case_title AS 'Title', 
                c.victim_name AS 'Victim',
                c.status AS 'Status',
                DATE_FORMAT(c.created_at, '%b %d, %Y') AS 'Date Filed',
                e.file_path
            FROM cases c
            LEFT JOIN evidence_records e ON c.case_id = e.case_id
            WHERE c.reported_by_account = @myId"

        If statusFilter <> "ALL" Then query &= $" AND c.status = '{statusFilter}'"
        If victimFilter <> "ALL VICTIMS" Then query &= $" AND c.victim_name = @victim"

        query &= " ORDER BY c.created_at DESC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@myId", SessionManager.CurrentAccountId)
                    If victimFilter <> "ALL VICTIMS" Then cmd.Parameters.AddWithValue("@victim", victimFilter)

                    Dim dt As New DataTable()
                    dt.Load(cmd.ExecuteReader())

                    dgvTrackCases.DataSource = Nothing
                    dgvTrackCases.Columns.Clear()

                    If dt.Rows.Count > 0 Then
                        dgvTrackCases.DataSource = dt

                        ' Create the empty Image Column
                        Dim imgCol As DataGridViewImageColumn = DataGridViewHelper.CreatePhotoColumn("Evidence")
                        imgCol.Name = "EvidenceImage"
                        dgvTrackCases.Columns.Insert(2, imgCol) ' Insert near the front

                        ' Hide internal IDs
                        dgvTrackCases.Columns("case_id").Visible = False
                        dgvTrackCases.Columns("file_path").Visible = False

                        dgvTrackCases.ClearSelection()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading tracking board: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' DYNAMIC IMAGE RENDERING (PREVENTS MEMORY CRASHES)
    ' =================================================================
    Private Sub dgvTrackCases_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvTrackCases.CellFormatting
        If dgvTrackCases.Columns(e.ColumnIndex).Name = "EvidenceImage" AndAlso e.RowIndex >= 0 Then
            Dim pathObj = dgvTrackCases.Rows(e.RowIndex).Cells("file_path").Value

            If pathObj IsNot Nothing AndAlso Not IsDBNull(pathObj) Then
                Dim fileName As String = pathObj.ToString()
                Dim fullPath As String = Path.Combine(Application.StartupPath, "EvidenceVault", fileName)

                If File.Exists(fullPath) Then
                    ' Check Cache to prevent reading disk on every frame paint
                    If Not imageCache.ContainsKey(fullPath) Then
                        Try
                            ' Load and heavily scale down the image for memory protection
                            Using originalImg As Image = Image.FromFile(fullPath)
                                Dim thumbnail As New Bitmap(originalImg, New Size(100, 60))
                                imageCache.Add(fullPath, thumbnail)
                            End Using
                        Catch ex As Exception
                            ' Corrupt image
                            Return
                        End Try
                    End If
                    e.Value = imageCache(fullPath)
                End If
            End If
        End If
    End Sub

    ' =================================================================
    ' PROFESSIONAL HTML PRINT GENERATOR
    ' =================================================================
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If dgvTrackCases.CurrentRow Is Nothing Then
            MessageBox.Show("Select a case record to generate a victim print report.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim caseId As Integer = Convert.ToInt32(dgvTrackCases.CurrentRow.Cells("case_id").Value)

        ' Fetch all deep details for the print out
        Dim query As String = "
            SELECT 
                c.case_number, c.case_title, c.incident_description, c.victim_name, c.status, c.incident_date,
                z.zone_name, cat.category_name, a.account_name AS inspector_name
            FROM cases c
            LEFT JOIN zones z ON c.zone_id = z.zone_id
            LEFT JOIN crime_categories cat ON c.category_id = cat.category_id
            LEFT JOIN accounts a ON c.reported_by_account = a.account_id
            WHERE c.case_id = @id"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", caseId)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            GenerateAndOpenHtmlReport(reader)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to generate print report: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerateAndOpenHtmlReport(reader As MySqlDataReader)
        Dim caseNo As String = reader("case_number").ToString()
        Dim printFilePath As String = Path.Combine(Path.GetTempPath(), $"VictimReport_{caseNo}.html")

        Dim html As New StringBuilder()
        html.AppendLine("<!DOCTYPE html><html><head><style>")
        html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; padding: 40px; color: #333; }")
        html.AppendLine(".header { border-bottom: 3px solid #DarkRed; padding-bottom: 10px; margin-bottom: 20px; }")
        html.AppendLine(".title { font-size: 24px; font-weight: bold; color: #DarkRed; }")
        html.AppendLine(".meta-data { margin-bottom: 20px; font-size: 14px; background: #f9f9f9; padding: 15px; border-radius: 5px; }")
        html.AppendLine(".field { font-weight: bold; }")
        html.AppendLine(".status { font-size: 18px; font-weight: bold; padding: 10px; background: #eee; text-align: center; border: 1px solid #ccc; margin-top: 30px; }")
        html.AppendLine("</style></head><body>")

        html.AppendLine("<div class='header'>")
        html.AppendLine("<div class='title'>DISTRICT POLICE MANAGEMENT SYSTEM</div>")
        html.AppendLine($"<div>Official Victim Case Report</div>")
        html.AppendLine("</div>")

        html.AppendLine("<div class='meta-data'>")
        html.AppendLine($"<p><span class='field'>Case Number:</span> {caseNo}</p>")
        html.AppendLine($"<p><span class='field'>Victim Name:</span> {reader("victim_name")}</p>")
        html.AppendLine($"<p><span class='field'>Filing Inspector:</span> {reader("inspector_name")}</p>")
        html.AppendLine($"<p><span class='field'>Date of Incident:</span> {Convert.ToDateTime(reader("incident_date")).ToString("dd MMMM yyyy")}</p>")
        html.AppendLine($"<p><span class='field'>Jurisdiction/Zone:</span> {reader("zone_name")}</p>")
        html.AppendLine($"<p><span class='field'>Classification:</span> {reader("category_name")}</p>")
        html.AppendLine("</div>")

        html.AppendLine($"<h3>Incident Classification: {reader("case_title")}</h3>")
        html.AppendLine($"<p>{reader("incident_description")}</p>")

        html.AppendLine($"<div class='status'>CURRENT INVESTIGATION STATUS: {reader("status")}</div>")

        html.AppendLine("<p style='margin-top: 50px; font-size: 12px; color: #777; text-align: center;'>This document is generated for informational tracking purposes by the DPMS. Present this Case Number for any official inquiries.</p>")
        html.AppendLine("</body></html>")

        File.WriteAllText(printFilePath, html.ToString())

        ' Launch in default browser for easy formatting and printing
        Process.Start(New ProcessStartInfo(printFilePath) With {.UseShellExecute = True})
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint
    End Sub

    Private Sub dgvTrackCases_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTrackCases.CellContentClick
    End Sub
End Class