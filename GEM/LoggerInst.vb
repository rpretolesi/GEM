Imports System.Data.SqlClient

Public Class LoggerInst
    Dim m_bs As New BindingSource

    Private m_iCID As Integer
    Private m_iIDPID As Integer

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

    Protected Overrides Sub BaseForm_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Text = "Logger Installati"

        CaricaCID()

        CaricaIDPID()

        CaricaLID()

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
            CaricaDati()
        End If

    End Sub

    Private Sub ComboBox_IDP_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_IDP_ID.SelectedIndexChanged
        Dim cb As ComboBox
        Dim st As Type = GetType(Integer)
        cb = sender

        If cb.SelectedValue.GetType().Name = st.Name Then
            m_iIDPID = Me.ComboBox_IDP_ID.SelectedValue()
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
                            If Not DataGridView.SelectedRows(0).Cells("L_TIPO").Value Is DBNull.Value Then
                                Me.ComboBox_L_ID.Text = DataGridView.SelectedRows(0).Cells("L_TIPO").Value
                            End If

                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("LI_ID").Value Is DBNull.Value Then
                                If DataGridView.SelectedRows(0).Cells("LI_ID").Value > 0 Then

                                    If Not Application.OpenForms.Item("LoggerInstParamConfig") Is Nothing Then
                                        LoggerInstParamConfig.Close()
                                        LoggerInstParamConfig.UID = m_iUID
                                        LoggerInstParamConfig.LIID = DataGridView.SelectedRows(0).Cells("LI_ID").Value
                                        LoggerInstParamConfig.MdiParent = Me.MdiParent
                                        LoggerInstParamConfig.Show()
                                    End If

                                End If
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
        'Try
        '    If DataGridView.SelectedRows.Count > 0 Then
        '        If DataGridView.SelectedRows(0).Index >= 0 Then
        '            If Not m_bs.DataSource Is Nothing Then
        '                If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
        '                    If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("LI_ID").Value Is DBNull.Value Then

        '                        Dim lg As New Login
        '                        Dim dr As Windows.Forms.DialogResult
        '                        lg.UID = m_iUID
        '                        lg.ULivello = GetMinLevelReq("InverterTesterInst")
        '                        dr = lg.ShowDialog()
        '                        m_iUID = lg.UID
        '                        If dr = Windows.Forms.DialogResult.Yes Then
        '                            InverterTesterInst.Close()
        '                            InverterTesterInst.UID = m_iUID
        '                            If m_iCID > 0 Then
        '                                InverterTesterInst.CID = m_iCID
        '                            Else
        '                                InverterTesterInst.CID = DataGridView.SelectedRows(0).Cells("C_ID").Value
        '                            End If
        '                            If m_iIDPID > 0 Then
        '                                InverterTesterInst.IDPID = m_iIDPID
        '                            Else
        '                                InverterTesterInst.IDPID = DataGridView.SelectedRows(0).Cells("IDP_ID").Value
        '                            End If

        '                            InverterTesterInst.LIID = DataGridView.SelectedRows(0).Cells("LI_ID").Value

        '                            InverterTesterInst.MdiParent = Me.MdiParent
        '                            InverterTesterInst.Show()
        '                        End If

        '                        lg.Dispose()

        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        'End Try

        MyBase.DataGridView_RowHeaderMouseDoubleClick(sender, e)

    End Sub

    Protected Overrides Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not m_bs.DataSource Is Nothing Then

            RimuoviBinding()

            Me.TextBox_LIRel.Text = ""
            Me.TextBox_LI_PotenzaGestitaKw.Text = ""
            Me.TextBox_LI_Kd.Text = "1"
            Me.TextBox_Soglia_Calcolo_HG.Text = "100"
            Me.TextBox_LI_EuroKwh.Text = "0"
            Me.TextBox_LI_Nr.Text = ""
            Me.TextBox_LI_TCPIP_Set_Ind.Text = ""
            Me.TextBox_LI_TCPIP_Set_Port.Text = "502"
            Me.TextBox_LI_TCPIP_Get_Ind.Text = "0.0.0.0"
            Me.TextBox_LI_TCPIP_Get_Port.Text = "502"
            Me.TextBox_LI_TCPIP_Web_Port.Text = "80"
            Me.TextBox_LI_OperatoreSim.Text = ""
            Me.TextBox_LI_NrSerialeSim.Text = ""
            Me.TextBox_LI_PINSim.Text = ""
            Me.TextBox_LI_NrTelefonicoSim.Text = ""
            Me.TextBox_LI_PUKSim.Text = ""
            Me.TextBox_LI_AutoAggDopoXOre.Text = "0"
            Me.TextBox_LI_Note.Text = ""
            Me.CheckBox_LI_LogModbus.Checked = False
            Me.CheckBox_LI_In_Funzione.Checked = False
            Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Checked = False

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

                                strSQL = "DELETE FROM [LoggerInst] "
                                strSQL = strSQL + "WHERE LI_ID = @LI_ID AND LI_CC = @LI_CC "

                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@LI_ID", dgvr.Cells("LI_ID").Value)
                                cmd.Parameters.AddWithValue("@LI_CC", dgvr.Cells("LI_CC").Value)

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

                            strSQL = " UPDATE [LoggerInst] "
                            strSQL = strSQL + " SET LI_L_ID = @LI_L_ID, LI_IDP_ID = @LI_IDP_ID, LI_Rel = @LI_Rel, LI_LogModbus = @LI_LogModbus, LI_PotenzaGestitaKw = @LI_PotenzaGestitaKw, LI_Kd = @LI_Kd, LI_SogliaCalcoloHG = @LI_SogliaCalcoloHG, LI_EuroKwh = @LI_EuroKwh, LI_Nr = @LI_Nr, LI_TCPIP_Set_Ind = @LI_TCPIP_Set_Ind, LI_TCPIP_Set_Port = @LI_TCPIP_Set_Port, LI_TCPIP_Web_Port = @LI_TCPIP_Web_Port, LI_Auto_Get_TCPIP_Ind_Port = @LI_Auto_Get_TCPIP_Ind_Port, LI_TCPIP_Get_Ind = @LI_TCPIP_Get_Ind, LI_TCPIP_Get_Port = @LI_TCPIP_Get_Port, LI_OperatoreSim = @LI_OperatoreSim, LI_NrSerialeSim = @LI_NrSerialeSim, LI_PINSim = @LI_PINSim, LI_NrTelefonicoSim = @LI_NrTelefonicoSim, LI_PUKSim = @LI_PUKSim, LI_AutoAggDopoXOre = @LI_AutoAggDopoXOre, LI_Note = @LI_Note, LI_In_Funzione = @LI_In_Funzione, LI_DataOra = @LI_DataOra, LI_U_ID = @LI_U_ID "
                            strSQL = strSQL + " WHERE LI_ID = @LI_ID AND LI_CC = @LI_CC "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LI_L_ID") = Me.ComboBox_L_ID.SelectedValue()
                            dr.Item("LI_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("LI_Rel") = Me.TextBox_LIRel.Text
                            dr.Item("LI_LogModbus") = Me.CheckBox_LI_LogModbus.Checked
                            dr.Item("LI_PotenzaGestitaKw") = Me.TextBox_LI_PotenzaGestitaKw.Text
                            dr.Item("LI_Kd") = Me.TextBox_LI_Kd.Text
                            dr.Item("LI_SogliaCalcoloHG") = Me.TextBox_Soglia_Calcolo_HG.Text
                            dr.Item("LI_EuroKwh") = Me.TextBox_LI_EuroKwh.Text
                            dr.Item("LI_Nr") = Me.TextBox_LI_Nr.Text
                            dr.Item("LI_TCPIP_Set_Ind") = Me.TextBox_LI_TCPIP_Set_Ind.Text
                            dr.Item("LI_TCPIP_Set_Port") = Me.TextBox_LI_TCPIP_Set_Port.Text
                            dr.Item("LI_TCPIP_Web_Port") = Me.TextBox_LI_TCPIP_Web_Port.Text
                            dr.Item("LI_Auto_Get_TCPIP_Ind_Port") = Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Checked
                            dr.Item("LI_TCPIP_Get_Ind") = Me.TextBox_LI_TCPIP_Get_Ind.Text
                            dr.Item("LI_TCPIP_Get_Port") = Me.TextBox_LI_TCPIP_Get_Port.Text
                            dr.Item("LI_OperatoreSim") = Me.TextBox_LI_OperatoreSim.Text
                            dr.Item("LI_NrSerialeSim") = Me.TextBox_LI_NrSerialeSim.Text
                            dr.Item("LI_PINSim") = Me.TextBox_LI_PINSim.Text
                            dr.Item("LI_NrTelefonicoSim") = Me.TextBox_LI_NrTelefonicoSim.Text
                            dr.Item("LI_PUKSim") = Me.TextBox_LI_PUKSim.Text
                            dr.Item("LI_AutoAggDopoXOre") = Me.TextBox_LI_AutoAggDopoXOre.Text
                            dr.Item("LI_Note") = Me.TextBox_LI_Note.Text
                            dr.Item("LI_In_Funzione") = Me.CheckBox_LI_In_Funzione.Checked
                            dr.Item("LI_DataOra") = Date.Now
                            dr.Item("LI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LI_ID", dr.Item("LI_ID"))
                            cmd.Parameters.AddWithValue("@LI_CC", dr.Item("LI_CC"))
                            cmd.Parameters.AddWithValue("@LI_L_ID", dr.Item("LI_L_ID"))
                            cmd.Parameters.AddWithValue("@LI_IDP_ID", dr.Item("LI_IDP_ID"))
                            cmd.Parameters.AddWithValue("@LI_Rel", dr.Item("LI_Rel"))
                            cmd.Parameters.AddWithValue("@LI_LogModbus", dr.Item("LI_LogModbus"))
                            cmd.Parameters.AddWithValue("@LI_PotenzaGestitaKw", dr.Item("LI_PotenzaGestitaKw"))
                            cmd.Parameters.AddWithValue("@LI_Kd", dr.Item("LI_Kd"))
                            cmd.Parameters.AddWithValue("@LI_SogliaCalcoloHG", dr.Item("LI_SogliaCalcoloHG"))
                            cmd.Parameters.AddWithValue("@LI_EuroKwh", dr.Item("LI_EuroKwh"))
                            cmd.Parameters.AddWithValue("@LI_Nr", dr.Item("LI_Nr"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Set_Ind", dr.Item("LI_TCPIP_Set_Ind"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Set_Port", dr.Item("LI_TCPIP_Set_Port"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Web_Port", dr.Item("LI_TCPIP_Web_Port"))
                            cmd.Parameters.AddWithValue("@LI_Auto_Get_TCPIP_Ind_Port", dr.Item("LI_Auto_Get_TCPIP_Ind_Port"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Ind", dr.Item("LI_TCPIP_Get_Ind"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Port", dr.Item("LI_TCPIP_Get_Port"))
                            cmd.Parameters.AddWithValue("@LI_OperatoreSim", dr.Item("LI_OperatoreSim"))
                            cmd.Parameters.AddWithValue("@LI_NrSerialeSim", dr.Item("LI_NrSerialeSim"))
                            cmd.Parameters.AddWithValue("@LI_PINSim", dr.Item("LI_PINSim"))
                            cmd.Parameters.AddWithValue("@LI_NrTelefonicoSim", dr.Item("LI_NrTelefonicoSim"))
                            cmd.Parameters.AddWithValue("@LI_PUKSim", dr.Item("LI_PUKSim"))
                            cmd.Parameters.AddWithValue("@LI_AutoAggDopoXOre", dr.Item("LI_AutoAggDopoXOre"))
                            cmd.Parameters.AddWithValue("@LI_Note", dr.Item("LI_Note"))
                            cmd.Parameters.AddWithValue("@LI_In_Funzione", dr.Item("LI_In_Funzione"))
                            cmd.Parameters.AddWithValue("@LI_DataOra", dr.Item("LI_DataOra"))
                            cmd.Parameters.AddWithValue("@LI_U_ID", m_iUID)

                            If cmd.ExecuteNonQuery() > 0 Then
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", m_iUID, Me)
                            Else
                                ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_ERR_CONCORRENZA, "Attenzione, la tabella e' stata ricaricata.", "", "", m_iUID, Me)
                            End If

                            CaricaDati()

                        ElseIf dr.RowState = DataRowState.Added Then

                            strSQL = " INSERT INTO [LoggerInst] "
                            strSQL = strSQL + "  (LI_L_ID,  LI_IDP_ID,  LI_Rel,  LI_LogModbus,  LI_PotenzaGestitaKw,  LI_Kd,  LI_SogliaCalcoloHG,  LI_EuroKwh,  LI_Nr,  LI_TCPIP_Set_Ind,  LI_TCPIP_Set_Port,  LI_TCPIP_Web_Port,  LI_Auto_Get_TCPIP_Ind_Port,  LI_TCPIP_Get_Ind,  LI_TCPIP_Get_Port,  LI_OperatoreSim,  LI_NrSerialeSim,  LI_PINSim,  LI_NrTelefonicoSim,  LI_PUKSim,  LI_AutoAggDopoXOre,  LI_Note,  LI_In_Funzione,  LI_DataOra,  LI_U_ID) VALUES "
                            strSQL = strSQL + "  (@LI_L_ID, @LI_IDP_ID, @LI_Rel, @LI_LogModbus, @LI_PotenzaGestitaKw, @LI_Kd, @LI_SogliaCalcoloHG, @LI_EuroKwh, @LI_Nr, @LI_TCPIP_Set_Ind, @LI_TCPIP_Set_Port, @LI_TCPIP_Web_Port, @LI_Auto_Get_TCPIP_Ind_Port, @LI_TCPIP_Get_Ind, @LI_TCPIP_Get_Port, @LI_OperatoreSim, @LI_NrSerialeSim, @LI_PINSim, @LI_NrTelefonicoSim, @LI_PUKSim, @LI_AutoAggDopoXOre, @LI_Note, @LI_In_Funzione, @LI_DataOra, @LI_U_ID) "
                            strSQL = strSQL + " SELECT @LI_ID = SCOPE_IDENTITY() "

                            CustomSQLConnectionOpen(cn, cmd)
                            'cmd.Connection = cn
                            cmd.CommandText = strSQL

                            dr.Item("LI_L_ID") = Me.ComboBox_L_ID.SelectedValue()
                            dr.Item("LI_IDP_ID") = Me.ComboBox_IDP_ID.SelectedValue()
                            dr.Item("LI_Rel") = Me.TextBox_LIRel.Text
                            dr.Item("LI_LogModbus") = Me.CheckBox_LI_LogModbus.Checked
                            dr.Item("LI_PotenzaGestitaKw") = Me.TextBox_LI_PotenzaGestitaKw.Text
                            dr.Item("LI_Kd") = Me.TextBox_LI_Kd.Text
                            dr.Item("LI_SogliaCalcoloHG") = Me.TextBox_Soglia_Calcolo_HG.Text
                            dr.Item("LI_EuroKwh") = Me.TextBox_LI_EuroKwh.Text
                            dr.Item("LI_Nr") = Me.TextBox_LI_Nr.Text
                            dr.Item("LI_TCPIP_Set_Ind") = Me.TextBox_LI_TCPIP_Set_Ind.Text
                            dr.Item("LI_TCPIP_Set_Port") = Me.TextBox_LI_TCPIP_Set_Port.Text
                            dr.Item("LI_TCPIP_Web_Port") = Me.TextBox_LI_TCPIP_Web_Port.Text
                            dr.Item("LI_Auto_Get_TCPIP_Ind_Port") = Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Checked
                            dr.Item("LI_TCPIP_Get_Ind") = Me.TextBox_LI_TCPIP_Get_Ind.Text
                            dr.Item("LI_TCPIP_Get_Port") = Me.TextBox_LI_TCPIP_Get_Port.Text
                            dr.Item("LI_OperatoreSim") = Me.TextBox_LI_OperatoreSim.Text
                            dr.Item("LI_NrSerialeSim") = Me.TextBox_LI_NrSerialeSim.Text
                            dr.Item("LI_PINSim") = Me.TextBox_LI_PINSim.Text
                            dr.Item("LI_NrTelefonicoSim") = Me.TextBox_LI_NrTelefonicoSim.Text
                            dr.Item("LI_PUKSim") = Me.TextBox_LI_PUKSim.Text
                            dr.Item("LI_AutoAggDopoXOre") = Me.TextBox_LI_AutoAggDopoXOre.Text
                            dr.Item("LI_Note") = Me.TextBox_LI_Note.Text
                            dr.Item("LI_In_Funzione") = Me.CheckBox_LI_In_Funzione.Checked
                            dr.Item("LI_DataOra") = Date.Now
                            dr.Item("LI_U_ID") = m_iUID
                            dr.Item("U_NomeCognome") = GetUNomeCognome(m_iUID)

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@LI_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            cmd.Parameters.AddWithValue("@LI_L_ID", dr.Item("LI_L_ID"))
                            cmd.Parameters.AddWithValue("@LI_IDP_ID", dr.Item("LI_IDP_ID"))
                            cmd.Parameters.AddWithValue("@LI_Rel", dr.Item("LI_Rel"))
                            cmd.Parameters.AddWithValue("@LI_LogModbus", dr.Item("LI_LogModbus"))
                            cmd.Parameters.AddWithValue("@LI_PotenzaGestitaKw", dr.Item("LI_PotenzaGestitaKw"))
                            cmd.Parameters.AddWithValue("@LI_Kd", dr.Item("LI_Kd"))
                            cmd.Parameters.AddWithValue("@LI_SogliaCalcoloHG", dr.Item("LI_SogliaCalcoloHG"))
                            cmd.Parameters.AddWithValue("@LI_EuroKwh", dr.Item("LI_EuroKwh"))
                            cmd.Parameters.AddWithValue("@LI_Nr", dr.Item("LI_Nr"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Set_Ind", dr.Item("LI_TCPIP_Set_Ind"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Set_Port", dr.Item("LI_TCPIP_Set_Port"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Web_Port", dr.Item("LI_TCPIP_Web_Port"))
                            cmd.Parameters.AddWithValue("@LI_Auto_Get_TCPIP_Ind_Port", dr.Item("LI_Auto_Get_TCPIP_Ind_Port"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Ind", dr.Item("LI_TCPIP_Get_Ind"))
                            cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Port", dr.Item("LI_TCPIP_Get_Port"))
                            cmd.Parameters.AddWithValue("@LI_OperatoreSim", dr.Item("LI_OperatoreSim"))
                            cmd.Parameters.AddWithValue("@LI_NrSerialeSim", dr.Item("LI_NrSerialeSim"))
                            cmd.Parameters.AddWithValue("@LI_PINSim", dr.Item("LI_PINSim"))
                            cmd.Parameters.AddWithValue("@LI_NrTelefonicoSim", dr.Item("LI_NrTelefonicoSim"))
                            cmd.Parameters.AddWithValue("@LI_PUKSim", dr.Item("LI_PUKSim"))
                            cmd.Parameters.AddWithValue("@LI_AutoAggDopoXOre", dr.Item("LI_AutoAggDopoXOre"))
                            cmd.Parameters.AddWithValue("@LI_Note", dr.Item("LI_Note"))
                            cmd.Parameters.AddWithValue("@LI_In_Funzione", dr.Item("LI_In_Funzione"))
                            cmd.Parameters.AddWithValue("@LI_DataOra", dr.Item("LI_DataOra"))
                            cmd.Parameters.AddWithValue("@LI_U_ID", dr.Item("LI_U_ID"))

                            If cmd.ExecuteNonQuery() > 0 Then
                                dr.Item("LI_ID") = cmd.Parameters.Item("@LI_ID").Value
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

        If (Me.TextBox_LIRel.DataBindings.Count > 0) Then
            Me.TextBox_LIRel.DataBindings.Clear()
        End If
        If (Me.CheckBox_LI_LogModbus.DataBindings.Count > 0) Then
            Me.CheckBox_LI_LogModbus.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Count > 0) Then
            Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Kd.DataBindings.Count > 0) Then
            Me.TextBox_LI_Kd.DataBindings.Clear()
        End If
        If (Me.TextBox_Soglia_Calcolo_HG.DataBindings.Count > 0) Then
            Me.TextBox_Soglia_Calcolo_HG.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_EuroKwh.DataBindings.Count > 0) Then
            Me.TextBox_LI_EuroKwh.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_LI_Nr.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Clear()
        End If
        If (Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Count > 0) Then
            Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_OperatoreSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_OperatoreSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_NrSerialeSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_NrSerialeSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PINSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_PINSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PUKSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_PUKSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_NrTelefonicoSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_NrTelefonicoSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Count > 0) Then
            Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Note.DataBindings.Count > 0) Then
            Me.TextBox_LI_Note.DataBindings.Clear()
        End If
        If (Me.CheckBox_LI_In_Funzione.DataBindings.Count > 0) Then
            Me.CheckBox_LI_In_Funzione.DataBindings.Clear()
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

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LoggerInst.*, L_Marca + ' - ' + L_Modello as L_TIPO, U_NomeCognome "
            strSQL = strSQL + " FROM LoggerInst INNER JOIN Utente ON LI_U_ID = U_ID INNER JOIN Logger ON LI_L_ID = L_ID INNER JOIN ImpiantoDiProduzione ON LI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
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

    Private Sub CaricaLID()
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT L_ID, L_Marca, L_Modello, L_Marca + ' - ' + L_Modello as L_TIPO "
            strSQL = strSQL + " FROM Logger "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    With ComboBox_L_ID
                        .DataSource = ds.Tables(0)
                        .DisplayMember = "L_TIPO"
                        .ValueMember = "L_ID"
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
            Me.Label_L_ID.Text = m_bs.DataSource.Columns("L_TIPO").Caption
            Me.Label_LIRel.Text = m_bs.DataSource.Columns("LI_Rel").Caption
            Me.CheckBox_LI_LogModbus.Text = m_bs.DataSource.Columns("LI_LogModbus").Caption
            Me.Label_LI_PotenzaGestitaKw.Text = m_bs.DataSource.Columns("LI_PotenzaGestitaKw").Caption
            Me.Label_LI_Kd.Text = m_bs.DataSource.Columns("LI_Kd").Caption
            Me.Label_Soglia_Calcolo_HG.Text = m_bs.DataSource.Columns("LI_SogliaCalcoloHG").Caption
            Me.TextBox_LI_EuroKwh.Text = m_bs.DataSource.Columns("LI_EuroKwh").Caption
            Me.Label_LI_Nr.Text = m_bs.DataSource.Columns("LI_Nr").Caption
            Me.Label_LI_TCPIP_Set_Ind.Text = m_bs.DataSource.Columns("LI_TCPIP_Set_Ind").Caption
            Me.Label_LI_TCPIP_Set_Port.Text = m_bs.DataSource.Columns("LI_TCPIP_Set_Port").Caption
            Me.Label_LI_TCPIP_Web_Port.Text = m_bs.DataSource.Columns("LI_TCPIP_Web_Port").Caption
            Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Text = m_bs.DataSource.Columns("LI_Auto_Get_TCPIP_Ind_Port").Caption
            Me.Label_LI_TCPIP_Get_Ind.Text = m_bs.DataSource.Columns("LI_TCPIP_Get_Ind").Caption
            Me.Label_LI_TCPIP_Get_Port.Text = m_bs.DataSource.Columns("LI_TCPIP_Get_Port").Caption
            Me.Label_LI_OperatoreSim.Text = m_bs.DataSource.Columns("LI_OperatoreSim").Caption
            Me.Label_LI_NrSerialeSim.Text = m_bs.DataSource.Columns("LI_NrSerialeSim").Caption
            Me.Label_LI_PINSim.Text = m_bs.DataSource.Columns("LI_PINSim").Caption
            Me.Label_LI_PUKSim.Text = m_bs.DataSource.Columns("LI_PUKSim").Caption
            Me.Label_LI_NrTelefonicoSim.Text = m_bs.DataSource.Columns("LI_NrTelefonicoSim").Caption
            Me.Label_LI_AutoAggDopoXOre.Text = m_bs.DataSource.Columns("LI_AutoAggDopoXOre").Caption
            Me.Label_LI_Note.Text = m_bs.DataSource.Columns("LI_Note").Caption
            Me.CheckBox_LI_In_Funzione.Text = m_bs.DataSource.Columns("LI_In_Funzione").Caption

            If (Me.TextBox_LIRel.DataBindings.Count = 0) Then
                Me.TextBox_LIRel.DataBindings.Add(New Binding("Text", m_bs, "LI_Rel", True))
            End If
            If (Me.CheckBox_LI_LogModbus.DataBindings.Count = 0) Then
                Me.CheckBox_LI_LogModbus.DataBindings.Add(New Binding("Checked", m_bs, "LI_LogModbus", True))
            End If
            If (Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Count = 0) Then
                Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Add(New Binding("Text", m_bs, "LI_PotenzaGestitaKw", True))
            End If
            If (Me.TextBox_LI_Kd.DataBindings.Count = 0) Then
                Me.TextBox_LI_Kd.DataBindings.Add(New Binding("Text", m_bs, "LI_Kd", True))
            End If
            If (Me.TextBox_Soglia_Calcolo_HG.DataBindings.Count = 0) Then
                Me.TextBox_Soglia_Calcolo_HG.DataBindings.Add(New Binding("Text", m_bs, "LI_SogliaCalcoloHG", True))
            End If
            If (Me.TextBox_LI_EuroKwh.DataBindings.Count = 0) Then
                Me.TextBox_LI_EuroKwh.DataBindings.Add(New Binding("Text", m_bs, "LI_EuroKwh", True))
            End If
            If (Me.TextBox_LI_Nr.DataBindings.Count = 0) Then
                Me.TextBox_LI_Nr.DataBindings.Add(New Binding("Text", m_bs, "LI_Nr", True))
            End If
            If (Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Count = 0) Then
                Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Add(New Binding("Text", m_bs, "LI_TCPIP_Set_Ind", True))
            End If
            If (Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Count = 0) Then
                Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Add(New Binding("Text", m_bs, "LI_TCPIP_Set_Port", True))
            End If
            If (Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Count = 0) Then
                Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Add(New Binding("Text", m_bs, "LI_TCPIP_Web_Port", True))
            End If
            If (Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Count = 0) Then
                Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Add(New Binding("Checked", m_bs, "LI_Auto_Get_TCPIP_Ind_Port", True))
            End If
            If (Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Count = 0) Then
                Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Add(New Binding("Text", m_bs, "LI_TCPIP_Get_Ind", True))
            End If
            If (Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Count = 0) Then
                Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Add(New Binding("Text", m_bs, "LI_TCPIP_Get_Port", True))
            End If
            If (Me.TextBox_LI_OperatoreSim.DataBindings.Count = 0) Then
                Me.TextBox_LI_OperatoreSim.DataBindings.Add(New Binding("Text", m_bs, "LI_OperatoreSim", True))
            End If
            If (Me.TextBox_LI_NrSerialeSim.DataBindings.Count = 0) Then
                Me.TextBox_LI_NrSerialeSim.DataBindings.Add(New Binding("Text", m_bs, "LI_NrSerialeSim", True))
            End If
            If (Me.TextBox_LI_PINSim.DataBindings.Count = 0) Then
                Me.TextBox_LI_PINSim.DataBindings.Add(New Binding("Text", m_bs, "LI_PINSim", True))
            End If
            If (Me.TextBox_LI_PUKSim.DataBindings.Count = 0) Then
                Me.TextBox_LI_PUKSim.DataBindings.Add(New Binding("Text", m_bs, "LI_PUKSim", True))
            End If
            If (Me.TextBox_LI_NrTelefonicoSim.DataBindings.Count = 0) Then
                Me.TextBox_LI_NrTelefonicoSim.DataBindings.Add(New Binding("Text", m_bs, "LI_NrTelefonicoSim", True))
            End If
            If (Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Count = 0) Then
                Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Add(New Binding("Text", m_bs, "LI_AutoAggDopoXOre", True))
            End If
            If (Me.TextBox_LI_Note.DataBindings.Count = 0) Then
                Me.TextBox_LI_Note.DataBindings.Add(New Binding("Text", m_bs, "LI_Note", True))
            End If
            If (Me.CheckBox_LI_In_Funzione.DataBindings.Count = 0) Then
                Me.CheckBox_LI_In_Funzione.DataBindings.Add(New Binding("Checked", m_bs, "LI_In_Funzione", True))
            End If

        End If

        Me.ComboBox_C_ID.Enabled = True
        Me.ComboBox_IDP_ID.Enabled = True

        Me.ComboBox_L_ID.Enabled = False
        Me.CheckBox_LI_LogModbus.Enabled = False
        Me.TextBox_LIRel.ReadOnly = True
        Me.TextBox_LI_PotenzaGestitaKw.ReadOnly = True
        Me.TextBox_LI_Kd.ReadOnly = True
        Me.TextBox_Soglia_Calcolo_HG.ReadOnly = True
        Me.TextBox_LI_EuroKwh.ReadOnly = True
        Me.TextBox_LI_Nr.ReadOnly = True
        Me.TextBox_LI_TCPIP_Set_Ind.ReadOnly = True
        Me.TextBox_LI_TCPIP_Set_Port.ReadOnly = True
        Me.TextBox_LI_TCPIP_Web_Port.ReadOnly = True
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Enabled = False
        Me.TextBox_LI_TCPIP_Get_Ind.ReadOnly = True
        Me.TextBox_LI_TCPIP_Get_Port.ReadOnly = True
        Me.TextBox_LI_OperatoreSim.ReadOnly = True
        Me.TextBox_LI_NrSerialeSim.ReadOnly = True
        Me.TextBox_LI_PINSim.ReadOnly = True
        Me.TextBox_LI_PUKSim.ReadOnly = True
        Me.TextBox_LI_NrTelefonicoSim.ReadOnly = True
        Me.TextBox_LI_AutoAggDopoXOre.ReadOnly = True
        Me.TextBox_LI_Note.ReadOnly = True
        Me.CheckBox_LI_In_Funzione.Enabled = False

        If m_iCID > 0 Then
            Me.ComboBox_C_ID.SelectedValue = m_iCID
        End If
        If m_iIDPID > 0 Then
            Me.ComboBox_IDP_ID.SelectedValue = m_iIDPID
        End If

        Me.ButtonConfig_LI.Enabled = True
        Me.Button_LI_AutoAggInCorso.Enabled = True

    End Sub

    Private Sub RimuoviBinding()

        If (Me.CheckBox_LI_LogModbus.DataBindings.Count > 0) Then
            Me.CheckBox_LI_LogModbus.DataBindings.Clear()
        End If
        If (Me.TextBox_LIRel.DataBindings.Count > 0) Then
            Me.TextBox_LIRel.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Count > 0) Then
            Me.TextBox_LI_PotenzaGestitaKw.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Kd.DataBindings.Count > 0) Then
            Me.TextBox_LI_Kd.DataBindings.Clear()
        End If
        If (Me.TextBox_Soglia_Calcolo_HG.DataBindings.Count > 0) Then
            Me.TextBox_Soglia_Calcolo_HG.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_EuroKwh.DataBindings.Count > 0) Then
            Me.TextBox_LI_EuroKwh.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Nr.DataBindings.Count > 0) Then
            Me.TextBox_LI_Nr.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Set_Ind.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Set_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Web_Port.DataBindings.Clear()
        End If
        If (Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Count > 0) Then
            Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Get_Ind.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Count > 0) Then
            Me.TextBox_LI_TCPIP_Get_Port.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_OperatoreSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_OperatoreSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_NrSerialeSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_NrSerialeSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PINSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_PINSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_PUKSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_PUKSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_NrTelefonicoSim.DataBindings.Count > 0) Then
            Me.TextBox_LI_NrTelefonicoSim.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Count > 0) Then
            Me.TextBox_LI_AutoAggDopoXOre.DataBindings.Clear()
        End If
        If (Me.TextBox_LI_Note.DataBindings.Count > 0) Then
            Me.TextBox_LI_Note.DataBindings.Clear()
        End If
        If (Me.CheckBox_LI_In_Funzione.DataBindings.Count > 0) Then
            Me.CheckBox_LI_In_Funzione.DataBindings.Clear()
        End If

        Me.ComboBox_C_ID.Enabled = False
        Me.ComboBox_IDP_ID.Enabled = False

        Me.ComboBox_L_ID.Enabled = True
        Me.CheckBox_LI_LogModbus.Enabled = True
        Me.TextBox_LIRel.ReadOnly = False
        Me.TextBox_LI_PotenzaGestitaKw.ReadOnly = False
        Me.TextBox_LI_Kd.ReadOnly = False
        Me.TextBox_Soglia_Calcolo_HG.ReadOnly = False
        Me.TextBox_LI_EuroKwh.ReadOnly = False
        Me.TextBox_LI_Nr.ReadOnly = False
        Me.TextBox_LI_TCPIP_Set_Ind.ReadOnly = False
        Me.TextBox_LI_TCPIP_Set_Port.ReadOnly = False
        Me.TextBox_LI_TCPIP_Web_Port.ReadOnly = False
        Me.CheckBox_LI_Auto_Get_TCPIP_Ind_Port.Enabled = True
        Me.TextBox_LI_TCPIP_Get_Ind.ReadOnly = False
        Me.TextBox_LI_TCPIP_Get_Port.ReadOnly = False
        Me.TextBox_LI_OperatoreSim.ReadOnly = False
        Me.TextBox_LI_NrSerialeSim.ReadOnly = False
        Me.TextBox_LI_PINSim.ReadOnly = False
        Me.TextBox_LI_PUKSim.ReadOnly = False
        Me.TextBox_LI_NrTelefonicoSim.ReadOnly = False
        Me.TextBox_LI_AutoAggDopoXOre.ReadOnly = False
        Me.TextBox_LI_Note.ReadOnly = False
        Me.CheckBox_LI_In_Funzione.Enabled = True

        Me.ButtonConfig_LI.Enabled = False
        Me.Button_LI_AutoAggInCorso.Enabled = False
    End Sub

    Private Sub ButtonConfig_LI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConfig_LI.Click
        Try
            If DataGridView.SelectedRows.Count > 0 Then
                If DataGridView.SelectedRows(0).Index >= 0 Then
                    If Not m_bs.DataSource Is Nothing Then
                        If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                            If Not DataGridView.SelectedRows(0).Cells("C_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("IDP_ID").Value Is DBNull.Value And Not DataGridView.SelectedRows(0).Cells("LI_ID").Value Is DBNull.Value Then
                                If DataGridView.SelectedRows(0).Cells("LI_ID").Value > 0 Then

                                    LoggerInstParamConfig.Close()
                                    LoggerInstParamConfig.UID = m_iUID
                                    LoggerInstParamConfig.LIID = DataGridView.SelectedRows(0).Cells("LI_ID").Value
                                    LoggerInstParamConfig.MdiParent = Me.MdiParent
                                    LoggerInstParamConfig.Show()

                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

    End Sub

    Private Sub Button_LI_AutoAggInCorso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_LI_AutoAggInCorso.Click
        If Not m_bs.DataSource Is Nothing Then
            If Not m_bs.Current() Is Nothing Then
                Dim dr As DataRow

                dr = m_bs.Current().Row()
                SetLIAutoAggInCorso(dr.Item("LI_ID"), False)
                ' Devo aggiornare i dati ....
                ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_MODBUS_TI_AUTO_START_CONNECTION_RESET, RISULTATO_OK, "Operazione eseguita Manualmente", "", "", DEFAULT_OPERATOR_ID, Nothing, False)

            End If
        End If
    End Sub

End Class
