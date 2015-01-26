Imports System.Management
Imports System.Data.SqlClient

Public Class VisDatiInverterTester
    Dim m_bs As New BindingSource
    Dim m_dtVisGraf As New DataTable

    Private m_bAbilitaAggiornamentoAutomatico As Boolean

    Private m_pPosition As Point

    Private m_strChartTitleText As String = ""
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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Visualizzazione Dati Inverter Tester"
        Me.DesktopLocation = m_pPosition

        CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

    End Sub

    Private Sub CaricaDati(ByVal bSoloValoriDiCorrenteUscitaInverterECellaCampione As Boolean)

        Dim b_CGEMClientVisAll As Boolean

        ' Opzioni GEM Client
        If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
            b_CGEMClientVisAll = True
        Else
            b_CGEMClientVisAll = GENERICA_DESCRIZIONE("C_GEMClient_Vis_All", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
        End If

        Dim dtVisGraf As New DataTable("IT")

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT ContatoreDiProduzioneInst.CDPI_Nr, InverterFotovInst.IFI_ID, InverterFotovInst.IFI_Nr, InverterTesterInst.ITI_Indirizzo_Modbus, IngressoTipo.IT_ID, IngressoTipo.IT_Nome, InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_Valore, IngressoTipo.IT_UM, InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra "
            strSQL = strSQL + " FROM InverterTesterInst_X_InverterFotovInst_X_Config "
            strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
            strSQL = strSQL + " INNER JOIN InverterFotovInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IFI_ID = InverterFotovInst.IFI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON InverterTesterInst.ITI_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            strSQL = strSQL + " INNER JOIN ContatoreDiProduzioneInst ON InverterFotovInst.IFI_CDPI_ID = ContatoreDiProduzioneInst.CDPI_ID AND ImpiantoDiProduzione.IDP_ID = ContatoreDiProduzioneInst.CDPI_IDP_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
            If bSoloValoriDiCorrenteUscitaInverterECellaCampione = True Then
                If b_CGEMClientVisAll = True Then
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
                Else
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND (IngressoTipo.IT_ID = 413 OR IngressoTipo.IT_ID = 415 OR (IngressoTipo.IT_ID >= 471 AND IngressoTipo.IT_ID <= 473) OR (IngressoTipo.IT_ID >= 531 AND IngressoTipo.IT_ID <= 536) OR (IngressoTipo.IT_ID >= 591 AND IngressoTipo.IT_ID <= 598)) "
                    strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
                End If
            Else
                If b_CGEMClientVisAll = True Then
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
                Else
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND ((IngressoTipo.IT_ID >= 411 AND IngressoTipo.IT_ID <= 418) OR (IngressoTipo.IT_ID >= 471 AND IngressoTipo.IT_ID <= 478) OR (IngressoTipo.IT_ID >= 531 AND IngressoTipo.IT_ID <= 538) OR (IngressoTipo.IT_ID >= 591 AND IngressoTipo.IT_ID <= 598)) "
                    strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
                End If
            End If
            strSQL = strSQL + " ORDER BY InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra DESC, ContatoreDiProduzioneInst.CDPI_Nr, InverterFotovInst.IFI_Nr, InverterTesterInst.ITI_Indirizzo_Modbus, IngressoTipo.IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", m_dtSTART)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", m_dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    ' Aggiungo la colonna descrizione
                    ds.Tables(0).Columns.Add("LIPC_Descrizione")
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If dr.Item("IT_ID") = 538 Then
                            dr.Item("LIPC_Descrizione") = GetLIPCLPCDESCRMEMODB(m_iLIID, 85)
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
                    MyBase.ReportHeader = GENERICA_DESCRIZIONE("C_Nome", "Cliente", "C_ID", m_iCID, DEFAULT_OPERATOR_ID).ToString() + " - " + GENERICA_DESCRIZIONE("IDP_Nome", "ImpiantoDiProduzione", "IDP_ID", m_iIDPID, DEFAULT_OPERATOR_ID).ToString() + " - Nr DL: " + GENERICA_DESCRIZIONE("LI_Nr", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID).ToString()
                    MyBase.BS = m_bs

                    ' Creo la base per la visualizzazione grafica
                    Dim iIndice_1 As Integer
                    Dim drTemp As DataRow
                    Dim iTemp As Integer
                    Dim dcTemp As DataColumn
                    Dim strIngressoTipo() As String

                    If Not ds Is Nothing Then
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then

                                For Each dr As DataRow In ds.Tables(0).Rows
                                    iTemp = CInt(dr.Item(4))
                                    If iTemp >= 411 And iTemp <= 636 Then
                                        If dtVisGraf.Columns.Contains(dr.Item(0).ToString + "-" + dr.Item(2).ToString + "-" + dr.Item(3).ToString + "-" + dr.Item(5) + "-" + dr.Item(7)) = False Then
                                            dcTemp = dtVisGraf.Columns.Add(dr.Item(0).ToString + "-" + dr.Item(2).ToString + "-" + dr.Item(3).ToString + "-" + dr.Item(5) + "-" + dr.Item(7))
                                            dcTemp.DefaultValue = 0
                                        End If
                                    End If
                                Next
                                ' Data ed ora
                                dtVisGraf.Columns.Add("DataOra")
                                Try

                                    iIndice_1 = 0
                                    strIngressoTipo = Array.CreateInstance(GetType(String), dtVisGraf.Columns.Count)
                                    For Each dr As DataRow In ds.Tables(0).Rows
                                        If strIngressoTipo.Contains(dr.Item(0).ToString() + dr.Item(1).ToString() + dr.Item(2).ToString() + dr.Item(3).ToString() + dr.Item(4).ToString()) = True Then
                                            Array.Clear(strIngressoTipo, 0, strIngressoTipo.Length)
                                            iIndice_1 = 0
                                        End If
                                        If iIndice_1 = 0 Then
                                            drTemp = dtVisGraf.Rows.Add()
                                            drTemp.Item("DataOra") = dr.Item(8)
                                        End If

                                        If drTemp.Table.Columns.Contains(dr.Item(0).ToString + "-" + dr.Item(2).ToString + "-" + dr.Item(3).ToString + "-" + dr.Item(5) + "-" + dr.Item(7)) = True Then
                                            drTemp.Item(dr.Item(0).ToString + "-" + dr.Item(2).ToString + "-" + dr.Item(3).ToString + "-" + dr.Item(5) + "-" + dr.Item(7)) = dr.Item(6)
                                        End If

                                        strIngressoTipo(iIndice_1) = dr.Item(0).ToString() + dr.Item(1).ToString() + dr.Item(2).ToString() + dr.Item(3).ToString() + dr.Item(4).ToString()
                                        iIndice_1 = iIndice_1 + 1

                                    Next
                                Catch ex As Exception

                                End Try

                            End If
                        End If
                    End If
                    m_dtVisGraf = dtVisGraf
                    If Not VisInFormatoGrafico Is Nothing Then
                        VisInFormatoGrafico.UpdateGraph(m_dtVisGraf)
                    End If
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

    Private Sub Button_VisGraf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_VisGraf.Click
        Dim p As Point

        ' Aggiorno
        CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)

        VisInFormatoGrafico.Close()
        VisInFormatoGrafico.Table = m_dtVisGraf
        VisInFormatoGrafico.ChartTitleText = m_strChartTitleText
        VisInFormatoGrafico.ChartEnergiaProdotta = ""
        VisInFormatoGrafico.ChartPotenzaMedia = ""
        VisInFormatoGrafico.CID = m_iCID
        VisInFormatoGrafico.IDPID = m_iIDPID
        VisInFormatoGrafico.LIID = m_iLIID
        VisInFormatoGrafico.DT_START = m_dtSTART
        VisInFormatoGrafico.DT_STOP = m_dtSTOP
        VisInFormatoGrafico.Position = p
        VisInFormatoGrafico.MdiParent = Me.MdiParent
        VisInFormatoGrafico.Show()
    End Sub

    Private Sub CheckBoxAbilitaAggiornamento_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAbilitaAggiornamento.CheckedChanged
        Dim cb As CheckBox
        cb = sender
        If cb.CheckState = CheckState.Checked Then
            m_bAbilitaAggiornamentoAutomatico = True
        Else
            m_bAbilitaAggiornamentoAutomatico = False
        End If
    End Sub

    Private Sub Timer_1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_1.Tick
        Timer_1.Stop()
        If m_bAbilitaAggiornamentoAutomatico = True Then
            CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)
        End If
        Timer_1.Start()
    End Sub

    Private Sub CheckBoxVisualizzaSoloGliAllarmi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)
    End Sub

    Private Sub CheckBoxTuttiGliInverterTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)
    End Sub

    Private Sub CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.CheckedChanged
        CaricaDati(CheckBox_Ordina_Per_Nr_Inverter_e_Correnti_Uscita_e_Cella_Campione.Checked)
    End Sub
End Class
