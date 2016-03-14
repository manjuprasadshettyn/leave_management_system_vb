Imports System.Data.SqlClient
Public Class principal

    Dim ada As SqlDataAdapter
    Dim dt As DataTable = New DataTable
    Dim dept, query As String
    Private Sub uinter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        action.flag = 1
        Try
            query = "select empid,ename,email,day(dob),month(dob) from employee where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            Label1.Text = dt.Rows(0).Item(1) & "!"
            Label5.Text = dt.Rows(0).Item(0)
            Label6.Text = dt.Rows(0).Item(2)
            Dim dte, mon As Integer
            dte = dt.Rows(0).Item(3)
            mon = dt.Rows(0).Item(4)
            Dim today As Date = Date.Now()
            If dte = today.Day And mon = today.Month Then
                Label15.Text = "Happy Birthday !"
            End If

            Dim n As Integer
            query = "select deptcode from department"
            ada = New SqlDataAdapter(query, con)
            Dim dat As New DataTable
            ada.Fill(dat)
            n = dat.Rows.Count

            For i = 0 To n - 1
                dept = dat.Rows(i).Item(0)
                ComboBox1.Items.Add(dept)
            Next
        Catch ex As Exception
            MsgBox("Unexpected error occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")

        End Try
        Try
            query = "select empid,ename,email from employee where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            Label1.Text = dt.Rows(0).Item(1) & "!"
            'Label5.Text = dt.Rows(0).Item(0)
            'Label6.Text = dt.Rows(0).Item(2)
            query = "select leaveid as [Leave ID],ename as Name,dept as [Department], DATEDIFF(day,leavestartdate,leaveenddate) as [No. Of Days] from employee,leavedetails where employee.empid=leavedetails.empid and status = 1"
            ada = New SqlDataAdapter(query, con)
            Dim ds As New DataSet
            ada.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")
        End Try

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)
        login.Show()
        Me.Close()
    End Sub

 

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If action.valu = "" Then
            MsgBox("Select a record form above table", MsgBoxStyle.Exclamation, "Leave Mangement")
            Exit Sub
        End If
        action.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        action.valu = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString 'get selected row's first column value i.e leaveid 
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            dept = ComboBox1.SelectedItem.ToString
            query = "select leaveid as [Leave ID],ename as Name,dept as [Department], DATEDIFF(day,leavestartdate,leaveenddate) as [No. Of Days] from employee,leavedetails where employee.empid=leavedetails.empid and status = 1 and dept='" & dept & "'"
            ada = New SqlDataAdapter(query, con)
            Dim ds As New DataSet
            ada.Fill(ds)
            If ds.Tables(0).Rows.Count = 0 Then
                DataGridView1.DataSource = ds.Tables(0)
                MsgBox("No Leaves to Display")
                Exit Sub
            End If
            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        reset.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        login.Show()
        Me.Close()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Try
            Dim days As Integer = Val(NumericUpDown1.Value)
            query = "select leaveid as [Leave ID],ename as Name,dept as [Department], DATEDIFF(day,leavestartdate,leaveenddate) as [No. Of Days] from employee,leavedetails where employee.empid=leavedetails.empid and status = 1 and DATEDIFF(day,leavestartdate,leaveenddate)>=" & days & " "
            ada = New SqlDataAdapter(query, con)
            Dim ds As New DataSet
            ada.Fill(ds)
            If ds.Tables(0).Rows.Count = 0 Then
                DataGridView1.DataSource = ds.Tables(0)
                MsgBox("No Leaves to Display")
                Exit Sub
            End If
            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message)
        End Try
    End Sub
End Class