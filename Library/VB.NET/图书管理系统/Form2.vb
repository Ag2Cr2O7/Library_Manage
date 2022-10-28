Imports System.Data.SqlClient
Public Class Form2
    Dim ds As DataSet = New DataSet
    Public mybind As BindingManagerBase
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myconn As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
        Dim mysql As String = "SELECT 书号,书名,作者,出版社,出版时间,价格,总数量,剩余数量,位置,被借次数,类型.类型名 from 图书,类型 WHERE 图书.类型编号=类型.类型编号 
 and 书号 like '%" & TextBox1.Text.Trim & "%' and 书名 like '%" & TextBox2.Text.Trim & "%' and 类型名 like '%" & TextBox3.Text.Trim & "%' and 作者 like '%" & TextBox4.Text.Trim & "%'"
        '定义SQL语句模糊查找，其中%表示后任意的字符串
        Dim myadapter As New SqlDataAdapter(mysql, myconn)
        Dim mydataset As New DataSet
        myadapter.Fill(mydataset, "图书")
        DataGridView1.DataSource = mydataset.Tables("图书")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strconn As String = "Server=localhost;Database=图书馆;Integrated Security=SSPI"
        Dim strsql As String = "select 借书证号,姓名,性别,学院,借阅次数 from 读者 where 借书证号='" & (_account) & "'"
        Dim myconnect As SqlConnection = New SqlConnection(strconn)
        Dim mycommand As SqlDataAdapter = New SqlDataAdapter(strsql, myconnect)
        mycommand.Fill（ds, "读者"）
        mybind = Me.BindingContext(ds, "读者")
        TextBox5.DataBindings.Add("Text", ds, "读者.借书证号")
        TextBox6.DataBindings.Add("Text", ds, "读者.姓名")
        TextBox7.DataBindings.Add("Text", ds, "读者.性别")
        TextBox8.DataBindings.Add("Text", ds, "读者.学院")
        TextBox9.DataBindings.Add("Text", ds, "读者.借阅次数")
        Button2.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Application.Exit()
        Me.Hide()
        Form1.Show()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim myconn As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
        Dim mysql As String = "SELECT 图书.书号,书名,作者,出版社,出版时间,借出日期,应归还日期 from 图书,借阅 WHERE 图书.书号=借阅.书号
 and 归还日期 is NULL and 借书证号 like '%" & (_account) & "%'"
        Dim myadapter As New SqlDataAdapter(mysql, myconn)
        Dim mydataset As New DataSet
        myadapter.Fill(mydataset, "图书")
        DataGridView2.DataSource = mydataset.Tables("图书")

        Dim myconn1 As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
        Dim mysql1 As String = "SELECT 图书.书号,书名,作者,出版社,出版时间,借出日期,归还日期 from 图书,借阅 WHERE 图书.书号=借阅.书号
 and 归还日期 is NOT NULL and 借书证号 like '%" & (_account) & "%'"
        Dim myadapter1 As New SqlDataAdapter(mysql1, myconn1)
        Dim mydataset1 As New DataSet
        myadapter1.Fill(mydataset1, "图书")
        DataGridView3.DataSource = mydataset1.Tables("图书")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)



    End Sub

    Private Sub TabPage1_Click_1(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Form6.Show()
    End Sub
End Class