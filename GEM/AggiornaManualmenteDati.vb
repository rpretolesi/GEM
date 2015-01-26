Imports System.Data.SqlClient

Public Class AggiornaManualmenteDati
    Dim m_bs As New BindingSource

    Private m_ltcpipClient As List(Of TCPIPClient)

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Property TCPIPCL() As List(Of TCPIPClient)
        Get
            Return m_ltcpipClient
        End Get

        Set(ByVal TCPIPCL As List(Of TCPIPClient))
            m_ltcpipClient = TCPIPCL
        End Set
    End Property

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Aggiorna Manualmente Dati"

        CaricaCID()

        CaricaIDPID()

        CaricaLIID()

        CaricaDati()

        CaricaTCPIP_Addr()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

        ' Verifico e imposto i check box
        CaricaCheckBox()

        TimerTCPIP.Start()

    End Sub

    Private Sub ComboBox_C_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_C_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaIDPID()
            If ComboBox_IDP_ID.Items.Count = 0 Then
                CaricaDati()
            End If
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaLIID()
            If ComboBox_LI_ID.Items.Count = 0 Then
                CaricaDati()
            End If
        End If

    End Sub

    Private Sub ComboBox_LI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaDati()
        End If

    End Sub

    Protected Overrides Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If DataGridView.SelectedRows.Count > 0 Then
                If DataGridView.SelectedRows(0).Index >= 0 Then
                    If Not m_bs.DataSource Is Nothing Then
                        If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                            If Not DataGridView.SelectedRows(0).Cells("C_Nome").Value Is DBNull.Value Then
                                Me.ComboBox_C_ID.Text = DataGridView.SelectedRows(0).Cells("C_Nome").Value
                            End If
                            If Not DataGridView.SelectedRows(0).Cells("IDP_Nome").Value Is DBNull.Value Then
                                Me.ComboBox_IDP_ID.Text = DataGridView.SelectedRows(0).Cells("IDP_Nome").Value
                            End If
                            If Not DataGridView.SelectedRows(0).Cells("LI_Nr").Value Is DBNull.Value Then
                                Me.ComboBox_LI_ID.Text = DataGridView.SelectedRows(0).Cells("LI_Nr").Value
                            End If

                            ' Verifico e imposto i check box
                            CaricaCheckBox()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
        End Try

        MyBase.DataGridView_SelectionChanged(sender, e)

    End Sub

    Protected Overrides Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        'If (Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Count > 0) Then
        '    Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Clear()
        'End If

        TimerTCPIP.Stop()

        MyBase.BaseForm_1_FormClosed(sender, e)

    End Sub

    Private Sub CaricaDati()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim iIndice_1 As Integer

        Try

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LoggerInst.*, U_NomeCognome "
            strSQL = strSQL + " FROM LoggerInst INNER JOIN Utente ON LI_U_ID = U_ID INNER JOIN ImpiantoDiProduzione ON LI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "

            If ComboBox_C_ID.Items.Count > 0 Or ComboBox_IDP_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE "
            End If

            If ComboBox_C_ID.Items.Count > 0 Then
                strSQL = strSQL + " C_ID = " + ComboBox_C_ID.SelectedValue().ToString()
                iIndice_1 = iIndice_1 + 1
            End If

            If ComboBox_IDP_ID.Items.Count > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " IDP_ID = " + ComboBox_IDP_ID.SelectedValue().ToString
                iIndice_1 = iIndice_1 + 1
            End If

            If ComboBox_LI_ID.Items.Count > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " LI_ID = " + ComboBox_LI_ID.SelectedValue().ToString
                iIndice_1 = iIndice_1 + 1
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

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
                    MyBase.BS = m_bs
                End If
            End If


        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Private Sub CaricaCID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT C_ID, C_Nome "
            strSQL = strSQL + " FROM Cliente "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
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
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
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
            If Me.ComboBox_C_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE IDP_C_ID = " + Me.ComboBox_C_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE IDP_C_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
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
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
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
            'cmd.Connection = cn
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
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub

    Private Sub CaricaTCPIP_Addr()
        Try
            ListBox_Remote_Connected.Items.Clear()
            If Not m_ltcpipClient Is Nothing Then
                For Each tcpipClient As TCPIPClient In m_ltcpipClient
                    If Not tcpipClient.TC Is Nothing Then
                        Try
                            ListBox_Remote_Connected.Items.Add(tcpipClient.TC.Client.RemoteEndPoint)
                        Catch ex As Exception

                        End Try
                    End If
                Next tcpipClient
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Nothing, False)
        End Try

    End Sub

    Private Sub CaricaCheckBox()
        Try
            Dim usLIAMDEAPC As UShort
            usLIAMDEAPC = GetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue)
            If usLIAMDEAPC = AZIONE_MODBUS_TI_GET_CONFIG_PROC Then
                Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Checked = True
                Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Checked = False
            ElseIf usLIAMDEAPC = AZIONE_MODBUS_TI_SET_CONFIG_PROC Then
                Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Checked = True
                Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Checked = False
            ElseIf usLIAMDEAPC = AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC Then
                Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Checked = True
            Else
                Me.CheckBox_DL_Vs_PC_Leggi_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Invia_Configurazione.Checked = False
                Me.CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Checked = False
            End If

            Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID)
        End Try

    End Sub

    Private Sub EseguiBinding()
        If Not m_bs.DataSource Is Nothing Then
            Me.Label_C_ID.Text = m_bs.DataSource.Columns("C_Nome").Caption
            Me.Label_IDP_ID.Text = m_bs.DataSource.Columns("IDP_Nome").Caption
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_Nr").Caption

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_LI_ID.Enabled = True

        Button_Stop_TCPIP.Enabled = False

    End Sub

    'Private Sub RimuoviBinding()

    '    If m_iCID = 0 Then
    '        Me.ComboBox_C_ID.Enabled = True
    '    End If
    '    If m_iIDPID = 0 Then
    '        Me.ComboBox_IDP_ID.Enabled = True
    '    End If
    '    If m_iLIID = 0 Then
    '        Me.ComboBox_LI_ID.Enabled = True
    '    End If

    'End Sub

    Private Sub Button_Scarica_Dati_Man_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Scarica_Dati_Man.Click
        Dim tcpipc As TCPIPClient
        Dim strTCPIPHostAddr As String
        Dim iTCPIPHostPort As Integer

        If Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True Then
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Get_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        Else
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        End If
        iTCPIPHostPort = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)

        ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "E' Stata Avviata la procedura manuale di Prelevamento dei Dati Dal Datalogger.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        If Not strTCPIPHostAddr Is Nothing Then
            If iTCPIPHostPort > 0 Then
                If strTCPIPHostAddr.Length > 0 Then
                    tcpipc = New TCPIPClient
                    tcpipc.BeginConnect(Me.ComboBox_LI_ID.SelectedValue, strTCPIPHostAddr, iTCPIPHostPort, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, Nothing)
                    m_ltcpipClient.Add(tcpipc)
                    CaricaTCPIP_Addr()
                Else
                    ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
                End If
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nr Porta TCP/IP non impostata.", "", "", DEFAULT_OPERATOR_ID)
            End If
        Else
            ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
        End If

    End Sub

    Private Sub Button_Sincronizza_Dati_Man_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Sincronizza_Dati_Man.Click
        Dim tcpipc As TCPIPClient
        Dim strTCPIPHostAddr As String
        Dim iTCPIPHostPort As Integer

        If Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True Then
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Get_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        Else
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        End If
        iTCPIPHostPort = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)

        ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "E' Stata Avviata la procedura manuale di Sincronizzazione dei Dati Dal Datalogger.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        If Not strTCPIPHostAddr Is Nothing Then
            If iTCPIPHostPort > 0 Then
                If strTCPIPHostAddr.Length > 0 Then

                    If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC) = True Then
                        ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, verranno Sincronizzati i Dati dal Datalogger Al Database.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    Else
                        ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                    tcpipc = New TCPIPClient
                    tcpipc.BeginConnect(Me.ComboBox_LI_ID.SelectedValue, strTCPIPHostAddr, iTCPIPHostPort, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, Nothing)
                    m_ltcpipClient.Add(tcpipc)
                    CaricaTCPIP_Addr()
                Else
                    ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
                End If
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nr Porta TCP/IP non impostata.", "", "", DEFAULT_OPERATOR_ID)
            End If
        Else
            ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
        End If

    End Sub

    Private Sub Button_Invia_Configurazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Invia_Configurazione.Click
        Dim tcpipc As TCPIPClient
        Dim strTCPIPHostAddr As String
        Dim iTCPIPHostPort As Integer
        Dim dt As DataTable
        Dim bAll As Boolean

        If Me.CheckBoxInviaTuttiIParametri.Checked = True Then
            bAll = True
        End If

        dt = GetConfigValueToWrite(Me.ComboBox_LI_ID.SelectedValue, bAll)

        If Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True Then
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Get_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        Else
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        End If
        iTCPIPHostPort = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)

        If Not dt Is Nothing Then
            If Not strTCPIPHostAddr Is Nothing Then
                If iTCPIPHostPort > 0 Then
                    If strTCPIPHostAddr.Length > 0 Then
                        tcpipc = New TCPIPClient
                        tcpipc.BeginConnect(Me.ComboBox_LI_ID.SelectedValue, strTCPIPHostAddr, iTCPIPHostPort, AZIONE_MODBUS_TI_SET_CONFIG_PROC, dt)
                        m_ltcpipClient.Add(tcpipc)
                        CaricaTCPIP_Addr()
                    Else
                        ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID, Me)
                    End If
                Else
                    ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nr Porta TCP/IP non impostata.", "", "", DEFAULT_OPERATOR_ID)
                End If
            Else
                ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
            End If
        Else
            ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nessun Parametro di Configurazione disponibile.", "", "", DEFAULT_OPERATOR_ID)
        End If

        'Me.Close()
    End Sub

    Private Sub Button_Leggi_Configurazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Leggi_Configurazione.Click
        Dim tcpipc As TCPIPClient
        Dim strTCPIPHostAddr As String
        Dim iTCPIPHostPort As Integer
        Dim dt As DataTable
        Dim bAll As Boolean

        If Me.CheckBoxLeggiTuttiIParametri.Checked = True Then
            bAll = True
        End If

        dt = GetConfigValueToRead(Me.ComboBox_LI_ID.SelectedValue, bAll)

        If Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True Then
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Get_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        Else
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        End If
        iTCPIPHostPort = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)

        If Not dt Is Nothing Then
            If Not strTCPIPHostAddr Is Nothing Then
                If iTCPIPHostPort > 0 Then
                    If strTCPIPHostAddr.Length > 0 Then
                        tcpipc = New TCPIPClient
                        tcpipc.BeginConnect(Me.ComboBox_LI_ID.SelectedValue, strTCPIPHostAddr, iTCPIPHostPort, AZIONE_MODBUS_TI_GET_CONFIG_PROC, dt)
                        m_ltcpipClient.Add(tcpipc)
                        CaricaTCPIP_Addr()
                    Else
                        ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID, Me)
                    End If
                Else
                    ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nr Porta TCP/IP non impostata.", "", "", DEFAULT_OPERATOR_ID)
                End If
            Else
                ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Indirizzo TCP/IP non impostato.", "", "", DEFAULT_OPERATOR_ID)
            End If
        Else
            ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Dispositivo Remoto Selezionato oppure Nessun Parametro di Configurazione disponibile.", "", "", DEFAULT_OPERATOR_ID)
        End If

        'Me.Close()
    End Sub

    Private Sub Button_Stop_TCPIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Stop_TCPIP.Click
        If Not m_ltcpipClient Is Nothing Then
            For Each tcpipClient As TCPIPClient In m_ltcpipClient
                If Not tcpipClient.TC Is Nothing Then
                    If ListBox_Remote_Connected.SelectedItems.Count > 0 Then
                        If tcpipClient.TC.Client.RemoteEndPoint.ToString() = ListBox_Remote_Connected.SelectedItem.ToString() Then
                            tcpipClient.Close()
                            ListBox_Remote_Connected.Items.Remove(ListBox_Remote_Connected.SelectedItem)
                            Button_Stop_TCPIP.Enabled = False
                        End If
                    End If
                End If
            Next tcpipClient
        End If

        'Me.Close()
    End Sub

    Private Sub ListBox_TCPIP_Addr_InUse_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox_Remote_Connected.SelectedValueChanged
        If ListBox_Remote_Connected.SelectedItems.Count > 0 Then
            ' Riavvio il timer in modo da avere piu' tempo per le operazioni
            TimerTCPIP.Stop()
            TimerTCPIP.Start()

            Button_Stop_TCPIP.Enabled = True
        End If
    End Sub

    Private Sub TimerTCPIP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTCPIP.Tick

        TimerTCPIP.Stop()

        ' Aggiorno
        CaricaTCPIP_Addr()

        TimerTCPIP.Start()

    End Sub

    Private Sub CheckBox_DL_Vs_PC_Leggi_Configurazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_DL_Vs_PC_Leggi_Configurazione.Click
        If CheckBox_DL_Vs_PC_Leggi_Configurazione.Checked = True Then
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, AZIONE_MODBUS_TI_GET_CONFIG_PROC) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, verranno prelevati tutti i parametri del Datalogger.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, 0) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, non verra' effettuata nessuna operazione aggiuntiva.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        'Me.Close()
    End Sub

    Private Sub CheckBox_DL_Vs_PC_Invia_Configurazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_DL_Vs_PC_Invia_Configurazione.Click
        If CheckBox_DL_Vs_PC_Invia_Configurazione.Checked = True Then
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, AZIONE_MODBUS_TI_SET_CONFIG_PROC) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, verranno inviati tutti i parametri del Datalogger modificati.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, 0) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, non verra' effettuata nessuna operazione aggiuntiva.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        'Me.Close()
    End Sub

    Private Sub CheckBox_DL_Vs_PC_Sincronizza_Dati_Man_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Click
        If CheckBox_DL_Vs_PC_Sincronizza_Dati_Man.Checked = True Then
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, verranno Sincronizzati i Dati dal Datalogger Al Database.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            If SetLIAzioneModbusDaEseguireAllaProssConn(Me.ComboBox_LI_ID.SelectedValue, 0) = True Then
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_OK, "Alla prossima connessione instaurata dal Datalogger, non verra' effettuata nessuna operazione aggiuntiva.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_ERR, "Alla prossima connessione instaurata dal Datalogger, verranno effettuate le operazioni gia' previste.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        'Me.Close()
    End Sub

    Private Sub Button_Connect_Via_HTTP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Connect_Via_HTTP.Click
        Dim strURL As String = "http://" 'www.google.it/"
        Dim strTCPIPHostAddr As String
        Dim strTCPIPHostWebPort As Integer
        If Me.CheckBox_Usa_LI_TCPIP_Get_Ind.Checked = True Then
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Get_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
            strTCPIPHostWebPort = GENERICA_DESCRIZIONE("LI_TCPIP_Web_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        Else
            strTCPIPHostAddr = GENERICA_DESCRIZIONE("LI_TCPIP_Set_Ind", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
            strTCPIPHostWebPort = GENERICA_DESCRIZIONE("LI_TCPIP_Web_Port", "LoggerInst", "LI_ID", Me.ComboBox_LI_ID.SelectedValue, m_iUID)
        End If

        Process.Start("http://" + strTCPIPHostAddr + ":" + strTCPIPHostWebPort.ToString())
    End Sub

End Class
