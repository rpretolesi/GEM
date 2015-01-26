Imports System.Management
Imports System.Data.SqlClient

Public Class VisDatiStringTester
    Dim m_bs As New BindingSource
    Private m_bAbilitaAggiornamentoAutomatico As Boolean

    Private m_pPosition As Point

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

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
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

        CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, 0, False)

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

        Me.TextBox_STI_Indirizzo_Modbus.Enabled = False

    End Sub

    Private Sub CaricaDati(ByVal bSoloAllarmiTuttiGliStringTester As Boolean, ByVal iSTIIndirizzoModbus As Integer, ByVal bFiltraPerSTIIndirizzoModbus As Boolean)

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            If bSoloAllarmiTuttiGliStringTester = True Then
                strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, Cliente.C_Societa, ImpiantoDiProduzione.IDP_ID, ImpiantoDiProduzione.IDP_Nome, PannelloFotovString.PFS_Nr, StringTesterInst.STI_Indirizzo_Modbus, IngressoTipo.IT_Nome, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_Valore, IngressoTipo.IT_UM, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_UDT, StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_Descrizione "
            Else
                strSQL = " SELECT PannelloFotovString.PFS_Nr, StringTesterInst.STI_Indirizzo_Modbus, IngressoTipo.IT_Nome, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_Valore, IngressoTipo.IT_UM, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra, StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_UDT, StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_Descrizione "
            End If
            strSQL = strSQL + " FROM StringTesterInst_X_PannelloFotovString_X_Valore "
            strSQL = strSQL + " INNER JOIN StringTesterInst_X_PannelloFotovString_X_Config ON StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_STIPFSC_ID = dbo.StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_ID "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_STI_ID = dbo.StringTesterInst.STI_ID "
            strSQL = strSQL + " INNER JOIN Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = dbo.ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = dbo.LoggerInst.LI_IDP_ID ON dbo.StringTesterInst.STI_LI_ID = dbo.LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID = dbo.IngressoTipo.IT_ID "
            strSQL = strSQL + " INNER JOIN PannelloFotovString ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_PFS_ID = dbo.PannelloFotovString.PFS_ID "
            If bSoloAllarmiTuttiGliStringTester = True Then
                strSQL = strSQL + " WHERE IngressoTipo.IT_ID >= 211 AND IngressoTipo.IT_ID <= 399 "
                strSQL = strSQL + " AND (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
            Else
                strSQL = strSQL + " WHERE IDP_ID = " + m_iIDPID.ToString + " AND LI_ID = " + m_iLIID.ToString + " "
                strSQL = strSQL + " AND (StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra >= CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra <= CONVERT(DATETIME, @STIPFSV_DataOra_Stop, 105)) "
            End If

            If bFiltraPerSTIIndirizzoModbus = True Then
                strSQL = strSQL + " AND (StringTesterInst.STI_Indirizzo_Modbus = " + iSTIIndirizzoModbus.ToString() + ") "
            End If

            strSQL = strSQL + " ORDER BY StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra DESC, StringTesterInst.STI_Indirizzo_Modbus, IngressoTipo.IT_ID  "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
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
            Dim iSTIIndirizzoModbus As Integer
            Try
                iSTIIndirizzoModbus = CInt(Me.TextBox_STI_Indirizzo_Modbus.Text)
                CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, iSTIIndirizzoModbus, Me.CheckBox_FiltraPer_STI_Indirizzo_Modbus.Checked)
            Catch ex As Exception
                CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, 0, False)
            End Try
        End If
        Timer_1.Start()
    End Sub

    Private Sub CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.CheckedChanged
        Dim iSTIIndirizzoModbus As Integer
        Try
            iSTIIndirizzoModbus = CInt(Me.TextBox_STI_Indirizzo_Modbus.Text)
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, iSTIIndirizzoModbus, Me.CheckBox_FiltraPer_STI_Indirizzo_Modbus.Checked)
        Catch ex As Exception
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, 0, False)
        End Try
    End Sub

    Private Sub TextBox_STI_Indirizzo_Modbus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_STI_Indirizzo_Modbus.TextChanged
        Dim iSTIIndirizzoModbus As Integer
        Try
            iSTIIndirizzoModbus = CInt(Me.TextBox_STI_Indirizzo_Modbus.Text)
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, iSTIIndirizzoModbus, Me.CheckBox_FiltraPer_STI_Indirizzo_Modbus.Checked)
        Catch ex As Exception
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, 0, False)
        End Try
    End Sub

    Private Sub CheckBox_FiltraPer_STI_Indirizzo_Modbus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_FiltraPer_STI_Indirizzo_Modbus.CheckedChanged
        Dim iSTIIndirizzoModbus As Integer
        Me.TextBox_STI_Indirizzo_Modbus.Enabled = Me.CheckBox_FiltraPer_STI_Indirizzo_Modbus.Checked
        Try
            iSTIIndirizzoModbus = CInt(Me.TextBox_STI_Indirizzo_Modbus.Text)
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, iSTIIndirizzoModbus, Me.CheckBox_FiltraPer_STI_Indirizzo_Modbus.Checked)
        Catch ex As Exception
            CaricaDati(Me.CheckBoxVisualizzaSoloGliAllarmiDiTuttiGliStringTesterConfiguratiDiTuttiGliImpiantiDiTuttiIClienti.Checked, 0, False)
        End Try
    End Sub

End Class
