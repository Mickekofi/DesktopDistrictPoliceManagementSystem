<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_AllUserAccount
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
        Panel1 = New Panel()
        PanelBackground = New Panel()
        PanelWithDgv = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        PanelWithSearch = New Panel()
        btnDeleteAccount = New Button()
        Label2 = New Label()
        cmbFilterZones = New ComboBox()
        ccc = New Label()
        Qu = New Label()
        txtSearch = New TextBox()
        btnStatus = New Button()
        cmbAccountRole = New ComboBox()
        Panel2 = New Panel()
        dgvAccounts = New DataGridView()
        Panel1.SuspendLayout()
        PanelBackground.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel2.SuspendLayout()
        CType(dgvAccounts, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(PanelBackground)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1390, 763)
        Panel1.TabIndex = 0
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(1390, 763)
        PanelBackground.TabIndex = 2
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
        PanelWithDgv.Size = New Size(1390, 763)
        PanelWithDgv.TabIndex = 4
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(PanelWithSearch)
        FlowLayoutPanel1.Controls.Add(Panel2)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1390, 763)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(btnDeleteAccount)
        PanelWithSearch.Controls.Add(Label2)
        PanelWithSearch.Controls.Add(cmbFilterZones)
        PanelWithSearch.Controls.Add(ccc)
        PanelWithSearch.Controls.Add(Qu)
        PanelWithSearch.Controls.Add(txtSearch)
        PanelWithSearch.Controls.Add(btnStatus)
        PanelWithSearch.Controls.Add(cmbAccountRole)
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(1390, 267)
        PanelWithSearch.TabIndex = 28
        ' 
        ' btnDeleteAccount
        ' 
        btnDeleteAccount.BackColor = Color.Red
        btnDeleteAccount.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDeleteAccount.ForeColor = Color.LavenderBlush
        btnDeleteAccount.Location = New Point(988, 117)
        btnDeleteAccount.Margin = New Padding(4, 5, 4, 5)
        btnDeleteAccount.Name = "btnDeleteAccount"
        btnDeleteAccount.Size = New Size(372, 59)
        btnDeleteAccount.TabIndex = 82
        btnDeleteAccount.Text = "DELETE"
        btnDeleteAccount.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(46, 159)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(165, 33)
        Label2.TabIndex = 81
        Label2.Text = "Filter Zones"
        ' 
        ' cmbFilterZones
        ' 
        cmbFilterZones.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbFilterZones.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        cmbFilterZones.FormattingEnabled = True
        cmbFilterZones.Location = New Point(46, 201)
        cmbFilterZones.Margin = New Padding(4, 5, 4, 5)
        cmbFilterZones.Name = "cmbFilterZones"
        cmbFilterZones.Size = New Size(386, 41)
        cmbFilterZones.TabIndex = 80
        cmbFilterZones.Text = "--select Zones--"
        ' 
        ' ccc
        ' 
        ccc.AutoSize = True
        ccc.BackColor = Color.Transparent
        ccc.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ccc.ForeColor = Color.Black
        ccc.Location = New Point(34, 41)
        ccc.Margin = New Padding(4, 0, 4, 0)
        ccc.Name = "ccc"
        ccc.Size = New Size(208, 33)
        ccc.TabIndex = 79
        ccc.Text = "*Filter By Type"
        ' 
        ' Qu
        ' 
        Qu.AutoSize = True
        Qu.BackColor = Color.Transparent
        Qu.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Qu.ForeColor = Color.Black
        Qu.Location = New Point(532, 41)
        Qu.Margin = New Padding(4, 0, 4, 0)
        Qu.Name = "Qu"
        Qu.Size = New Size(112, 33)
        Qu.TabIndex = 78
        Qu.Text = "*Search"
        ' 
        ' txtSearch
        ' 
        txtSearch.BackColor = SystemColors.ButtonHighlight
        txtSearch.Font = New Font("Garamond", 14.25F)
        txtSearch.Location = New Point(532, 83)
        txtSearch.Margin = New Padding(4, 5, 4, 5)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(386, 40)
        txtSearch.TabIndex = 77
        ' 
        ' btnStatus
        ' 
        btnStatus.BackColor = Color.Red
        btnStatus.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnStatus.ForeColor = Color.LavenderBlush
        btnStatus.Location = New Point(988, 28)
        btnStatus.Margin = New Padding(4, 5, 4, 5)
        btnStatus.Name = "btnStatus"
        btnStatus.Size = New Size(372, 59)
        btnStatus.TabIndex = 60
        btnStatus.Text = "Status"
        btnStatus.UseVisualStyleBackColor = False
        ' 
        ' cmbAccountRole
        ' 
        cmbAccountRole.Font = New Font("Garamond", 14.25F, FontStyle.Bold)
        cmbAccountRole.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        cmbAccountRole.FormattingEnabled = True
        cmbAccountRole.Location = New Point(46, 83)
        cmbAccountRole.Margin = New Padding(4, 5, 4, 5)
        cmbAccountRole.Name = "cmbAccountRole"
        cmbAccountRole.Size = New Size(386, 41)
        cmbAccountRole.TabIndex = 59
        cmbAccountRole.Text = "--select Type--"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(dgvAccounts)
        Panel2.Location = New Point(4, 281)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1404, 940)
        Panel2.TabIndex = 29
        ' 
        ' dgvAccounts
        ' 
        dgvAccounts.BackgroundColor = Color.White
        dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccounts.Location = New Point(4, 4)
        dgvAccounts.Margin = New Padding(4)
        dgvAccounts.Name = "dgvAccounts"
        dgvAccounts.RowHeadersWidth = 51
        dgvAccounts.Size = New Size(1386, 880)
        dgvAccounts.TabIndex = 0
        ' 
        ' UC_AllUserAccount
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_AllUserAccount"
        Size = New Size(1390, 763)
        Panel1.ResumeLayout(False)
        PanelBackground.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(dgvAccounts, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelBackground As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents ccc As Label
    Friend WithEvents Qu As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnStatus As Button
    Friend WithEvents cmbAccountRole As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents dgvAccounts As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbFilterZones As ComboBox
    Friend WithEvents btnDeleteAccount As Button

End Class
