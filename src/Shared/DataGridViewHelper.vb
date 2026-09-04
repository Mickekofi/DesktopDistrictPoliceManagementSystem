Imports System.Drawing
Imports System.Windows.Forms

Module DataGridViewHelper

    ' =================================================================
    ' GLOBAL UI CONFIGURATION: Change these to rebrand the entire app
    ' =================================================================
    Private ReadOnly ConfigHeaderBg As Color = Color.DarkRed
    Private ReadOnly ConfigHeaderFg As Color = Color.White

    Private ReadOnly ConfigRowBg As Color = Color.White
    Private ReadOnly ConfigRowFg As Color = Color.Black

    Private ReadOnly ConfigAltRowBg As Color = Color.WhiteSmoke

    Private ReadOnly ConfigSelectionBg As Color = Color.Black
    Private ReadOnly ConfigSelectionFg As Color = Color.White

    Private ReadOnly ConfigGridLine As Color = Color.LightGray

    ' =================================================================
    ' 🌟 Function to adjust row height
    ' =================================================================
    Public Sub AdjustDataGridViewRowHeight(dgv As DataGridView, height As Integer)
        dgv.RowTemplate.Height = height
        For Each row As DataGridViewRow In dgv.Rows
            row.Height = height
        Next
    End Sub

    ' =================================================================
    ' 🌟 Function to apply colorful style dynamically
    ' =================================================================
    Public Sub ApplyBeautifulStyle(dgv As DataGridView,
                                   Optional customHeaderBg As Color? = Nothing,
                                   Optional customAltRowBg As Color? = Nothing,
                                   Optional customSelectionBg As Color? = Nothing)

        ' Fallback to the Global Configuration if no custom color is passed
        Dim finalHeaderBg As Color = If(customHeaderBg, ConfigHeaderBg)
        Dim finalAltRowBg As Color = If(customAltRowBg, ConfigAltRowBg)
        Dim finalSelectionBg As Color = If(customSelectionBg, ConfigSelectionBg)

        With dgv
            .EnableHeadersVisualStyles = False

            ' Headers
            .ColumnHeadersDefaultCellStyle.BackColor = finalHeaderBg
            .ColumnHeadersDefaultCellStyle.ForeColor = ConfigHeaderFg
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            .ColumnHeadersHeight = 40

            ' Default Rows
            .DefaultCellStyle.BackColor = ConfigRowBg
            .DefaultCellStyle.ForeColor = ConfigRowFg
            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)

            ' Alternating Rows
            .AlternatingRowsDefaultCellStyle.BackColor = finalAltRowBg
            .AlternatingRowsDefaultCellStyle.ForeColor = ConfigRowFg

            ' Selection
            .DefaultCellStyle.SelectionBackColor = finalSelectionBg
            .DefaultCellStyle.SelectionForeColor = ConfigSelectionFg

            ' Grid Layout
            .GridColor = ConfigGridLine
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    ' =================================================================
    ' 🌟 Function to create a properly fitting Photo column
    ' =================================================================
    Public Function CreatePhotoColumn(Optional headerText As String = "Picture") As DataGridViewImageColumn
        Dim imgCol As New DataGridViewImageColumn() With {
            .Name = "Photo",
            .HeaderText = headerText,
            .ImageLayout = DataGridViewImageCellLayout.Stretch ' ✅ fits to cell
        }
        Return imgCol
    End Function

    ' How to use:
    ' Dim dgvApplicants As New DataGridView()
    ' DataGridViewHelper.ApplyBeautifulStyle(dgvAppliacants)



End Module
