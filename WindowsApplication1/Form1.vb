Imports ADODB

Public Class Form1
    Dim cn As New ADODB.Connection
    Dim rs As New ADODB.Recordset

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cn.ConnectionString = "Provider=SQLNCLI11;Server=192.168.0.1;Database=AN_SUMATRA;Uid=itdevelopment;Pwd=itdevelopment2015"
            cn.Open()

            rs = cn.Execute("SELECT [code] FROM [AN_SUMATRA].[dbo].[OT_t_test_01] ORDER BY [code] ASC")
            If ((rs.EOF = False) And (rs.BOF = False)) = True Then
                While Not rs.EOF
                    cboItemCode.Items.Add(rs(0).Value.ToString)
                    rs.MoveNext()
                End While
            End If

            Me.Show()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Private Sub cboItemCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItemCode.SelectedIndexChanged
        Try
            rs = cn.Execute("SELECT TOP 1 [description] FROM [AN_SUMATRA].[dbo].[OT_t_test_01] WHERE [code]='" & cboItemCode.Text.ToString & "' ORDER BY [code] ASC")
            If (rs.EOF = False) And (rs.BOF = False) Then
                txtItemDesc.Text = rs(0).Value.ToString
            Else
                txtItemDesc.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
        
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
           

            Dim sqlInsert As String = "INSERT INTO [AN_SUMATRA].[dbo].[OT_t_test_02] ([code],[description],[qtykg],[qtyfish],[size],[inserttime]) VALUES "
            sqlInsert = sqlInsert & "('" & cboItemCode.Text.ToString & "'" & _
                                    ",'" & txtItemDesc.Text & "'" & _
                                    ",'" & txtQtyKg.Text & "'" & _
                                    ",'" & txtFishTotal.Text & "'" & _
                                    ",'" & Math.Round(((Val(txtQtyKg.Text) / Val(txtFishTotal.Text)) * 1000), 0) & "'" & _
                                    ", " & "GETDATE()" & " " & ")"
            cn.Execute(sqlInsert)

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
