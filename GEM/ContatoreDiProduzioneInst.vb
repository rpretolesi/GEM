Imports System.Data.SqlClient

Public Class ContatoreDiProduzioneInst
    Dim m_bs As New BindingSource

    Private m_iCID As Integer
    Private m_iIDPID As Integer

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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Contatori Di Produzione Installati"

        CaricaCID()

        CaricaIDPID()

        CaricaCDPID()

        CaricaDati()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Private Sub ComboBox_C_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_C_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iCID = Me.ComboBox_C_ID.SelectedValue()
            CaricaIDPID()

            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIDPID = Me.ComboBox_IDP_ID.SelectedValue()

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
                            If Not DataGridView.SelectedRows(0).Cells("CDP_TIPO").Value Is DBNull.Value Then
                                Me.ComboBox_CDP_ID.Text = DataGridView.SelectedRows(0).Cells("CDP_TIPO").Value
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
        Try
            If DataGridView.SelectedRows.Count > 0 Then
                If DataGridView.SelectedRows(0).Index >= 0 Then
                    If Not m_bs.DataSource Is Nothing Then
                        If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("CDPI_ID").Value Is DBNull.Value Then

                                Dim lg As New Login
                                Dim dr As Windows.Forms.DialogResult
                                lg.UID = m_iUID
                                lg.ULivello = GetMinLevelReq("InverterFotovInst")
                                dr = lg.ShowDialog(Me)
                                m_iUID = lg.UID
                                If dr = Windows.Forms.DialogResult.Yes Then
                                    InverterFotovInst.Close()
                                    InverterFotovInst.UID = m_iUID
                                    If m_iCID > 0 Then
                                        InverterFotovInst.CID = m_iCID
                                    Else
                                        InverterFotovInst.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
                                    End If
                                    If m_iIDPID > 0 Then
                                        InverterFotovInst.IDPID = m_iIDPID
                                    Else
                                        InverterFotovInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
                                    End If
                                    InverterFotovInst.CDPIID = DataGridView.SelectedRows(0).Cells("CDPI_ID").Value
                                    InverterFotovInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
                                    InverterFotovInst.MdiParent = Me.MdiParent
                                    InverterFotovInst.Show()
                                End If

                                lg.Dispose()

                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

        MyBase.DataGridView_RowHeaderMouseDoubleClick(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then

            RimuoviBinding()

            Me.TextBox_CDPI_Nr.Text = ""

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

                                strSQL = "DELETE FROM [ContatoreDiProduzioneInst] "
                                strSQL = strSQL + "WHERE CDPI_ID = @CDPI_ID AND CDPI_CC = @CDPI_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@CDPI_ID", dgvr.Cells("CDPI_ID").Value)
                                cmd.Parameters.AddWithValue("@CDPI_CC", dgvr.Cells("CDPI_CC").Value)

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

                            strSQL = " UPDATE [ContatoreDiProduzioneInst] "
                            strSQL = strSQL + " SET CDPI_CDP_ID = @CDPI_CDP_ID, CDPI_IDP_ID = @CDPI_IDP_ID, CDPI_Nr = @CDPI_Nr, CDPI_DataOra = @CDPI_DataOra, CDPI_U_ID = @CDPI_U_ID "
                            strSQL = strSQL + " WHERE CDPI_ID = @CDPI_ID AND CDPI_CC = @CDPI_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("CDPI_CDP_ID") = Me.ComboBox_CDP_ID.SelectedValue()
                            dr.Item("CDPI_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("CDPI_Nr") = Me.TextBox_CDPI_Nr.Text
                            dr.Item("CDPI_DataOra") = Date.Now
                            dr.Item("CDPI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@CDPI_ID", dr.Item("CDPI_ID"))
                            cmd.Parameters.AddWithValue("@CDPI_CC", dr.Item("CDPI_CC"))
                            cmd.Parameters.AddWithValue("@CDPI_CDP_ID", dr.Item("CDPI_CDP_ID"))
                            cmd.Parameters.AddWithValue("@CDPI_IDP_ID", dr.Item("CDPI_IDP_ID"))
                            cmd.Parameters.AddWithValue("@CDPI_Nr", dr.Item("CDPI_Nr"))
                            cmd.Parameters.AddWithValue("@CDPI_DataOra", dr.Item("CDPI_DataOra"))
                            cmd.Parameters.AddWithValue("@CDPI_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [ContatoreDiProduzioneInst] "
                            strSQL = strSQL + "  (CDPI_CDP_ID,  CDPI_IDP_ID,  CDPI_Nr,  CDPI_DataOra,  CDPI_U_ID) VALUES "
                            strSQL = strSQL + "  (@CDPI_CDP_ID, @CDPI_IDP_ID, @CDPI_Nr, @CDPI_DataOra, @CDPI_U_ID) "
                            strSQL = strSQL + " SELECT @CDPI_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("CDPI_CDP_ID") = Me.ComboBox_CDP_ID.SelectedValue()
                            dr.Item("CDPI_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("CDPI_Nr") = Me.TextBox_CDPI_Nr.Text
                            dr.Item("CDPI_DataOra") = Date.Now
                            dr.Item("CDPI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@CDPI_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@CDPI_CDP_ID", dr.Item("CDPI_CDP_ID"))
                            cmd.Parameters.AddWithValue("@CDPI_IDP_ID", dr.Item("CDPI_IDP_ID"))
                            cmd.Parameters.AddWithValue("@CDPI_Nr", dr.Item("CDPI_Nr"))
                            cmd.Parameters.AddWithValue("@CDPI_DataOra", dr.Item("CDPI_DataOra"))
                            cmd.Parameters.AddWithValue("@CDPI_U_ID", dr.Item("CDPI_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("CDPI_ID") = cmd.Parameters.Item("@CDPI_ID").Value
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

        If (Me.TextBox_CDPI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_CDPI_Nr.DataBindings.Clear()
        End If

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

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, ContatoreDiProduzioneInst.*, CDP_Marca + ' - ' + CDP_Modello as CDP_TIPO, U_NomeCognome "
            strSQL = strSQL + " FROM ContatoreDiProduzioneInst INNER JOIN Utente ON CDPI_U_ID = U_ID INNER JOIN ContatoreDiProduzione ON CDPI_CDP_ID = CDP_ID INNER JOIN ImpiantoDiProduzione ON CDPI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
            If m_iCID > 0 Or m_iIDPID > 0 Then
                strSQL = strSQL + " WHERE "
            End If

            If m_iCID > 0 Then
                strSQL = strSQL + " C_ID = " + m_iCID.ToString
                iIndice_1 = iIndice_1 + 1
            End If

            If m_iIDPID > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " IDP_ID = " + m_iIDPID.ToString
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
                            If m_iCID = 0 Then
                                m_iCID = .SelectedValue()
                            End If
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
                            If m_iIDPID = 0 Then
                                m_iIDPID = .SelectedValue()
                            End If
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

    Private Sub CaricaCDPID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT CDP_ID, CDP_Marca, CDP_Modello, CDP_Marca + ' - ' + CDP_Modello as CDP_TIPO "
            strSQL = strSQL + " FROM ContatoreDiProduzione "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_CDP_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "CDP_TIPO"
                        .ValueMember = "CDP_ID"
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
            Me.Label_CDP_ID.Text = m_bs.DataSource.Columns("CDP_TIPO").Caption
            Me.Label_CDPI_Nr.Text = m_bs.DataSource.Columns("CDPI_Nr").Caption

            If (Me.TextBox_CDPI_Nr.DataBindings.Count = 0) Then
                Me.TextBox_CDPI_Nr.DataBindings.Add(New Binding("Text", m_bs, "CDPI_Nr", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_CDP_ID.Enabled = False
        Me.TextBox_CDPI_Nr.ReadOnly = True

        If m_iCID > 0 Then
            Me.ComboBox_C_ID.SelectedValue = m_iCID
        End If
        If m_iIDPID > 0 Then
            Me.ComboBox_IDP_ID.SelectedValue = m_iIDPID
        End If

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_CDPI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_CDPI_Nr.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False

        Me.ComboBox_CDP_ID.Enabled = True
        Me.TextBox_CDPI_Nr.ReadOnly = False
    End Sub
End Class
