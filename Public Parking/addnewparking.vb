﻿Public Class addnewparking

    Dim cnn As New OleDb.OleDbConnection
    Private Sub Addnewparking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        connection()
        addcombo()
        clear()
        parkingid()

    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        Button1.BackColor = Color.FromArgb(255, 102, 0)
        Button1.ForeColor = Color.FromKnownColor(KnownColor.Control)

    End Sub
    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        Button2.BackColor = Color.FromArgb(255, 102, 0)
        Button2.ForeColor = Color.FromKnownColor(KnownColor.Control)

    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackColor = Color.FromKnownColor(KnownColor.Control)
        Button1.ForeColor = Color.FromArgb(255, 102, 0)
    End Sub
    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.BackColor = Color.FromKnownColor(KnownColor.Control)
        Button2.ForeColor = Color.FromArgb(255, 102, 0)
    End Sub


    Private Sub TextBox5_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox5.MouseClick
        Close()
        addparking.Close()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        TextBox1.Focus()

    End Sub
    Public Sub connection()
        cnn = New OleDb.OleDbConnection
        cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Local Disk D\Degree\Year 1\Vehical Parking System Project\Project\Database\Public Parking.accdb"

    End Sub
    Private Sub addcombo()

        'connect database
        connection()
        cnn.Open()

        Dim SQLQuery = ""
        SQLQuery = "SELECT category FROM vehicle"
        Dim cm As New OleDb.OleDbCommand(SQLQuery, cnn)
        Dim dr As OleDb.OleDbDataReader = cm.ExecuteReader


        'add items to combo box
        While dr.Read
            ComboBox1.Items.Add(dr(0).ToString)
        End While

        cnn.Close()
        'add default as first row in database
        Me.ComboBox1.Text = Me.ComboBox1.Items(0).ToString

    End Sub
    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        'here now can not type
        e.Handled = True

    End Sub
    Private Sub parkingid()

        connection()
        cnn.Open()

        Dim dapid As New OleDb.OleDbDataAdapter("SELECT * from parking ORDER BY parkingid", cnn)
        Dim dtpid As New DataTable
        dapid.Fill(dtpid)

        Dim dap1 As New OleDb.OleDbDataAdapter("SELECT count(parkingid) from parking", cnn)
        Dim dtp1 As New DataTable
        dap1.Fill(dtp1)

        Label9.Text = "PID0" & dtp1.Rows(0).Item(0) + 1
        cnn.Close()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox6.Text = Date.Now.ToShortDateString()
        TextBox7.Text = Date.Now.ToLongTimeString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'check if all fields are completly fill
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.ComboBox1.Text = "Select" Or Me.TextBox3.Text = "" Or Me.TextBox4.Text = "" Then
            MessageBox.Show("Please complete the required fields.", "Authentication Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            connection()
            If Not cnn.State = ConnectionState.Open Then
                cnn.Open()
            End If
            'query to add data
            Dim SQLQuery1 = ""
            SQLQuery1 = "INSERT INTO parking ([parkingid],[name],[telephone],[category],[vehicleno],[slotid],[date],[starttime],[endtime],[booking],[exsit]) VALUES ('" & Me.Label9.Text & "', '" & Me.TextBox1.Text & "', '" & Me.TextBox2.Text & "', '" & Me.ComboBox1.Text & "', '" & Me.TextBox3.Text & "','" & Me.TextBox4.Text & "','" & Me.TextBox6.Text & "','" & Me.TextBox7.Text & "','12:00:00 PM','NO','NO')"

            Dim cmd1 As New OleDb.OleDbCommand(SQLQuery1, cnn)

            Try
                cmd1.ExecuteNonQuery()
                MessageBox.Show("Successfully Added", "Successfully Operation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                parkingid()
                clear()
                cnn.Close()

            Catch ex As Exception
                MessageBox.Show("Error Database Connection", "Authentication Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                clear()
            End Try
        End If
    End Sub
    Private Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox1.Text = "Select"

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clear()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        priselist.Show()
    End Sub
End Class