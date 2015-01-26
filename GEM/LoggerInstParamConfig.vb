Imports System.Data.SqlClient

Public Class LoggerInstParamConfig
    Dim m_bs As New BindingSource

    Private m_iLIID As Integer

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Logger Installati Parametri di Configurazione"

        CaricaDati()

        CaricaPCID(False)

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Protected Overrides Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If DataGridView.SelectedRows.Count > 0 Then
                If DataGridView.SelectedRows(0).Index >= 0 Then
                    If Not m_bs.DataSource Is Nothing Then
                        If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                            If Not DataGridView.SelectedRows(0).Cells("LPC_TIPO").Value Is DBNull.Value Then
                                Me.ComboBox_LPC_ID.Text = DataGridView.SelectedRows(0).Cells("LPC_TIPO").Value
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        MyBase.DataGridView_SelectionChanged(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then

            Button_ADD_ALL_LPC_ID.Enabled = False

            CaricaPCID(True)

            RimuoviBinding()
            Me.TextBox_LIPC_LPC_VAL_MEMO_DB.Text = ""
            Me.TextBox_LIPC_Descrizione.Text = ""
            Me.TextBox_LIPC_K.Text = "1"
            Me.ComboBox_LPC_ID.Enabled = True
            ' Controllo che ci siano dei valori
            If Me.ComboBox_LPC_ID.Items.Count > 0 Then

                Dim dr As DataRow

                dr = m_bs.DataSource.Rows.Add()

                MyBase.ToolStripButton_Nuovo_Click(sender, e)
            Else
                EseguiBinding()
                ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, non ci sono altri parametri da aggiungere.", "", "", m_iUID, Me)
            End If
        End If
    End Sub

    Protected Overrides Sub ToolStripButton_Elimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim dr As DataRow

        If Not m_bs.DataSource Is Nothing Then

            If Not m_bs.Current() Is Nothing Then

                dr = m_bs.Current().Row()

                Try
                    strSQL = "DELETE FROM [LoggerInstParamConfig] "
                    strSQL = strSQL + "WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID AND LIPC_CC = @LIPC_CC "

                    CustomSQLConnectionOpen(cn, cmd)
                    'cmd.Connection = cn
                    cmd.CommandText = strSQL

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@LIPC_LI_ID", dr.Item("LIPC_LI_ID"))
                    cmd.Parameters.AddWithValue("@LIPC_LPC_ID", dr.Item("LIPC_LPC_ID"))
                    cmd.Parameters.AddWithValue("@LIPC_CC", dr.Item("LIPC_CC"))

                    If cmd.ExecuteNonQuery() > 0 Then
                        ScriviLogEventi(m_iLIID, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", m_iUID, Me)
                    Else
                        ScriviLogEventi(m_iLIID, 0, AZIONE_DEL, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                    End If

                    dr.Delete()

                Catch ex As Exception
                    ScriviLogEventi(m_iLIID, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
                End Try

            End If

        End If

        ds.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

            MyBase.ToolStripButton_Elimina_Click(sender, e)

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Modifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Not m_bs.DataSource Is Nothing Then

            If Not m_bs.Current() Is Nothing Then

                ComboBox_LPC_ID.Enabled = False
                Button_ADD_ALL_LPC_ID.Enabled = False

                RimuoviBinding()

                Dim dr As DataRow

                dr = m_bs.Current().Row()
                dr.SetModified()

            End If

            MyBase.ToolStripButton_Modifica_Click(sender, e)

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Salva_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            If Not m_bs.DataSource Is Nothing Then
                If m_bs.DataSource.Rows.Count > 0 Then
                    For Each dr As DataRow In m_bs.DataSource.Rows
                        If dr.RowState = DataRowState.Modified Then

                            strSQL = " UPDATE [LoggerInstParamConfig] "
                            strSQL = strSQL + " SET LIPC_Descrizione = @LIPC_Descrizione, LIPC_LPC_VAL_MEMO_DB = @LIPC_LPC_VAL_MEMO_DB, LIPC_K = @LIPC_K, LIPC_Inviato = @LIPC_Inviato, LIPC_Letto = @LIPC_Letto, LIPC_DataOra = @LIPC_DataOra, LIPC_U_ID = @LIPC_U_ID "
                            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_CC = @LIPC_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL
                            dr.Item("LIPC_Descrizione") = Me.TextBox_LIPC_Descrizione.Text
                            dr.Item("LIPC_LPC_VAL_MEMO_DB") = Me.TextBox_LIPC_LPC_VAL_MEMO_DB.Text
                            dr.Item("LIPC_K") = Me.TextBox_LIPC_K.Text
                            dr.Item("LIPC_Inviato") = False
                            dr.Item("LIPC_Letto") = False
                            dr.Item("LIPC_DataOra") = Date.Now
                            dr.Item("LIPC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LIPC_LI_ID", dr.Item("LIPC_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", dr.Item("LIPC_LPC_ID"))
                            cmd.Parameters.AddWithValue("@LIPC_CC", dr.Item("LIPC_CC"))
                            cmd.Parameters.AddWithValue("@LIPC_Descrizione", dr.Item("LIPC_Descrizione"))
                            cmd.Parameters.AddWithValue("@LIPC_LPC_VAL_MEMO_DB", dr.Item("LIPC_LPC_VAL_MEMO_DB"))
                            cmd.Parameters.AddWithValue("@LIPC_K", dr.Item("LIPC_K"))
                            cmd.Parameters.AddWithValue("@LIPC_Inviato", dr.Item("LIPC_Inviato"))
                            cmd.Parameters.AddWithValue("@LIPC_Letto", dr.Item("LIPC_Letto"))
                            cmd.Parameters.AddWithValue("@LIPC_DataOra", dr.Item("LIPC_DataOra"))
                            cmd.Parameters.AddWithValue("@LIPC_U_ID", dr.Item("LIPC_U_ID"))

                            If IsParameterDataLoggerValueInRange(dr.Item("LIPC_LPC_ID"), dr.Item("LIPC_LPC_VAL_MEMO_DB"), m_iUID) = False Then
                                ScriviLogEventi(m_iLIID, 0, AZIONE_MOD, RISULTATO_ERR, "Il valore immesso e' fuori dai limiti previsti.", "", "", m_iUID, Me)
                            Else
                                If cmd.ExecuteNonQuery() > 0 Then
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                                Else
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                                End If
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [LoggerInstParamConfig] "
                            strSQL = strSQL + "  (LIPC_LI_ID,  LIPC_LPC_ID,  LIPC_Descrizione,  LIPC_LPC_VAL_MEMO_DB,  LIPC_K,  LIPC_Inviato,  LIPC_Letto,  LIPC_DataOra,  LIPC_U_ID  ) VALUES "
                            strSQL = strSQL + "  (@LIPC_LI_ID, @LIPC_LPC_ID, @LIPC_Descrizione, @LIPC_LPC_VAL_MEMO_DB, @LIPC_K, @LIPC_Inviato, @LIPC_Letto, @LIPC_DataOra, @LIPC_U_ID) "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LIPC_LI_ID") = m_iLIID
                            dr.Item("LIPC_LPC_ID") = Me.ComboBox_LPC_ID.SelectedValue()
                            dr.Item("LIPC_Descrizione") = Me.TextBox_LIPC_Descrizione.Text
                            dr.Item("LIPC_LPC_VAL_MEMO_DB") = Me.TextBox_LIPC_LPC_VAL_MEMO_DB.Text
                            dr.Item("LIPC_K") = Me.TextBox_LIPC_K.Text
                            dr.Item("LIPC_Inviato") = False
                            dr.Item("LIPC_Letto") = False
                            dr.Item("LIPC_DataOra") = Date.Now
                            dr.Item("LIPC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LIPC_LI_ID", dr.Item("LIPC_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", dr.Item("LIPC_LPC_ID"))
                            cmd.Parameters.AddWithValue("@LIPC_Descrizione", dr.Item("LIPC_Descrizione"))
                            cmd.Parameters.AddWithValue("@LIPC_LPC_VAL_MEMO_DB", dr.Item("LIPC_LPC_VAL_MEMO_DB"))
                            cmd.Parameters.AddWithValue("@LIPC_K", dr.Item("LIPC_K"))
                            cmd.Parameters.AddWithValue("@LIPC_Inviato", dr.Item("LIPC_Inviato"))
                            cmd.Parameters.AddWithValue("@LIPC_Letto", dr.Item("LIPC_Letto"))
                            cmd.Parameters.AddWithValue("@LIPC_DataOra", dr.Item("LIPC_DataOra"))
                            cmd.Parameters.AddWithValue("@LIPC_U_ID", dr.Item("LIPC_U_ID"))

                            If IsParameterDataLoggerValueInRange(dr.Item("LIPC_LPC_ID"), dr.Item("LIPC_LPC_VAL_MEMO_DB"), m_iUID) = False Then
                                ScriviLogEventi(m_iLIID, 0, AZIONE_MOD, RISULTATO_ERR, "Il valore immesso e' fuori dai limiti previsti.", "", "", m_iUID, Me)
                            Else
                                If cmd.ExecuteNonQuery() > 0 Then
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
                                Else
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                                End If
                            End If

                            CaricaDati()

                        End If
                    Next dr

                    cn.Close()
                End If
            End If

        Catch ex As Exception

            CaricaDati()

            ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If Not m_bs.DataSource Is Nothing Then

            EseguiBinding()

        End If

        Button_ADD_ALL_LPC_ID.Enabled = True

        CaricaPCID(False)

        MyBase.ToolStripButton_Salva_Click(sender, e)
        Me.m_bs.Position = 10

    End Sub

    Protected Overrides Sub ToolStripButton_Annulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

            EseguiBinding()

            MyBase.ToolStripButton_Annulla_Click(sender, e)

        End If

        ComboBox_LPC_ID.Enabled = True
        Button_ADD_ALL_LPC_ID.Enabled = True

        CaricaPCID(False)

    End Sub

    Protected Overrides Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        If (Me.TextBox_C_ID.DataBindings.Count > 0) Then
            Me.TextBox_C_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_ID.DataBindings.Count > 0) Then
            Me.TextBox_IDP_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_L_ID.DataBindings.Count > 0) Then
            Me.TextBox_L_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_LI_Nr.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_ID.DataBindings.Count > 0) Then
            Me.TextBox_LI_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_Descrizione.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_Descrizione.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_K.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_K.DataBindings.Clear()
        End If

        MyBase.BaseForm_1_FormClosed(sender, e)

    End Sub

    Private Sub CaricaDati()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            'strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, ImpiantoDiProduzione.IDP_Nome, L_Marca + ' - ' + L_Modello as L_TIPO, LoggerInst.LI_Nr, LoggerInst.LI_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO, LoggerParamConfig.LPC_ID, LoggerParamConfig.LPC_Nome, LoggerParamConfig.LPC_UM, LoggerInstParamConfig.LIPC_Descrizione, LoggerInstParamConfig.LIPC_LPC_VAL_MEMO_DB, LoggerInstParamConfig.LIPC_LPC_VAL_MEMO_DL, LoggerParamConfig.LPC_Indirizzo_Registro_L, LoggerParamConfig.LPC_Indirizzo_Registro_H, LoggerInstParamConfig.LIPC_LI_ID, LoggerInstParamConfig.LIPC_LPC_ID, LoggerParamConfig.LPC_Min_Value, LoggerParamConfig.LPC_Max_Value, LoggerParamConfig.LPC_Default_Value, LoggerInstParamConfig.LIPC_K, LoggerInstParamConfig.LIPC_Inviato, LoggerInstParamConfig.LIPC_DataOra_Invio, LoggerInstParamConfig.LIPC_Letto, LoggerInstParamConfig.LIPC_DataOra_Lettura, LoggerParamConfig.LPC_ReadOnly, LoggerInstParamConfig.LIPC_DataOra, LoggerInstParamConfig.LIPC_U_ID, LoggerInstParamConfig.LIPC_CC, Utente.U_NomeCognome "
            'strSQL = strSQL + " FROM LoggerInst "
            'strSQL = strSQL + " INNER JOIN LoggerInstParamConfig ON LoggerInst.LI_ID = LoggerInstParamConfig.LIPC_LI_ID "
            'strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            'strSQL = strSQL + " INNER JOIN Utente ON LoggerInst.LI_U_ID = Utente.U_ID AND LoggerInstParamConfig.LIPC_U_ID = Utente.U_ID AND LoggerParamConfig.LPC_U_ID = Utente.U_ID "
            'strSQL = strSQL + " INNER JOIN Cliente ON Utente.U_ID = Cliente.C_U_ID "
            'strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID AND Utente.U_ID = ImpiantoDiProduzione.IDP_U_ID AND Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID "
            'strSQL = strSQL + " INNER JOIN Logger ON LoggerInst.LI_L_ID = Logger.L_ID AND Utente.U_ID = Logger.L_U_ID "
            'strSQL = strSQL + " WHERE LI_ID = " + m_iLIID.ToString
            strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, ImpiantoDiProduzione.IDP_Nome, L_Marca + ' - ' + L_Modello as L_TIPO, LoggerInst.LI_Nr, LoggerInst.LI_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO, LoggerParamConfig.LPC_ID, LoggerParamConfig.LPC_Nome, LoggerParamConfig.LPC_UM, LoggerInstParamConfig.LIPC_Descrizione, LoggerInstParamConfig.LIPC_LPC_VAL_MEMO_DB, LoggerInstParamConfig.LIPC_LPC_VAL_MEMO_DL, LoggerParamConfig.LPC_Indirizzo_Registro_L, LoggerParamConfig.LPC_Indirizzo_Registro_H, LoggerInstParamConfig.LIPC_LI_ID, LoggerInstParamConfig.LIPC_LPC_ID, LoggerParamConfig.LPC_Min_Value, LoggerParamConfig.LPC_Max_Value, LoggerParamConfig.LPC_Default_Value, LoggerInstParamConfig.LIPC_K, LoggerInstParamConfig.LIPC_Inviato, LoggerInstParamConfig.LIPC_DataOra_Invio, LoggerInstParamConfig.LIPC_Letto, LoggerInstParamConfig.LIPC_DataOra_Lettura, LoggerParamConfig.LPC_ReadOnly, LoggerInstParamConfig.LIPC_DataOra, LoggerInstParamConfig.LIPC_U_ID, LoggerInstParamConfig.LIPC_CC, Utente.U_NomeCognome "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInstParamConfig.LIPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN Logger ON LoggerInst.LI_L_ID = Logger.L_ID "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            strSQL = strSQL + " INNER JOIN Cliente ON ImpiantoDiProduzione.IDP_C_ID = Cliente.C_ID "
            strSQL = strSQL + " INNER JOIN Utente ON LoggerInstParamConfig.LIPC_U_ID = Utente.U_ID "
            strSQL = strSQL + " WHERE LI_ID = " + m_iLIID.ToString

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
            ScriviLogEventi(m_iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Private Sub CaricaPCID(ByVal bOnlyValueNotUsed As Boolean)
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            If bOnlyValueNotUsed = False Then
                strSQL = " SELECT LPC_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO "
                strSQL = strSQL + " FROM LoggerParamConfig "
            Else
                strSQL = " SELECT LPC_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO "
                strSQL = strSQL + " FROM LoggerParamConfig "
                strSQL = strSQL + " EXCEPT "
                strSQL = strSQL + " SELECT LPC_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO "
                strSQL = strSQL + " FROM LoggerInstParamConfig "
                strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInstParamConfig.LIPC_LI_ID = LoggerInst.LI_ID "
                strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
                strSQL = strSQL + " WHERE (LoggerInst.LI_ID = " + m_iLIID.ToString + ") "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_LPC_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "LPC_TIPO"
                        .ValueMember = "LPC_ID"
                        If .Items.Count > 0 Then
                            .SelectedIndex = 0
                        Else
                            .SelectedIndex = -1
                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(m_iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub


    Private Sub EseguiBinding()
        If Not m_bs.DataSource Is Nothing Then
            Me.Label_C_ID.Text = m_bs.DataSource.Columns("C_Nome").Caption
            Me.Label_IDP_ID.Text = m_bs.DataSource.Columns("IDP_Nome").Caption
            Me.Label_L_ID.Text = m_bs.DataSource.Columns("L_TIPO").Caption
            Me.Label_LI_Nr.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_ID").Caption
            Me.Label_LIPC_Descrizione.Text = m_bs.DataSource.Columns("LIPC_Descrizione").Caption
            Me.Label_LIPC_LPC_VAL.Text = m_bs.DataSource.Columns("LIPC_LPC_VAL_MEMO_DB").Caption
            Me.Label_LIPC_K.Text = m_bs.DataSource.Columns("LIPC_K").Caption

            If (Me.TextBox_C_ID.DataBindings.Count = 0) Then
                Me.TextBox_C_ID.DataBindings.Add(New Binding("Text", m_bs, "C_Nome", True))
            End If
            If (Me.TextBox_IDP_ID.DataBindings.Count = 0) Then
                Me.TextBox_IDP_ID.DataBindings.Add(New Binding("Text", m_bs, "IDP_Nome", True))
            End If
            If (Me.TextBox_L_ID.DataBindings.Count = 0) Then
                Me.TextBox_L_ID.DataBindings.Add(New Binding("Text", m_bs, "L_TIPO", True))
            End If
            If (Me.TextBox_LI_Nr.DataBindings.Count = 0) Then
                Me.TextBox_LI_Nr.DataBindings.Add(New Binding("Text", m_bs, "LI_Nr", True))
            End If
            If (Me.TextBox_LI_ID.DataBindings.Count = 0) Then
                Me.TextBox_LI_ID.DataBindings.Add(New Binding("Text", m_bs, "LI_ID", True))
            End If
            If (Me.TextBox_LIPC_Descrizione.DataBindings.Count = 0) Then
                Me.TextBox_LIPC_Descrizione.DataBindings.Add(New Binding("Text", m_bs, "LIPC_Descrizione", True))
            End If
            If (Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Count = 0) Then
                Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Add(New Binding("Text", m_bs, "LIPC_LPC_VAL_MEMO_DB", True))
            End If
            If (Me.TextBox_LIPC_K.DataBindings.Count = 0) Then
                Me.TextBox_LIPC_K.DataBindings.Add(New Binding("Text", m_bs, "LIPC_K", True))
            End If

        End If

        Me.TextBox_C_ID.ReadOnly = True
        Me.TextBox_IDP_ID.ReadOnly = True
        Me.TextBox_L_ID.ReadOnly = True
        Me.TextBox_LI_Nr.ReadOnly = True
        Me.TextBox_LI_ID.ReadOnly = True
        Me.TextBox_LIPC_Descrizione.ReadOnly = True
        Me.TextBox_LIPC_LPC_VAL_MEMO_DB.ReadOnly = True
        Me.TextBox_LIPC_K.ReadOnly = True
        Me.ComboBox_LPC_ID.Enabled = False

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_C_ID.DataBindings.Count > 0) Then
            Me.TextBox_C_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_ID.DataBindings.Count > 0) Then
            Me.TextBox_IDP_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_L_ID.DataBindings.Count > 0) Then
            Me.TextBox_L_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_LI_Nr.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_ID.DataBindings.Count > 0) Then
            Me.TextBox_LI_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_Descrizione.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_Descrizione.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_LPC_VAL_MEMO_DB.DataBindings.Clear()
        End If
        If (Me.TextBox_LIPC_K.DataBindings.Count > 0) Then
            Me.TextBox_LIPC_K.DataBindings.Clear()
        End If

        Me.TextBox_C_ID.ReadOnly = True
        Me.TextBox_IDP_ID.ReadOnly = True
        Me.TextBox_L_ID.ReadOnly = True
        Me.TextBox_LI_Nr.ReadOnly = True
        Me.TextBox_LI_ID.ReadOnly = True
        Me.TextBox_LIPC_Descrizione.ReadOnly = False
        Me.TextBox_LIPC_LPC_VAL_MEMO_DB.ReadOnly = False
        Me.TextBox_LIPC_K.ReadOnly = False
        Me.ComboBox_LPC_ID.Enabled = False
    End Sub

    Private Sub ComboBox_LPC_ID_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LPC_ID.SelectedValueChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender
        If Not cb.SelectedValue Is Nothing Then
            If cb.SelectedValue.GetType().Name = st.Name Then
                If IsParameterDataLoggerValueConfigured(m_iLIID, ComboBox_LPC_ID.SelectedValue, m_iUID) = False Then
                    Me.TextBox_LIPC_LPC_VAL_MEMO_DB.Text = GetDefaultDataLoggerParameterValue(ComboBox_LPC_ID.SelectedValue, m_iUID)
                End If
            End If
        End If
    End Sub

    Private Sub Button_ADD_ALL_LPC_ID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ADD_ALL_LPC_ID.Click
        If IsParameterDataLoggerAllConfigured(m_iLIID, m_iUID) = False Then
            If AddAllParameterDataLoggerDefaultValue(m_iLIID, m_iUID) = True Then
                ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
            Else
                ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_ERR, "", "", "", m_iUID, Me)
            End If
        Else
            ScriviLogEventi(m_iLIID, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, non ci sono altri parametri da aggiungere.", "", "", m_iUID, Me)
        End If
        CaricaDati()

    End Sub

    Private Sub Button_VAL_MEMO_DL_VAL_MEMO_DB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_VAL_MEMO_DL_VAL_MEMO_DB.Click
        Dim dr As Windows.Forms.DialogResult
        dr = System.Windows.Forms.MessageBox.Show(Owner, "ATTENZIONE, Questa operazione copiera' tutti i dati dalla colonna DL alla colonna DB sovrascivendo, di fatto, tutti i dati nel DB con quelli del DL aggiornati all'ultima acquisizione dei dati acquisiti dal DL." + vbCrLf + "Sei Sicuro ?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dr = Windows.Forms.DialogResult.Yes Then
            CopyDLValueInDBValue(m_iLIID)
        End If

        CaricaDati()
    End Sub
End Class
