<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatiGeneraliImpianto
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
        Me.TextBox_DGI_Valore = New System.Windows.Forms.TextBox
        Me.Label_DGI_Valore = New System.Windows.Forms.Label
        Me.CheckBox_Server = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'TextBox_DGI_Valore
        '
        Me.TextBox_DGI_Valore.Location = New System.Drawing.Point(245, 28)
        Me.TextBox_DGI_Valore.Name = "TextBox_DGI_Valore"
        Me.TextBox_DGI_Valore.Size = New System.Drawing.Size(400, 20)
        Me.TextBox_DGI_Valore.TabIndex = 36
        '
        'Label_DGI_Valore
        '
        Me.Label_DGI_Valore.AutoSize = True
        Me.Label_DGI_Valore.Location = New System.Drawing.Point(12, 31)
        Me.Label_DGI_Valore.Name = "Label_DGI_Valore"
        Me.Label_DGI_Valore.Size = New System.Drawing.Size(37, 13)
        Me.Label_DGI_Valore.TabIndex = 37
        Me.Label_DGI_Valore.Text = "Valore"
        '
        'CheckBox_Server
        '
        Me.CheckBox_Server.AutoSize = True
        Me.CheckBox_Server.Location = New System.Drawing.Point(12, 264)
        Me.CheckBox_Server.Name = "CheckBox_Server"
        Me.CheckBox_Server.Size = New System.Drawing.Size(57, 17)
        Me.CheckBox_Server.TabIndex = 38
        Me.CheckBox_Server.Text = "Server"
        Me.CheckBox_Server.UseVisualStyleBackColor = True
        '
        'DatiGeneraliImpianto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.CheckBox_Server)
        Me.Controls.Add(Me.TextBox_DGI_Valore)
        Me.Controls.Add(Me.Label_DGI_Valore)
        Me.Name = "DatiGeneraliImpianto"
        Me.Controls.SetChildIndex(Me.Label_DGI_Valore, 0)
        Me.Controls.SetChildIndex(Me.TextBox_DGI_Valore, 0)
        Me.Controls.SetChildIndex(Me.CheckBox_Server, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_DGI_Valore As System.Windows.Forms.TextBox
    Friend WithEvents Label_DGI_Valore As System.Windows.Forms.Label
    Friend WithEvents CheckBox_Server As System.Windows.Forms.CheckBox

End Class
