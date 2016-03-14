Imports System.Data.SqlClient

Module sql
    Public con As SqlConnection
    Public Function connect()
        Try
            con = New SqlConnection("Server=VISHWAS-NAVADA;Database=leavemanagement;Trusted_Connection=True;")
            con.open()


        Catch ex As Exception
            MsgBox("Error connecting to database", MsgBoxStyle.Critical, "Server Error")
        End Try
        Return Nothing
    End Function
End Module
