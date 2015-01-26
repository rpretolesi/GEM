<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.TextBox_Livello = New System.Windows.Forms.TextBox
        Me.Button_Login = New System.Windows.Forms.Button
        Me.TextBox_Password = New System.Windows.Forms.TextBox
        Me.TextBox_UserName = New System.Windows.Forms.TextBox
        Me.Label_Livello_Richiesto = New System.Windows.Forms.Label
        Me.Label_Password = New System.Windows.Forms.Label
        Me.Label_Utente = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Button_Cancel
        '
        Me.Button_Cancel.AccessibleDescription = Nothing
        Me.Button_Cancel.AccessibleName = Nothing
        resources.ApplyResources(Me.Button_Cancel, "Button_Cancel")
        Me.Button_Cancel.BackgroundImage = Nothing
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Font = Nothing
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'TextBox_Livello
        '
        Me.TextBox_Livello.AccessibleDescription = Nothing
        Me.TextBox_Livello.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBox_Livello, "TextBox_Livello")
        Me.TextBox_Livello.BackgroundImage = Nothing
        Me.TextBox_Livello.Font = Nothing
        Me.TextBox_Livello.Name = "TextBox_Livello"
        Me.TextBox_Livello.ReadOnly = True
        Me.TextBox_Livello.TabStop = False
        '
        'Button_Login
        '
        Me.Button_Login.AccessibleDescription = Nothing
        Me.Button_Login.AccessibleName = Nothing
        resources.ApplyResources(Me.Button_Login, "Button_Login")
        Me.Button_Login.BackgroundImage = Nothing
        Me.Button_Login.Font = Nothing
        Me.Button_Login.Name = "Button_Login"
        Me.Button_Login.UseVisualStyleBackColor = True
        '
        'TextBox_Password
        '
        Me.TextBox_Password.AccessibleDescription = Nothing
        Me.TextBox_Password.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBox_Password, "TextBox_Password")
        Me.TextBox_Password.BackgroundImage = Nothing
        Me.TextBox_Password.Font = Nothing
        Me.TextBox_Password.Name = "TextBox_Password"
        '
        'TextBox_UserName
        '
        Me.TextBox_UserName.AccessibleDescription = Nothing
        Me.TextBox_UserName.AccessibleName = Nothing
        resources.ApplyResources(Me.TextBox_UserName, "TextBox_UserName")
        Me.TextBox_UserName.BackgroundImage = Nothing
        Me.TextBox_UserName.Font = Nothing
        Me.TextBox_UserName.Name = "TextBox_UserName"
        '
        'Label_Livello_Richiesto
        '
        Me.Label_Livello_Richiesto.AccessibleDescription = Nothing
        Me.Label_Livello_Richiesto.AccessibleName = Nothing
        resources.ApplyResources(Me.Label_Livello_Richiesto, "Label_Livello_Richiesto")
        Me.Label_Livello_Richiesto.Font = Nothing
        Me.Label_Livello_Richiesto.Name = "Label_Livello_Richiesto"
        '
        'Label_Password
        '
        Me.Label_Password.AccessibleDescription = Nothing
        Me.Label_Password.AccessibleName = Nothing
        resources.ApplyResources(Me.Label_Password, "Label_Password")
        Me.Label_Password.Font = Nothing
        Me.Label_Password.Name = "Label_Password"
        '
        'Label_Utente
        '
        Me.Label_Utente.AccessibleDescription = Nothing
        Me.Label_Utente.AccessibleName = Nothing
        resources.ApplyResources(Me.Label_Utente, "Label_Utente")
        Me.Label_Utente.Font = Nothing
        Me.Label_Utente.Name = "Label_Utente"
        '
        'Login
        '
        Me.AcceptButton = Me.Button_Login
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.Button_Cancel
        Me.Controls.Add(Me.Label_Livello_Richiesto)
        Me.Controls.Add(Me.Label_Password)
        Me.Controls.Add(Me.Label_Utente)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.TextBox_Livello)
        Me.Controls.Add(Me.Button_Login)
        Me.Controls.Add(Me.TextBox_Password)
        Me.Controls.Add(Me.TextBox_UserName)
        Me.Font = Nothing
        Me.Icon = Nothing
        Me.Name = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents TextBox_Livello As System.Windows.Forms.TextBox
    Friend WithEvents Button_Login As System.Windows.Forms.Button
    Friend WithEvents TextBox_Password As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_UserName As System.Windows.Forms.TextBox
    Friend WithEvents Label_Livello_Richiesto As System.Windows.Forms.Label
    Friend WithEvents Label_Password As System.Windows.Forms.Label
    Friend WithEvents Label_Utente As System.Windows.Forms.Label
End Class
