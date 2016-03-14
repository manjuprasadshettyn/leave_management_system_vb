Imports System.Data.SqlClient

Public Class details
    Dim sqld As SqlDataAdapter
    Dim dat As DataTable
    Dim query, dept As String

    Private Sub details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        MaximizeBox = False

        Try  
            Dim n As Integer
        query = "select deptcode from department"
        sqld = New SqlDataAdapter(query, con)
        dat = New DataTable
            sqld.Fill(dat)
            n = dat.Rows.Count

            For i = 0 To n - 1
                dept = dat.Rows(i).Item(0)
                ComboBox1.Items.Add(dept)
            Next
        Catch ex As Exception
            MsgBox("Unexpected error occured: " & ex.Message, MsgBoxStyle.Critical, "Server Error")

        End Try
    End Sub

    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        admin.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.SelectedItem = "" Then
            MsgBox("Please fill all the fields", MsgBoxStyle.Information, "Form Validation")
            Exit Sub
        End If
        Dim ada As SqlDataAdapter
        Dim dt As DataTable = New DataTable
        Dim query As String
        Dim n As Integer = 0
        dept = ComboBox1.SelectedItem
        Try

            query = "select count(*),dept from employee where dept='" & dept & "' group by dept"
            ada = New SqlDataAdapter(query, con)
            ada.Fill(dt)
            n = dt.Rows(0).Item(0)

        Catch ex As Exception
            MsgBox("Error Occurred", MsgBoxStyle.Critical, "Srver Error")
        End Try
        Dim name, id, email, desig As String
        Dim dob, jd As Date
        name = TextBox1.Text
        dob = DateTimePicker2.Value.Date
        email = TextBox3.Text
        desig = TextBox4.Text
        dept = ComboBox1.SelectedItem
        Dim dept2 As String = dept.Substring(0, dept.IndexOf(" ")) 'remove extra space

        jd = DateTimePicker1.Value.Date
        If RadioButton1.Checked Then
            If n < 9 Then
                id = "TEMP" & dept2 & "000" & (n + 1)
            ElseIf n < 99 Then
                id = "TEMP" & dept2 & "00" & (n + 1)
            Else
                id = "TEMP" & dept2 & "0" & (n + 1)
            End If

        ElseIf RadioButton2.Checked Then
            If n < 9 Then
                id = "NTEMP" & dept2 & "00" & (n + 1)
            ElseIf n < 99 Then
                id = "NTEMP" & dept2 & "0" & (n + 1)
            Else
                id = "NTEMP" & dept2 & (n + 1)
            End If

        Else
            MsgBox("Please select the employment type", MsgBoxStyle.Information, "Form Validation")
            Exit Sub
        End If
        
        Try
            query = "insert into employee values('" & id & "','" & name & "','" & email & "','" & desig & "','" & dept & "','" & dob & "','" & jd & "' )"
            Dim insert As SqlCommand
            insert = New SqlCommand(query, con)
            insert.ExecuteNonQuery()
            query = "insert into logincredentials values('" & id & "','password')"

            insert = New SqlCommand(query, con)
            insert.ExecuteNonQuery()
            MsgBox("Employee  Added " & Environment.NewLine & "Emp ID: " & id & Environment.NewLine & "Password: password", MsgBoxStyle.Information, "Success")
            Me.Close()
        Catch ex As Exception
            MsgBox("An Error Occured: " & ex.Message, MsgBoxStyle.Critical, "Error !")
        End Try

    End Sub
End Class