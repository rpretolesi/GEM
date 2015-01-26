<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoggerParamConfig
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
        Me.TextBox_LPC_Nome = New System.Windows.Forms.TextBox
        Me.Label_LPC_Nome = New System.Windows.Forms.Label
        Me.TextBox_LPC_Default_Value = New System.Windows.Forms.TextBox
        Me.Label_LPC_Default_Value = New System.Windows.Forms.Label
        Me.Label_LPC_Min_Value = New System.Windows.Forms.Label
        Me.TextBox_LPC_Min_Value = New System.Windows.Forms.TextBox
        Me.Label_LPC_Max_Value = New System.Windows.Forms.Label
        Me.TextBox_LPC_Max_Value = New System.Windows.Forms.TextBox
        Me.Label_LPC_UM = New System.Windows.Forms.Label
        Me.TextBox_LPC_UM = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TextBox_LPC_Nome
        '
        Me.TextBox_LPC_Nome.Location = New System.Drawing.Point(212, 31)
        Me.TextBox_LPC_Nome.Name = "TextBox_LPC_Nome"
        Me.TextBox_LPC_Nome.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_LPC_Nome.TabIndex = 38
        '
        'Label_LPC_Nome
        '
        Me.Label_LPC_Nome.AutoSize = True
        Me.Label_LPC_Nome.Location = New System.Drawing.Point(12, 34)
        Me.Label_LPC_Nome.Name = "Label_LPC_Nome"
        Me.Label_LPC_Nome.Size = New System.Drawing.Size(35, 13)
        Me.Label_LPC_Nome.TabIndex = 39
        Me.Label_LPC_Nome.Text = "Nome"
        '
        'TextBox_LPC_Default_Value
        '
        Me.TextBox_LPC_Default_Value.Location = New System.Drawing.Point(212, 135)
        Me.TextBox_LPC_Default_Value.Name = "TextBox_LPC_Default_Value"
        Me.TextBox_LPC_Default_Value.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_LPC_Default_Value.TabIndex = 40
        '
        'Label_LPC_Default_Value
        '
        Me.Label_LPC_Default_Value.AutoSize = True
        Me.Label_LPC_Default_Value.Location = New System.Drawing.Point(12, 138)
        Me.Label_LPC_Default_Value.Name = "Label_LPC_Default_Value"
        Me.Label_LPC_Default_Value.Size = New System.Drawing.Size(85, 13)
        Me.Label_LPC_Default_Value.TabIndex = 41
        Me.Label_LPC_Default_Value.Text = "Valore di Default"
        '
        'Label_LPC_Min_Value
        '
        Me.Label_LPC_Min_Value.AutoSize = True
        Me.Label_LPC_Min_Value.Location = New System.Drawing.Point(12, 60)
        Me.Label_LPC_Min_Value.Name = "Label_LPC_Min_Value"
        Me.Label_LPC_Min_Value.Size = New System.Drawing.Size(24, 13)
        Me.Label_LPC_Min_Value.TabIndex = 42
        Me.Label_LPC_Min_Value.Text = "Min"
        '
        'TextBox_LPC_Min_Value
        '
        Me.TextBox_LPC_Min_Value.Location = New System.Drawing.Point(212, 57)
        Me.TextBox_LPC_Min_Value.Name = "TextBox_LPC_Min_Value"
        Me.TextBox_LPC_Min_Value.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_LPC_Min_Value.TabIndex = 43
        '
        'Label_LPC_Max_Value
        '
        Me.Label_LPC_Max_Value.AutoSize = True
        Me.Label_LPC_Max_Value.Location = New System.Drawing.Point(12, 86)
        Me.Label_LPC_Max_Value.Name = "Label_LPC_Max_Value"
        Me.Label_LPC_Max_Value.Size = New System.Drawing.Size(27, 13)
        Me.Label_LPC_Max_Value.TabIndex = 44
        Me.Label_LPC_Max_Value.Text = "Max"
        '
        'TextBox_LPC_Max_Value
        '
        Me.TextBox_LPC_Max_Value.Location = New System.Drawing.Point(212, 83)
        Me.TextBox_LPC_Max_Value.Name = "TextBox_LPC_Max_Value"
        Me.TextBox_LPC_Max_Value.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_LPC_Max_Value.TabIndex = 45
        '
        'Label_LPC_UM
        '
        Me.Label_LPC_UM.AutoSize = True
        Me.Label_LPC_UM.Location = New System.Drawing.Point(12, 112)
        Me.Label_LPC_UM.Name = "Label_LPC_UM"
        Me.Label_LPC_UM.Size = New System.Drawing.Size(79, 13)
        Me.Label_LPC_UM.TabIndex = 46
        Me.Label_LPC_UM.Text = "Unita' di Misura"
        '
        'TextBox_LPC_UM
        '
        Me.TextBox_LPC_UM.Location = New System.Drawing.Point(212, 109)
        Me.TextBox_LPC_UM.Name = "TextBox_LPC_UM"
        Me.TextBox_LPC_UM.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_LPC_UM.TabIndex = 47
        '
        'LoggerParamConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_LPC_UM)
        Me.Controls.Add(Me.Label_LPC_UM)
        Me.Controls.Add(Me.TextBox_LPC_Max_Value)
        Me.Controls.Add(Me.Label_LPC_Max_Value)
        Me.Controls.Add(Me.TextBox_LPC_Min_Value)
        Me.Controls.Add(Me.Label_LPC_Min_Value)
        Me.Controls.Add(Me.TextBox_LPC_Default_Value)
        Me.Controls.Add(Me.Label_LPC_Default_Value)
        Me.Controls.Add(Me.TextBox_LPC_Nome)
        Me.Controls.Add(Me.Label_LPC_Nome)
        Me.Name = "LoggerParamConfig"
        Me.Controls.SetChildIndex(Me.Label_LPC_Nome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LPC_Nome, 0)
        Me.Controls.SetChildIndex(Me.Label_LPC_Default_Value, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LPC_Default_Value, 0)
        Me.Controls.SetChildIndex(Me.Label_LPC_Min_Value, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LPC_Min_Value, 0)
        Me.Controls.SetChildIndex(Me.Label_LPC_Max_Value, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LPC_Max_Value, 0)
        Me.Controls.SetChildIndex(Me.Label_LPC_UM, 0)
        Me.Controls.SetChildIndex(Me.TextBox_LPC_UM, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_LPC_Nome As System.Windows.Forms.TextBox
    Friend WithEvents Label_LPC_Nome As System.Windows.Forms.Label
    Friend WithEvents TextBox_LPC_Default_Value As System.Windows.Forms.TextBox
    Friend WithEvents Label_LPC_Default_Value As System.Windows.Forms.Label
    Friend WithEvents Label_LPC_Min_Value As System.Windows.Forms.Label
    Friend WithEvents TextBox_LPC_Min_Value As System.Windows.Forms.TextBox
    Friend WithEvents Label_LPC_Max_Value As System.Windows.Forms.Label
    Friend WithEvents TextBox_LPC_Max_Value As System.Windows.Forms.TextBox
    Friend WithEvents Label_LPC_UM As System.Windows.Forms.Label
    Friend WithEvents TextBox_LPC_UM As System.Windows.Forms.TextBox

End Class
