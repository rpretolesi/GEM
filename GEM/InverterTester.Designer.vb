<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InverterTester
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
        Me.TextBox_INT_Modello = New System.Windows.Forms.TextBox
        Me.Label_INT_Modello = New System.Windows.Forms.Label
        Me.TextBox_INT_Marca = New System.Windows.Forms.TextBox
        Me.Label_INT_Marca = New System.Windows.Forms.Label
        Me.Label_INT_Tipo = New System.Windows.Forms.Label
        Me.ComboBox_INT_Tipo = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'TextBox_INT_Modello
        '
        Me.TextBox_INT_Modello.Location = New System.Drawing.Point(250, 54)
        Me.TextBox_INT_Modello.Name = "TextBox_INT_Modello"
        Me.TextBox_INT_Modello.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_INT_Modello.TabIndex = 53
        '
        'Label_INT_Modello
        '
        Me.Label_INT_Modello.AutoSize = True
        Me.Label_INT_Modello.Location = New System.Drawing.Point(12, 57)
        Me.Label_INT_Modello.Name = "Label_INT_Modello"
        Me.Label_INT_Modello.Size = New System.Drawing.Size(44, 13)
        Me.Label_INT_Modello.TabIndex = 52
        Me.Label_INT_Modello.Text = "Modello"
        '
        'TextBox_INT_Marca
        '
        Me.TextBox_INT_Marca.Location = New System.Drawing.Point(250, 28)
        Me.TextBox_INT_Marca.Name = "TextBox_INT_Marca"
        Me.TextBox_INT_Marca.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_INT_Marca.TabIndex = 50
        '
        'Label_INT_Marca
        '
        Me.Label_INT_Marca.AutoSize = True
        Me.Label_INT_Marca.Location = New System.Drawing.Point(12, 31)
        Me.Label_INT_Marca.Name = "Label_INT_Marca"
        Me.Label_INT_Marca.Size = New System.Drawing.Size(37, 13)
        Me.Label_INT_Marca.TabIndex = 51
        Me.Label_INT_Marca.Text = "Marca"
        '
        'Label_INT_Tipo
        '
        Me.Label_INT_Tipo.AutoSize = True
        Me.Label_INT_Tipo.Location = New System.Drawing.Point(12, 83)
        Me.Label_INT_Tipo.Name = "Label_INT_Tipo"
        Me.Label_INT_Tipo.Size = New System.Drawing.Size(44, 13)
        Me.Label_INT_Tipo.TabIndex = 54
        Me.Label_INT_Tipo.Text = "Modello"
        '
        'ComboBox_INT_Tipo
        '
        Me.ComboBox_INT_Tipo.FormattingEnabled = True
        Me.ComboBox_INT_Tipo.Items.AddRange(New Object() {"21", "22", "23", "24"})
        Me.ComboBox_INT_Tipo.Location = New System.Drawing.Point(250, 80)
        Me.ComboBox_INT_Tipo.Name = "ComboBox_INT_Tipo"
        Me.ComboBox_INT_Tipo.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox_INT_Tipo.TabIndex = 56
        '
        'InverterTester
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.ComboBox_INT_Tipo)
        Me.Controls.Add(Me.Label_INT_Tipo)
        Me.Controls.Add(Me.TextBox_INT_Modello)
        Me.Controls.Add(Me.Label_INT_Modello)
        Me.Controls.Add(Me.TextBox_INT_Marca)
        Me.Controls.Add(Me.Label_INT_Marca)
        Me.Name = "InverterTester"
        Me.Controls.SetChildIndex(Me.Label_INT_Marca, 0)
        Me.Controls.SetChildIndex(Me.TextBox_INT_Marca, 0)
        Me.Controls.SetChildIndex(Me.Label_INT_Modello, 0)
        Me.Controls.SetChildIndex(Me.TextBox_INT_Modello, 0)
        Me.Controls.SetChildIndex(Me.Label_INT_Tipo, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_INT_Tipo, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_INT_Modello As System.Windows.Forms.TextBox
    Friend WithEvents Label_INT_Modello As System.Windows.Forms.Label
    Friend WithEvents TextBox_INT_Marca As System.Windows.Forms.TextBox
    Friend WithEvents Label_INT_Marca As System.Windows.Forms.Label
    Friend WithEvents Label_INT_Tipo As System.Windows.Forms.Label
    Friend WithEvents ComboBox_INT_Tipo As System.Windows.Forms.ComboBox

End Class
