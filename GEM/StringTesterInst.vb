Imports System.Data.SqlClient

Public Class StringTesterInst
    Dim m_bs As New BindingSource

    Private m_iCID As Integer
    Private m_iIDPID As Integer
    Private m_iCDPIID As Integer
    Private m_iIFIID As Integer
    Private m_iPFSID As Integer
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

    Property CDPIID() As Integer
        Get
            Return m_iCDPIID
        End Get

        Set(ByVal CDPIID As Integer)
            m_iCDPIID = CDPIID
        End Set
    End Property

    Property IFIID() As Integer
        Get
            Return m_iIFIID
        End Get

        Set(ByVal IFIID As Integer)
            m_iIFIID = IFIID
        End Set
    End Property

    Property PFSID() As Integer
        Get
            Return m_iPFSID
        End Get

        Set(ByVal PFSID As Integer)
            m_iPFSID = PFSID
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

        Me.Text = "String Tester Installati"

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
                                Me.ComboBox_LI_ID.Text = DataGridView.SelectedRows(0).Cells("LI_Nr").Value
                            End If
                            If Not DataGridView.SelectedRows(0).Cells("ST_TIPO").Value Is DBNull.Value Then
                                Me.ComboBox_ST_ID.Text = DataGridView.SelectedRows(0).Cells("ST_TIPO").Value
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

            RimuoviBinding()

            Me.TextBox_STIRel.Text = ""
            Me.TextBox_STI_Indirizzo_Modbus.Text = ""

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

                                strSQL = "DELETE FROM [StringTesterInst] "
                                strSQL = strSQL + "WHERE STI_ID = @STI_ID AND STI_CC = @STI_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@STI_ID", dgvr.Cells("STI_ID").Value)
                                cmd.Parameters.AddWithValue("@STI_CC", dgvr.Cells("STI_CC").Value)

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

                            strSQL = " UPDATE [StringTesterInst] "
                            strSQL = strSQL + " SET STI_ST_ID = @STI_ST_ID, STI_LI_ID = @STI_LI_ID, STI_Rel = @STI_Rel, STI_Indirizzo_Modbus = @STI_Indirizzo_Modbus, STI_DataOra = @STI_DataOra, STI_U_ID = @STI_U_ID "
                            strSQL = strSQL + " WHERE STI_ID = @STI_ID AND STI_CC = @STI_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("STI_ST_ID") = Me.ComboBox_ST_ID.SelectedValue()
                            dr.Item("STI_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                            dr.Item("STI_Rel") = Me.TextBox_STIRel.Text
                            dr.Item("STI_Indirizzo_Modbus") = Me.TextBox_STI_Indirizzo_Modbus.Text
                            dr.Item("STI_DataOra") = Date.Now
                            dr.Item("STI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@STI_ID", dr.Item("STI_ID"))
                            cmd.Parameters.AddWithValue("@STI_CC", dr.Item("STI_CC"))
                            cmd.Parameters.AddWithValue("@STI_ST_ID", dr.Item("STI_ST_ID"))
                            cmd.Parameters.AddWithValue("@STI_LI_ID", dr.Item("STI_LI_ID"))
                            cmd.Parameters.AddWithValue("@STI_Rel", dr.Item("STI_Rel"))
                            cmd.Parameters.AddWithValue("@STI_Indirizzo_Modbus", dr.Item("STI_Indirizzo_Modbus"))
                            cmd.Parameters.AddWithValue("@STI_DataOra", dr.Item("STI_DataOra"))
                            cmd.Parameters.AddWithValue("@STI_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then
                            If IsModbusAddressAlreadyPresent(Me.ComboBox_LI_ID.SelectedValue(), Me.TextBox_STI_Indirizzo_Modbus.Text) = False Then

                                strSQL = " INSERT INTO [StringTesterInst] "
                                strSQL = strSQL + "  (STI_ST_ID,  STI_LI_ID,  STI_Rel,  STI_Indirizzo_Modbus,  STI_DataOra,  STI_U_ID) VALUES "
                                strSQL = strSQL + "  (@STI_ST_ID, @STI_LI_ID, @STI_Rel, @STI_Indirizzo_Modbus, @STI_DataOra, @STI_U_ID) "
                                strSQL = strSQL + " SELECT @STI_ID = SCOPE_IDENTITY() "

                                CustomSQLConnectionOpen(cn, cmd)
                                'cmd.Connection = cn
                                cmd.CommandText = strSQL

                                dr.Item("STI_ST_ID") = Me.ComboBox_ST_ID.SelectedValue()
                                dr.Item("STI_LI_ID") = Me.ComboBox_LI_ID.SelectedValue()
                                dr.Item("STI_Rel") = Me.TextBox_STIRel.Text
                                dr.Item("STI_Indirizzo_Modbus") = Me.TextBox_STI_Indirizzo_Modbus.Text
                                dr.Item("STI_DataOra") = Date.Now
                                dr.Item("STI_U_ID") = m_iUID
                                dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                                cmd.Parameters.Clear()
                                cmd.Parameters.Add("@STI_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                                cmd.Parameters.AddWithValue("@STI_ST_ID", dr.Item("STI_ST_ID"))
                                cmd.Parameters.AddWithValue("@STI_LI_ID", dr.Item("STI_LI_ID"))
                                cmd.Parameters.AddWithValue("@STI_Rel", dr.Item("STI_Rel"))
                                cmd.Parameters.AddWithValue("@STI_Indirizzo_Modbus", dr.Item("STI_Indirizzo_Modbus"))
                                cmd.Parameters.AddWithValue("@STI_DataOra", dr.Item("STI_DataOra"))
                                cmd.Parameters.AddWithValue("@STI_U_ID", dr.Item("STI_U_ID"))

                                If cmd.ExecuteNonQuery() > 0 Then
                                    dr.Item("STI_ID") = cmd.Parameters.Item("@STI_ID").Value
                                    ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
                                Else
                                    ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                                End If

                                CaricaDati()

                            Else
                                CaricaDati()

                                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, L'indirizzo Modbus: " + Me.TextBox_STI_Indirizzo_Modbus.Text + " e' gia' presente.", "", "", m_iUID, Me)
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

        If (Me.TextBox_STIRel.DataBindings.Count > 0) Then
            Me.TextBox_STIRel.DataBindings.Clear()
        End If
        If (Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Count > 0) Then
            Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Clear()
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

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, StringTesterInst.*, ST_Marca + ' - ' + ST_Modello as ST_TIPO, U_NomeCognome "
            strSQL = strSQL + " FROM StringTesterInst INNER JOIN Utente ON STI_U_ID = U_ID INNER JOIN StringTester ON STI_ST_ID = ST_ID INNER JOIN LoggerInst ON STI_LI_ID = LI_ID INNER JOIN ImpiantoDiProduzione ON LI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
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

            strSQL = " SELECT ST_ID, ST_Marca, ST_Modello, ST_Marca + ' - ' + ST_Modello as ST_TIPO "
            strSQL = strSQL + " FROM StringTester "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_ST_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "ST_TIPO"
                        .ValueMember = "ST_ID"
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
            Me.Label_ST_ID.Text = m_bs.DataSource.Columns("ST_TIPO").Caption
            Me.Label_STIRel.Text = m_bs.DataSource.Columns("STI_Rel").Caption
            Me.Label_STI_Indirizzo_Modbus.Text = m_bs.DataSource.Columns("STI_Indirizzo_Modbus").Caption

            If (Me.TextBox_STIRel.DataBindings.Count = 0) Then
                Me.TextBox_STIRel.DataBindings.Add(New Binding("Text", m_bs, "STI_Rel", True))
            End If
            If (Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Count = 0) Then
                Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Add(New Binding("Text", m_bs, "STI_Indirizzo_Modbus", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True

        Me.ComboBox_LI_ID.Enabled = False
        Me.ComboBox_ST_ID.Enabled = False
        Me.TextBox_STIRel.ReadOnly = True
        Me.TextBox_STI_Indirizzo_Modbus.ReadOnly = True

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

        If (Me.TextBox_STIRel.DataBindings.Count > 0) Then
            Me.TextBox_STIRel.DataBindings.Clear()
        End If
        If (Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Count > 0) Then
            Me.TextBox_STI_Indirizzo_Modbus.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False

        Me.ComboBox_LI_ID.Enabled = True

        Me.ComboBox_ST_ID.Enabled = True
        Me.TextBox_STIRel.ReadOnly = False
        Me.TextBox_STI_Indirizzo_Modbus.ReadOnly = False
    End Sub

End Class
