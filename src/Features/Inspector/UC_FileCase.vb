Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.IO

Public Class UC_FileCase

    Private caseToolTip As New ToolTip()
    Private caseErrorProvider As New ErrorProvider()
    Private selectedEvidencePath As String = String.Empty

    ' The State Flag to prevent event loops during form reset
    Private isResetting As Boolean = False

    ' Regex Patterns
    Private Const REGEX_TEXT As String = "^[a-zA-Z0-9\s\-_.,()']+$"
    Private Const REGEX_NAME As String = "^[a-zA-Z\s\-']+$"

    ' =================================================================
    ' INITIALIZATION
    ' =================================================================
    Private Sub UC_FileCase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        caseErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' ToolTips for UX
        caseToolTip.IsBalloon = True
        caseToolTip.ToolTipIcon = ToolTipIcon.Info
        caseToolTip.SetToolTip(txtTitle, "Official classification title of the reported incident.")
        caseToolTip.SetToolTip(cmbCrimeCategory, "Select the legally defined category of the offense.")
        caseToolTip.SetToolTip(cmbZone, "Select the jurisdiction where the incident occurred.")
        caseToolTip.SetToolTip(txtCaseDescription, "Detailed, objective factual narrative of the incident.")
        caseToolTip.SetToolTip(txtVictimName, "Full legal name of the primary victim. (Use 'UNKNOWN' if not identified).")
        caseToolTip.SetToolTip(txtSuspectName, "Full legal name of the suspect. (Use 'UNKNOWN' if not identified).")
        caseToolTip.SetToolTip(btnUploadCasePhoto, "Attach primary photographic evidence (JPG, PNG).")
        caseToolTip.SetToolTip(dtpCaseDate, "Select the exact date the incident physically occurred.")
        caseToolTip.SetToolTip(btnCreateCase, "Submit official record. This action logs the reporting officer and timestamp.")

        LoadDropdowns()
        dtpCaseDate.MaxDate = DateTime.Now
    End Sub

    Private Sub LoadDropdowns()
        Dim catQuery As String = "SELECT category_id, category_name FROM crime_categories ORDER BY category_name ASC"
        Dim zoneQuery As String = "SELECT zone_id, zone_name FROM zones ORDER BY zone_name ASC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(catQuery, conn)
                    Dim dtCat As New DataTable()
                    dtCat.Load(cmd.ExecuteReader())
                    cmbCrimeCategory.DataSource = dtCat
                    cmbCrimeCategory.DisplayMember = "category_name"
                    cmbCrimeCategory.ValueMember = "category_id"
                    cmbCrimeCategory.SelectedIndex = -1
                End Using

                Using cmd As New MySqlCommand(zoneQuery, conn)
                    Dim dtZone As New DataTable()
                    dtZone.Load(cmd.ExecuteReader())
                    cmbZone.DataSource = dtZone
                    cmbZone.DisplayMember = "zone_name"
                    cmbZone.ValueMember = "zone_id"
                    cmbZone.SelectedIndex = -1
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show($"Database setup missing for dropdowns: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =================================================================
    ' EVIDENCE UPLOAD LOGIC
    ' =================================================================
    Private Sub btnUploadCasePhoto_Click(sender As Object, e As EventArgs) Handles btnUploadCasePhoto.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "Select Evidence Image"
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png"
            ofd.Multiselect = False

            If ofd.ShowDialog() = DialogResult.OK Then
                Dim fileInfo As New FileInfo(ofd.FileName)
                If fileInfo.Length > 5 * 1024 * 1024 Then
                    MessageBox.Show("Image exceeds 5MB limit. Please compress the file.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                selectedEvidencePath = ofd.FileName
                pbxEvidenceImage.ImageLocation = selectedEvidencePath
                pbxEvidenceImage.SizeMode = PictureBoxSizeMode.Zoom
                caseErrorProvider.SetError(btnUploadCasePhoto, "")
            End If
        End Using
    End Sub

    ' =================================================================
    ' LIVE VALIDATION (ONCHANGE) & KEY DOWN EVENTS
    ' =================================================================
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTitle.KeyDown, txtVictimName.KeyDown, txtSuspectName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.SelectNextControl(DirectCast(sender, Control), True, True, True, True)
        End If
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        If isResetting Then Return
        If String.IsNullOrWhiteSpace(txtTitle.Text) OrElse Not Regex.IsMatch(txtTitle.Text.Trim(), REGEX_TEXT) Then
            caseErrorProvider.SetError(txtTitle, "Title requires alphanumeric characters and standard punctuation.")
        Else
            caseErrorProvider.SetError(txtTitle, "")
        End If
    End Sub

    Private Sub txtCaseDescription_TextChanged(sender As Object, e As EventArgs) Handles txtCaseDescription.TextChanged
        If isResetting Then Return
        If String.IsNullOrWhiteSpace(txtCaseDescription.Text) OrElse Not Regex.IsMatch(txtCaseDescription.Text.Trim(), REGEX_TEXT) Then
            caseErrorProvider.SetError(txtCaseDescription, "Description requires alphanumeric characters and standard punctuation.")
        Else
            caseErrorProvider.SetError(txtCaseDescription, "")
        End If
    End Sub

    Private Sub txtVictimName_TextChanged(sender As Object, e As EventArgs) Handles txtVictimName.TextChanged
        If isResetting Then Return
        If String.IsNullOrWhiteSpace(txtVictimName.Text) OrElse Not Regex.IsMatch(txtVictimName.Text.Trim(), REGEX_NAME) Then
            caseErrorProvider.SetError(txtVictimName, "Letters, spaces, hyphens, and apostrophes only. (Use 'UNKNOWN' if missing).")
        Else
            caseErrorProvider.SetError(txtVictimName, "")
        End If
    End Sub

    Private Sub txtSuspectName_TextChanged(sender As Object, e As EventArgs) Handles txtSuspectName.TextChanged
        If isResetting Then Return
        If String.IsNullOrWhiteSpace(txtSuspectName.Text) OrElse Not Regex.IsMatch(txtSuspectName.Text.Trim(), REGEX_NAME) Then
            caseErrorProvider.SetError(txtSuspectName, "Letters, spaces, hyphens, and apostrophes only. (Use 'UNKNOWN' if missing).")
        Else
            caseErrorProvider.SetError(txtSuspectName, "")
        End If
    End Sub

    Private Function ValidateInputs() As Boolean
        Dim isValid As Boolean = True

        If String.IsNullOrWhiteSpace(txtTitle.Text) OrElse Not Regex.IsMatch(txtTitle.Text.Trim(), REGEX_TEXT) Then
            caseErrorProvider.SetError(txtTitle, "Title is required and must contain only standard characters.")
            isValid = False
        End If

        If dtpCaseDate.Value.Date > DateTime.Now.Date Then
            caseErrorProvider.SetError(dtpCaseDate, "Incident date cannot be in the future.")
            isValid = False
        End If
        If dtpCaseDate.Value.Date < DateTime.Now.Date.AddYears(-50) Then
            caseErrorProvider.SetError(dtpCaseDate, "Incident date exceeds reasonable historical limits (50 years).")
            isValid = False
        End If

        If cmbCrimeCategory.SelectedIndex = -1 Then
            caseErrorProvider.SetError(cmbCrimeCategory, "Crime Category is required.")
            isValid = False
        Else
            caseErrorProvider.SetError(cmbCrimeCategory, "")
        End If

        If cmbZone.SelectedIndex = -1 Then
            caseErrorProvider.SetError(cmbZone, "Zone/Jurisdiction is required.")
            isValid = False
        Else
            caseErrorProvider.SetError(cmbZone, "")
        End If

        If String.IsNullOrWhiteSpace(txtCaseDescription.Text) OrElse Not Regex.IsMatch(txtCaseDescription.Text.Trim(), REGEX_TEXT) Then
            caseErrorProvider.SetError(txtCaseDescription, "Description is required and must contain only standard characters.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtVictimName.Text) OrElse Not Regex.IsMatch(txtVictimName.Text.Trim(), REGEX_NAME) Then
            caseErrorProvider.SetError(txtVictimName, "Victim name is required (use 'UNKNOWN' if missing). Letters only.")
            isValid = False
        End If
        If String.IsNullOrWhiteSpace(txtSuspectName.Text) OrElse Not Regex.IsMatch(txtSuspectName.Text.Trim(), REGEX_NAME) Then
            caseErrorProvider.SetError(txtSuspectName, "Suspect name is required (use 'UNKNOWN' if missing). Letters only.")
            isValid = False
        End If

        If String.IsNullOrEmpty(selectedEvidencePath) Then
            caseErrorProvider.SetError(btnUploadCasePhoto, "Primary evidence image is required to file a case.")
            isValid = False
        End If

        Return isValid
    End Function

    ' =================================================================
    ' SECURE TRANSACTIONAL SUBMIT
    ' =================================================================
    Private Sub btnCreateCase_Click(sender As Object, e As EventArgs) Handles btnCreateCase.Click
        If Not ValidateInputs() Then
            MessageBox.Show("Please correct the highlighted errors before submitting.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to officially file this case? This action will be permanently audited.", "Confirm Submission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim appDataFolder As String = Path.Combine(Application.StartupPath, "EvidenceVault")
        If Not Directory.Exists(appDataFolder) Then
            Directory.CreateDirectory(appDataFolder)
        End If

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim uniqueCaseNumber As String = $"CN-{DateTime.Now.ToString("yyyyMMddHHmmss")}-{New Random().Next(10, 99)}"

                        Dim caseQuery As String = "INSERT INTO cases (case_number, case_title, incident_description, category_id, zone_id, victim_name, suspect_name, reported_by_account, incident_date, status, created_at) " &
                                                  "VALUES (@caseNo, @title, @desc, @cat, @zone, @victim, @suspect, @reporter, @incidentDate, 'NEW', NOW())"

                        Dim caseId As Integer
                        Using cmd As New MySqlCommand(caseQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@caseNo", uniqueCaseNumber)
                            cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim())
                            cmd.Parameters.AddWithValue("@desc", txtCaseDescription.Text.Trim())
                            cmd.Parameters.AddWithValue("@cat", cmbCrimeCategory.SelectedValue)
                            cmd.Parameters.AddWithValue("@zone", cmbZone.SelectedValue)
                            cmd.Parameters.AddWithValue("@victim", txtVictimName.Text.Trim())
                            cmd.Parameters.AddWithValue("@suspect", txtSuspectName.Text.Trim())
                            cmd.Parameters.AddWithValue("@reporter", SessionManager.CurrentAccountId)
                            cmd.Parameters.AddWithValue("@incidentDate", dtpCaseDate.Value.Date)
                            cmd.ExecuteNonQuery()

                            cmd.CommandText = "SELECT LAST_INSERT_ID()"
                            caseId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        Dim fileExt As String = Path.GetExtension(selectedEvidencePath)
                        Dim newFileName As String = $"CASE_{caseId}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExt}"
                        Dim secureFilePath As String = Path.Combine(appDataFolder, newFileName)
                        File.Copy(selectedEvidencePath, secureFilePath, True)

                        Dim evQuery As String = "INSERT INTO evidence_records (case_id, uploaded_by_account, file_path, created_at) " &
                                                "VALUES (@cId, @uId, @path, NOW())"
                        Using cmdEv As New MySqlCommand(evQuery, conn, transaction)
                            cmdEv.Parameters.AddWithValue("@cId", caseId)
                            cmdEv.Parameters.AddWithValue("@uId", SessionManager.CurrentAccountId)
                            cmdEv.Parameters.AddWithValue("@path", newFileName)
                            cmdEv.ExecuteNonQuery()
                        End Using

                        transaction.Commit()

                        MessageBox.Show($"Case #{caseId} ({uniqueCaseNumber}) filed successfully. Evidence secured.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetForm()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception($"Transaction failed. Database changes reverted. Details: {ex.Message}")
                    End Try
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        ' TURN ON THE SHIELD: Ignore text change events
        isResetting = True

        txtTitle.Clear()
        txtCaseDescription.Clear()
        txtVictimName.Clear()
        txtSuspectName.Clear()
        cmbCrimeCategory.SelectedIndex = -1
        cmbZone.SelectedIndex = -1
        pbxEvidenceImage.Image = Nothing
        selectedEvidencePath = String.Empty
        dtpCaseDate.Value = DateTime.Now

        caseErrorProvider.Clear()

        ' TURN OFF THE SHIELD: Resume normal validation
        isResetting = False
        txtTitle.Focus()
    End Sub

    Private Sub PanelInputBundle_Paint(sender As Object, e As PaintEventArgs) Handles PanelInputBundle.Paint
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

End Class