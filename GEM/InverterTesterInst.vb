Imports System.Data.SqlClient

Public Class InverterTesterInst
    Dim m_bs As New BindingSource

    Private m_iCID As Integer
    Private m_iIDPID As Integer
    Private m_iLIID As Integer

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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Inverter Tester Installati"

        CaricaCID()

        CaricaIDPID()

        CaricaLIID()

        CaricaSTID()

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
            CaricaLIID()
            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIDPID = Me.ComboBox_IDP_ID.SelectedValue()
            CaricaLIID()
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
                                Me.ComboBox_IDP_ID.Text = DataGridView.SelectedRows(0).Cells("LI_Nr").Value
                            End If
                            If Not DataGridView.SelectedRows(0).Cells("TIPO").Value Is DBNull.Value Then
                                Me.ComboBox_INT_ID.Text = DataGridView.SelectedRows(0).Cells("TIPO").Value
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
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("LI_ID").Value Is DBNull.Value Then

                                Dim lg As New Login
                                Dim dr As Windows.Forms.DialogResult
                                lg.UID = m_iUID
                                lg.ULivello = GetMinLevelReq("StringTesterInst")
                                dr = lg.ShowDialog()
                                m_iUID = lg.UID
                                If dr = Windows.Forms.DialogResult.Yes Then
                                    StringTesterInst.Close()
                                    StringTesterInst.UID = m_iUID
                                    If m_iCID > 0 Then
                                        StringTesterInst.CID = m_iCID
                                    Else
                                        StringTesterInst.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
                                    End If
                                    If m_iIDPID > 0 Then
                                        StringTesterInst.IDPID = m_iIDPID
                                    Else
                                        StringTesterInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
                                    End If

                                    StringTesterInst.LIID = DataGridView.SelectedRows(0).Cells("LI_ID").Value

                                    StringTesterInst.MdiParent = Me.MdiParent
                                    StringTesterInst.Show()
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

            Me.TextBox_ITIRel.Text = ""
            Me.TextBox_ITI_Indirizzo_Modbus.Text = ""

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

                                strSQL = "DELETE FROM [InverterTesterInst] "
                                strSQL = strSQL + "WHERE ITI_ID = @ITI_ID AND ITI_CC = @ITI_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@ITI_ID", dgvr.Cells("ITI_ID").Value)
                                cmd.Parameters.AddWithValue("@ITI_CC", dgvr.Cells("ITI_CC").Value)

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

                            strSQL = " UPDATE [InverterTesterInst] "
                            strSQL = strSQL + " SET ITI_INT_ID = @ITI_INT_ID, ITI_LI_ID = @ITI_LI_ID, ITI_Rel = @ITI_Rel, ITI_Indirizzo_Modbus = @ITI_Indirizzo_Modbus, ITI_DataOra = @ITI_DataOra, ITI_U_ID = @ITI_U_ID "
                            strSQL = strSQL + " WHERE ITI_ID = @ITI_ID AND ITI_CC = @ITI_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("ITI_INT_ID") = Me.ComboBox_INT_ID.SelectedValue()
                            dr.Item("ITI_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("ITI_Rel") = Me.TextBox_ITIRel.Text
                            dr.Item("ITI_Indirizzo_Modbus") = Me.TextBox_ITI_Indirizzo_Modbus.Text
                            dr.Item("ITI_DataOra") = Date.Now
                            dr.Item("ITI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@ITI_ID", dr.Item("ITI_ID"))
                            cmd.Parameters.AddWithValue("@ITI_CC", dr.Item("ITI_CC"))
                            cmd.Parameters.AddWithValue("@ITI_INT_ID", dr.Item("ITI_INT_ID"))
                            cmd.Parameters.AddWithValue("@ITI_LI_ID", dr.Item("ITI_LI_ID"))
                            cmd.Parameters.AddWithValue("@ITI_Rel", dr.Item("ITI_Rel"))
                            cmd.Parameters.AddWithValue("@ITI_Indirizzo_Modbus", dr.Item("ITI_Indirizzo_Modbus"))
                            cmd.Parameters.AddWithValue("@ITI_DataOra", dr.Item("ITI_DataOra"))
                            cmd.Parameters.AddWithValue("@ITI_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            If IsModbusAddressAlreadyPresent(Me.ComboBox_LI_ID.SelectedValue(), Me.TextBox_ITI_Indirizzo_Modbus.Text) = False Then

                                strSQL = " INSERT INTO [InverterTesterInst] "
                                strSQL = strSQL + "  (ITI_INT_ID,  ITI_LI_ID,  ITI_Rel,  ITI_Indirizzo_Modbus,  ITI_DataOra,  ITI_U_ID) VALUES "
                                strSQL = strSQL + "  (@ITI_INT_ID, @ITI_LI_ID, @ITI_Rel, @ITI_Indirizzo_Modbus, @ITI_DataOra, @ITI_U_ID) "
                                strSQL = strSQL + " SELECT @ITI_ID = SCOPE_IDENTITY() "

                                CustomSQLConnectionOpen(cn, cmd)
                                'cmd.Connection = cn
                                cmd.CommandText = strSQL

                                dr.Item("ITI_INT_ID") = Me.ComboBox_INT_ID.SelectedValue()
                                dr.Item("ITI_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                                dr.Item("ITI_Rel") = Me.TextBox_ITIRel.Text
                                dr.Item("ITI_Indirizzo_Modbus") = Me.TextBox_ITI_Indirizzo_Modbus.Text
                                dr.Item("ITI_DataOra") = Date.Now
                                dr.Item("ITI_U_ID") = m_iUID
                                dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                                cmd.Parameters.Clear()
                                cmd.Parameters.Add("@ITI_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                                cmd.Parameters.AddWithValue("@ITI_INT_ID", dr.Item("ITI_INT_ID"))
                                cmd.Parameters.AddWithValue("@ITI_LI_ID", dr.Item("ITI_LI_ID"))
                                cmd.Parameters.AddWithValue("@ITI_Rel", dr.Item("ITI_Rel"))
                                cmd.Parameters.AddWithValue("@ITI_Indirizzo_Modbus", dr.Item("ITI_Indirizzo_Modbus"))
                                cmd.Parameters.AddWithValue("@ITI_DataOra", dr.Item("ITI_DataOra"))
                                cmd.Parameters.AddWithValue("@ITI_U_ID", dr.Item("ITI_U_ID"))

                                If cmd.ExecuteNonQuery() > 0 Then
                                    dr.Item("ITI_ID") = cmd.Parameters.Item("@ITI_ID").Value
                                    ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
                                Else
                                    ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                                End If

                                CaricaDati()
                            Else
                                CaricaDati()

                                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, L'indirizzo Modbus: " + Me.TextBox_ITI_Indirizzo_Modbus.Text + " e' gia' presente.", "", "", m_iUID, Me)
                            End If

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

        If (Me.TextBox_ITIRel.DataBindings.Count > 0) Then
            Me.TextBox_ITIRel.DataBindings.Clear()
        End If
        If (Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Count > 0) Then
            Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Clear()
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

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, InverterTesterInst.*, CONVERT(nvarchar(512), INT_Marca) + ' - ' + CONVERT(nvarchar(512), INT_Modello) + ' - ' + CONVERT(nvarchar(512), INT_Tipo) as TIPO, U_NomeCognome "
            strSQL = strSQL + " FROM InverterTesterInst INNER JOIN Utente ON ITI_U_ID = U_ID INNER JOIN InverterTester ON ITI_INT_ID = INT_ID INNER JOIN LoggerInst ON ITI_LI_ID = LI_ID INNER JOIN ImpiantoDiProduzione ON LI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
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

            If m_iLIID > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " LI_ID = " + m_iLIID.ToString
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

    Private Sub CaricaSTID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT INT_ID, INT_Marca, INT_Modello, CONVERT(nvarchar(512), INT_Marca) + ' - ' + CONVERT(nvarchar(512), INT_Modello) + ' - ' + CONVERT(nvarchar(512), INT_Tipo) as TIPO "
            strSQL = strSQL + " FROM InverterTester "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_INT_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "TIPO"
                        .ValueMember = "INT_ID"
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
            Me.Label_INT_ID.Text = m_bs.DataSource.Columns("TIPO").Caption
            Me.Label_ITIRel.Text = m_bs.DataSource.Columns("ITI_Rel").Caption
            Me.Label_ITI_Indirizzo_Modbus.Text = m_bs.DataSource.Columns("ITI_Indirizzo_Modbus").Caption

            If (Me.TextBox_ITIRel.DataBindings.Count = 0) Then
                Me.TextBox_ITIRel.DataBindings.Add(New Binding("Text", m_bs, "ITI_Rel", True))
            End If
            If (Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Count = 0) Then
                Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Add(New Binding("Text", m_bs, "ITI_Indirizzo_Modbus", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True

        Me.ComboBox_LI_ID.Enabled = False
        Me.ComboBox_INT_ID.Enabled = False
        Me.TextBox_ITIRel.ReadOnly = True
        Me.TextBox_ITI_Indirizzo_Modbus.ReadOnly = True

        If m_iCID > 0 Then
            Me.ComboBox_C_ID.SelectedValue = m_iCID
        End If
        If m_iIDPID > 0 Then
            Me.ComboBox_IDP_ID.SelectedValue = m_iIDPID
        End If
        If m_iLIID > 0 Then
            Me.ComboBox_LI_ID.SelectedValue = m_iLIID
        End If

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_ITIRel.DataBindings.Count > 0) Then
            Me.TextBox_ITIRel.DataBindings.Clear()
        End If
        If (Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Count > 0) Then
            Me.TextBox_ITI_Indirizzo_Modbus.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False

        Me.ComboBox_LI_ID.Enabled = True


        Me.ComboBox_INT_ID.Enabled = True
        Me.TextBox_ITIRel.ReadOnly = False
        Me.TextBox_ITI_Indirizzo_Modbus.ReadOnly = False
    End Sub
End Class
