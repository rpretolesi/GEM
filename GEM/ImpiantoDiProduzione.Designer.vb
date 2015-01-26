<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImpiantoDiProduzione
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
        Me.TextBox_IDP_CAP = New System.Windows.Forms.TextBox
        Me.Label_IDP_CAP = New System.Windows.Forms.Label
        Me.TextBox_IDP_Indirizzo = New System.Windows.Forms.TextBox
        Me.Label_IDP_Indirizzo = New System.Windows.Forms.Label
        Me.TextBox_IDP_Nome = New System.Windows.Forms.TextBox
        Me.Label_IDP_Nome = New System.Windows.Forms.Label
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'TextBox_IDP_CAP
        '
        Me.TextBox_IDP_CAP.Location = New System.Drawing.Point(250, 107)
        Me.TextBox_IDP_CAP.Name = "TextBox_IDP_CAP"
        Me.TextBox_IDP_CAP.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IDP_CAP.TabIndex = 41
        '
        'Label_IDP_CAP
        '
        Me.Label_IDP_CAP.AutoSize = True
        Me.Label_IDP_CAP.Location = New System.Drawing.Point(12, 110)
        Me.Label_IDP_CAP.Name = "Label_IDP_CAP"
        Me.Label_IDP_CAP.Size = New System.Drawing.Size(43, 13)
        Me.Label_IDP_CAP.TabIndex = 40
        Me.Label_IDP_CAP.Text = "Societa"
        '
        'TextBox_IDP_Indirizzo
        '
        Me.TextBox_IDP_Indirizzo.Location = New System.Drawing.Point(250, 81)
        Me.TextBox_IDP_Indirizzo.Name = "TextBox_IDP_Indirizzo"
        Me.TextBox_IDP_Indirizzo.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IDP_Indirizzo.TabIndex = 39
        '
        'Label_IDP_Indirizzo
        '
        Me.Label_IDP_Indirizzo.AutoSize = True
        Me.Label_IDP_Indirizzo.Location = New System.Drawing.Point(12, 84)
        Me.Label_IDP_Indirizzo.Name = "Label_IDP_Indirizzo"
        Me.Label_IDP_Indirizzo.Size = New System.Drawing.Size(52, 13)
        Me.Label_IDP_Indirizzo.TabIndex = 38
        Me.Label_IDP_Indirizzo.Text = "Cognome"
        '
        'TextBox_IDP_Nome
        '
        Me.TextBox_IDP_Nome.Location = New System.Drawing.Point(250, 55)
        Me.TextBox_IDP_Nome.Name = "TextBox_IDP_Nome"
        Me.TextBox_IDP_Nome.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IDP_Nome.TabIndex = 36
        '
        'Label_IDP_Nome
        '
        Me.Label_IDP_Nome.AutoSize = True
        Me.Label_IDP_Nome.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_Nome.Name = "Label_IDP_Nome"
        Me.Label_IDP_Nome.Size = New System.Drawing.Size(35, 13)
        Me.Label_IDP_Nome.TabIndex = 37
        Me.Label_IDP_Nome.Text = "Nome"
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 42
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 43
        '
        'ImpiantoDiProduzione
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.TextBox_IDP_CAP)
        Me.Controls.Add(Me.Label_IDP_CAP)
        Me.Controls.Add(Me.TextBox_IDP_Indirizzo)
        Me.Controls.Add(Me.Label_IDP_Indirizzo)
        Me.Controls.Add(Me.TextBox_IDP_Nome)
        Me.Controls.Add(Me.Label_IDP_Nome)
        Me.Name = "ImpiantoDiProduzione"
        Me.Controls.SetChildIndex(Me.Label_IDP_Nome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IDP_Nome, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_Indirizzo, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IDP_Indirizzo, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_CAP, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IDP_CAP, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_IDP_CAP As System.Windows.Forms.TextBox
    Friend WithEvents Label_IDP_CAP As System.Windows.Forms.Label
    Friend WithEvents TextBox_IDP_Indirizzo As System.Windows.Forms.TextBox
    Friend WithEvents Label_IDP_Indirizzo As System.Windows.Forms.Label
    Friend WithEvents TextBox_IDP_Nome As System.Windows.Forms.TextBox
    Friend WithEvents Label_IDP_Nome As System.Windows.Forms.Label
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox

End Class
