Public Class search

    Private Sub search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Search in " & Form1.OpenFileDialog1.SafeFileName
    End Sub

    Private Sub MetroTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MetroTextBox1.TextChanged
        For I = 0 To Form1.MetroGrid1.Rows().Count - 1
            For Each CELDA In Form1.MetroGrid1.Rows(I).Cells
                If CELDA.VALUE = MetroTextBox1.Text Then
                    Form1.MetroGrid1.CurrentCell = CELDA 'SELECCIONA LA CELDA
                    Exit Sub
                End If
            Next
        Next
    End Sub
End Class