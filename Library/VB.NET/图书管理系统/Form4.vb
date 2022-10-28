Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Runtime.InteropServices

Public Class Form4
    Dim ds As DataSet = New DataSet
    Public mybind As BindingManagerBase
    Public shaozi1 As Int16 = 0
    Public shaozi2 As Int16 = 0
    Public huanshushijian As New DateTime
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBox5.Text.Trim <> "" And Me.TextBox6.Text.Trim <> "" Then
            Dim myConn As SqlConnection = New SqlConnection("Data Source=(local); Integrated Security=SSPI; Database=图书馆;")
            '定义DateTime类型的借书日期和归还日期 
            Dim BorrowDate As New DateTime
            Dim ShouldReturnDate As New DateTime
            BorrowDate = Now
            ShouldReturnDate = DateAdd(DateInterval.Day, 30, BorrowDate)
            Dim strSql As String = "INSERT INTO 借阅(书号,借书证号,借出日期,应归还日期) VALUES ('" & TextBox5.Text & "', '" & TextBox6.Text & "','" & BorrowDate & "','" & ShouldReturnDate & "')"    '实现插入操作 
            Dim myconnStr As New SqlCommand(strSql, myConn)
            myConn.Open()
            Dim intResult = MessageBox.Show("确认借书", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            If intResult = DialogResult.OK Then
                Try
                    myconnStr.ExecuteScalar()
                    MsgBox("借书成功")
                Catch ex As Exception
                    MsgBox("借书失败")
                End Try
                myConn.Close()
            End If
        Else
            MsgBox("内容为空！")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            shaozi1 = 1
            Dim myconn As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
            Dim mysql As String = "SELECT 图书.书号,书名,作者,出版社,出版时间,借出日期,应归还日期 from 图书,借阅 WHERE 图书.书号=借阅.书号
 and 归还日期 IS NULL and 借书证号 like '%" & TextBox1.Text.Trim & "%'"
            Dim myadapter As New SqlDataAdapter(mysql, myconn)
            Dim mydataset As New DataSet
            myadapter.Fill(mydataset, "图书")
            DataGridView1.DataSource = mydataset.Tables("图书")

            Dim myconn1 As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
            Dim mysql1 As String = "SELECT 图书.书号,书名,作者,出版社,出版时间,借出日期,归还日期 from 图书,借阅 WHERE 图书.书号=借阅.书号
 and 归还日期 IS NOT NULL and 借书证号 like '%" & TextBox1.Text.Trim & "%'"
            Dim myadapter1 As New SqlDataAdapter(mysql1, myconn1)
            Dim mydataset1 As New DataSet
            myadapter1.Fill(mydataset1, "图书")
            DataGridView2.DataSource = mydataset1.Tables("图书")
        Else
            MsgBox("借书证号为空！")
        End If

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim BookNumber As String
        Try
            BookNumber = DataGridView2.Item("书号", DataGridView1.CurrentCell.RowIndex).Value
            Dim intResult As Integer
            Dim strConnection As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
            Dim mycmd As New SqlCommand
            Dim myconn As New SqlConnection(strConnection)
            Dim strSql As String = "DELETE FROM 借阅 WHERE 书号 = '" & BookNumber & "'"
            Dim myconnStr As New SqlCommand(strSql, myconn)
            myconn.Open()
            intResult = MessageBox.Show("确认删除", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            If intResult = DialogResult.OK Then
                Try
                    myconnStr.ExecuteScalar()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                myconn.Close()
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentCell.RowIndex)
                MsgBox("删除成功")
            End If
        Catch ex As Exception
            MsgBox("未选择要删除的数据行")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If shaozi1 = 1 And TextBox1.Text <> "" Then
            Try
                Dim BookNumber As String
                BookNumber = DataGridView2.Item("书号", DataGridView2.CurrentCell.RowIndex).Value
                Dim intResult As Integer
                Dim strConnection As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
                Dim mycmd As New SqlCommand
                Dim myconn As New SqlConnection(strConnection)
                Dim strSql As String = "DELETE FROM 借阅 WHERE 书号 = '" & BookNumber & "' And 借出日期 = '" & DataGridView1.CurrentRow.Cells(5).Value & "'"
                Dim myconnStr As New SqlCommand(strSql, myconn)
                myconn.Open()
                intResult = MessageBox.Show("确认删除？", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                If intResult = DialogResult.OK Then
                    Try
                        myconnStr.ExecuteScalar()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    myconn.Close()
                    DataGridView2.Rows.RemoveAt(DataGridView2.CurrentCell.RowIndex)
                    MsgBox("删除成功！")
                End If
            Catch ex As Exception
                MsgBox("未选择要删除的数据行")
            End Try
        Else
            MsgBox("借书证号为空！")
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        shaozi2 = 1
        Dim myconn As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
        Dim mysql As String = "SELECT 书号,书名,作者,出版社,出版时间,价格,总数量,剩余数量,位置,被借次数,类型编号 from 图书 WHERE 
 书号 like '%" & TextBox2.Text.Trim & "%' and 书名 like '%" & TextBox3.Text.Trim & "%' and 作者 like '%" & TextBox4.Text.Trim & "%'"
        '定义SQL语句模糊查找，其中%表示后任意的字符串
        Dim myadapter As New SqlDataAdapter(mysql, myconn)
        Dim mydataset As New DataSet
        myadapter.Fill(mydataset, "图书")
        DataGridView3.DataSource = mydataset.Tables("图书")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If shaozi2 = 1 Then
            Dim myconnection As New SqlConnection("Server=localhost;Database=图书馆;Integrated Security = True;")
            Dim mycmd As String = "update 图书 set 书名='" & DataGridView3.CurrentRow.Cells(1).Value & "',作者='" & DataGridView3.CurrentRow.Cells(2).Value & "',出版社='" & DataGridView3.CurrentRow.Cells(3).Value & "',出版时间='" & DataGridView3.CurrentRow.Cells(4).Value & "',价格='" & DataGridView3.CurrentRow.Cells(5).Value & "',总数量='" & DataGridView3.CurrentRow.Cells(6).Value & "',剩余数量='" & DataGridView3.CurrentRow.Cells(7).Value & "',位置='" & DataGridView3.CurrentRow.Cells(8).Value & "'where 书号='" & DataGridView3.CurrentRow.Cells(0).Value & "'"
            Dim mycomm As New SqlCommand(mycmd, myconnection)
            myconnection.Open()
            mycomm.ExecuteNonQuery()
            MsgBox("修改成功！")
            myconnection.Close()
        Else
            MsgBox("数据为空！")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If shaozi2 = 1 Then
            Try
                'Dim BookNumber As String
                'BookNumber = DataGridView3.Item("书号", DataGridView3.CurrentCell.RowIndex).Value
                Dim intResult As Integer
                Dim strConnection As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
                Dim mycmd As New SqlCommand
                Dim myconn As New SqlConnection(strConnection)
                Dim strSql As String = "DELETE FROM 图书 WHERE 书号 = '" & DataGridView3.CurrentRow.Cells(0).Value & "'"
                Dim myconnStr As New SqlCommand(strSql, myconn)
                myconn.Open()
                intResult = MessageBox.Show("确认删除？", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                If intResult = DialogResult.OK Then
                    Try
                        myconnStr.ExecuteScalar()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    myconn.Close()
                    DataGridView3.Rows.RemoveAt(DataGridView3.CurrentCell.RowIndex)
                    MsgBox("删除成功！")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("数据为空！")
        End If



    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub TabPage2_Click_1(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage4_Click(sender As Object, e As EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim myconn As New SqlClient.SqlConnection("Server=localhost;Database=图书馆;Integrated Security=TRUE")
        Dim mysql As String = "INSERT INTO 读者 VALUES('" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox11.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "'," & Val(Me.TextBox14.Text) & ")"
        Dim mycmd As New SqlClient.SqlCommand(mysql, myconn)
        myconn.Open()
        Try
            mycmd.ExecuteNonQuery()
            MsgBox("插入成功！")
        Catch ex As Exception
            MsgBox("插入失败！")
        End Try

        myconn.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim myconn As New SqlClient.SqlConnection("Server=localhost;Database=图书馆;Integrated Security=TRUE")
        Dim mysql As String = "DELETE FROM 读者 where 借书证号 ='" & TextBox9.Text & "'"
        Dim mycmd As New SqlClient.SqlCommand(mysql, myconn)
        myconn.Open()
        Try
            mycmd.ExecuteNonQuery()
            MsgBox("删除成功！")
        Catch ex As Exception
            MsgBox("删除失败！")
        End Try
        myconn.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If Me.TextBox10.Text.Trim <> "" Then
            Dim myconn As New SqlClient.SqlConnection("Server=localhost;Database=图书馆;Integrated Security=TRUE")
            Dim mysql As String = "UPDATE 读者 set 密码='" & Me.TextBox10.Text & "',姓名='" & Me.TextBox11.Text & "',性别='" & Me.TextBox12.Text & "',学院='" & Me.TextBox13.Text & "',借阅次数=" & Val(Me.TextBox14.Text) & " where 借书证号='" & Me.TextBox9.Text & "'"
            Dim mycmd As New SqlClient.SqlCommand(mysql, myconn)
            myconn.Open()
            Try
                mycmd.ExecuteNonQuery()
                MsgBox("更新成功！")
            Catch ex As Exception
                MsgBox("更新失败！")
            End Try
            myconn.Close()
        Else
            MsgBox("密码为空！")
        End If

    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.TextBox7.Text.Trim <> "" And Me.TextBox8.Text.Trim <> "" Then
            Dim myConn1 As SqlConnection = New SqlConnection("Data Source=(local); Integrated Security=SSPI; Database=图书馆;")
            '定义DateTime类型的借书日期和归还日期 
            Dim ReturnDate As New DateTime
            ReturnDate = Now
            huanshushijian = ReturnDate
            Dim strSql1 As String = "UPDATE 借阅 SET 归还日期 = '" & ReturnDate & "' WHERE 书号 = '" & TextBox7.Text & "' And 借书证号 = '" & TextBox8.Text & "'"
            Dim myconnStr As New SqlCommand(strSql1, myConn1)
            myConn1.Open()
            Dim intResult1 = MessageBox.Show("确认还书", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            If intResult1 = DialogResult.OK Then
                Try
                    myconnStr.ExecuteScalar()
                    MsgBox("还书成功")
                Catch ex As Exception
                    MsgBox("还书失败")
                End Try
                myConn1.Close()
            End If
        Else
            MsgBox("内容为空！")
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Form5.Show()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim myconn As String = "Server=localhost;Database=图书馆;Integrated Security=TRUE"
        Dim mysql As String = "SELECT 借书证号,姓名,性别,学院,借阅次数 from 读者 WHERE 
 借书证号 like '%" & TextBox15.Text.Trim & "%' and 姓名 like '%" & TextBox16.Text.Trim & "%'"
        '定义SQL语句模糊查找，其中%表示后任意的字符串
        Dim myadapter As New SqlDataAdapter(mysql, myconn)
        Dim mydataset As New DataSet
        myadapter.Fill(mydataset, "图书")
        DataGridView4.DataSource = mydataset.Tables("图书")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If shaozi1 = 1 And TextBox1.Text <> "" Then
            Dim myConn1 As SqlConnection = New SqlConnection("Data Source=(local); Integrated Security=SSPI; Database=图书馆;")
            '定义DateTime类型的借书日期和归还日期 
            Dim ReturnDate As New DateTime
            ReturnDate = Now
            huanshushijian = ReturnDate
            Dim strSql1 As String = "UPDATE 借阅 SET 归还日期 = '" & ReturnDate & "' WHERE 书号 = '" & DataGridView1.CurrentRow.Cells(0).Value & "' And 借书证号 = '" & TextBox1.Text & "' And 借出日期 = '" & DataGridView1.CurrentRow.Cells(5).Value & "'"
            Dim myconnStr As New SqlCommand(strSql1, myConn1)
            myConn1.Open()
            Dim intResult1 = MessageBox.Show("确认还书", "取消", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            If intResult1 = DialogResult.OK Then
                Try
                    myconnStr.ExecuteScalar()
                    MsgBox("还书成功")
                Catch ex As Exception
                    MsgBox("还书失败")
                End Try
                myConn1.Close()
            End If
        Else
            MsgBox("借书证号为空！")
        End If
    End Sub
End Class