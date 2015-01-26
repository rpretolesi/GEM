<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisDatiStringTester
    Inherits GEMClient.BaseForm_1

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
        Me.components = New System.ComponentModel.Container
        Me.CheckBoxAbilitaAggiornamento = New System.Windows.Forms.CheckBox
        Me.Timer_1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button_VisGraf = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'CheckBoxAbilitaAggiornamento
        '
        Me.CheckBoxAbilitaAggiornamento.AutoSize = True
        Me.CheckBoxAbilitaAggiornamento.Location = New System.Drawing.Point(12, 264)
        Me.CheckBoxAbilitaAggiornamento.Name = "CheckBoxAbilitaAggiornamento"
        Me.CheckBoxAbilitaAggiornamento.Size = New System.Drawing.Size(184, 17)
        Me.CheckBoxAbilitaAggiornamento.TabIndex = 111
        Me.CheckBoxAbilitaAggiornamento.Text = "Abilita Aggiornamento Automatico"
        Me.CheckBoxAbilitaAggiornamento.UseVisualStyleBackColor = True
        '
        'Timer_1
        '
        Me.Timer_1.Interval = 600000
        '
        'Button_VisGraf
        '
        Me.Button_VisGraf.Location = New System.Drawing.Point(680, 260)
        Me.Button_VisGraf.Name = "Button_VisGraf"
        Me.Button_VisGraf.Size = New System.Drawing.Size(100, 23)
        Me.Button_VisGraf.TabIndex = 148
        Me.Button_VisGraf.Text = "Formato Grafico"
        Me.Button_VisGraf.UseVisualStyleBackColor = True
        '
        'VisDatiStringTester
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Button_VisGraf)
        Me.Controls.Add(Me.CheckBoxAbilitaAggiornamento)
        Me.Name = "VisDatiStringTester"
        Me.Controls.SetChildIndex(Me.CheckBoxAbilitaAggiornamento, 0)
        Me.Controls.SetChildIndex(Me.Button_VisGraf, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxAbilitaAggiornamento As System.Windows.Forms.CheckBox
    Friend WithEvents Timer_1 As System.Windows.Forms.Timer
    Friend WithEvents Button_VisGraf As System.Windows.Forms.Button

End Class
