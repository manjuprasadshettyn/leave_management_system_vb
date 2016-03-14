Imports System.Data.SqlClient

Public Class login

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username, password As String

        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Fill all the fields", MsgBoxStyle.Exclamation, "Login")
            Exit Sub
        End If

        username = TextBox1.Text
        password = TextBox2.Text

        Dim adap As SqlDataAdapter
        Dim dt As DataTable

        Dim query As String
        query = "select username from logincredentials where username='" & username & "' and binary_checksum(password)=binary_checksum('" & password & "')"
       
        Try
            adap = New SqlDataAdapter(query, con)
            dt = New DataTable
            adap.Fill(dt)

            If dt.Rows.Count = 0 Then
                MsgBox("Invalid Login Credentials", MsgBoxStyle.Critical, "Error")
                TextBox1.Focus()
                Exit Sub
            End If
            Dim adap2 As SqlDataAdapter
            Dim dt2 As DataTable
            query = "select designation from employee where empid= '" & username & "'"
            adap2 = New SqlDataAdapter(query, con)
            dt2 = New DataTable
            adap2.Fill(dt2)
            If dt2.Rows(0).Item(0) = "Principal" Then 'check if principal
                principal.Show()
                Me.Hide()
                Exit Sub
            End If
            If dt2.Rows(0).Item(0) = "Admin" Then 'check if admin
                admin.Show()
                Me.Hide()
                Exit Sub
            End If
            'if not then check for HOD
            Dim adap1 As SqlDataAdapter
                    Dim dt1 As DataTable
                    query = "select hodid from department where hodid= '" & username & "'"
                    adap1 = New SqlDataAdapter(query, con)
                    dt1 = New DataTable
                adap1.Fill(dt1)

            If dt1.Rows.Count = 1 Then
                hod.Show() 'redirect to HOD page
                Me.Hide()
                Exit Sub

            Else 'goto user page
                Me.Hide()
                user.Show()

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try

    End Sub


    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
    End Sub

  
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

End Class
