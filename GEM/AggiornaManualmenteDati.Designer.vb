<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AggiornaManualmenteDati
    Inherits GEM.BaseForm_1

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ComboBox_LI_ID = New System.Windows.Forms.ComboBox
        Me.Label_LI_ID = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.ListBox_Remote_Connected = New System.Windows.Forms.ListBox
        Me.Button_Scarica_Dati_Man = New System.Windows.Forms.Button
        Me.Button_Stop_TCPIP = New System.Windows.Forms.Button
        Me.Label_Connected = New System.Windows.Forms.Label
        Me.Button_Invia_Configurazione = New System.Windows.Forms.Button
        Me.TimerTCPIP = New System.Windows.Forms.Timer(Me.components)
        Me.Button_Leggi_Configurazione = New System.Windows.Forms.Button
        Me.CheckBoxInviaTuttiIParametri = New System.Windows.Forms.CheckBox
        Me.Button_Sincronizza_Dati_Man = New System.Windows.Forms.Button
        Me.Label_PC_Vs_DL = New System.Windows.Forms.Label
        Me.Label_DL_Vs_PC = New System.Windows.Forms.Label
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione = New System.Windows.Forms.CheckBox
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione = New System.Windows.Forms.CheckBox
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man = New System.Windows.Forms.CheckBox
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind = New System.Windows.Forms.CheckBox
        Me.Button_Connect_Via_HTTP = New System.Windows.Forms.Button
        Me.CheckBoxLeggiTuttiIParametri = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'ComboBox_LI_ID
        '
        Me.ComboBox_LI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_LI_ID.FormattingEnabled = True
        Me.ComboBox_LI_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_LI_ID.Name = "ComboBox_LI_ID"
        Me.ComboBox_LI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_LI_ID.TabIndex = 75
        '
        'Label_LI_ID
        '
        Me.Label_LI_ID.AutoSize = True
        Me.Label_LI_ID.Location = New System.Drawing.Point(9, 85)
        Me.Label_LI_ID.Name = "Label_LI_ID"
        Me.Label_LI_ID.Size = New System.Drawing.Size(125, 13)
        Me.Label_LI_ID.TabIndex = 74
        Me.Label_LI_ID.Text = "Numero Logger Installato"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 73
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(9, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 72
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 71
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(9, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 70
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'ListBox_Remote_Connected
        '
        Me.ListBox_Remote_Connected.FormattingEnabled = True
        Me.ListBox_Remote_Connected.Location = New System.Drawing.Point(12, 227)
        Me.ListBox_Remote_Connected.Name = "ListBox_Remote_Connected"
        Me.ListBox_Remote_Connected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Remote_Connected.Size = New System.Drawing.Size(582, 56)
        Me.ListBox_Remote_Connected.TabIndex = 77
        '
        'Button_Scarica_Dati_Man
        '
        Me.Button_Scarica_Dati_Man.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Button_Scarica_Dati_Man.Location = New System.Drawing.Point(201, 109)
        Me.Button_Scarica_Dati_Man.Name = "Button_Scarica_Dati_Man"
        Me.Button_Scarica_Dati_Man.Size = New System.Drawing.Size(100, 34)
        Me.Button_Scarica_Dati_Man.TabIndex = 82
        Me.Button_Scarica_Dati_Man.Text = "Scarica Dati Manualmente"
        Me.Button_Scarica_Dati_Man.UseVisualStyleBackColor = True
        '
        'Button_Stop_TCPIP
        '
        Me.Button_Stop_TCPIP.Location = New System.Drawing.Point(600, 235)
        Me.Button_Stop_TCPIP.Name = "Button_Stop_TCPIP"
        Me.Button_Stop_TCPIP.Size = New System.Drawing.Size(150, 48)
        Me.Button_Stop_TCPIP.TabIndex = 83
        Me.Button_Stop_TCPIP.Text = "Disconnetti Manualmente"
        Me.Button_Stop_TCPIP.UseVisualStyleBackColor = True
        '
        'Label_Connected
        '
        Me.Label_Connected.AutoSize = True
        Me.Label_Connected.Location = New System.Drawing.Point(184, 206)
        Me.Label_Connected.Name = "Label_Connected"
        Me.Label_Connected.Size = New System.Drawing.Size(154, 13)
        Me.Label_Connected.TabIndex = 84
        Me.Label_Connected.Text = "Dispositivi Remoti Connessi Via"
        '
        'Button_Invia_Configurazione
        '
        Me.Button_Invia_Configurazione.Location = New System.Drawing.Point(519, 110)
        Me.Button_Invia_Configurazione.Name = "Button_Invia_Configurazione"
        Me.Button_Invia_Configurazione.Size = New System.Drawing.Size(100, 34)
        Me.Button_Invia_Configurazione.TabIndex = 85
        Me.Button_Invia_Configurazione.Text = "Invia Configurazione"
        Me.Button_Invia_Configurazione.UseVisualStyleBackColor = True
        '
        'TimerTCPIP
        '
        Me.TimerTCPIP.Interval = 5000
        '
        'Button_Leggi_Configurazione
        '
        Me.Button_Leggi_Configurazione.Location = New System.Drawing.Point(413, 109)
        Me.Button_Leggi_Configurazione.Name = "Button_Leggi_Configurazione"
        Me.Button_Leggi_Configurazione.Size = New System.Drawing.Size(100, 34)
        Me.Button_Leggi_Configurazione.TabIndex = 86
        Me.Button_Leggi_Configurazione.Text = "Leggi Configurazione"
        Me.Button_Leggi_Configurazione.UseVisualStyleBackColor = True
        '
        'CheckBoxInviaTuttiIParametri
        '
        Me.CheckBoxInviaTuttiIParametri.AutoSize = True
        Me.CheckBoxInviaTuttiIParametri.Location = New System.Drawing.Point(519, 146)
        Me.CheckBoxInviaTuttiIParametri.Name = "CheckBoxInviaTuttiIParametri"
        Me.CheckBoxInviaTuttiIParametri.Size = New System.Drawing.Size(100, 17)
        Me.CheckBoxInviaTuttiIParametri.TabIndex = 87
        Me.CheckBoxInviaTuttiIParametri.Text = "Invia Tutti i Par."
        Me.CheckBoxInviaTuttiIParametri.UseVisualStyleBackColor = True
        '
        'Button_Sincronizza_Dati_Man
        '
        Me.Button_Sincronizza_Dati_Man.Location = New System.Drawing.Point(307, 109)
        Me.Button_Sincronizza_Dati_Man.Name = "Button_Sincronizza_Dati_Man"
        Me.Button_Sincronizza_Dati_Man.Size = New System.Drawing.Size(100, 34)
        Me.Button_Sincronizza_Dati_Man.TabIndex = 88
        Me.Button_Sincronizza_Dati_Man.Text = "Sincronizza DB->DL"
        Me.Button_Sincronizza_Dati_Man.UseVisualStyleBackColor = True
        '
        'Label_PC_Vs_DL
        '
        Me.Label_PC_Vs_DL.AutoSize = True
        Me.Label_PC_Vs_DL.Location = New System.Drawing.Point(9, 121)
        Me.Label_PC_Vs_DL.Name = "Label_PC_Vs_DL"
        Me.Label_PC_Vs_DL.Size = New System.Drawing.Size(50, 13)
        Me.Label_PC_Vs_DL.TabIndex = 89
        Me.Label_PC_Vs_DL.Text = "PC -> DL"
        '
        'Label_DL_Vs_PC
        '
        Me.Label_DL_Vs_PC.AutoSize = True
        Me.Label_DL_Vs_PC.Location = New System.Drawing.Point(9, 186)
        Me.Label_DL_Vs_PC.Name = "Label_DL_Vs_PC"
        Me.Label_DL_Vs_PC.Size = New System.Drawing.Size(50, 13)
        Me.Label_DL_Vs_PC.TabIndex = 90
        Me.Label_DL_Vs_PC.Text = "DL -> PC"
        '
        'CheckBox_DL_Vs_PC_Leggi_Configurazione
        '
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.AutoSize = True
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Location = New System.Drawing.Point(413, 182)
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Name = "CheckBox_DL_Vs_PC_Leggi_Configurazione"
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Size = New System.Drawing.Size(88, 17)
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.TabIndex = 91
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Text = "Leggi Config."
        Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.UseVisualStyleBackColor = True
        '
        'CheckBox_DL_Vs_PC_Invia_Configurazione
        '
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.AutoSize = True
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Location = New System.Drawing.Point(519, 182)
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Name = "CheckBox_DL_Vs_PC_Invia_Configurazione"
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Size = New System.Drawing.Size(122, 17)
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.TabIndex = 92
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Text = "Invia Configurazione"
        Me.CheckBox_DL_Vs_PC_Invia_Configurazione.UseVisualStyleBackColor = True
        '
        'CheckBox_DL_Vs_PC_Sincronizza_Dati_Man
        '
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.AutoSize = True
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Location = New System.Drawing.Point(307, 182)
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Name = "CheckBox_DL_Vs_PC_Sincronizza_Dati_Man"
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Size = New System.Drawing.Size(94, 17)
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.TabIndex = 93
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Text = "Sincr. DB->DL"
        Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.UseVisualStyleBackColor = True
        '
        'CheckBox_Usa_LI_TCPIP_Get_Ind
        '
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.AutoSize = True
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Location = New System.Drawing.Point(95, 146)
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Name = "CheckBox_Usa_LI_TCPIP_Get_Ind"
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Size = New System.Drawing.Size(301, 17)
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.TabIndex = 94
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Text = "Utilizza Indirizzo TCP/IP Ricevuto Dall'Ultima Connessione"
        Me.CheckBox_Usa_LI_TCPIP_Get_Ind.UseVisualStyleBackColor = True
        '
        'Button_Connect_Via_HTTP
        '
        Me.Button_Connect_Via_HTTP.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Button_Connect_Via_HTTP.Location = New System.Drawing.Point(95, 109)
        Me.Button_Connect_Via_HTTP.Name = "Button_Connect_Via_HTTP"
        Me.Button_Connect_Via_HTTP.Size = New System.Drawing.Size(100, 34)
        Me.Button_Connect_Via_HTTP.TabIndex = 95
        Me.Button_Connect_Via_HTTP.Text = "Connettiti Via HTTP"
        Me.Button_Connect_Via_HTTP.UseVisualStyleBackColor = True
        '
        'CheckBoxLeggiTuttiIParametri
        '
        Me.CheckBoxLeggiTuttiIParametri.AutoSize = True
        Me.CheckBoxLeggiTuttiIParametri.Location = New System.Drawing.Point(413, 146)
        Me.CheckBoxLeggiTuttiIParametri.Name = "CheckBoxLeggiTuttiIParametri"
        Me.CheckBoxLeggiTuttiIParametri.Size = New System.Drawing.Size(103, 17)
        Me.CheckBoxLeggiTuttiIParametri.TabIndex = 96
        Me.CheckBoxLeggiTuttiIParametri.Text = "Leggi Tutti i Par."
        Me.CheckBoxLeggiTuttiIParametri.UseVisualStyleBackColor = True
        '
        'AggiornaManualmenteDati
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.CheckBoxLeggiTuttiIParametri)
        Me.Controls.Add(Me.Button_Connect_Via_HTTP)
        Me.Controls.Add(Me.CheckBox_Usa_LI_TCPIP_Get_Ind)
        Me.Controls.Add(Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man)
        Me.Controls.Add(Me.CheckBox_DL_Vs_PC_Invia_Configurazione)
        Me.Controls.Add(Me.CheckBox_DL_Vs_PC_Leggi_Configurazione)
        Me.Controls.Add(Me.Label_DL_Vs_PC)
        Me.Controls.Add(Me.Label_PC_Vs_DL)
        Me.Controls.Add(Me.Button_Sincronizza_Dati_Man)
        Me.Controls.Add(Me.CheckBoxInviaTuttiIParametri)
        Me.Controls.Add(Me.Button_Leggi_Configurazione)
        Me.Controls.Add(Me.Button_Invia_Configurazione)
        Me.Controls.Add(Me.Label_Connected)
        Me.Controls.Add(Me.Button_Stop_TCPIP)
        Me.Controls.Add(Me.Button_Scarica_Dati_Man)
        Me.Controls.Add(Me.ListBox_Remote_Connected)
        Me.Controls.Add(Me.ComboBox_LI_ID)
        Me.Controls.Add(Me.Label_LI_ID)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Name = "AggiornaManualmenteDati"
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.ListBox_Remote_Connected, 0)
        Me.Controls.SetChildIndex(Me.Button_Scarica_Dati_Man, 0)
        Me.Controls.SetChildIndex(Me.Button_Stop_TCPIP, 0)
        Me.Controls.SetChildIndex(Me.Label_Connected, 0)
        Me.Controls.SetChildIndex(Me.Button_Invia_Configurazione, 0)
        Me.Controls.SetChildIndex(Me.Button_Leggi_Configurazione, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxInviaTuttiIParametri, 0)
        Me.Controls.SetChildIndex(Me.Button_Sincronizza_Dati_Man, 0)
        Me.Controls.SetChildIndex(Me.Label_PC_Vs_DL, 0)
        Me.Controls.SetChildIndex(Me.Label_DL_Vs_PC, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_DL_Vs_PC_Leggi_Configurazione, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_DL_Vs_PC_Invia_Configurazione, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_Usa_LI_TCPIP_Get_Ind, 0)
        Me.Controls.SetChildIndex(Me.Button_Connect_Via_HTTP, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxLeggiTuttiIParametri, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_LI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_LI_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents ListBox_Remote_Connected As System.Windows.Forms.ListBox
    Friend WithEvents Button_Scarica_Dati_Man As System.Windows.Forms.Button
    Friend WithEvents Button_Stop_TCPIP As System.Windows.Forms.Button
    Friend WithEvents Label_Connected As System.Windows.Forms.Label
    Friend WithEvents Button_Invia_Configurazione As System.Windows.Forms.Button
    Friend WithEvents TimerTCPIP As System.Windows.Forms.Timer
    Friend WithEvents Button_Leggi_Configurazione As System.Windows.Forms.Button
    Friend WithEvents CheckBoxInviaTuttiIParametri As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Sincronizza_Dati_Man As System.Windows.Forms.Button
    Friend WithEvents Label_PC_Vs_DL As System.Windows.Forms.Label
    Friend WithEvents Label_DL_Vs_PC As System.Windows.Forms.Label
    Friend WithEvents CheckBox_DL_Vs_PC_Leggi_Configurazione As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_DL_Vs_PC_Invia_Configurazione As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_DL_Vs_PC_Sincronizza_Dati_Man As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Usa_LI_TCPIP_Get_Ind As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Connect_Via_HTTP As System.Windows.Forms.Button
    Friend WithEvents CheckBoxLeggiTuttiIParametri As System.Windows.Forms.CheckBox

End Class
