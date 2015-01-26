Imports System.Management
Imports System.Data.SqlClient

Public Class VisDatiDataLogger
    Dim m_bs As New BindingSource
    Dim m_dtVisGraf As New DataTable

    Private m_pPosition As Point
    Private m_strChartTitleText As String = ""
    Private m_strChartEnergiaProdotta As String = ""
    Private m_strChartPotenzaMedia As String = ""

    Private m_iCID As Integer
    Private m_iIDPID As Integer
    Private m_iLIID As Integer

    Property Position() As Point
        Get
            Return m_pPosition
        End Get

        Set(ByVal Position As Point)
            m_pPosition = Position
        End Set
    End Property

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Visualizzazione Dati Data Logger"
        Me.DesktopLocation = m_pPosition

        CaricaCID()

        CaricaIDPID()

        CaricaLIID()

        ' Imposto la data di Start, l'evento scatenato richiama il caricamento dei dati
        Dim dt As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        DateTimePicker_START.Value = dt
        DateTimePicker_STOP.Value = dt.Add(TimeSpan.Parse("23:59:59"))

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

        Dim b_CGEMClientVisDatiStat As Boolean
        If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
            b_CGEMClientVisDatiStat = True
        Else
            b_CGEMClientVisDatiStat = GENERICA_DESCRIZIONE("C_GEMClient_Vis_DatiStat", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
        End If

        If b_CGEMClientVisDatiStat = True Then
            Me.Label_Performance_Ratio.Visible = True
            Me.TextBox_Performance_Ratio.Visible = True
            Me.Label_HG.Visible = True
            Me.TextBox_HG.Visible = True
            Me.Label_LI_Kd.Visible = True
            Me.TextBox_LI_Kd.Visible = True
        Else
            Me.Label_Performance_Ratio.Visible = False
            Me.TextBox_Performance_Ratio.Visible = False
            Me.Label_HG.Visible = False
            Me.TextBox_HG.Visible = False
            Me.Label_LI_Kd.Visible = False
            Me.TextBox_LI_Kd.Visible = False
        End If

    End Sub

    Private Sub ComboBox_C_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_C_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iCID = Me.ComboBox_C_ID.SelectedValue()
            CaricaIDPID()
            m_strChartTitleText = Me.ComboBox_C_ID.Text + " - " + Me.ComboBox_IDP_ID.Text
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIDPID = Me.ComboBox_IDP_ID.SelectedValue()
            CaricaLIID()
            m_strChartTitleText = Me.ComboBox_C_ID.Text + " - " + Me.ComboBox_IDP_ID.Text
        End If

    End Sub

    Private Sub ComboBox_LI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iLIID = Me.ComboBox_LI_ID.SelectedValue()
        End If
        m_strChartTitleText = Me.ComboBox_C_ID.Text + " - " + Me.ComboBox_IDP_ID.Text
    End Sub

    Protected Overrides Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        MyBase.DataGridView_SelectionChanged(sender, e)

    End Sub


    Protected Overrides Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        MyBase.BaseForm_1_FormClosed(sender, e)

    End Sub

    Private Sub CaricaDati(ByVal bSoloAllarmi As Boolean)

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim b_CGEMClientVisAll As Boolean

        ' Opzioni GEM Client
        If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
            b_CGEMClientVisAll = True
        Else
            b_CGEMClientVisAll = GENERICA_DESCRIZIONE("C_GEMClient_Vis_All", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
        End If

        Try

            strSQL = " SELECT ImpiantoDiProduzione.IDP_ID, IngressoTipo.IT_ID, IngressoTipo.IT_Nome, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore, IngressoTipo.IT_UM, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra "
            strSQL = strSQL + " FROM Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON ImpiantoDiProduzione.IDP_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = IngressoTipo.IT_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = LoggerInst.LI_IDP_ID AND LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID"
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE C_ID = " + Me.m_iCID.ToString + " AND IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue().ToString + " AND LI_ID = " + Me.ComboBox_LI_ID.SelectedValue().ToString + " "
            If b_CGEMClientVisAll = True Then
                strSQL = strSQL + " AND (IngressoTipo.IT_ID >= 151 AND IngressoTipo.IT_ID <= 189) "
                strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "
            Else
                strSQL = strSQL + " AND (IngressoTipo.IT_ID >= 151 AND IngressoTipo.IT_ID <= 158) "
                strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "
            End If
            strSQL = strSQL + " ORDER BY LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra DESC, IngressoTipo.IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", DateTimePicker_START.Value)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", DateTimePicker_STOP.Value)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    ' Aggiungo la colonna descrizione
                    ds.Tables(0).Columns.Add("LIPC_Descrizione")
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If dr.Item("IT_ID") = 151 Then
                            dr.Item("LIPC_Descrizione") = GetLIPCLPCDESCRMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 133)
                        End If
                        If dr.Item("IT_ID") = 152 Then
                            dr.Item("LIPC_Descrizione") = GetLIPCLPCDESCRMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 134)
                        End If
                        If dr.Item("IT_ID") = 153 Then
                            dr.Item("LIPC_Descrizione") = GetLIPCLPCDESCRMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 135)
                        End If
                    Next dr
                    For Each dc As DataColumn In ds.Tables(0).Columns
                        ' Inserisco le caption
                        strSQL = "SELECT DCC_ColonnaNome FROM [DatabaseCampoColonna] WHERE DCC_CampoNome = @DCC_CampoNome "
                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@DCC_CampoNome", dc.ColumnName.ToString)

                        Dim rdr As SqlDataReader = cmd.ExecuteReader()
                        If rdr.HasRows = True Then
                            rdr.Read()
                            dc.Caption = rdr.Item(0).ToString
                        End If
                        rdr.Close()

                    Next dc

                    m_bs.DataSource = ds.Tables(0)
                    MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
                    MyBase.BS = m_bs


                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Private Sub CaricaDatiStatistici()
        ' Aggiorno anche le somme dei contatori di energia 
        ' 151 Energia Consumata
        ' 152,153 Energia Prodotta da Cont 1 e 2
        ' 155 Energia Dai Sensori di irraggiamento
        ' 156 Energia Da tutte le uscite inverter
        ' 157 Energia Da tutte le stringhe
        ' 415 W/mq Sensori di irraggiamento

        Dim dbl_CDC As Double
        Dim dbl_CDP_1 As Double
        Dim dbl_CDP_2 As Double
        Dim dbl_SI As Double
        Dim dbl_STR As Double
        Dim dbl_INV As Double
        Dim dblPR As Double
        Dim dblHG As Double
        Dim dblMS As Double
        Dim dblMT As Double
        Dim dblS As Double
        Dim dbl_P1 As Double
        Dim b_CGEMClientVisDatiStat As Boolean

        GetStatMemoDay(Me.ComboBox_LI_ID.SelectedValue(), dbl_CDC, dbl_CDP_1, dbl_CDP_2, dbl_SI, dbl_STR, dbl_INV, dblPR, dblHG, dblMS, dblMT, dblS, DateTimePicker_START.Value, DateTimePicker_STOP.Value)

        TextBox_EnergiaProdotta.Text = " - " + (dbl_CDP_1 + dbl_CDP_2).ToString() + " - " + dbl_CDP_1.ToString() + " - " + dbl_CDP_2.ToString() + " - "
        TextBox_EnergiaDaInverter.Text = Math.Round(dbl_INV, 1).ToString()
        If dbl_INV > 0.0 Then
            TextBox_REND_INV_CDP.Text = Math.Round((100.0 - (((dbl_INV - (dbl_CDP_1 + dbl_CDP_2)) / dbl_INV) * 100.0)), 0).ToString() + " %"
        Else
            TextBox_REND_INV_CDP.Text = "-"
        End If
        TextBox_REND_INV_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 112)
        TextBox_EnergiaDaStringhe.Text = Math.Round(dbl_STR, 1).ToString()
        If dbl_STR > 0.0 Then
            TextBox_REND_STR_CDP.Text = Math.Round((100.0 - (((dbl_STR - (dbl_CDP_1 + dbl_CDP_2)) / dbl_STR) * 100.0)), 0).ToString() + " %"
        Else
            TextBox_REND_STR_CDP.Text = "-"
        End If
        TextBox_REND_STR_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 113)
        TextBox_EnergiaDaSensoriDiIrragg.Text = Math.Round(dbl_SI, 1).ToString()
        If dbl_SI > 0.0 Then
            TextBox_REND_SI_CDP.Text = Math.Round((100.0 - (((dbl_SI - (dbl_CDP_1 + dbl_CDP_2)) / dbl_SI) * 100.0)), 0).ToString() + " %"
        Else
            TextBox_REND_SI_CDP.Text = "-"
        End If
        TextBox_REND_SI_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 111)

        TextBox_EnergiaConsumata.Text = Math.Round(dbl_CDC, 1).ToString() 'dbl_CDCGetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 151, 151, DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()
        TextBox_EnergiaMediaDaSensoriDiIrragg.Text = Math.Round(dblMS, 1).ToString()
        TextBox_MediaDaSondeDiTempPann.Text = Math.Round(dblMT, 1).ToString()

        ' Visualizzo ultimo dato relativo ai contatori, in questo caso P1
        dbl_P1 = GetLastStoredITIDDLValue(ComboBox_LI_ID.SelectedValue(), 141, "", "")
        If dbl_P1 > 0.0 Then
            TextBox_P1.Visible = True
            Label_P11.Visible = True
            Label_P12.Visible = True
            TextBox_P1.Text = dbl_P1.ToString()
        Else
            TextBox_P1.Visible = False
            Label_P11.Visible = False
            Label_P12.Visible = False
        End If

        ' Totale Mese Precedente
        Dim dtStop As New Date(Date.Now.Year, Date.Now.Month, 1)
        Dim dtStart As New Date
        Dim dbl_CDC_MesePrec As Double
        Dim dbl_CDP_1_MesePrec As Double
        Dim dbl_CDP_2_MesePrec As Double
        Dim dbl_SI_MesePrec As Double
        Dim dbl_STR_MesePrec As Double
        Dim dbl_INV_MesePrec As Double
        Dim dblPR_MesePrec As Double
        Dim dblHG_MesePrec As Double
        Dim dblMS_MesePrec As Double
        Dim dblMT_MesePrec As Double
        Dim dblS_MesePrec As Double

        dtStart = dtStop.AddMonths(-1)
        GetStatMemoDay(Me.ComboBox_LI_ID.SelectedValue(), dbl_CDC_MesePrec, dbl_CDP_1_MesePrec, dbl_CDP_2_MesePrec, dbl_SI_MesePrec, dbl_STR_MesePrec, dbl_INV_MesePrec, dblPR_MesePrec, dblHG_MesePrec, dblMS_MesePrec, dblMT_MesePrec, dblS_MesePrec, dtStart, dtStop)
        TextBox_EnergiaProdottaMesePrec.Text = Math.Round((dbl_CDP_1_MesePrec + dbl_CDP_2_MesePrec), 1).ToString()

        ' Opzioni GEM Client
        If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
            b_CGEMClientVisDatiStat = True
        Else
            b_CGEMClientVisDatiStat = GENERICA_DESCRIZIONE("C_GEMClient_Vis_DatiStat", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
        End If

        If b_CGEMClientVisDatiStat = True Then
            TextBox_Performance_Ratio.Text = Math.Round(dblPR, 1).ToString()
            TextBox_HG.Text = Math.Round(dblHG, 1).ToString()

            Me.Label_Performance_Ratio.Visible = True
            Me.TextBox_Performance_Ratio.Visible = True
            Me.Label_HG.Visible = True
            Me.TextBox_HG.Visible = True
            Me.Label_LI_Kd.Visible = False
            Me.TextBox_LI_Kd.Visible = False
        Else
            Me.Label_Performance_Ratio.Visible = False
            Me.TextBox_Performance_Ratio.Visible = False
            Me.Label_HG.Visible = False
            Me.TextBox_HG.Visible = False
            Me.Label_LI_Kd.Visible = False
            Me.TextBox_LI_Kd.Visible = False
        End If
        Dim dsRep As New DataSet

        Try

            dsRep.Tables.Add()
            dsRep.Tables(0).Columns.Add("N Impianto Di Produzione")
            dsRep.Tables(0).Columns("N Impianto Di Produzione").Caption = "Impianto Di Produzione"
            dsRep.Tables(0).Columns.Add("N Data Inizio Conteggio")
            dsRep.Tables(0).Columns("N Data Inizio Conteggio").Caption = "Data Inizio Conteggio"
            dsRep.Tables(0).Columns.Add("N Data Fine Conteggio")
            dsRep.Tables(0).Columns("N Data Fine Conteggio").Caption = "Data Fine Conteggio"
            dsRep.Tables(0).Columns.Add("N Energia Da Sensori Di Irraggiamento Kwh")
            dsRep.Tables(0).Columns("N Energia Da Sensori Di Irraggiamento Kwh").Caption = "Energia Da Sensori Di Irraggiamento Kwh"
            dsRep.Tables(0).Columns.Add("N Energia Da Stringhe Kwh")
            dsRep.Tables(0).Columns("N Energia Da Stringhe Kwh").Caption = "Energia Da Stringhe Kwh"
            dsRep.Tables(0).Columns.Add("N Energia Da Inverter Kwh")
            dsRep.Tables(0).Columns("N Energia Da Inverter Kwh").Caption = "Energia Da Inverter Kwh"
            dsRep.Tables(0).Columns.Add("N Energia Da Contatori di Produzione Kwh")
            dsRep.Tables(0).Columns("N Energia Da Contatori di Produzione Kwh").Caption = "Energia Da Contatori di Produzione Kwh"
            dsRep.Tables(0).Columns.Add("N Potenza Gestita Kwh")
            dsRep.Tables(0).Columns("N Potenza Gestita Kwh").Caption = "Potenza Gestita Kwh"
            If b_CGEMClientVisDatiStat = True Then
                dsRep.Tables(0).Columns.Add("N Performance Ratio (%)")
                dsRep.Tables(0).Columns("N Performance Ratio (%)").Caption = "Performance Ratio (%)"
                dsRep.Tables(0).Columns.Add("N HG (Kwh/mq)")
                dsRep.Tables(0).Columns("N HG (Kwh/mq)").Caption = "N HG (Kwh/mq)"
            End If
            dsRep.Tables(0).Columns.Add("N Media Solarimetri(W/mq)")
            dsRep.Tables(0).Columns("N Media Solarimetri(W/mq)").Caption = "Media Solarimetri(W/mq)"
            dsRep.Tables(0).Columns.Add("N Media Temp. Pann.(°C)")
            dsRep.Tables(0).Columns("N Media Temp. Pann.(°C)").Caption = "Media Temp. Pann.(°C)"
            dsRep.Tables(0).Columns.Add("N Soglia W/mq Calcolo Medie")
            dsRep.Tables(0).Columns("N Soglia W/mq Calcolo Medie").Caption = "Soglia W/mq Calcolo Medie"

            dsRep.Tables(0).Rows.Add()
            dsRep.Tables(0).Rows(0).Item("N Impianto Di Produzione") = Me.ComboBox_C_ID.Text()
            dsRep.Tables(0).Rows(0).Item("N Data Inizio Conteggio") = DateTimePicker_START.Value
            dsRep.Tables(0).Rows(0).Item("N Data Fine Conteggio") = DateTimePicker_STOP.Value
            dsRep.Tables(0).Rows(0).Item("N Energia Da Sensori Di Irraggiamento Kwh") = Math.Round(dbl_SI, 1)
            dsRep.Tables(0).Rows(0).Item("N Energia Da Stringhe Kwh") = Math.Round(dbl_STR, 1)
            dsRep.Tables(0).Rows(0).Item("N Energia Da Inverter Kwh") = Math.Round(dbl_INV, 1)
            dsRep.Tables(0).Rows(0).Item("N Energia Da Contatori di Produzione Kwh") = Math.Round((dbl_CDP_1 + dbl_CDP_2), 1)
            dsRep.Tables(0).Rows(0).Item("N Potenza Gestita Kwh") = GENERICA_DESCRIZIONE("LI_PotenzaGestitaKw", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
            If b_CGEMClientVisDatiStat = True Then
                dsRep.Tables(0).Rows(0).Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                dsRep.Tables(0).Rows(0).Item("N HG (Kwh/mq)") = Math.Round(dblHG, 1)
            End If
            dsRep.Tables(0).Rows(0).Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
            dsRep.Tables(0).Rows(0).Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
            dsRep.Tables(0).Rows(0).Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblS, 1)

            m_bs.DataSource = dsRep.Tables(0)
            MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
            MyBase.BS = m_bs

        Catch ex As Exception
            ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        dsRep.Dispose()

    End Sub

    Private Sub CaricaDatiDL()

        Dim bRes As Boolean = False

        Dim dtValoriDelGiornoStart As Date
        Dim dtValoriDelGiornoStop As Date
        Dim iNrSolarimetri As Integer
        Dim iValoreUDTMinuti As Integer
        Dim dblLIPotenzaGestitaKw As Double
        Dim dblLIKd As Double
        Dim dblEgse As Double
        Dim dblHj As Double
        Dim dblLISogliaWMQ As Double
        Dim dblEref As Double
        Dim dbl_CDC As Double
        Dim dbl_CDP_1 As Double
        Dim dbl_CDP_2 As Double
        Dim dbl_SI As Double
        Dim dbl_STR As Double
        Dim dbl_INV As Double
        Dim dblHG As Double
        Dim dblPR As Double
        Dim dblMS As Double
        Dim dblMT As Double

        Dim dbl_P1 As Double
        Dim b_CGEMClientVisDatiStat As Boolean
        Dim ts As TimeSpan
        ts = DateTimePicker_STOP.Value - DateTimePicker_START.Value

        If ts.TotalHours > 48 Then
            System.Windows.Forms.MessageBox.Show(Owner, "Attenzione, non e' possibile impostare una differenza di date superiore a 48 ore per i dati del Datalogger. Per intervalli superiori e' possibile consultare solo i dati statistici.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else

            dtValoriDelGiornoStart = DateTimePicker_START.Value
            dtValoriDelGiornoStop = DateTimePicker_STOP.Value

            ' Ricavo i valori
            If GetEnergiaCDC_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDC) = True Then
                If GetEnergiaCDP_1_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDP_1) = True Then
                    If GetEnergiaCDP_2_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDP_2) = True Then
                        If GetEnergiaSI_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_SI) = True Then
                            If GetEnergiaSTR_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_STR) = True Then
                                If GetEnergiaINV_ByLIID(Me.ComboBox_LI_ID.SelectedValue(), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_INV) = True Then
                                    dblLISogliaWMQ = GENERICA_DESCRIZIONE("LI_SogliaCalcoloHG", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
                                    dblLIKd = GENERICA_DESCRIZIONE("LI_Kd", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
                                    dblEgse = (dbl_CDP_1 + dbl_CDP_2)
                                    iNrSolarimetri = GetLIPCLPCVALMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 6)
                                    iValoreUDTMinuti = GetLIPCLPCVALMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 125)

                                    If GetHj(Me.ComboBox_LI_ID.SelectedValue(), iNrSolarimetri, iValoreUDTMinuti, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dblHj) = True Then
                                        If GetHG(Me.ComboBox_LI_ID.SelectedValue(), iNrSolarimetri, iValoreUDTMinuti, dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dblHG) = True Then
                                            dblLIPotenzaGestitaKw = GENERICA_DESCRIZIONE("LI_PotenzaGestitaKw", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
                                            dblEref = dblLIPotenzaGestitaKw * dblHj
                                            If (dblEref > 0.0) And (dblLIKd > 0.0) Then
                                                dblPR = Math.Round(((dblEgse / (dblEref * dblLIKd)) * 100.0), 1)
                                            Else
                                                dblPR = Math.Round(0.0, 1)
                                            End If
                                            ' Irraggiamento medio con soglia w/mq
                                            If GetIrraggiamentoMedio(Me.ComboBox_LI_ID.SelectedValue(), iNrSolarimetri, dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dblMS) = True Then
                                                ' Temperatura media con soglia w/mq
                                                If GetTemperaturaMedia(Me.ComboBox_LI_ID.SelectedValue(), dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dblMT) = True Then

                                                    TextBox_EnergiaProdotta.Text = " - " + (dbl_CDP_1 + dbl_CDP_2).ToString() + " - " + dbl_CDP_1.ToString() + " - " + dbl_CDP_2.ToString() + " - "
                                                    TextBox_EnergiaDaInverter.Text = Math.Round(dbl_INV, 1).ToString()
                                                    If dbl_INV > 0.0 Then
                                                        TextBox_REND_INV_CDP.Text = Math.Round((100.0 - (((dbl_INV - (dbl_CDP_1 + dbl_CDP_2)) / dbl_INV) * 100.0)), 0).ToString() + " %"
                                                    Else
                                                        TextBox_REND_INV_CDP.Text = "-"
                                                    End If
                                                    TextBox_REND_INV_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 112)
                                                    TextBox_EnergiaDaStringhe.Text = Math.Round(dbl_STR, 1).ToString()
                                                    If dbl_STR > 0.0 Then
                                                        TextBox_REND_STR_CDP.Text = Math.Round((100.0 - (((dbl_STR - (dbl_CDP_1 + dbl_CDP_2)) / dbl_STR) * 100.0)), 0).ToString() + " %"
                                                    Else
                                                        TextBox_REND_STR_CDP.Text = "-"
                                                    End If
                                                    TextBox_REND_STR_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 113)
                                                    TextBox_EnergiaDaSensoriDiIrragg.Text = Math.Round(dbl_SI, 1).ToString()
                                                    If dbl_SI > 0.0 Then
                                                        TextBox_REND_SI_CDP.Text = Math.Round((100.0 - (((dbl_SI - (dbl_CDP_1 + dbl_CDP_2)) / dbl_SI) * 100.0)), 0).ToString() + " %"
                                                    Else
                                                        TextBox_REND_SI_CDP.Text = "-"
                                                    End If
                                                    TextBox_REND_SI_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 111)

                                                    TextBox_EnergiaConsumata.Text = Math.Round(dbl_CDC, 1).ToString() 'dbl_CDCGetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 151, 151, DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()
                                                    TextBox_Performance_Ratio.Text = Math.Round(dblPR, 1).ToString()
                                                    TextBox_HG.Text = Math.Round(dblHG, 1).ToString()
                                                    TextBox_EnergiaMediaDaSensoriDiIrragg.Text = Math.Round(dblMS, 1).ToString()
                                                    TextBox_MediaDaSondeDiTempPann.Text = Math.Round(dblMT, 1).ToString()

                                                    ' Visualizzo ultimo dato relativo ai contatori, in questo caso P1
                                                    dbl_P1 = GetLastStoredITIDDLValue(ComboBox_LI_ID.SelectedValue(), 141, "", "")
                                                    If dbl_P1 > 0.0 Then
                                                        TextBox_P1.Visible = True
                                                        Label_P11.Visible = True
                                                        Label_P12.Visible = True
                                                        TextBox_P1.Text = dbl_P1.ToString()
                                                    Else
                                                        TextBox_P1.Visible = False
                                                        Label_P11.Visible = False
                                                        Label_P12.Visible = False
                                                    End If

                                                    ' Totale Mese Precedente
                                                    Dim dtStop As New Date(Date.Now.Year, Date.Now.Month, 1)
                                                    Dim dtStart As New Date
                                                    Dim dbl_CDC_MesePrec As Double
                                                    Dim dbl_CDP_1_MesePrec As Double
                                                    Dim dbl_CDP_2_MesePrec As Double
                                                    Dim dbl_SI_MesePrec As Double
                                                    Dim dbl_STR_MesePrec As Double
                                                    Dim dbl_INV_MesePrec As Double
                                                    Dim dblPR_MesePrec As Double
                                                    Dim dblHG_MesePrec As Double
                                                    Dim dblMS_MesePrec As Double
                                                    Dim dblMT_MesePrec As Double
                                                    Dim dblS_MesePrec As Double

                                                    dtStart = dtStop.AddMonths(-1)
                                                    GetStatMemoDay(Me.ComboBox_LI_ID.SelectedValue(), dbl_CDC_MesePrec, dbl_CDP_1_MesePrec, dbl_CDP_2_MesePrec, dbl_SI_MesePrec, dbl_STR_MesePrec, dbl_INV_MesePrec, dblPR_MesePrec, dblHG_MesePrec, dblMS_MesePrec, dblMT_MesePrec, dblS_MesePrec, dtStart, dtStop)
                                                    TextBox_EnergiaProdottaMesePrec.Text = Math.Round((dbl_CDP_1_MesePrec + dbl_CDP_2_MesePrec), 1).ToString()

                                                    ' Opzioni GEM Client
                                                    If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
                                                        b_CGEMClientVisDatiStat = True
                                                    Else
                                                        b_CGEMClientVisDatiStat = GENERICA_DESCRIZIONE("C_GEMClient_Vis_DatiStat", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
                                                    End If

                                                    If b_CGEMClientVisDatiStat = True Then
                                                        TextBox_Performance_Ratio.Text = Math.Round(dblPR, 1).ToString()
                                                        TextBox_HG.Text = Math.Round(dblHG, 1).ToString()

                                                        Me.Label_Performance_Ratio.Visible = True
                                                        Me.TextBox_Performance_Ratio.Visible = True
                                                        Me.Label_HG.Visible = True
                                                        Me.TextBox_HG.Visible = True
                                                        Me.Label_LI_Kd.Visible = False
                                                        Me.TextBox_LI_Kd.Visible = False
                                                    Else
                                                        Me.Label_Performance_Ratio.Visible = False
                                                        Me.TextBox_Performance_Ratio.Visible = False
                                                        Me.Label_HG.Visible = False
                                                        Me.TextBox_HG.Visible = False
                                                        Me.Label_LI_Kd.Visible = False
                                                        Me.TextBox_LI_Kd.Visible = False
                                                    End If
                                                    Dim dsRep As New DataSet

                                                    Try

                                                        dsRep.Tables.Add()
                                                        dsRep.Tables(0).Columns.Add("N Impianto Di Produzione")
                                                        dsRep.Tables(0).Columns("N Impianto Di Produzione").Caption = "Impianto Di Produzione"
                                                        dsRep.Tables(0).Columns.Add("N Data Inizio Conteggio")
                                                        dsRep.Tables(0).Columns("N Data Inizio Conteggio").Caption = "Data Inizio Conteggio"
                                                        dsRep.Tables(0).Columns.Add("N Data Fine Conteggio")
                                                        dsRep.Tables(0).Columns("N Data Fine Conteggio").Caption = "Data Fine Conteggio"
                                                        dsRep.Tables(0).Columns.Add("N Energia Da Sensori Di Irraggiamento Kwh")
                                                        dsRep.Tables(0).Columns("N Energia Da Sensori Di Irraggiamento Kwh").Caption = "Energia Da Sensori Di Irraggiamento Kwh"
                                                        dsRep.Tables(0).Columns.Add("N Energia Da Stringhe Kwh")
                                                        dsRep.Tables(0).Columns("N Energia Da Stringhe Kwh").Caption = "Energia Da Stringhe Kwh"
                                                        dsRep.Tables(0).Columns.Add("N Energia Da Inverter Kwh")
                                                        dsRep.Tables(0).Columns("N Energia Da Inverter Kwh").Caption = "Energia Da Inverter Kwh"
                                                        dsRep.Tables(0).Columns.Add("N Energia Da Contatori di Produzione Kwh")
                                                        dsRep.Tables(0).Columns("N Energia Da Contatori di Produzione Kwh").Caption = "Energia Da Contatori di Produzione Kwh"
                                                        dsRep.Tables(0).Columns.Add("N Potenza Gestita Kwh")
                                                        dsRep.Tables(0).Columns("N Potenza Gestita Kwh").Caption = "Potenza Gestita Kwh"
                                                        If b_CGEMClientVisDatiStat = True Then
                                                            dsRep.Tables(0).Columns.Add("N Performance Ratio (%)")
                                                            dsRep.Tables(0).Columns("N Performance Ratio (%)").Caption = "Performance Ratio (%)"
                                                            dsRep.Tables(0).Columns.Add("N HG (Kwh/mq)")
                                                            dsRep.Tables(0).Columns("N HG (Kwh/mq)").Caption = "N HG (Kwh/mq)"
                                                        End If
                                                        dsRep.Tables(0).Columns.Add("N Media Solarimetri(W/mq)")
                                                        dsRep.Tables(0).Columns("N Media Solarimetri(W/mq)").Caption = "Media Solarimetri(W/mq)"
                                                        dsRep.Tables(0).Columns.Add("N Media Temp. Pann.(°C)")
                                                        dsRep.Tables(0).Columns("N Media Temp. Pann.(°C)").Caption = "Media Temp. Pann.(°C)"
                                                        dsRep.Tables(0).Columns.Add("N Soglia W/mq Calcolo Medie")
                                                        dsRep.Tables(0).Columns("N Soglia W/mq Calcolo Medie").Caption = "Soglia W/mq Calcolo Medie"

                                                        dsRep.Tables(0).Rows.Add()
                                                        dsRep.Tables(0).Rows(0).Item("N Impianto Di Produzione") = Me.ComboBox_C_ID.Text()
                                                        dsRep.Tables(0).Rows(0).Item("N Data Inizio Conteggio") = DateTimePicker_START.Value
                                                        dsRep.Tables(0).Rows(0).Item("N Data Fine Conteggio") = DateTimePicker_STOP.Value
                                                        dsRep.Tables(0).Rows(0).Item("N Energia Da Sensori Di Irraggiamento Kwh") = Math.Round(dbl_SI, 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Energia Da Stringhe Kwh") = Math.Round(dbl_STR, 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Energia Da Inverter Kwh") = Math.Round(dbl_INV, 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Energia Da Contatori di Produzione Kwh") = Math.Round((dbl_CDP_1 + dbl_CDP_2), 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Potenza Gestita Kwh") = GENERICA_DESCRIZIONE("LI_PotenzaGestitaKw", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
                                                        If b_CGEMClientVisDatiStat = True Then
                                                            dsRep.Tables(0).Rows(0).Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                                                            dsRep.Tables(0).Rows(0).Item("N HG (Kwh/mq)") = Math.Round(dblHG, 1)
                                                        End If
                                                        dsRep.Tables(0).Rows(0).Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
                                                        dsRep.Tables(0).Rows(0).Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblLISogliaWMQ, 1)

                                                        m_bs.DataSource = dsRep.Tables(0)
                                                        MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
                                                        MyBase.BS = m_bs

                                                        bRes = True

                                                    Catch ex As Exception
                                                        ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
                                                    End Try

                                                    dsRep.Dispose()

                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    'Private Sub CaricaDatiStatistici()

    '    ' Aggiorno anche le somme dei contatori di energia 
    '    ' 151 Energia Consumata
    '    ' 152,153 Energia Prodotta da Cont 1 e 2
    '    ' 155 Energia Dai Sensori di irraggiamento
    '    ' 156 Energia Da tutte le uscite inverter
    '    ' 157 Energia Da tutte le stringhe
    '    ' 415 W/mq Sensori di irraggiamento

    '    Dim dbl_CDP_1 As Double
    '    Dim dbl_CDP_2 As Double
    '    Dim dbl_INV As Double
    '    Dim dbl_STR As Double
    '    Dim dbl_SI As Double
    '    Dim dbl_SI_AVG As Double
    '    Dim dbl_TP_AVG As Double
    '    Dim b_CGEMClientVisDatiStat As Boolean
    '    Dim dbl_P1 As Double
    '    Dim iValoreUDTMinuti As Integer
    '    Dim iNrSolarimetri As Integer
    '    Dim dblLIPotenzaGestitaKw As Double
    '    Dim dblLIKd As Double
    '    Dim dblEgse As Double
    '    Dim dblHj As Double
    '    Dim dblLISogliaCalcoloHG As Double
    '    Dim dblHG As Double
    '    Dim dblEref As Double
    '    Dim dblPR As Double

    '    dbl_CDP_1 = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 152, 152, DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    dbl_CDP_2 = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 153, 153, DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    dbl_INV = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 156, 156, DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    dbl_STR = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 157, 157, DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    dbl_SI = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 155, 155, DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    TextBox_EnergiaProdotta.Text = " - " + (dbl_CDP_1 + dbl_CDP_2).ToString() + " - " + dbl_CDP_1.ToString() + " - " + dbl_CDP_2.ToString() + " - "
    '    TextBox_EnergiaDaInverter.Text = dbl_INV.ToString()
    '    If dbl_INV > 0.0 Then
    '        TextBox_REND_INV_CDP.Text = Math.Round((100.0 - (((dbl_INV - (dbl_CDP_1 + dbl_CDP_2)) / dbl_INV) * 100.0)), 0).ToString() + " %"
    '    Else
    '        TextBox_REND_INV_CDP.Text = "-"
    '    End If
    '    TextBox_REND_INV_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 112)
    '    TextBox_EnergiaDaStringhe.Text = dbl_STR.ToString()
    '    If dbl_STR > 0.0 Then
    '        TextBox_REND_STR_CDP.Text = Math.Round((100.0 - (((dbl_STR - (dbl_CDP_1 + dbl_CDP_2)) / dbl_STR) * 100.0)), 0).ToString() + " %"
    '    Else
    '        TextBox_REND_STR_CDP.Text = "-"
    '    End If
    '    TextBox_REND_STR_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 113)
    '    TextBox_EnergiaDaSensoriDiIrragg.Text = dbl_SI.ToString()
    '    If dbl_SI > 0.0 Then
    '        TextBox_REND_SI_CDP.Text = Math.Round((100.0 - (((dbl_SI - (dbl_CDP_1 + dbl_CDP_2)) / dbl_SI) * 100.0)), 0).ToString() + " %"
    '    Else
    '        TextBox_REND_SI_CDP.Text = "-"
    '    End If
    '    TextBox_REND_SI_CDP_REF.Text = GetLIPCLPCVALMEMODB(ComboBox_LI_ID.SelectedValue(), 111)

    '    TextBox_EnergiaConsumata.Text = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 151, 151, DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()

    '    dbl_SI_AVG = GetIrraggiamento(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 415, 415, DateTimePicker_START.Value, DateTimePicker_STOP.Value, "AVG", 1)
    '    TextBox_EnergiaMediaDaSensoriDiIrragg.Text = dbl_SI_AVG.ToString()

    '    dbl_TP_AVG = GetTemperaturaMedia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), DateTimePicker_START.Value, DateTimePicker_STOP.Value)
    '    TextBox_MediaDaSondeDiTempPann.Text = dbl_TP_AVG.ToString()

    '    ' Visualizzo ultimo dato relativo ai contatori, in questo caso P1
    '    dbl_P1 = GetLastStoredITIDDLValue(ComboBox_LI_ID.SelectedValue(), 141, "", "")
    '    If dbl_P1 > 0.0 Then
    '        TextBox_P1.Visible = True
    '        Label_P11.Visible = True
    '        Label_P12.Visible = True
    '        TextBox_P1.Text = dbl_P1.ToString()
    '    Else
    '        TextBox_P1.Visible = False
    '        Label_P11.Visible = False
    '        Label_P12.Visible = False
    '    End If

    '    ' Totale Mese Precedente
    '    Dim dtStop As New Date(Date.Now.Year, Date.Now.Month, 1)
    '    Dim dtStart As New Date
    '    dtStart = dtStop.AddMonths(-1)
    '    TextBox_EnergiaProdottaMesePrec.Text = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 152, 153, dtStart, dtStop)

    '    ' Opzioni GEM Client
    '    If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
    '        b_CGEMClientVisDatiStat = True
    '    Else
    '        b_CGEMClientVisDatiStat = GENERICA_DESCRIZIONE("C_GEMClient_Vis_DatiStat", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
    '    End If

    '    If b_CGEMClientVisDatiStat = True Then
    '        ' Producibilita' Specifica
    '        iValoreUDTMinuti = GetLIPCLPCVALMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 125)
    '        iNrSolarimetri = GetLIPCLPCVALMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 6)
    '        dblLIPotenzaGestitaKw = GENERICA_DESCRIZIONE("LI_PotenzaGestitaKw", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
    '        dblLIKd = GENERICA_DESCRIZIONE("LI_Kd", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
    '        dblEgse = (dbl_CDP_1 + dbl_CDP_2)
    '        dblHj = GetHj(Me.ComboBox_LI_ID.SelectedValue(), iNrSolarimetri, iValoreUDTMinuti, DateTimePicker_START.Value, DateTimePicker_STOP.Value, 3)
    '        dblLISogliaCalcoloHG = GENERICA_DESCRIZIONE("LI_SogliaCalcoloHG", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue(), DEFAULT_OPERATOR_ID)
    '        dblHG = GetHG(Me.ComboBox_LI_ID.SelectedValue(), iNrSolarimetri, iValoreUDTMinuti, dblLISogliaCalcoloHG, DateTimePicker_START.Value, DateTimePicker_STOP.Value, 3)
    '        dblEref = dblLIPotenzaGestitaKw * dblHj
    '        If (dblEref > 0.0) And (dblLIKd > 0.0) Then
    '            dblPR = ((dblEgse / (dblEref * dblLIKd)) * 100.0)
    '            TextBox_Performance_Ratio.Text = Math.Round(dblPR, 3).ToString()
    '        Else
    '            TextBox_Performance_Ratio.Text = "---"
    '        End If
    '        TextBox_HG.Text = dblHG.ToString()
    '        TextBox_LI_Kd.Text = dblLIKd.ToString()

    '        Me.Label_Performance_Ratio.Visible = True
    '        Me.TextBox_Performance_Ratio.Visible = True
    '        Me.Label_HG.Visible = True
    '        Me.TextBox_HG.Visible = True
    '        Me.Label_LI_Kd.Visible = True
    '        Me.TextBox_LI_Kd.Visible = True
    '    Else
    '        Me.Label_Performance_Ratio.Visible = False
    '        Me.TextBox_Performance_Ratio.Visible = False
    '        Me.Label_HG.Visible = False
    '        Me.TextBox_HG.Visible = False
    '        Me.Label_LI_Kd.Visible = False
    '        Me.TextBox_LI_Kd.Visible = False
    '    End If

    '    Dim dsRep As New DataSet

    '    Try

    '        dsRep.Tables.Add()
    '        dsRep.Tables(0).Columns.Add("N Impianto Di Produzione")
    '        dsRep.Tables(0).Columns("N Impianto Di Produzione").Caption = "Impianto Di Produzione"
    '        dsRep.Tables(0).Columns.Add("N Data Inizio Conteggio")
    '        dsRep.Tables(0).Columns("N Data Inizio Conteggio").Caption = "Data Inizio Conteggio"
    '        dsRep.Tables(0).Columns.Add("N Data Fine Conteggio")
    '        dsRep.Tables(0).Columns("N Data Fine Conteggio").Caption = "Data Fine Conteggio"
    '        dsRep.Tables(0).Columns.Add("N Energia Da Sensori Di Irraggiamento Kwh")
    '        dsRep.Tables(0).Columns("N Energia Da Sensori Di Irraggiamento Kwh").Caption = "Energia Da Sensori Di Irraggiamento Kwh"
    '        dsRep.Tables(0).Columns.Add("N Energia Da Stringhe Kwh")
    '        dsRep.Tables(0).Columns("N Energia Da Stringhe Kwh").Caption = "Energia Da Stringhe Kwh"
    '        dsRep.Tables(0).Columns.Add("N Energia Da Inverter Kwh")
    '        dsRep.Tables(0).Columns("N Energia Da Inverter Kwh").Caption = "Energia Da Inverter Kwh"
    '        dsRep.Tables(0).Columns.Add("N Energia Da Contatori di Produzione Kwh")
    '        dsRep.Tables(0).Columns("N Energia Da Contatori di Produzione Kwh").Caption = "Energia Da Contatori di Produzione Kwh"

    '        If b_CGEMClientVisDatiStat = True Then
    '            dsRep.Tables(0).Columns.Add("N Performance Ratio (%)")
    '            dsRep.Tables(0).Columns("N Performance Ratio (%)").Caption = "Performance Ratio (%)"
    '            dsRep.Tables(0).Columns.Add("N HG (Kwh/mq)")
    '            dsRep.Tables(0).Columns("N HG (Kwh/mq)").Caption = "N HG (Kwh/mq)"
    '        End If

    '        dsRep.Tables(0).Rows.Add()
    '        dsRep.Tables(0).Rows(0).Item("N Impianto Di Produzione") = Me.ComboBox_C_ID.Text
    '        dsRep.Tables(0).Rows(0).Item("N Data Inizio Conteggio") = DateTimePicker_START.Value
    '        dsRep.Tables(0).Rows(0).Item("N Data Fine Conteggio") = DateTimePicker_STOP.Value
    '        dsRep.Tables(0).Rows(0).Item("N Energia Da Sensori Di Irraggiamento Kwh") = dbl_SI
    '        dsRep.Tables(0).Rows(0).Item("N Energia Da Stringhe Kwh") = dbl_STR
    '        dsRep.Tables(0).Rows(0).Item("N Energia Da Inverter Kwh") = dbl_INV
    '        dsRep.Tables(0).Rows(0).Item("N Energia Da Contatori di Produzione Kwh") = (dbl_CDP_1 + dbl_CDP_2)

    '        If b_CGEMClientVisDatiStat = True Then
    '            dsRep.Tables(0).Rows(0).Item("N Performance Ratio (%)") = Math.Round(dblPR, 3)
    '            dsRep.Tables(0).Rows(0).Item("N HG (Kwh/mq)") = dblHG
    '        End If

    '        m_bs.DataSource = dsRep.Tables(0)
    '        MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
    '        MyBase.BS = m_bs

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
    '    End Try

    '    dsRep.Dispose()

    'End Sub

    Private Sub CaricaDatiGrafico()
        ' Creo la base per la visualizzazione grafica
        Dim iIndice_1 As Integer
        Dim drTemp As DataRow
        Dim dcTemp As DataColumn
        Dim dtVisGraf As New DataTable("DL")
        Dim strIngressoTipo() As String

        If Not m_bs.DataSource Is Nothing Then
            If m_bs.DataSource.Rows.Count > 0 Then

                m_strChartEnergiaProdotta = GetEnergia(ComboBox_IDP_ID.SelectedValue(), ComboBox_LI_ID.SelectedValue(), False, 152, 153, DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()

                ' Calcolo potenza media Ultimo UDT
                Dim drPM_1 As DataRow()
                Dim drPM_2 As DataRow()
                Dim dblPM As Double
                Dim dblPM_1 As Double
                Dim dblPM_2 As Double
                Dim iValoreUDTMinutiPM As Integer
                dblPM = 0.0
                dblPM_1 = 0.0
                dblPM_2 = 0.0
                Try
                    drPM_1 = m_bs.DataSource.Select("IT_ID = 152", "LIIDPV_DataOra DESC")
                    drPM_2 = m_bs.DataSource.Select("IT_ID = 153", "LIIDPV_DataOra DESC")
                    iValoreUDTMinutiPM = GetLIPCLPCVALMEMODB(Me.ComboBox_LI_ID.SelectedValue(), 125)

                    If drPM_1.Count > 0 Then
                        dblPM_1 = CDbl(drPM_1.GetValue(0).Item("LIIDPV_Valore"))
                    End If
                    If drPM_2.Count > 0 Then
                        dblPM_2 = CDbl(drPM_2.GetValue(0).Item("LIIDPV_Valore"))
                    End If
                    If iValoreUDTMinutiPM > 0 Then
                        dblPM = ((dblPM_1 + dblPM_2) * (60.0 / CDbl(iValoreUDTMinutiPM)))
                    End If
                    m_strChartPotenzaMedia = Math.Round(dblPM, 1).ToString()

                    For Each dr As DataRow In m_bs.DataSource.Rows
                        If dr.Item(1) >= 101 And dr.Item(1) <= 196 Then
                            If dtVisGraf.Columns.Contains(dr.Item(2) + "-" + dr.Item(4)) = False Then
                                dcTemp = dtVisGraf.Columns.Add(dr.Item(2) + "-" + dr.Item(4))
                                dcTemp.DefaultValue = 0
                            End If
                        End If
                    Next

                    ' Data ed ora
                    dtVisGraf.Columns.Add("DataOra")
                    iIndice_1 = 0
                    strIngressoTipo = Array.CreateInstance(GetType(String), dtVisGraf.Columns.Count)
                    For Each dr As DataRow In m_bs.DataSource.Rows
                        If strIngressoTipo.Contains(dr.Item(1).ToString()) = True Then
                            Array.Clear(strIngressoTipo, 0, strIngressoTipo.Length)
                            iIndice_1 = 0
                        End If
                        If iIndice_1 = 0 Then
                            drTemp = dtVisGraf.Rows.Add()
                            drTemp.Item("DataOra") = dr.Item(5)
                        End If

                        If drTemp.Table.Columns.Contains(dr.Item(2) + "-" + dr.Item(4)) = True Then
                            drTemp.Item(dr.Item(2) + "-" + dr.Item(4)) = dr.Item(3)
                        End If

                        strIngressoTipo(iIndice_1) = dr.Item(1).ToString()
                        iIndice_1 = iIndice_1 + 1

                    Next

                    m_dtVisGraf = dtVisGraf

                Catch ex As Exception
                    dblPM = 0.0
                    dblPM_1 = 0.0
                    dblPM_2 = 0.0
                    m_strChartPotenzaMedia = ""
                End Try

            End If
        End If

        If Not VisInFormatoGrafico Is Nothing Then
            VisInFormatoGrafico.ChartEnergiaProdotta = m_strChartEnergiaProdotta
            VisInFormatoGrafico.ChartPotenzaMedia = m_strChartPotenzaMedia
            VisInFormatoGrafico.UpdateGraph(m_dtVisGraf)
        End If

    End Sub

    Private Sub CaricaCID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
                strSQL = " SELECT C_ID, C_Nome "
                strSQL = strSQL + " FROM Cliente "
            Else
                strSQL = " SELECT C_ID, C_Nome "
                strSQL = strSQL + " FROM Cliente "
                strSQL = strSQL + " WHERE C_Codice = '" + My.Settings.CodiceCliente.ToString() + "'"
            End If

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_C_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "C_Nome"
                        .ValueMember = "C_ID"
                        If .Items.Count > 0 Then
                            .SelectedIndex = 0
                            m_iCID = .SelectedValue()
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    Private Sub CaricaIDPID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT IDP_ID, IDP_Nome "
            strSQL = strSQL + " FROM ImpiantoDiProduzione "
            strSQL = strSQL + " WHERE IDP_C_ID = " + Me.m_iCID.ToString()

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_IDP_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "IDP_Nome"
                        .ValueMember = "IDP_ID"
                        If .Items.Count > 0 Then
                            .SelectedIndex = 0
                            m_iIDPID = .SelectedValue()
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    Private Sub CaricaLIID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT LI_ID, LI_Nr "
            strSQL = strSQL + " FROM LoggerInst "
            If Me.ComboBox_IDP_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE LI_IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE LI_IDP_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_LI_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "LI_Nr"
                        .ValueMember = "LI_ID"
                        If .Items.Count > 0 Then
                            .SelectedIndex = 0
                            m_iLIID = .SelectedValue()
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    Public Sub VisualizzaDatiInverterTester()
        Dim p As Point

        VisDatiInverterTester.Close()
        VisDatiInverterTester.ChartTitleText = m_strChartTitleText
        VisDatiInverterTester.CID = m_iCID
        VisDatiInverterTester.IDPID = m_iIDPID
        VisDatiInverterTester.LIID = m_iLIID
        VisDatiInverterTester.DT_START = DateTimePicker_START.Value
        VisDatiInverterTester.DT_STOP = DateTimePicker_STOP.Value
        VisDatiInverterTester.Position = p
        VisDatiInverterTester.MdiParent = Me.MdiParent
        VisDatiInverterTester.Show()

    End Sub

    Public Sub VisualizzaDatiStringTester()
        Dim p As Point

        VisDatiStringTester.Close()
        VisDatiStringTester.ChartTitleText = m_strChartTitleText
        VisDatiStringTester.CID = m_iCID
        VisDatiStringTester.IDPID = m_iIDPID
        VisDatiStringTester.LIID = m_iLIID
        VisDatiStringTester.DT_START = DateTimePicker_START.Value
        VisDatiStringTester.DT_STOP = DateTimePicker_STOP.Value
        VisDatiStringTester.Position = p
        VisDatiStringTester.MdiParent = Me.MdiParent
        VisDatiStringTester.Show()

    End Sub

    Private Sub Button_VisGraf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_VisGraf.Click
        Dim p As Point
        Dim ts As TimeSpan
        ts = DateTimePicker_STOP.Value - DateTimePicker_START.Value

        If ts.TotalHours > 48 Then
            System.Windows.Forms.MessageBox.Show(Owner, "Attenzione, non e' possibile impostare una differenza di date superiore a 48 ore per i dati del Datalogger. Per intervalli superiori e' possibile consultare solo i dati statistici.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else

            ' Aggiorno
            CaricaDatiGrafico()

            VisInFormatoGrafico.Close()
            VisInFormatoGrafico.Table = m_dtVisGraf
            VisInFormatoGrafico.ChartTitleText = m_strChartTitleText
            VisInFormatoGrafico.ChartEnergiaProdotta = m_strChartEnergiaProdotta
            VisInFormatoGrafico.ChartPotenzaMedia = m_strChartPotenzaMedia
            VisInFormatoGrafico.CID = m_iCID
            VisInFormatoGrafico.IDPID = m_iIDPID
            VisInFormatoGrafico.LIID = m_iLIID
            VisInFormatoGrafico.DT_START = DateTimePicker_START.Value
            VisInFormatoGrafico.DT_STOP = DateTimePicker_STOP.Value
            VisInFormatoGrafico.Position = p
            VisInFormatoGrafico.MdiParent = Me.MdiParent
            VisInFormatoGrafico.Show()
        End If
    End Sub

    Private Sub Timer_1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_1.Tick
        Timer_1.Stop()
        If Me.CheckBoxAbilitaAggiornamento.Checked = True Then

            Dim dt As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
            DateTimePicker_START.Value = dt
            DateTimePicker_STOP.Value = dt.Add(TimeSpan.Parse("23:59:59"))

            CaricaDati(False)
            CaricaDatiGrafico()

        End If
        Timer_1.Start()
    End Sub

    Private Sub Button_ST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ST.Click
        Dim ts As TimeSpan
        ts = DateTimePicker_STOP.Value - DateTimePicker_START.Value

        If ts.TotalHours > 48 Then
            System.Windows.Forms.MessageBox.Show(Owner, "Attenzione, non e' possibile impostare una differenza di date superiore a 48 ore per i dati del Datalogger. Per intervalli superiori e' possibile consultare solo i dati statistici.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            VisualizzaDatiStringTester()
        End If
    End Sub

    Private Sub Button_IT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_IT.Click
        Dim ts As TimeSpan
        ts = DateTimePicker_STOP.Value - DateTimePicker_START.Value

        If ts.TotalHours > 48 Then
            System.Windows.Forms.MessageBox.Show(Owner, "Attenzione, non e' possibile impostare una differenza di date superiore a 48 ore per i dati del Datalogger. Per intervalli superiori e' possibile consultare solo i dati statistici.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            VisualizzaDatiInverterTester()
        End If
    End Sub

    Private Sub Button_DL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DL.Click
        Dim ts As TimeSpan
        ts = DateTimePicker_STOP.Value - DateTimePicker_START.Value

        If ts.TotalHours > 48 Then
            System.Windows.Forms.MessageBox.Show(Owner, "Attenzione, non e' possibile impostare una differenza di date superiore a 48 ore per i dati del Datalogger. Per intervalli superiori e' possibile consultare solo i dati statistici.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            CaricaDati(False)
        End If
    End Sub

    Private Sub Button_DatiStatistici_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DatiStatistici.Click
        CaricaDatiStatistici()
    End Sub

    Private Sub Button_DatiDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DatiDL.Click
        CaricaDatiDL()
    End Sub
    Private Sub Button_Report_Riass_Giorni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Report_Riass_Giorni.Click
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            strSQL = " SELECT LoggerInstStatMemoDay.LISM_CDP_1, LoggerInstStatMemoDay.LISM_CDP_2, LoggerInstStatMemoDay.LISM_Data "
            strSQL = strSQL + " FROM LoggerInstStatMemoDay "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInstStatMemoDay.LISM_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE LoggerInst.LI_ID = " + Me.ComboBox_LI_ID.SelectedValue().ToString + " "
            strSQL = strSQL + " AND (LoggerInstStatMemoDay.LISM_Data >= CONVERT(DATETIME, @LISM_DataOra_Start, 105) AND LoggerInstStatMemoDay.LISM_Data <= CONVERT(DATETIME, @LISM_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LISM_DataOra_Start", DateTimePicker_START.Value)
            cmd.Parameters.AddWithValue("@LISM_DataOra_Stop", DateTimePicker_STOP.Value)

            da.SelectCommand = cmd
            da.Fill(ds)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    For Each dc As DataColumn In ds.Tables(0).Columns
                        ' Inserisco le caption
                        strSQL = "SELECT DCC_ColonnaNome FROM [DatabaseCampoColonna] WHERE DCC_CampoNome = @DCC_CampoNome "
                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@DCC_CampoNome", dc.ColumnName.ToString)

                        Dim rdr As SqlDataReader = cmd.ExecuteReader()
                        If rdr.HasRows = True Then
                            rdr.Read()
                            dc.Caption = rdr.Item(0).ToString
                        End If
                        rdr.Close()
                    Next dc

                    m_bs.DataSource = ds.Tables(0)
                    MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
                    MyBase.BS = m_bs
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    Private Sub Button_Report_Riass_Mesi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Report_Riass_Mesi.Click
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim dr As DataRow

        Dim dbl_CDC As Double
        Dim dbl_CDP_1 As Double
        Dim dbl_CDP_2 As Double
        Dim dbl_SI As Double
        Dim dbl_STR As Double
        Dim dbl_INV As Double
        Dim dblPR As Double
        Dim dblHG As Double
        Dim dblMS As Double
        Dim dblMT As Double
        Dim dblS As Double

        Dim dtStart As Date
        Dim dtStop As Date

        Try
            Date.TryParse("01/" + DateTimePicker_START.Value.Month.ToString() + "/" + DateTimePicker_START.Value.Year.ToString() + " 00:00:00", dtStart)
            Date.TryParse(Date.DaysInMonth(DateTimePicker_START.Value.Year, DateTimePicker_START.Value.Month).ToString() + "/" + DateTimePicker_START.Value.Month.ToString() + "/" + DateTimePicker_START.Value.Year.ToString() + " 00:00:00", dtStop)

            ds.Tables.Add()
            'ds.Tables(0).Columns.Add("N Energia Da Contatore di Cessione Kwh")
            'ds.Tables(0).Columns("N Energia Da Contatore di Cessione Kwh").Caption = "Energia Da Contatore di Cessione Kwh"
            ds.Tables(0).Columns.Add("N Energia Da Contatore di Produzione 1 Kwh")
            ds.Tables(0).Columns("N Energia Da Contatore di Produzione 1 Kwh").Caption = "Energia Da Contatore di Produzione 1 Kwh"
            ds.Tables(0).Columns.Add("N Energia Da Contatore di Produzione 2 Kwh")
            ds.Tables(0).Columns("N Energia Da Contatore di Produzione 2 Kwh").Caption = "Energia Da Contatore di Produzione 2 Kwh"
            'ds.Tables(0).Columns.Add("N Energia Da Sensori Di Irraggiamento Kwh")
            'ds.Tables(0).Columns("N Energia Da Sensori Di Irraggiamento Kwh").Caption = "Energia Da Sensori Di Irraggiamento Kwh"
            'ds.Tables(0).Columns.Add("N Energia Da Stringhe Kwh")
            'ds.Tables(0).Columns("N Energia Da Stringhe Kwh").Caption = "Energia Da Stringhe Kwh"
            'ds.Tables(0).Columns.Add("N Energia Da Inverter Kwh")
            'ds.Tables(0).Columns("N Energia Da Inverter Kwh").Caption = "Energia Da Inverter Kwh"
            'ds.Tables(0).Columns.Add("N Performance Ratio (%)")
            'ds.Tables(0).Columns("N Performance Ratio (%)").Caption = "Performance Ratio (%)"
            'ds.Tables(0).Columns.Add("N HG (Kwh/mq)")
            'ds.Tables(0).Columns("N HG (Kwh/mq)").Caption = "N HG (Kwh/mq)"
            'ds.Tables(0).Columns.Add("N Media Solarimetri(W/mq)")
            'ds.Tables(0).Columns("N Media Solarimetri(W/mq)").Caption = "Media Solarimetri(W/mq)"
            'ds.Tables(0).Columns.Add("N Media Temp. Pann.(°C)")
            'ds.Tables(0).Columns("N Media Temp. Pann.(°C)").Caption = "Media Temp. Pann.(°C)"
            'ds.Tables(0).Columns.Add("N Soglia W/mq Calcolo Medie")
            'ds.Tables(0).Columns("N Soglia W/mq Calcolo Medie").Caption = "Soglia W/mq Calcolo Medie"
            ds.Tables(0).Columns.Add("N Data")
            ds.Tables(0).Columns("N Data").Caption = "Data"

            While (dtStart < DateTimePicker_STOP.Value)

                GetStatMemoDay(Me.ComboBox_LI_ID.SelectedValue(), dbl_CDC, dbl_CDP_1, dbl_CDP_2, dbl_SI, dbl_STR, dbl_INV, dblPR, dblHG, dblMS, dblMT, dblS, dtStart, dtStop)

                dr = ds.Tables(0).Rows.Add()
                'dr.Item("N Energia Da Contatore di Cessione Kwh") = Math.Round((dbl_CDC), 1)
                dr.Item("N Energia Da Contatore di Produzione 1 Kwh") = Math.Round((dbl_CDP_1), 1)
                dr.Item("N Energia Da Contatore di Produzione 2 Kwh") = Math.Round((dbl_CDP_2), 1)
                'dr.Item("N Energia Da Sensori Di Irraggiamento Kwh") = Math.Round(dbl_SI, 1)
                'dr.Item("N Energia Da Stringhe Kwh") = Math.Round(dbl_STR, 1)
                'dr.Item("N Energia Da Inverter Kwh") = Math.Round(dbl_INV, 1)
                'dr.Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                'dr.Item("N HG (Kwh/mq)") = Math.Round(dblHG, 1)
                'dr.Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
                'dr.Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
                'dr.Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblS, 1)
                dr.Item("N Data") = dtStart.Month.ToString() + "/" + dtStart.Year.ToString()

                dtStart = dtStart.AddMonths(1)
                dtStop = dtStop.AddMonths(1)

            End While

            m_bs.DataSource = ds.Tables(0)
            MyBase.ReportHeader = Me.ComboBox_C_ID.Text() + " - " + Me.ComboBox_IDP_ID.Text() + " - Nr DL: " + Me.ComboBox_LI_ID.Text()
            MyBase.BS = m_bs

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    'Private Sub DateTimePicker_START_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_START.ValueChanged
    '    Dim td As TimeSpan = TimeSpan.FromDays(31) + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59)

    '    If DateTimePicker_STOP.Value.Subtract(DateTimePicker_START.Value) > td Then
    '        DateTimePicker_STOP.Value = DateTimePicker_START.Value.Subtract(-td)
    '    End If
    'End Sub

    'Private Sub DateTimePicker_STOP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_STOP.ValueChanged
    '    Dim td As TimeSpan = TimeSpan.FromDays(31) + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59)

    '    If DateTimePicker_STOP.Value.Subtract(DateTimePicker_START.Value) > td Then
    '        DateTimePicker_START.Value = DateTimePicker_STOP.Value.Subtract(td)
    '    End If
    'End Sub

End Class
