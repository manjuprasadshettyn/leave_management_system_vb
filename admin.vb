Imports System.Data.SqlClient
Public Class admin
    Dim ada As SqlDataAdapter
    Dim dt As DataTable = New DataTable
    Dim dept, query As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        details.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        setleaves.Show()
    End Sub

    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        Try
            query = "select empid,ename,email from employee where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            Label1.Text = dt.Rows(0).Item(1) & "!"
            Label5.Text = dt.Rows(0).Item(0)
            Label6.Text = dt.Rows(0).Item(2)
        Catch ex As Exception
            MsgBox("Unexpected error occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        reset.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        login.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        report.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        report1.Show()
    End Sub

    Private Sub RectangleShape2_Click(sender As Object, e As EventArgs) Handles RectangleShape2.Click

    End Sub
End Class