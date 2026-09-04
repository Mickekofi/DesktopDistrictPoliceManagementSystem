Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class AdminDashboard

    Private adminToolTip As New ToolTip()
    Private WithEvents pollTimer As New Timer()

    ' =================================================================
    ' FORM INITIALIZATION & SECURITY GATEKEEPER
    ' =================================================================
    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. THE GATEKEEPER: Instantly kick out unauthorized users
        If Not SessionManager.HasRole("ADMIN") Then
            MessageBox.Show("Unauthorized Access. Administrator privileges required.", "Security Violation", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            SessionManager.EndSession()
            Dim login As New Login()
            login.Show()
            Me.Close()
            Return
        End If

        Me.WindowState = FormWindowState.Maximized

        ' 2. UI Styling
        ' Assuming RadiusButton is in your Shared Module
        RadiusButton(btnCreateZones, 1.5F)
        RadiusButton(btnCreateAccount, 1.5F)
        RadiusButton(btnCreateCrimeCategory, 1.5F)
        RadiusButton(btnCaseFeeds, 1.5F)

        Dim navButtons As New List(Of Button) From {
            btnCreateZones,
            btnCreateAccount,
            btnCreateCrimeCategory,
            btnCaseFeeds
        }

        ' Initialize Nav Styles. Defaulting to Case Feeds.
        NavButtonStyles.InitializeNavButtons(Me, navButtons, btnNoUse)

        ' 3. UX GUIDANCE: Setup Corrected Tooltips
        adminToolTip.ToolTipIcon = ToolTipIcon.Info
        adminToolTip.IsBalloon = True
        adminToolTip.AutoPopDelay = 5000
        adminToolTip.InitialDelay = 500

        adminToolTip.SetToolTip(btnCreateZones, "Manage geographic zones and jurisdictions.")
        adminToolTip.SetToolTip(btnCreateAccount, "Create and manage Inspector and Officer Group accounts.")
        adminToolTip.SetToolTip(btnCreateCrimeCategory, "Manage severity levels and crime classifications.")
        adminToolTip.SetToolTip(btnCaseFeeds, "Monitor live incident reports and assign cases.")

        ' 4. Initialize Live Notifications safely
        pollTimer.Interval = 10000 ' 10 SECONDS. Do not change this to 5 seconds.
        pollTimer.Start()

        ' Run the first check immediately
        RefreshNotifications()
    End Sub

    Private Sub AdminDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If SessionManager.HasRole("ADMIN") Then
            ' Load default screen
            Dim defaultScreen As New UC_Statistics()
            LoadControl(defaultScreen)
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
    Private Sub btnCreateZones_Click(sender As Object, e As EventArgs) Handles btnCreateZones.Click
        LoadControl(New UC_CreateZones())
    End Sub

    Private Sub btnCreateAccount_Click(sender As Object, e As EventArgs) Handles btnCreateAccount.Click
        LoadControl(New UC_CreateAccount())
    End Sub

    Private Sub btnCreateCrimeCategory_Click(sender As Object, e As EventArgs) Handles btnCreateCrimeCategory.Click
        LoadControl(New UC_CreateCrimeCategory())
    End Sub

    Private Sub btnCaseFeeds_Click(sender As Object, e As EventArgs) Handles btnCaseFeeds.Click
        LoadControl(New UC_CaseFeeds())
    End Sub

    ' =================================================================
    ' MENUSTRIP EVENTS
    ' =================================================================
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        pollTimer.Stop()
        SessionManager.EndSession()
        Dim login As New Login()
        login.Show()
        Me.Close()
    End Sub

    Private Sub AllUserAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUserAccountToolStripMenuItem.Click
        LoadControl(New UC_AllUserAccount())
    End Sub

    Private Sub STATISTICSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATISTICSToolStripMenuItem.Click
        LoadControl(New UC_Statistics())
    End Sub

    Private Sub pbxNotify_Click(sender As Object, e As EventArgs) Handles pbxNotify.Click
        ' Clicking the notification bell navigates straight to Case Feeds
        LoadControl(New UC_CaseFeeds())
        ' Force UI button state update
        NavButtonStyles.InitializeNavButtons(Me, New List(Of Button) From {btnNoUse, btnCreateZones, btnCreateAccount, btnCreateCrimeCategory, btnCaseFeeds}, btnCaseFeeds)
    End Sub

    ' =================================================================
    ' NOTIFICATION ENGINE (Controlled Polling)
    ' =================================================================
    Private Sub pollTimer_Tick(sender As Object, e As EventArgs) Handles pollTimer.Tick
        RefreshNotifications()
    End Sub

    ' =================================================================
    ' NOTIFICATION ENGINE (Strictly Actionable Alerts)
    ' =================================================================
    Private Sub RefreshNotifications()
        ' Fetch ONLY actionable unassigned cases. Analytics do not belong in alert badges.
        Dim queryUnassigned As String = "SELECT COUNT(case_id) FROM cases WHERE status = 'NEW'"

        Try
            Using conn As MySqlConnection = Database.CreateOpenConnection()
                Using cmdUnassigned As New MySqlCommand(queryUnassigned, conn)
                    Dim unassignedCount As Integer = Convert.ToInt32(cmdUnassigned.ExecuteScalar())

                    If unassignedCount > 0 Then
                        lblNotify.Text = unassignedCount.ToString()
                        lblNotify.Visible = True ' Ensure badge is visible when there is work to do

                        Dim tooltipMessage As String = $"{unassignedCount} New Case(s) Awaiting Dispatch"
                        adminToolTip.SetToolTip(pbxNotify, tooltipMessage)
                        adminToolTip.SetToolTip(lblNotify, tooltipMessage)
                    Else
                        lblNotify.Visible = False ' Hide the badge completely when the queue is clear
                        adminToolTip.SetToolTip(pbxNotify, "Dispatch queue is clear. No pending alerts.")
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            lblNotify.Visible = True
            lblNotify.Text = "!"
            adminToolTip.SetToolTip(pbxNotify, "Database connection lost. Cannot refresh dispatch queue.")
        End Try
    End Sub

    ' =================================================================
    ' CLEANUP
    ' =================================================================
    Private Sub AdminDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        pollTimer.Stop()
        If Application.OpenForms.Count = 0 Then
            Application.Exit()
        End If
    End Sub

    Private Sub PanelWithUC_Paint(sender As Object, e As PaintEventArgs) Handles PanelWithUC.Paint

    End Sub

    Private Sub btnNoUse_Click(sender As Object, e As EventArgs) Handles btnNoUse.Click

    End Sub
End Class