Imports System.Management
Imports System.Data.SqlClient

Public Class VisDatiStringTester
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

        Me.Text = "Visualizzazione Dati String Tester"
        Me.DesktopLocation = m_pPosition

        CaricaDati(False, False)

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

    End Sub

    Private Sub CaricaDati(ByVal bSoloAllarmi As Boolean, ByVal bTuttiGliStringTester As Boolean)

        Dim b_CGEMClientVisAll As Boolean

        ' Opzioni GEM Client
        If My.Settings.CodiceCliente.ToString() = "acb16f40-4259-4a9a-9c07-a77633fe2645" Then
            b_CGEMClientVisAll = True
        Else
            b_CGEMClientVisAll = GENERICA_DESCRIZIONE("C_GEMClient_Vis_All", "Cliente", "C_ID", Me.m_iCID.ToString, DEFAULT_OPERATOR_ID)
        End If

        Dim dtVisGraf As New DataTable("ST")

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT PannelloFotovString.PFS_Nr, StringTesterInst.STI_Indirizzo_Modbus, IngressoTipo.IT_ID, IngressoTipo.IT_Nome, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_Valore, IngressoTipo.IT_UM, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra "
            strSQL = strSQL + " FROM StringTesterInst_X_PannelloFotovString_X_Valore "
            strSQL = strSQL + " INNER JOIN StringTesterInst_X_PannelloFotovString_X_Config ON StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_STIPFSC_ID = dbo.StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_ID "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_STI_ID = dbo.StringTesterInst.STI_ID "
            strSQL = strSQL + " INNER JOIN Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = dbo.ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = dbo.LoggerInst.LI_IDP_ID ON dbo.StringTesterInst.STI_LI_ID = dbo.LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID = dbo.IngressoTipo.IT_ID "
            strSQL = strSQL + " INNER JOIN PannelloFotovString ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_PFS_ID = dbo.PannelloFotovString.PFS_ID "
            If bTuttiGliStringTester = True Then
                If b_CGEMClientVisAll = True Then
                    strSQL = strSQL + " WHERE (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
                Else
                    strSQL = strSQL + " WHERE IngressoTipo.IT_ID >= 201 AND IngressoTipo.IT_ID <= 208 "
                    strSQL = strSQL + " AND (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
                End If
            Else
                If b_CGEMClientVisAll = True Then
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
                Else
                    strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                    strSQL = strSQL + " AND IngressoTipo.IT_ID >= 201 AND IngressoTipo.IT_ID <= 208 "
                    strSQL = strSQL + " AND (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
                End If
            End If
            strSQL = strSQL + " ORDER BY StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra DESC, StringTesterInst.STI_Indirizzo_Modbus, IngressoTipo.IT_ID  "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STIPFSV_DataOra_Start", m_dtSTART)
            cmd.Parameters.AddWithValue("@STIPFSV_DataOra_Stop", m_dtSTOP)

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
                    MyBase.ReportHeader = GENERICA_DESCRIZIONE("C_Nome", "Cliente", "C_ID", m_iCID, DEFAULT_OPERATOR_ID).ToString() + " - " + GENERICA_DESCRIZIONE("IDP_Nome", "ImpiantoDiProduzione", "IDP_ID", m_iIDPID, DEFAULT_OPERATOR_ID).ToString() + " - Nr DL: " + GENERICA_DESCRIZIONE("LI_Nr", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID).ToString()
                    MyBase.BS = m_bs

                    ' Creo la base per la visualizzazione grafica
                    Dim iIndice_1 As Integer
                    Dim drTemp As DataRow
                    Dim dcTemp As DataColumn
                    Dim strIngressoTipo() As String

                    If Not ds Is Nothing Then
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then

                                For Each dr As DataRow In ds.Tables(0).Rows
                                    If dr.Item(2) >= 201 And dr.Item(2) <= 396 Then
                                        If dtVisGraf.Columns.Contains(dr.Item(1).ToString + "-" + dr.Item(3) + "-" + dr.Item(5)) = False Then
                                            dcTemp = dtVisGraf.Columns.Add(dr.Item(1).ToString + "-" + dr.Item(3) + "-" + dr.Item(5))
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
                                        If strIngressoTipo.Contains(dr.Item(1).ToString() + dr.Item(2).ToString()) = True Then
                                            Array.Clear(strIngressoTipo, 0, strIngressoTipo.Length)
                                            iIndice_1 = 0
                                        End If
                                        If iIndice_1 = 0 Then
                                            drTemp = dtVisGraf.Rows.Add()
                                            drTemp.Item("DataOra") = dr.Item(6)
                                        End If

                                        If drTemp.Table.Columns.Contains(dr.Item(1).ToString + "-" + dr.Item(3) + "-" + dr.Item(5)) = True Then
                                            drTemp.Item(dr.Item(1).ToString + "-" + dr.Item(3) + "-" + dr.Item(5)) = dr.Item(4)
                                        End If

                                        strIngressoTipo(iIndice_1) = dr.Item(1).ToString() + dr.Item(2).ToString()
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
        CaricaDati(False, False)

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
            CaricaDati(False, False)
        End If
        Timer_1.Start()
    End Sub

    Private Sub CheckBoxVisualizzaSoloGliAllarmi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CaricaDati(False, False)
    End Sub

    Private Sub CheckBoxTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CaricaDati(False, False)
    End Sub
End Class
