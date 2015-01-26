Imports System.Data.SqlClient

Public Class DatiGeneraliImpianto

    Dim m_bs As New BindingSource

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Private Sub DatiGeneraliImpianto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Dati Generali Impianto"

        'Prelevo il dato Server
        Me.CheckBox_Server.Checked = My.Settings.Server

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

                            strSQL = " UPDATE [DatiGeneraliImpianto] "
                            strSQL = strSQL + " SET DGI_Valore = @DGI_Valore, DGI_DataOra = @DGI_DataOra, DGI_U_ID = @DGI_U_ID "
                            strSQL = strSQL + " WHERE DGI_ID = @DGI_ID AND DGI_CC = @DGI_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("DGI_Valore") = Me.TextBox_DGI_Valore.Text
                            dr.Item("DGI_DataOra") = Date.Now
                            dr.Item("DGI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@DGI_ID", dr.Item("DGI_ID"))
                            cmd.Parameters.AddWithValue("@DGI_CC", dr.Item("DGI_CC"))
                            cmd.Parameters.AddWithValue("@DGI_Valore", dr.Item("DGI_Valore"))
                            cmd.Parameters.AddWithValue("@DGI_DataOra", dr.Item("DGI_DataOra"))
                            cmd.Parameters.AddWithValue("@DGI_U_ID", m_iUID)

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

        If (Me.TextBox_DGI_Valore.DataBindings.Count > 0) Then
            Me.TextBox_DGI_Valore.DataBindings.Clear()
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

            strSQL = " SELECT DGI_ID, DGI_Valore, DGI_Descrizione, DGI_DataOra, DGI_U_ID, Utente.U_NomeCognome, DGI_CC "
            strSQL = strSQL + " FROM DatiGeneraliImpianto INNER JOIN Utente ON DGI_U_ID = U_ID "

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

            Me.Label_DGI_Valore.Text = m_bs.DataSource.Columns("DGI_Valore").Caption

            If (Me.TextBox_DGI_Valore.DataBindings.Count = 0) Then
                Me.TextBox_DGI_Valore.DataBindings.Add(New Binding("Text", m_bs, "DGI_Valore", True))
            End If

        End If

        Me.TextBox_DGI_Valore.ReadOnly = True
    End Sub

    Private Sub RimuoviBinding()
        If (Me.TextBox_DGI_Valore.DataBindings.Count > 0) Then
            Me.TextBox_DGI_Valore.DataBindings.Clear()
        End If

        Me.TextBox_DGI_Valore.ReadOnly = False
    End Sub

    Private Sub CheckBox_Server_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Server.CheckedChanged
        If My.Settings.Server <> Me.CheckBox_Server.Checked Then
            My.Settings.Server = Me.CheckBox_Server.Checked
            My.Settings.Save() 'save the settings        
            System.Windows.Forms.MessageBox.Show(Owner, "Per rendere effettive le modifiche e' necessario riavviare il programma.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If System.Windows.Forms.MessageBox.Show(Owner, "Premere Ok per riavviare automaticamente, Annulla per farlo manualmente.", My.Application.Info.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                Application.Restart()
            End If
        End If
    End Sub
End Class
