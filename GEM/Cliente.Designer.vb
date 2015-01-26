<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cliente
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
        Me.TextBox_C_Nome = New System.Windows.Forms.TextBox
        Me.Label_C_Nome = New System.Windows.Forms.Label
        Me.Label_C_Cognome = New System.Windows.Forms.Label
        Me.TextBox_C_Cognome = New System.Windows.Forms.TextBox
        Me.TextBox_C_Societa = New System.Windows.Forms.TextBox
        Me.Label_C_Societa = New System.Windows.Forms.Label
        Me.TextBox_C_Codice = New System.Windows.Forms.TextBox
        Me.Label_C_Codice = New System.Windows.Forms.Label
        Me.Label_Opzioni_GEMClient = New System.Windows.Forms.Label
        Me.CheckBox_Opzioni_GEMClient_Vis_All = New System.Windows.Forms.CheckBox
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'TextBox_C_Nome
        '
        Me.TextBox_C_Nome.Location = New System.Drawing.Point(250, 28)
        Me.TextBox_C_Nome.Name = "TextBox_C_Nome"
        Me.TextBox_C_Nome.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_C_Nome.TabIndex = 30
        '
        'Label_C_Nome
        '
        Me.Label_C_Nome.AutoSize = True
        Me.Label_C_Nome.Location = New System.Drawing.Point(12, 31)
        Me.Label_C_Nome.Name = "Label_C_Nome"
        Me.Label_C_Nome.Size = New System.Drawing.Size(35, 13)
        Me.Label_C_Nome.TabIndex = 31
        Me.Label_C_Nome.Text = "Nome"
        '
        'Label_C_Cognome
        '
        Me.Label_C_Cognome.AutoSize = True
        Me.Label_C_Cognome.Location = New System.Drawing.Point(12, 57)
        Me.Label_C_Cognome.Name = "Label_C_Cognome"
        Me.Label_C_Cognome.Size = New System.Drawing.Size(52, 13)
        Me.Label_C_Cognome.TabIndex = 32
        Me.Label_C_Cognome.Text = "Cognome"
        '
        'TextBox_C_Cognome
        '
        Me.TextBox_C_Cognome.Location = New System.Drawing.Point(250, 54)
        Me.TextBox_C_Cognome.Name = "TextBox_C_Cognome"
        Me.TextBox_C_Cognome.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_C_Cognome.TabIndex = 33
        '
        'TextBox_C_Societa
        '
        Me.TextBox_C_Societa.Location = New System.Drawing.Point(250, 80)
        Me.TextBox_C_Societa.Name = "TextBox_C_Societa"
        Me.TextBox_C_Societa.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_C_Societa.TabIndex = 35
        '
        'Label_C_Societa
        '
        Me.Label_C_Societa.AutoSize = True
        Me.Label_C_Societa.Location = New System.Drawing.Point(12, 83)
        Me.Label_C_Societa.Name = "Label_C_Societa"
        Me.Label_C_Societa.Size = New System.Drawing.Size(43, 13)
        Me.Label_C_Societa.TabIndex = 34
        Me.Label_C_Societa.Text = "Societa"
        '
        'TextBox_C_Codice
        '
        Me.TextBox_C_Codice.Location = New System.Drawing.Point(250, 106)
        Me.TextBox_C_Codice.Name = "TextBox_C_Codice"
        Me.TextBox_C_Codice.ReadOnly = True
        Me.TextBox_C_Codice.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_C_Codice.TabIndex = 37
        '
        'Label_C_Codice
        '
        Me.Label_C_Codice.AutoSize = True
        Me.Label_C_Codice.Location = New System.Drawing.Point(12, 109)
        Me.Label_C_Codice.Name = "Label_C_Codice"
        Me.Label_C_Codice.Size = New System.Drawing.Size(40, 13)
        Me.Label_C_Codice.TabIndex = 36
        Me.Label_C_Codice.Text = "Codice"
        '
        'Label_Opzioni_GEMClient
        '
        Me.Label_Opzioni_GEMClient.AutoSize = True
        Me.Label_Opzioni_GEMClient.Location = New System.Drawing.Point(341, 139)
        Me.Label_Opzioni_GEMClient.Name = "Label_Opzioni_GEMClient"
        Me.Label_Opzioni_GEMClient.Size = New System.Drawing.Size(98, 13)
        Me.Label_Opzioni_GEMClient.TabIndex = 38
        Me.Label_Opzioni_GEMClient.Text = "Opzioni GEM Client"
        '
        'CheckBox_Opzioni_GEMClient_Vis_All
        '
        Me.CheckBox_Opzioni_GEMClient_Vis_All.AutoSize = True
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Location = New System.Drawing.Point(15, 155)
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Name = "CheckBox_Opzioni_GEMClient_Vis_All"
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Size = New System.Drawing.Size(105, 17)
        Me.CheckBox_Opzioni_GEMClient_Vis_All.TabIndex = 39
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Text = "Visualizza Allarmi"
        Me.CheckBox_Opzioni_GEMClient_Vis_All.UseVisualStyleBackColor = True
        '
        'CheckBox_Opzioni_GEMClient_Vis_DatiStat
        '
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.AutoSize = True
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Location = New System.Drawing.Point(15, 178)
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Name = "CheckBox_Opzioni_GEMClient_Vis_DatiStat"
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.TabIndex = 40
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Text = "Visualizza Dati Statistici"
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.UseVisualStyleBackColor = True
        '
        'Cliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat)
        Me.Controls.Add(Me.CheckBox_Opzioni_GEMClient_Vis_All)
        Me.Controls.Add(Me.Label_Opzioni_GEMClient)
        Me.Controls.Add(Me.TextBox_C_Codice)
        Me.Controls.Add(Me.Label_C_Codice)
        Me.Controls.Add(Me.TextBox_C_Societa)
        Me.Controls.Add(Me.Label_C_Societa)
        Me.Controls.Add(Me.TextBox_C_Cognome)
        Me.Controls.Add(Me.Label_C_Cognome)
        Me.Controls.Add(Me.TextBox_C_Nome)
        Me.Controls.Add(Me.Label_C_Nome)
        Me.Name = "Cliente"
        Me.Controls.SetChildIndex(Me.Label_C_Nome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_C_Nome, 0)
        Me.Controls.SetChildIndex(Me.Label_C_Cognome, 0)
        Me.Controls.SetChildIndex(Me.TextBox_C_Cognome, 0)
        Me.Controls.SetChildIndex(Me.Label_C_Societa, 0)
        Me.Controls.SetChildIndex(Me.TextBox_C_Societa, 0)
        Me.Controls.SetChildIndex(Me.Label_C_Codice, 0)
        Me.Controls.SetChildIndex(Me.TextBox_C_Codice, 0)
        Me.Controls.SetChildIndex(Me.Label_Opzioni_GEMClient, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_Opzioni_GEMClient_Vis_All, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_C_Nome As System.Windows.Forms.TextBox
    Friend WithEvents Label_C_Nome As System.Windows.Forms.Label
    Friend WithEvents Label_C_Cognome As System.Windows.Forms.Label
    Friend WithEvents TextBox_C_Cognome As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_C_Societa As System.Windows.Forms.TextBox
    Friend WithEvents Label_C_Societa As System.Windows.Forms.Label
    Friend WithEvents TextBox_C_Codice As System.Windows.Forms.TextBox
    Friend WithEvents Label_C_Codice As System.Windows.Forms.Label
    Friend WithEvents Label_Opzioni_GEMClient As System.Windows.Forms.Label
    Friend WithEvents CheckBox_Opzioni_GEMClient_Vis_All As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Opzioni_GEMClient_Vis_DatiStat As System.Windows.Forms.CheckBox

End Class
