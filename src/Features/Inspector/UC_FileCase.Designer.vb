<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_FileCase
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
        Label2 = New Label()
        PanelRedDesign = New Panel()
        PictureBox1 = New PictureBox()
        Panel4 = New Panel()
        btnUploadCasePhoto = New Button()
        pbxEvidenceImage = New PictureBox()
        Panel2 = New Panel()
        Panel5 = New Panel()
        txtCaseDescription = New TextBox()
        Label4 = New Label()
        btnCreateCase = New Button()
        Label3 = New Label()
        PanelInputBundle = New Panel()
        dtpCaseDate = New DateTimePicker()
        Label8 = New Label()
        txtSuspectName = New TextBox()
        Label6 = New Label()
        txtVictimName = New TextBox()
        Label1 = New Label()
        Label7 = New Label()
        cmbZone = New ComboBox()
        Label5 = New Label()
        cmbCrimeCategory = New ComboBox()
        PictureBox2 = New PictureBox()
        txtTitle = New TextBox()
        Panel1 = New Panel()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel4.SuspendLayout()
        CType(pbxEvidenceImage, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        PanelInputBundle.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.DarkRed
        Label2.Location = New Point(4, 11)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(380, 61)
        Label2.TabIndex = 23
        Label2.Text = "File a Crime Case"
        ' 
        ' PanelRedDesign
        ' 
        PanelRedDesign.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        PanelRedDesign.Location = New Point(225, 104)
        PanelRedDesign.Margin = New Padding(4, 5, 4, 5)
        PanelRedDesign.Name = "PanelRedDesign"
        PanelRedDesign.Size = New Size(93, 2037)
        PanelRedDesign.TabIndex = 5
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.White
        PictureBox1.Image = My.Resources.Resources._20260731_220030
        PictureBox1.Location = New Point(0, 0)
        PictureBox1.Margin = New Padding(4, 5, 4, 5)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(252, 266)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 65
        PictureBox1.TabStop = False
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(btnUploadCasePhoto)
        Panel4.Controls.Add(pbxEvidenceImage)
        Panel4.Location = New Point(411, 80)
        Panel4.Margin = New Padding(4, 5, 4, 5)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(328, 312)
        Panel4.TabIndex = 63
        ' 
        ' btnUploadCasePhoto
        ' 
        btnUploadCasePhoto.BackColor = Color.FromArgb(CByte(192), CByte(0), CByte(0))
        btnUploadCasePhoto.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnUploadCasePhoto.ForeColor = Color.White
        btnUploadCasePhoto.Location = New Point(18, 259)
        btnUploadCasePhoto.Margin = New Padding(4, 5, 4, 5)
        btnUploadCasePhoto.Name = "btnUploadCasePhoto"
        btnUploadCasePhoto.Size = New Size(290, 49)
        btnUploadCasePhoto.TabIndex = 35
        btnUploadCasePhoto.Text = "Upload Image Evidence "
        btnUploadCasePhoto.UseVisualStyleBackColor = False
        ' 
        ' pbxEvidenceImage
        ' 
        pbxEvidenceImage.BackColor = Color.White
        pbxEvidenceImage.Location = New Point(4, 6)
        pbxEvidenceImage.Margin = New Padding(4, 5, 4, 5)
        pbxEvidenceImage.Name = "pbxEvidenceImage"
        pbxEvidenceImage.Size = New Size(320, 245)
        pbxEvidenceImage.SizeMode = PictureBoxSizeMode.StretchImage
        pbxEvidenceImage.TabIndex = 0
        pbxEvidenceImage.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.WhiteSmoke
        Panel2.Controls.Add(Label2)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1389, 89)
        Panel2.TabIndex = 0
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel5.Location = New Point(-8, 160)
        Panel5.Margin = New Padding(4, 5, 4, 5)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(1069, 40)
        Panel5.TabIndex = 64
        ' 
        ' txtCaseDescription
        ' 
        txtCaseDescription.BackColor = SystemColors.HighlightText
        txtCaseDescription.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtCaseDescription.ForeColor = SystemColors.ActiveCaptionText
        txtCaseDescription.Location = New Point(49, 990)
        txtCaseDescription.Margin = New Padding(4, 5, 4, 5)
        txtCaseDescription.Multiline = True
        txtCaseDescription.Name = "txtCaseDescription"
        txtCaseDescription.Size = New Size(978, 176)
        txtCaseDescription.TabIndex = 31
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(49, 935)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(227, 33)
        Label4.TabIndex = 30
        Label4.Text = "Case Description"
        ' 
        ' btnCreateCase
        ' 
        btnCreateCase.BackColor = Color.DarkSlateGray
        btnCreateCase.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCreateCase.ForeColor = Color.LavenderBlush
        btnCreateCase.Location = New Point(45, 1735)
        btnCreateCase.Margin = New Padding(4, 5, 4, 5)
        btnCreateCase.Name = "btnCreateCase"
        btnCreateCase.Size = New Size(341, 68)
        btnCreateCase.TabIndex = 29
        btnCreateCase.Text = "Create a Crime Case"
        btnCreateCase.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(49, 446)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(74, 33)
        Label3.TabIndex = 14
        Label3.Text = "Title"
        ' 
        ' PanelInputBundle
        ' 
        PanelInputBundle.AutoScroll = True
        PanelInputBundle.BackColor = Color.White
        PanelInputBundle.Controls.Add(dtpCaseDate)
        PanelInputBundle.Controls.Add(Label8)
        PanelInputBundle.Controls.Add(txtSuspectName)
        PanelInputBundle.Controls.Add(Label6)
        PanelInputBundle.Controls.Add(txtVictimName)
        PanelInputBundle.Controls.Add(Label1)
        PanelInputBundle.Controls.Add(Label7)
        PanelInputBundle.Controls.Add(cmbZone)
        PanelInputBundle.Controls.Add(Label5)
        PanelInputBundle.Controls.Add(cmbCrimeCategory)
        PanelInputBundle.Controls.Add(PictureBox2)
        PanelInputBundle.Controls.Add(PictureBox1)
        PanelInputBundle.Controls.Add(Panel4)
        PanelInputBundle.Controls.Add(Panel5)
        PanelInputBundle.Controls.Add(txtCaseDescription)
        PanelInputBundle.Controls.Add(Label4)
        PanelInputBundle.Controls.Add(btnCreateCase)
        PanelInputBundle.Controls.Add(txtTitle)
        PanelInputBundle.Controls.Add(Label3)
        PanelInputBundle.Location = New Point(233, 112)
        PanelInputBundle.Margin = New Padding(4, 5, 4, 5)
        PanelInputBundle.Name = "PanelInputBundle"
        PanelInputBundle.Size = New Size(1090, 1916)
        PanelInputBundle.TabIndex = 4
        ' 
        ' dtpCaseDate
        ' 
        dtpCaseDate.Location = New Point(49, 1317)
        dtpCaseDate.Name = "dtpCaseDate"
        dtpCaseDate.Size = New Size(966, 31)
        dtpCaseDate.TabIndex = 78
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Transparent
        Label8.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.Black
        Label8.Location = New Point(45, 1250)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(273, 33)
        Label8.TabIndex = 77
        Label8.Text = "When Did it Happen"
        ' 
        ' txtSuspectName
        ' 
        txtSuspectName.BackColor = SystemColors.InactiveBorder
        txtSuspectName.Font = New Font("Garamond", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSuspectName.ForeColor = SystemColors.ActiveCaptionText
        txtSuspectName.Location = New Point(45, 1593)
        txtSuspectName.Margin = New Padding(4, 5, 4, 5)
        txtSuspectName.Name = "txtSuspectName"
        txtSuspectName.Size = New Size(976, 48)
        txtSuspectName.TabIndex = 76
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.Black
        Label6.Location = New Point(45, 1538)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(194, 33)
        Label6.TabIndex = 75
        Label6.Text = "Suspect Name"
        ' 
        ' txtVictimName
        ' 
        txtVictimName.BackColor = Color.GhostWhite
        txtVictimName.Font = New Font("Garamond", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtVictimName.ForeColor = SystemColors.ActiveCaptionText
        txtVictimName.Location = New Point(45, 1460)
        txtVictimName.Margin = New Padding(4, 5, 4, 5)
        txtVictimName.Name = "txtVictimName"
        txtVictimName.Size = New Size(976, 48)
        txtVictimName.TabIndex = 74
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(45, 1405)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(187, 33)
        Label1.TabIndex = 73
        Label1.Text = "Victim  Name"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.BackColor = Color.Transparent
        Label7.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.Black
        Label7.Location = New Point(45, 797)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(264, 33)
        Label7.TabIndex = 72
        Label7.Text = "* Select Crime Zone"
        ' 
        ' cmbZone
        ' 
        cmbZone.BackColor = Color.LightGray
        cmbZone.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbZone.ForeColor = Color.Red
        cmbZone.FormattingEnabled = True
        cmbZone.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbZone.Location = New Point(45, 838)
        cmbZone.Margin = New Padding(4, 5, 4, 5)
        cmbZone.Name = "cmbZone"
        cmbZone.Size = New Size(953, 44)
        cmbZone.TabIndex = 71
        cmbZone.Text = "--Select a Zone--"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(49, 617)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(418, 33)
        Label5.TabIndex = 70
        Label5.Text = "*Select a related Crime Category"
        ' 
        ' cmbCrimeCategory
        ' 
        cmbCrimeCategory.BackColor = Color.LightGray
        cmbCrimeCategory.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbCrimeCategory.ForeColor = Color.Red
        cmbCrimeCategory.FormattingEnabled = True
        cmbCrimeCategory.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbCrimeCategory.Location = New Point(45, 671)
        cmbCrimeCategory.Margin = New Padding(4, 5, 4, 5)
        cmbCrimeCategory.Name = "cmbCrimeCategory"
        cmbCrimeCategory.Size = New Size(953, 44)
        cmbCrimeCategory.TabIndex = 69
        cmbCrimeCategory.Text = "--Select Category--"
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackColor = Color.White
        PictureBox2.Image = My.Resources.Resources.Ghana_Coat_Arms
        PictureBox2.Location = New Point(838, 1)
        PictureBox2.Margin = New Padding(4, 5, 4, 5)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(252, 266)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 68
        PictureBox2.TabStop = False
        ' 
        ' txtTitle
        ' 
        txtTitle.BackColor = SystemColors.HighlightText
        txtTitle.Font = New Font("Garamond", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtTitle.ForeColor = SystemColors.ActiveCaptionText
        txtTitle.Location = New Point(49, 501)
        txtTitle.Margin = New Padding(4, 5, 4, 5)
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(939, 48)
        txtTitle.TabIndex = 15
        ' 
        ' Panel1
        ' 
        Panel1.AutoScroll = True
        Panel1.Controls.Add(PanelInputBundle)
        Panel1.Controls.Add(PanelRedDesign)
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1415, 756)
        Panel1.TabIndex = 2
        ' 
        ' UC_FileCase
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_FileCase"
        Size = New Size(1415, 756)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel4.ResumeLayout(False)
        CType(pbxEvidenceImage, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        PanelInputBundle.ResumeLayout(False)
        PanelInputBundle.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents PanelRedDesign As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents btnUploadCasePhoto As Button
    Friend WithEvents pbxEvidenceImage As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents txtCaseDescription As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCreateCase As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents PanelInputBundle As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbZone As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbCrimeCategory As ComboBox
    Friend WithEvents txtVictimName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtSuspectName As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents dtpCaseDate As DateTimePicker
    Friend WithEvents Label8 As Label

End Class
