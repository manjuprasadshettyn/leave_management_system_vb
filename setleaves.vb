Imports System.Data.SqlClient

Public Class setleaves
    Dim ada As SqlDataAdapter
    Dim dt As DataTable = New DataTable
    Dim query As String
    Private Sub setleaves_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim empid As String = TextBox1.Text
        Dim cl As Integer = TextBox2.Text
        Dim el As Integer = TextBox3.Text
        Dim va As Integer = TextBox4.Text
        Dim hf As Integer = TextBox5.Text
        Try

            query = "insert into leaves values('" & empid & "','CL','" & cl & "','" & cl & "')"
            Dim sqlcmd As SqlCommand
            sqlcmd = New SqlCommand(query, con)
            sqlcmd.ExecuteNonQuery()

            query = "insert into leaves values('" & empid & "','EL','" & el & "','" & el & "')"
            Dim sqlcmd1 As SqlCommand
            sqlcmd1 = New SqlCommand(query, con)
            sqlcmd1.ExecuteNonQuery()

            query = "insert into leaves values('" & empid & "','VACATION','" & va & "','" & va & "')"
            Dim sqlcmd2 As SqlCommand
            sqlcmd2 = New SqlCommand(query, con)
            sqlcmd2.ExecuteNonQuery()

            query = "insert into leaves values('" & empid & "','HALF PAID','" & hf & "','" & hf & "')"
            Dim sqlcmd3 As SqlCommand
            sqlcmd3 = New SqlCommand(query, con)
            sqlcmd3.ExecuteNonQuery()

            MsgBox("Leaves Updated", MsgBoxStyle.Information, "Leave Management")

        Catch ex As Exception
            MsgBox("Error Occurred " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim empid As String = TextBox1.Text
        Dim cl As Integer = TextBox2.Text
        Dim el As Integer = TextBox3.Text
        Dim va As Integer = TextBox4.Text
        Dim hf As Integer = TextBox5.Text
        Try

            query = "update leaves set numberofdays='" & cl & "',remaining='" & cl & "' where empid='" & empid & "' and leavetype='CL' "
            Dim sqlcmd As SqlCommand
            sqlcmd = New SqlCommand(query, con)
            sqlcmd.ExecuteNonQuery()

            query = "update leaves set numberofdays='" & el & "',remaining='" & el & "' where empid='" & empid & "' and leavetype='EL' "
            Dim sqlcmd1 As SqlCommand
            sqlcmd1 = New SqlCommand(query, con)
            sqlcmd1.ExecuteNonQuery()

            query = "update leaves set numberofdays='" & va & "',remaining='" & va & "' where empid='" & empid & "' and leavetype='VACATION' "
            Dim sqlcmd2 As SqlCommand
            sqlcmd2 = New SqlCommand(query, con)
            sqlcmd2.ExecuteNonQuery()

            query = "update leaves set numberofdays='" & hf & "',remaining='" & hf & "' where empid='" & empid & "' and leavetype='HALF PAID' "
            Dim sqlcmd3 As SqlCommand
            sqlcmd3 = New SqlCommand(query, con)
            sqlcmd3.ExecuteNonQuery()

            MsgBox("Leaves Updated", MsgBoxStyle.Information, "Leave Management")

        Catch ex As Exception
            MsgBox("Error Occurred " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try

    End Sub
End Class