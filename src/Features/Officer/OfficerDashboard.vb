Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class OfficerDashboard

    Private officerToolTip As New ToolTip()
    Private WithEvents pollTimer As New Timer()

    ' =================================================================
    ' FORM INITIALIZATION & SECURITY GATEKEEPER
    ' =================================================================
    Private Sub OfficerDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. THE GATEKEEPER: Instantly kick out unauthorized users
        If Not SessionManager.HasRole("OFFICER_GROUP") Then
            MessageBox.Show("Unauthorized Access. Officer Group privileges required.", "Security Violation", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            SessionManager.EndSession()
            Dim login As New Login()
            login.Show()
            Me.Close()
            Return
        End If

        Me.WindowState = FormWindowState.Maximized

        ' 2. Personalize Dashboard
        lblFullName.Text = $"Active Unit: {SessionManager.CurrentAccountName}"

        ' 3. UI Styling
        RadiusButton(btnHandleCrimeCases, 1.5F)

        Dim navButtons As New List(Of Button) From {
            btnHandleCrimeCases
        }
        ' Assuming you have your standard NavButtonStyles helper
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnHandleCrimeCases)

        ' 4. UX GUIDANCE: Setup Tooltips
        officerToolTip.ToolTipIcon = ToolTipIcon.Info
        officerToolTip.IsBalloon = True
        officerToolTip.AutoPopDelay = 5000
        officerToolTip.InitialDelay = 500

        officerToolTip.SetToolTip(btnHandleCrimeCases, "View your active workload, update investigation statuses, and file operational reports.")
        officerToolTip.SetToolTip(lblFullName, "Your authorized operational unit identifier.")

        ' 5. Initialize Live Notifications safely
        pollTimer.Interval = 10000 ' Poll every 10 SECONDS
        pollTimer.Start()

        ' Run the first check immediately
        RefreshNotifications()
    End Sub

    Private Sub OfficerDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If SessionManager.HasRole("OFFICER_GROUP") Then
            ' Load default operational screen
            LoadControl(New UC_HandleCrimeCase())
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
    Private Sub btnHandleCrimeCases_Click(sender As Object, e As EventArgs) Handles btnHandleCrimeCases.Click
        LoadControl(New UC_HandleCrimeCase())
    End Sub

    Private Sub LOGOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOGOUTToolStripMenuItem.Click
        pollTimer.Stop()
        SessionManager.EndSession()
        Dim login As New Login()
        login.Show()
        Me.Close()
    End Sub

    ' =================================================================
    ' NOTIFICATION ENGINE (Filtered strictly to Pending Assignments)
    ' =================================================================
    Private Sub pollTimer_Tick(sender As Object, e As EventArgs) Handles pollTimer.Tick
        RefreshNotifications()
    End Sub

    Private Sub RefreshNotifications()
        ' Query: How many cases are explicitly ASSIGNED to this specific group and remain untouched (Status still 'ASSIGNED')?
        Dim queryCount As String = "
            SELECT COUNT(c.case_id) 
            FROM cases c
            INNER JOIN case_assignments ca ON c.case_id = ca.case_id
            WHERE ca.assigned_to_group = @accountId 
            AND c.status = 'ASSIGNED'"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmdCount As New MySqlCommand(queryCount, conn)
                    cmdCount.Parameters.AddWithValue("@accountId", SessionManager.CurrentAccountId)

                    Dim pendingCount As Integer = Convert.ToInt32(cmdCount.ExecuteScalar())

                    If pendingCount > 0 Then
                        lblNotify.Text = pendingCount.ToString()
                        lblNotify.Visible = True

                        Dim alertMessage As String = $"URGENT DISPATCH: You have {pendingCount} new case(s) awaiting your operational review."
                        officerToolTip.SetToolTip(pbxNotify, alertMessage)
                        officerToolTip.SetToolTip(lblNotify, alertMessage)
                    Else
                        lblNotify.Visible = False
                        officerToolTip.SetToolTip(pbxNotify, "Your operational queue is clear. No pending dispatches.")
                    End If
                End Using
            End Using
        Catch ex As MySqlException
            lblNotify.Text = "!"
            lblNotify.Visible = True
            officerToolTip.SetToolTip(pbxNotify, "System connection lost. Cannot refresh dispatch queue.")
        End Try
    End Sub

    Private Sub pbxNotify_Click(sender As Object, e As EventArgs) Handles pbxNotify.Click
        ' Clicking the notification badge drives them straight to their workload
        LoadControl(New UC_HandleCrimeCase())
    End Sub

    ' =================================================================
    ' CLEANUP
    ' =================================================================
    Private Sub OfficerDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        pollTimer.Stop()
        If Application.OpenForms.Count = 0 Then
            Application.Exit()
        End If
    End Sub

    Private Sub PanelWithUC_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithUC.Paint
    End Sub

End Class