<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClienteCodiceStringaDiConnessione
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClienteCodiceStringaDiConnessione))
        Me.TextBox_C_Codice = New System.Windows.Forms.TextBox
        Me.Label_C_Codice = New System.Windows.Forms.Label
        Me.Ok = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.TextBox_Conn_String = New System.Windows.Forms.TextBox
        Me.Label_Conn_String = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TextBox_C_Codice
        '
        Me.TextBox_C_Codice.Location = New System.Drawing.Point(12, 105)
        Me.TextBox_C_Codice.Name = "TextBox_C_Codice"
        Me.TextBox_C_Codice.Size = New System.Drawing.Size(368, 20)
        Me.TextBox_C_Codice.TabIndex = 0
        '
        'Label_C_Codice
        '
        Me.Label_C_Codice.AutoSize = True
        Me.Label_C_Codice.Location = New System.Drawing.Point(12, 89)
        Me.Label_C_Codice.Name = "Label_C_Codice"
        Me.Label_C_Codice.Size = New System.Drawing.Size(75, 13)
        Me.Label_C_Codice.TabIndex = 1
        Me.Label_C_Codice.Text = "Codice Cliente"
        '
        'Ok
        '
        Me.Ok.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Ok.Location = New System.Drawing.Point(12, 131)
        Me.Ok.Name = "Ok"
        Me.Ok.Size = New System.Drawing.Size(75, 23)
        Me.Ok.TabIndex = 2
        Me.Ok.Text = "Ok"
        Me.Ok.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(305, 131)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 3
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'TextBox_Conn_String
        '
        Me.TextBox_Conn_String.Location = New System.Drawing.Point(12, 25)
        Me.TextBox_Conn_String.Name = "TextBox_Conn_String"
        Me.TextBox_Conn_String.Size = New System.Drawing.Size(368, 20)
        Me.TextBox_Conn_String.TabIndex = 4
        '
        'Label_Conn_String
        '
        Me.Label_Conn_String.AutoSize = True
        Me.Label_Conn_String.Location = New System.Drawing.Point(12, 9)
        Me.Label_Conn_String.Name = "Label_Conn_String"
        Me.Label_Conn_String.Size = New System.Drawing.Size(114, 13)
        Me.Label_Conn_String.TabIndex = 5
        Me.Label_Conn_String.Text = "Stringa di connessione"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 51)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(368, 20)
        Me.TextBox1.TabIndex = 6
        Me.TextBox1.Text = "Data Source=xxx.xxx.xxx.xxx,yyyyy\SQLEXPRESS;Initial Catalog=GEM;Persist Security" & _
            " Info=True;User ID=fase;Password=fasefase"
        '
        'ClienteCodiceStringaDiConnessione
        '
        Me.AcceptButton = Me.Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(392, 166)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label_Conn_String)
        Me.Controls.Add(Me.TextBox_Conn_String)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Ok)
        Me.Controls.Add(Me.Label_C_Codice)
        Me.Controls.Add(Me.TextBox_C_Codice)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ClienteCodiceStringaDiConnessione"
        Me.Text = "ClienteCodice"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_C_Codice As System.Windows.Forms.TextBox
    Friend WithEvents Label_C_Codice As System.Windows.Forms.Label
    Friend WithEvents Ok As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents TextBox_Conn_String As System.Windows.Forms.TextBox
    Friend WithEvents Label_Conn_String As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
