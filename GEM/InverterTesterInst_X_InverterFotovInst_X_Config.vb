Imports System.Data.SqlClient

Public Class InverterTesterInst_X_InverterFotovInst_X_Config
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

        Me.Text = "Associa Inverter Tester Installati a Inverter Fotovoltaici Installati"

        CaricaCID()

        CaricaIDPID()

        CaricaDati()

        CaricaCDPIID()

        CaricaIFIID()

        CaricaLIID()

        CaricaITIID()

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
            CaricaCDPIID()
            CaricaIFIID()

            CaricaLIID()
            CaricaITIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaDati()

            CaricaCDPIID()
            CaricaIFIID()

            CaricaLIID()
            CaricaITIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_CDPI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_CDPI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaIFIID()

            'CaricaLIID()
            'CaricaITIID()
            'CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_IFI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IFI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then

            'CaricaLIID()
            'CaricaITIID()
            'CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_LI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaITIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_ITI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_ITI_ID.SelectedIndexChanged
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
    '                strSQL = "DELETE FROM [InverterTesterInst_X_InverterFotovInst_X_Config] "
    '                strSQL = strSQL + "WHERE ITIIFIC_ID = @ITIIFIC_ID AND ITIIFIC_CC = @ITIIFIC_CC "

    '                CustomSQLConnectionOpen(cn, cmd)
    '                'cmd.Connection = cn
    '                cmd.CommandText = strSQL

    '                cmd.Parameters.Clear()
    '                cmd.Parameters.AddWithValue("@ITIIFIC_ID", dr.Item("ITIIFIC_ID"))
    '                cmd.Parameters.AddWithValue("@ITIIFIC_CC", dr.Item("ITIIFIC_CC"))

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

        If Windows.Forms.MessageBox.Show("Sei Sicuro? Premendo su Ok verranno cancellati tutti i dati relativi all'associazione selezionata.", "Eliminazione dati Inverter Tester", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand

            If Not m_bs.DataSource Is Nothing Then

                If Not m_bs.Current() Is Nothing Then

                    Try

                        CustomSQLConnectionOpen(cn, cmd)
                        'cmd.Connection = cn

                        strSQL = "DELETE FROM [InverterTesterInst_X_InverterFotovInst_X_Valore] "
                        strSQL = strSQL + "WHERE ITIIFIV_ITIIFIC_ID = @ITIIFIV_ITIIFIC_ID "

                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@ITIIFIV_ITIIFIC_ID", m_bs.Current().Row().Item("ITIIFIC_ID"))

                        If cmd.ExecuteNonQuery() > 0 Then
                            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione di tutti i valori dell' associazione con l'Inverter Tester.", "", "", m_iUID, Me)
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

                                strSQL = "DELETE FROM [InverterTesterInst_X_InverterFotovInst_X_Config] "
                                strSQL = strSQL + "WHERE ITIIFIC_ID = @ITIIFIC_ID AND ITIIFIC_CC = @ITIIFIC_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@ITIIFIC_ID", dgvr.Cells("ITIIFIC_ID").Value)
                                cmd.Parameters.AddWithValue("@ITIIFIC_CC", dgvr.Cells("ITIIFIC_CC").Value)

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

                            strSQL = " UPDATE [InverterTesterInst_X_InverterFotovInst_X_Config] "
                            strSQL = strSQL + " SET ITIIFIC_ITI_ID = @ITIIFIC_ITI_ID, ITIIFIC_IT_ID = @ITIIFIC_IT_ID, ITIIFIC_IFI_ID = @ITIIFIC_IFI_ID, ITIIFIC_Descrizione = @ITIIFIC_Descrizione, ITIIFIC_DataOra = @ITIIFIC_DataOra, ITIIFIC_U_ID = @ITIIFIC_U_ID "
                            strSQL = strSQL + " WHERE ITIIFIC_ID = @ITIIFIC_ID AND ITIIFIC_CC = @ITIIFIC_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("ITIIFIC_ITI_ID") = Me.ComboBox_ITI_ID.SelectedValue()
                            dr.Item("ITIIFIC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("ITIIFIC_IFI_ID") = Me.ComboBox_IFI_ID.SelectedValue()
                            dr.Item("ITIIFIC_Descrizione") = Me.TextBox_ITIIFIC_Descrizione.Text
                            dr.Item("ITIIFIC_DataOra") = Date.Now
                            dr.Item("ITIIFIC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@ITIIFIC_ID", dr.Item("ITIIFIC_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_CC", dr.Item("ITIIFIC_CC"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_ITI_ID", dr.Item("ITIIFIC_ITI_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_IT_ID", dr.Item("ITIIFIC_IT_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_IFI_ID", dr.Item("ITIIFIC_IFI_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_Descrizione", dr.Item("ITIIFIC_Descrizione"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_DataOra", dr.Item("ITIIFIC_DataOra"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [InverterTesterInst_X_InverterFotovInst_X_Config] "
                            strSQL = strSQL + "  (ITIIFIC_ITI_ID,  ITIIFIC_IT_ID,  ITIIFIC_IFI_ID,  ITIIFIC_Descrizione,  ITIIFIC_DataOra,  ITIIFIC_U_ID) VALUES "
                            strSQL = strSQL + "  (@ITIIFIC_ITI_ID, @ITIIFIC_IT_ID, @ITIIFIC_IFI_ID, @ITIIFIC_Descrizione, @ITIIFIC_DataOra, @ITIIFIC_U_ID) "
                            strSQL = strSQL + " SELECT @ITIIFIC_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("ITIIFIC_ITI_ID") = Me.ComboBox_ITI_ID.SelectedValue()
                            dr.Item("ITIIFIC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("ITIIFIC_IFI_ID") = Me.ComboBox_IFI_ID.SelectedValue()
                            dr.Item("ITIIFIC_Descrizione") = Me.TextBox_ITIIFIC_Descrizione.Text
                            dr.Item("ITIIFIC_DataOra") = Date.Now
                            dr.Item("ITIIFIC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@ITIIFIC_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@ITIIFIC_ITI_ID", dr.Item("ITIIFIC_ITI_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_IT_ID", dr.Item("ITIIFIC_IT_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_IFI_ID", dr.Item("ITIIFIC_IFI_ID"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_Descrizione", dr.Item("ITIIFIC_Descrizione"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_DataOra", dr.Item("ITIIFIC_DataOra"))
                            cmd.Parameters.AddWithValue("@ITIIFIC_U_ID", dr.Item("ITIIFIC_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("ITIIFIC_ID") = cmd.Parameters.Item("@ITIIFIC_ID").Value
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
            strSQL = " SELECT ITIIFIC_ID, ITIIFIC_CC, C_ID, C_Nome, IDP_ID, IDP_Nome, CDPI_ID, CDPI_Nr, IFI_ID, IFI_Nr, LI_ID, ITI_ID, ITI_Indirizzo_Modbus, INT_Modello, INT_Tipo, LI_Nr, IT_ID, IT_Nome, InverterTesterInst_X_InverterFotovInst_X_Config.*, U_NomeCognome"
            strSQL = strSQL + " FROM Cliente INNER JOIN ImpiantoDiProduzione ON C_ID = IDP_C_ID INNER JOIN ContatoreDiProduzioneInst ON IDP_ID = CDPI_IDP_ID INNER JOIN InverterFotovInst ON CDPI_ID = IFI_CDPI_ID INNER JOIN LoggerInst ON IDP_ID = LI_IDP_ID INNER JOIN InverterTesterInst ON LI_ID = ITI_LI_ID INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON IFI_ID = ITIIFIC_IFI_ID AND ITI_ID = ITIIFIC_ITI_ID INNER JOIN IngressoTipo ON ITIIFIC_IT_ID = IT_ID "
            strSQL = strSQL + " INNER JOIN InverterTester ON InverterTesterInst.ITI_INT_ID = InverterTester.INT_ID "
            strSQL = strSQL + " INNER JOIN Utente ON ITIIFIC_U_ID = U_ID "
            strSQL = strSQL + " WHERE IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue.ToString()
            strSQL = strSQL + " ORDER BY CDPI_Nr, IFI_Nr, ITI_Indirizzo_Modbus, LI_Nr, IT_ID "

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

    Private Sub CaricaCDPIID()

        If (Me.ComboBox_CDPI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_CDPI_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT CDPI_ID, CDPI_Nr "
            strSQL = strSQL + " FROM ContatoreDiProduzioneInst "
            If Me.ComboBox_IDP_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE CDPI_IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE CDPI_IDP_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_CDPI_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "CDPI_Nr"
                        .ValueMember = "CDPI_ID"
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

        If (Me.ComboBox_CDPI_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_CDPI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "CDPI_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_CDPI_ID.Items.Count > 0 Then
            If ComboBox_CDPI_ID.SelectedIndex = -1 Then
                ComboBox_CDPI_ID.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub CaricaIFIID()

        If (Me.ComboBox_IFI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IFI_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT IFI_ID, IFI_Nr  "
            strSQL = strSQL + " FROM InverterFotovInst "
            If Me.ComboBox_CDPI_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE IFI_CDPI_ID = " + Me.ComboBox_CDPI_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE IFI_CDPI_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_IFI_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "IFI_Nr"
                        .ValueMember = "IFI_ID"
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

        If (Me.ComboBox_IFI_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_IFI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IFI_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_IFI_ID.Items.Count > 0 Then
            If ComboBox_IFI_ID.SelectedIndex = -1 Then
                ComboBox_IFI_ID.SelectedIndex = 0
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

    Private Sub CaricaITIID()

        If (Me.ComboBox_ITI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_ITI_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT ITI_ID, ITI_Indirizzo_Modbus, CONVERT(nvarchar(512), ITI_Indirizzo_Modbus) + ' - ' + CONVERT(nvarchar(512), INT_Modello) + ' - ' + CONVERT(nvarchar(512), INT_Tipo) as Tipo "
            strSQL = strSQL + " FROM InverterTesterInst "
            strSQL = strSQL + " INNER JOIN InverterTester ON InverterTesterInst.ITI_INT_ID = InverterTester.INT_ID "

            If Me.ComboBox_LI_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE ITI_LI_ID = " + Me.ComboBox_LI_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE ITI_LI_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_ITI_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "Tipo"
                        .ValueMember = "ITI_ID"
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

        If (Me.ComboBox_ITI_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_ITI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "ITI_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_ITI_ID.Items.Count > 0 Then
            If ComboBox_ITI_ID.SelectedIndex = -1 Then
                ComboBox_ITI_ID.SelectedIndex = 0
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
            If Me.ComboBox_ITI_ID.Items.Count > 0 Then
                If Me.ComboBox_ITI_ID.Text.Contains("IT - 21") = True Then
                    strSQL = strSQL + " WHERE IT_ID >= 411 AND IT_ID <= 456 "
                End If
                If Me.ComboBox_ITI_ID.Text.Contains("IT - 22") = True Then
                    strSQL = strSQL + " WHERE IT_ID >= 471 AND IT_ID <= 516 "
                End If
                If Me.ComboBox_ITI_ID.Text.Contains("IT - 23") = True Then
                    strSQL = strSQL + " WHERE IT_ID >= 531 AND IT_ID <= 576 "
                End If
                If Me.ComboBox_ITI_ID.Text.Contains("IT - 24") = True Then
                    strSQL = strSQL + " WHERE IT_ID >= 591 AND IT_ID <= 636 "
                End If
            End If

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
            Me.Label_CDPI_ID.Text = m_bs.DataSource.Columns("CDPI_Nr").Caption
            Me.Label_IFI_ID.Text = m_bs.DataSource.Columns("IFI_Nr").Caption
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_ITI_ID.Text = m_bs.DataSource.Columns("ITI_Indirizzo_Modbus").Caption
            Me.Label_IT_ID.Text = m_bs.DataSource.Columns("IT_Nome").Caption
            Me.Label_ITIIFIC_Descrizione.Text = m_bs.DataSource.Columns("ITIIFIC_Descrizione").Caption
        End If
        'If (Me.ComboBox_C_ID.DataBindings.Count = 0) Then
        '    Me.ComboBox_C_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "C_ID", True))
        'End If
        'If (Me.ComboBox_IDP_ID.DataBindings.Count = 0) Then
        '    Me.ComboBox_IDP_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IDP_ID", True))
        'End If
        If (Me.ComboBox_CDPI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_CDPI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "CDPI_ID", True))
        End If
        If (Me.ComboBox_IFI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_IFI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IFI_ID", True))
        End If
        If (Me.ComboBox_LI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_LI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "LI_ID", True))
        End If
        If (Me.ComboBox_ITI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_ITI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "ITI_ID", True))
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count = 0) Then
            Me.ComboBox_IT_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IT_ID", True))
        End If
        If (Me.TextBox_ITIIFIC_Descrizione.DataBindings.Count = 0) Then
            Me.TextBox_ITIIFIC_Descrizione.DataBindings.Add(New Binding("Text", m_bs, "ITIIFIC_Descrizione", True))
        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_CDPI_ID.Enabled = False
        Me.ComboBox_IFI_ID.Enabled = False
        Me.ComboBox_LI_ID.Enabled = False
        Me.ComboBox_ITI_ID.Enabled = False
        Me.ComboBox_IT_ID.Enabled = False
        Me.TextBox_ITIIFIC_Descrizione.Enabled = False

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
        If (Me.ComboBox_CDPI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_CDPI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_IFI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IFI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_LI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_LI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_ITI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_ITI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IT_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_ITIIFIC_Descrizione.DataBindings.Count > 0) Then
            Me.TextBox_ITIIFIC_Descrizione.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False
        Me.ComboBox_CDPI_ID.Enabled = True
        Me.ComboBox_IFI_ID.Enabled = True
        Me.ComboBox_LI_ID.Enabled = True
        Me.ComboBox_ITI_ID.Enabled = True
        Me.ComboBox_IT_ID.Enabled = True
        Me.TextBox_ITIIFIC_Descrizione.Enabled = True

        Me.Button_Aggungi_Tutti_In.Enabled = True

        Me.Button_Elimina_Dati.Enabled = False

    End Sub

    Private Sub Button_Aggungi_Tutti_In_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Aggungi_Tutti_In.Click
        Dim iITIINTID As Integer
        Dim iINTID As Integer

        iITIINTID = CInt(GENERICA_DESCRIZIONE("ITI_INT_ID", "InverterTesterInst", "ITI_ID", Me.ComboBox_ITI_ID.SelectedValue(), m_iUID))
        iINTID = CInt(GENERICA_DESCRIZIONE("INT_Tipo", "InverterTester", "INT_ID", iITIINTID, m_iUID))

        If AddAllInputInverterTester(iINTID, ComboBox_ITI_ID.SelectedValue(), ComboBox_IFI_ID.SelectedValue(), m_iUID) = True Then
            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
        Else
            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_ADD, RISULTATO_ERR, "", "", "", m_iUID, Me)
        End If
        CaricaDati()

        EseguiBinding()

        MyBase.ToolStripButton_Salva_Click(sender, e)
    End Sub

End Class