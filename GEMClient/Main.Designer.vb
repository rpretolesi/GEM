<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.MenuMain = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.TestConnessioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VisualizzaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FinestraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SovrapponiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AffiancaOrizzontalmenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AffiancaVerticalmenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_Info = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.MenuMain.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.DatiToolStripMenuItem, Me.FinestraToolStripMenuItem, Me.ToolStripMenuItem_Info})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Size = New System.Drawing.Size(792, 24)
        Me.MenuMain.TabIndex = 2
        Me.MenuMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem, Me.ToolStripSeparator1, Me.TestConnessioneToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem
        '
        Me.InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem.Name = "InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem"
        Me.InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem.Text = "Inserisci Codice Cliente / Stringa Di Connessione"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(326, 6)
        '
        'TestConnessioneToolStripMenuItem
        '
        Me.TestConnessioneToolStripMenuItem.Name = "TestConnessioneToolStripMenuItem"
        Me.TestConnessioneToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.TestConnessioneToolStripMenuItem.Text = "Test Connessione"
        '
        'DatiToolStripMenuItem
        '
        Me.DatiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VisualizzaToolStripMenuItem})
        Me.DatiToolStripMenuItem.Name = "DatiToolStripMenuItem"
        Me.DatiToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.DatiToolStripMenuItem.Text = "Dati"
        '
        'VisualizzaToolStripMenuItem
        '
        Me.VisualizzaToolStripMenuItem.Name = "VisualizzaToolStripMenuItem"
        Me.VisualizzaToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.VisualizzaToolStripMenuItem.Text = "Visualizza"
        '
        'FinestraToolStripMenuItem
        '
        Me.FinestraToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SovrapponiToolStripMenuItem, Me.AffiancaOrizzontalmenteToolStripMenuItem, Me.AffiancaVerticalmenteToolStripMenuItem, Me.ToolStripSeparator2})
        Me.FinestraToolStripMenuItem.Name = "FinestraToolStripMenuItem"
        Me.FinestraToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.FinestraToolStripMenuItem.Text = "Finestra"
        '
        'SovrapponiToolStripMenuItem
        '
        Me.SovrapponiToolStripMenuItem.Name = "SovrapponiToolStripMenuItem"
        Me.SovrapponiToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SovrapponiToolStripMenuItem.Text = "Sovrapponi"
        '
        'AffiancaOrizzontalmenteToolStripMenuItem
        '
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Name = "AffiancaOrizzontalmenteToolStripMenuItem"
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Text = "Orizzontale"
        '
        'AffiancaVerticalmenteToolStripMenuItem
        '
        Me.AffiancaVerticalmenteToolStripMenuItem.Name = "AffiancaVerticalmenteToolStripMenuItem"
        Me.AffiancaVerticalmenteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AffiancaVerticalmenteToolStripMenuItem.Text = "Verticale"
        '
        'ToolStripMenuItem_Info
        '
        Me.ToolStripMenuItem_Info.Name = "ToolStripMenuItem_Info"
        Me.ToolStripMenuItem_Info.Size = New System.Drawing.Size(24, 20)
        Me.ToolStripMenuItem_Info.Text = "?"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip.TabIndex = 4
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(162, 17)
        Me.ToolStripStatusLabel1.Text = "Stato connessione col server: "
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(85, 17)
        Me.ToolStripStatusLabel2.Text = "Non Connesso"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(149, 6)
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.GEMClient.My.Resources.Resources.LogoFase
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fase Engineering - GEM Client"
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DatiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Info As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InserisciCodiceClienteStringaDiConnessioneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VisualizzaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FinestraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SovrapponiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffiancaOrizzontalmenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffiancaVerticalmenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TestConnessioneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
