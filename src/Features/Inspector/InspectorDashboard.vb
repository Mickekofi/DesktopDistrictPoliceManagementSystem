Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class InspectorDashboard

    Private inspectorToolTip As New ToolTip()
    Private WithEvents pollTimer As New Timer()

    ' =================================================================
    ' FORM INITIALIZATION & SECURITY GATEKEEPER
    ' =================================================================
    Private Sub InspectorDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. THE GATEKEEPER: Instantly kick out unauthorized users
        If Not SessionManager.HasRole("INSPECTOR") Then
            MessageBox.Show("Unauthorized Access. Inspector privileges required.", "Security Violation", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            SessionManager.EndSession()
            Dim login As New Login()
            login.Show()
            Me.Close()
            Return
        End If

        Me.WindowState = FormWindowState.Maximized

        ' 2. Personalize Dashboard
        ' Assuming SessionManager stores the name during Login
        lblFullName.Text = $"Welcome, {SessionManager.CurrentAccountName}"

        ' 3. UI Styling
        RadiusButton(btnFileCase, 1.5F)

        Dim navButtons As New List(Of Button) From {
            btnFileCase
        }
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnFileCase)

        ' 4. UX GUIDANCE: Setup Tooltips
        inspectorToolTip.ToolTipIcon = ToolTipIcon.Info
        inspectorToolTip.IsBalloon = True
        inspectorToolTip.AutoPopDelay = 5000
        inspectorToolTip.InitialDelay = 500

        inspectorToolTip.SetToolTip(btnFileCase, "Draft and submit a new official case report.")
        'inspectorToolTip.SetToolTip(LOGOUTToolStripMenuItem, "Securely end your current session.")
        inspectorToolTip.SetToolTip(lblFullName, "Your authorized personnel identifier.")

        ' 5. Initialize Live Notifications safely
        pollTimer.Interval = 10000 ' 10 SECONDS.
        pollTimer.Start()

        ' Run the first check immediately
        RefreshNotifications()
    End Sub

    Private Sub InspectorDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If SessionManager.HasRole("INSPECTOR") Then
            ' Load default screen
            LoadControl(New UC_FileCase())
        End If
    End Sub

    ' =================================================================
    ' USER CONTROL MANAGER
    ' =================================================================
    Private Sub LoadControl(control As UserControl)
        PanelWithUC.Controls.Clear()
        control.Dock = DockStyle.Fill
        PanelWithUC.Controls.Add(control)
    End Sub

    ' =================================================================
    ' NAVIGATION EVENTS
    ' =================================================================
    Private Sub btnFileCase_Click(sender As Object, e As EventArgs) Handles btnFileCase.Click
        LoadControl(New UC_FileCase())
    End Sub

    Private Sub LOGOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOGOUTToolStripMenuItem.Click
        pollTimer.Stop()
        SessionManager.EndSession()
        Dim login As New Login()
        login.Show()
        Me.Close()
    End Sub

    Private Sub pbxNotify_Click(sender As Object, e As EventArgs) Handles pbxNotify.Click
        ' You requested this navigates to Case Tracker
        ' Ensure you actually have a UC_CaseTracker.vb created
        LoadControl(New UC_CaseTracker())
    End Sub

    ' =================================================================
    ' NOTIFICATION ENGINE (Filtered specifically to THIS Inspector)
    ' =================================================================
    Private Sub pollTimer_Tick(sender As Object, e As EventArgs) Handles pollTimer.Tick
        RefreshNotifications()
    End Sub

    ' =================================================================
    ' NOTIFICATION ENGINE (Strictly Status Tracking)
    ' =================================================================
    Private Sub RefreshNotifications()
        ' Query: How many of THIS inspector's cases are currently sitting in the ASSIGNED state?
        ' Note: This is a state-tracker, not a clearable alert. It will persist until the Officer updates it.
        Dim queryAssigned As String = "SELECT COUNT(case_id) FROM cases WHERE reported_by_account = @accountId AND status = 'ASSIGNED'"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmdAssigned As New MySqlCommand(queryAssigned, conn)
                    cmdAssigned.Parameters.AddWithValue("@accountId", SessionManager.CurrentAccountId)

                    Dim assignedCount As Integer = Convert.ToInt32(cmdAssigned.ExecuteScalar())

                    If assignedCount > 0 Then
                        lblNotify.Text = assignedCount.ToString()
                        lblNotify.Visible = True

                        Dim tooltipMessage As String = $"Update: {assignedCount} of your case(s) have been dispatched to field units."
                        inspectorToolTip.SetToolTip(pbxNotify, tooltipMessage)
                        inspectorToolTip.SetToolTip(lblNotify, tooltipMessage)
                    Else
                        lblNotify.Visible = False
                        inspectorToolTip.SetToolTip(pbxNotify, "No recent dispatch updates on your filed cases.")
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            lblNotify.Text = "!"
            lblNotify.Visible = True
            inspectorToolTip.SetToolTip(pbxNotify, "Database connection lost. Cannot refresh case statuses.")
        End Try
    End Sub

    ' =================================================================
    ' CLEANUP
    ' =================================================================
    Private Sub InspectorDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        pollTimer.Stop()
        If Application.OpenForms.Count = 0 Then
            Application.Exit()
        End If
    End Sub

    Private Sub PanelWithUC_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithUC.Paint
    End Sub

    Private Sub PanelWithNavButtons_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithNavButtons.Paint

    End Sub
End Class