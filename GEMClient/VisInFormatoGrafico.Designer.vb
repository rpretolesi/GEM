<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisInFormatoGrafico
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title
        Dim Title4 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VisInFormatoGrafico))
        Me.ComboBox_Valore_2 = New System.Windows.Forms.ComboBox
        Me.Label_Dati_2 = New System.Windows.Forms.Label
        Me.Label_Dati_1 = New System.Windows.Forms.Label
        Me.ComboBox_Valore_1 = New System.Windows.Forms.ComboBox
        Me.ComboBox_Tipo = New System.Windows.Forms.ComboBox
        Me.Label_Tipo = New System.Windows.Forms.Label
        Me.Chart_1 = New System.Windows.Forms.DataVisualization.Charting.Chart
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
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        CType(Me.Chart_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox_Valore_2
        '
        Me.ComboBox_Valore_2.FormattingEnabled = True
        Me.ComboBox_Valore_2.Location = New System.Drawing.Point(370, 533)
        Me.ComboBox_Valore_2.Name = "ComboBox_Valore_2"
        Me.ComboBox_Valore_2.Size = New System.Drawing.Size(230, 21)
        Me.ComboBox_Valore_2.TabIndex = 28
        '
        'Label_Dati_2
        '
        Me.Label_Dati_2.AutoSize = True
        Me.Label_Dati_2.Location = New System.Drawing.Point(314, 536)
        Me.Label_Dati_2.Name = "Label_Dati_2"
        Me.Label_Dati_2.Size = New System.Drawing.Size(50, 13)
        Me.Label_Dati_2.TabIndex = 26
        Me.Label_Dati_2.Text = "2° Valore"
        '
        'Label_Dati_1
        '
        Me.Label_Dati_1.AutoSize = True
        Me.Label_Dati_1.Location = New System.Drawing.Point(5, 536)
        Me.Label_Dati_1.Name = "Label_Dati_1"
        Me.Label_Dati_1.Size = New System.Drawing.Size(50, 13)
        Me.Label_Dati_1.TabIndex = 25
        Me.Label_Dati_1.Text = "1° Valore"
        '
        'ComboBox_Valore_1
        '
        Me.ComboBox_Valore_1.FormattingEnabled = True
        Me.ComboBox_Valore_1.Location = New System.Drawing.Point(61, 533)
        Me.ComboBox_Valore_1.Name = "ComboBox_Valore_1"
        Me.ComboBox_Valore_1.Size = New System.Drawing.Size(230, 21)
        Me.ComboBox_Valore_1.TabIndex = 24
        '
        'ComboBox_Tipo
        '
        Me.ComboBox_Tipo.FormattingEnabled = True
        Me.ComboBox_Tipo.Location = New System.Drawing.Point(659, 533)
        Me.ComboBox_Tipo.Name = "ComboBox_Tipo"
        Me.ComboBox_Tipo.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_Tipo.TabIndex = 35
        '
        'Label_Tipo
        '
        Me.Label_Tipo.AutoSize = True
        Me.Label_Tipo.Location = New System.Drawing.Point(625, 536)
        Me.Label_Tipo.Name = "Label_Tipo"
        Me.Label_Tipo.Size = New System.Drawing.Size(28, 13)
        Me.Label_Tipo.TabIndex = 36
        Me.Label_Tipo.Text = "Tipo"
        '
        'Chart_1
        '
        ChartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray
        ChartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX.TitleForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX2.LabelStyle.ForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX2.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Gray
        ChartArea1.AxisX2.TitleForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray
        ChartArea1.AxisY.TitleForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY2.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisY2.TitleForeColor = System.Drawing.Color.DarkGray
        ChartArea1.BackColor = System.Drawing.Color.Transparent
        ChartArea1.Name = "ChartArea1"
        Me.Chart_1.ChartAreas.Add(ChartArea1)
        Legend1.BackColor = System.Drawing.Color.Transparent
        Legend1.Name = "Legend1"
        Me.Chart_1.Legends.Add(Legend1)
        Me.Chart_1.Location = New System.Drawing.Point(0, 28)
        Me.Chart_1.Name = "Chart_1"
        Series1.ChartArea = "ChartArea1"
        Series1.CustomProperties = "PointWidth=0.1"
        Series1.Legend = "Legend1"
        Series1.MarkerSize = 10
        Series1.Name = "S_1"
        Series2.ChartArea = "ChartArea1"
        Series2.CustomProperties = "PointWidth=0.1"
        Series2.Legend = "Legend1"
        Series2.Name = "S_2"
        Me.Chart_1.Series.Add(Series1)
        Me.Chart_1.Series.Add(Series2)
        Me.Chart_1.Size = New System.Drawing.Size(792, 489)
        Me.Chart_1.TabIndex = 37
        Me.Chart_1.Text = "Chart1"
        Title1.Name = "T_1"
        Title1.Text = "T1"
        Title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Title2.Name = "T_2"
        Title2.Text = "T2"
        Title3.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title3.DockedToChartArea = "ChartArea1"
        Title3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title3.ForeColor = System.Drawing.Color.Lime
        Title3.Name = "T_3"
        Title3.Text = "T3"
        Title4.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title4.DockedToChartArea = "ChartArea1"
        Title4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title4.ForeColor = System.Drawing.Color.DarkGreen
        Title4.Name = "T_4"
        Title4.Text = "T4"
        Me.Chart_1.Titles.Add(Title1)
        Me.Chart_1.Titles.Add(Title2)
        Me.Chart_1.Titles.Add(Title3)
        Me.Chart_1.Titles.Add(Title4)
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
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator5, Me.ToolStripButton_Nuovo, Me.ToolStripSeparator1, Me.ToolStripButton_Elimina, Me.ToolStripSeparator2, Me.ToolStripButton_Modifica, Me.ToolStripSeparator4, Me.ToolStripButton_Salva, Me.ToolStripSeparator3, Me.ToolStripButton_Annulla, Me.ToolStripSeparator6, Me.ToolStripButton_Stampa, Me.ToolStripSeparator8, Me.ToolStripButtonSelezionaTutto, Me.ToolStripSeparator9})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip.TabIndex = 33
        '
        'VisInFormatoGrafico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.Chart_1)
        Me.Controls.Add(Me.Label_Tipo)
        Me.Controls.Add(Me.ComboBox_Tipo)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.ComboBox_Valore_2)
        Me.Controls.Add(Me.Label_Dati_2)
        Me.Controls.Add(Me.Label_Dati_1)
        Me.Controls.Add(Me.ComboBox_Valore_1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VisInFormatoGrafico"
        Me.Text = "VisInFormatoGrafico"
        CType(Me.Chart_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_Valore_2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Dati_2 As System.Windows.Forms.Label
    Friend WithEvents Label_Dati_1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Valore_1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_Tipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Tipo As System.Windows.Forms.Label
    Friend WithEvents Chart_1 As System.Windows.Forms.DataVisualization.Charting.Chart
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
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
End Class
