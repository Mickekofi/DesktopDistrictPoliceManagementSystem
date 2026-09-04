<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Statistics
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Panel3 = New Panel()
        dgvTrackCases = New DataGridView()
        Label6 = New Label()
        btnPrint = New Button()
        Label2 = New Label()
        cmbFilterByVictim = New ComboBox()
        Label12 = New Label()
        cmbFilterStatus = New ComboBox()
        Panel2 = New Panel()
        lblTotalCasesFiled = New Label()
        Label3 = New Label()
        Panel4 = New Panel()
        lblTotalCasesCompleted = New Label()
        Label5 = New Label()
        Panel5 = New Panel()
        lblTotalPercentage = New Label()
        Label8 = New Label()
        Panel6 = New Panel()
        PanelWithSearch = New Panel()
        Label1 = New Label()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        PanelWithDgv = New Panel()
        PanelBackground = New Panel()
        Panel1 = New Panel()
        Panel3.SuspendLayout()
        CType(dgvTrackCases, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        PanelBackground.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(dgvTrackCases)
        Panel3.Location = New Point(4, 354)
        Panel3.Margin = New Padding(4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1337, 970)
        Panel3.TabIndex = 30
        ' 
        ' dgvTrackCases
        ' 
        dgvTrackCases.BackgroundColor = Color.White
        dgvTrackCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTrackCases.Location = New Point(4, 0)
        dgvTrackCases.Margin = New Padding(4)
        dgvTrackCases.Name = "dgvTrackCases"
        dgvTrackCases.RowHeadersWidth = 51
        dgvTrackCases.Size = New Size(1329, 943)
        dgvTrackCases.TabIndex = 0
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Label6.Location = New Point(42, 9)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(311, 51)
        Label6.TabIndex = 68
        Label6.Text = "Case Tracker"
        ' 
        ' btnPrint
        ' 
        btnPrint.BackColor = Color.DarkRed
        btnPrint.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrint.ForeColor = Color.LavenderBlush
        btnPrint.Location = New Point(1044, 13)
        btnPrint.Margin = New Padding(4, 5, 4, 5)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(225, 59)
        btnPrint.TabIndex = 73
        btnPrint.Text = "Print"
        btnPrint.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(6, 231)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(208, 33)
        Label2.TabIndex = 81
        Label2.Text = "Filter by Victim"
        ' 
        ' cmbFilterByVictim
        ' 
        cmbFilterByVictim.BackColor = SystemColors.ButtonFace
        cmbFilterByVictim.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFilterByVictim.ForeColor = Color.Black
        cmbFilterByVictim.FormattingEnabled = True
        cmbFilterByVictim.Items.AddRange(New Object() {"South Campus", "North Campus", "Central Campus", "Ajumako Campus"})
        cmbFilterByVictim.Location = New Point(7, 278)
        cmbFilterByVictim.Margin = New Padding(4, 5, 4, 5)
        cmbFilterByVictim.Name = "cmbFilterByVictim"
        cmbFilterByVictim.Size = New Size(375, 44)
        cmbFilterByVictim.TabIndex = 80
        cmbFilterByVictim.Text = "--Select--"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.BackColor = Color.Transparent
        Label12.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.Black
        Label12.Location = New Point(13, 103)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(201, 33)
        Label12.TabIndex = 79
        Label12.Text = "Filter by Status"
        ' 
        ' cmbFilterStatus
        ' 
        cmbFilterStatus.BackColor = SystemColors.ButtonFace
        cmbFilterStatus.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFilterStatus.ForeColor = Color.Black
        cmbFilterStatus.FormattingEnabled = True
        cmbFilterStatus.Items.AddRange(New Object() {"South Campus", "North Campus", "Central Campus", "Ajumako Campus"})
        cmbFilterStatus.Location = New Point(14, 150)
        cmbFilterStatus.Margin = New Padding(4, 5, 4, 5)
        cmbFilterStatus.Name = "cmbFilterStatus"
        cmbFilterStatus.Size = New Size(375, 44)
        cmbFilterStatus.TabIndex = 78
        cmbFilterStatus.Text = "--Select--"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(btnPrint)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1337, 84)
        Panel2.TabIndex = 75
        ' 
        ' lblTotalCasesFiled
        ' 
        lblTotalCasesFiled.AutoSize = True
        lblTotalCasesFiled.BackColor = Color.Transparent
        lblTotalCasesFiled.Font = New Font("Garamond", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalCasesFiled.ForeColor = Color.Black
        lblTotalCasesFiled.Location = New Point(60, 10)
        lblTotalCasesFiled.Margin = New Padding(4, 0, 4, 0)
        lblTotalCasesFiled.Name = "lblTotalCasesFiled"
        lblTotalCasesFiled.Size = New Size(90, 108)
        lblTotalCasesFiled.TabIndex = 80
        lblTotalCasesFiled.Text = "0"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(556, 133)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(231, 33)
        Label3.TabIndex = 83
        Label3.Text = "Total Cases Filed"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.WhiteSmoke
        Panel4.Controls.Add(lblTotalCasesFiled)
        Panel4.Location = New Point(556, 187)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(219, 135)
        Panel4.TabIndex = 82
        ' 
        ' lblTotalCasesCompleted
        ' 
        lblTotalCasesCompleted.AutoSize = True
        lblTotalCasesCompleted.BackColor = Color.Transparent
        lblTotalCasesCompleted.Font = New Font("Garamond", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalCasesCompleted.ForeColor = Color.Black
        lblTotalCasesCompleted.Location = New Point(60, 10)
        lblTotalCasesCompleted.Margin = New Padding(4, 0, 4, 0)
        lblTotalCasesCompleted.Name = "lblTotalCasesCompleted"
        lblTotalCasesCompleted.Size = New Size(90, 108)
        lblTotalCasesCompleted.TabIndex = 80
        lblTotalCasesCompleted.Text = "0"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(826, 133)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(201, 33)
        Label5.TabIndex = 84
        Label5.Text = "Total Compled"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.WhiteSmoke
        Panel5.Controls.Add(lblTotalCasesCompleted)
        Panel5.Location = New Point(791, 187)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(219, 135)
        Panel5.TabIndex = 83
        ' 
        ' lblTotalPercentage
        ' 
        lblTotalPercentage.AutoSize = True
        lblTotalPercentage.BackColor = Color.Transparent
        lblTotalPercentage.Font = New Font("Garamond", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalPercentage.ForeColor = Color.Black
        lblTotalPercentage.Location = New Point(4, 10)
        lblTotalPercentage.Margin = New Padding(4, 0, 4, 0)
        lblTotalPercentage.Name = "lblTotalPercentage"
        lblTotalPercentage.Size = New Size(90, 108)
        lblTotalPercentage.TabIndex = 80
        lblTotalPercentage.Text = "0"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Transparent
        Label8.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Black
        Label8.Location = New Point(1105, 133)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(152, 33)
        Label8.TabIndex = 86
        Label8.Text = "percentage"
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.WhiteSmoke
        Panel6.Controls.Add(lblTotalPercentage)
        Panel6.Location = New Point(1032, 187)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(283, 135)
        Panel6.TabIndex = 85
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(Label8)
        PanelWithSearch.Controls.Add(Panel6)
        PanelWithSearch.Controls.Add(Label5)
        PanelWithSearch.Controls.Add(Panel5)
        PanelWithSearch.Controls.Add(Label3)
        PanelWithSearch.Controls.Add(Panel4)
        PanelWithSearch.Controls.Add(Label2)
        PanelWithSearch.Controls.Add(cmbFilterByVictim)
        PanelWithSearch.Controls.Add(Label12)
        PanelWithSearch.Controls.Add(cmbFilterStatus)
        PanelWithSearch.Controls.Add(Panel2)
        PanelWithSearch.Controls.Add(Label1)
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(1337, 340)
        PanelWithSearch.TabIndex = 28
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(389, 46)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 38)
        Label1.TabIndex = 62
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(PanelWithSearch)
        FlowLayoutPanel1.Controls.Add(Panel3)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1345, 778)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithDgv
        ' 
        PanelWithDgv.AutoScroll = True
        PanelWithDgv.BackColor = Color.White
        PanelWithDgv.Controls.Add(FlowLayoutPanel1)
        PanelWithDgv.Dock = DockStyle.Fill
        PanelWithDgv.Location = New Point(0, 0)
        PanelWithDgv.Margin = New Padding(4, 5, 4, 5)
        PanelWithDgv.Name = "PanelWithDgv"
        PanelWithDgv.Size = New Size(1345, 778)
        PanelWithDgv.TabIndex = 4
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(1345, 778)
        PanelBackground.TabIndex = 3
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(PanelBackground)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1345, 778)
        Panel1.TabIndex = 2
        ' 
        ' UC_Statistics
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_Statistics"
        Size = New Size(1345, 778)
        Panel3.ResumeLayout(False)
        CType(dgvTrackCases, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        PanelBackground.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel3 As Panel
    Friend WithEvents dgvTrackCases As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbFilterByVictim As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbFilterStatus As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblTotalCasesFiled As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalCasesCompleted As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lblTotalPercentage As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents PanelBackground As Panel
    Friend WithEvents Panel1 As Panel

End Class
