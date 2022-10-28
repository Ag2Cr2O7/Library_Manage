Public Class Form5
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myconn As New SqlClient.SqlConnection("Server=localhost;Database=图书馆;Integrated Security=TRUE")
        Dim mysql As String = "INSERT INTO 图书 VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "',0,'" & TextBox10.Text & "')"
        Dim mycmd As New SqlClient.SqlCommand(mysql, myconn)
        myconn.Open()
        Try
            mycmd.ExecuteNonQuery()
            MsgBox("录入成功！")
        Catch ex As Exception
            MsgBox("录入失败！")
        End Try

        myconn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub
End Class