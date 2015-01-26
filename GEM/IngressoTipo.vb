Imports System.Data.SqlClient

Public Class IngressoTipo
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

        Me.Text = "Tipo Ingresso"

        CaricaDati()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False

    End Sub

    'Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not m_bs.DataSource Is Nothing Then

    '        RimuoviBinding()

    '        Me.TextBox_IT_ID.Text = ""
    '        Me.TextBox_IT_Nome.Text = ""
    '        Me.TextBox_IT_UM.Text = ""
    '        Me.TextBox_IT_K.Text = "1"

    '        Dim dr As DataRow

    '        dr = m_bs.DataSource.Rows.Add()

    '        MyBase.ToolStripButton_Nuovo_Click(sender, e)

    '    End If
    'End Sub

    'Protected Overrides Sub ToolStripButton_Elimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim dr As DataRow

    '    If Not m_bs.DataSource Is Nothing Then

    '        If Not m_bs.Current() Is Nothing Then

    '            dr = m_bs.Current().Row()

    '            Try
    '                strSQL = "DELETE FROM [IngressoTipo] "
    '                strSQL = strSQL + "WHERE IT_ID = @IT_ID AND IT_CC = @IT_CC "

    '                CustomSQLConnectionOpen(cn, cmd)
    '                'cmd.Connection = cn
    '                cmd.CommandText = strSQL

    '                cmd.Parameters.Clear()
    '                cmd.Parameters.AddWithValue("@IT_ID", dr.Item("IT_ID"))
    '                cmd.Parameters.AddWithValue("@IT_CC", dr.Item("IT_CC"))

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

    '        MyBase.ToolStripButton_Elimina_Click(sender, e)

    '    End If

    'End Sub

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

                            strSQL = " UPDATE [IngressoTipo] "
                            strSQL = strSQL + " SET IT_Nome = @IT_Nome, IT_UM = @IT_UM, IT_K = @IT_K, IT_DataOra = @IT_DataOra, IT_U_ID = @IT_U_ID "
                            strSQL = strSQL + " WHERE IT_ID = @IT_ID AND IT_CC = @IT_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("IT_ID") = Me.TextBox_IT_ID.Text
                            dr.Item("IT_Nome") = Me.TextBox_IT_Nome.Text
                            dr.Item("IT_UM") = Me.TextBox_IT_UM.Text
                            dr.Item("IT_K") = Me.TextBox_IT_K.Text
                            dr.Item("IT_DataOra") = Date.Now
                            dr.Item("IT_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@IT_ID", dr.Item("IT_ID"))
                            cmd.Parameters.AddWithValue("@IT_CC", dr.Item("IT_CC"))
                            cmd.Parameters.AddWithValue("@IT_Nome", dr.Item("IT_Nome"))
                            cmd.Parameters.AddWithValue("@IT_UM", dr.Item("IT_UM"))
                            cmd.Parameters.AddWithValue("@IT_K", dr.Item("IT_K"))
                            cmd.Parameters.AddWithValue("@IT_DataOra", dr.Item("IT_DataOra"))
                            cmd.Parameters.AddWithValue("@IT_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                            'ElseIf dr.RowState = DataRowState.Added Then

                            '    strSQL = " INSERT INTO [IngressoTipo] "
                            '    strSQL = strSQL + "  (IT_ID,  IT_Nome,  IT_UM,  IT_K,  IT_DataOra,  IT_U_ID) VALUES "
                            '    strSQL = strSQL + "  (@IT_ID, @IT_Nome, @IT_UM, @IT_K, @IT_DataOra, @IT_U_ID) "

                            '    CustomSQLConnectionOpen(cn, cmd)
                            '    'cmd.Connection = cn
                            '    cmd.CommandText = strSQL

                            '    dr.Item("IT_ID") = Me.TextBox_IT_ID.Text
                            '    dr.Item("IT_Nome") = Me.TextBox_IT_Nome.Text
                            '    dr.Item("IT_UM") = Me.TextBox_IT_UM.Text
                            '    dr.Item("IT_K") = Me.TextBox_IT_K.Text
                            '    dr.Item("IT_DataOra") = Date.Now
                            '    dr.Item("IT_U_ID") = m_iUID
                            '    dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            '    cmd.Parameters.Clear()
                            '    cmd.Parameters.AddWithValue("@IT_ID", dr.Item("IT_ID"))
                            '    cmd.Parameters.AddWithValue("@IT_Nome", dr.Item("IT_Nome"))
                            '    cmd.Parameters.AddWithValue("@IT_UM", dr.Item("IT_UM"))
                            '    cmd.Parameters.AddWithValue("@IT_K", dr.Item("IT_K"))
                            '    cmd.Parameters.AddWithValue("@IT_DataOra", dr.Item("IT_DataOra"))
                            '    cmd.Parameters.AddWithValue("@IT_U_ID", dr.Item("IT_U_ID"))

                            '    If cmd.ExecuteNonQuery() > 0 Then
                            '        ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            '    Else
                            '        ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            '    End If

                            '    CaricaDati()

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

            Me.ToolStripButton_Nuovo.Enabled = False
            Me.ToolStripButton_Elimina.Enabled = False

        End If

    End Sub

    Protected Overrides Sub ToolStripButton_Annulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

            EseguiBinding()

            MyBase.ToolStripButton_Annulla_Click(sender, e)

            Me.ToolStripButton_Nuovo.Enabled = False
            Me.ToolStripButton_Elimina.Enabled = False

        End If

    End Sub

    Protected Overrides Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        If (Me.TextBox_IT_ID.DataBindings.Count > 0) Then
            Me.TextBox_IT_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_Nome.DataBindings.Count > 0) Then
            Me.TextBox_IT_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_UM.DataBindings.Count > 0) Then
            Me.TextBox_IT_UM.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_K.DataBindings.Count > 0) Then
            Me.TextBox_IT_K.DataBindings.Clear()
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

            strSQL = " SELECT IT_ID, IT_Nome, IT_UM, IT_K, IT_DataOra, IT_U_ID, U_NomeCognome, IT_CC "
            strSQL = strSQL + " FROM IngressoTipo INNER JOIN Utente ON IT_U_ID = U_ID "

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

            Me.Label_IT_ID.Text = m_bs.DataSource.Columns("IT_ID").Caption
            Me.Label_IT_Nome.Text = m_bs.DataSource.Columns("IT_Nome").Caption
            Me.Label_IT_UM.Text = m_bs.DataSource.Columns("IT_UM").Caption
            Me.Label_IT_K.Text = m_bs.DataSource.Columns("IT_k").Caption

            If (Me.TextBox_IT_ID.DataBindings.Count = 0) Then
                Me.TextBox_IT_ID.DataBindings.Add(New Binding("Text", m_bs, "IT_ID", True))
            End If
            If (Me.TextBox_IT_Nome.DataBindings.Count = 0) Then
                Me.TextBox_IT_Nome.DataBindings.Add(New Binding("Text", m_bs, "IT_Nome", True))
            End If
            If (Me.TextBox_IT_UM.DataBindings.Count = 0) Then
                Me.TextBox_IT_UM.DataBindings.Add(New Binding("Text", m_bs, "IT_UM", True))
            End If
            If (Me.TextBox_IT_K.DataBindings.Count = 0) Then
                Me.TextBox_IT_K.DataBindings.Add(New Binding("Text", m_bs, "IT_k", True))
            End If

        End If

        Me.TextBox_IT_ID.ReadOnly = True
        Me.TextBox_IT_Nome.ReadOnly = True
        Me.TextBox_IT_UM.ReadOnly = True
        Me.TextBox_IT_K.ReadOnly = True
    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_IT_ID.DataBindings.Count > 0) Then
            Me.TextBox_IT_ID.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_Nome.DataBindings.Count > 0) Then
            Me.TextBox_IT_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_UM.DataBindings.Count > 0) Then
            Me.TextBox_IT_UM.DataBindings.Clear()
        End If
        If (Me.TextBox_IT_K.DataBindings.Count > 0) Then
            Me.TextBox_IT_K.DataBindings.Clear()
        End If

        Me.TextBox_IT_ID.ReadOnly = False
        Me.TextBox_IT_Nome.ReadOnly = False
        Me.TextBox_IT_UM.ReadOnly = False
        Me.TextBox_IT_K.ReadOnly = False
    End Sub
End Class
