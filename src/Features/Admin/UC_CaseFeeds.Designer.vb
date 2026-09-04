<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_CaseFeeds
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Panel6 = New Panel()
        dgvCrimeCases = New DataGridView()
        Panel10 = New Panel()
        Label12 = New Label()
        cmbFilterStatus = New ComboBox()
        Label10 = New Label()
        cmbFilterCrimeCategory = New ComboBox()
        lblReportedByInspector = New Label()
        Label9 = New Label()
        Label8 = New Label()
        cmbFilterZone = New ComboBox()
        Label6 = New Label()
        PanelInputBundle = New Panel()
        uiii = New Label()
        Panel7 = New Panel()
        lblDate = New Label()
        txtSuspectName = New TextBox()
        Label11 = New Label()
        txtVictimName = New TextBox()
        Label13 = New Label()
        txtCaseDescription = New TextBox()
        Label5 = New Label()
        Label1 = New Label()
        cmbZone = New ComboBox()
        cmbCrimeCategory = New ComboBox()
        txtTitle = New TextBox()
        Panel4 = New Panel()
        btnUploadPhoto = New Button()
        pbxImageEvidence = New PictureBox()
        Panel5 = New Panel()
        Label4 = New Label()
        Label3 = New Label()
        Panel9 = New Panel()
        Panel3 = New Panel()
        btnExport = New Button()
        Label2 = New Label()
        Label7 = New Label()
        cmbAssignOfficer = New ComboBox()
        btnAssignOfficerGroup = New Button()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel6.SuspendLayout()
        CType(dgvCrimeCases, ComponentModel.ISupportInitialize).BeginInit()
        PanelInputBundle.SuspendLayout()
        Panel7.SuspendLayout()
        Panel4.SuspendLayout()
        CType(pbxImageEvidence, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1753, 755)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.AutoScroll = True
        Panel2.BackColor = SystemColors.Menu
        Panel2.Controls.Add(Panel6)
        Panel2.Controls.Add(PanelInputBundle)
        Panel2.Controls.Add(Panel3)
        Panel2.Controls.Add(Label7)
        Panel2.Controls.Add(cmbAssignOfficer)
        Panel2.Controls.Add(btnAssignOfficerGroup)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1753, 755)
        Panel2.TabIndex = 3
        ' 
        ' Panel6
        ' 
        Panel6.AutoScroll = True
        Panel6.BackColor = Color.WhiteSmoke
        Panel6.Controls.Add(dgvCrimeCases)
        Panel6.Controls.Add(Panel10)
        Panel6.Controls.Add(Label12)
        Panel6.Controls.Add(cmbFilterStatus)
        Panel6.Controls.Add(Label10)
        Panel6.Controls.Add(cmbFilterCrimeCategory)
        Panel6.Controls.Add(lblReportedByInspector)
        Panel6.Controls.Add(Label9)
        Panel6.Controls.Add(Label8)
        Panel6.Controls.Add(cmbFilterZone)
        Panel6.Controls.Add(Label6)
        Panel6.Location = New Point(4, 98)
        Panel6.Margin = New Padding(4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(674, 697)
        Panel6.TabIndex = 7
        ' 
        ' dgvCrimeCases
        ' 
        dgvCrimeCases.BackgroundColor = Color.WhiteSmoke
        dgvCrimeCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCrimeCases.Location = New Point(8, 498)
        dgvCrimeCases.Name = "dgvCrimeCases"
        dgvCrimeCases.RowHeadersWidth = 62
        dgvCrimeCases.Size = New Size(626, 610)
        dgvCrimeCases.TabIndex = 66
        ' 
        ' Panel10
        ' 
        Panel10.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel10.Location = New Point(-1, 405)
        Panel10.Margin = New Padding(4, 5, 4, 5)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(648, 10)
        Panel10.TabIndex = 65
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.BackColor = Color.Transparent
        Label12.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.Black
        Label12.Location = New Point(13, 56)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(201, 33)
        Label12.TabIndex = 45
        Label12.Text = "Filter by Status"
        ' 
        ' cmbFilterStatus
        ' 
        cmbFilterStatus.BackColor = SystemColors.ButtonFace
        cmbFilterStatus.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFilterStatus.ForeColor = Color.Black
        cmbFilterStatus.FormattingEnabled = True
        cmbFilterStatus.Items.AddRange(New Object() {"South Campus", "North Campus", "Central Campus", "Ajumako Campus"})
        cmbFilterStatus.Location = New Point(222, 56)
        cmbFilterStatus.Margin = New Padding(4, 5, 4, 5)
        cmbFilterStatus.Name = "cmbFilterStatus"
        cmbFilterStatus.Size = New Size(375, 44)
        cmbFilterStatus.TabIndex = 44
        cmbFilterStatus.Text = "--Select--"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.Transparent
        Label10.Font = New Font("Garamond", 11.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Black
        Label10.Location = New Point(13, 137)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(217, 25)
        Label10.TabIndex = 43
        Label10.Text = "Filter Crime Category"
        ' 
        ' cmbFilterCrimeCategory
        ' 
        cmbFilterCrimeCategory.BackColor = SystemColors.ButtonFace
        cmbFilterCrimeCategory.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFilterCrimeCategory.ForeColor = Color.Black
        cmbFilterCrimeCategory.FormattingEnabled = True
        cmbFilterCrimeCategory.Items.AddRange(New Object() {"South Campus", "North Campus", "Central Campus", "Ajumako Campus"})
        cmbFilterCrimeCategory.Location = New Point(222, 132)
        cmbFilterCrimeCategory.Margin = New Padding(4, 5, 4, 5)
        cmbFilterCrimeCategory.Name = "cmbFilterCrimeCategory"
        cmbFilterCrimeCategory.Size = New Size(375, 44)
        cmbFilterCrimeCategory.TabIndex = 42
        cmbFilterCrimeCategory.Text = "--Select--"
        ' 
        ' lblReportedByInspector
        ' 
        lblReportedByInspector.AutoSize = True
        lblReportedByInspector.BackColor = Color.Transparent
        lblReportedByInspector.Font = New Font("Garamond", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblReportedByInspector.ForeColor = Color.Black
        lblReportedByInspector.Location = New Point(205, 302)
        lblReportedByInspector.Margin = New Padding(4, 0, 4, 0)
        lblReportedByInspector.Name = "lblReportedByInspector"
        lblReportedByInspector.Size = New Size(56, 27)
        lblReportedByInspector.TabIndex = 41
        lblReportedByInspector.Text = "Sam"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Transparent
        Label9.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.Black
        Label9.Location = New Point(8, 302)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(189, 33)
        Label9.TabIndex = 39
        Label9.Text = "Recorderd by:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Transparent
        Label8.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Black
        Label8.Location = New Point(8, 211)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(165, 33)
        Label8.TabIndex = 38
        Label8.Text = "Filter Zones"
        ' 
        ' cmbFilterZone
        ' 
        cmbFilterZone.BackColor = SystemColors.ButtonFace
        cmbFilterZone.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbFilterZone.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        cmbFilterZone.FormattingEnabled = True
        cmbFilterZone.Location = New Point(222, 209)
        cmbFilterZone.Margin = New Padding(4, 5, 4, 5)
        cmbFilterZone.Name = "cmbFilterZone"
        cmbFilterZone.Size = New Size(388, 44)
        cmbFilterZone.TabIndex = 37
        cmbFilterZone.Text = "--Select--"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Label6.Location = New Point(230, 431)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(170, 33)
        Label6.TabIndex = 28
        Label6.Text = "Crime Cases"
        ' 
        ' PanelInputBundle
        ' 
        PanelInputBundle.AutoScroll = True
        PanelInputBundle.BackColor = Color.White
        PanelInputBundle.Controls.Add(uiii)
        PanelInputBundle.Controls.Add(Panel7)
        PanelInputBundle.Controls.Add(txtSuspectName)
        PanelInputBundle.Controls.Add(Label11)
        PanelInputBundle.Controls.Add(txtVictimName)
        PanelInputBundle.Controls.Add(Label13)
        PanelInputBundle.Controls.Add(txtCaseDescription)
        PanelInputBundle.Controls.Add(Label5)
        PanelInputBundle.Controls.Add(Label1)
        PanelInputBundle.Controls.Add(cmbZone)
        PanelInputBundle.Controls.Add(cmbCrimeCategory)
        PanelInputBundle.Controls.Add(txtTitle)
        PanelInputBundle.Controls.Add(Panel4)
        PanelInputBundle.Controls.Add(Panel5)
        PanelInputBundle.Controls.Add(Label4)
        PanelInputBundle.Controls.Add(Label3)
        PanelInputBundle.Controls.Add(Panel9)
        PanelInputBundle.Location = New Point(668, 274)
        PanelInputBundle.Margin = New Padding(4, 5, 4, 5)
        PanelInputBundle.Name = "PanelInputBundle"
        PanelInputBundle.Size = New Size(1055, 1735)
        PanelInputBundle.TabIndex = 4
        ' 
        ' uiii
        ' 
        uiii.AutoSize = True
        uiii.BackColor = Color.Transparent
        uiii.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        uiii.ForeColor = Color.Black
        uiii.Location = New Point(423, 1213)
        uiii.Margin = New Padding(4, 0, 4, 0)
        uiii.Name = "uiii"
        uiii.Size = New Size(237, 33)
        uiii.TabIndex = 82
        uiii.Text = "Date it Happened"
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.White
        Panel7.Controls.Add(lblDate)
        Panel7.Location = New Point(45, 1295)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(967, 53)
        Panel7.TabIndex = 81
        ' 
        ' lblDate
        ' 
        lblDate.AutoSize = True
        lblDate.BackColor = Color.Transparent
        lblDate.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblDate.ForeColor = Color.Black
        lblDate.Location = New Point(370, 12)
        lblDate.Margin = New Padding(4, 0, 4, 0)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(23, 33)
        lblDate.TabIndex = 69
        lblDate.Text = "!"
        ' 
        ' txtSuspectName
        ' 
        txtSuspectName.BackColor = SystemColors.InactiveBorder
        txtSuspectName.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSuspectName.ForeColor = SystemColors.ActiveCaptionText
        txtSuspectName.Location = New Point(45, 1637)
        txtSuspectName.Margin = New Padding(4, 5, 4, 5)
        txtSuspectName.Name = "txtSuspectName"
        txtSuspectName.Size = New Size(976, 48)
        txtSuspectName.TabIndex = 80
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.BackColor = Color.Transparent
        Label11.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.ForeColor = Color.Black
        Label11.Location = New Point(455, 1586)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(194, 33)
        Label11.TabIndex = 79
        Label11.Text = "Suspect Name"
        ' 
        ' txtVictimName
        ' 
        txtVictimName.BackColor = Color.GhostWhite
        txtVictimName.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtVictimName.ForeColor = SystemColors.ActiveCaptionText
        txtVictimName.Location = New Point(45, 1454)
        txtVictimName.Margin = New Padding(4, 5, 4, 5)
        txtVictimName.Name = "txtVictimName"
        txtVictimName.Size = New Size(976, 48)
        txtVictimName.TabIndex = 78
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.BackColor = Color.Transparent
        Label13.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.Black
        Label13.Location = New Point(462, 1404)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(187, 33)
        Label13.TabIndex = 77
        Label13.Text = "Victim  Name"
        ' 
        ' txtCaseDescription
        ' 
        txtCaseDescription.BackColor = SystemColors.HighlightText
        txtCaseDescription.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtCaseDescription.ForeColor = SystemColors.ActiveCaptionText
        txtCaseDescription.Location = New Point(31, 904)
        txtCaseDescription.Margin = New Padding(4, 5, 4, 5)
        txtCaseDescription.Multiline = True
        txtCaseDescription.Name = "txtCaseDescription"
        txtCaseDescription.Size = New Size(978, 252)
        txtCaseDescription.TabIndex = 76
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(432, 856)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(227, 33)
        Label5.TabIndex = 75
        Label5.Text = "Case Description"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(487, 706)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(162, 33)
        Label1.TabIndex = 74
        Label1.Text = "Crime Zone"
        ' 
        ' cmbZone
        ' 
        cmbZone.BackColor = Color.LightGray
        cmbZone.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbZone.ForeColor = Color.Red
        cmbZone.FormattingEnabled = True
        cmbZone.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbZone.Location = New Point(45, 744)
        cmbZone.Margin = New Padding(4, 5, 4, 5)
        cmbZone.Name = "cmbZone"
        cmbZone.Size = New Size(953, 44)
        cmbZone.TabIndex = 73
        ' 
        ' cmbCrimeCategory
        ' 
        cmbCrimeCategory.BackColor = Color.LightGray
        cmbCrimeCategory.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbCrimeCategory.ForeColor = Color.Red
        cmbCrimeCategory.FormattingEnabled = True
        cmbCrimeCategory.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbCrimeCategory.Location = New Point(45, 617)
        cmbCrimeCategory.Margin = New Padding(4, 5, 4, 5)
        cmbCrimeCategory.Name = "cmbCrimeCategory"
        cmbCrimeCategory.Size = New Size(953, 44)
        cmbCrimeCategory.TabIndex = 72
        ' 
        ' txtTitle
        ' 
        txtTitle.BackColor = SystemColors.HighlightText
        txtTitle.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtTitle.ForeColor = SystemColors.ActiveCaptionText
        txtTitle.Location = New Point(45, 473)
        txtTitle.Margin = New Padding(4, 5, 4, 5)
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(939, 48)
        txtTitle.TabIndex = 71
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(btnUploadPhoto)
        Panel4.Controls.Add(pbxImageEvidence)
        Panel4.Location = New Point(328, 22)
        Panel4.Margin = New Padding(4, 5, 4, 5)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(382, 375)
        Panel4.TabIndex = 63
        ' 
        ' btnUploadPhoto
        ' 
        btnUploadPhoto.BackColor = Color.FromArgb(CByte(192), CByte(0), CByte(0))
        btnUploadPhoto.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnUploadPhoto.ForeColor = Color.White
        btnUploadPhoto.Location = New Point(41, 300)
        btnUploadPhoto.Margin = New Padding(4, 5, 4, 5)
        btnUploadPhoto.Name = "btnUploadPhoto"
        btnUploadPhoto.Size = New Size(290, 49)
        btnUploadPhoto.TabIndex = 35
        btnUploadPhoto.Text = "Image Evidence "
        btnUploadPhoto.UseVisualStyleBackColor = False
        ' 
        ' pbxImageEvidence
        ' 
        pbxImageEvidence.BackColor = Color.White
        pbxImageEvidence.Location = New Point(20, 16)
        pbxImageEvidence.Margin = New Padding(4, 5, 4, 5)
        pbxImageEvidence.Name = "pbxImageEvidence"
        pbxImageEvidence.Size = New Size(325, 274)
        pbxImageEvidence.SizeMode = PictureBoxSizeMode.StretchImage
        pbxImageEvidence.TabIndex = 0
        pbxImageEvidence.TabStop = False
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.Black
        Panel5.Location = New Point(302, 56)
        Panel5.Margin = New Padding(4, 5, 4, 5)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(321, 232)
        Panel5.TabIndex = 64
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(449, 565)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(210, 33)
        Label4.TabIndex = 30
        Label4.Text = "Crime Category"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(521, 429)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(74, 33)
        Label3.TabIndex = 14
        Label3.Text = "Title"
        ' 
        ' Panel9
        ' 
        Panel9.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel9.Location = New Point(690, 429)
        Panel9.Margin = New Padding(4, 5, 4, 5)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(33, 1017)
        Panel9.TabIndex = 70
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel3.Controls.Add(btnExport)
        Panel3.Controls.Add(Label2)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Margin = New Padding(4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1727, 89)
        Panel3.TabIndex = 0
        ' 
        ' btnExport
        ' 
        btnExport.BackColor = Color.White
        btnExport.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExport.ForeColor = Color.Black
        btnExport.Location = New Point(1418, 18)
        btnExport.Margin = New Padding(4, 5, 4, 5)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(234, 47)
        btnExport.TabIndex = 46
        btnExport.Text = "Export"
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(17, 18)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(423, 61)
        Label2.TabIndex = 23
        Label2.Text = "Crime Portal Feeds"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.BackColor = Color.Transparent
        Label7.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.Black
        Label7.Location = New Point(759, 113)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(309, 33)
        Label7.TabIndex = 30
        Label7.Text = "Assingn Patroll Officers"
        ' 
        ' cmbAssignOfficer
        ' 
        cmbAssignOfficer.BackColor = SystemColors.Info
        cmbAssignOfficer.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbAssignOfficer.ForeColor = Color.Red
        cmbAssignOfficer.FormattingEnabled = True
        cmbAssignOfficer.Items.AddRange(New Object() {"South Patrol Team", "North Patrol Team", "Central Patrol Team", "Ajumako Patrol Team"})
        cmbAssignOfficer.Location = New Point(1076, 107)
        cmbAssignOfficer.Margin = New Padding(4, 5, 4, 5)
        cmbAssignOfficer.Name = "cmbAssignOfficer"
        cmbAssignOfficer.Size = New Size(440, 44)
        cmbAssignOfficer.TabIndex = 29
        cmbAssignOfficer.Text = "--Select--"
        ' 
        ' btnAssignOfficerGroup
        ' 
        btnAssignOfficerGroup.BackColor = Color.FromArgb(CByte(192), CByte(0), CByte(0))
        btnAssignOfficerGroup.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnAssignOfficerGroup.ForeColor = Color.White
        btnAssignOfficerGroup.Location = New Point(1079, 188)
        btnAssignOfficerGroup.Margin = New Padding(4, 5, 4, 5)
        btnAssignOfficerGroup.Name = "btnAssignOfficerGroup"
        btnAssignOfficerGroup.Size = New Size(282, 61)
        btnAssignOfficerGroup.TabIndex = 36
        btnAssignOfficerGroup.Text = "Assign Officer Patrol"
        btnAssignOfficerGroup.UseVisualStyleBackColor = False
        ' 
        ' UC_CaseFeeds
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_CaseFeeds"
        Size = New Size(1753, 755)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        CType(dgvCrimeCases, ComponentModel.ISupportInitialize).EndInit()
        PanelInputBundle.ResumeLayout(False)
        PanelInputBundle.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel4.ResumeLayout(False)
        CType(pbxImageEvidence, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbFilterStatus As ComboBox
    Friend WithEvents cmbFilterCrimeCategory As ComboBox
    Friend WithEvents lblReportedByInspector As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbFilterZone As ComboBox
    Friend WithEvents btnAssignOfficerGroup As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbAssignOfficer As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents PanelInputBundle As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents btnUploadPhoto As Button
    Friend WithEvents pbxImageEvidence As PictureBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnExport As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents cmbCrimeCategory As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbZone As ComboBox
    Friend WithEvents txtCaseDescription As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSuspectName As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtVictimName As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents uiii As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents lblDate As Label
    Friend WithEvents dgvCrimeCases As DataGridView

End Class
