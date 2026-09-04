<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDashboard))
        Panel2 = New Panel()
        pbxNotify = New PictureBox()
        lblNotify = New Label()
        LogoutToolStripMenuItem = New ToolStripMenuItem()
        btnCaseFeeds = New Button()
        MenuStrip1 = New MenuStrip()
        AllUserAccountToolStripMenuItem = New ToolStripMenuItem()
        STATISTICSToolStripMenuItem = New ToolStripMenuItem()
        Panel1 = New Panel()
        panelLeft = New Panel()
        btnNoUse = New Button()
        btnCreateAccount = New Button()
        PictureBox1 = New PictureBox()
        btnCreateCrimeCategory = New Button()
        btnCreateZones = New Button()
        Panel4 = New Panel()
        Panel5 = New Panel()
        PanelWithUC = New Panel()
        Panel2.SuspendLayout()
        CType(pbxNotify, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        Panel1.SuspendLayout()
        panelLeft.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(pbxNotify)
        Panel2.Controls.Add(lblNotify)
        Panel2.Location = New Point(48, 260)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(281, 174)
        Panel2.TabIndex = 11
        ' 
        ' pbxNotify
        ' 
        pbxNotify.Image = My.Resources.Resources.notify
        pbxNotify.Location = New Point(134, 48)
        pbxNotify.Margin = New Padding(4)
        pbxNotify.Name = "pbxNotify"
        pbxNotify.Size = New Size(88, 78)
        pbxNotify.SizeMode = PictureBoxSizeMode.StretchImage
        pbxNotify.TabIndex = 27
        pbxNotify.TabStop = False
        ' 
        ' lblNotify
        ' 
        lblNotify.AutoSize = True
        lblNotify.BackColor = Color.Transparent
        lblNotify.Font = New Font("Garamond", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNotify.ForeColor = Color.DarkRed
        lblNotify.Location = New Point(43, 34)
        lblNotify.Margin = New Padding(4, 0, 4, 0)
        lblNotify.Name = "lblNotify"
        lblNotify.Size = New Size(90, 108)
        lblNotify.TabIndex = 26
        lblNotify.Text = "0"
        lblNotify.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LogoutToolStripMenuItem
        ' 
        LogoutToolStripMenuItem.Font = New Font("Garamond", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LogoutToolStripMenuItem.ForeColor = Color.DarkRed
        LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        LogoutToolStripMenuItem.Size = New Size(104, 29)
        LogoutToolStripMenuItem.Text = "LOGOUT"
        ' 
        ' btnCaseFeeds
        ' 
        btnCaseFeeds.BackColor = Color.WhiteSmoke
        btnCaseFeeds.FlatAppearance.BorderSize = 0
        btnCaseFeeds.Font = New Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCaseFeeds.ForeColor = Color.Black
        btnCaseFeeds.Location = New Point(14, 941)
        btnCaseFeeds.Margin = New Padding(4, 5, 4, 5)
        btnCaseFeeds.Name = "btnCaseFeeds"
        btnCaseFeeds.Size = New Size(342, 95)
        btnCaseFeeds.TabIndex = 9
        btnCaseFeeds.Text = "Crime Feeds"
        btnCaseFeeds.UseVisualStyleBackColor = False
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {LogoutToolStripMenuItem, AllUserAccountToolStripMenuItem, STATISTICSToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(8, 2, 0, 2)
        MenuStrip1.Size = New Size(1760, 33)
        MenuStrip1.TabIndex = 4
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' AllUserAccountToolStripMenuItem
        ' 
        AllUserAccountToolStripMenuItem.Font = New Font("Garamond", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        AllUserAccountToolStripMenuItem.Name = "AllUserAccountToolStripMenuItem"
        AllUserAccountToolStripMenuItem.Size = New Size(208, 29)
        AllUserAccountToolStripMenuItem.Text = "ALL USER ACCOUNTS"
        ' 
        ' STATISTICSToolStripMenuItem
        ' 
        STATISTICSToolStripMenuItem.Name = "STATISTICSToolStripMenuItem"
        STATISTICSToolStripMenuItem.Size = New Size(116, 29)
        STATISTICSToolStripMenuItem.Text = "STATISTICS"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(panelLeft)
        Panel1.Controls.Add(PanelWithUC)
        Panel1.Controls.Add(MenuStrip1)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1760, 688)
        Panel1.TabIndex = 3
        ' 
        ' panelLeft
        ' 
        panelLeft.AutoScroll = True
        panelLeft.BackColor = Color.White
        panelLeft.BorderStyle = BorderStyle.FixedSingle
        panelLeft.Controls.Add(btnNoUse)
        panelLeft.Controls.Add(btnCreateAccount)
        panelLeft.Controls.Add(PictureBox1)
        panelLeft.Controls.Add(Panel2)
        panelLeft.Controls.Add(btnCaseFeeds)
        panelLeft.Controls.Add(btnCreateCrimeCategory)
        panelLeft.Controls.Add(btnCreateZones)
        panelLeft.Controls.Add(Panel4)
        panelLeft.Controls.Add(Panel5)
        panelLeft.Dock = DockStyle.Left
        panelLeft.ForeColor = Color.MintCream
        panelLeft.Location = New Point(0, 33)
        panelLeft.Margin = New Padding(4, 5, 4, 5)
        panelLeft.Name = "panelLeft"
        panelLeft.Size = New Size(410, 655)
        panelLeft.TabIndex = 3
        ' 
        ' btnNoUse
        ' 
        btnNoUse.BackColor = Color.White
        btnNoUse.FlatAppearance.BorderSize = 0
        btnNoUse.Font = New Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNoUse.ForeColor = Color.Black
        btnNoUse.Location = New Point(31, 307)
        btnNoUse.Margin = New Padding(4, 5, 4, 5)
        btnNoUse.Name = "btnNoUse"
        btnNoUse.Size = New Size(10, 95)
        btnNoUse.TabIndex = 14
        btnNoUse.Text = "Create Zones"
        btnNoUse.UseVisualStyleBackColor = False
        ' 
        ' btnCreateAccount
        ' 
        btnCreateAccount.BackColor = Color.WhiteSmoke
        btnCreateAccount.FlatAppearance.BorderSize = 0
        btnCreateAccount.Font = New Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCreateAccount.ForeColor = Color.Black
        btnCreateAccount.Location = New Point(12, 637)
        btnCreateAccount.Margin = New Padding(4, 5, 4, 5)
        btnCreateAccount.Name = "btnCreateAccount"
        btnCreateAccount.Size = New Size(342, 95)
        btnCreateAccount.TabIndex = 13
        btnCreateAccount.Text = "Create Users"
        btnCreateAccount.UseVisualStyleBackColor = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(61, 4)
        PictureBox1.Margin = New Padding(4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(268, 246)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' btnCreateCrimeCategory
        ' 
        btnCreateCrimeCategory.BackColor = Color.WhiteSmoke
        btnCreateCrimeCategory.FlatAppearance.BorderSize = 0
        btnCreateCrimeCategory.Font = New Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCreateCrimeCategory.ForeColor = Color.Black
        btnCreateCrimeCategory.Location = New Point(12, 793)
        btnCreateCrimeCategory.Margin = New Padding(4, 5, 4, 5)
        btnCreateCrimeCategory.Name = "btnCreateCrimeCategory"
        btnCreateCrimeCategory.Size = New Size(342, 95)
        btnCreateCrimeCategory.TabIndex = 8
        btnCreateCrimeCategory.Text = "Crime Category"
        btnCreateCrimeCategory.UseVisualStyleBackColor = False
        ' 
        ' btnCreateZones
        ' 
        btnCreateZones.BackColor = Color.WhiteSmoke
        btnCreateZones.FlatAppearance.BorderSize = 0
        btnCreateZones.Font = New Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCreateZones.ForeColor = Color.Black
        btnCreateZones.Location = New Point(12, 478)
        btnCreateZones.Margin = New Padding(4, 5, 4, 5)
        btnCreateZones.Name = "btnCreateZones"
        btnCreateZones.Size = New Size(342, 95)
        btnCreateZones.TabIndex = 1
        btnCreateZones.Text = "Create Zones"
        btnCreateZones.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel4.Location = New Point(182, 461)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(26, 602)
        Panel4.TabIndex = 1
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel5.Location = New Point(189, 148)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(19, 112)
        Panel5.TabIndex = 1
        ' 
        ' PanelWithUC
        ' 
        PanelWithUC.AutoScroll = True
        PanelWithUC.BackColor = Color.WhiteSmoke
        PanelWithUC.BorderStyle = BorderStyle.FixedSingle
        PanelWithUC.Dock = DockStyle.Right
        PanelWithUC.ForeColor = SystemColors.InactiveBorder
        PanelWithUC.Location = New Point(354, 33)
        PanelWithUC.Margin = New Padding(571, 5, 4, 5)
        PanelWithUC.Name = "PanelWithUC"
        PanelWithUC.Size = New Size(1406, 655)
        PanelWithUC.TabIndex = 2
        ' 
        ' AdminDashboard
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1760, 688)
        Controls.Add(Panel1)
        Name = "AdminDashboard"
        Text = "AdminDashboard"
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(pbxNotify, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        panelLeft.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pbxNotify As PictureBox
    Friend WithEvents lblNotify As Label
    Friend WithEvents LogoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnCaseFeeds As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents Panel1 As Panel
    Friend WithEvents panelLeft As Panel
    Friend WithEvents btnCreateCrimeCategory As Button
    Friend WithEvents btnCreateZones As Button
    Friend WithEvents PanelWithUC As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents AllUserAccountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnCreateAccount As Button
    Friend WithEvents btnNoUse As Button
    Friend WithEvents STATISTICSToolStripMenuItem As ToolStripMenuItem
End Class
