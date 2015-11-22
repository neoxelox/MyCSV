Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MetroGrid1.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        MetroGrid1.SelectionMode = DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        OpenFileDialog1.Filter = "CSV file | *.csv"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            MetroGrid1.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

            'CABECERA

            Dim CABECERA As String = IO.File.ReadLines(OpenFileDialog1.FileName)(0) 'PRIMERA LINEA DEL ARCHIVO COMO CABECERA
            Dim COLUMNAS As String() = CABECERA.Split(",")
            MetroGrid1.ColumnCount = COLUMNAS.Count
            For I = 0 To COLUMNAS.Count - 1
                MetroGrid1.Columns(I).Name = COLUMNAS(I)
            Next

            'RESTO DE FILAS
            For I = 1 To IO.File.ReadLines(OpenFileDialog1.FileName).Count - 1
                Dim FILA As String() = IO.File.ReadLines(OpenFileDialog1.FileName)(I).Split(",")
                MetroGrid1.Rows.Add(FILA)
            Next

        End If
    End Sub
    Sub alternarColoFilasDatagridview(ByVal dgv As MetroFramework.Controls.MetroGrid)
        Try
            With dgv
                .RowsDefaultCellStyle.BackColor = Color.LightBlue
                .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            End With
        Catch ex As Exception

        End Try

    End Sub

    Sub alternarColoFilasDatagridview2(ByVal dgv As MetroFramework.Controls.MetroGrid)
        Try
            With dgv
                .RowsDefaultCellStyle.BackColor = Color.White
                .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MetroToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroToggle1.CheckedChanged
        If MetroToggle1.Checked = True Then
            alternarColoFilasDatagridview(MetroGrid1)
        Else
            alternarColoFilasDatagridview2(MetroGrid1)
        End If

    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        SaveFileDialog1.Filter = "CSV file | *.csv"
        SaveFileDialog1.DefaultExt = ".csv" 'EVITA TENER QUE ES CRIBIR LA EXTENSION AL GUARDAR

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            'GUARDA EL VALOR DE LA CABECERA (COLUMNAS)
            Dim COLUMNAS As Integer = MetroGrid1.Columns.Count
            Dim CABECERA As String = String.Empty
            For I = 0 To COLUMNAS - 2 'RECORRE LA CABECERA
                CABECERA += MetroGrid1.Columns(I).Name & "," 'AÑADE LA , PARA LOS CAMPOS
            Next
            CABECERA += MetroGrid1.Columns(COLUMNAS - 1).Name & vbCrLf 'AÑADE EL SALTO DE LINEA CUANDO SE HA COMPLETADO EL REGISTRO
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, CABECERA, False) 'GUARDA EL REGISTRO CABECERA

            'GUARDA EL RESTO DE VALORES (FILAS)
            Dim FILAS As Integer = MetroGrid1.Rows.Count
            Dim TEXTO As String = String.Empty

            'RECORRE FILAS X COLUMNAS
            For I = 0 To FILAS - 2
                For J = 0 To COLUMNAS - 2
                    TEXTO += MetroGrid1.Item(J, I).Value & "," 'AÑADE LA , PARA LOS CAMPOS
                Next
                TEXTO += MetroGrid1.Item(COLUMNAS - 1, I).Value & vbCrLf 'AÑADE EL SALTO DE LINEA CUANDO SE HA COMPLETADO CADA REGISTRO
            Next
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TEXTO, True) 'GUARDA CADA REGSITRO
        End If
    End Sub

    Private Sub MetroGrid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid1.CellContentClick

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case OpenFileDialog1.SafeFileName
            Case "OpenFileDialog1"
                MetroLabel4.Text = "File :"
            Case ""
                MetroLabel4.Text = "File :"
            Case Else
                MetroLabel4.Text = OpenFileDialog1.SafeFileName & " | Rows: " & MetroGrid1.RowCount - 1 & " | Columns: " & MetroGrid1.ColumnCount
        End Select

    End Sub

    Private Sub MetroComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub MetroTextBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        search.Show()
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Select Case MetroButton4.Style
            Case MetroFramework.MetroColorStyle.Red
                fedit.Show()
                Timer2.Start()
                MetroButton4.Style = MetroFramework.MetroColorStyle.Green
            Case MetroFramework.MetroColorStyle.Green
                fedit.Close()
                Timer2.Stop()
                MetroButton4.Style = MetroFramework.MetroColorStyle.Red
        End Select
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        MetroGrid1.CurrentCell.Value = fedit.MetroTextBox1.Text
    End Sub

    Private Sub MetroComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles MetroComboBox1.SelectedIndexChanged

    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        Select Case MetroComboBox1.SelectedItem
            Case "NxH Manager"
                Try
                    Dim RUTA As String = Application.StartupPath 'RUTA AL EXE NORMAL
                    RUTA = RUTA.Substring(0, RUTA.LastIndexOf("\")) & "\NxH.exe" 'NO INCLUYE LA ULTIMA CARPETA Y AÑADE EL ARCHIVO 
                    Shell(RUTA)
                    Me.Close()
                Catch ex As Exception
                    MsgBox("You don't have NxH Server Manager!")
                End Try
            Case "Dark$oul Editor"
                Me.Close()
            Case "UltraCommands"
                Try
                    Dim RUTA2 As String = Application.StartupPath 'RUTA AL EXE NORMAL
                    RUTA2 = RUTA2.Substring(0, RUTA2.LastIndexOf("\")) & "\UltraCommands.exe" 'NO INCLUYE LA ULTIMA CARPETA Y AÑADE EL ARCHIVO 
                    Shell(RUTA2)
                    Me.Close()
                Catch ex As Exception
                    MsgBox("You don't have UltraCommands!")
                End Try
            Case "Matteke Tool"
                Me.Close()
            Case Else
                MsgBox("Please select a tool to back")
        End Select
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs)
    End Sub
End Class
