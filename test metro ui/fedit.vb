Public Class fedit

    Private Sub fedit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Timer2.Stop()
        Form1.MetroButton4.Style = MetroFramework.MetroColorStyle.Red
    End Sub

    Private Sub fedit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class