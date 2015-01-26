Imports System.Data.SqlClient

Public Class LoggerParamConfig

    Dim m_bs As New BindingSource

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Private Sub LoggerParamConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Parametri Configurazione Datalogger"

        'Prelevo il dato Server
        CaricaDati()

        EseguiBinding()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
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

                            strSQL = " UPDATE [LoggerParamConfig] "
                            strSQL = strSQL + " SET LPC_Nome = @LPC_Nome, LPC_Min_Value = @LPC_Min_Value, LPC_Max_Value = @LPC_Max_Value, LPC_Default_Value = @LPC_Default_Value, LPC_UM = @LPC_UM, LPC_DataOra = @LPC_DataOra, LPC_U_ID = @LPC_U_ID "
                            strSQL = strSQL + " WHERE LPC_ID = @LPC_ID AND LPC_CC = @LPC_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LPC_Nome") = Me.TextBox_LPC_Nome.Text
                            dr.Item("LPC_Min_Value") = Me.TextBox_LPC_Min_Value.Text
                            dr.Item("LPC_Max_Value") = Me.TextBox_LPC_Max_Value.Text
                            dr.Item("LPC_Default_Value") = Me.TextBox_LPC_Default_Value.Text
                            dr.Item("LPC_UM") = Me.TextBox_LPC_UM.Text
                            dr.Item("LPC_DataOra") = Date.Now
                            dr.Item("LPC_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LPC_ID", dr.Item("LPC_ID"))
                            cmd.Parameters.AddWithValue("@LPC_CC", dr.Item("LPC_CC"))
                            cmd.Parameters.AddWithValue("@LPC_Nome", dr.Item("LPC_Nome"))
                            cmd.Parameters.AddWithValue("@LPC_Min_Value", dr.Item("LPC_Min_Value"))
                            cmd.Parameters.AddWithValue("@LPC_Max_Value", dr.Item("LPC_Max_Value"))
                            cmd.Parameters.AddWithValue("@LPC_Default_Value", dr.Item("LPC_Default_Value"))
                            cmd.Parameters.AddWithValue("@LPC_UM", dr.Item("LPC_UM"))
                            cmd.Parameters.AddWithValue("@LPC_DataOra", dr.Item("LPC_DataOra"))
                            cmd.Parameters.AddWithValue("@LPC_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
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

        If (Me.TextBox_LPC_Nome.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Min_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Min_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Max_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Max_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Default_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Default_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_UM.DataBindings.Count > 0) Then
            Me.TextBox_LPC_UM.DataBindings.Clear()
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
            strSQL = " SELECT LPC_ID, LPC_Nome, LPC_Min_Value, LPC_Max_Value, LPC_Default_Value, LPC_UM, LPC_Tipo, LPC_Indirizzo_Registro_L, LPC_Indirizzo_Registro_H, LPC_ReadOnly, LPC_DataOra, LPC_U_ID, Utente.U_NomeCognome, LPC_CC"
            strSQL = strSQL + " FROM LoggerParamConfig INNER JOIN Utente ON LPC_U_ID = U_ID "

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

            Me.Label_LPC_Nome.Text = m_bs.DataSource.Columns("LPC_Nome").Caption
            Me.Label_LPC_Min_Value.Text = m_bs.DataSource.Columns("LPC_Min_Value").Caption
            Me.Label_LPC_Max_Value.Text = m_bs.DataSource.Columns("LPC_Max_Value").Caption
            Me.Label_LPC_Default_Value.Text = m_bs.DataSource.Columns("LPC_Default_Value").Caption
            Me.Label_LPC_UM.Text = m_bs.DataSource.Columns("LPC_UM").Caption

            If (Me.TextBox_LPC_Nome.DataBindings.Count = 0) Then
                Me.TextBox_LPC_Nome.DataBindings.Add(New Binding("Text", m_bs, "LPC_Nome", True))
            End If
            If (Me.TextBox_LPC_Min_Value.DataBindings.Count = 0) Then
                Me.TextBox_LPC_Min_Value.DataBindings.Add(New Binding("Text", m_bs, "LPC_Min_Value", True))
            End If
            If (Me.TextBox_LPC_Max_Value.DataBindings.Count = 0) Then
                Me.TextBox_LPC_Max_Value.DataBindings.Add(New Binding("Text", m_bs, "LPC_Max_Value", True))
            End If
            If (Me.TextBox_LPC_Default_Value.DataBindings.Count = 0) Then
                Me.TextBox_LPC_Default_Value.DataBindings.Add(New Binding("Text", m_bs, "LPC_Default_Value", True))
            End If
            If (Me.TextBox_LPC_UM.DataBindings.Count = 0) Then
                Me.TextBox_LPC_UM.DataBindings.Add(New Binding("Text", m_bs, "LPC_UM", True))
            End If

        End If

        Me.TextBox_LPC_Nome.ReadOnly = True
        Me.TextBox_LPC_Min_Value.ReadOnly = True
        Me.TextBox_LPC_Max_Value.ReadOnly = True
        Me.TextBox_LPC_Default_Value.ReadOnly = True
        Me.TextBox_LPC_UM.ReadOnly = True
    End Sub

    Private Sub RimuoviBinding()
        If (Me.TextBox_LPC_Nome.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Nome.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Min_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Min_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Max_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Max_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_Default_Value.DataBindings.Count > 0) Then
            Me.TextBox_LPC_Default_Value.DataBindings.Clear()
        End If
        If (Me.TextBox_LPC_UM.DataBindings.Count > 0) Then
            Me.TextBox_LPC_UM.DataBindings.Clear()
        End If

        Me.TextBox_LPC_Nome.ReadOnly = False
        Me.TextBox_LPC_Min_Value.ReadOnly = False
        Me.TextBox_LPC_Max_Value.ReadOnly = False
        Me.TextBox_LPC_Default_Value.ReadOnly = False
        Me.TextBox_LPC_UM.ReadOnly = False
    End Sub

End Class
