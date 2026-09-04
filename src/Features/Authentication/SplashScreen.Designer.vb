<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Panel1 = New Panel()
        Label1 = New Label()
        lblPercentage = New Label()
        ProgressBar1 = New ProgressBar()
        PictureBox1 = New PictureBox()
        Timer1 = New Timer(components)
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(lblPercentage)
        Panel1.Controls.Add(ProgressBar1)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1232, 711)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        Label1.Font = New Font("Garamond", 20F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(310, 545)
        Label1.Name = "Label1"
        Label1.Size = New Size(632, 45)
        Label1.TabIndex = 2
        Label1.Text = "District Police Management System"
        ' 
        ' lblPercentage
        ' 
        lblPercentage.AutoSize = True
        lblPercentage.BackColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        lblPercentage.Font = New Font("Garamond", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPercentage.ForeColor = Color.White
        lblPercentage.Location = New Point(853, 638)
        lblPercentage.Name = "lblPercentage"
        lblPercentage.Size = New Size(22, 25)
        lblPercentage.TabIndex = 1
        lblPercentage.Text = "0"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.BackColor = Color.DarkSlateBlue
        ProgressBar1.Location = New Point(371, 638)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(512, 23)
        ProgressBar1.TabIndex = 1
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Location = New Point(0, 0)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(1232, 711)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Timer1
        ' 
        ' 
        ' SplashScreen
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1232, 711)
        ControlBox = False
        Controls.Add(Panel1)
        Name = "SplashScreen"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SplashScreen"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblPercentage As Label
    Friend WithEvents Label1 As Label
End Class
