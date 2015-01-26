﻿Imports System.Data.SqlClient

Public Class Logger
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

        Me.Text = "Logger"

        CaricaDati()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then

            RimuoviBinding()

            Me.TextBox_L_Marca.Text = ""
            Me.TextBox_L_Modello.Text = ""

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
        Dim dr As DataRow

        If Not m_bs.DataSource Is Nothing Then

            If Not m_bs.Current() Is Nothing Then

                dr = m_bs.Current().Row()

                Try
                    strSQL = "DELETE FROM [Logger] "
                    strSQL = strSQL + "WHERE L_ID = @L_ID AND L_CC = @L_CC "

                    CustomSQLConnectionOpen(cn, cmd)
                    'cmd.Connection = cn
                    cmd.CommandText = strSQL

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@L_ID", dr.Item("L_ID"))
                    cmd.Parameters.AddWithValue("@L_CC", dr.Item("L_CC"))

                    If cmd.ExecuteNonQuery() > 0 Then
                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", m_iUID, Me)
                    Else
                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                    End If

                    dr.Delete()

                Catch ex As Exception
                    ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
                End Try

            End If

        End If

        ds.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If Not m_bs.DataSource Is Nothing Then

            CaricaDati()

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

                            strSQL = " UPDATE [Logger] "
                            strSQL = strSQL + " SET L_Marca = @L_Marca, L_Modello = @L_Modello, L_DataOra = @L_DataOra, L_U_ID = @L_U_ID "
                            strSQL = strSQL + " WHERE L_ID = @L_ID AND L_CC = @L_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("L_Marca") = Me.TextBox_L_Marca.Text
                            dr.Item("L_Modello") = Me.TextBox_L_Modello.Text
                            dr.Item("L_DataOra") = Date.Now
                            dr.Item("L_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@L_ID", dr.Item("L_ID"))
                            cmd.Parameters.AddWithValue("@L_CC", dr.Item("L_CC"))
                            cmd.Parameters.AddWithValue("@L_Marca", dr.Item("L_Marca"))
                            cmd.Parameters.AddWithValue("@L_Modello", dr.Item("L_Modello"))
                            cmd.Parameters.AddWithValue("@L_DataOra", dr.Item("L_DataOra"))
                            cmd.Parameters.AddWithValue("@L_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [Logger] "
                            strSQL = strSQL + "  (L_Marca,  L_Modello,  L_DataOra,  L_U_ID) VALUES "
                            strSQL = strSQL + "  (@L_Marca, @L_Modello, @L_DataOra, @L_U_ID) "
                            strSQL = strSQL + " SELECT @L_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("L_Marca") = Me.TextBox_L_Marca.Text
                            dr.Item("L_Modello") = Me.TextBox_L_Modello.Text
                            dr.Item("L_DataOra") = Date.Now
                            dr.Item("L_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@L_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@L_Marca", dr.Item("L_Marca"))
                            cmd.Parameters.AddWithValue("@L_Modello", dr.Item("L_Modello"))
                            cmd.Parameters.AddWithValue("@L_DataOra", dr.Item("L_DataOra"))
                            cmd.Parameters.AddWithValue("@L_U_ID", dr.Item("L_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("L_ID") = cmd.Parameters.Item("@L_ID").Value
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

        If (Me.TextBox_L_Marca.DataBindings.Count > 0) Then
            Me.TextBox_L_Marca.DataBindings.Clear()
        End If
        If (Me.TextBox_L_Modello.DataBindings.Count > 0) Then
            Me.TextBox_L_Modello.DataBindings.Clear()
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

            strSQL = " SELECT L_ID, L_Marca, L_Modello, L_DataOra, L_U_ID, Utente.U_NomeCognome, L_CC "
            strSQL = strSQL + " FROM Logger INNER JOIN Utente ON L_U_ID = U_ID "

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

            Me.Label_L_Marca.Text = m_bs.DataSource.Columns("L_Marca").Caption
            Me.Label_L_Modello.Text = m_bs.DataSource.Columns("L_Modello").Caption

            If (Me.TextBox_L_Marca.DataBindings.Count = 0) Then
                Me.TextBox_L_Marca.DataBindings.Add(New Binding("Text", m_bs, "L_Marca", True))
            End If
            If (Me.TextBox_L_Modello.DataBindings.Count = 0) Then
                Me.TextBox_L_Modello.DataBindings.Add(New Binding("Text", m_bs, "L_Modello", True))
            End If

        End If

        Me.TextBox_L_Marca.ReadOnly = True
        Me.TextBox_L_Modello.ReadOnly = True
    End Sub

    Private Sub RimuoviBinding()

        If (Me.TextBox_L_Marca.DataBindings.Count > 0) Then
            Me.TextBox_L_Marca.DataBindings.Clear()
        End If
        If (Me.TextBox_L_Modello.DataBindings.Count > 0) Then
            Me.TextBox_L_Modello.DataBindings.Clear()
        End If

        Me.TextBox_L_Marca.ReadOnly = False
        Me.TextBox_L_Modello.ReadOnly = False
    End Sub
End Class
