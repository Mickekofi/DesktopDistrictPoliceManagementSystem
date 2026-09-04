Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data

Public Class UC_CreateCrimeCategory

    Private bsCategories As New BindingSource()
    Private dtCategories As DataTable
    Private errorProv As New ErrorProvider()

    ' Regex Pattern: Allows letters, spaces, hyphens, and basic punctuation for category names
    Private Const REGEX_CAT_NAME As String = "^[a-zA-Z0-9\s\-_/&]+$"

    ' =================================================================
    ' 1. INITIALIZATION & UX SETUP
    ' =================================================================
    Private Sub UC_CreateCrimeCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Apply UI Styling 
        RadiusButton(btnAddCategory, 1.5F)
        RadiusButton(btnDelete, 1.5F)
        DataGridViewHelper.ApplyBeautifulStyle(dgvCrimeCat)
        DataGridViewHelper.AdjustDataGridViewRowHeight(dgvCrimeCat, 40)

        ' Grid UI Lockdown
        dgvCrimeCat.AllowUserToAddRows = False
        dgvCrimeCat.AllowUserToDeleteRows = False
        dgvCrimeCat.ReadOnly = True
        dgvCrimeCat.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCrimeCat.MultiSelect = False
        dgvCrimeCat.RowHeadersVisible = False

        ' UX GUIDANCE: Setup Tooltips
        Dim catToolTip As New ToolTip() With {
            .ToolTipIcon = ToolTipIcon.Info,
            .IsBalloon = True,
            .AutoPopDelay = 5000,
            .InitialDelay = 500
        }
        catToolTip.SetToolTip(txtCategory_name, "Enter the official category name (e.g., Theft, Vandalism).")
        catToolTip.SetToolTip(txtCategorySearch, "Type to instantly filter the category list.")
        catToolTip.SetToolTip(btnAddCategory, "Validate and save the new category to the system.")
        catToolTip.SetToolTip(btnDelete, "Permanently delete the selected category (Only if unused).")
        catToolTip.SetToolTip(dgvCrimeCat, "Double-click any row to edit the category details.")

        ' Ensure ErrorProvider doesn't blink annoyingly
        errorProv.BlinkStyle = ErrorBlinkStyle.NeverBlink

        LoadCategoriesGrid()
    End Sub

    ' =================================================================
    ' 2. DATA RETRIEVAL & LIVE SEARCH
    ' =================================================================
    Private Sub LoadCategoriesGrid()
        Dim query As String = "SELECT category_id, category_name FROM crime_categories ORDER BY category_name ASC"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmd As New MySqlCommand(query, conn)
                    dtCategories = New DataTable()
                    dtCategories.Load(cmd.ExecuteReader())

                    bsCategories.DataSource = dtCategories
                    dgvCrimeCat.DataSource = bsCategories

                    ' Format Grid Columns
                    If dgvCrimeCat.Columns.Contains("category_id") Then
                        dgvCrimeCat.Columns("category_id").Visible = False
                    End If
                    If dgvCrimeCat.Columns.Contains("category_name") Then
                        dgvCrimeCat.Columns("category_name").HeaderText = "Official Crime Category"
                        dgvCrimeCat.Columns("category_name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    End If

                    dgvCrimeCat.ClearSelection()
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show($"Failed to load categories: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCategorySearch_TextChanged(sender As Object, e As EventArgs) Handles txtCategorySearch.TextChanged
        If dtCategories Is Nothing OrElse dtCategories.Rows.Count = 0 Then Return

        Dim searchTxt As String = txtCategorySearch.Text.Trim().Replace("'", "''")
        If String.IsNullOrEmpty(searchTxt) Then
            bsCategories.Filter = ""
        Else
            ' Live filter on the BindingSource (Memory side, extremely fast)
            bsCategories.Filter = $"category_name LIKE '%{searchTxt}%'"
        End If
    End Sub

    ' =================================================================
    ' 3. INSERT LOGIC (ON-LEAVE VALIDATION)
    ' =================================================================
    Private Sub txtCategory_name_Leave(sender As Object, e As EventArgs) Handles txtCategory_name.Leave
        If String.IsNullOrWhiteSpace(txtCategory_name.Text) OrElse Not Regex.IsMatch(txtCategory_name.Text.Trim(), REGEX_CAT_NAME) Then
            errorProv.SetError(txtCategory_name, "Category name must contain only letters, numbers, spaces, and standard characters (-_&/).")
        Else
            errorProv.SetError(txtCategory_name, "")
        End If
    End Sub

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        Dim catName As String = txtCategory_name.Text.Trim()

        ' Final Validation Check
        If String.IsNullOrWhiteSpace(catName) OrElse Not Regex.IsMatch(catName, REGEX_CAT_NAME) Then
            MessageBox.Show("Please provide a valid category name before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Dim insertQuery As String = "INSERT INTO crime_categories (category_name) VALUES (@name)"
                Using cmd As New MySqlCommand(insertQuery, conn)
                    cmd.Parameters.AddWithValue("@name", catName.ToUpper()) ' Standardize to uppercase
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Crime category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCategory_name.Clear()
            LoadCategoriesGrid()

        Catch ex As MySqlException
            If ex.Number = 1062 Then
                MessageBox.Show($"The category '{catName.ToUpper()}' already exists in the system.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show($"Failed to add category: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ' =================================================================
    ' 4. DELETION LOGIC (WITH FK PROTECTION)
    ' =================================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvCrimeCat.CurrentRow Is Nothing Then
            MessageBox.Show("Select a category from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim catId As Integer = Convert.ToInt32(dgvCrimeCat.CurrentRow.Cells("category_id").Value)
        Dim catName As String = dgvCrimeCat.CurrentRow.Cells("category_name").Value.ToString()

        Dim warningMsg As String = $"Are you absolutely sure you want to permanently delete the category '{catName}'?" & vbCrLf & vbCrLf &
                                   "WARNING: If there are any criminal cases actively using this category, the deletion will be blocked by the database."

        If MessageBox.Show(warningMsg, "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Try
                Using conn As MySqlConnection = Database.CreateOpenConnection()
                    Dim deleteQuery As String = "DELETE FROM crime_categories WHERE category_id = @id"
                    Using cmd As New MySqlCommand(deleteQuery, conn)
                        cmd.Parameters.AddWithValue("@id", catId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show($"Category '{catName}' was successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCategoriesGrid()

            Catch ex As MySqlException
                If ex.Number = 1451 Then
                    MessageBox.Show($"DELETION BLOCKED." & vbCrLf & vbCrLf &
                                    $"The category '{catName}' cannot be deleted because it is currently assigned to existing cases. " &
                                    "Erasing it would corrupt police records.",
                                    "Database Integrity Constraint", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Else
                    MessageBox.Show($"System Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
        End If
    End Sub

    ' =================================================================
    ' 5. UPDATE LOGIC (IN-MEMORY DIALOG)
    ' =================================================================
    Private Sub dgvCrimeCat_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCrimeCat.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim catId As Integer = Convert.ToInt32(dgvCrimeCat.Rows(e.RowIndex).Cells("category_id").Value)
        Dim currentName As String = dgvCrimeCat.Rows(e.RowIndex).Cells("category_name").Value.ToString()

        Using editForm As New Form()
            editForm.Text = "Update Category"
            editForm.Size = New Size(350, 150)
            editForm.StartPosition = FormStartPosition.CenterParent
            editForm.FormBorderStyle = FormBorderStyle.FixedDialog
            editForm.MaximizeBox = False
            editForm.MinimizeBox = False

            Dim lblName As New Label() With {.Text = "Category Name:", .Location = New Point(20, 20), .AutoSize = True}
            Dim txtName As New TextBox() With {.Text = currentName, .Location = New Point(20, 45), .Width = 280}

            Dim btnSave As New Button() With {.Text = "Update", .Location = New Point(20, 80), .Width = 100, .BackColor = Color.DarkBlue, .ForeColor = Color.White}
            Dim btnCancel As New Button() With {.Text = "Cancel", .Location = New Point(130, 80), .Width = 100}

            editForm.Controls.AddRange(New Control() {lblName, txtName, btnSave, btnCancel})

            AddHandler btnCancel.Click, Sub(s, args) editForm.DialogResult = DialogResult.Cancel

            AddHandler btnSave.Click, Sub(s, args)
                                          If String.IsNullOrWhiteSpace(txtName.Text) OrElse Not Regex.IsMatch(txtName.Text.Trim(), REGEX_CAT_NAME) Then
                                              MessageBox.Show("Invalid category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If
                                          Try
                                              Using conn As MySqlConnection = Database.CreateOpenConnection()
                                                  Dim cmd As New MySqlCommand("UPDATE crime_categories SET category_name = @name WHERE category_id = @id", conn)
                                                  cmd.Parameters.AddWithValue("@name", txtName.Text.Trim().ToUpper())
                                                  cmd.Parameters.AddWithValue("@id", catId)
                                                  cmd.ExecuteNonQuery()
                                              End Using
                                              editForm.DialogResult = DialogResult.OK
                                          Catch ex As MySqlException
                                              If ex.Number = 1062 Then
                                                  MessageBox.Show("This category already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Else
                                                  MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End If
                                          End Try
                                      End Sub

            If editForm.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Category updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCategoriesGrid()
            End If
        End Using
    End Sub

    Private Sub PanelWithSearch_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithSearch.Paint
    End Sub

    Private Sub dgvCrimeCat_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCrimeCat.CellContentClick
    End Sub

End Class