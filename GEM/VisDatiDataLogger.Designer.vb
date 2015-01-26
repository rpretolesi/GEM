<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisDatiDataLogger
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
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.ComboBox_LI_ID = New System.Windows.Forms.ComboBox
        Me.Label_LI_ID = New System.Windows.Forms.Label
        Me.CheckBoxAbilitaAggiornamento = New System.Windows.Forms.CheckBox
        Me.Timer_1 = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti = New System.Windows.Forms.CheckBox
        Me.DateTimePicker_START = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_STOP = New System.Windows.Forms.DateTimePicker
        Me.Label_Da = New System.Windows.Forms.Label
        Me.Label_A = New System.Windows.Forms.Label
        Me.Label_EnergiaProdotta = New System.Windows.Forms.Label
        Me.TextBox_EnergiaProdotta = New System.Windows.Forms.TextBox
        Me.TextBox_EnergiaConsumata = New System.Windows.Forms.TextBox
        Me.Label_EnergiaConsumata = New System.Windows.Forms.Label
        Me.Labelx_EnergiaDaSensoriDiIrragg = New System.Windows.Forms.Label
        Me.TextBox_EnergiaDaSensoriDiIrragg = New System.Windows.Forms.TextBox
        Me.Label_EnergiaDaInverter = New System.Windows.Forms.Label
        Me.TextBox_EnergiaDaInverter = New System.Windows.Forms.TextBox
        Me.Label_EnergiaDaStringhe = New System.Windows.Forms.Label
        Me.TextBox_EnergiaDaStringhe = New System.Windows.Forms.TextBox
        Me.Button_ST = New System.Windows.Forms.Button
        Me.Button_IT = New System.Windows.Forms.Button
        Me.TextBox_REND_SI_CDP = New System.Windows.Forms.TextBox
        Me.TextBox_REND_SI_CDP_REF = New System.Windows.Forms.TextBox
        Me.TextBox_REND_INV_CDP_REF = New System.Windows.Forms.TextBox
        Me.TextBox_REND_INV_CDP = New System.Windows.Forms.TextBox
        Me.TextBox_REND_STR_CDP_REF = New System.Windows.Forms.TextBox
        Me.TextBox_REND_STR_CDP = New System.Windows.Forms.TextBox
        Me.Label_REND_SI_CDP_REF = New System.Windows.Forms.Label
        Me.Label_REND_INV_CDP_REF = New System.Windows.Forms.Label
        Me.Label_REND_STR_CDP_REF = New System.Windows.Forms.Label
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg = New System.Windows.Forms.TextBox
        Me.Label_MediaDaSensoriDiIrragg = New System.Windows.Forms.Label
        Me.Button_Report_All_Plant = New System.Windows.Forms.Button
        Me.Label_Ultimo_Agg_Ora_DL_LOC = New System.Windows.Forms.Label
        Me.TextBox_ORA_LOC = New System.Windows.Forms.TextBox
        Me.TextBox_ORA_DL = New System.Windows.Forms.TextBox
        Me.Label_ORA_DL = New System.Windows.Forms.Label
        Me.Label_ORA_LOCALE = New System.Windows.Forms.Label
        Me.Button_Send_Email_Report_All_Plant = New System.Windows.Forms.Button
        Me.TextBox_P1 = New System.Windows.Forms.TextBox
        Me.Label_P11 = New System.Windows.Forms.Label
        Me.Label_P12 = New System.Windows.Forms.Label
        Me.TextBox_EnergiaProdottaMesePrec = New System.Windows.Forms.TextBox
        Me.Label_EnergiaProdottaMesePrec_1 = New System.Windows.Forms.Label
        Me.Label_EnergiaProdottaMesePrec_2 = New System.Windows.Forms.Label
        Me.Button_DL = New System.Windows.Forms.Button
        Me.TextBox_Performance_Ratio = New System.Windows.Forms.TextBox
        Me.Label_Performance_Ratio = New System.Windows.Forms.Label
        Me.TextBox_HG = New System.Windows.Forms.TextBox
        Me.Label_HG = New System.Windows.Forms.Label
        Me.TextBox_MediaDaSondeDiTempPann = New System.Windows.Forms.TextBox
        Me.Label_MediaDaSondeDiTempPann = New System.Windows.Forms.Label
        Me.Button_DatiStatistici = New System.Windows.Forms.Button
        Me.Label_MaxProduzione = New System.Windows.Forms.Label
        Me.TextBox_MaxProduzione = New System.Windows.Forms.TextBox
        Me.Button_DataMaxProduzione = New System.Windows.Forms.Button
        Me.Button_DatiDL = New System.Windows.Forms.Button
        Me.Button_UDTMaxProduzione = New System.Windows.Forms.Button
        Me.Button_Report_Riass_Giorni = New System.Windows.Forms.Button
        Me.Button_Report_Riass_Mesi = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(243, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(285, 21)
        Me.ComboBox_C_ID.TabIndex = 71
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(5, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 70
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(243, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(285, 21)
        Me.ComboBox_IDP_ID.TabIndex = 69
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(5, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 68
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'ComboBox_LI_ID
        '
        Me.ComboBox_LI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_LI_ID.FormattingEnabled = True
        Me.ComboBox_LI_ID.Location = New System.Drawing.Point(243, 82)
        Me.ComboBox_LI_ID.Name = "ComboBox_LI_ID"
        Me.ComboBox_LI_ID.Size = New System.Drawing.Size(285, 21)
        Me.ComboBox_LI_ID.TabIndex = 109
        '
        'Label_LI_ID
        '
        Me.Label_LI_ID.AutoSize = True
        Me.Label_LI_ID.Location = New System.Drawing.Point(5, 85)
        Me.Label_LI_ID.Name = "Label_LI_ID"
        Me.Label_LI_ID.Size = New System.Drawing.Size(125, 13)
        Me.Label_LI_ID.TabIndex = 108
        Me.Label_LI_ID.Text = "Numero Logger Installato"
        '
        'CheckBoxAbilitaAggiornamento
        '
        Me.CheckBoxAbilitaAggiornamento.AutoSize = True
        Me.CheckBoxAbilitaAggiornamento.Location = New System.Drawing.Point(12, 264)
        Me.CheckBoxAbilitaAggiornamento.Name = "CheckBoxAbilitaAggiornamento"
        Me.CheckBoxAbilitaAggiornamento.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxAbilitaAggiornamento.TabIndex = 110
        Me.CheckBoxAbilitaAggiornamento.Text = "Abilita Aggiornamento Automatico"
        Me.CheckBoxAbilitaAggiornamento.UseVisualStyleBackColor = True
        '
        'Timer_1
        '
        Me.Timer_1.Interval = 60000
        '
        'CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti
        '
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.AutoSize = True
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Location = New System.Drawing.Point(202, 264)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Name = "CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDi" & _
            "TuttiIClienti"
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Size = New System.Drawing.Size(439, 17)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.TabIndex = 111
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Text = "Visualizza solo gli Allarmi Di Tutti i Datalogger configurati di tutti gli Impian" & _
            "ti di tutti i Clienti"
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.UseVisualStyleBackColor = True
        '
        'DateTimePicker_START
        '
        Me.DateTimePicker_START.CustomFormat = "'Data:' dd MM yyyy  '- Ora:' HH:mm:ss"
        Me.DateTimePicker_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_START.Location = New System.Drawing.Point(543, 109)
        Me.DateTimePicker_START.Name = "DateTimePicker_START"
        Me.DateTimePicker_START.Size = New System.Drawing.Size(201, 20)
        Me.DateTimePicker_START.TabIndex = 112
        '
        'DateTimePicker_STOP
        '
        Me.DateTimePicker_STOP.CustomFormat = "'Data:' dd MM yyyy  '- Ora:' HH:mm:ss"
        Me.DateTimePicker_STOP.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_STOP.Location = New System.Drawing.Point(543, 135)
        Me.DateTimePicker_STOP.Name = "DateTimePicker_STOP"
        Me.DateTimePicker_STOP.Size = New System.Drawing.Size(201, 20)
        Me.DateTimePicker_STOP.TabIndex = 113
        '
        'Label_Da
        '
        Me.Label_Da.AutoSize = True
        Me.Label_Da.Location = New System.Drawing.Point(513, 111)
        Me.Label_Da.Name = "Label_Da"
        Me.Label_Da.Size = New System.Drawing.Size(24, 13)
        Me.Label_Da.TabIndex = 114
        Me.Label_Da.Text = "Da:"
        '
        'Label_A
        '
        Me.Label_A.AutoSize = True
        Me.Label_A.Location = New System.Drawing.Point(520, 137)
        Me.Label_A.Name = "Label_A"
        Me.Label_A.Size = New System.Drawing.Size(17, 13)
        Me.Label_A.TabIndex = 115
        Me.Label_A.Text = "A:"
        '
        'Label_EnergiaProdotta
        '
        Me.Label_EnergiaProdotta.AutoSize = True
        Me.Label_EnergiaProdotta.Location = New System.Drawing.Point(5, 215)
        Me.Label_EnergiaProdotta.Name = "Label_EnergiaProdotta"
        Me.Label_EnergiaProdotta.Size = New System.Drawing.Size(230, 13)
        Me.Label_EnergiaProdotta.TabIndex = 117
        Me.Label_EnergiaProdotta.Text = "En. Prod. Da Cont. Di Prod. - 1+2 - 1 - 2 - (Kwh)"
        '
        'TextBox_EnergiaProdotta
        '
        Me.TextBox_EnergiaProdotta.Location = New System.Drawing.Point(243, 212)
        Me.TextBox_EnergiaProdotta.Name = "TextBox_EnergiaProdotta"
        Me.TextBox_EnergiaProdotta.ReadOnly = True
        Me.TextBox_EnergiaProdotta.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_EnergiaProdotta.TabIndex = 118
        '
        'TextBox_EnergiaConsumata
        '
        Me.TextBox_EnergiaConsumata.Location = New System.Drawing.Point(243, 108)
        Me.TextBox_EnergiaConsumata.Name = "TextBox_EnergiaConsumata"
        Me.TextBox_EnergiaConsumata.ReadOnly = True
        Me.TextBox_EnergiaConsumata.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_EnergiaConsumata.TabIndex = 120
        '
        'Label_EnergiaConsumata
        '
        Me.Label_EnergiaConsumata.AutoSize = True
        Me.Label_EnergiaConsumata.Location = New System.Drawing.Point(5, 111)
        Me.Label_EnergiaConsumata.Name = "Label_EnergiaConsumata"
        Me.Label_EnergiaConsumata.Size = New System.Drawing.Size(152, 13)
        Me.Label_EnergiaConsumata.TabIndex = 119
        Me.Label_EnergiaConsumata.Text = "En. Cons. Da Cont. Enel (Kwh)"
        '
        'Labelx_EnergiaDaSensoriDiIrragg
        '
        Me.Labelx_EnergiaDaSensoriDiIrragg.AutoSize = True
        Me.Labelx_EnergiaDaSensoriDiIrragg.Location = New System.Drawing.Point(5, 137)
        Me.Labelx_EnergiaDaSensoriDiIrragg.Name = "Labelx_EnergiaDaSensoriDiIrragg"
        Me.Labelx_EnergiaDaSensoriDiIrragg.Size = New System.Drawing.Size(169, 13)
        Me.Labelx_EnergiaDaSensoriDiIrragg.TabIndex = 121
        Me.Labelx_EnergiaDaSensoriDiIrragg.Text = "En. Prod. Da Sensori Irragg. (Kwh)"
        '
        'TextBox_EnergiaDaSensoriDiIrragg
        '
        Me.TextBox_EnergiaDaSensoriDiIrragg.Location = New System.Drawing.Point(243, 134)
        Me.TextBox_EnergiaDaSensoriDiIrragg.Name = "TextBox_EnergiaDaSensoriDiIrragg"
        Me.TextBox_EnergiaDaSensoriDiIrragg.ReadOnly = True
        Me.TextBox_EnergiaDaSensoriDiIrragg.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_EnergiaDaSensoriDiIrragg.TabIndex = 122
        '
        'Label_EnergiaDaInverter
        '
        Me.Label_EnergiaDaInverter.AutoSize = True
        Me.Label_EnergiaDaInverter.Location = New System.Drawing.Point(5, 189)
        Me.Label_EnergiaDaInverter.Name = "Label_EnergiaDaInverter"
        Me.Label_EnergiaDaInverter.Size = New System.Drawing.Size(137, 13)
        Me.Label_EnergiaDaInverter.TabIndex = 123
        Me.Label_EnergiaDaInverter.Text = "En. Prod. Da Inverter (Kwh)"
        '
        'TextBox_EnergiaDaInverter
        '
        Me.TextBox_EnergiaDaInverter.Location = New System.Drawing.Point(243, 186)
        Me.TextBox_EnergiaDaInverter.Name = "TextBox_EnergiaDaInverter"
        Me.TextBox_EnergiaDaInverter.ReadOnly = True
        Me.TextBox_EnergiaDaInverter.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_EnergiaDaInverter.TabIndex = 124
        '
        'Label_EnergiaDaStringhe
        '
        Me.Label_EnergiaDaStringhe.AutoSize = True
        Me.Label_EnergiaDaStringhe.Location = New System.Drawing.Point(5, 163)
        Me.Label_EnergiaDaStringhe.Name = "Label_EnergiaDaStringhe"
        Me.Label_EnergiaDaStringhe.Size = New System.Drawing.Size(140, 13)
        Me.Label_EnergiaDaStringhe.TabIndex = 125
        Me.Label_EnergiaDaStringhe.Text = "En. Prod. Da Stringhe (Kwh)"
        '
        'TextBox_EnergiaDaStringhe
        '
        Me.TextBox_EnergiaDaStringhe.Location = New System.Drawing.Point(243, 160)
        Me.TextBox_EnergiaDaStringhe.Name = "TextBox_EnergiaDaStringhe"
        Me.TextBox_EnergiaDaStringhe.ReadOnly = True
        Me.TextBox_EnergiaDaStringhe.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_EnergiaDaStringhe.TabIndex = 126
        '
        'Button_ST
        '
        Me.Button_ST.Location = New System.Drawing.Point(624, 237)
        Me.Button_ST.Name = "Button_ST"
        Me.Button_ST.Size = New System.Drawing.Size(75, 22)
        Me.Button_ST.TabIndex = 128
        Me.Button_ST.Text = "String Tester"
        Me.Button_ST.UseVisualStyleBackColor = True
        '
        'Button_IT
        '
        Me.Button_IT.Location = New System.Drawing.Point(705, 237)
        Me.Button_IT.Name = "Button_IT"
        Me.Button_IT.Size = New System.Drawing.Size(75, 22)
        Me.Button_IT.TabIndex = 129
        Me.Button_IT.Text = "Inverter Tester"
        Me.Button_IT.UseVisualStyleBackColor = True
        '
        'TextBox_REND_SI_CDP
        '
        Me.TextBox_REND_SI_CDP.Location = New System.Drawing.Point(396, 134)
        Me.TextBox_REND_SI_CDP.Name = "TextBox_REND_SI_CDP"
        Me.TextBox_REND_SI_CDP.ReadOnly = True
        Me.TextBox_REND_SI_CDP.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_SI_CDP.TabIndex = 130
        '
        'TextBox_REND_SI_CDP_REF
        '
        Me.TextBox_REND_SI_CDP_REF.Location = New System.Drawing.Point(350, 134)
        Me.TextBox_REND_SI_CDP_REF.Name = "TextBox_REND_SI_CDP_REF"
        Me.TextBox_REND_SI_CDP_REF.ReadOnly = True
        Me.TextBox_REND_SI_CDP_REF.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_SI_CDP_REF.TabIndex = 131
        '
        'TextBox_REND_INV_CDP_REF
        '
        Me.TextBox_REND_INV_CDP_REF.Location = New System.Drawing.Point(350, 186)
        Me.TextBox_REND_INV_CDP_REF.Name = "TextBox_REND_INV_CDP_REF"
        Me.TextBox_REND_INV_CDP_REF.ReadOnly = True
        Me.TextBox_REND_INV_CDP_REF.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_INV_CDP_REF.TabIndex = 133
        '
        'TextBox_REND_INV_CDP
        '
        Me.TextBox_REND_INV_CDP.Location = New System.Drawing.Point(396, 186)
        Me.TextBox_REND_INV_CDP.Name = "TextBox_REND_INV_CDP"
        Me.TextBox_REND_INV_CDP.ReadOnly = True
        Me.TextBox_REND_INV_CDP.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_INV_CDP.TabIndex = 132
        '
        'TextBox_REND_STR_CDP_REF
        '
        Me.TextBox_REND_STR_CDP_REF.Location = New System.Drawing.Point(350, 160)
        Me.TextBox_REND_STR_CDP_REF.Name = "TextBox_REND_STR_CDP_REF"
        Me.TextBox_REND_STR_CDP_REF.ReadOnly = True
        Me.TextBox_REND_STR_CDP_REF.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_STR_CDP_REF.TabIndex = 135
        '
        'TextBox_REND_STR_CDP
        '
        Me.TextBox_REND_STR_CDP.Location = New System.Drawing.Point(396, 160)
        Me.TextBox_REND_STR_CDP.Name = "TextBox_REND_STR_CDP"
        Me.TextBox_REND_STR_CDP.ReadOnly = True
        Me.TextBox_REND_STR_CDP.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_REND_STR_CDP.TabIndex = 134
        '
        'Label_REND_SI_CDP_REF
        '
        Me.Label_REND_SI_CDP_REF.AutoSize = True
        Me.Label_REND_SI_CDP_REF.Location = New System.Drawing.Point(317, 137)
        Me.Label_REND_SI_CDP_REF.Name = "Label_REND_SI_CDP_REF"
        Me.Label_REND_SI_CDP_REF.Size = New System.Drawing.Size(27, 13)
        Me.Label_REND_SI_CDP_REF.TabIndex = 136
        Me.Label_REND_SI_CDP_REF.Text = "Ref."
        '
        'Label_REND_INV_CDP_REF
        '
        Me.Label_REND_INV_CDP_REF.AutoSize = True
        Me.Label_REND_INV_CDP_REF.Location = New System.Drawing.Point(317, 189)
        Me.Label_REND_INV_CDP_REF.Name = "Label_REND_INV_CDP_REF"
        Me.Label_REND_INV_CDP_REF.Size = New System.Drawing.Size(27, 13)
        Me.Label_REND_INV_CDP_REF.TabIndex = 137
        Me.Label_REND_INV_CDP_REF.Text = "Ref."
        '
        'Label_REND_STR_CDP_REF
        '
        Me.Label_REND_STR_CDP_REF.AutoSize = True
        Me.Label_REND_STR_CDP_REF.Location = New System.Drawing.Point(317, 163)
        Me.Label_REND_STR_CDP_REF.Name = "Label_REND_STR_CDP_REF"
        Me.Label_REND_STR_CDP_REF.Size = New System.Drawing.Size(27, 13)
        Me.Label_REND_STR_CDP_REF.TabIndex = 138
        Me.Label_REND_STR_CDP_REF.Text = "Ref."
        '
        'TextBox_EnergiaMediaDaSensoriDiIrragg
        '
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg.Location = New System.Drawing.Point(243, 238)
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg.Name = "TextBox_EnergiaMediaDaSensoriDiIrragg"
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg.ReadOnly = True
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg.Size = New System.Drawing.Size(60, 20)
        Me.TextBox_EnergiaMediaDaSensoriDiIrragg.TabIndex = 145
        '
        'Label_MediaDaSensoriDiIrragg
        '
        Me.Label_MediaDaSensoriDiIrragg.AutoSize = True
        Me.Label_MediaDaSensoriDiIrragg.Location = New System.Drawing.Point(5, 241)
        Me.Label_MediaDaSensoriDiIrragg.Name = "Label_MediaDaSensoriDiIrragg"
        Me.Label_MediaDaSensoriDiIrragg.Size = New System.Drawing.Size(191, 13)
        Me.Label_MediaDaSensoriDiIrragg.TabIndex = 144
        Me.Label_MediaDaSensoriDiIrragg.Text = "Media Prod. Da Sensori Irragg. (W/mq)"
        '
        'Button_Report_All_Plant
        '
        Me.Button_Report_All_Plant.Location = New System.Drawing.Point(541, 28)
        Me.Button_Report_All_Plant.Name = "Button_Report_All_Plant"
        Me.Button_Report_All_Plant.Size = New System.Drawing.Size(95, 75)
        Me.Button_Report_All_Plant.TabIndex = 146
        Me.Button_Report_All_Plant.Text = "Report semplificato di tutti gli impianti"
        Me.Button_Report_All_Plant.UseVisualStyleBackColor = True
        '
        'Label_Ultimo_Agg_Ora_DL_LOC
        '
        Me.Label_Ultimo_Agg_Ora_DL_LOC.AutoSize = True
        Me.Label_Ultimo_Agg_Ora_DL_LOC.Location = New System.Drawing.Point(451, 176)
        Me.Label_Ultimo_Agg_Ora_DL_LOC.Name = "Label_Ultimo_Agg_Ora_DL_LOC"
        Me.Label_Ultimo_Agg_Ora_DL_LOC.Size = New System.Drawing.Size(64, 13)
        Me.Label_Ultimo_Agg_Ora_DL_LOC.TabIndex = 147
        Me.Label_Ultimo_Agg_Ora_DL_LOC.Text = "Ultimo Agg.:"
        '
        'TextBox_ORA_LOC
        '
        Me.TextBox_ORA_LOC.Location = New System.Drawing.Point(543, 186)
        Me.TextBox_ORA_LOC.Name = "TextBox_ORA_LOC"
        Me.TextBox_ORA_LOC.ReadOnly = True
        Me.TextBox_ORA_LOC.Size = New System.Drawing.Size(112, 20)
        Me.TextBox_ORA_LOC.TabIndex = 148
        '
        'TextBox_ORA_DL
        '
        Me.TextBox_ORA_DL.Location = New System.Drawing.Point(543, 160)
        Me.TextBox_ORA_DL.Name = "TextBox_ORA_DL"
        Me.TextBox_ORA_DL.ReadOnly = True
        Me.TextBox_ORA_DL.Size = New System.Drawing.Size(112, 20)
        Me.TextBox_ORA_DL.TabIndex = 149
        '
        'Label_ORA_DL
        '
        Me.Label_ORA_DL.AutoSize = True
        Me.Label_ORA_DL.Location = New System.Drawing.Point(516, 163)
        Me.Label_ORA_DL.Name = "Label_ORA_DL"
        Me.Label_ORA_DL.Size = New System.Drawing.Size(21, 13)
        Me.Label_ORA_DL.TabIndex = 150
        Me.Label_ORA_DL.Text = "DL"
        '
        'Label_ORA_LOCALE
        '
        Me.Label_ORA_LOCALE.AutoSize = True
        Me.Label_ORA_LOCALE.Location = New System.Drawing.Point(509, 189)
        Me.Label_ORA_LOCALE.Name = "Label_ORA_LOCALE"
        Me.Label_ORA_LOCALE.Size = New System.Drawing.Size(28, 13)
        Me.Label_ORA_LOCALE.TabIndex = 151
        Me.Label_ORA_LOCALE.Text = "Loc."
        '
        'Button_Send_Email_Report_All_Plant
        '
        Me.Button_Send_Email_Report_All_Plant.Location = New System.Drawing.Point(649, 28)
        Me.Button_Send_Email_Report_All_Plant.Name = "Button_Send_Email_Report_All_Plant"
        Me.Button_Send_Email_Report_All_Plant.Size = New System.Drawing.Size(95, 75)
        Me.Button_Send_Email_Report_All_Plant.TabIndex = 152
        Me.Button_Send_Email_Report_All_Plant.Text = "Invia per email Report semplificato di tutti gli impianti"
        Me.Button_Send_Email_Report_All_Plant.UseVisualStyleBackColor = True
        '
        'TextBox_P1
        '
        Me.TextBox_P1.Location = New System.Drawing.Point(750, 29)
        Me.TextBox_P1.Name = "TextBox_P1"
        Me.TextBox_P1.ReadOnly = True
        Me.TextBox_P1.Size = New System.Drawing.Size(80, 20)
        Me.TextBox_P1.TabIndex = 153
        '
        'Label_P11
        '
        Me.Label_P11.AutoSize = True
        Me.Label_P11.Location = New System.Drawing.Point(833, 25)
        Me.Label_P11.Name = "Label_P11"
        Me.Label_P11.Size = New System.Drawing.Size(119, 13)
        Me.Label_P11.TabIndex = 154
        Me.Label_P11.Text = "Energia Totale Prodotta"
        '
        'Label_P12
        '
        Me.Label_P12.AutoSize = True
        Me.Label_P12.Location = New System.Drawing.Point(833, 38)
        Me.Label_P12.Name = "Label_P12"
        Me.Label_P12.Size = New System.Drawing.Size(67, 13)
        Me.Label_P12.TabIndex = 155
        Me.Label_P12.Text = "Dall'Impianto"
        '
        'TextBox_EnergiaProdottaMesePrec
        '
        Me.TextBox_EnergiaProdottaMesePrec.Location = New System.Drawing.Point(750, 62)
        Me.TextBox_EnergiaProdottaMesePrec.Name = "TextBox_EnergiaProdottaMesePrec"
        Me.TextBox_EnergiaProdottaMesePrec.ReadOnly = True
        Me.TextBox_EnergiaProdottaMesePrec.Size = New System.Drawing.Size(80, 20)
        Me.TextBox_EnergiaProdottaMesePrec.TabIndex = 156
        '
        'Label_EnergiaProdottaMesePrec_1
        '
        Me.Label_EnergiaProdottaMesePrec_1.AutoSize = True
        Me.Label_EnergiaProdottaMesePrec_1.Location = New System.Drawing.Point(833, 57)
        Me.Label_EnergiaProdottaMesePrec_1.Name = "Label_EnergiaProdottaMesePrec_1"
        Me.Label_EnergiaProdottaMesePrec_1.Size = New System.Drawing.Size(119, 13)
        Me.Label_EnergiaProdottaMesePrec_1.TabIndex = 157
        Me.Label_EnergiaProdottaMesePrec_1.Text = "Energia Totale Prodotta"
        '
        'Label_EnergiaProdottaMesePrec_2
        '
        Me.Label_EnergiaProdottaMesePrec_2.AutoSize = True
        Me.Label_EnergiaProdottaMesePrec_2.Location = New System.Drawing.Point(833, 70)
        Me.Label_EnergiaProdottaMesePrec_2.Name = "Label_EnergiaProdottaMesePrec_2"
        Me.Label_EnergiaProdottaMesePrec_2.Size = New System.Drawing.Size(91, 13)
        Me.Label_EnergiaProdottaMesePrec_2.TabIndex = 158
        Me.Label_EnergiaProdottaMesePrec_2.Text = "Mese Precedente"
        '
        'Button_DL
        '
        Me.Button_DL.Location = New System.Drawing.Point(543, 237)
        Me.Button_DL.Name = "Button_DL"
        Me.Button_DL.Size = New System.Drawing.Size(75, 22)
        Me.Button_DL.TabIndex = 159
        Me.Button_DL.Text = "Data Logger"
        Me.Button_DL.UseVisualStyleBackColor = True
        '
        'TextBox_Performance_Ratio
        '
        Me.TextBox_Performance_Ratio.Location = New System.Drawing.Point(750, 108)
        Me.TextBox_Performance_Ratio.Name = "TextBox_Performance_Ratio"
        Me.TextBox_Performance_Ratio.ReadOnly = True
        Me.TextBox_Performance_Ratio.Size = New System.Drawing.Size(80, 20)
        Me.TextBox_Performance_Ratio.TabIndex = 160
        '
        'Label_Performance_Ratio
        '
        Me.Label_Performance_Ratio.AutoSize = True
        Me.Label_Performance_Ratio.Location = New System.Drawing.Point(833, 111)
        Me.Label_Performance_Ratio.Name = "Label_Performance_Ratio"
        Me.Label_Performance_Ratio.Size = New System.Drawing.Size(112, 13)
        Me.Label_Performance_Ratio.TabIndex = 161
        Me.Label_Performance_Ratio.Text = "Performance Ratio (%)"
        '
        'TextBox_HG
        '
        Me.TextBox_HG.Location = New System.Drawing.Point(750, 135)
        Me.TextBox_HG.Name = "TextBox_HG"
        Me.TextBox_HG.ReadOnly = True
        Me.TextBox_HG.Size = New System.Drawing.Size(80, 20)
        Me.TextBox_HG.TabIndex = 162
        '
        'Label_HG
        '
        Me.Label_HG.AutoSize = True
        Me.Label_HG.Location = New System.Drawing.Point(833, 138)
        Me.Label_HG.Name = "Label_HG"
        Me.Label_HG.Size = New System.Drawing.Size(72, 13)
        Me.Label_HG.TabIndex = 163
        Me.Label_HG.Text = "HG (Kwh/mq)"
        '
        'TextBox_MediaDaSondeDiTempPann
        '
        Me.TextBox_MediaDaSondeDiTempPann.Location = New System.Drawing.Point(488, 238)
        Me.TextBox_MediaDaSondeDiTempPann.Name = "TextBox_MediaDaSondeDiTempPann"
        Me.TextBox_MediaDaSondeDiTempPann.ReadOnly = True
        Me.TextBox_MediaDaSondeDiTempPann.Size = New System.Drawing.Size(40, 20)
        Me.TextBox_MediaDaSondeDiTempPann.TabIndex = 174
        '
        'Label_MediaDaSondeDiTempPann
        '
        Me.Label_MediaDaSondeDiTempPann.AutoSize = True
        Me.Label_MediaDaSondeDiTempPann.Location = New System.Drawing.Point(328, 242)
        Me.Label_MediaDaSondeDiTempPann.Name = "Label_MediaDaSondeDiTempPann"
        Me.Label_MediaDaSondeDiTempPann.Size = New System.Drawing.Size(154, 13)
        Me.Label_MediaDaSondeDiTempPann.TabIndex = 173
        Me.Label_MediaDaSondeDiTempPann.Text = "Media Sonde Temp. Pann. (°C)"
        '
        'Button_DatiStatistici
        '
        Me.Button_DatiStatistici.Location = New System.Drawing.Point(786, 237)
        Me.Button_DatiStatistici.Name = "Button_DatiStatistici"
        Me.Button_DatiStatistici.Size = New System.Drawing.Size(75, 22)
        Me.Button_DatiStatistici.TabIndex = 175
        Me.Button_DatiStatistici.Text = "Dati Stat."
        Me.Button_DatiStatistici.UseVisualStyleBackColor = True
        '
        'Label_MaxProduzione
        '
        Me.Label_MaxProduzione.AutoSize = True
        Me.Label_MaxProduzione.Location = New System.Drawing.Point(702, 195)
        Me.Label_MaxProduzione.Name = "Label_MaxProduzione"
        Me.Label_MaxProduzione.Size = New System.Drawing.Size(83, 13)
        Me.Label_MaxProduzione.TabIndex = 176
        Me.Label_MaxProduzione.Text = "Max Produzione"
        '
        'TextBox_MaxProduzione
        '
        Me.TextBox_MaxProduzione.Location = New System.Drawing.Point(661, 211)
        Me.TextBox_MaxProduzione.Name = "TextBox_MaxProduzione"
        Me.TextBox_MaxProduzione.ReadOnly = True
        Me.TextBox_MaxProduzione.Size = New System.Drawing.Size(169, 20)
        Me.TextBox_MaxProduzione.TabIndex = 177
        '
        'Button_DataMaxProduzione
        '
        Me.Button_DataMaxProduzione.Location = New System.Drawing.Point(836, 209)
        Me.Button_DataMaxProduzione.Name = "Button_DataMaxProduzione"
        Me.Button_DataMaxProduzione.Size = New System.Drawing.Size(106, 22)
        Me.Button_DataMaxProduzione.TabIndex = 178
        Me.Button_DataMaxProduzione.Text = "<- Data Max Prod."
        Me.Button_DataMaxProduzione.UseVisualStyleBackColor = True
        '
        'Button_DatiDL
        '
        Me.Button_DatiDL.Location = New System.Drawing.Point(867, 237)
        Me.Button_DatiDL.Name = "Button_DatiDL"
        Me.Button_DatiDL.Size = New System.Drawing.Size(75, 22)
        Me.Button_DatiDL.TabIndex = 179
        Me.Button_DatiDL.Text = "Dati DL"
        Me.Button_DatiDL.UseVisualStyleBackColor = True
        '
        'Button_UDTMaxProduzione
        '
        Me.Button_UDTMaxProduzione.Location = New System.Drawing.Point(543, 210)
        Me.Button_UDTMaxProduzione.Name = "Button_UDTMaxProduzione"
        Me.Button_UDTMaxProduzione.Size = New System.Drawing.Size(112, 22)
        Me.Button_UDTMaxProduzione.TabIndex = 180
        Me.Button_UDTMaxProduzione.Text = "UDT Max Prod. ->"
        Me.Button_UDTMaxProduzione.UseVisualStyleBackColor = True
        '
        'Button_Report_Riass_Giorni
        '
        Me.Button_Report_Riass_Giorni.Location = New System.Drawing.Point(836, 160)
        Me.Button_Report_Riass_Giorni.Name = "Button_Report_Riass_Giorni"
        Me.Button_Report_Riass_Giorni.Size = New System.Drawing.Size(162, 22)
        Me.Button_Report_Riass_Giorni.TabIndex = 181
        Me.Button_Report_Riass_Giorni.Text = "Report Riassuntivo Per Giorni"
        Me.Button_Report_Riass_Giorni.UseVisualStyleBackColor = True
        '
        'Button_Report_Riass_Mesi
        '
        Me.Button_Report_Riass_Mesi.Location = New System.Drawing.Point(836, 184)
        Me.Button_Report_Riass_Mesi.Name = "Button_Report_Riass_Mesi"
        Me.Button_Report_Riass_Mesi.Size = New System.Drawing.Size(162, 22)
        Me.Button_Report_Riass_Mesi.TabIndex = 182
        Me.Button_Report_Riass_Mesi.Text = "Report Riassuntivo Per Mesi"
        Me.Button_Report_Riass_Mesi.UseVisualStyleBackColor = True
        '
        'VisDatiDataLogger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1016, 566)
        Me.Controls.Add(Me.Button_Report_Riass_Mesi)
        Me.Controls.Add(Me.Button_Report_Riass_Giorni)
        Me.Controls.Add(Me.Button_UDTMaxProduzione)
        Me.Controls.Add(Me.Button_DatiDL)
        Me.Controls.Add(Me.Button_DataMaxProduzione)
        Me.Controls.Add(Me.TextBox_MaxProduzione)
        Me.Controls.Add(Me.Label_MaxProduzione)
        Me.Controls.Add(Me.Button_DatiStatistici)
        Me.Controls.Add(Me.TextBox_MediaDaSondeDiTempPann)
        Me.Controls.Add(Me.Label_MediaDaSondeDiTempPann)
        Me.Controls.Add(Me.Label_HG)
        Me.Controls.Add(Me.TextBox_HG)
        Me.Controls.Add(Me.Label_Performance_Ratio)
        Me.Controls.Add(Me.TextBox_Performance_Ratio)
        Me.Controls.Add(Me.Button_DL)
        Me.Controls.Add(Me.Label_EnergiaProdottaMesePrec_2)
        Me.Controls.Add(Me.Label_EnergiaProdottaMesePrec_1)
        Me.Controls.Add(Me.TextBox_EnergiaProdottaMesePrec)
        Me.Controls.Add(Me.Label_P12)
        Me.Controls.Add(Me.Label_P11)
        Me.Controls.Add(Me.TextBox_P1)
        Me.Controls.Add(Me.Button_Send_Email_Report_All_Plant)
        Me.Controls.Add(Me.Label_ORA_LOCALE)
        Me.Controls.Add(Me.Label_ORA_DL)
        Me.Controls.Add(Me.TextBox_ORA_DL)
        Me.Controls.Add(Me.TextBox_ORA_LOC)
        Me.Controls.Add(Me.Label_Ultimo_Agg_Ora_DL_LOC)
        Me.Controls.Add(Me.Button_Report_All_Plant)
        Me.Controls.Add(Me.TextBox_EnergiaMediaDaSensoriDiIrragg)
        Me.Controls.Add(Me.Label_MediaDaSensoriDiIrragg)
        Me.Controls.Add(Me.Label_REND_STR_CDP_REF)
        Me.Controls.Add(Me.Label_REND_INV_CDP_REF)
        Me.Controls.Add(Me.Label_REND_SI_CDP_REF)
        Me.Controls.Add(Me.Button_IT)
        Me.Controls.Add(Me.Button_ST)
        Me.Controls.Add(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti)
        Me.Controls.Add(Me.CheckBoxAbilitaAggiornamento)
        Me.Controls.Add(Me.TextBox_EnergiaConsumata)
        Me.Controls.Add(Me.TextBox_EnergiaDaStringhe)
        Me.Controls.Add(Me.TextBox_EnergiaDaSensoriDiIrragg)
        Me.Controls.Add(Me.TextBox_EnergiaDaInverter)
        Me.Controls.Add(Me.Label_LI_ID)
        Me.Controls.Add(Me.Label_EnergiaDaStringhe)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_A)
        Me.Controls.Add(Me.TextBox_REND_INV_CDP_REF)
        Me.Controls.Add(Me.Label_EnergiaDaInverter)
        Me.Controls.Add(Me.Label_EnergiaConsumata)
        Me.Controls.Add(Me.TextBox_REND_STR_CDP)
        Me.Controls.Add(Me.TextBox_REND_STR_CDP_REF)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_LI_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_Da)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Controls.Add(Me.TextBox_REND_SI_CDP_REF)
        Me.Controls.Add(Me.TextBox_REND_INV_CDP)
        Me.Controls.Add(Me.Labelx_EnergiaDaSensoriDiIrragg)
        Me.Controls.Add(Me.DateTimePicker_STOP)
        Me.Controls.Add(Me.TextBox_REND_SI_CDP)
        Me.Controls.Add(Me.Label_EnergiaProdotta)
        Me.Controls.Add(Me.TextBox_EnergiaProdotta)
        Me.Controls.Add(Me.DateTimePicker_START)
        Me.Name = "VisDatiDataLogger"
        Me.Controls.SetChildIndex(Me.DateTimePicker_START, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaProdotta, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaProdotta, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_SI_CDP, 0)
        Me.Controls.SetChildIndex(Me.DateTimePicker_STOP, 0)
        Me.Controls.SetChildIndex(Me.Labelx_EnergiaDaSensoriDiIrragg, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_INV_CDP, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_SI_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_Da, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_STR_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_STR_CDP, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaConsumata, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaDaInverter, 0)
        Me.Controls.SetChildIndex(Me.TextBox_REND_INV_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.Label_A, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaDaStringhe, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaDaInverter, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaDaSensoriDiIrragg, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaDaStringhe, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaConsumata, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxAbilitaAggiornamento, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti, 0)
        Me.Controls.SetChildIndex(Me.Button_ST, 0)
        Me.Controls.SetChildIndex(Me.Button_IT, 0)
        Me.Controls.SetChildIndex(Me.Label_REND_SI_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.Label_REND_INV_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.Label_REND_STR_CDP_REF, 0)
        Me.Controls.SetChildIndex(Me.Label_MediaDaSensoriDiIrragg, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaMediaDaSensoriDiIrragg, 0)
        Me.Controls.SetChildIndex(Me.Button_Report_All_Plant, 0)
        Me.Controls.SetChildIndex(Me.Label_Ultimo_Agg_Ora_DL_LOC, 0)
        Me.Controls.SetChildIndex(Me.TextBox_ORA_LOC, 0)
        Me.Controls.SetChildIndex(Me.TextBox_ORA_DL, 0)
        Me.Controls.SetChildIndex(Me.Label_ORA_DL, 0)
        Me.Controls.SetChildIndex(Me.Label_ORA_LOCALE, 0)
        Me.Controls.SetChildIndex(Me.Button_Send_Email_Report_All_Plant, 0)
        Me.Controls.SetChildIndex(Me.TextBox_P1, 0)
        Me.Controls.SetChildIndex(Me.Label_P11, 0)
        Me.Controls.SetChildIndex(Me.Label_P12, 0)
        Me.Controls.SetChildIndex(Me.TextBox_EnergiaProdottaMesePrec, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaProdottaMesePrec_1, 0)
        Me.Controls.SetChildIndex(Me.Label_EnergiaProdottaMesePrec_2, 0)
        Me.Controls.SetChildIndex(Me.Button_DL, 0)
        Me.Controls.SetChildIndex(Me.TextBox_Performance_Ratio, 0)
        Me.Controls.SetChildIndex(Me.Label_Performance_Ratio, 0)
        Me.Controls.SetChildIndex(Me.TextBox_HG, 0)
        Me.Controls.SetChildIndex(Me.Label_HG, 0)
        Me.Controls.SetChildIndex(Me.Label_MediaDaSondeDiTempPann, 0)
        Me.Controls.SetChildIndex(Me.TextBox_MediaDaSondeDiTempPann, 0)
        Me.Controls.SetChildIndex(Me.Button_DatiStatistici, 0)
        Me.Controls.SetChildIndex(Me.Label_MaxProduzione, 0)
        Me.Controls.SetChildIndex(Me.TextBox_MaxProduzione, 0)
        Me.Controls.SetChildIndex(Me.Button_DataMaxProduzione, 0)
        Me.Controls.SetChildIndex(Me.Button_DatiDL, 0)
        Me.Controls.SetChildIndex(Me.Button_UDTMaxProduzione, 0)
        Me.Controls.SetChildIndex(Me.Button_Report_Riass_Giorni, 0)
        Me.Controls.SetChildIndex(Me.Button_Report_Riass_Mesi, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_LI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_LI_ID As System.Windows.Forms.Label
    Friend WithEvents CheckBoxAbilitaAggiornamento As System.Windows.Forms.CheckBox
    Friend WithEvents Timer_1 As System.Windows.Forms.Timer
    Friend WithEvents CheckBoxVisualizzaSoloGliAllarmiDiTuttiIDataloggerConfiguratiDiTuttiGliImpiantiDiTuttiIClienti As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePicker_START As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_STOP As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label_Da As System.Windows.Forms.Label
    Friend WithEvents Label_A As System.Windows.Forms.Label
    Friend WithEvents Label_EnergiaProdotta As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaProdotta As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_EnergiaConsumata As System.Windows.Forms.TextBox
    Friend WithEvents Label_EnergiaConsumata As System.Windows.Forms.Label
    Friend WithEvents Labelx_EnergiaDaSensoriDiIrragg As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaDaSensoriDiIrragg As System.Windows.Forms.TextBox
    Friend WithEvents Label_EnergiaDaInverter As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaDaInverter As System.Windows.Forms.TextBox
    Friend WithEvents Label_EnergiaDaStringhe As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaDaStringhe As System.Windows.Forms.TextBox
    Friend WithEvents Button_ST As System.Windows.Forms.Button
    Friend WithEvents Button_IT As System.Windows.Forms.Button
    Friend WithEvents TextBox_REND_SI_CDP As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_REND_SI_CDP_REF As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_REND_INV_CDP_REF As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_REND_INV_CDP As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_REND_STR_CDP_REF As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_REND_STR_CDP As System.Windows.Forms.TextBox
    Friend WithEvents Label_REND_SI_CDP_REF As System.Windows.Forms.Label
    Friend WithEvents Label_REND_INV_CDP_REF As System.Windows.Forms.Label
    Friend WithEvents Label_REND_STR_CDP_REF As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaMediaDaSensoriDiIrragg As System.Windows.Forms.TextBox
    Friend WithEvents Label_MediaDaSensoriDiIrragg As System.Windows.Forms.Label
    Friend WithEvents Button_Report_All_Plant As System.Windows.Forms.Button
    Friend WithEvents Label_Ultimo_Agg_Ora_DL_LOC As System.Windows.Forms.Label
    Friend WithEvents TextBox_ORA_LOC As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_ORA_DL As System.Windows.Forms.TextBox
    Friend WithEvents Label_ORA_DL As System.Windows.Forms.Label
    Friend WithEvents Label_ORA_LOCALE As System.Windows.Forms.Label
    Friend WithEvents Button_Send_Email_Report_All_Plant As System.Windows.Forms.Button
    Friend WithEvents TextBox_P1 As System.Windows.Forms.TextBox
    Friend WithEvents Label_P11 As System.Windows.Forms.Label
    Friend WithEvents Label_P12 As System.Windows.Forms.Label
    Friend WithEvents TextBox_EnergiaProdottaMesePrec As System.Windows.Forms.TextBox
    Friend WithEvents Label_EnergiaProdottaMesePrec_1 As System.Windows.Forms.Label
    Friend WithEvents Label_EnergiaProdottaMesePrec_2 As System.Windows.Forms.Label
    Friend WithEvents Button_DL As System.Windows.Forms.Button
    Friend WithEvents TextBox_Performance_Ratio As System.Windows.Forms.TextBox
    Friend WithEvents Label_Performance_Ratio As System.Windows.Forms.Label
    Friend WithEvents TextBox_HG As System.Windows.Forms.TextBox
    Friend WithEvents Label_HG As System.Windows.Forms.Label
    Friend WithEvents TextBox_MediaDaSondeDiTempPann As System.Windows.Forms.TextBox
    Friend WithEvents Label_MediaDaSondeDiTempPann As System.Windows.Forms.Label
    Friend WithEvents Button_DatiStatistici As System.Windows.Forms.Button
    Friend WithEvents Label_MaxProduzione As System.Windows.Forms.Label
    Friend WithEvents TextBox_MaxProduzione As System.Windows.Forms.TextBox
    Friend WithEvents Button_DataMaxProduzione As System.Windows.Forms.Button
    Friend WithEvents Button_DatiDL As System.Windows.Forms.Button
    Friend WithEvents Button_UDTMaxProduzione As System.Windows.Forms.Button
    Friend WithEvents Button_Report_Riass_Giorni As System.Windows.Forms.Button
    Friend WithEvents Button_Report_Riass_Mesi As System.Windows.Forms.Button

End Class
