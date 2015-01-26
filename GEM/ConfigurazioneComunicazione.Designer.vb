<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigurazioneComunicazione
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
        Me.Button_Ok = New System.Windows.Forms.Button
        Me.Label_TCPIP_Port = New System.Windows.Forms.Label
        Me.TextBox_TCPIP_Port_Disp = New System.Windows.Forms.TextBox
        Me.TextBox_TCPIP_Port_InUse = New System.Windows.Forms.TextBox
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Label_TCPIP_Addr_InUse = New System.Windows.Forms.Label
        Me.Label_TCPIP_Addr_Disp = New System.Windows.Forms.Label
        Me.Button_From_InUse_To_Disp = New System.Windows.Forms.Button
        Me.Button_From_Disp_To_InUse = New System.Windows.Forms.Button
        Me.ListBox_TCPIP_Addr_InUse = New System.Windows.Forms.ListBox
        Me.ListBox_TCPIP_Addr_Disp = New System.Windows.Forms.ListBox
        Me.CheckBox_Abilita_Debug_DataloggerDevice = New System.Windows.Forms.CheckBox
        Me.ComboBox_Com_Port_Disp = New System.Windows.Forms.ComboBox
        Me.TextBox_ReadTimeout = New System.Windows.Forms.TextBox
        Me.Label_ReadTimeout = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Button_Ok
        '
        Me.Button_Ok.Location = New System.Drawing.Point(12, 231)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 1
        Me.Button_Ok.Text = "Ok"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Label_TCPIP_Port
        '
        Me.Label_TCPIP_Port.AutoSize = True
        Me.Label_TCPIP_Port.Location = New System.Drawing.Point(186, 125)
        Me.Label_TCPIP_Port.Name = "Label_TCPIP_Port"
        Me.Label_TCPIP_Port.Size = New System.Drawing.Size(71, 13)
        Me.Label_TCPIP_Port.TabIndex = 3
        Me.Label_TCPIP_Port.Text = "Porta TCP/IP"
        '
        'TextBox_TCPIP_Port_Disp
        '
        Me.TextBox_TCPIP_Port_Disp.Location = New System.Drawing.Point(302, 122)
        Me.TextBox_TCPIP_Port_Disp.Name = "TextBox_TCPIP_Port_Disp"
        Me.TextBox_TCPIP_Port_Disp.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_TCPIP_Port_Disp.TabIndex = 4
        '
        'TextBox_TCPIP_Port_InUse
        '
        Me.TextBox_TCPIP_Port_InUse.Location = New System.Drawing.Point(19, 122)
        Me.TextBox_TCPIP_Port_InUse.Name = "TextBox_TCPIP_Port_InUse"
        Me.TextBox_TCPIP_Port_InUse.ReadOnly = True
        Me.TextBox_TCPIP_Port_InUse.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_TCPIP_Port_InUse.TabIndex = 6
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(405, 231)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 7
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label_TCPIP_Addr_InUse
        '
        Me.Label_TCPIP_Addr_InUse.AutoSize = True
        Me.Label_TCPIP_Addr_InUse.Location = New System.Drawing.Point(288, 5)
        Me.Label_TCPIP_Addr_InUse.Name = "Label_TCPIP_Addr_InUse"
        Me.Label_TCPIP_Addr_InUse.Size = New System.Drawing.Size(180, 13)
        Me.Label_TCPIP_Addr_InUse.TabIndex = 13
        Me.Label_TCPIP_Addr_InUse.Text = "Schede Ethernet Da Utilizzare su PC"
        '
        'Label_TCPIP_Addr_Disp
        '
        Me.Label_TCPIP_Addr_Disp.AutoSize = True
        Me.Label_TCPIP_Addr_Disp.Location = New System.Drawing.Point(6, 5)
        Me.Label_TCPIP_Addr_Disp.Name = "Label_TCPIP_Addr_Disp"
        Me.Label_TCPIP_Addr_Disp.Size = New System.Drawing.Size(168, 13)
        Me.Label_TCPIP_Addr_Disp.TabIndex = 12
        Me.Label_TCPIP_Addr_Disp.Text = "Schede Ethernet Disponibili su PC"
        '
        'Button_From_InUse_To_Disp
        '
        Me.Button_From_InUse_To_Disp.Location = New System.Drawing.Point(182, 93)
        Me.Button_From_InUse_To_Disp.Name = "Button_From_InUse_To_Disp"
        Me.Button_From_InUse_To_Disp.Size = New System.Drawing.Size(75, 23)
        Me.Button_From_InUse_To_Disp.TabIndex = 11
        Me.Button_From_InUse_To_Disp.Text = "<-"
        Me.Button_From_InUse_To_Disp.UseVisualStyleBackColor = True
        '
        'Button_From_Disp_To_InUse
        '
        Me.Button_From_Disp_To_InUse.Location = New System.Drawing.Point(182, 21)
        Me.Button_From_Disp_To_InUse.Name = "Button_From_Disp_To_InUse"
        Me.Button_From_Disp_To_InUse.Size = New System.Drawing.Size(75, 23)
        Me.Button_From_Disp_To_InUse.TabIndex = 10
        Me.Button_From_Disp_To_InUse.Text = "->"
        Me.Button_From_Disp_To_InUse.UseVisualStyleBackColor = True
        '
        'ListBox_TCPIP_Addr_InUse
        '
        Me.ListBox_TCPIP_Addr_InUse.FormattingEnabled = True
        Me.ListBox_TCPIP_Addr_InUse.Location = New System.Drawing.Point(302, 21)
        Me.ListBox_TCPIP_Addr_InUse.Name = "ListBox_TCPIP_Addr_InUse"
        Me.ListBox_TCPIP_Addr_InUse.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_TCPIP_Addr_InUse.Size = New System.Drawing.Size(120, 95)
        Me.ListBox_TCPIP_Addr_InUse.TabIndex = 9
        '
        'ListBox_TCPIP_Addr_Disp
        '
        Me.ListBox_TCPIP_Addr_Disp.FormattingEnabled = True
        Me.ListBox_TCPIP_Addr_Disp.Location = New System.Drawing.Point(19, 21)
        Me.ListBox_TCPIP_Addr_Disp.Name = "ListBox_TCPIP_Addr_Disp"
        Me.ListBox_TCPIP_Addr_Disp.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_TCPIP_Addr_Disp.Size = New System.Drawing.Size(120, 95)
        Me.ListBox_TCPIP_Addr_Disp.TabIndex = 8
        '
        'CheckBox_Abilita_Debug_DataloggerDevice
        '
        Me.CheckBox_Abilita_Debug_DataloggerDevice.AutoSize = True
        Me.CheckBox_Abilita_Debug_DataloggerDevice.Location = New System.Drawing.Point(19, 171)
        Me.CheckBox_Abilita_Debug_DataloggerDevice.Name = "CheckBox_Abilita_Debug_DataloggerDevice"
        Me.CheckBox_Abilita_Debug_DataloggerDevice.Size = New System.Drawing.Size(190, 17)
        Me.CheckBox_Abilita_Debug_DataloggerDevice.TabIndex = 14
        Me.CheckBox_Abilita_Debug_DataloggerDevice.Text = "Abilita Debug Datalogger > Device"
        Me.CheckBox_Abilita_Debug_DataloggerDevice.UseVisualStyleBackColor = True
        '
        'ComboBox_Com_Port_Disp
        '
        Me.ComboBox_Com_Port_Disp.FormattingEnabled = True
        Me.ComboBox_Com_Port_Disp.Location = New System.Drawing.Point(302, 168)
        Me.ComboBox_Com_Port_Disp.Name = "ComboBox_Com_Port_Disp"
        Me.ComboBox_Com_Port_Disp.Size = New System.Drawing.Size(120, 21)
        Me.ComboBox_Com_Port_Disp.TabIndex = 15
        '
        'TextBox_ReadTimeout
        '
        Me.TextBox_ReadTimeout.Location = New System.Drawing.Point(302, 195)
        Me.TextBox_ReadTimeout.Name = "TextBox_ReadTimeout"
        Me.TextBox_ReadTimeout.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_ReadTimeout.TabIndex = 16
        Me.TextBox_ReadTimeout.Text = "3"
        '
        'Label_ReadTimeout
        '
        Me.Label_ReadTimeout.AutoSize = True
        Me.Label_ReadTimeout.Location = New System.Drawing.Point(16, 198)
        Me.Label_ReadTimeout.Name = "Label_ReadTimeout"
        Me.Label_ReadTimeout.Size = New System.Drawing.Size(194, 13)
        Me.Label_ReadTimeout.TabIndex = 17
        Me.Label_ReadTimeout.Text = "Timeout Per Discriminazione Frame (ms)"
        '
        'ConfigurazioneComunicazione
        '
        Me.AcceptButton = Me.Button_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(492, 266)
        Me.Controls.Add(Me.Label_ReadTimeout)
        Me.Controls.Add(Me.TextBox_ReadTimeout)
        Me.Controls.Add(Me.ComboBox_Com_Port_Disp)
        Me.Controls.Add(Me.CheckBox_Abilita_Debug_DataloggerDevice)
        Me.Controls.Add(Me.Label_TCPIP_Addr_InUse)
        Me.Controls.Add(Me.Label_TCPIP_Addr_Disp)
        Me.Controls.Add(Me.Button_From_InUse_To_Disp)
        Me.Controls.Add(Me.Button_From_Disp_To_InUse)
        Me.Controls.Add(Me.ListBox_TCPIP_Addr_InUse)
        Me.Controls.Add(Me.ListBox_TCPIP_Addr_Disp)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.TextBox_TCPIP_Port_InUse)
        Me.Controls.Add(Me.TextBox_TCPIP_Port_Disp)
        Me.Controls.Add(Me.Label_TCPIP_Port)
        Me.Controls.Add(Me.Button_Ok)
        Me.Name = "ConfigurazioneComunicazione"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configurazione Comunicazione"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Ok As System.Windows.Forms.Button
    Friend WithEvents Label_TCPIP_Port As System.Windows.Forms.Label
    Friend WithEvents TextBox_TCPIP_Port_Disp As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_TCPIP_Port_InUse As System.Windows.Forms.TextBox
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label_TCPIP_Addr_InUse As System.Windows.Forms.Label
    Friend WithEvents Label_TCPIP_Addr_Disp As System.Windows.Forms.Label
    Friend WithEvents Button_From_InUse_To_Disp As System.Windows.Forms.Button
    Friend WithEvents Button_From_Disp_To_InUse As System.Windows.Forms.Button
    Friend WithEvents ListBox_TCPIP_Addr_InUse As System.Windows.Forms.ListBox
    Friend WithEvents ListBox_TCPIP_Addr_Disp As System.Windows.Forms.ListBox
    Friend WithEvents CheckBox_Abilita_Debug_DataloggerDevice As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_Com_Port_Disp As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox_ReadTimeout As System.Windows.Forms.TextBox
    Friend WithEvents Label_ReadTimeout As System.Windows.Forms.Label
End Class
