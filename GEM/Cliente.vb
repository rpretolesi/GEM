Imports System.Data.SqlClient

Public Class Cliente

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
        Me.Text = "Cliente"

        CaricaDati()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Protected Overrides Sub DataGridView_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Try
            If DataGridView.SelectedRows.Count > 0 Then
                If DataGridView.SelectedRows(0).Index >= 0 Then
                    If Not m_bs.DataSource Is Nothing Then
                        If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value Then

                                Dim lg As New Login
                                Dim dr As Windows.Forms.DialogResult
                                lg.UID = m_iUID
                                lg.ULivello = GetMinLevelReq("ImpiantoDiProduzione")
                                dr = lg.ShowDialog(Me)
                                m_iUID = lg.UID
                                If dr = Windows.Forms.DialogResult.Yes Then
                                    ImpiantoDiProduzione.Close()
                                    ImpiantoDiProduzione.UID = m_iUID
                                    ImpiantoDiProduzione.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
                                    ImpiantoDiProduzione.MdiParent = Me.MdiParent
                                    ImpiantoDiProduzione.Show()
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

            Me.TextBox_C_Nome.Text = ""
            Me.TextBox_C_Cognome.Text = ""
            Me.TextBox_C_Societa.Text = ""
            Me.CheckBox_Opzioni_GEMClient_Vis_All.Checked = False

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

                                strSQL = "DELETE FROM [Cliente] "
                                strSQL = strSQL + "WHERE C_ID = @C_ID AND C_CC = @C_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@C_ID", dgvr.Cells("C_ID").Value)
                                cmd.Parameters.AddWithValue("@C_CC", dgvr.Cells("C_CC").Value)

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

                            strSQL = " UPDATE [Cliente] "
                            strSQL = strSQL + " SET C_Nome = @C_Nome, C_Cognome = @C_Cognome, C_Societa = @C_Societa, C_GEMClient_Vis_All = @C_GEMClient_Vis_All, C_GEMClient_Vis_DatiStat = @C_GEMClient_Vis_DatiStat, C_DataOra = @C_DataOra, C_U_ID = @C_U_ID "
                            strSQL = strSQL + " WHERE C_ID = @C_ID AND C_CC = @C_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("C_Nome") = Me.TextBox_C_Nome.Text
                            dr.Item("C_Cognome") = Me.TextBox_C_Cognome.Text
                            dr.Item("C_Societa") = Me.TextBox_C_Societa.Text
                            dr.Item("C_GEMClient_Vis_All") = Me.CheckBox_Opzioni_GEMClient_Vis_All.Checked
                            dr.Item("C_GEMClient_Vis_DatiStat") = Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Checked
                            dr.Item("C_DataOra") = Date.Now
                            dr.Item("C_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@C_ID", dr.Item("C_ID"))
                            cmd.Parameters.AddWithValue("@C_CC", dr.Item("C_CC"))
                            cmd.Parameters.AddWithValue("@C_Nome", dr.Item("C_Nome"))
                            cmd.Parameters.AddWithValue("@C_Cognome", dr.Item("C_Cognome"))
                            cmd.Parameters.AddWithValue("@C_Societa", dr.Item("C_Societa"))
                            cmd.Parameters.AddWithValue("@C_GEMClient_Vis_All", dr.Item("C_GEMClient_Vis_All"))
                            cmd.Parameters.AddWithValue("@C_GEMClient_Vis_DatiStat", dr.Item("C_GEMClient_Vis_DatiStat"))
                            cmd.Parameters.AddWithValue("@C_DataOra", dr.Item("C_DataOra"))
                            cmd.Parameters.AddWithValue("@C_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [Cliente] "
                            strSQL = strSQL + "  (C_Codice, C_Nome,  C_Cognome,  C_Societa,  C_GEMClient_Vis_All,   C_GEMClient_Vis_DatiStat, C_DataOra,  C_U_ID) VALUES "
                            strSQL = strSQL + "  (@C_Codice, @C_Nome, @C_Cognome, @C_Societa, @C_GEMClient_Vis_All, @C_GEMClient_Vis_DatiStat, @C_DataOra, @C_U_ID) "
                            strSQL = strSQL + " SELECT @C_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("C_Codice") = System.Guid.NewGuid().ToString()
                            dr.Item("C_Nome") = Me.TextBox_C_Nome.Text
                            dr.Item("C_Cognome") = Me.TextBox_C_Cognome.Text
                            dr.Item("C_Societa") = Me.TextBox_C_Societa.Text
                            dr.Item("C_GEMClient_Vis_All") = Me.CheckBox_Opzioni_GEMClient_Vis_All.Checked
                            dr.Item("C_GEMClient_Vis_DatiStat") = Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Checked
                            dr.Item("C_DataOra") = Date.Now
                            dr.Item("C_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@C_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@C_Codice", dr.Item("C_Codice"))
                            cmd.Parameters.AddWithValue("@C_Nome", dr.Item("C_Nome"))
                            cmd.Parameters.AddWithValue("@C_Cognome", dr.Item("C_Cognome"))
                            cmd.Parameters.AddWithValue("@C_Societa", dr.Item("C_Societa"))
                            cmd.Parameters.AddWithValue("@C_GEMClient_Vis_All", dr.Item("C_GEMClient_Vis_All"))
                            cmd.Parameters.AddWithValue("@C_GEMClient_Vis_DatiStat", dr.Item("C_GEMClient_Vis_DatiStat"))
                            cmd.Parameters.AddWithValue("@C_DataOra", dr.Item("C_DataOra"))
                            cmd.Parameters.AddWithValue("@C_U_ID", dr.Item("C_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("C_ID") = cmd.Parameters.Item("@C_ID").Value
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

        If (Me.TextBox_C_Nome.DataBindings.Count > 0) Then
            Me.TextBox_C_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Cognome.DataBindings.Count > 0) Then
            Me.TextBox_C_Cognome.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Societa.DataBindings.Count > 0) Then
            Me.TextBox_C_Societa.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Codice.DataBindings.Count > 0) Then
            Me.TextBox_C_Codice.DataBindings.Clear()
        End If
        If (Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Count > 0) Then
            Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Clear()
        End If
        If (Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Count > 0) Then
            Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Clear()
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

            strSQL = " SELECT Cliente.C_ID, Cliente.C_Nome, Cliente.C_Cognome, Cliente.C_Societa, Cliente.C_Codice, C_GEMClient_Vis_All, C_GEMClient_Vis_DatiStat, Cliente.C_DataOra, Cliente.C_U_ID, Utente.U_NomeCognome, Cliente.C_CC "
            strSQL = strSQL + " FROM Cliente INNER JOIN Utente ON Cliente.C_U_ID = Utente.U_ID "

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

    Private Sub EseguiBinding()
        If Not m_bs.DataSource Is Nothing Then

            Me.Label_C_Nome.Text = m_bs.DataSource.Columns("C_Nome").Caption
            Me.Label_C_Cognome.Text = m_bs.DataSource.Columns("C_Cognome").Caption
            Me.Label_C_Societa.Text = m_bs.DataSource.Columns("C_Societa").Caption
            Me.Label_C_Codice.Text = m_bs.DataSource.Columns("C_Codice").Caption

            If (Me.TextBox_C_Nome.DataBindings.Count = 0) Then
                Me.TextBox_C_Nome.DataBindings.Add(New Binding("Text", m_bs, "C_Nome", True))
            End If
            If (Me.TextBox_C_Cognome.DataBindings.Count = 0) Then
                Me.TextBox_C_Cognome.DataBindings.Add(New Binding("Text", m_bs, "C_Cognome", True))
            End If
            If (Me.TextBox_C_Societa.DataBindings.Count = 0) Then
                Me.TextBox_C_Societa.DataBindings.Add(New Binding("Text", m_bs, "C_Societa", True))
            End If
            If (Me.TextBox_C_Codice.DataBindings.Count = 0) Then
                Me.TextBox_C_Codice.DataBindings.Add(New Binding("Text", m_bs, "C_Codice", True))
            End If
            If (Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Count = 0) Then
                Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Add(New Binding("Checked", m_bs, "C_GEMClient_Vis_All", True))
            End If
            If (Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Count = 0) Then
                Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Add(New Binding("Checked", m_bs, "C_GEMClient_Vis_DatiStat", True))
            End If

        End If

        Me.TextBox_C_Nome.ReadOnly = True
        Me.TextBox_C_Cognome.ReadOnly = True
        Me.TextBox_C_Societa.ReadOnly = True
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Enabled = False
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Enabled = False
    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_C_Nome.DataBindings.Count > 0) Then
            Me.TextBox_C_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Cognome.DataBindings.Count > 0) Then
            Me.TextBox_C_Cognome.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Societa.DataBindings.Count > 0) Then
            Me.TextBox_C_Societa.DataBindings.Clear()
        End If
        If (Me.TextBox_C_Codice.DataBindings.Count > 0) Then
            Me.TextBox_C_Codice.DataBindings.Clear()
        End If
        If (Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Count > 0) Then
            Me.CheckBox_Opzioni_GEMClient_Vis_All.DataBindings.Clear()
        End If
        If (Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Count > 0) Then
            Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.DataBindings.Clear()
        End If

        Me.TextBox_C_Nome.ReadOnly = False
        Me.TextBox_C_Cognome.ReadOnly = False
        Me.TextBox_C_Societa.ReadOnly = False
        Me.CheckBox_Opzioni_GEMClient_Vis_All.Enabled = True
        Me.CheckBox_Opzioni_GEMClient_Vis_DatiStat.Enabled = True
    End Sub

End Class
