<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BaseForm_1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BaseForm_1))
        Me.DataGridView = New System.Windows.Forms.DataGridView
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Nuovo = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Elimina = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Modifica = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Salva = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Annulla = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Stampa = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButtonSelezionaTutto = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel_Nr_Record = New System.Windows.Forms.ToolStripStatusLabel
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToOrderColumns = True
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Location = New System.Drawing.Point(12, 287)
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.Size = New System.Drawing.Size(768, 254)
        Me.DataGridView.TabIndex = 0
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator5, Me.ToolStripButton_Nuovo, Me.ToolStripSeparator1, Me.ToolStripButton_Elimina, Me.ToolStripSeparator2, Me.ToolStripButton_Modifica, Me.ToolStripSeparator4, Me.ToolStripButton_Salva, Me.ToolStripSeparator3, Me.ToolStripButton_Annulla, Me.ToolStripSeparator6, Me.ToolStripButton_Stampa, Me.ToolStripSeparator8, Me.ToolStripButtonSelezionaTutto, Me.ToolStripSeparator9})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip.TabIndex = 28
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Nuovo
        '
        Me.ToolStripButton_Nuovo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Nuovo.Image = CType(resources.GetObject("ToolStripButton_Nuovo.Image"), System.Drawing.Image)
        Me.ToolStripButton_Nuovo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Nuovo.Name = "ToolStripButton_Nuovo"
        Me.ToolStripButton_Nuovo.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Nuovo.ToolTipText = "Aggiungi"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Elimina
        '
        Me.ToolStripButton_Elimina.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Elimina.Image = CType(resources.GetObject("ToolStripButton_Elimina.Image"), System.Drawing.Image)
        Me.ToolStripButton_Elimina.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Elimina.Name = "ToolStripButton_Elimina"
        Me.ToolStripButton_Elimina.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Elimina.ToolTipText = "Elimina"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Modifica
        '
        Me.ToolStripButton_Modifica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Modifica.Image = CType(resources.GetObject("ToolStripButton_Modifica.Image"), System.Drawing.Image)
        Me.ToolStripButton_Modifica.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Modifica.Name = "ToolStripButton_Modifica"
        Me.ToolStripButton_Modifica.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Modifica.Text = "ToolStripButton1"
        Me.ToolStripButton_Modifica.ToolTipText = "Modifica"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Salva
        '
        Me.ToolStripButton_Salva.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Salva.Image = CType(resources.GetObject("ToolStripButton_Salva.Image"), System.Drawing.Image)
        Me.ToolStripButton_Salva.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Salva.Name = "ToolStripButton_Salva"
        Me.ToolStripButton_Salva.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Salva.ToolTipText = "Salva"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Annulla
        '
        Me.ToolStripButton_Annulla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Annulla.Image = CType(resources.GetObject("ToolStripButton_Annulla.Image"), System.Drawing.Image)
        Me.ToolStripButton_Annulla.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Annulla.Name = "ToolStripButton_Annulla"
        Me.ToolStripButton_Annulla.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Annulla.ToolTipText = "Annulla"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton_Stampa
        '
        Me.ToolStripButton_Stampa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Stampa.Image = CType(resources.GetObject("ToolStripButton_Stampa.Image"), System.Drawing.Image)
        Me.ToolStripButton_Stampa.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Stampa.Name = "ToolStripButton_Stampa"
        Me.ToolStripButton_Stampa.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Stampa.ToolTipText = "Stampa"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButtonSelezionaTutto
        '
        Me.ToolStripButtonSelezionaTutto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonSelezionaTutto.Image = CType(resources.GetObject("ToolStripButtonSelezionaTutto.Image"), System.Drawing.Image)
        Me.ToolStripButtonSelezionaTutto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSelezionaTutto.Name = "ToolStripButtonSelezionaTutto"
        Me.ToolStripButtonSelezionaTutto.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonSelezionaTutto.Text = "ToolStripButtonSelezionaTutto"
        Me.ToolStripButtonSelezionaTutto.ToolTipText = "Seleziona Tutto"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Nr_Record})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 544)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip.TabIndex = 29
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_Nr_Record
        '
        Me.ToolStripStatusLabel_Nr_Record.Name = "ToolStripStatusLabel_Nr_Record"
        Me.ToolStripStatusLabel_Nr_Record.Size = New System.Drawing.Size(150, 17)
        Me.ToolStripStatusLabel_Nr_Record.Text = "ToolStripStatusLabelNrRecord"
        '
        'BaseForm_1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.DataGridView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BaseForm_1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BaseForm_1"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Nuovo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Elimina As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Modifica As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Salva As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Annulla As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Stampa As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButtonSelezionaTutto As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel_Nr_Record As System.Windows.Forms.ToolStripStatusLabel
End Class
