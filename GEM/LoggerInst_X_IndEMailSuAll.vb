Imports System.Data.SqlClient

Public Class LoggerInst_X_IndEMailSuAll
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

        Me.Text = "Associa Logger Installati ad EMail per invio Allarmi"

        CaricaCID()

        CaricaIDPID()

        CaricaLIID()

        CaricaDati()


        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Private Sub ComboBox_C_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_C_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaIDPID()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            CaricaLIID()
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
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        MyBase.DataGridView_SelectionChanged(sender, e)

    End Sub

    Protected Overrides Sub DataGridView_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)

        MyBase.DataGridView_RowHeaderMouseDoubleClick(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then
            Dim strParam71 As String = ""
            Dim strParam72 As String = ""
            Dim strParam74 As String = ""
            Dim strParam75 As String = ""
            GetDGI(71, strParam71)
            GetDGI(72, strParam72)
            GetDGI(74, strParam74)
            GetDGI(75, strParam75)

            RimuoviBinding()

            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Text = strParam71
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Text = strParam72
            Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Text = Me.ComboBox_IDP_ID.Text
            Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Text = strParam74
            Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Text = strParam75
            Me.CheckBox_LIIEMSA_InviaAllarme.Checked = True
            Me.CheckBox_LIIEMSA_InviaReport.Checked = True

            Dim dr As DataRow

            dr = m_bs.DataSource.Rows.Add()

            MyBase.ToolStripButton_Nuovo_Click(sender, e)

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

                                strSQL = "DELETE FROM [LoggerInst_X_IndEMailSuAll] "
                                strSQL = strSQL + "WHERE LIIEMSA_ID = @LIIEMSA_ID AND LIIEMSA_CC = @LIIEMSA_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@LIIEMSA_ID", dgvr.Cells("LIIEMSA_ID").Value)
                                cmd.Parameters.AddWithValue("@LIIEMSA_CC", dgvr.Cells("LIIEMSA_CC").Value)

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

                            strSQL = " UPDATE [LoggerInst_X_IndEMailSuAll] "
                            strSQL = strSQL + " SET LIIEMSA_LI_ID = @LIIEMSA_LI_ID, LIIEMSA_EMAIL_INDIRIZZO_MITTENTE = @LIIEMSA_EMAIL_INDIRIZZO_MITTENTE, LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO = @LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO, LIIEMSA_EMAIL_OGGETTO = @LIIEMSA_EMAIL_OGGETTO, LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME = @LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME, LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME = @LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME, LIIEMSA_InviaAllarme = @LIIEMSA_InviaAllarme, LIIEMSA_InviaReport = @LIIEMSA_InviaReport, LIIEMSA_DataOra = @LIIEMSA_DataOra, LIIEMSA_U_ID = @LIIEMSA_U_ID "
                            strSQL = strSQL + " WHERE LIIEMSA_ID = @LIIEMSA_ID AND LIIEMSA_CC = @LIIEMSA_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LIIEMSA_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE") = Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Text
                            dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO") = Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Text
                            dr.Item("LIIEMSA_EMAIL_OGGETTO") = Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Text
                            dr.Item("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME") = Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Text
                            dr.Item("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME") = Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Text
                            dr.Item("LIIEMSA_InviaAllarme") = Me.CheckBox_LIIEMSA_InviaAllarme.Checked
                            dr.Item("LIIEMSA_InviaReport") = Me.CheckBox_LIIEMSA_InviaReport.Checked
                            dr.Item("LIIEMSA_DataOra") = Date.Now
                            dr.Item("LIIEMSA_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LIIEMSA_ID", dr.Item("LIIEMSA_ID"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_CC", dr.Item("LIIEMSA_CC"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_LI_ID", dr.Item("LIIEMSA_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_INDIRIZZO_MITTENTE", dr.Item("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO", dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_OGGETTO", dr.Item("LIIEMSA_EMAIL_OGGETTO"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME", dr.Item("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME", dr.Item("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_InviaAllarme", dr.Item("LIIEMSA_InviaAllarme"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_InviaReport", dr.Item("LIIEMSA_InviaReport"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_DataOra", dr.Item("LIIEMSA_DataOra"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [LoggerInst_X_IndEMailSuAll] "
                            strSQL = strSQL + " (LIIEMSA_LI_ID,  LIIEMSA_EMAIL_INDIRIZZO_MITTENTE,  LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO,  LIIEMSA_EMAIL_OGGETTO,  LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME,  LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME,  LIIEMSA_InviaAllarme,  LIIEMSA_InviaReport,  LIIEMSA_DataOra,  LIIEMSA_U_ID) VALUES "
                            strSQL = strSQL + " (@LIIEMSA_LI_ID, @LIIEMSA_EMAIL_INDIRIZZO_MITTENTE, @LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO, @LIIEMSA_EMAIL_OGGETTO, @LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME, @LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME, @LIIEMSA_InviaAllarme, @LIIEMSA_InviaReport, @LIIEMSA_DataOra, @LIIEMSA_U_ID) "
                            strSQL = strSQL + " SELECT @LIIEMSA_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LIIEMSA_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE") = Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Text
                            dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO") = Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Text
                            dr.Item("LIIEMSA_EMAIL_OGGETTO") = Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Text
                            dr.Item("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME") = Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Text
                            dr.Item("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME") = Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Text
                            dr.Item("LIIEMSA_InviaAllarme") = Me.CheckBox_LIIEMSA_InviaAllarme.Checked
                            dr.Item("LIIEMSA_InviaReport") = Me.CheckBox_LIIEMSA_InviaReport.Checked
                            dr.Item("LIIEMSA_DataOra") = Date.Now
                            dr.Item("LIIEMSA_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@LIIEMSA_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@LIIEMSA_LI_ID", dr.Item("LIIEMSA_LI_ID"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_INDIRIZZO_MITTENTE", dr.Item("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO", dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_OGGETTO", dr.Item("LIIEMSA_EMAIL_OGGETTO"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME", dr.Item("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME", dr.Item("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_InviaAllarme", dr.Item("LIIEMSA_InviaAllarme"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_InviaReport", dr.Item("LIIEMSA_InviaReport"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_DataOra", dr.Item("LIIEMSA_DataOra"))
                            cmd.Parameters.AddWithValue("@LIIEMSA_U_ID", dr.Item("LIIEMSA_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("LIIEMSA_ID") = cmd.Parameters.Item("@LIIEMSA_ID").Value
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

        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Clear()
        End If
        If (Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Count > 0) Then
            Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Clear()
        End If
        If (Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Count > 0) Then
            Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Clear()
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
            strSQL = " SELECT LIIEMSA_ID, LIIEMSA_CC, C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, LoggerInst_X_IndEMailSuAll.*, U_NomeCognome"
            strSQL = strSQL + " FROM Cliente INNER JOIN  ImpiantoDiProduzione ON Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = LoggerInst.LI_IDP_ID INNER JOIN  LoggerInst_X_IndEMailSuAll ON LoggerInst.LI_ID = LoggerInst_X_IndEMailSuAll.LIIEMSA_LI_ID "
            strSQL = strSQL + " INNER JOIN Utente ON LoggerInst_X_IndEMailSuAll.LIIEMSA_U_ID = Utente.U_ID "
            strSQL = strSQL + " WHERE C_ID = " + Me.ComboBox_C_ID.SelectedValue.ToString() + " AND IDP_ID = " + Me.ComboBox_IDP_ID.SelectedValue.ToString() + " AND LI_ID = " + Me.ComboBox_LI_ID.SelectedValue.ToString()

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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
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
            Me.Label_LI_ID.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Text = m_bs.DataSource.Columns("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE").Caption
            Me.Label_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Text = m_bs.DataSource.Columns("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").Caption
            Me.Label_LIIEMSA_EMAIL_OGGETTO.Text = m_bs.DataSource.Columns("LIIEMSA_EMAIL_OGGETTO").Caption
            Me.Label_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Text = m_bs.DataSource.Columns("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME").Caption
            Me.Label_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Text = m_bs.DataSource.Columns("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME").Caption
            Me.CheckBox_LIIEMSA_InviaAllarme.Text = m_bs.DataSource.Columns("LIIEMSA_InviaAllarme").Caption
            Me.CheckBox_LIIEMSA_InviaReport.Text = m_bs.DataSource.Columns("LIIEMSA_InviaReport").Caption
        End If

        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Count = 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Add(New Binding("Text", m_bs, "LIIEMSA_EMAIL_INDIRIZZO_MITTENTE", True))
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Count = 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Add(New Binding("Text", m_bs, "LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO", True))
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Count = 0) Then
            Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Add(New Binding("Text", m_bs, "LIIEMSA_EMAIL_OGGETTO", True))
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Count = 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Add(New Binding("Text", m_bs, "LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME", True))
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Count = 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Add(New Binding("Text", m_bs, "LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME", True))
        End If
        If (Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Count = 0) Then
            Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Add(New Binding("Checked", m_bs, "LIIEMSA_InviaAllarme", True))
        End If
        If (Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Count = 0) Then
            Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Add(New Binding("Checked", m_bs, "LIIEMSA_InviaReport", True))
        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_LI_ID.Enabled = True

        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Enabled = False
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Enabled = False
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Enabled = False
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Enabled = False
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Enabled = False
        Me.CheckBox_LIIEMSA_InviaAllarme.Enabled = False
        Me.CheckBox_LIIEMSA_InviaReport.Enabled = False

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_OGGETTO.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.DataBindings.Clear()
        End If
        If (Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Count > 0) Then
            Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.DataBindings.Clear()
        End If
        If (Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Count > 0) Then
            Me.CheckBox_LIIEMSA_InviaAllarme.DataBindings.Clear()
        End If
        If (Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Count > 0) Then
            Me.CheckBox_LIIEMSA_InviaReport.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False
        Me.ComboBox_LI_ID.Enabled = False

        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_MITTENTE.Enabled = True
        Me.TextBox_LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO.Enabled = True
        Me.TextBox_LIIEMSA_EMAIL_OGGETTO.Enabled = True
        Me.TextBox_LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME.Enabled = True
        Me.TextBox_LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME.Enabled = True
        Me.CheckBox_LIIEMSA_InviaAllarme.Enabled = True
        Me.CheckBox_LIIEMSA_InviaReport.Enabled = True

    End Sub

    Private Sub Button_Invia_EMail_Di_Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Invia_EMail_Di_Test.Click
        Dim strParam76 As String = ""
        If GetDGI(76, strParam76) = True Then
            SendEMailReportAllarmiDL(Me.ComboBox_LI_ID.SelectedValue(), strParam76, "Questo e' un messaggio di Test per verificare la connettivita' della Vs email in caso di allarme al Vs Impianto di Produzione.", "", True, True)
        Else
            ScriviLogEventi(Me.ComboBox_LI_ID.SelectedValue(), 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 76, Prefisso Oggetto In caso di Test invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End If

        'Dim strTestoAllarme As String = GetDataLoggerAllarmi(ComboBox_LI_ID.SelectedValue())
        'Dim strParam78 As String = ""
        'If strTestoAllarme.Count > 0 Then
        '    GetDGI(78, strParam78)
        '    SendEMailByLIID(ComboBox_LI_ID.SelectedValue(), strParam78, strTestoAllarme, True, False)
        'End If

    End Sub
End Class
