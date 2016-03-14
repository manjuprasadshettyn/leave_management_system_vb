Imports System.Data.SqlClient
Public Class reset
    Dim query As String

    Private Sub reset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        TextBox1.Text = login.TextBox1.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim up As SqlCommand
        query = "update logincredentials set password='" & TextBox3.Text & "' where username='" & login.TextBox1.Text & "' and password='" & TextBox2.Text & "'"
        Try
            up = New SqlCommand(query, con)
            up.ExecuteNonQuery()
            MsgBox("Password Changed Successfully", MsgBoxStyle.Information, "Password Change")
            Me.Close()
        Catch ex As Exception
            MsgBox("Current Password is invalid", MsgBoxStyle.Critical, "Error !")
        End Try
      

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class