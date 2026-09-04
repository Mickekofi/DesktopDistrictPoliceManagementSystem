<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Panel1 = New Panel()
        Panel2 = New Panel()
        Panel5 = New Panel()
        PictureBox1 = New PictureBox()
        Panel3 = New Panel()
        btnNoUse = New Button()
        Label3 = New Label()
        Panel6 = New Panel()
        Label1 = New Label()
        txtPassword = New TextBox()
        Username = New Label()
        btnLogin = New Button()
        txtUsername = New TextBox()
        Panel4 = New Panel()
        Panel7 = New Panel()
        Panel8 = New Panel()
        Panel9 = New Panel()
        tmrFade = New Timer(components)
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1623, 776)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.AutoScroll = True
        Panel2.AutoSize = True
        Panel2.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Panel2.Controls.Add(Panel5)
        Panel2.Controls.Add(Panel3)
        Panel2.Controls.Add(Panel4)
        Panel2.Controls.Add(Panel7)
        Panel2.Controls.Add(Panel8)
        Panel2.Controls.Add(Panel9)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1623, 776)
        Panel2.TabIndex = 1
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.White
        Panel5.Controls.Add(PictureBox1)
        Panel5.Location = New Point(706, 34)
        Panel5.Margin = New Padding(4)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(495, 492)
        Panel5.TabIndex = 1
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(0, 0)
        PictureBox1.Margin = New Padding(4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(491, 476)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(btnNoUse)
        Panel3.Controls.Add(Label3)
        Panel3.Controls.Add(Panel6)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(txtPassword)
        Panel3.Controls.Add(Username)
        Panel3.Controls.Add(btnLogin)
        Panel3.Controls.Add(txtUsername)
        Panel3.Location = New Point(213, 219)
        Panel3.Margin = New Padding(4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1387, 864)
        Panel3.TabIndex = 0
        ' 
        ' btnNoUse
        ' 
        btnNoUse.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        btnNoUse.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNoUse.ForeColor = Color.White
        btnNoUse.Location = New Point(985, 332)
        btnNoUse.Margin = New Padding(4)
        btnNoUse.Name = "btnNoUse"
        btnNoUse.Size = New Size(10, 76)
        btnNoUse.TabIndex = 81
        btnNoUse.Text = "Login"
        btnNoUse.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Garamond", 36F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(524, 327)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(347, 81)
        Label3.TabIndex = 79
        Label3.Text = "User Login"
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DarkRed
        Panel6.Location = New Point(513, 339)
        Panel6.Margin = New Padding(4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(180, 89)
        Panel6.TabIndex = 80
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(256, 607)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(128, 31)
        Label1.TabIndex = 9
        Label1.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.LightGray
        txtPassword.Font = New Font("Garamond", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtPassword.Location = New Point(419, 600)
        txtPassword.Margin = New Padding(4)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(743, 44)
        txtPassword.TabIndex = 5
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' Username
        ' 
        Username.AutoSize = True
        Username.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Username.Location = New Point(256, 495)
        Username.Margin = New Padding(4, 0, 4, 0)
        Username.Name = "Username"
        Username.Size = New Size(135, 31)
        Username.TabIndex = 3
        Username.Text = "Username"
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        btnLogin.Font = New Font("Garamond", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(256, 765)
        btnLogin.Margin = New Padding(4)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(342, 76)
        btnLogin.TabIndex = 2
        btnLogin.Text = "Login"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' txtUsername
        ' 
        txtUsername.BackColor = Color.LightGray
        txtUsername.Font = New Font("Garamond", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtUsername.Location = New Point(419, 488)
        txtUsername.Margin = New Padding(4)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(743, 44)
        txtUsername.TabIndex = 1
        txtUsername.WordWrap = False
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel4.Location = New Point(4, 441)
        Panel4.Margin = New Padding(4)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1671, 46)
        Panel4.TabIndex = 2
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.DarkRed
        Panel7.Location = New Point(1119, 17)
        Panel7.Margin = New Padding(4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(91, 194)
        Panel7.TabIndex = 3
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel8.Location = New Point(194, 197)
        Panel8.Margin = New Padding(4)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(107, 245)
        Panel8.TabIndex = 4
        ' 
        ' Panel9
        ' 
        Panel9.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Panel9.Location = New Point(1209, 211)
        Panel9.Margin = New Padding(4)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(139, 45)
        Panel9.TabIndex = 5
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1623, 776)
        Controls.Add(Panel1)
        Name = "Login"
        Text = "Login"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Username As Label
    Friend WithEvents btnLogin As Button
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents tmrFade As Timer
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents btnNoUse As Button
End Class
