<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_CreateZones
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
        Qu = New Label()
        txtZoneSearch = New TextBox()
        Panel2 = New Panel()
        Label6 = New Label()
        btnDeleteZone = New Button()
        lbll = New Label()
        txtZoneName = New TextBox()
        Label1 = New Label()
        btnAddZone = New Button()
        Panel3 = New Panel()
        dgvZones = New DataGridView()
        Panel1.SuspendLayout()
        PanelBackground.SuspendLayout()
        PanelWithDgv.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        PanelWithSearch.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        CType(dgvZones, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(PanelBackground)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1343, 759)
        Panel1.TabIndex = 0
        ' 
        ' PanelBackground
        ' 
        PanelBackground.Controls.Add(PanelWithDgv)
        PanelBackground.Dock = DockStyle.Fill
        PanelBackground.Location = New Point(0, 0)
        PanelBackground.Margin = New Padding(4)
        PanelBackground.Name = "PanelBackground"
        PanelBackground.Size = New Size(1343, 759)
        PanelBackground.TabIndex = 3
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
        PanelWithDgv.Size = New Size(1343, 759)
        PanelWithDgv.TabIndex = 4
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(PanelWithSearch)
        FlowLayoutPanel1.Controls.Add(Panel3)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Margin = New Padding(4)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1343, 759)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' PanelWithSearch
        ' 
        PanelWithSearch.BackColor = Color.White
        PanelWithSearch.Controls.Add(Qu)
        PanelWithSearch.Controls.Add(txtZoneSearch)
        PanelWithSearch.Controls.Add(Panel2)
        PanelWithSearch.Controls.Add(btnDeleteZone)
        PanelWithSearch.Controls.Add(lbll)
        PanelWithSearch.Controls.Add(txtZoneName)
        PanelWithSearch.Controls.Add(Label1)
        PanelWithSearch.Controls.Add(btnAddZone)
        PanelWithSearch.Location = New Point(4, 5)
        PanelWithSearch.Margin = New Padding(4, 5, 4, 5)
        PanelWithSearch.Name = "PanelWithSearch"
        PanelWithSearch.Size = New Size(1337, 386)
        PanelWithSearch.TabIndex = 28
        ' 
        ' Qu
        ' 
        Qu.AutoSize = True
        Qu.BackColor = Color.Transparent
        Qu.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Qu.ForeColor = Color.Black
        Qu.Location = New Point(634, 86)
        Qu.Margin = New Padding(4, 0, 4, 0)
        Qu.Name = "Qu"
        Qu.Size = New Size(112, 33)
        Qu.TabIndex = 77
        Qu.Text = "*Search"
        ' 
        ' txtZoneSearch
        ' 
        txtZoneSearch.BackColor = SystemColors.ButtonHighlight
        txtZoneSearch.Font = New Font("Garamond", 14.25F)
        txtZoneSearch.Location = New Point(634, 128)
        txtZoneSearch.Margin = New Padding(4, 5, 4, 5)
        txtZoneSearch.Name = "txtZoneSearch"
        txtZoneSearch.Size = New Size(386, 40)
        txtZoneSearch.TabIndex = 76
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel2.Controls.Add(Label6)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1337, 72)
        Panel2.TabIndex = 75
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.White
        Label6.Location = New Point(42, 9)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(330, 51)
        Label6.TabIndex = 68
        Label6.Text = "Create A Zone"
        ' 
        ' btnDeleteZone
        ' 
        btnDeleteZone.BackColor = Color.DarkRed
        btnDeleteZone.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDeleteZone.ForeColor = Color.LavenderBlush
        btnDeleteZone.Location = New Point(281, 219)
        btnDeleteZone.Margin = New Padding(4, 5, 4, 5)
        btnDeleteZone.Name = "btnDeleteZone"
        btnDeleteZone.Size = New Size(225, 59)
        btnDeleteZone.TabIndex = 73
        btnDeleteZone.Text = "Delete"
        btnDeleteZone.UseVisualStyleBackColor = False
        ' 
        ' lbll
        ' 
        lbll.AutoSize = True
        lbll.BackColor = Color.Transparent
        lbll.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbll.ForeColor = Color.Black
        lbll.Location = New Point(48, 85)
        lbll.Margin = New Padding(4, 0, 4, 0)
        lbll.Name = "lbll"
        lbll.Size = New Size(81, 32)
        lbll.TabIndex = 69
        lbll.Text = "Name"
        ' 
        ' txtZoneName
        ' 
        txtZoneName.BackColor = SystemColors.ButtonHighlight
        txtZoneName.Font = New Font("Garamond", 14.25F)
        txtZoneName.Location = New Point(45, 128)
        txtZoneName.Margin = New Padding(4, 5, 4, 5)
        txtZoneName.Name = "txtZoneName"
        txtZoneName.Size = New Size(461, 40)
        txtZoneName.TabIndex = 67
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
        ' btnAddZone
        ' 
        btnAddZone.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        btnAddZone.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnAddZone.ForeColor = Color.LavenderBlush
        btnAddZone.Location = New Point(42, 219)
        btnAddZone.Margin = New Padding(4, 5, 4, 5)
        btnAddZone.Name = "btnAddZone"
        btnAddZone.Size = New Size(225, 59)
        btnAddZone.TabIndex = 60
        btnAddZone.Text = "Add Zone"
        btnAddZone.UseVisualStyleBackColor = False
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(dgvZones)
        Panel3.Location = New Point(4, 400)
        Panel3.Margin = New Padding(4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1337, 970)
        Panel3.TabIndex = 30
        ' 
        ' dgvZones
        ' 
        dgvZones.BackgroundColor = Color.White
        dgvZones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvZones.Location = New Point(4, 0)
        dgvZones.Margin = New Padding(4)
        dgvZones.Name = "dgvZones"
        dgvZones.RowHeadersWidth = 51
        dgvZones.Size = New Size(1329, 943)
        dgvZones.TabIndex = 0
        ' 
        ' UC_CreateZones
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_CreateZones"
        Size = New Size(1343, 759)
        Panel1.ResumeLayout(False)
        PanelBackground.ResumeLayout(False)
        PanelWithDgv.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        PanelWithSearch.ResumeLayout(False)
        PanelWithSearch.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        CType(dgvZones, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelBackground As Panel
    Friend WithEvents PanelWithDgv As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents PanelWithSearch As Panel
    Friend WithEvents Qu As Label
    Friend WithEvents txtZoneSearch As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents btnDeleteZone As Button
    Friend WithEvents lbll As Label
    Friend WithEvents txtZoneName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAddZone As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dgvZones As DataGridView

End Class
