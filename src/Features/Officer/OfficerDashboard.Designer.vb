<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OfficerDashboard
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
        Panel1 = New Panel()
        Panel2 = New Panel()
        FlowLayoutPanelBackground = New FlowLayoutPanel()
        PanelWithNavButtons = New Panel()
        pbxNotify = New PictureBox()
        lblNotify = New Label()
        btnHandleCrimeCases = New Button()
        lblFullName = New Label()
        MenuStrip1 = New MenuStrip()
        MoreOptionsToolStripMenuItem = New ToolStripMenuItem()
        LOGOUTToolStripMenuItem = New ToolStripMenuItem()
        PanelWithUC = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        FlowLayoutPanelBackground.SuspendLayout()
        PanelWithNavButtons.SuspendLayout()
        CType(pbxNotify, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1621, 709)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(FlowLayoutPanelBackground)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1621, 709)
        Panel2.TabIndex = 1
        ' 
        ' FlowLayoutPanelBackground
        ' 
        FlowLayoutPanelBackground.Controls.Add(PanelWithNavButtons)
        FlowLayoutPanelBackground.Controls.Add(PanelWithUC)
        FlowLayoutPanelBackground.Dock = DockStyle.Fill
        FlowLayoutPanelBackground.Location = New Point(0, 0)
        FlowLayoutPanelBackground.Margin = New Padding(4)
        FlowLayoutPanelBackground.Name = "FlowLayoutPanelBackground"
        FlowLayoutPanelBackground.Size = New Size(1621, 709)
        FlowLayoutPanelBackground.TabIndex = 2
        ' 
        ' PanelWithNavButtons
        ' 
        PanelWithNavButtons.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        PanelWithNavButtons.Controls.Add(pbxNotify)
        PanelWithNavButtons.Controls.Add(lblNotify)
        PanelWithNavButtons.Controls.Add(btnHandleCrimeCases)
        PanelWithNavButtons.Controls.Add(lblFullName)
        PanelWithNavButtons.Controls.Add(MenuStrip1)
        PanelWithNavButtons.Location = New Point(4, 4)
        PanelWithNavButtons.Margin = New Padding(4)
        PanelWithNavButtons.Name = "PanelWithNavButtons"
        PanelWithNavButtons.Size = New Size(1620, 99)
        PanelWithNavButtons.TabIndex = 29
        ' 
        ' pbxNotify
        ' 
        pbxNotify.Image = My.Resources.Resources.notify
        pbxNotify.Location = New Point(812, 15)
        pbxNotify.Margin = New Padding(4)
        pbxNotify.Name = "pbxNotify"
        pbxNotify.Size = New Size(88, 78)
        pbxNotify.SizeMode = PictureBoxSizeMode.StretchImage
        pbxNotify.TabIndex = 25
        pbxNotify.TabStop = False
        ' 
        ' lblNotify
        ' 
        lblNotify.AutoSize = True
        lblNotify.BackColor = Color.Transparent
        lblNotify.Font = New Font("Garamond", 36F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNotify.ForeColor = Color.White
        lblNotify.Location = New Point(739, 14)
        lblNotify.Margin = New Padding(4, 0, 4, 0)
        lblNotify.Name = "lblNotify"
        lblNotify.Size = New Size(69, 81)
        lblNotify.TabIndex = 24
        lblNotify.Text = "0"
        lblNotify.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnHandleCrimeCases
        ' 
        btnHandleCrimeCases.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        btnHandleCrimeCases.FlatAppearance.BorderSize = 0
        btnHandleCrimeCases.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Bold)
        btnHandleCrimeCases.ForeColor = Color.Transparent
        btnHandleCrimeCases.Location = New Point(1167, 19)
        btnHandleCrimeCases.Margin = New Padding(4, 5, 4, 5)
        btnHandleCrimeCases.Name = "btnHandleCrimeCases"
        btnHandleCrimeCases.Size = New Size(342, 60)
        btnHandleCrimeCases.TabIndex = 21
        btnHandleCrimeCases.Text = "Handle Crime Cases"
        btnHandleCrimeCases.UseVisualStyleBackColor = False
        ' 
        ' lblFullName
        ' 
        lblFullName.AutoSize = True
        lblFullName.BackColor = Color.Transparent
        lblFullName.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFullName.ForeColor = Color.White
        lblFullName.Location = New Point(358, 34)
        lblFullName.Margin = New Padding(4, 0, 4, 0)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New Size(96, 31)
        lblFullName.TabIndex = 19
        lblFullName.Text = "Officer"
        lblFullName.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.BackColor = Color.Transparent
        MenuStrip1.Dock = DockStyle.Left
        MenuStrip1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {MoreOptionsToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(8, 2, 0, 2)
        MenuStrip1.Size = New Size(153, 99)
        MenuStrip1.TabIndex = 20
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' MoreOptionsToolStripMenuItem
        ' 
        MoreOptionsToolStripMenuItem.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        MoreOptionsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LOGOUTToolStripMenuItem})
        MoreOptionsToolStripMenuItem.ForeColor = Color.White
        MoreOptionsToolStripMenuItem.Name = "MoreOptionsToolStripMenuItem"
        MoreOptionsToolStripMenuItem.Size = New Size(136, 29)
        MoreOptionsToolStripMenuItem.Text = "More Options"
        ' 
        ' LOGOUTToolStripMenuItem
        ' 
        LOGOUTToolStripMenuItem.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        LOGOUTToolStripMenuItem.ForeColor = Color.White
        LOGOUTToolStripMenuItem.Name = "LOGOUTToolStripMenuItem"
        LOGOUTToolStripMenuItem.Size = New Size(188, 34)
        LOGOUTToolStripMenuItem.Text = "LOGOUT"
        ' 
        ' PanelWithUC
        ' 
        PanelWithUC.Location = New Point(4, 111)
        PanelWithUC.Margin = New Padding(4)
        PanelWithUC.Name = "PanelWithUC"
        PanelWithUC.Size = New Size(1618, 1055)
        PanelWithUC.TabIndex = 30
        ' 
        ' OfficerDashboard
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1621, 709)
        Controls.Add(Panel1)
        Name = "OfficerDashboard"
        Text = "OfficerDashboard"
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        FlowLayoutPanelBackground.ResumeLayout(False)
        PanelWithNavButtons.ResumeLayout(False)
        PanelWithNavButtons.PerformLayout()
        CType(pbxNotify, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents FlowLayoutPanelBackground As FlowLayoutPanel
    Friend WithEvents PanelWithNavButtons As Panel
    Friend WithEvents pbxNotify As PictureBox
    Friend WithEvents lblNotify As Label
    Friend WithEvents btnHandleCrimeCases As Button
    Friend WithEvents lblFullName As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MoreOptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LOGOUTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PanelWithUC As Panel
End Class
