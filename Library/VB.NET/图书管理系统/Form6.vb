Public Class Form6
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBox1.Text.Trim <> "" And Me.TextBox1.Text.Trim <> "" And Me.TextBox1.Text = Me.TextBox2.Text Then
            Dim myconn As New SqlClient.SqlConnection("Server=localhost;Database=图书馆;Integrated Security=TRUE")
            Dim mysql As String = "UPDATE 读者 set 密码='" & Me.TextBox2.Text & "'where 借书证号='" & (_account) & "'"
            Dim mycmd As New SqlClient.SqlCommand(mysql, myconn)
            myconn.Open()
            Try
                mycmd.ExecuteNonQuery()
                MsgBox("修改密码成功！")
                Me.TextBox1.Text = ""
                Me.TextBox2.Text = ""
                Me.Hide()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            myconn.Close()
        Else
            MsgBox("两次密码输入不一致或输入为空！")
        End If
    End Sub
End Class