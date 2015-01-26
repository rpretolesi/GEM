<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContatoreDiProduzioneInst
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
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.Label_CDP_ID = New System.Windows.Forms.Label
        Me.ComboBox_CDP_ID = New System.Windows.Forms.ComboBox
        Me.TextBox_CDPI_Nr = New System.Windows.Forms.TextBox
        Me.Label_CDPI_Nr = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 59
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 58
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 57
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 56
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'Label_CDP_ID
        '
        Me.Label_CDP_ID.AutoSize = True
        Me.Label_CDP_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_CDP_ID.Name = "Label_CDP_ID"
        Me.Label_CDP_ID.Size = New System.Drawing.Size(122, 13)
        Me.Label_CDP_ID.TabIndex = 63
        Me.Label_CDP_ID.Text = "Contatore Di Produzione"
        '
        'ComboBox_CDP_ID
        '
        Me.ComboBox_CDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CDP_ID.FormattingEnabled = True
        Me.ComboBox_CDP_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_CDP_ID.Name = "ComboBox_CDP_ID"
        Me.ComboBox_CDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_CDP_ID.TabIndex = 62
        '
        'TextBox_CDPI_Nr
        '
        Me.TextBox_CDPI_Nr.Location = New System.Drawing.Point(250, 109)
        Me.TextBox_CDPI_Nr.Name = "TextBox_CDPI_Nr"
        Me.TextBox_CDPI_Nr.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_CDPI_Nr.TabIndex = 60
        '
        'Label_CDPI_Nr
        '
        Me.Label_CDPI_Nr.AutoSize = True
        Me.Label_CDPI_Nr.Location = New System.Drawing.Point(12, 112)
        Me.Label_CDPI_Nr.Name = "Label_CDPI_Nr"
        Me.Label_CDPI_Nr.Size = New System.Drawing.Size(162, 13)
        Me.Label_CDPI_Nr.TabIndex = 61
        Me.Label_CDPI_Nr.Text = "Numero Contatore Di Produzione"
        '
        'ContatoreDiProduzioneInst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Label_CDP_ID)
        Me.Controls.Add(Me.ComboBox_CDP_ID)
        Me.Controls.Add(Me.TextBox_CDPI_Nr)
        Me.Controls.Add(Me.Label_CDPI_Nr)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Name = "ContatoreDiProduzioneInst"
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_CDPI_Nr, 0)
        Me.Controls.SetChildIndex(Me.TextBox_CDPI_Nr, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_CDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_CDP_ID, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents Label_CDP_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_CDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox_CDPI_Nr As System.Windows.Forms.TextBox
    Friend WithEvents Label_CDPI_Nr As System.Windows.Forms.Label

End Class
