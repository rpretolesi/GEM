Imports System.Data.SqlClient

Public Class VisualizzaUtentiCollegatiAlDB
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
        Me.Text = "Utenti Connessi"

        Me.CheckBoxAbilitaAggiornamento.Checked = True

        CaricaDati()

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

    End Sub

    Private Sub CaricaDati()

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        ScriviLogEventi(0, 0, AZIONE_COMPATTA_DATABASE, RISULTATO_OPERAZIONE_IN_CORSO, "Dimensione prima della compattazione: " + DimensioneDatabase(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " sp_who2 "

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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

End Class
