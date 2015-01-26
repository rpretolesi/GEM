<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoggerInst
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
        Me.Label_L_ID = New System.Windows.Forms.Label
        Me.ComboBox_L_ID = New System.Windows.Forms.ComboBox
        Me.TextBox_LI_TCPIP_Set_Ind = New System.Windows.Forms.TextBox
        Me.Label_LI_TCPIP_Set_Ind = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.TextBox_LI_Nr = New System.Windows.Forms.TextBox
        Me.Label_LI_Nr = New System.Windows.Forms.Label
        Me.ButtonConfig_LI = New System.Windows.Forms.Button
        Me.Label_LI_TCPIP_Set_Port = New System.Windows.Forms.Label
        Me.TextBox_LI_TCPIP_Set_Port = New System.Windows.Forms.TextBox
        Me.Label_LI_OperatoreSim = New System.Windows.Forms.Label
        Me.TextBox_LI_OperatoreSim = New System.Windows.Forms.TextBox
        Me.TextBox_LI_NrSerialeSim = New System.Windows.Forms.TextBox
        Me.Label_LI_NrSerialeSim = New System.Windows.Forms.Label
        Me.Label_LI_PINSim = New System.Windows.Forms.Label
        Me.TextBox_LI_PINSim = New System.Windows.Forms.TextBox
        Me.Label_LI_PUKSim = New System.Windows.Forms.Label
        Me.TextBox_LI_PUKSim = New System.Windows.Forms.TextBox
        Me.Label_LI_NrTelefonicoSim = New System.Windows.Forms.Label
        Me.TextBox_LI_NrTelefonicoSim = New System.Windows.Forms.TextBox
        Me.TextBox_LI_TCPIP_Get_Port = New System.Windows.Forms.TextBox
        Me.Label_LI_TCPIP_Get_Port = New System.Windows.Forms.Label
        Me.TextBox_LI_TCPIP_Get_Ind = New System.Windows.Forms.TextBox
        Me.Label_LI_TCPIP_Get_Ind = New System.Windows.Forms.Label
        Me.Label_LIRel = New System.Windows.Forms.Label
        Me.TextBox_LIRel = New System.Windows.Forms.TextBox
        Me.CheckBox_LI_LogModbus = New System.Windows.Forms.CheckBox
        Me.Label_LI_AutoAggDopoXOre = New System.Windows.Forms.Label
        Me.TextBox_LI_AutoAggDopoXOre = New System.Windows.Forms.TextBox
        Me.Button_LI_AutoAggInCorso = New System.Windows.Forms.Button
        Me.Label_LI_Note = New System.Windows.Forms.Label
        Me.TextBox_LI_Note = New System.Windows.Forms.TextBox
        Me.TextBox_LI_TCPIP_Web_Port = New System.Windows.Forms.TextBox
        Me.Label_LI_TCPIP_Web_Port = New System.Windows.Forms.Label
        Me.CheckBox_LI_In_Funzione = New System.Windows.Forms.CheckBox
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port = New System.Windows.Forms.CheckBox
        Me.TextBox_LI_PotenzaGestitaKw = New System.Windows.Forms.TextBox
        Me.Label_LI_PotenzaGestitaKw = New System.Windows.Forms.Label
        Me.Label_LI_Kd = New System.Windows.Forms.Label
        Me.TextBox_LI_Kd = New System.Windows.Forms.TextBox
        Me.Label_Soglia_Calcolo_HG = New System.Windows.Forms.Label
        Me.TextBox_Soglia_Calcolo_HG = New System.Windows.Forms.TextBox
        Me.TextBox_LI_EuroKwh = New System.Windows.Forms.TextBox
        Me.Label_LI_EuroKwh = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label_L_ID
        '
        Me.Label_L_ID.AutoSize = True
        Me.Label_L_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_L_ID.Name = "Label_L_ID"
        Me.Label_L_ID.Size = New System.Drawing.Size(40, 13)
        Me.Label_L_ID.TabIndex = 71
        Me.Label_L_ID.Text = "Logger"
        '
        'ComboBox_L_ID
        '
        Me.ComboBox_L_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_L_ID.FormattingEnabled = True
        Me.ComboBox_L_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_L_ID.Name = "ComboBox_L_ID"
        Me.ComboBox_L_ID.Size = New System.Drawing.Size(181, 21)
        Me.ComboBox_L_ID.TabIndex = 70
        '
        'TextBox_LI_TCPIP_Set_Ind
        '
        Me.TextBox_LI_TCPIP_Set_Ind.Location = New System.Drawing.Point(250, 135)
        Me.TextBox_LI_TCPIP_Set_Ind.Name = "TextBox_LI_TCPIP_Set_Ind"
        Me.TextBox_LI_TCPIP_Set_Ind.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_TCPIP_Set_Ind.TabIndex = 68
        '
        'Label_LI_TCPIP_Set_Ind
        '
        Me.Label_LI_TCPIP_Set_Ind.AutoSize = True
        Me.Label_LI_TCPIP_Set_Ind.Location = New System.Drawing.Point(12, 138)
        Me.Label_LI_TCPIP_Set_Ind.Name = "Label_LI_TCPIP_Set_Ind"
        Me.Label_LI_TCPIP_Set_Ind.Size = New System.Drawing.Size(101, 13)
        Me.Label_LI_TCPIP_Set_Ind.TabIndex = 69
        Me.Label_LI_TCPIP_Set_Ind.Text = "Indirizzo TCP IP Set"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(300, 21)
        Me.ComboBox_C_ID.TabIndex = 67
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 66
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(181, 21)
        Me.ComboBox_IDP_ID.TabIndex = 65
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 64
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'TextBox_LI_Nr
        '
        Me.TextBox_LI_Nr.Location = New System.Drawing.Point(250, 109)
        Me.TextBox_LI_Nr.Name = "TextBox_LI_Nr"
        Me.TextBox_LI_Nr.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_Nr.TabIndex = 72
        '
        'Label_LI_Nr
        '
        Me.Label_LI_Nr.AutoSize = True
        Me.Label_LI_Nr.Location = New System.Drawing.Point(12, 112)
        Me.Label_LI_Nr.Name = "Label_LI_Nr"
        Me.Label_LI_Nr.Size = New System.Drawing.Size(44, 13)
        Me.Label_LI_Nr.TabIndex = 73
        Me.Label_LI_Nr.Text = "Numero"
        '
        'ButtonConfig_LI
        '
        Me.ButtonConfig_LI.Location = New System.Drawing.Point(356, 107)
        Me.ButtonConfig_LI.Name = "ButtonConfig_LI"
        Me.ButtonConfig_LI.Size = New System.Drawing.Size(75, 23)
        Me.ButtonConfig_LI.TabIndex = 74
        Me.ButtonConfig_LI.Text = "Configura"
        Me.ButtonConfig_LI.UseVisualStyleBackColor = True
        '
        'Label_LI_TCPIP_Set_Port
        '
        Me.Label_LI_TCPIP_Set_Port.AutoSize = True
        Me.Label_LI_TCPIP_Set_Port.Location = New System.Drawing.Point(12, 164)
        Me.Label_LI_TCPIP_Set_Port.Name = "Label_LI_TCPIP_Set_Port"
        Me.Label_LI_TCPIP_Set_Port.Size = New System.Drawing.Size(88, 13)
        Me.Label_LI_TCPIP_Set_Port.TabIndex = 75
        Me.Label_LI_TCPIP_Set_Port.Text = "Porta TCP IP Set"
        '
        'TextBox_LI_TCPIP_Set_Port
        '
        Me.TextBox_LI_TCPIP_Set_Port.Location = New System.Drawing.Point(146, 161)
        Me.TextBox_LI_TCPIP_Set_Port.Name = "TextBox_LI_TCPIP_Set_Port"
        Me.TextBox_LI_TCPIP_Set_Port.Size = New System.Drawing.Size(45, 20)
        Me.TextBox_LI_TCPIP_Set_Port.TabIndex = 76
        '
        'Label_LI_OperatoreSim
        '
        Me.Label_LI_OperatoreSim.AutoSize = True
        Me.Label_LI_OperatoreSim.Location = New System.Drawing.Point(12, 190)
        Me.Label_LI_OperatoreSim.Name = "Label_LI_OperatoreSim"
        Me.Label_LI_OperatoreSim.Size = New System.Drawing.Size(76, 13)
        Me.Label_LI_OperatoreSim.TabIndex = 77
        Me.Label_LI_OperatoreSim.Text = "Operatore SIM"
        '
        'TextBox_LI_OperatoreSim
        '
        Me.TextBox_LI_OperatoreSim.Location = New System.Drawing.Point(250, 187)
        Me.TextBox_LI_OperatoreSim.Name = "TextBox_LI_OperatoreSim"
        Me.TextBox_LI_OperatoreSim.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_OperatoreSim.TabIndex = 78
        '
        'TextBox_LI_NrSerialeSim
        '
        Me.TextBox_LI_NrSerialeSim.Location = New System.Drawing.Point(250, 213)
        Me.TextBox_LI_NrSerialeSim.Name = "TextBox_LI_NrSerialeSim"
        Me.TextBox_LI_NrSerialeSim.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_NrSerialeSim.TabIndex = 79
        '
        'Label_LI_NrSerialeSim
        '
        Me.Label_LI_NrSerialeSim.AutoSize = True
        Me.Label_LI_NrSerialeSim.Location = New System.Drawing.Point(12, 216)
        Me.Label_LI_NrSerialeSim.Name = "Label_LI_NrSerialeSim"
        Me.Label_LI_NrSerialeSim.Size = New System.Drawing.Size(78, 13)
        Me.Label_LI_NrSerialeSim.TabIndex = 80
        Me.Label_LI_NrSerialeSim.Text = "Nr. Seriale SIM"
        '
        'Label_LI_PINSim
        '
        Me.Label_LI_PINSim.AutoSize = True
        Me.Label_LI_PINSim.Location = New System.Drawing.Point(12, 242)
        Me.Label_LI_PINSim.Name = "Label_LI_PINSim"
        Me.Label_LI_PINSim.Size = New System.Drawing.Size(47, 13)
        Me.Label_LI_PINSim.TabIndex = 81
        Me.Label_LI_PINSim.Text = "PIN SIM"
        '
        'TextBox_LI_PINSim
        '
        Me.TextBox_LI_PINSim.Location = New System.Drawing.Point(250, 239)
        Me.TextBox_LI_PINSim.Name = "TextBox_LI_PINSim"
        Me.TextBox_LI_PINSim.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_PINSim.TabIndex = 82
        '
        'Label_LI_PUKSim
        '
        Me.Label_LI_PUKSim.AutoSize = True
        Me.Label_LI_PUKSim.Location = New System.Drawing.Point(399, 216)
        Me.Label_LI_PUKSim.Name = "Label_LI_PUKSim"
        Me.Label_LI_PUKSim.Size = New System.Drawing.Size(51, 13)
        Me.Label_LI_PUKSim.TabIndex = 83
        Me.Label_LI_PUKSim.Text = "PUK SIM"
        '
        'TextBox_LI_PUKSim
        '
        Me.TextBox_LI_PUKSim.Location = New System.Drawing.Point(650, 213)
        Me.TextBox_LI_PUKSim.Name = "TextBox_LI_PUKSim"
        Me.TextBox_LI_PUKSim.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_PUKSim.TabIndex = 84
        '
        'Label_LI_NrTelefonicoSim
        '
        Me.Label_LI_NrTelefonicoSim.AutoSize = True
        Me.Label_LI_NrTelefonicoSim.Location = New System.Drawing.Point(399, 190)
        Me.Label_LI_NrTelefonicoSim.Name = "Label_LI_NrTelefonicoSim"
        Me.Label_LI_NrTelefonicoSim.Size = New System.Drawing.Size(88, 13)
        Me.Label_LI_NrTelefonicoSim.TabIndex = 85
        Me.Label_LI_NrTelefonicoSim.Text = "Nr. Telefono SIM"
        '
        'TextBox_LI_NrTelefonicoSim
        '
        Me.TextBox_LI_NrTelefonicoSim.Location = New System.Drawing.Point(650, 187)
        Me.TextBox_LI_NrTelefonicoSim.Name = "TextBox_LI_NrTelefonicoSim"
        Me.TextBox_LI_NrTelefonicoSim.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_NrTelefonicoSim.TabIndex = 86
        '
        'TextBox_LI_TCPIP_Get_Port
        '
        Me.TextBox_LI_TCPIP_Get_Port.Location = New System.Drawing.Point(650, 161)
        Me.TextBox_LI_TCPIP_Get_Port.Name = "TextBox_LI_TCPIP_Get_Port"
        Me.TextBox_LI_TCPIP_Get_Port.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_TCPIP_Get_Port.TabIndex = 90
        '
        'Label_LI_TCPIP_Get_Port
        '
        Me.Label_LI_TCPIP_Get_Port.AutoSize = True
        Me.Label_LI_TCPIP_Get_Port.Location = New System.Drawing.Point(399, 164)
        Me.Label_LI_TCPIP_Get_Port.Name = "Label_LI_TCPIP_Get_Port"
        Me.Label_LI_TCPIP_Get_Port.Size = New System.Drawing.Size(89, 13)
        Me.Label_LI_TCPIP_Get_Port.TabIndex = 89
        Me.Label_LI_TCPIP_Get_Port.Text = "Porta TCP IP Get"
        '
        'TextBox_LI_TCPIP_Get_Ind
        '
        Me.TextBox_LI_TCPIP_Get_Ind.Location = New System.Drawing.Point(650, 135)
        Me.TextBox_LI_TCPIP_Get_Ind.Name = "TextBox_LI_TCPIP_Get_Ind"
        Me.TextBox_LI_TCPIP_Get_Ind.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_TCPIP_Get_Ind.TabIndex = 87
        '
        'Label_LI_TCPIP_Get_Ind
        '
        Me.Label_LI_TCPIP_Get_Ind.AutoSize = True
        Me.Label_LI_TCPIP_Get_Ind.Location = New System.Drawing.Point(399, 138)
        Me.Label_LI_TCPIP_Get_Ind.Name = "Label_LI_TCPIP_Get_Ind"
        Me.Label_LI_TCPIP_Get_Ind.Size = New System.Drawing.Size(102, 13)
        Me.Label_LI_TCPIP_Get_Ind.TabIndex = 88
        Me.Label_LI_TCPIP_Get_Ind.Text = "Indirizzo TCP IP Get"
        '
        'Label_LIRel
        '
        Me.Label_LIRel.AutoSize = True
        Me.Label_LIRel.Location = New System.Drawing.Point(446, 85)
        Me.Label_LIRel.Name = "Label_LIRel"
        Me.Label_LIRel.Size = New System.Drawing.Size(26, 13)
        Me.Label_LIRel.TabIndex = 91
        Me.Label_LIRel.Text = "Rel."
        '
        'TextBox_LIRel
        '
        Me.TextBox_LIRel.Location = New System.Drawing.Point(478, 82)
        Me.TextBox_LIRel.Name = "TextBox_LIRel"
        Me.TextBox_LIRel.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_LIRel.TabIndex = 92
        '
        'CheckBox_LI_LogModbus
        '
        Me.CheckBox_LI_LogModbus.AutoSize = True
        Me.CheckBox_LI_LogModbus.Location = New System.Drawing.Point(650, 30)
        Me.CheckBox_LI_LogModbus.Name = "CheckBox_LI_LogModbus"
        Me.CheckBox_LI_LogModbus.Size = New System.Drawing.Size(141, 17)
        Me.CheckBox_LI_LogModbus.TabIndex = 93
        Me.CheckBox_LI_LogModbus.Text = "Tieni Copia Log Modbus"
        Me.CheckBox_LI_LogModbus.UseVisualStyleBackColor = True
        '
        'Label_LI_AutoAggDopoXOre
        '
        Me.Label_LI_AutoAggDopoXOre.AutoSize = True
        Me.Label_LI_AutoAggDopoXOre.Location = New System.Drawing.Point(399, 242)
        Me.Label_LI_AutoAggDopoXOre.Name = "Label_LI_AutoAggDopoXOre"
        Me.Label_LI_AutoAggDopoXOre.Size = New System.Drawing.Size(219, 13)
        Me.Label_LI_AutoAggDopoXOre.TabIndex = 94
        Me.Label_LI_AutoAggDopoXOre.Text = "Tempo di autoaggiornamento 10>Ab. (Minuti)"
        '
        'TextBox_LI_AutoAggDopoXOre
        '
        Me.TextBox_LI_AutoAggDopoXOre.Location = New System.Drawing.Point(650, 239)
        Me.TextBox_LI_AutoAggDopoXOre.Name = "TextBox_LI_AutoAggDopoXOre"
        Me.TextBox_LI_AutoAggDopoXOre.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_LI_AutoAggDopoXOre.TabIndex = 95
        '
        'Button_LI_AutoAggInCorso
        '
        Me.Button_LI_AutoAggInCorso.Location = New System.Drawing.Point(402, 260)
        Me.Button_LI_AutoAggInCorso.Name = "Button_LI_AutoAggInCorso"
        Me.Button_LI_AutoAggInCorso.Size = New System.Drawing.Size(348, 23)
        Me.Button_LI_AutoAggInCorso.TabIndex = 96
        Me.Button_LI_AutoAggInCorso.Text = "Reset Flag di Autoconnessione in Corso"
        Me.Button_LI_AutoAggInCorso.UseVisualStyleBackColor = True
        '
        'Label_LI_Note
        '
        Me.Label_LI_Note.AutoSize = True
        Me.Label_LI_Note.Location = New System.Drawing.Point(12, 268)
        Me.Label_LI_Note.Name = "Label_LI_Note"
        Me.Label_LI_Note.Size = New System.Drawing.Size(30, 13)
        Me.Label_LI_Note.TabIndex = 97
        Me.Label_LI_Note.Text = "Note"
        '
        'TextBox_LI_Note
        '
        Me.TextBox_LI_Note.Location = New System.Drawing.Point(48, 265)
        Me.TextBox_LI_Note.Name = "TextBox_LI_Note"
        Me.TextBox_LI_Note.Size = New System.Drawing.Size(302, 20)
        Me.TextBox_LI_Note.TabIndex = 98
        '
        'TextBox_LI_TCPIP_Web_Port
        '
        Me.TextBox_LI_TCPIP_Web_Port.Location = New System.Drawing.Point(305, 161)
        Me.TextBox_LI_TCPIP_Web_Port.Name = "TextBox_LI_TCPIP_Web_Port"
        Me.TextBox_LI_TCPIP_Web_Port.Size = New System.Drawing.Size(45, 20)
        Me.TextBox_LI_TCPIP_Web_Port.TabIndex = 99
        '
        'Label_LI_TCPIP_Web_Port
        '
        Me.Label_LI_TCPIP_Web_Port.AutoSize = True
        Me.Label_LI_TCPIP_Web_Port.Location = New System.Drawing.Point(197, 164)
        Me.Label_LI_TCPIP_Web_Port.Name = "Label_LI_TCPIP_Web_Port"
        Me.Label_LI_TCPIP_Web_Port.Size = New System.Drawing.Size(95, 13)
        Me.Label_LI_TCPIP_Web_Port.TabIndex = 100
        Me.Label_LI_TCPIP_Web_Port.Text = "Porta TCP IP Web"
        '
        'CheckBox_LI_In_Funzione
        '
        Me.CheckBox_LI_In_Funzione.AutoSize = True
        Me.CheckBox_LI_In_Funzione.Location = New System.Drawing.Point(437, 111)
        Me.CheckBox_LI_In_Funzione.Name = "CheckBox_LI_In_Funzione"
        Me.CheckBox_LI_In_Funzione.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox_LI_In_Funzione.TabIndex = 101
        Me.CheckBox_LI_In_Funzione.Text = "In Funzione"
        Me.CheckBox_LI_In_Funzione.UseVisualStyleBackColor = True
        '
        'CheckBox_LI_Auto_Get_TCPIP_Ind_Port
        '
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.AutoSize = True
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Location = New System.Drawing.Point(650, 111)
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Name = "CheckBox_LI_Auto_Get_TCPIP_Ind_Port"
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Size = New System.Drawing.Size(117, 17)
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.TabIndex = 102
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Text = "Mem. TCP IP Aut.  "
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.UseVisualStyleBackColor = True
        '
        'TextBox_LI_PotenzaGestitaKw
        '
        Me.TextBox_LI_PotenzaGestitaKw.Location = New System.Drawing.Point(620, 55)
        Me.TextBox_LI_PotenzaGestitaKw.Name = "TextBox_LI_PotenzaGestitaKw"
        Me.TextBox_LI_PotenzaGestitaKw.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_LI_PotenzaGestitaKw.TabIndex = 104
        '
        'Label_LI_PotenzaGestitaKw
        '
        Me.Label_LI_PotenzaGestitaKw.AutoSize = True
        Me.Label_LI_PotenzaGestitaKw.Location = New System.Drawing.Point(559, 58)
        Me.Label_LI_PotenzaGestitaKw.Name = "Label_LI_PotenzaGestitaKw"
        Me.Label_LI_PotenzaGestitaKw.Size = New System.Drawing.Size(55, 13)
        Me.Label_LI_PotenzaGestitaKw.TabIndex = 103
        Me.Label_LI_PotenzaGestitaKw.Text = "P. G. Kwh"
        '
        'Label_LI_Kd
        '
        Me.Label_LI_Kd.AutoSize = True
        Me.Label_LI_Kd.Location = New System.Drawing.Point(685, 59)
        Me.Label_LI_Kd.Name = "Label_LI_Kd"
        Me.Label_LI_Kd.Size = New System.Drawing.Size(20, 13)
        Me.Label_LI_Kd.TabIndex = 105
        Me.Label_LI_Kd.Text = "Kd"
        '
        'TextBox_LI_Kd
        '
        Me.TextBox_LI_Kd.Location = New System.Drawing.Point(711, 55)
        Me.TextBox_LI_Kd.Name = "TextBox_LI_Kd"
        Me.TextBox_LI_Kd.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_LI_Kd.TabIndex = 106
        '
        'Label_Soglia_Calcolo_HG
        '
        Me.Label_Soglia_Calcolo_HG.AutoSize = True
        Me.Label_Soglia_Calcolo_HG.Location = New System.Drawing.Point(573, 85)
        Me.Label_Soglia_Calcolo_HG.Name = "Label_Soglia_Calcolo_HG"
        Me.Label_Soglia_Calcolo_HG.Size = New System.Drawing.Size(132, 13)
        Me.Label_Soglia_Calcolo_HG.TabIndex = 107
        Me.Label_Soglia_Calcolo_HG.Text = "Soglia Calcolo HG (W/mq)"
        '
        'TextBox_Soglia_Calcolo_HG
        '
        Me.TextBox_Soglia_Calcolo_HG.Location = New System.Drawing.Point(711, 81)
        Me.TextBox_Soglia_Calcolo_HG.Name = "TextBox_Soglia_Calcolo_HG"
        Me.TextBox_Soglia_Calcolo_HG.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_Soglia_Calcolo_HG.TabIndex = 108
        '
        'TextBox_LI_EuroKwh
        '
        Me.TextBox_LI_EuroKwh.Location = New System.Drawing.Point(500, 56)
        Me.TextBox_LI_EuroKwh.Name = "TextBox_LI_EuroKwh"
        Me.TextBox_LI_EuroKwh.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_LI_EuroKwh.TabIndex = 110
        '
        'Label_LI_EuroKwh
        '
        Me.Label_LI_EuroKwh.AutoSize = True
        Me.Label_LI_EuroKwh.Location = New System.Drawing.Point(457, 59)
        Me.Label_LI_EuroKwh.Name = "Label_LI_EuroKwh"
        Me.Label_LI_EuroKwh.Size = New System.Drawing.Size(37, 13)
        Me.Label_LI_EuroKwh.TabIndex = 109
        Me.Label_LI_EuroKwh.Text = "€ Kwh"
        '
        'LoggerInst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_LI_EuroKwh)
        Me.Controls.Add(Me.Label_LI_EuroKwh)
        Me.Controls.Add(Me.TextBox_Soglia_Calcolo_HG)
        Me.Controls.Add(Me.Label_Soglia_Calcolo_HG)
        Me.Controls.Add(Me.TextBox_LI_Kd)
        Me.Controls.Add(Me.Label_LI_Kd)
        Me.Controls.Add(Me.TextBox_LI_PotenzaGestitaKw)
        Me.Controls.Add(Me.Label_LI_PotenzaGestitaKw)
        Me.Controls.Add(Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port)
        Me.Controls.Add(Me.CheckBox_LI_In_Funzione)
        Me.Controls.Add(Me.Label_LI_TCPIP_Web_Port)
        Me.Controls.Add(Me.TextBox_LI_TCPIP_Web_Port)
        Me.Controls.Add(Me.TextBox_LI_Note)
        Me.Controls.Add(Me.Label_LI_Note)
        Me.Controls.Add(Me.Button_LI_AutoAggInCorso)
        Me.Controls.Add(Me.TextBox_LI_AutoAggDopoXOre)
        Me.Controls.Add(Me.Label_LI_AutoAggDopoXOre)
        Me.Controls.Add(Me.CheckBox_LI_LogModbus)
        Me.Controls.Add(Me.TextBox_LIRel)
        Me.Controls.Add(Me.Label_LIRel)
        Me.Controls.Add(Me.TextBox_LI_TCPIP_Get_Port)
        Me.Controls.Add(Me.Label_LI_TCPIP_Get_Port)
        Me.Controls.Add(Me.TextBox_LI_TCPIP_Get_Ind)
        Me.Controls.Add(Me.Label_LI_TCPIP_Get_Ind)
        Me.Controls.Add(Me.TextBox_LI_NrTelefonicoSim)
        Me.Controls.Add(Me.Label_LI_NrTelefonicoSim)
        Me.Controls.Add(Me.TextBox_LI_PUKSim)
        Me.Controls.Add(Me.Label_LI_PUKSim)
        Me.Controls.Add(Me.TextBox_LI_PINSim)
        Me.Controls.Add(Me.Label_LI_PINSim)
        Me.Controls.Add(Me.Label_LI_NrSerialeSim)
        Me.Controls.Add(Me.TextBox_LI_NrSerialeSim)
        Me.Controls.Add(Me.TextBox_LI_OperatoreSim)
        Me.Controls.Add(Me.Label_LI_OperatoreSim)
        Me.Controls.Add(Me.TextBox_LI_TCPIP_Set_Port)
        Me.Controls.Add(Me.Label_LI_TCPIP_Set_Port)
        Me.Controls.Add(Me.ButtonConfig_LI)
        Me.Controls.Add(Me.TextBox_LI_Nr)
        Me.Controls.Add(Me.Label_LI_Nr)
        Me.Controls.Add(Me.Label_L_ID)
        Me.Controls.Add(Me.ComboBox_L_ID)
        Me.Controls.Add(Me.TextBox_LI_TCPIP_Set_Ind)
        Me.Controls.Add(Me.Label_LI_TCPIP_Set_Ind)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Name = "LoggerInst"
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_TCPIP_Set_Ind, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_TCPIP_Set_Ind, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_L_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_L_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_Nr, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_Nr, 0)
        Me.Controls.SetChildIndex(Me.ButtonConfig_LI, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_TCPIP_Set_Port, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_TCPIP_Set_Port, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_OperatoreSim, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_OperatoreSim, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_NrSerialeSim, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_NrSerialeSim, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_PINSim, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_PINSim, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_PUKSim, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_PUKSim, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_NrTelefonicoSim, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_NrTelefonicoSim, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_TCPIP_Get_Ind, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_TCPIP_Get_Ind, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_TCPIP_Get_Port, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_TCPIP_Get_Port, 0)
        Me.Controls.SetChildIndex(Me.Label_LIRel, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIRel, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_LI_LogModbus, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_AutoAggDopoXOre, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_AutoAggDopoXOre, 0)
        Me.Controls.SetChildIndex(Me.Button_LI_AutoAggInCorso, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_Note, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_Note, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_TCPIP_Web_Port, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_TCPIP_Web_Port, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_LI_In_Funzione, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_PotenzaGestitaKw, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_PotenzaGestitaKw, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_Kd, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_Kd, 0)
        Me.Controls.SetChildIndex(Me.Label_Soglia_Calcolo_HG, 0)
        Me.Controls.SetChildIndex(Me.TextBox_Soglia_Calcolo_HG, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_EuroKwh, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LI_EuroKwh, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_L_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_L_ID As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox_LI_TCPIP_Set_Ind As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_TCPIP_Set_Ind As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_Nr As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_Nr As System.Windows.Forms.Label
    Friend WithEvents ButtonConfig_LI As System.Windows.Forms.Button
    Friend WithEvents Label_LI_TCPIP_Set_Port As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_TCPIP_Set_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_OperatoreSim As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_OperatoreSim As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_LI_NrSerialeSim As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_NrSerialeSim As System.Windows.Forms.Label
    Friend WithEvents Label_LI_PINSim As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_PINSim As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_PUKSim As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_PUKSim As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_NrTelefonicoSim As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_NrTelefonicoSim As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_LI_TCPIP_Get_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_TCPIP_Get_Port As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_TCPIP_Get_Ind As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_TCPIP_Get_Ind As System.Windows.Forms.Label
    Friend WithEvents Label_LIRel As System.Windows.Forms.Label
    Friend WithEvents TextBox_LIRel As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox_LI_LogModbus As System.Windows.Forms.CheckBox
    Friend WithEvents Label_LI_AutoAggDopoXOre As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_AutoAggDopoXOre As System.Windows.Forms.TextBox
    Friend WithEvents Button_LI_AutoAggInCorso As System.Windows.Forms.Button
    Friend WithEvents Label_LI_Note As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_Note As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_LI_TCPIP_Web_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_TCPIP_Web_Port As System.Windows.Forms.Label
    Friend WithEvents CheckBox_LI_In_Funzione As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_LI_Auto_Get_TCPIP_Ind_Port As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox_LI_PotenzaGestitaKw As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_PotenzaGestitaKw As System.Windows.Forms.Label
    Friend WithEvents Label_LI_Kd As System.Windows.Forms.Label
    Friend WithEvents TextBox_LI_Kd As System.Windows.Forms.TextBox
    Friend WithEvents Label_Soglia_Calcolo_HG As System.Windows.Forms.Label
    Friend WithEvents TextBox_Soglia_Calcolo_HG As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_LI_EuroKwh As System.Windows.Forms.TextBox
    Friend WithEvents Label_LI_EuroKwh As System.Windows.Forms.Label

End Class
