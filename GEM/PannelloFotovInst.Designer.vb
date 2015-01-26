<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PannelloFotovInst
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
        Me.ComboBox_PF_ID = New System.Windows.Forms.ComboBox
        Me.Label_PF_ID = New System.Windows.Forms.Label
        Me.ComboBox_PFS_ID = New System.Windows.Forms.ComboBox
        Me.Label_PFS_ID = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.TextBox_PFI_Nr = New System.Windows.Forms.TextBox
        Me.Label_PFI_Nr = New System.Windows.Forms.Label
        Me.ComboBox_CDPI_ID = New System.Windows.Forms.ComboBox
        Me.Label_CDPI_ID = New System.Windows.Forms.Label
        Me.ComboBox_IFI_ID = New System.Windows.Forms.ComboBox
        Me.Label_IFI_ID = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ComboBox_PF_ID
        '
        Me.ComboBox_PF_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PF_ID.FormattingEnabled = True
        Me.ComboBox_PF_ID.Location = New System.Drawing.Point(250, 163)
        Me.ComboBox_PF_ID.Name = "ComboBox_PF_ID"
        Me.ComboBox_PF_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_PF_ID.TabIndex = 45
        '
        'Label_PF_ID
        '
        Me.Label_PF_ID.AutoSize = True
        Me.Label_PF_ID.Location = New System.Drawing.Point(12, 166)
        Me.Label_PF_ID.Name = "Label_PF_ID"
        Me.Label_PF_ID.Size = New System.Drawing.Size(133, 13)
        Me.Label_PF_ID.TabIndex = 44
        Me.Label_PF_ID.Text = "Tipo Pannello Fotovoltaico"
        '
        'ComboBox_PFS_ID
        '
        Me.ComboBox_PFS_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PFS_ID.FormattingEnabled = True
        Me.ComboBox_PFS_ID.Location = New System.Drawing.Point(250, 136)
        Me.ComboBox_PFS_ID.Name = "ComboBox_PFS_ID"
        Me.ComboBox_PFS_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_PFS_ID.TabIndex = 47
        '
        'Label_PFS_ID
        '
        Me.Label_PFS_ID.AutoSize = True
        Me.Label_PFS_ID.Location = New System.Drawing.Point(12, 139)
        Me.Label_PFS_ID.Name = "Label_PFS_ID"
        Me.Label_PFS_ID.Size = New System.Drawing.Size(125, 13)
        Me.Label_PFS_ID.TabIndex = 46
        Me.Label_PFS_ID.Text = "Numero Stringa Installata"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 53
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 52
        Me.Label_C_ID.Text = "Cliente"
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 51
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 50
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'TextBox_PFI_Nr
        '
        Me.TextBox_PFI_Nr.Location = New System.Drawing.Point(250, 190)
        Me.TextBox_PFI_Nr.Name = "TextBox_PFI_Nr"
        Me.TextBox_PFI_Nr.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PFI_Nr.TabIndex = 55
        '
        'Label_PFI_Nr
        '
        Me.Label_PFI_Nr.AutoSize = True
        Me.Label_PFI_Nr.Location = New System.Drawing.Point(12, 193)
        Me.Label_PFI_Nr.Name = "Label_PFI_Nr"
        Me.Label_PFI_Nr.Size = New System.Drawing.Size(88, 13)
        Me.Label_PFI_Nr.TabIndex = 54
        Me.Label_PFI_Nr.Text = "Numero Pannello"
        '
        'ComboBox_CDPI_ID
        '
        Me.ComboBox_CDPI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CDPI_ID.FormattingEnabled = True
        Me.ComboBox_CDPI_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_CDPI_ID.Name = "ComboBox_CDPI_ID"
        Me.ComboBox_CDPI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_CDPI_ID.TabIndex = 63
        '
        'Label_CDPI_ID
        '
        Me.Label_CDPI_ID.AutoSize = True
        Me.Label_CDPI_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_CDPI_ID.Name = "Label_CDPI_ID"
        Me.Label_CDPI_ID.Size = New System.Drawing.Size(207, 13)
        Me.Label_CDPI_ID.TabIndex = 62
        Me.Label_CDPI_ID.Text = "Numero Contatore Di Produzione Installato"
        '
        'ComboBox_IFI_ID
        '
        Me.ComboBox_IFI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IFI_ID.FormattingEnabled = True
        Me.ComboBox_IFI_ID.Location = New System.Drawing.Point(250, 109)
        Me.ComboBox_IFI_ID.Name = "ComboBox_IFI_ID"
        Me.ComboBox_IFI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IFI_ID.TabIndex = 65
        '
        'Label_IFI_ID
        '
        Me.Label_IFI_ID.AutoSize = True
        Me.Label_IFI_ID.Location = New System.Drawing.Point(12, 112)
        Me.Label_IFI_ID.Name = "Label_IFI_ID"
        Me.Label_IFI_ID.Size = New System.Drawing.Size(189, 13)
        Me.Label_IFI_ID.TabIndex = 64
        Me.Label_IFI_ID.Text = "Numero Inverter Fotovoltaico Installato"
        '
        'PannelloFotovInst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.ComboBox_IFI_ID)
        Me.Controls.Add(Me.Label_IFI_ID)
        Me.Controls.Add(Me.ComboBox_CDPI_ID)
        Me.Controls.Add(Me.Label_CDPI_ID)
        Me.Controls.Add(Me.TextBox_PFI_Nr)
        Me.Controls.Add(Me.Label_PFI_Nr)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Controls.Add(Me.ComboBox_PFS_ID)
        Me.Controls.Add(Me.Label_PFS_ID)
        Me.Controls.Add(Me.ComboBox_PF_ID)
        Me.Controls.Add(Me.Label_PF_ID)
        Me.Name = "PannelloFotovInst"
        Me.Controls.SetChildIndex(Me.Label_PF_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_PF_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_PFS_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_PFS_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_PFI_Nr, 0)
        Me.Controls.SetChildIndex(Me.TextBox_PFI_Nr, 0)
        Me.Controls.SetChildIndex(Me.Label_CDPI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_CDPI_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_IFI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IFI_ID, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_PF_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_PF_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_PFS_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_PFS_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_PFI_Nr As System.Windows.Forms.TextBox
    Friend WithEvents Label_PFI_Nr As System.Windows.Forms.Label
    Friend WithEvents ComboBox_CDPI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_CDPI_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IFI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IFI_ID As System.Windows.Forms.Label

End Class
