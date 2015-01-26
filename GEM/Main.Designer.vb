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
        Me.components = New System.ComponentModel.Container
        Me.MenuMain = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatabaseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.CompattaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelezionaCollegaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.VisualizzaUtentiConnessiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.DatiGeneraliImpiantoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ConfiguraParametriLoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfigurazioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ComponentiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PannelloFotovoltaicoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InverterFotovoltaicoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContatoreDiProduzioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.LoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StringTesterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InverterTesterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.IngressiDiMisuraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImpiantoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClienteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ImpiantoDiProduzioneToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ContatoreDiProduzioneToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.InverterFotovoltaiciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StringheDiPannelliFotovoltaiciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PannelliFotovoltaiciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SistemaDiControlloToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoggerInstallatiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InverterTesterInstallatiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StringTesterInstallatiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImpiantoDiProduzioneLoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InverterFotovoltaiciInverterTesterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AllarmiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EMailLoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ComunicazioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfiguraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VisualizzaLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AggiornaManualmenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.VisualizzaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FinestraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SovrapponiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AffiancaOrizzontalmenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AffiancaVerticalmenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem_Info = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStripTCPIP = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPConnesse = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusIndirizzoPortaTCPIPConnesse = New System.Windows.Forms.ToolStripStatusLabel
        Me.TimerTCPIP = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.TimerCheckDL = New System.Windows.Forms.Timer(Me.components)
        Me.MenuMain.SuspendLayout()
        Me.StatusStripTCPIP.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ConfigurazioneToolStripMenuItem, Me.UtenteToolStripMenuItem, Me.LogToolStripMenuItem, Me.DatiToolStripMenuItem, Me.FinestraToolStripMenuItem, Me.ToolStripMenuItem_Info})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Size = New System.Drawing.Size(1016, 24)
        Me.MenuMain.TabIndex = 1
        Me.MenuMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseToolStripMenuItem1, Me.ToolStripSeparator2, Me.DatiGeneraliImpiantoToolStripMenuItem, Me.ToolStripSeparator6, Me.ConfiguraParametriLoggerToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'DatabaseToolStripMenuItem1
        '
        Me.DatabaseToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompattaToolStripMenuItem, Me.SelezionaCollegaToolStripMenuItem1, Me.VisualizzaUtentiConnessiToolStripMenuItem})
        Me.DatabaseToolStripMenuItem1.Name = "DatabaseToolStripMenuItem1"
        Me.DatabaseToolStripMenuItem1.Size = New System.Drawing.Size(206, 22)
        Me.DatabaseToolStripMenuItem1.Text = "Database"
        '
        'CompattaToolStripMenuItem
        '
        Me.CompattaToolStripMenuItem.Name = "CompattaToolStripMenuItem"
        Me.CompattaToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.CompattaToolStripMenuItem.Text = "Compatta"
        '
        'SelezionaCollegaToolStripMenuItem1
        '
        Me.SelezionaCollegaToolStripMenuItem1.Name = "SelezionaCollegaToolStripMenuItem1"
        Me.SelezionaCollegaToolStripMenuItem1.Size = New System.Drawing.Size(197, 22)
        Me.SelezionaCollegaToolStripMenuItem1.Text = "Seleziona/Collega"
        '
        'VisualizzaUtentiConnessiToolStripMenuItem
        '
        Me.VisualizzaUtentiConnessiToolStripMenuItem.Name = "VisualizzaUtentiConnessiToolStripMenuItem"
        Me.VisualizzaUtentiConnessiToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.VisualizzaUtentiConnessiToolStripMenuItem.Text = "Visualizza Utenti Connessi"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(203, 6)
        '
        'DatiGeneraliImpiantoToolStripMenuItem
        '
        Me.DatiGeneraliImpiantoToolStripMenuItem.Name = "DatiGeneraliImpiantoToolStripMenuItem"
        Me.DatiGeneraliImpiantoToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.DatiGeneraliImpiantoToolStripMenuItem.Text = "Dati Generali Impianto"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(203, 6)
        '
        'ConfiguraParametriLoggerToolStripMenuItem
        '
        Me.ConfiguraParametriLoggerToolStripMenuItem.Name = "ConfiguraParametriLoggerToolStripMenuItem"
        Me.ConfiguraParametriLoggerToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ConfiguraParametriLoggerToolStripMenuItem.Text = "Configura Parametri Logger"
        '
        'ConfigurazioneToolStripMenuItem
        '
        Me.ConfigurazioneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ComponentiToolStripMenuItem, Me.ImpiantoToolStripMenuItem, Me.SistemaDiControlloToolStripMenuItem, Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem, Me.AllarmiToolStripMenuItem, Me.ComunicazioneToolStripMenuItem})
        Me.ConfigurazioneToolStripMenuItem.Name = "ConfigurazioneToolStripMenuItem"
        Me.ConfigurazioneToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.ConfigurazioneToolStripMenuItem.Text = "Configurazione"
        '
        'ComponentiToolStripMenuItem
        '
        Me.ComponentiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PannelloFotovoltaicoToolStripMenuItem, Me.InverterFotovoltaicoToolStripMenuItem, Me.ContatoreDiProduzioneToolStripMenuItem, Me.ToolStripSeparator3, Me.LoggerToolStripMenuItem, Me.StringTesterToolStripMenuItem, Me.InverterTesterToolStripMenuItem, Me.ToolStripSeparator4, Me.IngressiDiMisuraToolStripMenuItem})
        Me.ComponentiToolStripMenuItem.Name = "ComponentiToolStripMenuItem"
        Me.ComponentiToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.ComponentiToolStripMenuItem.Text = "Componenti"
        '
        'PannelloFotovoltaicoToolStripMenuItem
        '
        Me.PannelloFotovoltaicoToolStripMenuItem.Name = "PannelloFotovoltaicoToolStripMenuItem"
        Me.PannelloFotovoltaicoToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.PannelloFotovoltaicoToolStripMenuItem.Text = "Pannello Fotovoltaico"
        '
        'InverterFotovoltaicoToolStripMenuItem
        '
        Me.InverterFotovoltaicoToolStripMenuItem.Name = "InverterFotovoltaicoToolStripMenuItem"
        Me.InverterFotovoltaicoToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.InverterFotovoltaicoToolStripMenuItem.Text = "Inverter Fotovoltaico"
        '
        'ContatoreDiProduzioneToolStripMenuItem
        '
        Me.ContatoreDiProduzioneToolStripMenuItem.Name = "ContatoreDiProduzioneToolStripMenuItem"
        Me.ContatoreDiProduzioneToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ContatoreDiProduzioneToolStripMenuItem.Text = "Contatore Di Produzione"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(188, 6)
        '
        'LoggerToolStripMenuItem
        '
        Me.LoggerToolStripMenuItem.Name = "LoggerToolStripMenuItem"
        Me.LoggerToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.LoggerToolStripMenuItem.Text = "Logger"
        '
        'StringTesterToolStripMenuItem
        '
        Me.StringTesterToolStripMenuItem.Name = "StringTesterToolStripMenuItem"
        Me.StringTesterToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.StringTesterToolStripMenuItem.Text = "String Tester"
        '
        'InverterTesterToolStripMenuItem
        '
        Me.InverterTesterToolStripMenuItem.Name = "InverterTesterToolStripMenuItem"
        Me.InverterTesterToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.InverterTesterToolStripMenuItem.Text = "Inverter Tester"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(188, 6)
        '
        'IngressiDiMisuraToolStripMenuItem
        '
        Me.IngressiDiMisuraToolStripMenuItem.Name = "IngressiDiMisuraToolStripMenuItem"
        Me.IngressiDiMisuraToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.IngressiDiMisuraToolStripMenuItem.Text = "Ingressi Di Misura"
        '
        'ImpiantoToolStripMenuItem
        '
        Me.ImpiantoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClienteToolStripMenuItem1, Me.ImpiantoDiProduzioneToolStripMenuItem1, Me.ContatoreDiProduzioneToolStripMenuItem1, Me.InverterFotovoltaiciToolStripMenuItem, Me.StringheDiPannelliFotovoltaiciToolStripMenuItem, Me.PannelliFotovoltaiciToolStripMenuItem})
        Me.ImpiantoToolStripMenuItem.Name = "ImpiantoToolStripMenuItem"
        Me.ImpiantoToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.ImpiantoToolStripMenuItem.Text = "Impianto"
        '
        'ClienteToolStripMenuItem1
        '
        Me.ClienteToolStripMenuItem1.Name = "ClienteToolStripMenuItem1"
        Me.ClienteToolStripMenuItem1.Size = New System.Drawing.Size(223, 22)
        Me.ClienteToolStripMenuItem1.Text = "Cliente"
        '
        'ImpiantoDiProduzioneToolStripMenuItem1
        '
        Me.ImpiantoDiProduzioneToolStripMenuItem1.Name = "ImpiantoDiProduzioneToolStripMenuItem1"
        Me.ImpiantoDiProduzioneToolStripMenuItem1.Size = New System.Drawing.Size(223, 22)
        Me.ImpiantoDiProduzioneToolStripMenuItem1.Text = "Impianto Di Produzione"
        '
        'ContatoreDiProduzioneToolStripMenuItem1
        '
        Me.ContatoreDiProduzioneToolStripMenuItem1.Name = "ContatoreDiProduzioneToolStripMenuItem1"
        Me.ContatoreDiProduzioneToolStripMenuItem1.Size = New System.Drawing.Size(223, 22)
        Me.ContatoreDiProduzioneToolStripMenuItem1.Text = "Contatore Di Produzione"
        '
        'InverterFotovoltaiciToolStripMenuItem
        '
        Me.InverterFotovoltaiciToolStripMenuItem.Name = "InverterFotovoltaiciToolStripMenuItem"
        Me.InverterFotovoltaiciToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.InverterFotovoltaiciToolStripMenuItem.Text = "Inverter Fotovoltaici"
        '
        'StringheDiPannelliFotovoltaiciToolStripMenuItem
        '
        Me.StringheDiPannelliFotovoltaiciToolStripMenuItem.Name = "StringheDiPannelliFotovoltaiciToolStripMenuItem"
        Me.StringheDiPannelliFotovoltaiciToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.StringheDiPannelliFotovoltaiciToolStripMenuItem.Text = "Stringhe Di Pannelli Fotovoltaici"
        '
        'PannelliFotovoltaiciToolStripMenuItem
        '
        Me.PannelliFotovoltaiciToolStripMenuItem.Name = "PannelliFotovoltaiciToolStripMenuItem"
        Me.PannelliFotovoltaiciToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.PannelliFotovoltaiciToolStripMenuItem.Text = "Pannelli Fotovoltaici"
        '
        'SistemaDiControlloToolStripMenuItem
        '
        Me.SistemaDiControlloToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoggerInstallatiToolStripMenuItem, Me.InverterTesterInstallatiToolStripMenuItem, Me.StringTesterInstallatiToolStripMenuItem1})
        Me.SistemaDiControlloToolStripMenuItem.Name = "SistemaDiControlloToolStripMenuItem"
        Me.SistemaDiControlloToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.SistemaDiControlloToolStripMenuItem.Text = "Sistema Di Controllo"
        '
        'LoggerInstallatiToolStripMenuItem
        '
        Me.LoggerInstallatiToolStripMenuItem.Name = "LoggerInstallatiToolStripMenuItem"
        Me.LoggerInstallatiToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.LoggerInstallatiToolStripMenuItem.Text = "Logger Installati"
        '
        'InverterTesterInstallatiToolStripMenuItem
        '
        Me.InverterTesterInstallatiToolStripMenuItem.Name = "InverterTesterInstallatiToolStripMenuItem"
        Me.InverterTesterInstallatiToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.InverterTesterInstallatiToolStripMenuItem.Text = "Inverter Tester  Installati"
        '
        'StringTesterInstallatiToolStripMenuItem1
        '
        Me.StringTesterInstallatiToolStripMenuItem1.Name = "StringTesterInstallatiToolStripMenuItem1"
        Me.StringTesterInstallatiToolStripMenuItem1.Size = New System.Drawing.Size(195, 22)
        Me.StringTesterInstallatiToolStripMenuItem1.Text = "String Tester Installati"
        '
        'AssociaImpiantoASistemaDiControlloToolStripMenuItem
        '
        Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImpiantoDiProduzioneLoggerToolStripMenuItem, Me.InverterFotovoltaiciInverterTesterToolStripMenuItem, Me.StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem})
        Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem.Name = "AssociaImpiantoASistemaDiControlloToolStripMenuItem"
        Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.AssociaImpiantoASistemaDiControlloToolStripMenuItem.Text = "Associa Impianto A Sistema Di Controllo"
        '
        'ImpiantoDiProduzioneLoggerToolStripMenuItem
        '
        Me.ImpiantoDiProduzioneLoggerToolStripMenuItem.Name = "ImpiantoDiProduzioneLoggerToolStripMenuItem"
        Me.ImpiantoDiProduzioneLoggerToolStripMenuItem.Size = New System.Drawing.Size(297, 22)
        Me.ImpiantoDiProduzioneLoggerToolStripMenuItem.Text = "Impianto Di Produzione->Logger"
        '
        'InverterFotovoltaiciInverterTesterToolStripMenuItem
        '
        Me.InverterFotovoltaiciInverterTesterToolStripMenuItem.Name = "InverterFotovoltaiciInverterTesterToolStripMenuItem"
        Me.InverterFotovoltaiciInverterTesterToolStripMenuItem.Size = New System.Drawing.Size(297, 22)
        Me.InverterFotovoltaiciInverterTesterToolStripMenuItem.Text = "Inverter Fotovoltaici->Inverter Tester"
        '
        'StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem
        '
        Me.StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem.Name = "StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem"
        Me.StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem.Size = New System.Drawing.Size(297, 22)
        Me.StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem.Text = "Stringhe Di Pannelli Fotovoltaici->String Tester"
        '
        'AllarmiToolStripMenuItem
        '
        Me.AllarmiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EMailLoggerToolStripMenuItem})
        Me.AllarmiToolStripMenuItem.Name = "AllarmiToolStripMenuItem"
        Me.AllarmiToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.AllarmiToolStripMenuItem.Text = "Associa Allarmi"
        '
        'EMailLoggerToolStripMenuItem
        '
        Me.EMailLoggerToolStripMenuItem.Name = "EMailLoggerToolStripMenuItem"
        Me.EMailLoggerToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.EMailLoggerToolStripMenuItem.Text = "EMail->Logger"
        '
        'ComunicazioneToolStripMenuItem
        '
        Me.ComunicazioneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraToolStripMenuItem})
        Me.ComunicazioneToolStripMenuItem.Name = "ComunicazioneToolStripMenuItem"
        Me.ComunicazioneToolStripMenuItem.Size = New System.Drawing.Size(263, 22)
        Me.ComunicazioneToolStripMenuItem.Text = "Comunicazione"
        '
        'ConfiguraToolStripMenuItem
        '
        Me.ConfiguraToolStripMenuItem.Name = "ConfiguraToolStripMenuItem"
        Me.ConfiguraToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.ConfiguraToolStripMenuItem.Text = "Configurazione"
        '
        'UtenteToolStripMenuItem
        '
        Me.UtenteToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoginToolStripMenuItem, Me.LogoutToolStripMenuItem})
        Me.UtenteToolStripMenuItem.Name = "UtenteToolStripMenuItem"
        Me.UtenteToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.UtenteToolStripMenuItem.Text = "Utente"
        '
        'LoginToolStripMenuItem
        '
        Me.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem"
        Me.LoginToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.LoginToolStripMenuItem.Text = "Login"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'LogToolStripMenuItem
        '
        Me.LogToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VisualizzaLogToolStripMenuItem})
        Me.LogToolStripMenuItem.Name = "LogToolStripMenuItem"
        Me.LogToolStripMenuItem.Size = New System.Drawing.Size(36, 20)
        Me.LogToolStripMenuItem.Text = "Log"
        '
        'VisualizzaLogToolStripMenuItem
        '
        Me.VisualizzaLogToolStripMenuItem.Name = "VisualizzaLogToolStripMenuItem"
        Me.VisualizzaLogToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.VisualizzaLogToolStripMenuItem.Text = "Visualizza"
        '
        'DatiToolStripMenuItem
        '
        Me.DatiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AggiornaManualmenteToolStripMenuItem, Me.ToolStripSeparator1, Me.VisualizzaToolStripMenuItem})
        Me.DatiToolStripMenuItem.Name = "DatiToolStripMenuItem"
        Me.DatiToolStripMenuItem.Size = New System.Drawing.Size(38, 20)
        Me.DatiToolStripMenuItem.Text = "Dati"
        '
        'AggiornaManualmenteToolStripMenuItem
        '
        Me.AggiornaManualmenteToolStripMenuItem.Name = "AggiornaManualmenteToolStripMenuItem"
        Me.AggiornaManualmenteToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.AggiornaManualmenteToolStripMenuItem.Text = "Aggiorna Manualmente"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(181, 6)
        '
        'VisualizzaToolStripMenuItem
        '
        Me.VisualizzaToolStripMenuItem.Name = "VisualizzaToolStripMenuItem"
        Me.VisualizzaToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.VisualizzaToolStripMenuItem.Text = "Visualizza"
        '
        'FinestraToolStripMenuItem
        '
        Me.FinestraToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SovrapponiToolStripMenuItem, Me.AffiancaOrizzontalmenteToolStripMenuItem, Me.AffiancaVerticalmenteToolStripMenuItem, Me.ToolStripSeparator5})
        Me.FinestraToolStripMenuItem.Name = "FinestraToolStripMenuItem"
        Me.FinestraToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.FinestraToolStripMenuItem.Text = "Finestra"
        '
        'SovrapponiToolStripMenuItem
        '
        Me.SovrapponiToolStripMenuItem.Name = "SovrapponiToolStripMenuItem"
        Me.SovrapponiToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SovrapponiToolStripMenuItem.Text = "Sovrapponi"
        '
        'AffiancaOrizzontalmenteToolStripMenuItem
        '
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Name = "AffiancaOrizzontalmenteToolStripMenuItem"
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.AffiancaOrizzontalmenteToolStripMenuItem.Text = "Orizzontale"
        '
        'AffiancaVerticalmenteToolStripMenuItem
        '
        Me.AffiancaVerticalmenteToolStripMenuItem.Name = "AffiancaVerticalmenteToolStripMenuItem"
        Me.AffiancaVerticalmenteToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.AffiancaVerticalmenteToolStripMenuItem.Text = "Verticale"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(125, 6)
        '
        'ToolStripMenuItem_Info
        '
        Me.ToolStripMenuItem_Info.Name = "ToolStripMenuItem_Info"
        Me.ToolStripMenuItem_Info.Size = New System.Drawing.Size(24, 20)
        Me.ToolStripMenuItem_Info.Text = "?"
        '
        'StatusStripTCPIP
        '
        Me.StatusStripTCPIP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto, Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto, Me.ToolStripStatusLabelIndirizzoPortaTCPIPConnesse, Me.ToolStripStatusIndirizzoPortaTCPIPConnesse})
        Me.StatusStripTCPIP.Location = New System.Drawing.Point(0, 712)
        Me.StatusStripTCPIP.Name = "StatusStripTCPIP"
        Me.StatusStripTCPIP.Size = New System.Drawing.Size(1016, 22)
        Me.StatusStripTCPIP.TabIndex = 2
        Me.StatusStripTCPIP.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto
        '
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto.Name = "ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto"
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto.Size = New System.Drawing.Size(168, 17)
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto.Text = "Indirizzo:Porta TCP/IP In Ascolto:"
        '
        'ToolStripStatusIndirizzoPortaTCPIPInAscolto
        '
        Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto.Name = "ToolStripStatusIndirizzoPortaTCPIPInAscolto"
        Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto.Size = New System.Drawing.Size(19, 17)
        Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto.Text = "---"
        '
        'ToolStripStatusLabelIndirizzoPortaTCPIPConnesse
        '
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPConnesse.Name = "ToolStripStatusLabelIndirizzoPortaTCPIPConnesse"
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPConnesse.Size = New System.Drawing.Size(167, 17)
        Me.ToolStripStatusLabelIndirizzoPortaTCPIPConnesse.Text = "Indirizzo:Porta TCP/IP Connesse:"
        '
        'ToolStripStatusIndirizzoPortaTCPIPConnesse
        '
        Me.ToolStripStatusIndirizzoPortaTCPIPConnesse.Name = "ToolStripStatusIndirizzoPortaTCPIPConnesse"
        Me.ToolStripStatusIndirizzoPortaTCPIPConnesse.Size = New System.Drawing.Size(19, 17)
        Me.ToolStripStatusIndirizzoPortaTCPIPConnesse.Text = "---"
        '
        'TimerTCPIP
        '
        Me.TimerTCPIP.Interval = 1000
        '
        'SerialPort
        '
        Me.SerialPort.BaudRate = 38400
        '
        'TimerCheckDL
        '
        Me.TimerCheckDL.Interval = 30000
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.Controls.Add(Me.StatusStripTCPIP)
        Me.Controls.Add(Me.MenuMain)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuMain
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fase Engineering"
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.StatusStripTCPIP.ResumeLayout(False)
        Me.StatusStripTCPIP.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoginToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DatabaseToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelezionaCollegaToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurazioneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComponentiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PannelloFotovoltaicoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterFotovoltaicoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContatoreDiProduzioneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StringTesterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterTesterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents IngressiDiMisuraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImpiantoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClienteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImpiantoDiProduzioneToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContatoreDiProduzioneToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterFotovoltaiciToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StringheDiPannelliFotovoltaiciToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PannelliFotovoltaiciToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SistemaDiControlloToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoggerInstallatiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterTesterInstallatiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StringTesterInstallatiToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssociaImpiantoASistemaDiControlloToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImpiantoDiProduzioneLoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InverterFotovoltaiciInverterTesterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompattaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComunicazioneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStripTCPIP As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelIndirizzoPortaTCPIPInAscolto As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusIndirizzoPortaTCPIPInAscolto As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TimerTCPIP As System.Windows.Forms.Timer
    Friend WithEvents ToolStripStatusLabelIndirizzoPortaTCPIPConnesse As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusIndirizzoPortaTCPIPConnesse As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DatiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AggiornaManualmenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents VisualizzaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VisualizzaLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Info As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SerialPort As System.IO.Ports.SerialPort
    Friend WithEvents LogoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FinestraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SovrapponiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffiancaOrizzontalmenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffiancaVerticalmenteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllarmiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EMailLoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerCheckDL As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DatiGeneraliImpiantoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents VisualizzaUtentiConnessiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ConfiguraParametriLoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
