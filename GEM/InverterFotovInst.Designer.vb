<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InverterFotovInst
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
        Me.TextBox_IFI_Nr = New System.Windows.Forms.TextBox
        Me.Label_IFI_Nr = New System.Windows.Forms.Label
        Me.ComboBox_IF_ID = New System.Windows.Forms.ComboBox
        Me.Label_IF_ID = New System.Windows.Forms.Label
        Me.Label_CDPI_ID = New System.Windows.Forms.Label
        Me.ComboBox_CDPI_ID = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 55
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(9, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 54
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 53
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 52
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'TextBox_IFI_Nr
        '
        Me.TextBox_IFI_Nr.Location = New System.Drawing.Point(250, 136)
        Me.TextBox_IFI_Nr.Name = "TextBox_IFI_Nr"
        Me.TextBox_IFI_Nr.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_IFI_Nr.TabIndex = 50
        '
        'Label_IFI_Nr
        '
        Me.Label_IFI_Nr.AutoSize = True
        Me.Label_IFI_Nr.Location = New System.Drawing.Point(12, 139)
        Me.Label_IFI_Nr.Name = "Label_IFI_Nr"
        Me.Label_IFI_Nr.Size = New System.Drawing.Size(83, 13)
        Me.Label_IFI_Nr.TabIndex = 51
        Me.Label_IFI_Nr.Text = "Numero Inverter"
        '
        'ComboBox_IF_ID
        '
        Me.ComboBox_IF_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IF_ID.FormattingEnabled = True
        Me.ComboBox_IF_ID.Location = New System.Drawing.Point(250, 109)
        Me.ComboBox_IF_ID.Name = "ComboBox_IF_ID"
        Me.ComboBox_IF_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IF_ID.TabIndex = 56
        '
        'Label_IF_ID
        '
        Me.Label_IF_ID.AutoSize = True
        Me.Label_IF_ID.Location = New System.Drawing.Point(12, 112)
        Me.Label_IF_ID.Name = "Label_IF_ID"
        Me.Label_IF_ID.Size = New System.Drawing.Size(128, 13)
        Me.Label_IF_ID.TabIndex = 57
        Me.Label_IF_ID.Text = "Tipo Inverter Fotovoltaico"
        '
        'Label_CDPI_ID
        '
        Me.Label_CDPI_ID.AutoSize = True
        Me.Label_CDPI_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_CDPI_ID.Name = "Label_CDPI_ID"
        Me.Label_CDPI_ID.Size = New System.Drawing.Size(207, 13)
        Me.Label_CDPI_ID.TabIndex = 58
        Me.Label_CDPI_ID.Text = "Numero Contatore Di Produzione Installato"
        '
        'ComboBox_CDPI_ID
        '
        Me.ComboBox_CDPI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CDPI_ID.FormattingEnabled = True
        Me.ComboBox_CDPI_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_CDPI_ID.Name = "ComboBox_CDPI_ID"
        Me.ComboBox_CDPI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_CDPI_ID.TabIndex = 59
        '
        'InverterFotovInst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.ComboBox_CDPI_ID)
        Me.Controls.Add(Me.Label_CDPI_ID)
        Me.Controls.Add(Me.Label_IF_ID)
        Me.Controls.Add(Me.ComboBox_IF_ID)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Controls.Add(Me.TextBox_IFI_Nr)
        Me.Controls.Add(Me.Label_IFI_Nr)
        Me.Name = "InverterFotovInst"
        Me.Controls.SetChildIndex(Me.Label_IFI_Nr, 0)
        Me.Controls.SetChildIndex(Me.TextBox_IFI_Nr, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IF_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_IF_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_CDPI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_CDPI_ID, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_IFI_Nr As System.Windows.Forms.TextBox
    Friend WithEvents Label_IFI_Nr As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IF_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IF_ID As System.Windows.Forms.Label
    Friend WithEvents Label_CDPI_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_CDPI_ID As System.Windows.Forms.ComboBox

End Class
