Imports System.Data.SqlClient

Public Class StringTesterInst_X_PannelloFotovString_X_Config
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

        Me.Text = "Associa String Tester Installati a Pannelli Fotovoltaici Stringa"

        CaricaCID()

        CaricaIDPID()

        CaricaDati()

        CaricaCDPIID()

        CaricaIFIID()

        CaricaPFSID()


        CaricaLIID()

        CaricaSTIID()

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
            CaricaPFSID()

            CaricaLIID()
            CaricaSTIID()
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
            CaricaPFSID()

            CaricaLIID()
            CaricaSTIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_CDPI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_CDPI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaIFIID()
            'CaricaPFSID()

            'CaricaLIID()
            'CaricaSTIID()
        End If

    End Sub

    Private Sub ComboBox_IFI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IFI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaPFSID()

            'CaricaLIID()
            'CaricaSTIID()
        End If

    End Sub

    Private Sub ComboBox_LI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_LI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaLIID()
            CaricaSTIID()
            CaricaITID()
        End If

    End Sub

    Private Sub ComboBox_STI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_STI_ID.SelectedIndexChanged
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

    Private Sub Button_Elimina_Dati_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Elimina_Dati.Click

        If Windows.Forms.MessageBox.Show("Sei Sicuro? Premendo su Ok verranno cancellati tutti i dati relativi all'associazione selezionata.", "Eliminazione dati String Tester", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand

            If Not m_bs.DataSource Is Nothing Then

                If Not m_bs.Current() Is Nothing Then

                    Try

                        CustomSQLConnectionOpen(cn, cmd)
                        'cmd.Connection = cn

                        strSQL = "DELETE FROM [StringTesterInst_X_PannelloFotovString_X_Valore] "
                        strSQL = strSQL + "WHERE STIPFSV_STIPFSC_ID = @STIPFSV_STIPFSC_ID "

                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@STIPFSV_STIPFSC_ID", m_bs.Current().Row().Item("STIPFSC_ID"))

                        If cmd.ExecuteNonQuery() > 0 Then
                            ScriviLogEventi(ComboBox_LI_ID.SelectedValue(), 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione di tutti i valori dell' associazione con lo String Tester.", "", "", m_iUID, Me)
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

                                strSQL = "DELETE FROM [StringTesterInst_X_PannelloFotovString_X_Config] "
                                strSQL = strSQL + "WHERE STIPFSC_ID = @STIPFSC_ID AND STIPFSC_CC = @STIPFSC_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@STIPFSC_ID", dgvr.Cells("STIPFSC_ID").Value)
                                cmd.Parameters.AddWithValue("@STIPFSC_CC", dgvr.Cells("STIPFSC_CC").Value)

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

                            strSQL = " UPDATE [StringTesterInst_X_PannelloFotovString_X_Config] "
                            strSQL = strSQL + " SET STIPFSC_STI_ID = @STIPFSC_STI_ID, STIPFSC_IT_ID = @STIPFSC_IT_ID, STIPFSC_PFS_ID = @STIPFSC_PFS_ID, STIPFSC_Descrizione = @STIPFSC_Descrizione, STIPFSC_DataOra = @STIPFSC_DataOra, STIPFSC_U_ID = @STIPFSC_U_ID "
                            strSQL = strSQL + " WHERE STIPFSC_ID = @STIPFSC_ID AND STIPFSC_CC = @STIPFSC_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("STIPFSC_STI_ID") = Me.ComboBox_STI_ID.SelectedValue()
                            dr.Item("STIPFSC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("STIPFSC_PFS_ID") = Me.ComboBox_PFS_ID.SelectedValue()
                            dr.Item("STIPFSC_Descrizione") = Me.TextBox_STIPFSC_Descrizione.Text
                            dr.Item("STIPFSC_DataOra") = Date.Now
                            dr.Item("STIPFSC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@STIPFSC_ID", dr.Item("STIPFSC_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_CC", dr.Item("STIPFSC_CC"))
                            cmd.Parameters.AddWithValue("@STIPFSC_STI_ID", dr.Item("STIPFSC_STI_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_IT_ID", dr.Item("STIPFSC_IT_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_PFS_ID", dr.Item("STIPFSC_PFS_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_Descrizione", dr.Item("STIPFSC_Descrizione"))
                            cmd.Parameters.AddWithValue("@STIPFSC_DataOra", dr.Item("STIPFSC_DataOra"))
                            cmd.Parameters.AddWithValue("@STIPFSC_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [StringTesterInst_X_PannelloFotovString_X_Config] "
                            strSQL = strSQL + "  (STIPFSC_STI_ID,  STIPFSC_IT_ID,  STIPFSC_PFS_ID,  STIPFSC_Descrizione,  STIPFSC_DataOra,  STIPFSC_U_ID) VALUES "
                            strSQL = strSQL + "  (@STIPFSC_STI_ID, @STIPFSC_IT_ID, @STIPFSC_PFS_ID, @STIPFSC_Descrizione, @STIPFSC_DataOra, @STIPFSC_U_ID) "
                            strSQL = strSQL + " SELECT @STIPFSC_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("STIPFSC_STI_ID") = Me.ComboBox_STI_ID.SelectedValue()
                            dr.Item("STIPFSC_IT_ID") = Me.ComboBox_IT_ID.SelectedValue()
                            dr.Item("STIPFSC_PFS_ID") = Me.ComboBox_PFS_ID.SelectedValue()
                            dr.Item("STIPFSC_Descrizione") = Me.TextBox_STIPFSC_Descrizione.Text
                            dr.Item("STIPFSC_DataOra") = Date.Now
                            dr.Item("STIPFSC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@STIPFSC_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@STIPFSC_STI_ID", dr.Item("STIPFSC_STI_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_IT_ID", dr.Item("STIPFSC_IT_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_PFS_ID", dr.Item("STIPFSC_PFS_ID"))
                            cmd.Parameters.AddWithValue("@STIPFSC_Descrizione", dr.Item("STIPFSC_Descrizione"))
                            cmd.Parameters.AddWithValue("@STIPFSC_DataOra", dr.Item("STIPFSC_DataOra"))
                            cmd.Parameters.AddWithValue("@STIPFSC_U_ID", dr.Item("STIPFSC_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("STIPFSC_ID") = cmd.Parameters.Item("@STIPFSC_ID").Value
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
            strSQL = " SELECT STIPFSC_ID, STIPFSC_CC, C_ID, C_Nome, IDP_ID, IDP_Nome, CDPI_ID, CDPI_Nr, IFI_ID, IFI_Nr, PFS_ID, PFS_Nr, LI_ID, STI_ID, STI_Indirizzo_Modbus, LI_Nr, IT_ID, IT_Nome, StringTesterInst_X_PannelloFotovString_X_Config.*, U_NomeCognome"
            strSQL = strSQL + " FROM Cliente INNER JOIN ImpiantoDiProduzione ON C_ID = IDP_C_ID INNER JOIN ContatoreDiProduzioneInst ON IDP_ID = CDPI_IDP_ID INNER JOIN InverterFotovInst ON CDPI_ID = IFI_CDPI_ID INNER JOIN PannelloFotovString ON IFI_ID = PFS_IFI_ID INNER JOIN LoggerInst ON IDP_ID = LI_IDP_ID INNER JOIN StringTesterInst ON LI_ID = STI_LI_ID INNER JOIN StringTesterInst_X_PannelloFotovString_X_Config ON PFS_ID = STIPFSC_PFS_ID AND STI_ID = STIPFSC_STI_ID INNER JOIN IngressoTipo ON STIPFSC_IT_ID = IT_ID "
            strSQL = strSQL + " INNER JOIN Utente ON STIPFSC_U_ID = U_ID "
            strSQL = strSQL + " WHERE IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue.ToString()
            strSQL = strSQL + " ORDER BY CDPI_Nr, IFI_Nr, PFS_Nr, STI_Indirizzo_Modbus, LI_Nr, IT_ID "


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

    Private Sub CaricaPFSID()

        If (Me.ComboBox_PFS_ID.DataBindings.Count > 0) Then
            Me.ComboBox_PFS_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT PFS_ID, PFS_Nr  "
            strSQL = strSQL + " FROM PannelloFotovString "
            If Me.ComboBox_IFI_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE PFS_IFI_ID = " + Me.ComboBox_IFI_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE PFS_IFI_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_PFS_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "PFS_Nr"
                        .ValueMember = "PFS_ID"
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

        If (Me.ComboBox_PFS_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_PFS_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "PFS_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_PFS_ID.Items.Count > 0 Then
            If ComboBox_PFS_ID.SelectedIndex = -1 Then
                ComboBox_PFS_ID.SelectedIndex = 0
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

    Private Sub CaricaSTIID()

        If (Me.ComboBox_STI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_STI_ID.DataBindings.Clear()
        End If

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT STI_ID, STI_Indirizzo_Modbus "
            strSQL = strSQL + " FROM StringTesterInst "
            If Me.ComboBox_LI_ID.Items.Count > 0 Then
                strSQL = strSQL + " WHERE STI_LI_ID = " + Me.ComboBox_LI_ID.SelectedValue().ToString
            Else
                strSQL = strSQL + " WHERE STI_LI_ID = 0 "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_STI_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "STI_Indirizzo_Modbus"
                        .ValueMember = "STI_ID"
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

        If (Me.ComboBox_STI_ID.DataBindings.Count = 0) Then
            Try
                Me.ComboBox_STI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "STI_ID", True))
            Catch ex As Exception

            End Try
        End If
        If ComboBox_STI_ID.Items.Count > 0 Then
            If ComboBox_STI_ID.SelectedIndex = -1 Then
                ComboBox_STI_ID.SelectedIndex = 0
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
            strSQL = strSQL + " WHERE IT_ID >= 201 AND IT_ID <= 396 "

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
            Me.Label_PFS_ID.Text = m_bs.DataSource.Columns("PFS_Nr").Caption
            Me.Label_STIPFSC_Descrizione.Text = m_bs.DataSource.Columns("STIPFSC_Descrizione").Caption
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_STI_ID.Text = m_bs.DataSource.Columns("STI_Indirizzo_Modbus").Caption
            Me.Label_IT_ID.Text = m_bs.DataSource.Columns("IT_Nome").Caption
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
        If (Me.ComboBox_PFS_ID.DataBindings.Count = 0) Then
            Me.ComboBox_PFS_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "PFS_ID", True))
        End If
        If (Me.TextBox_STIPFSC_Descrizione.DataBindings.Count = 0) Then
            Me.TextBox_STIPFSC_Descrizione.DataBindings.Add(New Binding("Text", m_bs, "STIPFSC_Descrizione", True))
        End If
        If (Me.ComboBox_LI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_LI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "LI_ID", True))
        End If
        If (Me.ComboBox_STI_ID.DataBindings.Count = 0) Then
            Me.ComboBox_STI_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "STI_ID", True))
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count = 0) Then
            Me.ComboBox_IT_ID.DataBindings.Add(New Binding("SelectedValue", m_bs, "IT_ID", True))
        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_CDPI_ID.Enabled = False
        Me.ComboBox_IFI_ID.Enabled = False
        Me.ComboBox_PFS_ID.Enabled = False
        Me.TextBox_STIPFSC_Descrizione.Enabled = False
        Me.ComboBox_LI_ID.Enabled = False
        Me.ComboBox_STI_ID.Enabled = False
        Me.ComboBox_IT_ID.Enabled = False

        Me.Button_Aggungi_Tutti_In.Enabled = False
        Me.CheckBox_Salta_ST_su_cambio_Nr_Inverter.Enabled = False

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
        If (Me.ComboBox_PFS_ID.DataBindings.Count > 0) Then
            Me.ComboBox_PFS_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_STIPFSC_Descrizione.DataBindings.Count > 0) Then
            Me.TextBox_STIPFSC_Descrizione.DataBindings.Clear()
        End If
        If (Me.ComboBox_LI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_LI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_STI_ID.DataBindings.Count > 0) Then
            Me.ComboBox_STI_ID.DataBindings.Clear()
        End If
        If (Me.ComboBox_IT_ID.DataBindings.Count > 0) Then
            Me.ComboBox_IT_ID.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False
        Me.ComboBox_CDPI_ID.Enabled = True
        Me.ComboBox_IFI_ID.Enabled = True
        Me.ComboBox_PFS_ID.Enabled = True
        Me.TextBox_STIPFSC_Descrizione.Enabled = True
        Me.ComboBox_LI_ID.Enabled = True
        Me.ComboBox_STI_ID.Enabled = True
        Me.ComboBox_IT_ID.Enabled = True

        Me.Button_Aggungi_Tutti_In.Enabled = True
        Me.CheckBox_Salta_ST_su_cambio_Nr_Inverter.Enabled = True

        Me.Button_Elimina_Dati.Enabled = False

    End Sub

    Private Sub Button_Aggungi_Tutti_In_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Aggungi_Tutti_In.Click

        Dim bNoPFS As Boolean
        Dim strSQL_PFS As String
        Dim cn_PFS As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd_PFS As New SqlCommand
        Dim da_PFS As New SqlDataAdapter
        Dim ds_PFS As New DataSet

        Dim iIFINr As Integer
        Dim iIFIPrecNr As Integer
        Dim iPFSProgr As Integer

        Dim bNoSTI As Boolean
        Dim iSTINrIn As Integer
        Dim strSQL_STI As String
        Dim cn_STI As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd_STI As New SqlCommand
        Dim da_STI As New SqlDataAdapter
        Dim ds_STI As New DataSet

        Try
            strSQL_PFS = " SELECT IFI_Nr, PFS_ID, PFS_Nr  "
            strSQL_PFS = strSQL_PFS + " FROM PannelloFotovString "
            strSQL_PFS = strSQL_PFS + " INNER JOIN InverterFotovInst ON PannelloFotovString.PFS_IFI_ID = InverterFotovInst.IFI_ID "
            strSQL_PFS = strSQL_PFS + " INNER JOIN ContatoreDiProduzioneInst ON InverterFotovInst.IFI_CDPI_ID = ContatoreDiProduzioneInst.CDPI_ID "
            strSQL_PFS = strSQL_PFS + " INNER JOIN ImpiantoDiProduzione ON ContatoreDiProduzioneInst.CDPI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            If Me.ComboBox_IDP_ID.Items.Count > 0 Then
                strSQL_PFS = strSQL_PFS + " WHERE ImpiantoDiProduzione.IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue().ToString
            Else
                strSQL_PFS = strSQL_PFS + " WHERE ImpiantoDiProduzione.IDP_ID = 0 "
            End If
            strSQL_PFS = strSQL_PFS + " ORDER BY IFI_Nr ASC, PFS_Nr ASC "

            CustomSQLConnectionOpen(cn_PFS, cmd_PFS)
            'cmd_PFS.Connection = cn_PFS
            cmd_PFS.CommandText = strSQL_PFS

            da_PFS.SelectCommand = cmd_PFS
            da_PFS.Fill(ds_PFS)


            strSQL_STI = " SELECT STI_ID, STI_Indirizzo_Modbus "
            strSQL_STI = strSQL_STI + " FROM StringTesterInst "
            strSQL_STI = strSQL_STI + " INNER JOIN LoggerInst ON StringTesterInst.STI_LI_ID = LoggerInst.LI_ID "
            strSQL_STI = strSQL_STI + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            If Me.ComboBox_IDP_ID.Items.Count > 0 Then
                strSQL_STI = strSQL_STI + " WHERE ImpiantoDiProduzione.IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue().ToString
            Else
                strSQL_STI = strSQL_STI + " WHERE ImpiantoDiProduzione.IDP_ID = 0 "
            End If
            strSQL_STI = strSQL_STI + " ORDER BY LI_Nr ASC, STI_Indirizzo_Modbus ASC "


            CustomSQLConnectionOpen(cn_STI, cmd_STI)
            'cmd_STI.Connection = cn_STI
            cmd_STI.CommandText = strSQL_STI

            da_STI.SelectCommand = cmd_STI
            da_STI.Fill(ds_STI)

            If Not ds_PFS Is Nothing Then
                If ds_PFS.Tables.Count > 0 Then
                    If ds_PFS.Tables(0).Rows.Count > 0 Then

                        If Not ds_STI Is Nothing Then
                            If ds_STI.Tables.Count > 0 Then
                                If ds_STI.Tables(0).Rows.Count > 0 Then

                                    iPFSProgr = 0
                                    For Each dr_STI As DataRow In ds_STI.Tables(0).Rows
                                        iSTINrIn = 1
                                        If iPFSProgr <= (ds_PFS.Tables(0).Rows.Count - 1) Then
                                            iIFIPrecNr = ds_PFS.Tables(0).Rows(iPFSProgr).Item("IFI_Nr")
                                        End If
                                        For iPFSProgr = iPFSProgr To (ds_PFS.Tables(0).Rows.Count - 1)
                                            If iSTINrIn > 8 Then
                                                iSTINrIn = 1
                                                Exit For
                                            End If
                                            ' Se cambia il Nr dell'inverter, ricomincio dalla prima scheda ST successiva
                                            iIFINr = ds_PFS.Tables(0).Rows(iPFSProgr).Item("IFI_Nr")
                                            If iIFIPrecNr <> iIFINr Then
                                                iIFIPrecNr = iIFINr
                                                If Me.CheckBox_Salta_ST_su_cambio_Nr_Inverter.Checked = True Then
                                                    Exit For
                                                End If
                                            End If

                                            AddAllInputStringTester(iSTINrIn, dr_STI("STI_ID"), ds_PFS.Tables(0).Rows(iPFSProgr).Item("PFS_ID"), m_iUID)
                                            iSTINrIn = iSTINrIn + 1

                                        Next
                                    Next

                                    ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "Tutti gli ingressi degli String Tester sono stati assegnati Automaticamente alla propria Stringa di Pannelli Fotovoltaici. Controllare tuttavia che non ci siano Stringhe non assegnate oppure Ingressi liberi negli String Tester. Correggere eventualmente a mano. ", "", "", m_iUID, Me)

                                End If
                            End If
                        End If

                    End If
                End If
            End If

            If Not ds_STI Is Nothing Then
                If ds_STI.Tables.Count > 0 Then
                    If ds_STI.Tables(0).Rows.Count > 0 Then
                    Else
                        bNoSTI = True
                    End If
                Else
                    bNoSTI = True
                End If
            Else
                bNoSTI = True
            End If
            If Not ds_PFS Is Nothing Then
                If ds_PFS.Tables.Count > 0 Then
                    If ds_PFS.Tables(0).Rows.Count > 0 Then
                    Else
                        bNoPFS = True
                    End If
                Else
                    bNoPFS = True
                End If
            Else
                bNoPFS = True
            End If

            If bNoSTI = True Then
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "ATTENZIONE, Non ci sono String Tester Configurati", "", "", m_iUID, Me)
            End If

            If bNoPFS = True Then
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "ATTENZIONE, Non ci sono Stringhe di Pannelli Fotovoltaici Configurate.", "", "", m_iUID, Me)
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        ds_STI.Dispose()
        da_STI.Dispose()
        cmd_STI.Dispose()
        cn_STI.Close()
        cn_STI.Dispose()

        ds_PFS.Dispose()
        da_PFS.Dispose()
        cmd_PFS.Dispose()
        cn_PFS.Close()
        cn_PFS.Dispose()

        CaricaDati()

        EseguiBinding()

        MyBase.ToolStripButton_Salva_Click(sender, e)
    End Sub

End Class
