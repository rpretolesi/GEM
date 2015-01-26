<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisDatiInverterTester
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
        Me.Timer_1 = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti = New System.Windows.Forms.CheckBox
        Me.Label_ITI_Indirizzo_Modbus = New System.Windows.Forms.Label
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus = New System.Windows.Forms.CheckBox
        Me.TextBox_ITI_Indirizzo_Modbus = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'CheckBoxAbilitaAggiornamento
        '
        Me.CheckBoxAbilitaAggiornamento.AutoSize = True
        Me.CheckBoxAbilitaAggiornamento.Location = New System.Drawing.Point(12, 264)
        Me.CheckBoxAbilitaAggiornamento.Name = "CheckBoxAbilitaAggiornamento"
        Me.CheckBoxAbilitaAggiornamento.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxAbilitaAggiornamento.TabIndex = 111
        Me.CheckBoxAbilitaAggiornamento.Text = "Abilita Aggiornamento Automatico"
        Me.CheckBoxAbilitaAggiornamento.UseVisualStyleBackColor = True
        '
        'Timer_1
        '
        Me.Timer_1.Interval = 60000
        '
        'CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti
        '
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.AutoSize = True
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Location = New System.Drawing.Point(202, 264)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Name = "CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpi" & _
            "antiDiTuttiIClienti"
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Size = New System.Drawing.Size(464, 17)
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.TabIndex = 112
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Text = "Visualizza solo gli Allarmi Di Tutti gli Inverter Tester configurati di tutti gli" & _
            " Impianti di tutti i Clienti"
        Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.UseVisualStyleBackColor = True
        '
        'Label_ITI_Indirizzo_Modbus
        '
        Me.Label_ITI_Indirizzo_Modbus.AutoSize = True
        Me.Label_ITI_Indirizzo_Modbus.Location = New System.Drawing.Point(9, 31)
        Me.Label_ITI_Indirizzo_Modbus.Name = "Label_ITI_Indirizzo_Modbus"
        Me.Label_ITI_Indirizzo_Modbus.Size = New System.Drawing.Size(89, 13)
        Me.Label_ITI_Indirizzo_Modbus.TabIndex = 144
        Me.Label_ITI_Indirizzo_Modbus.Text = "Indirizzo Modbus:"
        '
        'CheckBox_FiltraPer_ITI_Indirizzo_Modbus
        '
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.AutoSize = True
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.Location = New System.Drawing.Point(210, 30)
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.Name = "CheckBox_FiltraPer_ITI_Indirizzo_Modbus"
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.Size = New System.Drawing.Size(148, 17)
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.TabIndex = 143
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.Text = "Filtra per Indirizzo Modbus"
        Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus.UseVisualStyleBackColor = True
        '
        'TextBox_ITI_Indirizzo_Modbus
        '
        Me.TextBox_ITI_Indirizzo_Modbus.Location = New System.Drawing.Point(104, 28)
        Me.TextBox_ITI_Indirizzo_Modbus.Name = "TextBox_ITI_Indirizzo_Modbus"
        Me.TextBox_ITI_Indirizzo_Modbus.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_ITI_Indirizzo_Modbus.TabIndex = 142
        '
        'VisDatiInverterTester
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Label_ITI_Indirizzo_Modbus)
        Me.Controls.Add(Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus)
        Me.Controls.Add(Me.TextBox_ITI_Indirizzo_Modbus)
        Me.Controls.Add(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti)
        Me.Controls.Add(Me.CheckBoxAbilitaAggiornamento)
        Me.Name = "VisDatiInverterTester"
        Me.Controls.SetChildIndex(Me.CheckBoxAbilitaAggiornamento, 0)
        Me.Controls.SetChildIndex(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti, 0)
        Me.Controls.SetChildIndex(Me.TextBox_ITI_Indirizzo_Modbus, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_FiltraPer_ITI_Indirizzo_Modbus, 0)
        Me.Controls.SetChildIndex(Me.Label_ITI_Indirizzo_Modbus, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxAbilitaAggiornamento As System.Windows.Forms.CheckBox
    Friend WithEvents Timer_1 As System.Windows.Forms.Timer
    Friend WithEvents CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti As System.Windows.Forms.CheckBox
    Friend WithEvents Label_ITI_Indirizzo_Modbus As System.Windows.Forms.Label
    Friend WithEvents CheckBox_FiltraPer_ITI_Indirizzo_Modbus As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox_ITI_Indirizzo_Modbus As System.Windows.Forms.TextBox

End Class
