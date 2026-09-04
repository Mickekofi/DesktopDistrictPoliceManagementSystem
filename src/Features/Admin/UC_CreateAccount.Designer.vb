<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_CreateAccount
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
        PanelInputBundle = New Panel()
        Label6 = New Label()
        cmbSelectZone = New ComboBox()
        Label3 = New Label()
        txtPhoneNumber = New TextBox()
        Label5 = New Label()
        txtPassword = New TextBox()
        Label4 = New Label()
        txtAccountName = New TextBox()
        Label1 = New Label()
        btnCreate = New Button()
        cmbSelectRole = New ComboBox()
        Create = New Label()
        txtAccountUsername = New TextBox()
        Label = New Label()
        PanelRedDesign = New Panel()
        Panel3 = New Panel()
        Label2 = New Label()
        Panel4 = New Panel()
        Panel7 = New Panel()
        Panel5 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        PanelInputBundle.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1351, 765)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.AutoScroll = True
        Panel2.BackColor = SystemColors.Menu
        Panel2.Controls.Add(Panel6)
        Panel2.Controls.Add(PanelInputBundle)
        Panel2.Controls.Add(PanelRedDesign)
        Panel2.Controls.Add(Panel3)
        Panel2.Controls.Add(Panel4)
        Panel2.Controls.Add(Panel7)
        Panel2.Controls.Add(Panel5)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1351, 765)
        Panel2.TabIndex = 1
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.White
        Panel6.Location = New Point(1290, 1008)
        Panel6.Margin = New Padding(4, 5, 4, 5)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(25, 66)
        Panel6.TabIndex = 9
        ' 
        ' PanelInputBundle
        ' 
        PanelInputBundle.BackColor = Color.White
        PanelInputBundle.Controls.Add(Label6)
        PanelInputBundle.Controls.Add(cmbSelectZone)
        PanelInputBundle.Controls.Add(Label3)
        PanelInputBundle.Controls.Add(txtPhoneNumber)
        PanelInputBundle.Controls.Add(Label5)
        PanelInputBundle.Controls.Add(txtPassword)
        PanelInputBundle.Controls.Add(Label4)
        PanelInputBundle.Controls.Add(txtAccountName)
        PanelInputBundle.Controls.Add(Label1)
        PanelInputBundle.Controls.Add(btnCreate)
        PanelInputBundle.Controls.Add(cmbSelectRole)
        PanelInputBundle.Controls.Add(Create)
        PanelInputBundle.Controls.Add(txtAccountUsername)
        PanelInputBundle.Controls.Add(Label)
        PanelInputBundle.Location = New Point(281, 150)
        PanelInputBundle.Margin = New Padding(4, 5, 4, 5)
        PanelInputBundle.Name = "PanelInputBundle"
        PanelInputBundle.Size = New Size(784, 1160)
        PanelInputBundle.TabIndex = 4
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.Black
        Label6.Location = New Point(25, 886)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(257, 33)
        Label6.TabIndex = 39
        Label6.Text = "*Area Of Operation"
        ' 
        ' cmbSelectZone
        ' 
        cmbSelectZone.BackColor = Color.WhiteSmoke
        cmbSelectZone.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbSelectZone.ForeColor = Color.Red
        cmbSelectZone.FormattingEnabled = True
        cmbSelectZone.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbSelectZone.Location = New Point(26, 933)
        cmbSelectZone.Margin = New Padding(4, 5, 4, 5)
        cmbSelectZone.Name = "cmbSelectZone"
        cmbSelectZone.Size = New Size(726, 44)
        cmbSelectZone.TabIndex = 38
        cmbSelectZone.Text = "--Select--"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(25, 539)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(205, 33)
        Label3.TabIndex = 37
        Label3.Text = "Phone Number"
        ' 
        ' txtPhoneNumber
        ' 
        txtPhoneNumber.BackColor = Color.WhiteSmoke
        txtPhoneNumber.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtPhoneNumber.ForeColor = SystemColors.ActiveCaptionText
        txtPhoneNumber.Location = New Point(18, 596)
        txtPhoneNumber.Margin = New Padding(4, 5, 4, 5)
        txtPhoneNumber.Name = "txtPhoneNumber"
        txtPhoneNumber.Size = New Size(726, 48)
        txtPhoneNumber.TabIndex = 36
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(24, 401)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(147, 33)
        Label5.TabIndex = 35
        Label5.Text = "*Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.WhiteSmoke
        txtPassword.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtPassword.ForeColor = SystemColors.ActiveCaptionText
        txtPassword.Location = New Point(17, 458)
        txtPassword.Margin = New Padding(4, 5, 4, 5)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(726, 48)
        txtPassword.TabIndex = 34
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(23, 733)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(167, 33)
        Label4.TabIndex = 32
        Label4.Text = "*Rank/Role"
        ' 
        ' txtAccountName
        ' 
        txtAccountName.BackColor = Color.WhiteSmoke
        txtAccountName.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtAccountName.ForeColor = SystemColors.ActiveCaptionText
        txtAccountName.Location = New Point(19, 177)
        txtAccountName.Margin = New Padding(4, 5, 4, 5)
        txtAccountName.Name = "txtAccountName"
        txtAccountName.Size = New Size(726, 48)
        txtAccountName.TabIndex = 31
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(16, 121)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(258, 33)
        Label1.TabIndex = 30
        Label1.Text = "Account Full Name"
        ' 
        ' btnCreate
        ' 
        btnCreate.BackColor = Color.DarkRed
        btnCreate.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCreate.ForeColor = Color.LavenderBlush
        btnCreate.Location = New Point(19, 1069)
        btnCreate.Margin = New Padding(4, 5, 4, 5)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(341, 68)
        btnCreate.TabIndex = 29
        btnCreate.Text = "Create Account"
        btnCreate.UseVisualStyleBackColor = False
        ' 
        ' cmbSelectRole
        ' 
        cmbSelectRole.BackColor = Color.WhiteSmoke
        cmbSelectRole.Font = New Font("Garamond", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmbSelectRole.ForeColor = Color.Red
        cmbSelectRole.FormattingEnabled = True
        cmbSelectRole.Items.AddRange(New Object() {"South Campus", "Central Campus", "North Campus", "Ajumako Campus"})
        cmbSelectRole.Location = New Point(24, 780)
        cmbSelectRole.Margin = New Padding(4, 5, 4, 5)
        cmbSelectRole.Name = "cmbSelectRole"
        cmbSelectRole.Size = New Size(726, 44)
        cmbSelectRole.TabIndex = 26
        cmbSelectRole.Text = "--Select--"
        ' 
        ' Create
        ' 
        Create.AutoSize = True
        Create.BackColor = Color.Transparent
        Create.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Create.ForeColor = Color.Red
        Create.Location = New Point(19, 19)
        Create.Margin = New Padding(4, 0, 4, 0)
        Create.Name = "Create"
        Create.Size = New Size(515, 51)
        Create.TabIndex = 15
        Create.Text = "District Police Account"
        ' 
        ' txtAccountUsername
        ' 
        txtAccountUsername.BackColor = Color.WhiteSmoke
        txtAccountUsername.Font = New Font("Garamond", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtAccountUsername.ForeColor = SystemColors.ActiveCaptionText
        txtAccountUsername.Location = New Point(19, 317)
        txtAccountUsername.Margin = New Padding(4, 5, 4, 5)
        txtAccountUsername.Name = "txtAccountUsername"
        txtAccountUsername.Size = New Size(726, 48)
        txtAccountUsername.TabIndex = 15
        ' 
        ' Label
        ' 
        Label.AutoSize = True
        Label.BackColor = Color.Transparent
        Label.Font = New Font("Garamond", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label.ForeColor = Color.Black
        Label.Location = New Point(16, 265)
        Label.Margin = New Padding(4, 0, 4, 0)
        Label.Name = "Label"
        Label.Size = New Size(280, 33)
        Label.TabIndex = 14
        Label.Text = "*Account User Name"
        ' 
        ' PanelRedDesign
        ' 
        PanelRedDesign.BackColor = Color.White
        PanelRedDesign.Location = New Point(20, 998)
        PanelRedDesign.Margin = New Padding(4, 5, 4, 5)
        PanelRedDesign.Name = "PanelRedDesign"
        PanelRedDesign.Size = New Size(25, 76)
        PanelRedDesign.TabIndex = 5
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel3.Controls.Add(Label2)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Margin = New Padding(4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1394, 89)
        Panel3.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(4, 11)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(662, 61)
        Label2.TabIndex = 23
        Label2.Text = "Create Officers and Inspectors"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel4.Location = New Point(4, 1021)
        Panel4.Margin = New Padding(4, 5, 4, 5)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1390, 33)
        Panel4.TabIndex = 6
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel7.Location = New Point(936, 76)
        Panel7.Margin = New Padding(4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(91, 1265)
        Panel7.TabIndex = 7
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel5.Location = New Point(300, 76)
        Panel5.Margin = New Padding(4)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(91, 1265)
        Panel5.TabIndex = 8
        ' 
        ' UC_CreateAccount
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel1)
        Name = "UC_CreateAccount"
        Size = New Size(1351, 765)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        PanelInputBundle.ResumeLayout(False)
        PanelInputBundle.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PanelInputBundle As Panel
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCreate As Button
    Friend WithEvents cmbSelectRole As ComboBox
    Friend WithEvents Create As Label
    Friend WithEvents txtAccountUsername As TextBox
    Friend WithEvents Label As Label
    Friend WithEvents PanelRedDesign As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtPhoneNumber As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbSelectZone As ComboBox

End Class
