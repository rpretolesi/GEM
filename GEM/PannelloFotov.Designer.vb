<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PannelloFotov
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
        Me.TextBox_PF_Modello = New System.Windows.Forms.TextBox
        Me.Label_PF_Modello = New System.Windows.Forms.Label
        Me.TextBox_PF_Marca = New System.Windows.Forms.TextBox
        Me.Label_PF_Marca = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TextBox_PF_Modello
        '
        Me.TextBox_PF_Modello.Location = New System.Drawing.Point(250, 54)
        Me.TextBox_PF_Modello.Name = "TextBox_PF_Modello"
        Me.TextBox_PF_Modello.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PF_Modello.TabIndex = 37
        '
        'Label_PF_Modello
        '
        Me.Label_PF_Modello.AutoSize = True
        Me.Label_PF_Modello.Location = New System.Drawing.Point(12, 57)
        Me.Label_PF_Modello.Name = "Label_PF_Modello"
        Me.Label_PF_Modello.Size = New System.Drawing.Size(44, 13)
        Me.Label_PF_Modello.TabIndex = 36
        Me.Label_PF_Modello.Text = "Modello"
        '
        'TextBox_PF_Marca
        '
        Me.TextBox_PF_Marca.Location = New System.Drawing.Point(250, 28)
        Me.TextBox_PF_Marca.Name = "TextBox_PF_Marca"
        Me.TextBox_PF_Marca.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PF_Marca.TabIndex = 34
        '
        'Label_PF_Marca
        '
        Me.Label_PF_Marca.AutoSize = True
        Me.Label_PF_Marca.Location = New System.Drawing.Point(12, 31)
        Me.Label_PF_Marca.Name = "Label_PF_Marca"
        Me.Label_PF_Marca.Size = New System.Drawing.Size(37, 13)
        Me.Label_PF_Marca.TabIndex = 35
        Me.Label_PF_Marca.Text = "Marca"
        '
        'PannelloFotov
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_PF_Modello)
        Me.Controls.Add(Me.Label_PF_Modello)
        Me.Controls.Add(Me.TextBox_PF_Marca)
        Me.Controls.Add(Me.Label_PF_Marca)
        Me.Name = "PannelloFotov"
        Me.Controls.SetChildIndex(Me.Label_PF_Marca, 0)
        Me.Controls.SetChildIndex(Me.TextBox_PF_Marca, 0)
        Me.Controls.SetChildIndex(Me.Label_PF_Modello, 0)
        Me.Controls.SetChildIndex(Me.TextBox_PF_Modello, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_PF_Modello As System.Windows.Forms.TextBox
    Friend WithEvents Label_PF_Modello As System.Windows.Forms.Label
    Friend WithEvents TextBox_PF_Marca As System.Windows.Forms.TextBox
    Friend WithEvents Label_PF_Marca As System.Windows.Forms.Label

End Class
