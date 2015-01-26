Imports System.Data.SqlClient

Public Class ImpiantoDiProduzione

    Dim m_bs As New BindingSource

    Private m_iCID As Integer

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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Impianto Di Produzione"

        CaricaDati()

        CaricaCID()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

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
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value Then

                                Dim lg As New Login
                                Dim dr As Windows.Forms.DialogResult
                                lg.UID = m_iUID
                                lg.ULivello = GetMinLevelReq("ContatoreDiProduzioneInst")
                                dr = lg.ShowDialog(Me)
                                m_iUID = lg.UID
                                If dr = Windows.Forms.DialogResult.Yes Then
                                    ContatoreDiProduzioneInst.Close()
                                    ContatoreDiProduzioneInst.UID = m_iUID
                                    If m_iCID > 0 Then
                                        ContatoreDiProduzioneInst.CID = m_iCID
                                    Else
                                        ContatoreDiProduzioneInst.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
                                    End If
                                    ContatoreDiProduzioneInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
                                    ContatoreDiProduzioneInst.MdiParent = Me.MdiParent
                                    ContatoreDiProduzioneInst.Show()
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

            Me.TextBox_IDP_Nome.Text = ""
            Me.TextBox_IDP_Indirizzo.Text = ""
            Me.TextBox_IDP_CAP.Text = ""

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

                                strSQL = "DELETE FROM [ImpiantoDiProduzione] "
                                strSQL = strSQL + "WHERE IDP_ID = @IDP_ID AND IDP_CC = @IDP_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@IDP_ID", dgvr.Cells("IDP_ID").Value)
                                cmd.Parameters.AddWithValue("@IDP_CC", dgvr.Cells("IDP_CC").Value)

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

                            strSQL = " UPDATE [ImpiantoDiProduzione] "
                            strSQL = strSQL + " SET IDP_Nome = @IDP_Nome, IDP_Indirizzo = @IDP_Indirizzo, IDP_CAP = @IDP_CAP, IDP_DataOra = @IDP_DataOra, IDP_U_ID = @IDP_U_ID "
                            strSQL = strSQL + " WHERE IDP_ID = @IDP_ID AND IDP_CC = @IDP_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("IDP_Nome") = Me.TextBox_IDP_Nome.Text
                            dr.Item("IDP_Indirizzo") = Me.TextBox_IDP_Indirizzo.Text
                            dr.Item("IDP_CAP") = Me.TextBox_IDP_CAP.Text
                            dr.Item("IDP_DataOra") = Date.Now
                            dr.Item("IDP_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@IDP_ID", dr.Item("IDP_ID"))
                            cmd.Parameters.AddWithValue("@IDP_CC", dr.Item("IDP_CC"))
                            cmd.Parameters.AddWithValue("@IDP_Nome", dr.Item("IDP_Nome"))
                            cmd.Parameters.AddWithValue("@IDP_Indirizzo", dr.Item("IDP_Indirizzo"))
                            cmd.Parameters.AddWithValue("@IDP_CAP", dr.Item("IDP_CAP"))
                            cmd.Parameters.AddWithValue("@IDP_DataOra", dr.Item("IDP_DataOra"))
                            cmd.Parameters.AddWithValue("@IDP_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [ImpiantoDiProduzione] "
                            strSQL = strSQL + "  (IDP_C_ID,  IDP_Nome,  IDP_Indirizzo,  IDP_CAP,  IDP_DataOra,  IDP_U_ID) VALUES "
                            strSQL = strSQL + "  (@IDP_C_ID, @IDP_Nome, @IDP_Indirizzo, @IDP_CAP, @IDP_DataOra, @IDP_U_ID) "
                            strSQL = strSQL + " SELECT @IDP_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("IDP_C_ID") = Me.ComboBox_C_ID.SelectedValue()
                            dr.Item("IDP_Nome") = Me.TextBox_IDP_Nome.Text
                            dr.Item("IDP_Indirizzo") = Me.TextBox_IDP_Indirizzo.Text
                            dr.Item("IDP_CAP") = CInt(Me.TextBox_IDP_CAP.Text)
                            dr.Item("IDP_DataOra") = Date.Now
                            dr.Item("IDP_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@IDP_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@IDP_C_ID", dr.Item("IDP_C_ID"))
                            cmd.Parameters.AddWithValue("@IDP_Nome", dr.Item("IDP_Nome"))
                            cmd.Parameters.AddWithValue("@IDP_Indirizzo", dr.Item("IDP_Indirizzo"))
                            cmd.Parameters.AddWithValue("@IDP_CAP", dr.Item("IDP_CAP"))
                            cmd.Parameters.AddWithValue("@IDP_DataOra", dr.Item("IDP_DataOra"))
                            cmd.Parameters.AddWithValue("@IDP_U_ID", dr.Item("IDP_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("IDP_ID") = cmd.Parameters.Item("@IDP_ID").Value
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

        If (Me.TextBox_IDP_Nome.DataBindings.Count > 0) Then
            Me.TextBox_IDP_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_Indirizzo.DataBindings.Count > 0) Then
            Me.TextBox_IDP_Indirizzo.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_CAP.DataBindings.Count > 0) Then
            Me.TextBox_IDP_CAP.DataBindings.Clear()
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

            strSQL = " SELECT C_ID, C_Nome, ImpiantoDiProduzione.*, U_NomeCognome "
            strSQL = strSQL + " FROM ImpiantoDiProduzione INNER JOIN Utente ON IDP_U_ID = U_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "

            If m_iCID > 0 Then
                strSQL = strSQL + " WHERE IDP_C_ID = " + m_iCID.ToString
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

            Me.Label_IDP_Nome.Text = m_bs.DataSource.Columns("IDP_Nome").Caption
            Me.Label_IDP_Indirizzo.Text = m_bs.DataSource.Columns("IDP_Indirizzo").Caption
            Me.Label_IDP_CAP.Text = m_bs.DataSource.Columns("IDP_CAP").Caption

            If (Me.TextBox_IDP_Nome.DataBindings.Count = 0) Then
                Me.TextBox_IDP_Nome.DataBindings.Add(New Binding("Text", m_bs, "IDP_Nome", True))
            End If
            If (Me.TextBox_IDP_Indirizzo.DataBindings.Count = 0) Then
                Me.TextBox_IDP_Indirizzo.DataBindings.Add(New Binding("Text", m_bs, "IDP_Indirizzo", True))
            End If
            If (Me.TextBox_IDP_CAP.DataBindings.Count = 0) Then
                Me.TextBox_IDP_CAP.DataBindings.Add(New Binding("Text", m_bs, "IDP_CAP", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.TextBox_IDP_Nome.ReadOnly = True
        Me.TextBox_IDP_Indirizzo.ReadOnly = True
        Me.TextBox_IDP_CAP.ReadOnly = True

        If m_iCID > 0 Then
            Me.ComboBox_C_ID.SelectedValue = m_iCID
        End If

    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_IDP_Nome.DataBindings.Count > 0) Then
            Me.TextBox_IDP_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_Indirizzo.DataBindings.Count > 0) Then
            Me.TextBox_IDP_Indirizzo.DataBindings.Clear()
        End If
        If (Me.TextBox_IDP_CAP.DataBindings.Count > 0) Then
            Me.TextBox_IDP_CAP.DataBindings.Clear()
        End If

        If m_iCID = 0 Then
            Me.ComboBox_C_ID.Enabled = True
        End If

        Me.TextBox_IDP_Nome.ReadOnly = False
        Me.TextBox_IDP_Indirizzo.ReadOnly = False
        Me.TextBox_IDP_CAP.ReadOnly = False
    End Sub

End Class
