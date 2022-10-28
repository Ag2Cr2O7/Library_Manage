Imports System.Data.SqlClient
Module Module1
    Public f_1 As New Form1
    Public f_2 As New Form2
    Public myConn As SqlConnection =
        New SqlConnection("Data Source=(local); Integrated Security=SSPI; Database=图书馆;")
    Public _password As String '密码
    Public _account As String  '账号

End Module

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.BackColor = System.Drawing.Color.Transparent
        Label2.BackColor = System.Drawing.Color.Transparent
        Label3.BackColor = System.Drawing.Color.Transparent
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mySelectQuery As String = "Select * FROM 读者 WHERE 借书证号=" & TextBox1.Text & " and 密码=" & TextBox2.Text & ""
        Dim myCommand As New SqlCommand(mySelectQuery, myConn)
        myConn.Open()
        Dim myReader As SqlDataReader
        myReader = myCommand.ExecuteReader()
        If myReader.HasRows = False Then
            MsgBox("账号或密码错误！")
        Else
            myReader.Read()
            _account = Me.TextBox1.Text.ToString
            _password = Me.TextBox2.Text.ToString
            f_2.Show()
            Me.Hide()
        End If
        myReader.Close()
        myConn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form3.Show()
        Me.Hide()
    End Sub
End Class

