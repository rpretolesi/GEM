Imports System.Data.SqlClient
Imports System.Runtime.InteropServices

Public Class VisInFormatoGrafico
    Const CF_BITMAP = 2

    Private m_pPosition As Point

    Private m_strChartTitleText As String = ""
    Private m_strChartEnergiaProdotta As String = ""
    Private m_strChartPotenzaMedia As String = ""

    Private m_iCID As Integer
    Private m_iIDPID As Integer
    Private m_iLIID As Integer
    Private m_dtSTART As Date
    Private m_dtSTOP As Date

    Property Position() As Point
        Get
            Return m_pPosition
        End Get

        Set(ByVal Position As Point)
            m_pPosition = Position
        End Set
    End Property

    Property ChartTitleText() As String
        Get
            Return m_strChartTitleText
        End Get

        Set(ByVal ChartTitleText As String)
            m_strChartTitleText = ChartTitleText
        End Set
    End Property

    Property ChartEnergiaProdotta() As String
        Get
            Return m_strChartEnergiaProdotta
        End Get

        Set(ByVal ChartEnergiaProdotta As String)
            m_strChartEnergiaProdotta = ChartEnergiaProdotta
        End Set
    End Property

    Property ChartPotenzaMedia() As String
        Get
            Return m_strChartPotenzaMedia
        End Get

        Set(ByVal ChartPotenzaMedia As String)
            m_strChartPotenzaMedia = ChartPotenzaMedia
        End Set
    End Property

    Property CID() As Integer
        Get
            Return m_iCID
        End Get

        Set(ByVal CID As Integer)
            m_iCID = CID
        End Set
    End Property

    Property IDPID() As Integer
        Get
            Return m_iIDPID
        End Get

        Set(ByVal IDPID As Integer)
            m_iIDPID = IDPID
        End Set
    End Property

    Property LIID() As Integer
        Get
            Return m_iLIID
        End Get

        Set(ByVal LIID As Integer)
            m_iLIID = LIID
        End Set
    End Property

    Property DT_START() As Date
        Get
            Return m_dtSTART
        End Get

        Set(ByVal DT_START As Date)
            m_dtSTART = DT_START
        End Set
    End Property

    Property DT_STOP() As Date
        Get
            Return m_dtSTOP
        End Get

        Set(ByVal DT_STOP As Date)
            m_dtSTOP = DT_STOP
        End Set
    End Property

    Public Sub UpdateGraph(ByVal Table As DataTable)
        If m_dt.TableName = Table.TableName Then
            m_dt = Table
        Else
            m_dt = Table
            PopolaCombo()
        End If
        PopolaGrafico()
    End Sub

    ' Variabile per i grafici
    Dim m_dt As New DataTable

    Property Table() As DataTable
        Get
            Return m_dt
        End Get

        Set(ByVal Table As DataTable)
            m_dt = Table
        End Set
    End Property

    Private Sub VisInFormatoGrafico_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Visualizzazione Grafica dei Dati"

        PopolaCombo()
        PopolaGrafico()

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False
        Me.ToolStripButtonSelezionaTutto.Enabled = False
    End Sub

    Private Sub PopolaCombo()
        ' Eseguo la query
        Dim iIndice_C As Integer

        Try
            ComboBox_Valore_1.Items.Clear()
            ComboBox_Valore_2.Items.Clear()
            ComboBox_Tipo.Items.Clear()

            ComboBox_Valore_1.Items.Add("<nessun valore>")
            ComboBox_Valore_2.Items.Add("<nessun valore>")

            If m_dt.Columns.Count > 0 Then
                iIndice_C = 1
                For Each dc As DataColumn In m_dt.Columns

                    If dc.ColumnName.ToString <> "DataOra" Then
                        ComboBox_Valore_1.Items.Add(dc.Caption.ToString)
                        ComboBox_Valore_2.Items.Add(dc.Caption.ToString)
                    End If

                    iIndice_C = iIndice_C + 1
                Next dc
            End If

            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Area)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Bar)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.BoxPlot)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Bubble)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Candlestick)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Column)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Doughnut)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.ErrorBar)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.FastLine)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.FastPoint)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Funnel)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Kagi)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Line)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Pie)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Point)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.PointAndFigure)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Polar)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Pyramid)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Radar)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Range)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.RangeBar)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.RangeColumn)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Renko)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Spline)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.SplineArea)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.SplineRange)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedArea)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedArea100)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedBar)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedBar100)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedColumn)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StackedColumn100)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.StepLine)
            ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.Stock)
            'ComboBox_Tipo.Items.Add(DataVisualization.Charting.SeriesChartType.ThreeLineBreak)


            ComboBox_Valore_1.SelectedIndex = 0
            ComboBox_Valore_2.SelectedIndex = 0
            ComboBox_Tipo.SelectedIndex = 11

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try

    End Sub

    Private Sub ComboBox_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_Valore_1.SelectedValueChanged, ComboBox_Valore_2.SelectedValueChanged, ComboBox_Tipo.SelectedValueChanged
        PopolaGrafico()
    End Sub

    Private Sub PopolaGrafico()
        ' Eseguo la query

        Try
            Chart_1.Titles(0).Text = m_strChartTitleText
            Chart_1.Titles(1).Text = "Dati Impianto. Dal: " + m_dtSTART.ToString + " - al: " + m_dtSTOP.ToString
            If m_strChartEnergiaProdotta.Count > 0 Then
                Chart_1.Titles(2).Text = "Energia Prodotta Kwh " + m_strChartEnergiaProdotta
            Else
                Chart_1.Titles(2).Text = ""
            End If
            If m_strChartPotenzaMedia.Count > 0 Then
                Chart_1.Titles(3).Text = "Potenza Media Riferita All' Ultimo Dato Visualizzato Kw " + m_strChartPotenzaMedia
            Else
                Chart_1.Titles(3).Text = ""
            End If


            If m_dt.Rows.Count > 0 Then
                If ComboBox_Valore_1.Items.Count > 0 And ComboBox_Valore_2.Items.Count > 0 Then

                    Chart_1.Series.Item(0).Points.Clear()
                    Chart_1.Series.Item(0).XValueType = DataVisualization.Charting.ChartValueType.DateTime
                    Chart_1.Series.Item(0).YValueType = DataVisualization.Charting.ChartValueType.Double
                    Chart_1.Series.Item(0).YAxisType = DataVisualization.Charting.AxisType.Primary

                    Chart_1.Series.Item(1).Points.Clear()
                    Chart_1.Series.Item(1).XValueType = DataVisualization.Charting.ChartValueType.DateTime
                    Chart_1.Series.Item(1).YValueType = DataVisualization.Charting.ChartValueType.Double
                    Chart_1.Series.Item(1).YAxisType = DataVisualization.Charting.AxisType.Secondary

                    If Not ComboBox_Tipo.SelectedItem Is Nothing Then
                        Chart_1.Series.Item(0).ChartType = CInt(ComboBox_Tipo.SelectedItem)
                        Chart_1.Series.Item(1).ChartType = CInt(ComboBox_Tipo.SelectedItem)
                    End If

                    For Each dr As DataRow In m_dt.Select("", "DataOra ASC")

                        If Not ComboBox_Valore_1.SelectedItem Is Nothing Then
                            If ComboBox_Valore_1.SelectedItem.ToString <> "<nessun valore>" And ComboBox_Valore_1.SelectedItem.ToString <> "----------------" Then
                                If Not dr.Item(m_dt.Columns(ComboBox_Valore_1.SelectedItem).ColumnName.ToString) Is DBNull.Value Then
                                    Chart_1.Series.Item(0).Points.AddXY(dr.Item("DataOra"), CDbl(dr.Item(m_dt.Columns(ComboBox_Valore_1.SelectedItem).ColumnName.ToString)))
                                End If
                            End If
                        End If

                        If Not ComboBox_Valore_2.SelectedItem Is Nothing Then
                            If ComboBox_Valore_2.SelectedItem.ToString <> "<nessun valore>" And ComboBox_Valore_2.SelectedItem.ToString <> "----------------" Then
                                If Not dr.Item(m_dt.Columns(ComboBox_Valore_2.SelectedItem).ColumnName.ToString) Is DBNull.Value Then
                                    Chart_1.Series.Item(1).Points.AddXY(dr.Item("DataOra"), CDbl(dr.Item(m_dt.Columns(ComboBox_Valore_2.SelectedItem).ColumnName.ToString)))
                                End If
                            End If
                        End If
                    Next dr

                    If Not ComboBox_Valore_1.SelectedItem Is Nothing Then
                        If ComboBox_Valore_1.SelectedItem.ToString <> "<nessun valore>" And ComboBox_Valore_1.SelectedItem.ToString <> "----------------" Then
                            Chart_1.Series.Item(0).LegendText = ComboBox_Valore_1.SelectedItem.ToString
                        Else
                            Chart_1.Series.Item(0).LegendText = ""
                        End If
                    End If
                    If Not ComboBox_Valore_2.SelectedItem Is Nothing Then
                        If ComboBox_Valore_2.SelectedItem.ToString <> "<nessun valore>" And ComboBox_Valore_2.SelectedItem.ToString <> "----------------" Then
                            Chart_1.Series.Item(1).LegendText = ComboBox_Valore_2.SelectedItem.ToString
                        Else
                            Chart_1.Series.Item(1).LegendText = ""
                        End If
                    End If
                End If
            End If

        Catch ex As System.Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try
    End Sub

    Private Sub Grafico_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        'Dim bmpPtr As IntPtr
        'Dim ptPoint As New Point

        'Try
        '    AxMSChart.EditCopy()

        '    If (IsClipboardFormatAvailable(CF_BITMAP)) Then
        '        If (OpenClipboard(Me.Handle)) Then
        '            bmpPtr = GetClipboardData(CF_BITMAP)
        '            If (bmpPtr <> IntPtr.Zero) Then
        '                e.Graphics.DrawImage(Image.FromHbitmap(bmpPtr), ptPoint)
        '            End If
        '            CloseClipboard()
        '        End If
        '    End If
        'Catch ex As Exception
        '    ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        'End Try
    End Sub

    Private Sub VisInFormatoGrafico_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.DesignMode = False Then
                Static bOneShot As Boolean
                Static szFormOriginalSize As Size

                Static szDataGridViewOriginalSize As Size
                Static ptDataGridViewOriginalLocation As Point

                Static ptLabel_Dati_1OriginalLocation As Point
                Static ptComboBox_Valore_1OriginalLocation As Point
                Static ptLabel_Dati_2OriginalLocation As Point
                Static ptComboBox_Valore_2OriginalLocation As Point
                Static ptLabel_TipoOriginalLocation As Point
                Static ptComboBox_TipoOriginalLocation As Point

                If bOneShot = False Then
                    bOneShot = True
                    szFormOriginalSize = Me.Size

                    szDataGridViewOriginalSize = Me.Chart_1.Size

                    ptDataGridViewOriginalLocation = Me.Chart_1.Location

                    ptLabel_Dati_1OriginalLocation = Me.Label_Dati_1.Location
                    ptComboBox_Valore_1OriginalLocation = Me.ComboBox_Valore_1.Location

                    ptLabel_Dati_2OriginalLocation = Me.Label_Dati_2.Location
                    ptComboBox_Valore_2OriginalLocation = Me.ComboBox_Valore_2.Location

                    ptLabel_TipoOriginalLocation = Me.Label_Tipo.Location
                    ptComboBox_TipoOriginalLocation = Me.ComboBox_Tipo.Location


                End If

                Me.Chart_1.Height = szDataGridViewOriginalSize.Height - (szFormOriginalSize.Height - Me.Height)
                Me.Chart_1.Width = szDataGridViewOriginalSize.Width - (szFormOriginalSize.Width - Me.Width)

                Me.Label_Dati_1.Top = ptLabel_Dati_1OriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))
                Me.ComboBox_Valore_1.Top = ptComboBox_Valore_1OriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))

                Me.Label_Dati_2.Top = ptLabel_Dati_2OriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))
                Me.ComboBox_Valore_2.Top = ptComboBox_Valore_2OriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))

                Me.Label_Tipo.Top = ptLabel_TipoOriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))
                Me.ComboBox_Tipo.Top = ptComboBox_TipoOriginalLocation.Y - ((szFormOriginalSize.Height - Me.Height))

            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try
    End Sub

    Private Sub ToolStripButton_Stampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Stampa.Click

        'Grafico = Chart_1.Printing.PrintDocument
        'PageSetupDialog.ShowDialog()
        'PrintPreviewDialog.ShowDialog()
        ''Dim bmpPtr As IntPtr
        'Dim ptPoint As New Point

        'Try
        '    AxMSChart.EditCopy()

        '    If (IsClipboardFormatAvailable(CF_BITMAP)) Then
        '        If (OpenClipboard(0)) Then
        '            bmpPtr = GetClipboardData(CF_BITMAP)
        '            If (bmpPtr <> IntPtr.Zero) Then
        '                ' Metto il file su Disco
        '                PictureBox1.Visible = False
        '                PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        '                PictureBox1.Image = Image.FromHbitmap(bmpPtr)
        '                PictureBox1.Image.Save(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\Grafico.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        '                EmptyClipboard()
        '                CloseClipboard()
        '            Else
        '                EmptyClipboard()
        '                CloseClipboard()
        '                ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, "Puntatore null, Cliboard vuoto.", "", "", DEFAULT_OPERATOR_ID, Me)
        '                Exit Sub
        '            End If
        '        Else
        '            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, "Errore in apertura ClipDoard. Errore Nr: " + GetLastError().ToString(), "", "", DEFAULT_OPERATOR_ID, Me)
        '            Exit Sub
        '        End If
        '    Else
        '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, "Formato ClipDoard non disponibile. Errore Nr: " + GetLastError().ToString(), "", "", DEFAULT_OPERATOR_ID, Me)
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        '    Exit Sub
        'End Try

        Try
            Chart_1.SaveImage(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\Grafico.gif", DataVisualization.Charting.ChartImageFormat.Gif)
        Catch ex As Exception

        End Try

        Dim strHTML As String
        Dim strNomeFile As String

        strHTML = ""
        strNomeFile = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\" + Me.Text + ".html"
        Try
            My.Computer.FileSystem.WriteAllText(strNomeFile, strHTML, False)
            strHTML = ""

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
            Exit Sub
        End Try

        strHTML = "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0//EN""" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + """http://www.w3.org/TR/REC-html140/strict.dtd"">" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<HTML>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<HEAD>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<TITLE>" + " Fase Engineering " + "</TITLE>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "</HEAD>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<BODY>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + " Fase Engineering " + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<IMG src=""" + "file:///" + My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoFase.jpg"" align=""center""> " + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.fasenet.com/"">Fase Engineering - www.fasenet.com</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<P1>" + "Elaborato il: " + Date.Now.ToString + "</P1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf


        strHTML = strHTML + "<IMG src=""" + "file:///" + My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\Grafico.gif"" align=""center""> " + Microsoft.VisualBasic.vbCrLf


        strHTML = strHTML + "<DIV align=center>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<IMG src=""" + "file:///" + My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + "\LogoCliente.jpg"" align=""center""> " + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<P1>" + "Fine Documento." + "</P1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<DIV align=left>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        'strHTML = strHTML + "<A href=""http://www.consulenzeperizie.it/"">Developed by Pretolesi Riccardo - www.consulenzeperizie.it</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A>Developed by Pretolesi Riccardo</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.consulenzeperizie.it/"">www.consulenzeperizie.it</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A>  -  </A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.pretolesi.com/"">www.pretolesi.com</A>" + Microsoft.VisualBasic.vbCrLf

        strHTML = strHTML + "</BODY>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "</HTML>" + Microsoft.VisualBasic.vbCrLf

        Try
            My.Computer.FileSystem.WriteAllText(strNomeFile, strHTML, True)
            strHTML = ""

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
            Exit Sub
        End Try

        Try
            Process.Start(strNomeFile)
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try
    End Sub

End Class
