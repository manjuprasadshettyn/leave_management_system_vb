Imports System.Data.SqlClient


Public Class leaveapply
    Dim ada As SqlDataAdapter
    Dim dt As DataTable = New DataTable
    Dim query As String
    Dim n As Integer = 0
    Private Sub leave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        Try

            query = "select count(*) from leavedetails"
            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            n = dt.Rows(0).Item(0)

        Catch ex As Exception
            MsgBox("Error Occurred", MsgBoxStyle.Critical, "Server Error")
        End Try

        Dim today As Date = Date.Now()
        Dim leaveid As String = "LEAVE" & today.ToString("MMdd") & (00000 + n + 1)
        TextBox1.Text = leaveid
        TextBox2.Text = login.TextBox1.Text
        DateTimePicker1.MinDate = today.AddDays(1)
        DateTimePicker2.MinDate = today.AddDays(1)

        Try
            Dim n As Integer
            query = "select leavetype from leaves where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            Dim dat As DataTable = New DataTable
            Dim leavetype As String
            ada.Fill(dat)
            n = dat.Rows.Count

            For i = 0 To n - 1
                leavetype = dat.Rows(i).Item(0)
                ComboBox1.Items.Add(leavetype)
            Next

        Catch ex As Exception
            MsgBox("Unexpected error occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.SelectedItem = "" Then
            MsgBox("Please enter all data", MsgBoxStyle.Exclamation, "Form Validation")
            Exit Sub
        End If
        If DateTimePicker1.Value > DateTimePicker2.Value Or DateTimePicker1.Value = DateTimePicker2.Value Then
            MsgBox("Please enter valid dates for leave application", MsgBoxStyle.Exclamation, "Form Validation")
            Exit Sub
        End If

        Dim leave As String = TextBox1.Text
        Dim emp As String = TextBox2.Text
        Dim startdate As Date = DateTimePicker1.Value.Date
        Dim enddate As Date = DateTimePicker2.Value.Date
        Dim leavetype As String = ComboBox1.SelectedItem

        Dim k As Integer
        Dim comp As String
        Dim dt1 As DataTable = New DataTable
        Dim reason As String = TextBox3.Text
        Dim status As Integer = 0
        Dim query As String
        Try
            If leavetype <> "OTHER" Then

                comp = "select remaining from leaves where empid = '" & emp & "' and leavetype= '" & leavetype & "' "
                ada = New SqlDataAdapter(comp, con)
                ada.Fill(dt1)
                k = dt1.Rows(0).Item(0)

                If k < DateDiff(DateInterval.Day, startdate, enddate) Then
                    MsgBox("you cannot apply for leave in this section", MsgBoxStyle.Exclamation, "Leave Management")
                    Exit Sub
                End If
            End If
            query = "insert into leavedetails values('" & leave & "','" & emp & "','" & startdate & "','" & enddate & "','" & leavetype & "','" & reason & "','" & status & "')"
            Dim sqlcmd As SqlCommand
            sqlcmd = New SqlCommand(query, con)
            sqlcmd.ExecuteNonQuery()


            MsgBox("Leave Application is Submitted", MsgBoxStyle.Information, "Leave Management")

        Catch ex As Exception
            MsgBox("Error Occurred " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try

        Dim adap1 As SqlDataAdapter
        Dim dt2 As DataTable
        query = "select hodid from department where hodid= '" & login.TextBox1.Text & "'"
        adap1 = New SqlDataAdapter(query, con)
        dt2 = New DataTable
        adap1.Fill(dt2)

        If dt2.Rows.Count = 1 Then
            hod.Close()
            Me.Close()
            hod.Show()

        Else
            user.Close()
            Me.Close()
            user.Show()

        End If




    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

End Class