Imports System.Data.SqlClient

Public Class Main

    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Notifico l'Aggiornamento.
        If My.Settings.NewRelease = True Then 'new version
            My.Settings.Upgrade() 'yes, reload previous settings
            My.Settings.NewRelease = False 'set new version to false
            My.Settings.Save() 'save the settings
            System.Windows.Forms.MessageBox.Show(Owner, "Il programma e' stato aggiornato alla versione: " + My.Application.Info.Version.ToString() + "." + vbCrLf + "Per info e dettagli potete scrivere una email a: service@fasenet.it" + vbCrLf + "Buon Lavoro da Fase Engineering." + vbCrLf + "www.fasenet.com" + vbCrLf + vbCrLf + vbCrLf + vbCrLf + "Sviluppato per Fase Engineering Da:" + vbCrLf + "Pretolesi Riccardo - www.consulenzeperizie.it - www.pretolesi.com", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Verifico che esista il logo della Fase nella directory utente, diversamente lo creo
        Try
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoFase.jpg") = False Then
                GEMClient.My.Resources.Resources.LogoFase.Save(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoFase.jpg")
            End If
        Catch ex As Exception

        End Try

        ' Verifico che esista il logo del Cliente nella directory utente, diversamente lo creo
        Try
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoCliente.jpg") = False Then
                GEMClient.My.Resources.Resources.LogoFase.Save(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoCliente.jpg")
            End If
        Catch ex As Exception

        End Try

        TestConnessione()

    End Sub

    Private Sub TestConnessione()

        ToolStripStatusLabel2.Text = "Connessione non Disponibile. Eseguire: 'File'->'Test Connessione' per Ritentare la Connessione."
        ToolStripStatusLabel2.ForeColor = Color.Red
        VisualizzaToolStripMenuItem.Enabled = False
        Try

            'Dim bNetAv As Boolean
            'bNetAv = My.Computer.Network.IsAvailable
            'If bNetAv = True Then
            '    ' Eseguo il ping
            '    Dim strSCS As New SqlConnectionStringBuilder(My.Settings.ConnectionString)
            '    Dim strSCSPA(1) As Char
            '    strSCSPA(0) = ","
            '    strSCSPA(1) = "\"
            '    Dim stra() As String = strSCS.DataSource.ToString().Split(strSCSPA)
            '    If stra.Length > 0 Then
            'Dim bIPResp As Boolean
            'bIPResp = My.Computer.Network.Ping(stra(0))
            'If bIPResp = True Then
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Try
                cn.Open()
                cn.Close()
                cn.Dispose()
                ToolStripStatusLabel2.Text = "Connessione Disponibile"
                ToolStripStatusLabel2.ForeColor = Color.Green
                VisualizzaToolStripMenuItem.Enabled = True
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(Owner, ex.Message + ex.StackTrace, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End Try
            'End If
            'End If
            'End If

        Catch ex As Exception
            ToolStripStatusLabel2.Text = "Connessione non Disponibile. Eseguire: 'File'->'Test Connessione' per Ritentare la Connessione."
            ToolStripStatusLabel2.ForeColor = Color.Red
            VisualizzaToolStripMenuItem.Enabled = False

            System.Windows.Forms.MessageBox.Show(Owner, ex.Message + ex.StackTrace, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Private Sub InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem.Click
        Dim cc As New ClienteCodiceStringaDiConnessione
        Dim dr As Windows.Forms.DialogResult
        cc.TextBox_Conn_String.Text = My.Settings.ConnectionString
        cc.TextBox_C_Codice.Text = My.Settings.CodiceCliente
        dr = cc.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK Then
            My.Settings.ConnectionString = cc.TextBox_Conn_String.Text
            My.Settings.CodiceCliente = cc.TextBox_C_Codice.Text
            My.Settings.Save()
        End If
        cc.Dispose()

        TestConnessione()

    End Sub

    Private Sub TestConnessioneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestConnessioneToolStripMenuItem.Click
        TestConnessione()
    End Sub

    Private Sub VisualizzaToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisualizzaToolStripMenuItem.Click
        VisDatiDataLogger.Close()
        VisDatiDataLogger.MdiParent = Me
        VisDatiDataLogger.Show()
    End Sub

    Private Sub ManualeUtenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ManualeUtente.Close()
        ManualeUtente.MdiParent = Me
        ManualeUtente.Show()
    End Sub

    Private Sub ToolStripMenuItem_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_Info.Click
        Dim strDatabaseRel As String = ""
        GetDGI(1, strDatabaseRel)
        System.Windows.Forms.MessageBox.Show(Owner, "GEMClient Versione: " + My.Application.Info.Version.ToString() + "." + vbCrLf + "Database Versione: " + strDatabaseRel + "." + vbCrLf + "Per info e dettagli potete scrivere una email a: service@fasenet.it" + vbCrLf + "www.fasenet.com" + vbCrLf + vbCrLf + vbCrLf + vbCrLf + "Sviluppato per Fase Engineering Da:" + vbCrLf + "Pretolesi Riccardo - www.consulenzeperizie.it - www.pretolesi.com", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub FinestraToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownOpening

        For Each frm As System.Windows.Forms.Form In Me.MdiChildren
            Dim tsmi As New ToolStripMenuItem
            tsmi.Text = frm.Text
            tsmi.Name = frm.Name
            tsmi.Tag = frm
            Me.FinestraToolStripMenuItem.DropDownItems.Add(tsmi)
            AddHandler tsmi.Click, AddressOf FinestraToolStripMenuItem_Click
        Next
    End Sub

    Private Sub FinestraToolStripMenuItem_DropDownClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownClosed
        For Each frm As System.Windows.Forms.Form In Me.MdiChildren
            Me.FinestraToolStripMenuItem.DropDownItems.RemoveByKey(frm.Name)
        Next
    End Sub

    Private Sub FinestraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not sender Is Nothing Then
            If sender.GetType().Name = "ToolStripMenuItem" Then
                If Not sender.Tag Is Nothing Then
                    If Not sender.Tag.GetType().GetMethod("Focus") Is Nothing Then
                        sender.Tag.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SovrapponiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SovrapponiToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub AffiancaOrizzontalmenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AffiancaOrizzontalmenteToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub AffiancaVerticalmenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AffiancaVerticalmenteToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub VisualizzaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownOpening
        Dim bAllMinimized As Boolean

        bAllMinimized = True
        If Me.MdiChildren.Length = 0 Then
            SovrapponiToolStripMenuItem.Enabled = False
            AffiancaOrizzontalmenteToolStripMenuItem.Enabled = False
            AffiancaVerticalmenteToolStripMenuItem.Enabled = False
        Else
            For Each swf As Form In MdiChildren
                If swf.WindowState = FormWindowState.Maximized Or swf.WindowState = FormWindowState.Normal Then
                    bAllMinimized = False
                End If
            Next
            If bAllMinimized = False Then
                SovrapponiToolStripMenuItem.Enabled = True
                AffiancaOrizzontalmenteToolStripMenuItem.Enabled = True
                AffiancaVerticalmenteToolStripMenuItem.Enabled = True
            Else
                SovrapponiToolStripMenuItem.Enabled = False
                AffiancaOrizzontalmenteToolStripMenuItem.Enabled = False
                AffiancaVerticalmenteToolStripMenuItem.Enabled = False
            End If
        End If
    End Sub

    'Private Sub CaricaLogoUtenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ofd As New OpenFileDialog
    '    ofd.ShowDialog(Owner)
    '    Try
    '        My.Computer.FileSystem.CopyFile(ofd.FileName, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoCliente.jpg", True)
    '    Catch ex As Exception
    '        System.Windows.Forms.MessageBox.Show(Owner, ex.Message + ex.StackTrace, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '    End Try
    'End Sub

End Class