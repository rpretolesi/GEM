Imports System.Data.SqlClient

Public Class PannelloFotovString
    Dim m_bs As New BindingSource

    Private m_iCID As Integer
    Private m_iIDPID As Integer
    Private m_iCDPIID As Integer
    Private m_iIFIID As Integer

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

    Property IFIID() As Integer
        Get
            Return m_iIFIID
        End Get

        Set(ByVal IFIID As Integer)
            m_iIFIID = IFIID
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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Definizione Stringhe"

        Me.TextBox_PFS_Nr.Text = 0
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Text = 0
        Me.TextBox_PFI_Nr.Text = 0

        CaricaCID()

        CaricaIDPID()

        CaricaCDPIID()

        CaricaIFIID()

        CaricaPFID()

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
            CaricaCDPIID()
            CaricaIFIID()
            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIDPID = Me.ComboBox_IDP_ID.SelectedValue()
            CaricaCDPIID()
            CaricaIFIID()
            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_CDPI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_CDPI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iCDPIID = Me.ComboBox_CDPI_ID.SelectedValue()
            CaricaIFIID()
            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_IFI_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IFI_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIFIID = Me.ComboBox_IFI_ID.SelectedValue()
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
                            If Not DataGridView.SelectedRows(0).Cells("CDPI_Nr").Value Is DBNull.Value Then
                                Me.ComboBox_CDPI_ID.Text = DataGridView.SelectedRows(0).Cells("CDPI_Nr").Value
                            End If
                            If Not DataGridView.SelectedRows(0).Cells("IFI_Nr").Value Is DBNull.Value Then
                                Me.ComboBox_IFI_ID.Text = DataGridView.SelectedRows(0).Cells("IFI_Nr").Value
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
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("CDPI_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IFI_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("PFS_ID").Value Is DBNull.Value Then

                                Dim lg As New Login
                                Dim dr As Windows.Forms.DialogResult
                                lg.UID = m_iUID
                                lg.ULivello = GetMinLevelReq("PannelloFotovInst")
                                dr = lg.ShowDialog()
                                m_iUID = lg.UID
                                If dr = Windows.Forms.DialogResult.Yes Then

                                    PannelloFotovInst.Close()
                                    PannelloFotovInst.UID = m_iUID
                                    If m_iCID > 0 Then
                                        PannelloFotovInst.CID = m_iCID
                                    Else
                                        PannelloFotovInst.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
                                    End If
                                    If m_iIDPID > 0 Then
                                        PannelloFotovInst.IDPID = m_iIDPID
                                    Else
                                        PannelloFotovInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
                                    End If
                                    If m_iCDPIID > 0 Then
                                        PannelloFotovInst.CDPIID = m_iCDPIID
                                    Else
                                        PannelloFotovInst.CDPIID = DataGridView.SelectedRows(0).Cells("CDPI_ID").Value
                                    End If
                                    If m_iIFIID > 0 Then
                                        PannelloFotovInst.IFIID = m_iIFIID
                                    Else
                                        PannelloFotovInst.IFIID = DataGridView.SelectedRows(0).Cells("IFI_ID").Value
                                    End If
                                    PannelloFotovInst.PFSID = DataGridView.SelectedRows(0).Cells("PFS_ID").Value

                                    PannelloFotovInst.MdiParent = Me.MdiParent
                                    PannelloFotovInst.Show()

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

            Me.TextBox_PFS_Nr.Text = ""

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

                                strSQL = "DELETE FROM [PannelloFotovString] "
                                strSQL = strSQL + "WHERE PFS_ID = @PFS_ID AND PFS_CC = @PFS_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@PFS_ID", dgvr.Cells("PFS_ID").Value)
                                cmd.Parameters.AddWithValue("@PFS_CC", dgvr.Cells("PFS_CC").Value)

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

                            strSQL = " UPDATE [PannelloFotovString] "
                            strSQL = strSQL + " SET PFS_IFI_ID = @PFS_IFI_ID, PFS_Nr = @PFS_Nr, PFS_DataOra = @PFS_DataOra, PFS_U_ID = @PFS_U_ID "
                            strSQL = strSQL + " WHERE PFS_ID = @PFS_ID AND PFS_CC = @PFS_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("PFS_IFI_ID") = Me.ComboBox_IFI_ID.SelectedValue()
                            dr.Item("PFS_Nr") = Me.TextBox_PFS_Nr.Text
                            dr.Item("PFS_DataOra") = Date.Now
                            dr.Item("PFS_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@PFS_ID", dr.Item("PFS_ID"))
                            cmd.Parameters.AddWithValue("@PFS_CC", dr.Item("PFS_CC"))
                            cmd.Parameters.AddWithValue("@PFS_IFI_ID", dr.Item("PFS_IFI_ID"))
                            cmd.Parameters.AddWithValue("@PFS_Nr", dr.Item("PFS_Nr"))
                            cmd.Parameters.AddWithValue("@PFS_DataOra", dr.Item("PFS_DataOra"))
                            cmd.Parameters.AddWithValue("@PFS_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [PannelloFotovString] "
                            strSQL = strSQL + "  (PFS_IFI_ID,  PFS_Nr,  PFS_DataOra,  PFS_U_ID) VALUES "
                            strSQL = strSQL + "  (@PFS_IFI_ID, @PFS_Nr, @PFS_DataOra, @PFS_U_ID) "
                            strSQL = strSQL + " SELECT @PFS_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("PFS_IFI_ID") = Me.ComboBox_IFI_ID.SelectedValue()
                            dr.Item("PFS_Nr") = Me.TextBox_PFS_Nr.Text
                            dr.Item("PFS_DataOra") = Date.Now
                            dr.Item("PFS_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@PFS_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@PFS_IFI_ID", dr.Item("PFS_IFI_ID"))
                            cmd.Parameters.AddWithValue("@PFS_Nr", dr.Item("PFS_Nr"))
                            cmd.Parameters.AddWithValue("@PFS_DataOra", dr.Item("PFS_DataOra"))
                            cmd.Parameters.AddWithValue("@PFS_U_ID", dr.Item("PFS_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("PFS_ID") = cmd.Parameters.Item("@PFS_ID").Value
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

        If (Me.TextBox_PFS_Nr.DataBindings.Count > 0) Then
            Me.TextBox_PFS_Nr.DataBindings.Clear()
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

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, CDPI_ID, CDPI_Nr, IFI_ID, IFI_Nr, PannelloFotovString.*, U_NomeCognome "
            strSQL = strSQL + " FROM PannelloFotovString INNER JOIN Utente ON PFS_U_ID = U_ID INNER JOIN InverterFotovInst ON PFS_IFI_ID = IFI_ID INNER JOIN ContatoreDiProduzioneInst ON IFI_CDPI_ID = CDPI_ID INNER JOIN ImpiantoDiProduzione ON CDPI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
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

            If m_iIFIID > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " IFI_ID = " + m_iIFIID.ToString
                iIndice_1 = iIndice_1 + 1
            End If

            If m_iCDPIID > 0 Then
                If iIndice_1 > 0 Then
                    strSQL = strSQL + " AND "
                End If
                strSQL = strSQL + " CDPI_ID = " + m_iCDPIID.ToString
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

    Private Sub CaricaCDPIID()
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
    End Sub

    Private Sub CaricaIFIID()
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
    End Sub

    Private Sub CaricaPFID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT PF_ID, PF_Marca, PF_Modello, PF_Marca + ' - ' + PF_Modello as PF_TIPO "
            strSQL = strSQL + " FROM PannelloFotov "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_PF_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "PF_TIPO"
                        .ValueMember = "PF_ID"
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
            Me.Label_CDPI_ID.Text = m_bs.DataSource.Columns("CDPI_Nr").Caption
            Me.Label_IFI_ID.Text = m_bs.DataSource.Columns("IFI_Nr").Caption

            Me.Label_PFS_Nr.Text = m_bs.DataSource.Columns("PFS_Nr").Caption

            If (Me.TextBox_PFS_Nr.DataBindings.Count = 0) Then
                Me.TextBox_PFS_Nr.DataBindings.Add(New Binding("Text", m_bs, "PFS_Nr", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True
        Me.ComboBox_CDPI_ID.Enabled = True
        Me.ComboBox_IFI_ID.Enabled = True

        Me.TextBox_PFS_Nr.ReadOnly = True

        If m_iCID > 0 Then
            Me.ComboBox_C_ID.SelectedValue = m_iCID
        End If
        If m_iIDPID > 0 Then
            Me.ComboBox_IDP_ID.SelectedValue = m_iIDPID
        End If
        If m_iCDPIID > 0 Then
            Me.ComboBox_CDPI_ID.SelectedValue = m_iCDPIID
        End If
        If m_iIFIID > 0 Then
            Me.ComboBox_IFI_ID.SelectedValue = m_iIFIID
        End If

        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Enabled = False
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Enabled = False

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_PFS_Nr.DataBindings.Count > 0) Then
            Me.TextBox_PFS_Nr.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False
        Me.ComboBox_CDPI_ID.Enabled = False
        Me.ComboBox_IFI_ID.Enabled = False

        Me.TextBox_PFS_Nr.ReadOnly = False

        Me.Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Enabled = True
        Me.TextBox_Nr_Stringhe_Da_Aggiungere.Enabled = True

    End Sub

    Private Sub Button_Aggiungi_Tutti_Pannelli_Fotovoltaici_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Aggiungi_Tutti_Pannelli_Fotovoltaici.Click
        Dim iPFInr As Integer
        Try
            iPFInr = CInt(TextBox_PFI_Nr.Text)

            If AddAllPannelliFotovoltaici(ComboBox_C_ID.SelectedValue(), ComboBox_IDP_ID.SelectedValue(), ComboBox_CDPI_ID.SelectedValue(), ComboBox_IFI_ID.SelectedValue(), iPFInr, ComboBox_PF_ID.SelectedValue(), m_iUID) = True Then
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
            Else
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "", "", "", m_iUID, Me)
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Nothing)
        End Try

    End Sub

    Private Sub Button_Elimina_Tutti_Pannelli_Fotovoltaici_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Elimina_Tutti_Pannelli_Fotovoltaici.Click
        If DelAllPannelliFotovoltaici(ComboBox_C_ID.SelectedValue(), ComboBox_IDP_ID.SelectedValue(), ComboBox_CDPI_ID.SelectedValue(), ComboBox_IFI_ID.SelectedValue(), m_iUID) = True Then
            ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", m_iUID, Me)
        Else
            ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR, "", "", "", m_iUID, Me)
        End If
    End Sub

    Private Sub Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Aggiungi_Automaticamente_Questo_Nr_Di_Stringhe.Click
        Dim iPFSNrTotDaAgg As Integer
        Try
            iPFSNrTotDaAgg = CInt(TextBox_Nr_Stringhe_Da_Aggiungere.Text)

            If AddAllStringhePannelliFotovoltaici(ComboBox_C_ID.SelectedValue(), ComboBox_IDP_ID.SelectedValue(), ComboBox_CDPI_ID.SelectedValue(), ComboBox_IFI_ID.SelectedValue(), iPFSNrTotDaAgg, m_iUID) = True Then
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
            Else
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "", "", "", m_iUID, Me)
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Nothing)
        End Try

        CaricaDati()

        EseguiBinding()

        MyBase.ToolStripButton_Salva_Click(sender, e)

    End Sub
End Class
