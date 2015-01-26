<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PannelloFotovString
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
        Me.ComboBox_IDP_ID = New System.Windows.Forms.ComboBox
        Me.Label_IDP_ID = New System.Windows.Forms.Label
        Me.TextBox_PFS_Nr = New System.Windows.Forms.TextBox
        Me.Label_PFS_Nr = New System.Windows.Forms.Label
        Me.ComboBox_C_ID = New System.Windows.Forms.ComboBox
        Me.Label_C_ID = New System.Windows.Forms.Label
        Me.Label_IFI_ID = New System.Windows.Forms.Label
        Me.ComboBox_IFI_ID = New System.Windows.Forms.ComboBox
        Me.ComboBox_CDPI_ID = New System.Windows.Forms.ComboBox
        Me.Label_CDPI_ID = New System.Windows.Forms.Label
        Me.TextBox_PFI_Nr = New System.Windows.Forms.TextBox
        Me.ComboBox_PF_ID = New System.Windows.Forms.ComboBox
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici = New System.Windows.Forms.Button
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici = New System.Windows.Forms.Button
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe = New System.Windows.Forms.Button
        Me.TextBox_Nr_Stringhe_Da_Aggiungere = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ComboBox_IDP_ID
        '
        Me.ComboBox_IDP_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IDP_ID.FormattingEnabled = True
        Me.ComboBox_IDP_ID.Location = New System.Drawing.Point(250, 55)
        Me.ComboBox_IDP_ID.Name = "ComboBox_IDP_ID"
        Me.ComboBox_IDP_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IDP_ID.TabIndex = 47
        '
        'Label_IDP_ID
        '
        Me.Label_IDP_ID.AutoSize = True
        Me.Label_IDP_ID.Location = New System.Drawing.Point(12, 58)
        Me.Label_IDP_ID.Name = "Label_IDP_ID"
        Me.Label_IDP_ID.Size = New System.Drawing.Size(113, 13)
        Me.Label_IDP_ID.TabIndex = 46
        Me.Label_IDP_ID.Text = "Impianto di produzione"
        '
        'TextBox_PFS_Nr
        '
        Me.TextBox_PFS_Nr.Location = New System.Drawing.Point(250, 136)
        Me.TextBox_PFS_Nr.Name = "TextBox_PFS_Nr"
        Me.TextBox_PFS_Nr.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PFS_Nr.TabIndex = 44
        '
        'Label_PFS_Nr
        '
        Me.Label_PFS_Nr.AutoSize = True
        Me.Label_PFS_Nr.Location = New System.Drawing.Point(12, 139)
        Me.Label_PFS_Nr.Name = "Label_PFS_Nr"
        Me.Label_PFS_Nr.Size = New System.Drawing.Size(80, 13)
        Me.Label_PFS_Nr.TabIndex = 45
        Me.Label_PFS_Nr.Text = "Numero Stringa"
        '
        'ComboBox_C_ID
        '
        Me.ComboBox_C_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_C_ID.FormattingEnabled = True
        Me.ComboBox_C_ID.Location = New System.Drawing.Point(250, 28)
        Me.ComboBox_C_ID.Name = "ComboBox_C_ID"
        Me.ComboBox_C_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_C_ID.TabIndex = 49
        '
        'Label_C_ID
        '
        Me.Label_C_ID.AutoSize = True
        Me.Label_C_ID.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_ID.Name = "Label_C_ID"
        Me.Label_C_ID.Size = New System.Drawing.Size(39, 13)
        Me.Label_C_ID.TabIndex = 48
        Me.Label_C_ID.Text = "Cliente"
        '
        'Label_IFI_ID
        '
        Me.Label_IFI_ID.AutoSize = True
        Me.Label_IFI_ID.Location = New System.Drawing.Point(12, 112)
        Me.Label_IFI_ID.Name = "Label_IFI_ID"
        Me.Label_IFI_ID.Size = New System.Drawing.Size(189, 13)
        Me.Label_IFI_ID.TabIndex = 50
        Me.Label_IFI_ID.Text = "Numero Inverter Fotovoltaico Installato"
        '
        'ComboBox_IFI_ID
        '
        Me.ComboBox_IFI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IFI_ID.FormattingEnabled = True
        Me.ComboBox_IFI_ID.Location = New System.Drawing.Point(250, 109)
        Me.ComboBox_IFI_ID.Name = "ComboBox_IFI_ID"
        Me.ComboBox_IFI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_IFI_ID.TabIndex = 51
        '
        'ComboBox_CDPI_ID
        '
        Me.ComboBox_CDPI_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CDPI_ID.FormattingEnabled = True
        Me.ComboBox_CDPI_ID.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox_CDPI_ID.Name = "ComboBox_CDPI_ID"
        Me.ComboBox_CDPI_ID.Size = New System.Drawing.Size(500, 21)
        Me.ComboBox_CDPI_ID.TabIndex = 61
        '
        'Label_CDPI_ID
        '
        Me.Label_CDPI_ID.AutoSize = True
        Me.Label_CDPI_ID.Location = New System.Drawing.Point(12, 85)
        Me.Label_CDPI_ID.Name = "Label_CDPI_ID"
        Me.Label_CDPI_ID.Size = New System.Drawing.Size(207, 13)
        Me.Label_CDPI_ID.TabIndex = 60
        Me.Label_CDPI_ID.Text = "Numero Contatore Di Produzione Installato"
        '
        'TextBox_PFI_Nr
        '
        Me.TextBox_PFI_Nr.Location = New System.Drawing.Point(524, 229)
        Me.TextBox_PFI_Nr.Name = "TextBox_PFI_Nr"
        Me.TextBox_PFI_Nr.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_PFI_Nr.TabIndex = 63
        '
        'ComboBox_PF_ID
        '
        Me.ComboBox_PF_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PF_ID.FormattingEnabled = True
        Me.ComboBox_PF_ID.Location = New System.Drawing.Point(580, 228)
        Me.ComboBox_PF_ID.Name = "ComboBox_PF_ID"
        Me.ComboBox_PF_ID.Size = New System.Drawing.Size(200, 21)
        Me.ComboBox_PF_ID.TabIndex = 62
        '
        'Button_Aggiungi_Tutti_Pannelli_Fotovoltaici
        '
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.Location = New System.Drawing.Point(12, 229)
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.Name = "Button_Aggiungi_Tutti_Pannelli_Fotovoltaici"
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.Size = New System.Drawing.Size(450, 23)
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.TabIndex = 64
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.Text = "Aggiungi il seguente Nr Di Pannelli, del Tipo Selezionato, per tutte le Stringhe " & _
            "Visualizzate ->"
        Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.UseVisualStyleBackColor = True
        '
        'Button_Elimina_Tutti_Pannelli_Fotovoltaici
        '
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.Location = New System.Drawing.Point(12, 258)
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.Name = "Button_Elimina_Tutti_Pannelli_Fotovoltaici"
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.Size = New System.Drawing.Size(768, 23)
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.TabIndex = 65
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.Text = "Elimina Tutti i Pannelli, del Tipo Selezionato, per tutte le Stringhe Visualizzat" & _
            "e"
        Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici.UseVisualStyleBackColor = True
        '
        'Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe
        '
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Location = New System.Drawing.Point(376, 134)
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Name = "Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe"
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Size = New System.Drawing.Size(290, 23)
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.TabIndex = 66
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Text = "Aggiungi Automaticamente questo Nr di Stringhe ->"
        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.UseVisualStyleBackColor = True
        '
        'TextBox_Nr_Stringhe_Da_Aggiungere
        '
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Location = New System.Drawing.Point(672, 136)
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Name = "TextBox_Nr_Stringhe_Da_Aggiungere"
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Size = New System.Drawing.Size(78, 20)
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.TabIndex = 67
        '
        'PannelloFotovString
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_Nr_Stringhe_Da_Aggiungere)
        Me.Controls.Add(Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici)
        Me.Controls.Add(Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici)
        Me.Controls.Add(Me.TextBox_PFI_Nr)
        Me.Controls.Add(Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe)
        Me.Controls.Add(Me.ComboBox_PF_ID)
        Me.Controls.Add(Me.ComboBox_CDPI_ID)
        Me.Controls.Add(Me.Label_CDPI_ID)
        Me.Controls.Add(Me.ComboBox_IFI_ID)
        Me.Controls.Add(Me.Label_IFI_ID)
        Me.Controls.Add(Me.ComboBox_C_ID)
        Me.Controls.Add(Me.Label_C_ID)
        Me.Controls.Add(Me.ComboBox_IDP_ID)
        Me.Controls.Add(Me.Label_IDP_ID)
        Me.Controls.Add(Me.TextBox_PFS_Nr)
        Me.Controls.Add(Me.Label_PFS_Nr)
        Me.Name = "PannelloFotovString"
        Me.Controls.SetChildIndex(Me.Label_PFS_Nr, 0)
        Me.Controls.SetChildIndex(Me.TextBox_PFS_Nr, 0)
        Me.Controls.SetChildIndex(Me.Label_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IDP_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_C_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_C_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_IFI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_IFI_ID, 0)
        Me.Controls.SetChildIndex(Me.Label_CDPI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_CDPI_ID, 0)
        Me.Controls.SetChildIndex(Me.ComboBox_PF_ID, 0)
        Me.Controls.SetChildIndex(Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe, 0)
        Me.Controls.SetChildIndex(Me.TextBox_PFI_Nr, 0)
        Me.Controls.SetChildIndex(Me.Button_Aggiungi_Tutti_Pannelli_Fotovoltaici, 0)
        Me.Controls.SetChildIndex(Me.Button_Elimina_Tutti_Pannelli_Fotovoltaici, 0)
        Me.Controls.SetChildIndex(Me.TextBox_Nr_Stringhe_Da_Aggiungere, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_IDP_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_IDP_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_PFS_Nr As System.Windows.Forms.TextBox
    Friend WithEvents Label_PFS_Nr As System.Windows.Forms.Label
    Friend WithEvents ComboBox_C_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_C_ID As System.Windows.Forms.Label
    Friend WithEvents Label_IFI_ID As System.Windows.Forms.Label
    Friend WithEvents ComboBox_IFI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_CDPI_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label_CDPI_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_PFI_Nr As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox_PF_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Button_Aggiungi_Tutti_Pannelli_Fotovoltaici As System.Windows.Forms.Button
    Friend WithEvents Button_Elimina_Tutti_Pannelli_Fotovoltaici As System.Windows.Forms.Button
    Friend WithEvents Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe As System.Windows.Forms.Button
    Friend WithEvents TextBox_Nr_Stringhe_Da_Aggiungere As System.Windows.Forms.TextBox

End Class
