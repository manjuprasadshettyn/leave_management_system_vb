Imports System.Data.SqlClient

Public Class action
    Dim query As String
    ' Dim selected As String = hod.ListBox2.SelectedItem
    Public valu As String
    Public flag As Integer
    Dim leaveid As String
    Private Sub action_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        leaveid = valu
        Dim ada As SqlDataAdapter
        Dim dt As DataTable = New DataTable

        Try
            query = "select leavedetails.empid,ename,leavestartdate,leaveenddate,DATEDIFF(day,leavestartdate,leaveenddate) as total,leavetype,reason from employee, leavedetails where employee.empid=leavedetails.empid and leaveid='" & leaveid & "'"

            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            Label14.Text = dt.Rows(0).Item(0)
            Label12.Text = dt.Rows(0).Item(1)
            Label11.Text = dt.Rows(0).Item(2)
            Label10.Text = dt.Rows(0).Item(3)
            Label9.Text = dt.Rows(0).Item(4)
            Label8.Text = dt.Rows(0).Item(5)
            Label16.Text = dt.Rows(0).Item(6)

            query = "select remaining from leaves,leavedetails where leaves.empid=leavedetails.empid and leaves.leavetype=leavedetails.leavetype and leaveid='" & leaveid & "'"
            ada = New SqlDataAdapter(query, con)
            Dim dt2 As DataTable = New DataTable
            ada.Fill(dt2)
            If Label8.Text = "OTHER" Then
                Label6.Text = "Other Leaves Took:"
            End If
            Label7.Text = dt2.Rows(0).Item(0)
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        query = "update leavedetails set status=status+1 where leaveid = '" & leaveid & "'"


        Try
            Dim sql As SqlCommand = New SqlCommand(query, con)
            sql.ExecuteNonQuery()

            If flag = 1 Then
                If Label8.Text <> "OTHER" Then
                    Dim remainingnew As Integer
                    remainingnew = Val(Label7.Text) - Val(Label9.Text)
                    query = "update leaves set remaining=" & remainingnew & " where empid = '" & Label14.Text & "' and leavetype='" & Label8.Text & "'"
                    sql = New SqlCommand(query, con)
                    sql.ExecuteNonQuery()
                Else
                    query = "update leaves set remaining=remaining+ " & Val(Label9.Text) & ",numberofdays=numberofdays+" & Val(Label9.Text) & " where empid='" & Label14.Text & "' and leavetype='OTHER' "
                    sql = New SqlCommand(query, con)
                    sql.ExecuteNonQuery()
                End If
            End If

            MsgBox("Accepted ", MsgBoxStyle.Information, "Leave Application")
            If flag = 1 Then
                principal.Close()
                Me.Close()
                principal.Show()
            Else
                hod.Close()
                Me.Close()
                hod.Show()
            End If

        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        query = "update leavedetails set status=-1 where leaveid = '" & leaveid & "'"
        Try
            Dim sql As SqlCommand = New SqlCommand(query, con)
            sql.ExecuteNonQuery()
            MsgBox("Declined", MsgBoxStyle.Information, "Leave Application")
            If flag = 1 Then
                principal.Close()
                Me.Close()
                principal.Show()
            Else
                hod.Close()
                Me.Close()
                hod.Show()
            End If
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class