Imports System.Data.SqlClient

Public Class LoggerInst_X_ImpiantoDiProduzione_X_Config
    Dim m_bs As New BindingSource

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Associa Logger Installati ad Impianto Di Produzione"

        CaricaCID()

        CaricaIDPID()

        CaricaDati()

        CaricaLIID()

        CaricaITID()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Private Sub ComboBox_C_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_C_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaIDPID()

            CaricaLIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaDati()

            CaricaLIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_LI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaITID()
        End If

    End Sub

    Protected Overrides Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        MyBase.DataGridView_SelectionChanged(sender, e)

    End Sub

    Protected Overrides Sub DataGridView_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)

        MyBase.DataGridView_RowHeaderMouseDoubleClick(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then

            RimuoviBinding()

            Dim dr As DataRow

            dr = m_bs.DataSource.Rows.Add()

            MyBase.ToolStripButton_Nuovo_Click(sender, e)

        End If
    End Sub

    'Protected Overrides Sub ToolStripButton_Elimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim dr As DataRow

    '    If Not m_bs.DataSource Is Nothing Then

    '        RimuoviBinding()

    '        If Not m_bs.Current() Is Nothing Then

    '            dr = m_bs.Current().Row()

    '            Try
    '                strSQL = "DELETE FROM [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
    '                strSQL = strSQL + "WHERE LIIDPC_ID = @LIIDPC_ID AND LIIDPC_CC = @LIIDPC_CC "

    '                CustomSQLConnectionOpen(cn, cmd)
    '                'cmd.Connection = cn
    '                cmd.CommandText = strSQL

    '                cmd.Parameters.Clear()
    '                cmd.Parameters.AddWithValue("@LIIDPC_ID", dr.Item("LIIDPC_ID"))
    '                cmd.Parameters.AddWithValue("@LIIDPC_CC", dr.Item("LIIDPC_CC"))

    '                If cmd.ExecuteNonQuery() > 0 Then
    '                    ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", m_iUID, Me)
    '                Else
    '                    ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
    '                End If

    '                dr.Delete()

    '            Catch ex As Exception
    '                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
    '            End Try

    '        End If

    '    End If

    '    ds.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()

    '    If Not m_bs.DataSource Is Nothing Then

    '        CaricaDati()

    '        EseguiBinding()

    '        MyBase.ToolStripButton_Elimina_Click(sender, e)

    '    End If

    'End Sub

    Private Sub Button_Elimina_Dati_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Elimina_Dati.Click

        If Windows.Forms.MessageBox.Show("Sei Sicuro? Premendo su Ok verranno cancellati tutti i dati relativi all'associazione selezionata.", "Eliminazione dati Logger", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand

            If Not m_bs.DataSource Is Nothing Then

                If Not m_bs.Current() Is Nothing Then

                    Try

                        CustomSQLConnectionOpen(cn, cmd)
                        'cmd.Connection = cn

                        strSQL = "DELETE FROM [LoggerInst_X_ImpiantoDiProduzione_X_Valore] "
                        strSQL = strSQL + "WHERE LIIDPV_LIIDPC_ID = @LIIDPV_LIIDPC_ID "

                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@LIIDPV_LIIDPC_ID", m_bs.Current().Row().Item("LIIDPC_ID"))

                        If cmd.ExecuteNonQuery() > 0 Then
                            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione di tutti i valori dell' associazione con il Logger.", "", "", m_iUID, Me)
                        Else
                            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_DEL, RISULTATO_ERR, "Attenzione, i criteri impostati per la cancellazione non coincidono con nessuna riga. Non e' stato possibile eliminare nessuna riga.", "", "", m_iUID, Me)
                        End If

                    Catch ex As Exception
                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
                    End Try

                End If

            End If

            ds.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Elimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        If Not m_bs.DataSource Is Nothing Then

            RimuoviBinding()

            Try

                CustomSQLConnectionOpen(cn, cmd)
                'cmd.Connection = cn

                If Not m_bs.Current() Is Nothing Then
                    If Not Me.DataGridView.CurrentRow Is Nothing Then
                        If Me.DataGridView.SelectedRows.Count > 0 Then
                            For Each dgvr As DataGridViewRow In Me.DataGridView.SelectedRows

                                strSQL = "DELETE FROM [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
                                strSQL = strSQL + "WHERE LIIDPC_ID = @LIIDPC_ID AND LIIDPC_CC = @LIIDPC_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@LIIDPC_ID", dgvr.Cells("LIIDPC_ID").Value)
                                cmd.Parameters.AddWithValue("@LIIDPC_CC", dgvr.Cells("LIIDPC_CC").Value)

                                If cmd.ExecuteNonQuery() > 0 Then
                                Else
                                    ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                                End If
                            Next
                        End If
                    End If
                End If

                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", m_iUID, Me)

            Catch ex As Exception
                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
            End Try

        End If

        ds.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

            EseguiBinding()

            MyBase.ToolStripButton_Elimina_Click(sender, e)

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Modifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Not m_bs.DataSource Is Nothing Then

            If Not m_bs.Current() Is Nothing Then

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

                            strSQL = " UPDATE [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
                            strSQL = strSQL + " SET LIIDPC_LI_ID = @LIIDPC_LI_ID, LIIDPC_IT_ID = @LIIDPC_IT_ID, LIIDPC_Descrizione = @LIIDPC_Descrizione, LIIDPC_InserisciDettaglioNelReport = @LIIDPC_InserisciDettaglioNelReport, LIIDPC_DataOra = @LIIDPC_DataOra, LIIDPC_U_ID = @LIIDPC_U_ID "
                            strSQL = strSQL + " WHERE LIIDPC_ID = @LIIDPC_ID AND LIIDPC_CC = @LIIDPC_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LIIDPC_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("LIIDPC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("LIIDPC_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("LIIDPC_Descrizione") = Me.TextBox_LIIDPC_Descrizione.Text
                            dr.Item("LIIDPC_InserisciDettaglioNelReport") = Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.Checked()
                            dr.Item("LIIDPC_DataOra") = Date.Now
                            dr.Item("LIIDPC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LIIDPC_ID", dr.Item("LIIDPC_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_CC", dr.Item("LIIDPC_CC"))
                            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", dr.Item("LIIDPC_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", dr.Item("LIIDPC_IT_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_IDP_ID", dr.Item("LIIDPC_IDP_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_Descrizione", dr.Item("LIIDPC_Descrizione"))
                            cmd.Parameters.AddWithValue("@LIIDPC_InserisciDettaglioNelReport", dr.Item("LIIDPC_InserisciDettaglioNelReport"))
                            cmd.Parameters.AddWithValue("@LIIDPC_DataOra", dr.Item("LIIDPC_DataOra"))
                            cmd.Parameters.AddWithValue("@LIIDPC_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
                            strSQL = strSQL + "  (LIIDPC_LI_ID,  LIIDPC_IT_ID,  LIIDPC_IDP_ID,  LIIDPC_Descrizione,  LIIDPC_InserisciDettaglioNelReport,  LIIDPC_DataOra,  LIIDPC_U_ID) VALUES "
                            strSQL = strSQL + "  (@LIIDPC_LI_ID, @LIIDPC_IT_ID, @LIIDPC_IDP_ID, @LIIDPC_Descrizione, @LIIDPC_InserisciDettaglioNelReport, @LIIDPC_DataOra, @LIIDPC_U_ID) "
                            strSQL = strSQL + " SELECT @LIIDPC_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LIIDPC_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("LIIDPC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("LIIDPC_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("LIIDPC_Descrizione") = Me.TextBox_LIIDPC_Descrizione.Text
                            dr.Item("LIIDPC_InserisciDettaglioNelReport") = Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.Checked()
                            dr.Item("LIIDPC_DataOra") = Date.Now
                            dr.Item("LIIDPC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@LIIDPC_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", dr.Item("LIIDPC_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", dr.Item("LIIDPC_IT_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_IDP_ID", dr.Item("LIIDPC_IDP_ID"))
                            cmd.Parameters.AddWithValue("@LIIDPC_Descrizione", dr.Item("LIIDPC_Descrizione"))
                            cmd.Parameters.AddWithValue("@LIIDPC_InserisciDettaglioNelReport", dr.Item("LIIDPC_InserisciDettaglioNelReport"))
                            cmd.Parameters.AddWithValue("@LIIDPC_DataOra", dr.Item("LIIDPC_DataOra"))
                            cmd.Parameters.AddWithValue("@LIIDPC_U_ID", dr.Item("LIIDPC_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("LIIDPC_ID") = cmd.Parameters.Item("@LIIDPC_ID").Value
                                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        End If
                    Next dr

                    cn.Close()
                End If
            End If

        Catch ex As Exception

            CaricaDati()

            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If Not m_bs.DataSource Is Nothing Then

            EseguiBinding()

            MyBase.ToolStripButton_Salva_Click(sender, e)

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Annulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

            EseguiBinding()

            MyBase.ToolStripButton_Annulla_Click(sender, e)

        End If

    End Sub

    Protected Overrides Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        MyBase.BaseForm_1_FormClosed(sender, e)

    End Sub

    Private Sub CaricaDati()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            strSQL = " SELECT LIIDPC_ID, LIIDPC_CC, C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, IT_ID, IT_Nome, LoggerInst_X_ImpiantoDiProduzione_X_Config.*, U_NomeCognome"
            strSQL = strSQL + " FROM Cliente INNER JOIN ImpiantoDiProduzione ON C_ID = IDP_C_ID INNER JOIN LoggerInst ON IDP_ID = LI_IDP_ID INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON IDP_ID = LIIDPC_IDP_ID AND LI_ID = LIIDPC_LI_ID INNER JOIN IngressoTipo ON LIIDPC_IT_ID = IT_ID "
            strSQL = strSQL + " INNER JOIN Utente ON LIIDPC_U_ID = U_ID "
            strSQL = strSQL + " WHERE IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue.ToString()

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

                    RimuoviBinding()
                    m_bs.DataSource = ds.Tables(0)
                    EseguiBinding()
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

    Private Sub CaricaCID()

        If (Me.ComboBox_C_ID.DataBindings.Count > 0) Then
            Me.ComboBox_C_ID.DataBindings.Clear()
        End If

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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If (Me.ComboBox_C_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_C_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "C_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_C_ID.Items.Count > 0 Then
            If ComboBox_C_ID.SelectedIndex = -1 Then
                ComboBox_C_ID.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub CaricaIDPID()

        If (Me.ComboBox_IDP_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IDP_ID.DataBindings.Clear()
        End If

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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If (Me.ComboBox_IDP_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_IDP_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IDP_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_IDP_ID.Items.Count > 0 Then
            If ComboBox_IDP_ID.SelectedIndex = -1 Then
                ComboBox_IDP_ID.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub CaricaLIID()

        If (Me.ComboBox_LI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_LI_ID.DataBindings.Clear()
        End If

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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If (Me.ComboBox_LI_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_LI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "LI_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_LI_ID.Items.Count > 0 Then
            If ComboBox_LI_ID.SelectedIndex = -1 Then
                ComboBox_LI_ID.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub CaricaITID()

        If (Me.ComboBox_IT_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IT_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT IT_ID, IT_Nome "
            strSQL = strSQL + " FROM IngressoTipo "
            strSQL = strSQL + " WHERE IT_ID >= 101 AND IT_ID <= 196 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_IT_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "IT_Nome"
                        .ValueMember = "IT_ID"
                        If .Items.Count > 0 Then
                            .SelectedIndex = 0
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

        If (Me.ComboBox_IT_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_IT_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IT_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_IT_ID.Items.Count > 0 Then
            If ComboBox_IT_ID.SelectedIndex = -1 Then
                ComboBox_IT_ID.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub EseguiBinding()
        If Not m_bs.DataSource Is Nothing Then
            Me.Label_C_ID.Text = m_bs.DataSource.Columns("C_Nome").Caption
            Me.Label_IDP_ID.Text = m_bs.DataSource.Columns("IDP_Nome").Caption
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_IT_ID.Text = m_bs.DataSource.Columns("IT_Nome").Caption
            Me.Label_LIIDPC_Descrizione.Text = m_bs.DataSource.Columns("LIIDPC_Descrizione").Caption
        End If

        If (Me.ComboBox_LI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_LI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "LI_ID", True))
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count = 0) Then
            Me.ComboBox_IT_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IT_ID", True))
        End If
        If (Me.TextBox_LIIDPC_Descrizione.DataBindings.Count = 0) Then
            Me.TextBox_LIIDPC_Descrizione.DataBindings.Add(New Binding("Text", m_bs, "LIIDPC_Descrizione", True))
        End If
        If (Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.DataBindings.Count = 0) Then
            Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.DataBindings.Add(New Binding("Checked", m_bs, "LIIDPC_InserisciDettaglioNelReport", True))
        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True

        Me.ComboBox_LI_ID.Enabled = False
        Me.ComboBox_IT_ID.Enabled = False
        Me.TextBox_LIIDPC_Descrizione.Enabled = False
        Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.Enabled = False

        Me.Button_Aggungi_Tutti_In.Enabled = False

        Me.Button_Elimina_Dati.Enabled = True

    End Sub

    Private Sub RimuoviBinding()

        If (Me.ComboBox_C_ID.DataBindings.Count > 0) Then
            Me.ComboBox_C_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_IDP_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IDP_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_LI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_LI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IT_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIDPC_Descrizione.DataBindings.Count > 0) Then
            Me.TextBox_LIIDPC_Descrizione.DataBindings.Clear()
        End If
        If (Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.DataBindings.Count > 0) Then
            Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False

        Me.ComboBox_LI_ID.Enabled = True
        Me.ComboBox_IT_ID.Enabled = True
        Me.TextBox_LIIDPC_Descrizione.Enabled = True
        Me.CheckBox_INS_DETT_NEL_FILE_DI_REP_GIORN.Enabled = True

        Me.Button_Aggungi_Tutti_In.Enabled = True

        Me.Button_Elimina_Dati.Enabled = False

    End Sub

    Private Sub Button_Aggungi_Tutti_In_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Aggungi_Tutti_In.Click

        If AddAllInputDatalogger(ComboBox_LI_ID.SelectedValue(), ComboBox_IDP_ID.SelectedValue(), m_iUID) = True Then
            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
        Else
            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_ERR, "", "", "", m_iUID, Me)
        End If
        CaricaDati()

        EseguiBinding()

        MyBase.ToolStripButton_Salva_Click(sender, e)
    End Sub
End Class
