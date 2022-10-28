Imports System.Data.SqlClient
Module Module3
    Public f_3 As New Form3
    Public f_4 As New Form4
    Public myConn3 As SqlConnection =
        New SqlConnection("Data Source=(local); Integrated Security=SSPI; Database=图书馆;")
    Public _gpassword As String '密码
    Public _gaccount As String  '账号

End Module
Public Class Form3
    Dim mybindres As New BindingSource
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mySelectQuery As String = "Select * FROM 管理员 WHERE 工号=" & TextBox1.Text & " and 密码=" & TextBox2.Text & ""
        Dim myCommand As New SqlCommand(mySelectQuery, myConn3)
        myConn3.Open()
        Dim gmyReader As SqlDataReader
        gmyReader = myCommand.ExecuteReader()
        If gmyReader.HasRows = False Then
            MsgBox("账号或密码错误！")
        Else
            gmyReader.Read()
            _gaccount = Me.TextBox1.Text.ToString
            _gpassword = Me.TextBox2.Text.ToString
            f_4.Show()
            Me.Hide()
        End If
        gmyReader.Close()
        myConn3.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Form1.Show()

    End Sub
End Class