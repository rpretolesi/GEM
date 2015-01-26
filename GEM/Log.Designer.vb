<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class log
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
        Me.CheckBoxAbilitaAggiornamento = New System.Windows.Forms.CheckBox
        Me.Label_A = New System.Windows.Forms.Label
        Me.Label_Da = New System.Windows.Forms.Label
        Me.DateTimePicker_STOP = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_START = New System.Windows.Forms.DateTimePicker
        Me.RadioButtonVisualizzaTutto = New System.Windows.Forms.RadioButton
        Me.RadioButtonVisualizzaSoloITotali = New System.Windows.Forms.RadioButton
        Me.RadioButtonVisualizzaSoloGliAllarmi = New System.Windows.Forms.RadioButton
        Me.Timer_1 = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBox_VisualizzaUltimi100Eventi = New System.Windows.Forms.CheckBox
        Me.Button_Elimina_Storico_Fino_A = New System.Windows.Forms.Button
        Me.TextBox_LIID = New System.Windows.Forms.TextBox
        Me.CheckBox_FiltraPerLIID = New System.Windows.Forms.CheckBox
        Me.Label_IDDL = New System.Windows.Forms.Label
        Me.Label_Nr_DL_Con_Almeno_1_Connessione = New System.Windows.Forms.Label
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato = New System.Windows.Forms.Label
        Me.TextBox_LG_RemoteAddress = New System.Windows.Forms.TextBox
        Me.Label_LG_RemoteAddress = New System.Windows.Forms.Label
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail = New System.Windows.Forms.RadioButton
        Me.Label_Memory_Details = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'CheckBoxAbilitaAggiornamento
        '
        Me.CheckBoxAbilitaAggiornamento.AutoSize = True
        Me.CheckBoxAbilitaAggiornamento.Checked = True
        Me.CheckBoxAbilitaAggiornamento.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAbilitaAggiornamento.Location = New System.Drawing.Point(12, 264)
        Me.CheckBoxAbilitaAggiornamento.Name = "CheckBoxAbilitaAggiornamento"
        Me.CheckBoxAbilitaAggiornamento.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxAbilitaAggiornamento.TabIndex = 111
        Me.CheckBoxAbilitaAggiornamento.Text = "Abilita Aggiornamento Automatico"
        Me.CheckBoxAbilitaAggiornamento.UseVisualStyleBackColor = True
        '
        'Label_A
        '
        Me.Label_A.AutoSize = True
        Me.Label_A.Location = New System.Drawing.Point(249, 228)
        Me.Label_A.Name = "Label_A"
        Me.Label_A.Size = New System.Drawing.Size(17, 13)
        Me.Label_A.TabIndex = 119
        Me.Label_A.Text = "A:"
        '
        'Label_Da
        '
        Me.Label_Da.AutoSize = True
        Me.Label_Da.Location = New System.Drawing.Point(9, 227)
        Me.Label_Da.Name = "Label_Da"
        Me.Label_Da.Size = New System.Drawing.Size(24, 13)
        Me.Label_Da.TabIndex = 118
        Me.Label_Da.Text = "Da:"
        '
        'DateTimePicker_STOP
        '
        Me.DateTimePicker_STOP.CustomFormat = "'Data:' dd MM yyyy  '- Ora:' HH:mm:ss"
        Me.DateTimePicker_STOP.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_STOP.Location = New System.Drawing.Point(272, 225)
        Me.DateTimePicker_STOP.Name = "DateTimePicker_STOP"
        Me.DateTimePicker_STOP.Size = New System.Drawing.Size(201, 20)
        Me.DateTimePicker_STOP.TabIndex = 117
        '
        'DateTimePicker_START
        '
        Me.DateTimePicker_START.CustomFormat = "'Data:' dd MM yyyy  '- Ora:' HH:mm:ss"
        Me.DateTimePicker_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_START.Location = New System.Drawing.Point(39, 224)
        Me.DateTimePicker_START.Name = "DateTimePicker_START"
        Me.DateTimePicker_START.Size = New System.Drawing.Size(201, 20)
        Me.DateTimePicker_START.TabIndex = 116
        '
        'RadioButtonVisualizzaTutto
        '
        Me.RadioButtonVisualizzaTutto.AutoSize = True
        Me.RadioButtonVisualizzaTutto.Checked = True
        Me.RadioButtonVisualizzaTutto.Location = New System.Drawing.Point(12, 88)
        Me.RadioButtonVisualizzaTutto.Name = "RadioButtonVisualizzaTutto"
        Me.RadioButtonVisualizzaTutto.Size = New System.Drawing.Size(99, 17)
        Me.RadioButtonVisualizzaTutto.TabIndex = 127
        Me.RadioButtonVisualizzaTutto.TabStop = True
        Me.RadioButtonVisualizzaTutto.Text = "Visualizza Tutto"
        Me.RadioButtonVisualizzaTutto.UseVisualStyleBackColor = True
        '
        'RadioButtonVisualizzaSoloITotali
        '
        Me.RadioButtonVisualizzaSoloITotali.AutoSize = True
        Me.RadioButtonVisualizzaSoloITotali.Location = New System.Drawing.Point(286, 88)
        Me.RadioButtonVisualizzaSoloITotali.Name = "RadioButtonVisualizzaSoloITotali"
        Me.RadioButtonVisualizzaSoloITotali.Size = New System.Drawing.Size(127, 17)
        Me.RadioButtonVisualizzaSoloITotali.TabIndex = 126
        Me.RadioButtonVisualizzaSoloITotali.TabStop = True
        Me.RadioButtonVisualizzaSoloITotali.Text = "Visualizza solo i Totali"
        Me.RadioButtonVisualizzaSoloITotali.UseVisualStyleBackColor = True
        '
        'RadioButtonVisualizzaSoloGliAllarmi
        '
        Me.RadioButtonVisualizzaSoloGliAllarmi.AutoSize = True
        Me.RadioButtonVisualizzaSoloGliAllarmi.Location = New System.Drawing.Point(139, 88)
        Me.RadioButtonVisualizzaSoloGliAllarmi.Name = "RadioButtonVisualizzaSoloGliAllarmi"
        Me.RadioButtonVisualizzaSoloGliAllarmi.Size = New System.Drawing.Size(139, 17)
        Me.RadioButtonVisualizzaSoloGliAllarmi.TabIndex = 125
        Me.RadioButtonVisualizzaSoloGliAllarmi.TabStop = True
        Me.RadioButtonVisualizzaSoloGliAllarmi.Text = "Visualizza solo gli Allarmi"
        Me.RadioButtonVisualizzaSoloGliAllarmi.UseVisualStyleBackColor = True
        '
        'Timer_1
        '
        Me.Timer_1.Interval = 3000
        '
        'CheckBox_VisualizzaUltimi100Eventi
        '
        Me.CheckBox_VisualizzaUltimi100Eventi.AutoSize = True
        Me.CheckBox_VisualizzaUltimi100Eventi.Checked = True
        Me.CheckBox_VisualizzaUltimi100Eventi.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_VisualizzaUltimi100Eventi.Location = New System.Drawing.Point(202, 265)
        Me.CheckBox_VisualizzaUltimi100Eventi.Name = "CheckBox_VisualizzaUltimi100Eventi"
        Me.CheckBox_VisualizzaUltimi100Eventi.Size = New System.Drawing.Size(105, 17)
        Me.CheckBox_VisualizzaUltimi100Eventi.TabIndex = 128
        Me.CheckBox_VisualizzaUltimi100Eventi.Text = "Ultimi 100 Eventi"
        Me.CheckBox_VisualizzaUltimi100Eventi.UseVisualStyleBackColor = True
        '
        'Button_Elimina_Storico_Fino_A
        '
        Me.Button_Elimina_Storico_Fino_A.Location = New System.Drawing.Point(272, 196)
        Me.Button_Elimina_Storico_Fino_A.Name = "Button_Elimina_Storico_Fino_A"
        Me.Button_Elimina_Storico_Fino_A.Size = New System.Drawing.Size(201, 23)
        Me.Button_Elimina_Storico_Fino_A.TabIndex = 129
        Me.Button_Elimina_Storico_Fino_A.Text = "Elimina Storico fino alla Data -> ""A:"""
        Me.Button_Elimina_Storico_Fino_A.UseVisualStyleBackColor = True
        '
        'TextBox_LIID
        '
        Me.TextBox_LIID.Location = New System.Drawing.Point(139, 31)
        Me.TextBox_LIID.Name = "TextBox_LIID"
        Me.TextBox_LIID.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LIID.TabIndex = 130
        '
        'CheckBox_FiltraPerLIID
        '
        Me.CheckBox_FiltraPerLIID.AutoSize = True
        Me.CheckBox_FiltraPerLIID.Location = New System.Drawing.Point(286, 31)
        Me.CheckBox_FiltraPerLIID.Name = "CheckBox_FiltraPerLIID"
        Me.CheckBox_FiltraPerLIID.Size = New System.Drawing.Size(139, 17)
        Me.CheckBox_FiltraPerLIID.TabIndex = 131
        Me.CheckBox_FiltraPerLIID.Text = "Filtra per ID DataLogger"
        Me.CheckBox_FiltraPerLIID.UseVisualStyleBackColor = True
        '
        'Label_IDDL
        '
        Me.Label_IDDL.AutoSize = True
        Me.Label_IDDL.Location = New System.Drawing.Point(9, 34)
        Me.Label_IDDL.Name = "Label_IDDL"
        Me.Label_IDDL.Size = New System.Drawing.Size(80, 13)
        Me.Label_IDDL.TabIndex = 132
        Me.Label_IDDL.Text = "ID DataLogger:"
        '
        'Label_Nr_DL_Con_Almeno_1_Connessione
        '
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.AutoSize = True
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.Location = New System.Drawing.Point(9, 247)
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.Name = "Label_Nr_DL_Con_Almeno_1_Connessione"
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.Size = New System.Drawing.Size(294, 13)
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.TabIndex = 133
        Me.Label_Nr_DL_Con_Almeno_1_Connessione.Text = "Nr Totale di DL che hanno instaurato almeno 1 connessione:"
        '
        'Label_Nr_DL_Con_Almeno_1_Connessione_Dato
        '
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.AutoSize = True
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Location = New System.Drawing.Point(309, 247)
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Name = "Label_Nr_DL_Con_Almeno_1_Connessione_Dato"
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Size = New System.Drawing.Size(13, 13)
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.TabIndex = 134
        Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Text = "--"
        '
        'TextBox_LG_RemoteAddress
        '
        Me.TextBox_LG_RemoteAddress.Location = New System.Drawing.Point(139, 56)
        Me.TextBox_LG_RemoteAddress.Name = "TextBox_LG_RemoteAddress"
        Me.TextBox_LG_RemoteAddress.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LG_RemoteAddress.TabIndex = 135
        '
        'Label_LG_RemoteAddress
        '
        Me.Label_LG_RemoteAddress.AutoSize = True
        Me.Label_LG_RemoteAddress.Location = New System.Drawing.Point(9, 59)
        Me.Label_LG_RemoteAddress.Name = "Label_LG_RemoteAddress"
        Me.Label_LG_RemoteAddress.Size = New System.Drawing.Size(127, 13)
        Me.Label_LG_RemoteAddress.TabIndex = 136
        Me.Label_LG_RemoteAddress.Text = "Indirizzo TCP/IP Remoto:"
        '
        'RadioButtonVisualizzaSoloInvioReportPerEmail
        '
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.AutoSize = True
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Location = New System.Drawing.Point(428, 88)
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Name = "RadioButtonVisualizzaSoloInvioReportPerEmail"
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Size = New System.Drawing.Size(200, 17)
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.TabIndex = 137
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.TabStop = True
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Text = "Visualizza solo Invio Report per Email"
        Me.RadioButtonVisualizzaSoloInvioReportPerEmail.UseVisualStyleBackColor = True
        '
        'Label_Memory_Details
        '
        Me.Label_Memory_Details.AutoSize = True
        Me.Label_Memory_Details.Location = New System.Drawing.Point(12, 117)
        Me.Label_Memory_Details.Name = "Label_Memory_Details"
        Me.Label_Memory_Details.Size = New System.Drawing.Size(89, 13)
        Me.Label_Memory_Details.TabIndex = 138
        Me.Label_Memory_Details.Text = "Dettagli Memoria."
        '
        'log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Label_Memory_Details)
        Me.Controls.Add(Me.RadioButtonVisualizzaSoloInvioReportPerEmail)
        Me.Controls.Add(Me.Label_LG_RemoteAddress)
        Me.Controls.Add(Me.TextBox_LG_RemoteAddress)
        Me.Controls.Add(Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato)
        Me.Controls.Add(Me.Label_Nr_DL_Con_Almeno_1_Connessione)
        Me.Controls.Add(Me.Label_IDDL)
        Me.Controls.Add(Me.CheckBox_FiltraPerLIID)
        Me.Controls.Add(Me.TextBox_LIID)
        Me.Controls.Add(Me.Button_Elimina_Storico_Fino_A)
        Me.Controls.Add(Me.CheckBox_VisualizzaUltimi100Eventi)
        Me.Controls.Add(Me.RadioButtonVisualizzaTutto)
        Me.Controls.Add(Me.RadioButtonVisualizzaSoloITotali)
        Me.Controls.Add(Me.RadioButtonVisualizzaSoloGliAllarmi)
        Me.Controls.Add(Me.Label_A)
        Me.Controls.Add(Me.Label_Da)
        Me.Controls.Add(Me.DateTimePicker_STOP)
        Me.Controls.Add(Me.DateTimePicker_START)
        Me.Controls.Add(Me.CheckBoxAbilitaAggiornamento)
        Me.Name = "log"
        Me.Controls.SetChildIndex(Me.CheckBoxAbilitaAggiornamento, 0)
        Me.Controls.SetChildIndex(Me.DateTimePicker_START, 0)
        Me.Controls.SetChildIndex(Me.DateTimePicker_STOP, 0)
        Me.Controls.SetChildIndex(Me.Label_Da, 0)
        Me.Controls.SetChildIndex(Me.Label_A, 0)
        Me.Controls.SetChildIndex(Me.RadioButtonVisualizzaSoloGliAllarmi, 0)
        Me.Controls.SetChildIndex(Me.RadioButtonVisualizzaSoloITotali, 0)
        Me.Controls.SetChildIndex(Me.RadioButtonVisualizzaTutto, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_VisualizzaUltimi100Eventi, 0)
        Me.Controls.SetChildIndex(Me.Button_Elimina_Storico_Fino_A, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIID, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_FiltraPerLIID, 0)
        Me.Controls.SetChildIndex(Me.Label_IDDL, 0)
        Me.Controls.SetChildIndex(Me.Label_Nr_DL_Con_Almeno_1_Connessione, 0)
        Me.Controls.SetChildIndex(Me.Label_Nr_DL_Con_Almeno_1_Connessione_Dato, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LG_RemoteAddress, 0)
        Me.Controls.SetChildIndex(Me.Label_LG_RemoteAddress, 0)
        Me.Controls.SetChildIndex(Me.RadioButtonVisualizzaSoloInvioReportPerEmail, 0)
        Me.Controls.SetChildIndex(Me.Label_Memory_Details, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxAbilitaAggiornamento As System.Windows.Forms.CheckBox
    Friend WithEvents Label_A As System.Windows.Forms.Label
    Friend WithEvents Label_Da As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker_STOP As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_START As System.Windows.Forms.DateTimePicker
    Friend WithEvents RadioButtonVisualizzaTutto As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVisualizzaSoloITotali As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVisualizzaSoloGliAllarmi As System.Windows.Forms.RadioButton
    Friend WithEvents Timer_1 As System.Windows.Forms.Timer
    Friend WithEvents CheckBox_VisualizzaUltimi100Eventi As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Elimina_Storico_Fino_A As System.Windows.Forms.Button
    Friend WithEvents TextBox_LIID As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox_FiltraPerLIID As System.Windows.Forms.CheckBox
    Friend WithEvents Label_IDDL As System.Windows.Forms.Label
    Friend WithEvents Label_Nr_DL_Con_Almeno_1_Connessione As System.Windows.Forms.Label
    Friend WithEvents Label_Nr_DL_Con_Almeno_1_Connessione_Dato As System.Windows.Forms.Label
    Friend WithEvents TextBox_LG_RemoteAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label_LG_RemoteAddress As System.Windows.Forms.Label
    Friend WithEvents RadioButtonVisualizzaSoloInvioReportPerEmail As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Memory_Details As System.Windows.Forms.Label

End Class
