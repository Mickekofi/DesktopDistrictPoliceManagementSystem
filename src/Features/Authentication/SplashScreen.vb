Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.Logging

Public Class SplashScreen

    Private Sub FadeIn(sender As Object, e As EventArgs)
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05
        Else
            Timer1.Stop()
            RemoveHandler Timer1.Tick, AddressOf FadeIn
        End If
    End Sub



    Dim quotes() As String = {
        "please wait...",
        "please wait... ",
        "please wait... ",
        "please wait... ",
        "please wait...",
        "please wait...",
        "please wait...",
        "please wait...",
        "Completed..."
                                    }


    Dim quoteIndex As Integer = 0







    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.Text = ""

        ' In your Form Load

        'Window State to Maximized
        ' Me.WindowState = FormWindowState.Maximized

        'Apply Circled hat picture
        ' CircleShape(PictureBox1, 5)


        'Always Remember that the Speed or progress oft the ProgressBar is always Controlled by the Timer Control, Do it From the UI
        'Mentioned the [Name] ProgressBar and set to start 
        ProgressBar1.Value = 10

        'Assigning ProgressBar motion counts to Lable [Name] called lblPercentage for display progress in percent
        lblPercentage.Text = ProgressBar1.Value.ToString() & "%"
        ' lblQuote.Text = quotes(quoteIndex)

        Timer1.Start()



        'LOADING THE GIF IMAGE FROM THE EMBEDDED RESOURCE
        ' Step 1: Gets a handle on the current running app
        Dim asm = System.Reflection.Assembly.GetExecutingAssembly()
        ' Step 2: Retrieves the embedded GIF file as a stream
        Dim stream = asm.GetManifestResourceStream("District_Police_Management_System.Slogo.gif")
        ' Then Step 3: Converts that stream into an image And loads it into a PictureBox
        PictureBox1.Image = Image.FromStream(stream)

    End Sub




    'This Handles the Timer Control Effects on Both the ProgressBar and the Quotes 
    'This Handles the Timer Control Effects on Both the ProgressBar and the Quotes 
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Progress increment
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 1
            lblPercentage.Text = ProgressBar1.Value.ToString() & "%"

            ' Change quote every 10%
            If ProgressBar1.Value Mod 10 = 0 Then
                quoteIndex = (quoteIndex + 1) Mod quotes.Length
                ' lblQuote.Text = quotes(quoteIndex)
            End If

        Else
            Timer1.Stop()
            'Showing the Next Page
            Dim nextForm As New Login()
            ' nextForm.WindowState = FormWindowState.Maximized ' or Maximized, as you prefer
            Login.Show()
            Me.Hide()

        End If


        'Set ProgressBar to Red



    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class