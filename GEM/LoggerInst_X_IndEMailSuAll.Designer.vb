<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoggerInst_X_IndEMailSuAll
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
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.ComboBox_LI_ID = New System.Windows.Forms.ComboBox
        Me.Label_LI_ID = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE = New System.Windows.Forms.Label
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE = New System.Windows.Forms.TextBox
        Me.Button_Invia_EMail_Di_Test = New System.Windows.Forms.Button
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO = New System.Windows.Forms.Label
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO = New System.Windows.Forms.TextBox
        Me.Label_LIIEMSA_EMAIL_OGGETTO = New System.Windows.Forms.Label
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO = New System.Windows.Forms.TextBox
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME = New System.Windows.Forms.Label
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME = New System.Windows.Forms.TextBox
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME = New System.Windows.Forms.TextBox
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME = New System.Windows.Forms.Label
        Me.CheckBox_LIIEMSA_InviaReport = New System.Windows.Forms.CheckBox
        Me.CheckBox_LIIEMSA_InviaAllarme = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 74
        Me.Label_C_ID.Text = "Cliente"
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 72
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'ComboBox_LI_ID
        '
        Me.ComboBox_LI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_LI_ID.FormattingEnabled = True
        Me.ComboBox_LI_ID.Location = New System.Drawing.Point(302, 82)
        Me.ComboBox_LI_ID.Name = "ComboBox_LI_ID"
        Me.ComboBox_LI_ID.Size = New System.Drawing.Size(450, 21)
        Me.ComboBox_LI_ID.TabIndex = 111
        '
        'Label_LI_ID
        '
        Me.Label_LI_ID.AutoSize = True
        Me.Label_LI_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_LI_ID.Name = "Label_LI_ID"
        Me.Label_LI_ID.Size = New System.Drawing.Size(125, 13)
        Me.Label_LI_ID.TabIndex = 110
        Me.Label_LI_ID.Text = "Numero Logger Installato"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(302, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(450, 21)
        Me.ComboBox_C_ID.TabIndex = 109
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(302, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(450, 21)
        Me.ComboBox_IDP_ID.TabIndex = 108
        '
        'Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE
        '
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.AutoSize = True
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Location = New System.Drawing.Point(12, 112)
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Name = "Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE"
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Size = New System.Drawing.Size(77, 13)
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.TabIndex = 112
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Text = "EMail: Mittente"
        '
        'TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE
        '
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Location = New System.Drawing.Point(302, 109)
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Name = "TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE"
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Size = New System.Drawing.Size(450, 20)
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.TabIndex = 113
        '
        'Button_Invia_EMail_Di_Test
        '
        Me.Button_Invia_EMail_Di_Test.Location = New System.Drawing.Point(302, 238)
        Me.Button_Invia_EMail_Di_Test.Name = "Button_Invia_EMail_Di_Test"
        Me.Button_Invia_EMail_Di_Test.Size = New System.Drawing.Size(450, 23)
        Me.Button_Invia_EMail_Di_Test.TabIndex = 114
        Me.Button_Invia_EMail_Di_Test.Text = "Invia EMail di Test"
        Me.Button_Invia_EMail_Di_Test.UseVisualStyleBackColor = True
        '
        'Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO
        '
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.AutoSize = True
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Location = New System.Drawing.Point(12, 137)
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Name = "Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO"
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Size = New System.Drawing.Size(95, 13)
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.TabIndex = 115
        Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Text = "EMail: Destinatario"
        '
        'TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO
        '
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Location = New System.Drawing.Point(302, 134)
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Name = "TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO"
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Size = New System.Drawing.Size(450, 20)
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.TabIndex = 116
        '
        'Label_LIIEMSA_EMAIL_OGGETTO
        '
        Me.Label_LIIEMSA_EMAIL_OGGETTO.AutoSize = True
        Me.Label_LIIEMSA_EMAIL_OGGETTO.Location = New System.Drawing.Point(12, 163)
        Me.Label_LIIEMSA_EMAIL_OGGETTO.Name = "Label_LIIEMSA_EMAIL_OGGETTO"
        Me.Label_LIIEMSA_EMAIL_OGGETTO.Size = New System.Drawing.Size(77, 13)
        Me.Label_LIIEMSA_EMAIL_OGGETTO.TabIndex = 117
        Me.Label_LIIEMSA_EMAIL_OGGETTO.Text = "EMail: Oggetto"
        '
        'TextBox_LIIEMSA_EMAIL_OGGETTO
        '
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Location = New System.Drawing.Point(302, 160)
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Name = "TextBox_LIIEMSA_EMAIL_OGGETTO"
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Size = New System.Drawing.Size(450, 20)
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.TabIndex = 118
        '
        'Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME
        '
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.AutoSize = True
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Location = New System.Drawing.Point(12, 189)
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Name = "Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME"
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Size = New System.Drawing.Size(159, 13)
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.TabIndex = 119
        Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Text = "EMail: Testo precedente allarme"
        '
        'TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME
        '
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Location = New System.Drawing.Point(302, 186)
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Name = "TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME"
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Size = New System.Drawing.Size(450, 20)
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.TabIndex = 120
        '
        'TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME
        '
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Location = New System.Drawing.Point(302, 212)
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Name = "TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME"
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Size = New System.Drawing.Size(450, 20)
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.TabIndex = 122
        '
        'Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME
        '
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.AutoSize = True
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Location = New System.Drawing.Point(12, 215)
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Name = "Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME"
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Size = New System.Drawing.Size(158, 13)
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.TabIndex = 121
        Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Text = "EMail: Testo successivo allarme"
        '
        'CheckBox_LIIEMSA_InviaReport
        '
        Me.CheckBox_LIIEMSA_InviaReport.AutoSize = True
        Me.CheckBox_LIIEMSA_InviaReport.Location = New System.Drawing.Point(134, 242)
        Me.CheckBox_LIIEMSA_InviaReport.Name = "CheckBox_LIIEMSA_InviaReport"
        Me.CheckBox_LIIEMSA_InviaReport.Size = New System.Drawing.Size(84, 17)
        Me.CheckBox_LIIEMSA_InviaReport.TabIndex = 126
        Me.CheckBox_LIIEMSA_InviaReport.Text = "Invia Report"
        Me.CheckBox_LIIEMSA_InviaReport.UseVisualStyleBackColor = True
        '
        'CheckBox_LIIEMSA_InviaAllarme
        '
        Me.CheckBox_LIIEMSA_InviaAllarme.AutoSize = True
        Me.CheckBox_LIIEMSA_InviaAllarme.Location = New System.Drawing.Point(12, 242)
        Me.CheckBox_LIIEMSA_InviaAllarme.Name = "CheckBox_LIIEMSA_InviaAllarme"
        Me.CheckBox_LIIEMSA_InviaAllarme.Size = New System.Drawing.Size(86, 17)
        Me.CheckBox_LIIEMSA_InviaAllarme.TabIndex = 125
        Me.CheckBox_LIIEMSA_InviaAllarme.Text = "Invia Allarme"
        Me.CheckBox_LIIEMSA_InviaAllarme.UseVisualStyleBackColor = True
        '
        'LoggerInst_X_IndEMailSuAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.CheckBox_LIIEMSA_InviaReport)
        Me.Controls.Add(Me.CheckBox_LIIEMSA_InviaAllarme)
        Me.Controls.Add(Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME)
        Me.Controls.Add(Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME)
        Me.Controls.Add(Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME)
        Me.Controls.Add(Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME)
        Me.Controls.Add(Me.TextBox_LIIEMSA_EMAIL_OGGETTO)
        Me.Controls.Add(Me.Label_LIIEMSA_EMAIL_OGGETTO)
        Me.Controls.Add(Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO)
        Me.Controls.Add(Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO)
        Me.Controls.Add(Me.Button_Invia_EMail_Di_Test)
        Me.Controls.Add(Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE)
        Me.Controls.Add(Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE)
        Me.Controls.Add(Me.ComboBox_LI_ID)
        Me.Controls.Add(Me.Label_LI_ID)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Name = "LoggerInst_X_IndEMailSuAll"
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE, 0)
        Me.Controls.SetChildIndex(Me.Button_Invia_EMail_Di_Test, 0)
        Me.Controls.SetChildIndex(Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO, 0)
        Me.Controls.SetChildIndex(Me.Label_LIIEMSA_EMAIL_OGGETTO, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIIEMSA_EMAIL_OGGETTO, 0)
        Me.Controls.SetChildIndex(Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME, 0)
        Me.Controls.SetChildIndex(Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_LIIEMSA_InviaAllarme, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_LIIEMSA_InviaReport, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_LI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_LI_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE As System.Windows.Forms.Label
    Friend WithEvents TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE As System.Windows.Forms.TextBox
    Friend WithEvents Button_Invia_EMail_Di_Test As System.Windows.Forms.Button
    Friend WithEvents Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO As System.Windows.Forms.Label
    Friend WithEvents TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO As System.Windows.Forms.TextBox
    Friend WithEvents Label_LIIEMSA_EMAIL_OGGETTO As System.Windows.Forms.Label
    Friend WithEvents TextBox_LIIEMSA_EMAIL_OGGETTO As System.Windows.Forms.TextBox
    Friend WithEvents Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME As System.Windows.Forms.Label
    Friend WithEvents TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME As System.Windows.Forms.TextBox
    Friend WithEvents Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME As System.Windows.Forms.Label
    Friend WithEvents CheckBox_LIIEMSA_InviaReport As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_LIIEMSA_InviaAllarme As System.Windows.Forms.CheckBox

End Class
