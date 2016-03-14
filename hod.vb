Imports System.Data.SqlClient

Public Class hod
    Dim ada As SqlDataAdapter
    Dim dt As DataTable = New DataTable
    Dim query As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False
        action.flag = 0
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
            query = "select leavetype,remaining from leaves where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            Dim dat As New DataTable
            ada.Fill(dat)
            For i As Integer = 0 To dat.Rows.Count - 1
                If dat.Rows(i).Item(0) = "EL" Then
                    Label7.Text = dat.Rows(i).Item(1)
                ElseIf dat.Rows(i).Item(0) = "CL" Then
                    Label13.Text = dat.Rows(i).Item(1)
                ElseIf dat.Rows(i).Item(0) = "HALF PAID" Then
                    Label18.Text = dat.Rows(i).Item(1)
                ElseIf dat.Rows(i).Item(0) = "VACATION" Then
                    Label20.Text = dat.Rows(i).Item(1)
                Else
                    Label22.Text = dat.Rows(i).Item(1)
                End If
            Next



            query = "select leaveid as [Leave ID],leavestartdate as [Leave Start Date],leaveenddate as [Leave End Date] from leavedetails where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            Dim ds As DataSet = New DataSet
            ada.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            Dim newcol As New DataGridViewTextBoxColumn
            newcol.ValueType = GetType(String)
            newcol.HeaderText = "Status"
            DataGridView1.Columns.Add(newcol)
            query = "select leaveid,status from leavedetails where empid='" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            Dim st As DataTable = New DataTable
            ada.Fill(st)
            For i As Integer = 0 To st.Rows.Count - 1
                If st.Rows(i).Item(1) = 0 Or st.Rows(i).Item(1) = 1 Then
                    DataGridView1.Rows(i).Cells(3).Value = "Pending"
                ElseIf st.Rows(i).Item(1) = -1 Then
                    DataGridView1.Rows(i).Cells(3).Value = "Declined"
                ElseIf st.Rows(i).Item(1) = 2 Then
                    DataGridView1.Rows(i).Cells(3).Value = "Aproved"

                End If
            Next

            DataGridView1.ClearSelection()


            query = "select leaveid as [Leave ID],ename as Name, DATEDIFF(day,leavestartdate,leaveenddate) as [No. of Days],leavetype as [Leave Type] from employee,leavedetails where employee.empid=leavedetails.empid and status = 0 and dept in (select deptcode from department where hodid='" & login.TextBox1.Text & "') " 'and leavedetails.empid!= '" & login.TextBox1.Text & "'"
            ada = New SqlDataAdapter(query, con)
            Dim ds2 As DataSet = New DataSet
            ada.Fill(ds2)
            DataGridView2.DataSource = ds2.Tables(0)
            DataGridView2.ClearSelection()
        Catch ex As Exception
            MsgBox("Error Occured: " & ex.Message)
        End Try
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        leaveapply.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If action.valu = "" Then
            MsgBox("Select a record form above table", MsgBoxStyle.Exclamation, "Leave Mangement")
            Exit Sub
        End If
        action.Show()
    End Sub

   
   

  
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        action.valu = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString 'get selected row's first column value i.e leaveid 
    End Sub

    

    Private Sub Button4_Click(sender As Object, e As EventArgs)

        Me.Close()
    End Sub

    
    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        reset.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        login.Show()
        Me.Close()
    End Sub
End Class