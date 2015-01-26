Public Class ManualeUtente

    Private Sub ManualeUtente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = " Manuale Utente  -->Click sulla pagina per passare a quella successiva<--"
        Me.BackgroundImage = GEMClient.My.Resources.Resources.LogoFase
        Me.Height = GEMClient.My.Resources.Resources.LogoFase.Height
        Me.Width = GEMClient.My.Resources.Resources.LogoFase.Width
    End Sub

    Private Sub ManualeUtente_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        'System.Windows.Forms.MessageBox.Show("")
    End Sub
End Class