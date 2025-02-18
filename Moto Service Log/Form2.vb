﻿Imports System.Data.SqlClient
Public Class form_dashboard

    'Database Connection String
    Dim connectionString As String = "Data Source=HOMEDESKTOP2452\SQLEXPRESS;Initial Catalog=Moto_Service_Log;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
    Private Sub form_dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    'Close Application
    Private Sub form_dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    'Shift Focus to Next Control on Enter Key Press
    Private Sub form_dashboard_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    'Registration Tab Code:

    'Function to Load Data into DataGridView from Registration Table
    Private Sub Load_RegTab_Data()
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand()
                cmd.Connection = conn

                'Selecting all details from Registration Table
                cmd.CommandText = "select * from Registration_Table"
                Dim datareader1 As SqlDataReader = cmd.ExecuteReader()

                'Displaying details in DataGridView
                Dim datatable1 As New DataTable()
                datatable1.Load(datareader1)
                dgv_regtab.DataSource = datatable1
                datareader1.Close()

            Catch ex As Exception
                MessageBox.Show("Error:" & ex.Message)
            End Try
        End Using
    End Sub

    'Register Button: User and Bike Registration
    Private Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click

        'Check if Registration Number is Empty or Zero and all other details are entered
        If txtbox_regno.Text = "" Or txtbox_regno.Text = "0" Or txtbox_custname.Text = "" Or txtbox_phoneno.Text = "" Or txtbox_address.Text = "" Or txtbox_email.Text = "" Or txtbox_make.Text = "" Or txtbox_model.Text = "" Or txtbox_yearofmfd.Text = "" Then
            MessageBox.Show("Please enter valid Registration Number and Details!")
            Return
        Else Using conn As New SqlConnection(connectionString)
                Try
                    conn.Open()
                    Dim cmd As New SqlCommand()
                    cmd.Connection = conn

                    'Inserting details into Registration Table
                    cmd.CommandText = "insert into Registration_Table([regno],[custname],[phoneno],[address],[email],[make],[model],[yearofmfd]) values (@regno, @custname, @phoneno, @address, @email, @make, @model, @yearofmfd)"
                    cmd.Parameters.AddWithValue("@regno", txtbox_regno.Text)
                    cmd.Parameters.AddWithValue("@custname", txtbox_custname.Text)
                    cmd.Parameters.AddWithValue("@phoneno", txtbox_phoneno.Text)
                    cmd.Parameters.AddWithValue("@address", txtbox_address.Text)
                    cmd.Parameters.AddWithValue("@email", txtbox_email.Text)
                    cmd.Parameters.AddWithValue("@make", txtbox_make.Text)
                    cmd.Parameters.AddWithValue("@model", txtbox_model.Text)
                    cmd.Parameters.AddWithValue("@yearofmfd", txtbox_yearofmfd.Text)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Rows inserted: " & rowsAffected)

                    Load_RegTab_Data()

                Catch ex As Exception
                    MessageBox.Show("Error:" & ex.Message)
                End Try
            End Using
        End If
    End Sub

    'Clear Button: Clear all Textboxes
    Private Sub btn_reg_clear_Click(sender As Object, e As EventArgs) Handles btn_reg_clear.Click
        txtbox_regno.Clear()
        txtbox_custname.Clear()
        txtbox_phoneno.Clear()
        txtbox_address.Clear()
        txtbox_email.Clear()
        txtbox_make.Clear()
        txtbox_model.Clear()
        txtbox_yearofmfd.Clear()
    End Sub

    'View Details Button: View all details from Registration Table
    Private Sub btn_reg_viewdetails_Click(sender As Object, e As EventArgs) Handles btn_reg_viewdetails.Click
        Load_RegTab_Data()
    End Sub

    'Delete Row Button: Delete selected row from Registration Table
    Private Sub btn_reg_deleterow_Click(sender As Object, e As EventArgs) Handles btn_reg_deleterow.Click

        'Check if any row is selected
        If dgv_regtab.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to delete!")
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete selected row?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand()
                cmd.Connection = conn

                'Deleting selected row from Registration Table
                cmd.CommandText = "delete from Registration_Table where regno = @regno"
                cmd.Parameters.AddWithValue("@regno", dgv_regtab.SelectedRows(0).Cells(0).Value)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                MessageBox.Show("Rows deleted: " & rowsAffected)

                Load_RegTab_Data()

            Catch ex As Exception
                MessageBox.Show("Error:" & ex.Message)
            End Try
        End Using
    End Sub

    'Edit Row Button: Edit selected row in Registration Table
    Private Sub btn_reg_editrow_Click(sender As Object, e As EventArgs) Handles btn_reg_editrow.Click

        'Check if any row is selected
        If dgv_regtab.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to edit!")
            Return
        End If

        If MessageBox.Show("Are you sure you want to edit selected row?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand()
                cmd.Connection = conn

                'Updating selected row in Registration Table
                cmd.CommandText = "update Registration_Table set custname = @custname, phoneno = @phoneno, address = @address, email = @email, make = @make, model = @model, yearofmfd = @yearofmfd where regno = @regno"
                cmd.Parameters.AddWithValue("@regno", txtbox_regno.Text)
                cmd.Parameters.AddWithValue("@custname", txtbox_custname.Text)
                cmd.Parameters.AddWithValue("@phoneno", txtbox_phoneno.Text)
                cmd.Parameters.AddWithValue("@address", txtbox_address.Text)
                cmd.Parameters.AddWithValue("@email", txtbox_email.Text)
                cmd.Parameters.AddWithValue("@make", txtbox_make.Text)
                cmd.Parameters.AddWithValue("@model", txtbox_model.Text)
                cmd.Parameters.AddWithValue("@yearofmfd", txtbox_yearofmfd.Text)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                MessageBox.Show("Rows updated: " & rowsAffected)

                Load_RegTab_Data()

            Catch ex As Exception
                MessageBox.Show("Error:" & ex.Message)
            End Try
        End Using

    End Sub

    'View selected row details in Textboxes
    Private Sub dgv_regtab_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_regtab.CellClick
        txtbox_regno.Text = dgv_regtab.SelectedRows(0).Cells(0).Value
        txtbox_custname.Text = dgv_regtab.SelectedRows(0).Cells(1).Value
        txtbox_phoneno.Text = dgv_regtab.SelectedRows(0).Cells(2).Value
        txtbox_address.Text = dgv_regtab.SelectedRows(0).Cells(3).Value
        txtbox_email.Text = dgv_regtab.SelectedRows(0).Cells(4).Value
        txtbox_make.Text = dgv_regtab.SelectedRows(0).Cells(5).Value
        txtbox_model.Text = dgv_regtab.SelectedRows(0).Cells(6).Value
        txtbox_yearofmfd.Text = dgv_regtab.SelectedRows(0).Cells(7).Value
    End Sub
End Class

' End of Registration Tab Code

