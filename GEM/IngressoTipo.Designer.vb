<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IngressoTipo
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
        Me.TextBox_IT_Nome = New System.Windows.Forms.TextBox
        Me.Label_IT_Nome = New System.Windows.Forms.Label
        Me.TextBox_IT_ID = New System.Windows.Forms.TextBox
        Me.Label_IT_ID = New System.Windows.Forms.Label
        Me.TextBox_IT_UM = New System.Windows.Forms.TextBox
        Me.Label_IT_UM = New System.Windows.Forms.Label
        Me.Label_IT_K = New System.Windows.Forms.Label
        Me.TextBox_IT_K = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TextBox_IT_Nome
        '
        Me.TextBox_IT_Nome.Location = New System.Drawing.Point(300, 54)
        Me.TextBox_IT_Nome.Name = "TextBox_IT_Nome"
        Me.TextBox_IT_Nome.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IT_Nome.TabIndex = 57
        '
        'Label_IT_Nome
        '
        Me.Label_IT_Nome.AutoSize = True
        Me.Label_IT_Nome.Location = New System.Drawing.Point(12, 57)
        Me.Label_IT_Nome.Name = "Label_IT_Nome"
        Me.Label_IT_Nome.Size = New System.Drawing.Size(44, 13)
        Me.Label_IT_Nome.TabIndex = 56
        Me.Label_IT_Nome.Text = "Modello"
        '
        'TextBox_IT_ID
        '
        Me.TextBox_IT_ID.Location = New System.Drawing.Point(300, 28)
        Me.TextBox_IT_ID.Name = "TextBox_IT_ID"
        Me.TextBox_IT_ID.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IT_ID.TabIndex = 54
        '
        'Label_IT_ID
        '
        Me.Label_IT_ID.AutoSize = True
        Me.Label_IT_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_IT_ID.Name = "Label_IT_ID"
        Me.Label_IT_ID.Size = New System.Drawing.Size(18, 13)
        Me.Label_IT_ID.TabIndex = 55
        Me.Label_IT_ID.Text = "ID"
        '
        'TextBox_IT_UM
        '
        Me.TextBox_IT_UM.Location = New System.Drawing.Point(300, 80)
        Me.TextBox_IT_UM.Name = "TextBox_IT_UM"
        Me.TextBox_IT_UM.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IT_UM.TabIndex = 58
        '
        'Label_IT_UM
        '
        Me.Label_IT_UM.AutoSize = True
        Me.Label_IT_UM.Location = New System.Drawing.Point(12, 83)
        Me.Label_IT_UM.Name = "Label_IT_UM"
        Me.Label_IT_UM.Size = New System.Drawing.Size(78, 13)
        Me.Label_IT_UM.TabIndex = 59
        Me.Label_IT_UM.Text = "Unita' di misura"
        '
        'Label_IT_K
        '
        Me.Label_IT_K.AutoSize = True
        Me.Label_IT_K.Location = New System.Drawing.Point(12, 109)
        Me.Label_IT_K.Name = "Label_IT_K"
        Me.Label_IT_K.Size = New System.Drawing.Size(14, 13)
        Me.Label_IT_K.TabIndex = 61
        Me.Label_IT_K.Text = "K"
        '
        'TextBox_IT_K
        '
        Me.TextBox_IT_K.Location = New System.Drawing.Point(300, 106)
        Me.TextBox_IT_K.Name = "TextBox_IT_K"
        Me.TextBox_IT_K.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IT_K.TabIndex = 60
        '
        'IngressoTipo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Label_IT_K)
        Me.Controls.Add(Me.TextBox_IT_K)
        Me.Controls.Add(Me.Label_IT_UM)
        Me.Controls.Add(Me.TextBox_IT_UM)
        Me.Controls.Add(Me.TextBox_IT_Nome)
        Me.Controls.Add(Me.Label_IT_Nome)
        Me.Controls.Add(Me.TextBox_IT_ID)
        Me.Controls.Add(Me.Label_IT_ID)
        Me.Name = "IngressoTipo"
        Me.Controls.SetChildIndex(Me.Label_IT_ID, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IT_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_IT_Nome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IT_Nome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IT_UM, 0)
        Me.Controls.SetChildIndex(Me.Label_IT_UM, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IT_K, 0)
        Me.Controls.SetChildIndex(Me.Label_IT_K, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_IT_Nome As System.Windows.Forms.TextBox
    Friend WithEvents Label_IT_Nome As System.Windows.Forms.Label
    Friend WithEvents TextBox_IT_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label_IT_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_IT_UM As System.Windows.Forms.TextBox
    Friend WithEvents Label_IT_UM As System.Windows.Forms.Label
    Friend WithEvents Label_IT_K As System.Windows.Forms.Label
    Friend WithEvents TextBox_IT_K As System.Windows.Forms.TextBox

End Class
