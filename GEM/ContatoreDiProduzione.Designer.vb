﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContatoreDiProduzione
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
        Me.TextBox_CDP_Modello = New System.Windows.Forms.TextBox
        Me.Label_CDP_Modello = New System.Windows.Forms.Label
        Me.TextBox_CDP_Marca = New System.Windows.Forms.TextBox
        Me.Label_CDP_Marca = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TextBox_CDP_Modello
        '
        Me.TextBox_CDP_Modello.Location = New System.Drawing.Point(250, 54)
        Me.TextBox_CDP_Modello.Name = "TextBox_CDP_Modello"
        Me.TextBox_CDP_Modello.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_CDP_Modello.TabIndex = 41
        '
        'Label_CDP_Modello
        '
        Me.Label_CDP_Modello.AutoSize = True
        Me.Label_CDP_Modello.Location = New System.Drawing.Point(12, 57)
        Me.Label_CDP_Modello.Name = "Label_CDP_Modello"
        Me.Label_CDP_Modello.Size = New System.Drawing.Size(44, 13)
        Me.Label_CDP_Modello.TabIndex = 40
        Me.Label_CDP_Modello.Text = "Modello"
        '
        'TextBox_CDP_Marca
        '
        Me.TextBox_CDP_Marca.Location = New System.Drawing.Point(250, 28)
        Me.TextBox_CDP_Marca.Name = "TextBox_CDP_Marca"
        Me.TextBox_CDP_Marca.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_CDP_Marca.TabIndex = 38
        '
        'Label_CDP_Marca
        '
        Me.Label_CDP_Marca.AutoSize = True
        Me.Label_CDP_Marca.Location = New System.Drawing.Point(12, 31)
        Me.Label_CDP_Marca.Name = "Label_CDP_Marca"
        Me.Label_CDP_Marca.Size = New System.Drawing.Size(37, 13)
        Me.Label_CDP_Marca.TabIndex = 39
        Me.Label_CDP_Marca.Text = "Marca"
        '
        'ContatoreDiProduzione
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.TextBox_CDP_Modello)
        Me.Controls.Add(Me.Label_CDP_Modello)
        Me.Controls.Add(Me.TextBox_CDP_Marca)
        Me.Controls.Add(Me.Label_CDP_Marca)
        Me.Name = "ContatoreDiProduzione"
        Me.Controls.SetChildIndex(Me.Label_CDP_Marca, 0)
        Me.Controls.SetChildIndex(Me.TextBox_CDP_Marca, 0)
        Me.Controls.SetChildIndex(Me.Label_CDP_Modello, 0)
        Me.Controls.SetChildIndex(Me.TextBox_CDP_Modello, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_CDP_Modello As System.Windows.Forms.TextBox
    Friend WithEvents Label_CDP_Modello As System.Windows.Forms.Label
    Friend WithEvents TextBox_CDP_Marca As System.Windows.Forms.TextBox
    Friend WithEvents Label_CDP_Marca As System.Windows.Forms.Label

End Class
