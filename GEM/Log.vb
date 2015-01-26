Imports System.Data.SqlClient

Public Class log
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
        Me.Text = "Log Eventi"

        ' Imposto la data di Start, l'evento scatenato richiama il caricamento dei dati
        Dim dt As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        DateTimePicker_START.Value = dt
        DateTimePicker_STOP.Value = dt.Add(TimeSpan.Parse("23:59:59"))

        CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
        ' Visualizzo il Nr di DL che sono riusciti a connettersi almeno 1 volta
        Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Text = GetTotalLIIDConnectedFromLog(DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()
        ' Visualizzo memoria
        Label_Memory_Details.Text = "Dettagli Memoria: Fisica Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Virtuale Tot.: " + My.Computer.Info.TotalVirtualMemory.ToString + ". Disponibile Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Disponibile Virt.: " + My.Computer.Info.AvailablePhysicalMemory.ToString

        Timer_1.Start()

        MyBase.BaseForm_1_Load(sender, e)

        Me.ToolStripButton_Nuovo.Enabled = False
        Me.ToolStripButton_Elimina.Enabled = False
        Me.ToolStripButton_Modifica.Enabled = False
        Me.ToolStripButton_Salva.Enabled = False
        Me.ToolStripButton_Annulla.Enabled = False

        Me.TextBox_LIID.Enabled = False

    End Sub

    Private Sub CaricaDati(ByVal bSoloAllarmi As Boolean, ByVal bSoloTotali As Boolean, ByVal bSoloInvioReportPerEmail As Boolean, ByVal bUltimo100Eventi As Boolean, ByVal iLIID As Integer, ByVal bFiltraPerLIID As Boolean)

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            If bUltimo100Eventi = True Then
                strSQL = " SELECT TOP 100 [Log].LG_LI_ID, [Log].LG_UltimoUDTMem, Azione.A_Nome, Risultato.R_Nome, [Log].LG_Descrizione, [Log].LG_LocalHostName, [Log].LG_LocalAddress, [Log].LG_RemoteAddress, [Log].LG_DataOra, Utente.U_NomeCognome "
            Else
                strSQL = " SELECT [Log].LG_LI_ID, [Log].LG_UltimoUDTMem, Azione.A_Nome, Risultato.R_Nome, [Log].LG_Descrizione, [Log].LG_LocalHostName, [Log].LG_LocalAddress, [Log].LG_RemoteAddress, [Log].LG_DataOra, Utente.U_NomeCognome "
            End If
            strSQL = strSQL + " FROM [Log] "
            strSQL = strSQL + " INNER JOIN Azione ON [Log].LG_A_ID = Azione.A_ID "
            strSQL = strSQL + " INNER JOIN Risultato ON [Log].LG_R_ID = Risultato.R_ID  "
            strSQL = strSQL + " INNER JOIN Utente ON [Log].LG_U_ID = Utente.U_ID AND Azione.A_U_ID = Utente.U_ID  "
            strSQL = strSQL + " WHERE (LG_DataOra >= CONVERT(DATETIME, @LG_DataOra_Start, 105) AND LG_DataOra <= CONVERT(DATETIME, @LG_DataOra_Stop, 105)) "
            If bSoloAllarmi = True Then
                strSQL = strSQL + " AND (Risultato.R_ID = 3 OR Risultato.R_ID = 4 OR Risultato.R_ID = 12 OR Risultato.R_ID = 13 OR Risultato.R_ID = 100 OR Risultato.R_ID = 10000 OR Risultato.R_ID = 11000) "
            End If

            If bSoloTotali = True Then
                strSQL = strSQL + " AND (Azione.A_ID = 510) "
            End If

            If bSoloInvioReportPerEmail = True Then
                strSQL = strSQL + " AND (Azione.A_ID = 51 OR Azione.A_ID = 61) "
            End If

            If bFiltraPerLIID = True Then
                strSQL = strSQL + " AND ([Log].LG_LI_ID = " + iLIID.ToString + ") "
            End If
            strSQL = strSQL + " AND ([Log].LG_RemoteAddress LIKE '%" + Me.TextBox_LG_RemoteAddress.Text + "%') "

            If bSoloInvioReportPerEmail = True Then
                strSQL = strSQL + " ORDER BY LG_LI_ID, LG_ID DESC "
            Else
                strSQL = strSQL + " ORDER BY LG_ID DESC  "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_DataOra_Start", DateTimePicker_START.Value)
            cmd.Parameters.AddWithValue("@LG_DataOra_Stop", DateTimePicker_STOP.Value)

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

    Protected Overrides Sub DataGridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim gw As DataGridView

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try
            gw = sender
            If gw.CurrentCell.ColumnIndex = 0 Then

                Dim str_1 As String
                Dim iIndice_1 As Integer

                strSQL = " SELECT C_Nome, IDP_Nome, LI_Nr, L_Marca + ' - ' + L_Modello as L_TIPO "
                strSQL = strSQL + " FROM LoggerInst INNER JOIN Logger ON LI_L_ID = L_ID INNER JOIN ImpiantoDiProduzione ON LI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
                strSQL = strSQL + " WHERE LI_ID = " + gw.CurrentCell.Value.ToString()

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

                        str_1 = ""
                        If ds.Tables(0).Rows.Count > 0 Then
                            iIndice_1 = ds.Tables(0).Columns.Count
                            For iIndice_1 = 0 To ds.Tables(0).Columns.Count - 1
                                str_1 = str_1 + ds.Tables(0).Columns(iIndice_1).Caption()
                                str_1 = str_1 + " - "
                                str_1 = str_1 + ds.Tables(0).Rows(0).Item(iIndice_1).ToString()
                                str_1 = str_1 + vbCrLf
                            Next iIndice_1
                        Else
                            str_1 = " L' ID: " + gw.CurrentCell.Value.ToString() + " Non corrisponde a nessun Data Logger."
                        End If

                        System.Windows.Forms.MessageBox.Show(Owner, str_1, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If
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

        MyBase.DataGridView_CellDoubleClick(sender, e)

    End Sub

    Private Sub Timer_1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_1.Tick
        Static dtStatNowDay As Date = Date.Now
        Dim iIDDL As Integer
        Timer_1.Stop()
        If Me.CheckBoxAbilitaAggiornamento.Checked = True Then
            If dtStatNowDay.DayOfYear <> Date.Now.DayOfYear Then
                Dim dt As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
                DateTimePicker_START.Value = dt
                DateTimePicker_STOP.Value = dt.Add(TimeSpan.Parse("23:59:59"))
            End If
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        End If
        ' Visualizzo memoria
        Label_Memory_Details.Text = "Dettagli Memoria: Fisica Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Virtuale Tot.: " + My.Computer.Info.TotalVirtualMemory.ToString + ". Disponibile Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Disponibile Virt.: " + My.Computer.Info.AvailablePhysicalMemory.ToString

        Timer_1.Start()
    End Sub

    Private Sub RadioButtonVisualizzaTutto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonVisualizzaTutto.CheckedChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        Else
            bFirstShot = True
        End If

    End Sub

    Private Sub RadioButtonVisualizzaSoloGliAllarmi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonVisualizzaSoloGliAllarmi.CheckedChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub RadioButtonVisualizzaSoloITotali_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonVisualizzaSoloITotali.CheckedChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub RadioButtonVisualizzaSoloInvioReportPerEmail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonVisualizzaSoloInvioReportPerEmail.CheckedChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub DateTimePicker_START_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_START.ValueChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Me.CheckBoxAbilitaAggiornamento.Checked = False
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
            ' Visualizzo il Nr di DL che sono riusciti a connettersi almeno 1 volta
            Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Text = GetTotalLIIDConnectedFromLog(DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub DateTimePicker_STOP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_STOP.ValueChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Me.CheckBoxAbilitaAggiornamento.Checked = False
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
            ' Visualizzo il Nr di DL che sono riusciti a connettersi almeno 1 volta
            Label_Nr_DL_Con_Almeno_1_Connessione_Dato.Text = GetTotalLIIDConnectedFromLog(DateTimePicker_START.Value, DateTimePicker_STOP.Value).ToString()
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub CheckBoxAbilitaAggiornamento_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAbilitaAggiornamento.CheckedChanged
        Static bFirstShot As Boolean = False
        If Me.CheckBoxAbilitaAggiornamento.Checked = True Then
            Me.CheckBox_VisualizzaUltimi100Eventi.Checked = True
        End If

        If bFirstShot = True Then
            If Me.CheckBoxAbilitaAggiornamento.Checked = True Then
                Dim dt As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
                DateTimePicker_START.Value = dt
                DateTimePicker_STOP.Value = dt.Add(TimeSpan.Parse("23:59:59"))
                Me.CheckBoxAbilitaAggiornamento.Checked = True
            End If
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub CheckBox_VisualizzaUltimi100Eventi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_VisualizzaUltimi100Eventi.CheckedChanged
        Static bFirstShot As Boolean = False
        Dim iIDDL As Integer

        If bFirstShot = True Then
            Try
                iIDDL = CInt(TextBox_LIID.Text)
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
            Catch ex As Exception
                CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
            End Try
        Else
            bFirstShot = True
        End If
    End Sub

    Private Sub TextBox_LIID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_LIID.TextChanged
        Dim iIDDL As Integer
        Try
            iIDDL = CInt(TextBox_LIID.Text)
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
        Catch ex As Exception
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
        End Try
    End Sub

    Private Sub CheckBox_FiltraPerLIID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_FiltraPerLIID.CheckedChanged
        Dim iIDDL As Integer

        Me.TextBox_LIID.Enabled = Me.CheckBox_FiltraPerLIID.Checked

        Try
            iIDDL = CInt(TextBox_LIID.Text)
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
        Catch ex As Exception
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
        End Try
    End Sub

    Private Sub TextBox_LG_RemoteAddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_LG_RemoteAddress.TextChanged
        Dim iIDDL As Integer
        Try
            iIDDL = CInt(TextBox_LIID.Text)
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)
        Catch ex As Exception
            CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
        End Try
    End Sub

    Private Sub Button_Elimina_Storico_Fino_A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Elimina_Storico_Fino_A.Click
        Dim lg As New Login
        Dim dlgr As Windows.Forms.DialogResult
        Dim iIDDL As Integer
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("Log")
        dlgr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dlgr = Windows.Forms.DialogResult.Yes Then
            If Me.CheckBox_FiltraPerLIID.Checked = True Then
                Try
                    iIDDL = CInt(TextBox_LIID.Text)

                    If Windows.Forms.MessageBox.Show("Sei Sicuro? Premendo su Ok verranno cancellati tutti i dati del Log del DL con ID: " + iIDDL.ToString() + " fino al: " + DateTimePicker_STOP.Value.ToString("G") + " ad esclusione dei riferimenti degli ultimi UDT", "Eliminazione dati di Log", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

                        DeleteLogEventi(iIDDL, DateTimePicker_STOP.Value)

                        CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, iIDDL, Me.CheckBox_FiltraPerLIID.Checked)

                    End If
                Catch ex As Exception
                    Windows.Forms.MessageBox.Show("Specificare un ID DataLogger abilitando 'Filtra per ID DataLogger'." + vbCrLf + "(ID 0 = Azioni Generali GEM)", "Eliminazione dati di Log", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    CaricaDati(Me.RadioButtonVisualizzaSoloGliAllarmi.Checked, Me.RadioButtonVisualizzaSoloITotali.Checked, Me.RadioButtonVisualizzaSoloInvioReportPerEmail.Checked, Me.CheckBox_VisualizzaUltimi100Eventi.Checked, 0, False)
                End Try
            Else
                Windows.Forms.MessageBox.Show("Specificare un ID DataLogger abilitando 'Filtra per ID DataLogger'." + vbCrLf + "(ID 0 = Azioni Generali GEM)", "Eliminazione dati di Log", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If

    End Sub

End Class
