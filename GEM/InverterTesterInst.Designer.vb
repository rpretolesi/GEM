<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InverterTesterInst
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
        Me.ComboBox_LI_ID = New System.Windows.Forms.ComboBox
        Me.Label_LI_ID = New System.Windows.Forms.Label
        Me.Label_INT_ID = New System.Windows.Forms.Label
        Me.ComboBox_INT_ID = New System.Windows.Forms.ComboBox
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.TextBox_ITI_Indirizzo_Modbus = New System.Windows.Forms.TextBox
        Me.Label_ITI_Indirizzo_Modbus = New System.Windows.Forms.Label
        Me.TextBox_ITIRel = New System.Windows.Forms.TextBox
        Me.Label_ITIRel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ComboBox_LI_ID
        '
        Me.ComboBox_LI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_LI_ID.FormattingEnabled = True
        Me.ComboBox_LI_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_LI_ID.Name = "ComboBox_LI_ID"
        Me.ComboBox_LI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_LI_ID.TabIndex = 79
        '
        'Label_LI_ID
        '
        Me.Label_LI_ID.AutoSize = True
        Me.Label_LI_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_LI_ID.Name = "Label_LI_ID"
        Me.Label_LI_ID.Size = New System.Drawing.Size(125, 13)
        Me.Label_LI_ID.TabIndex = 78
        Me.Label_LI_ID.Text = "Numero Logger Installato"
        '
        'Label_INT_ID
        '
        Me.Label_INT_ID.AutoSize = True
        Me.Label_INT_ID.Location = New System.Drawing.Point(12, 112)
        Me.Label_INT_ID.Name = "Label_INT_ID"
        Me.Label_INT_ID.Size = New System.Drawing.Size(91, 13)
        Me.Label_INT_ID.TabIndex = 77
        Me.Label_INT_ID.Text = "Tipo String Tester"
        '
        'ComboBox_INT_ID
        '
        Me.ComboBox_INT_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_INT_ID.FormattingEnabled = True
        Me.ComboBox_INT_ID.Location = New System.Drawing.Point(250, 109)
        Me.ComboBox_INT_ID.Name = "ComboBox_INT_ID"
        Me.ComboBox_INT_ID.Size = New System.Drawing.Size(300, 21)
        Me.ComboBox_INT_ID.TabIndex = 76
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 75
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(9, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 74
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 73
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
        'TextBox_ITI_Indirizzo_Modbus
        '
        Me.TextBox_ITI_Indirizzo_Modbus.Location = New System.Drawing.Point(250, 136)
        Me.TextBox_ITI_Indirizzo_Modbus.Name = "TextBox_ITI_Indirizzo_Modbus"
        Me.TextBox_ITI_Indirizzo_Modbus.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_ITI_Indirizzo_Modbus.TabIndex = 70
        '
        'Label_ITI_Indirizzo_Modbus
        '
        Me.Label_ITI_Indirizzo_Modbus.AutoSize = True
        Me.Label_ITI_Indirizzo_Modbus.Location = New System.Drawing.Point(12, 139)
        Me.Label_ITI_Indirizzo_Modbus.Name = "Label_ITI_Indirizzo_Modbus"
        Me.Label_ITI_Indirizzo_Modbus.Size = New System.Drawing.Size(86, 13)
        Me.Label_ITI_Indirizzo_Modbus.TabIndex = 71
        Me.Label_ITI_Indirizzo_Modbus.Text = "Indirizzo Modbus"
        '
        'TextBox_ITIRel
        '
        Me.TextBox_ITIRel.Location = New System.Drawing.Point(650, 109)
        Me.TextBox_ITIRel.Name = "TextBox_ITIRel"
        Me.TextBox_ITIRel.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_ITIRel.TabIndex = 94
        '
        'Label_ITIRel
        '
        Me.Label_ITIRel.AutoSize = True
        Me.Label_ITIRel.Location = New System.Drawing.Point(618, 112)
        Me.Label_ITIRel.Name = "Label_ITIRel"
        Me.Label_ITIRel.Size = New System.Drawing.Size(26, 13)
        Me.Label_ITIRel.TabIndex = 93
        Me.Label_ITIRel.Text = "Rel."
        '
        'InverterTesterInst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_ITIRel)
        Me.Controls.Add(Me.Label_ITIRel)
        Me.Controls.Add(Me.ComboBox_LI_ID)
        Me.Controls.Add(Me.Label_LI_ID)
        Me.Controls.Add(Me.Label_INT_ID)
        Me.Controls.Add(Me.ComboBox_INT_ID)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Controls.Add(Me.TextBox_ITI_Indirizzo_Modbus)
        Me.Controls.Add(Me.Label_ITI_Indirizzo_Modbus)
        Me.Name = "InverterTesterInst"
        Me.Controls.SetChildIndex(Me.Label_ITI_Indirizzo_Modbus, 0)
        Me.Controls.SetChildIndex(Me.TextBox_ITI_Indirizzo_Modbus, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_INT_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_INT_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_LI_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_ITIRel, 0)
        Me.Controls.SetChildIndex(Me.TextBox_ITIRel, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_LI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_LI_ID As System.Windows.Forms.Label
    Friend WithEvents Label_INT_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_INT_ID As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_ITI_Indirizzo_Modbus As System.Windows.Forms.TextBox
    Friend WithEvents Label_ITI_Indirizzo_Modbus As System.Windows.Forms.Label
    Friend WithEvents TextBox_ITIRel As System.Windows.Forms.TextBox
    Friend WithEvents Label_ITIRel As System.Windows.Forms.Label

End Class
