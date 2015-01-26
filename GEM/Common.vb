Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net

Module Common
    Public Const AZIONE_ADD As UShort = 1
    Public Const AZIONE_MOD As UShort = 2
    Public Const AZIONE_DEL As UShort = 3
    Public Const AZIONE_READ As UShort = 4

    Public Const AZIONE_COMPATTA_DATABASE As UShort = 9

    Public Const AZIONE_LOGIN As UShort = 11
    Public Const AZIONE_LOGOUT As UShort = 12

    Public Const AZIONE_CLIENT_TCPIP_CONNECTED As UShort = 21
    Public Const AZIONE_CLIENT_TCPIP_DISCONNECTED As UShort = 22

    Public Const AZIONE_CLIENT_TCPIP_REQ_CONNECTION As UShort = 25

    Public Const AZIONE_SERVER_TCPIP_LISTEN As UShort = 31
    Public Const AZIONE_SERVER_TCPIP_STOP As UShort = 32

    Public Const AZIONE_SERVER_COM_LISTEN As UShort = 41
    Public Const AZIONE_SERVER_COM_STOP As UShort = 42

    Public Const AZIONE_SERVER_INVIO_EMAIL As UShort = 51
    Public Const AZIONE_SERVER_INSERISCI_DATI_STATISTICI As UShort = 61

    Public Const AZIONE_MODBUS_TI_START_CONNECTION As UShort = 101
    Public Const AZIONE_MODBUS_TI_ACCEPT_CONNECTION As UShort = 105
    Public Const AZIONE_MODBUS_TI_CLOSE_CONNECTION As UShort = 111

    Public Const AZIONE_MODBUS_TI_AUTO_START_CONNECTION_SET As UShort = 121
    Public Const AZIONE_MODBUS_TI_AUTO_START_CONNECTION_RESET As UShort = 123

    Public Const AZIONE_MODBUS_READ As UShort = 151

    Public Const AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC As UShort = 201
    Public Const AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC As UShort = 205
    Public Const AZIONE_MODBUS_TI_GET_ID As UShort = 501
    Public Const AZIONE_MODBUS_TI_GET_LAST_UDT As UShort = 503
    Public Const AZIONE_MODBUS_TI_GET_NR_MAX_UDT As UShort = 504
    Public Const AZIONE_MODBUS_TI_GET_CONN_WAY As UShort = 507
    Public Const AZIONE_MODBUS_TI_GET_TOTALS As UShort = 510
    Public Const AZIONE_MODBUS_TI_GET_ALL_STORED_DATA As UShort = 512
    Public Const AZIONE_MODBUS_TI_RESET_CALL_FLAG As UShort = 515

    Public Const AZIONE_MODBUS_TI_GET_CONFIG_PROC As UShort = 521
    Public Const AZIONE_MODBUS_TI_RESET_CALL_FLAG_GET_CONFIG As UShort = 525
    Public Const AZIONE_MODBUS_TI_GET_CONFIG As UShort = 1000
    Public Const AZIONE_MODBUS_TI_GET_CONFIG_CODE_MIN_VALUE As UShort = 1001
    Public Const AZIONE_MODBUS_TI_GET_CONFIG_CODE_MAX_VALUE As UShort = 1999

    Public Const AZIONE_MODBUS_TI_SET_CONFIG_PROC As UShort = 531
    Public Const AZIONE_MODBUS_TI_RESET_CALL_FLAG_SET_CONFIG As UShort = 535
    Public Const AZIONE_MODBUS_TI_SET_CONFIG As UShort = 2000
    Public Const AZIONE_MODBUS_TI_SET_CONFIG_CODE_MIN_VALUE As UShort = 2001
    Public Const AZIONE_MODBUS_TI_SET_CONFIG_CODE_MAX_VALUE As UShort = 2999

    Public Const AZIONE_GENERICA As UShort = 10000
    Public Const AZIONE_MODBUS_GENERICA As UShort = 11000

    Public Const RISULTATO_OPERAZIONE_IN_CORSO As UShort = 1
    Public Const RISULTATO_OK As UShort = 2
    Public Const RISULTATO_ERR As UShort = 3
    Public Const RISULTATO_TIMEOUT As UShort = 4

    Public Const RISULTATO_MODBUS_OPERAZIONE_IN_CORSO As UShort = 8
    Public Const RISULTATO_MODBUS_OK As UShort = 11
    Public Const RISULTATO_MODBUS_ERR As UShort = 12
    Public Const RISULTATO_MODBUS_TIMEOUT As UShort = 13

    Public Const RISULTATO_ERR_CONCORRENZA As UShort = 100

    Public Const RISULTATO_EXCEPT As UShort = 10000
    Public Const RISULTATO_MODBUS_EXCEPT As UShort = 11000

    Public Const MODBUS_HOLDING_REGISTERS_START = 40001
    Public Const MODBUS_HOLDING_REGISTERS_STOP = 49999

    Public Const TIPO_DATO_BSTR = 8
    Public Const TIPO_DATO_UI2 = 18
    Public Const TIPO_DATO_UI4 = 19

    Public Const DEFAULT_OPERATOR_ID As Integer = 1

    Public Sub CustomSQLConnectionOpen(ByRef SC As SqlConnection, ByRef SCMD As SqlCommand)

        ' Apro la connessione
        SC.Open()

        ' Assegno l'oggetto command
        SCMD.Connection = SC
        ' Imposto il Timeout
        SCMD.CommandTimeout = My.Settings.SQLCmdTimeout

    End Sub

    Public Sub ScriviLogEventi(ByVal iLLIID As Integer, ByVal ushLUltimoUDTMem As UShort, ByVal iAID As Integer, ByVal iRID As Integer, ByVal strDescrizione As String, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal iUID As Integer, Optional ByVal owner As System.Windows.Forms.IWin32Window = Nothing, Optional ByVal bShowDlg As Boolean = True)
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim dtDateTime As Date = Date.Now
        Dim strAID As String
        Dim strRID As String
        Dim iUIDTest As Integer

        If String.IsNullOrEmpty(strDescrizione) Then
            strDescrizione = " "
        End If

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT U_ID FROM Utente "
            strSQL = strSQL + " WHERE U_ID = " + iUID.ToString()

            cmd.CommandText = strSQL
            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iUIDTest = ds.Tables(0).Rows(0).Item("U_ID")
                    End If
                End If
            End If


            strSQL = " SELECT A_Nome FROM Azione "
            strSQL = strSQL + " WHERE A_ID = " + iAID.ToString()

            cmd.CommandText = strSQL
            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            strAID = "Tipo di Azione non trovata"
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strAID = ds.Tables(0).Rows(0).Item("A_Nome")
                    End If
                End If
            End If


            strSQL = " SELECT R_Nome FROM Risultato "
            strSQL = strSQL + " WHERE R_ID = " + iRID.ToString()

            cmd.CommandText = strSQL
            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            strRID = "Tipo di Risultato non trovato"
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strRID = ds.Tables(0).Rows(0).Item("R_Nome")
                    End If
                End If
            End If


            strSQL = "INSERT INTO [Log] "
            strSQL = strSQL + "  (LG_LI_ID,  LG_UltimoUDTMem,  LG_A_ID,  LG_R_ID,  LG_Descrizione,  LG_LocalHostName,  LG_LocalAddress,  LG_RemoteAddress,  LG_DataOra,  LG_U_ID) VALUES "
            strSQL = strSQL + "  (@LG_LI_ID, @LG_UltimoUDTMem, @LG_A_ID, @LG_R_ID, @LG_Descrizione, @LG_LocalHostName, @LG_LocalAddress, @LG_RemoteAddress, @LG_DataOra, @LG_U_ID)"

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLLIID)
            cmd.Parameters.AddWithValue("@LG_UltimoUDTMem", CInt(ushLUltimoUDTMem))
            cmd.Parameters.AddWithValue("@LG_A_ID", iAID)
            cmd.Parameters.AddWithValue("@LG_R_ID", iRID)
            cmd.Parameters.AddWithValue("@LG_Descrizione", strDescrizione)
            cmd.Parameters.AddWithValue("@LG_LocalHostName", My.Computer.Name)
            cmd.Parameters.AddWithValue("@LG_LocalAddress", strLocalAddress)
            cmd.Parameters.AddWithValue("@LG_RemoteAddress", strRemoteAddress)
            cmd.Parameters.AddWithValue("@LG_DataOra", Date.Now)
            ' Se l'utente non esiste, inserisco quello di default...
            If iUIDTest = 0 Then
                iUID = DEFAULT_OPERATOR_ID
            End If
            cmd.Parameters.AddWithValue("@LG_U_ID", iUID)

            cmd.ExecuteNonQuery()

            If bShowDlg = True Then
                Dim mbi As Windows.Forms.MessageBoxIcon
                If iRID = RISULTATO_OK Or iRID = RISULTATO_MODBUS_OK Then
                    mbi = MessageBoxIcon.Information
                ElseIf iRID = RISULTATO_ERR Or iRID = RISULTATO_MODBUS_ERR Then
                    mbi = MessageBoxIcon.Error
                ElseIf iRID = RISULTATO_OPERAZIONE_IN_CORSO Or iRID = RISULTATO_MODBUS_OPERAZIONE_IN_CORSO Then
                    mbi = MessageBoxIcon.Warning
                ElseIf iRID = RISULTATO_EXCEPT Or iRID = RISULTATO_MODBUS_EXCEPT Then
                    mbi = MessageBoxIcon.Stop
                Else
                    mbi = MessageBoxIcon.Stop
                End If
                System.Windows.Forms.MessageBox.Show(owner, strAID + vbCrLf + strRID + vbCrLf + strDescrizione + vbCrLf + "Indirizzo:Porta Locale: " + strLocalAddress + vbCrLf + "Indirizzo:Porta Remoto: " + strRemoteAddress, My.Application.Info.ProductName, MessageBoxButtons.OK, mbi)
            End If

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(owner, ex.Message + ex.StackTrace, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    'Public Sub DeleteLogEventi(ByVal iLIID As Integer, ByVal dtSTART As Date)

    '    ' Prima prelevo tutti gli ultimi UDT Memorizzati con successo....
    '    Dim strSQL As String
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter
    '    Dim ds As New DataSet
    '    Dim iLGLIID As Integer
    '    Dim bPrimaRiga As Boolean

    '    Try

    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn

    '        strSQL = " SELECT LoggerInst.LI_ID "
    '        strSQL = strSQL + " FROM LoggerInst "

    '        cmd.CommandText = strSQL
    '        cmd.Parameters.Clear()

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    ' Memorizzo tutti gli ID che NON devo cancellare
    '                    strSQL = "DELETE FROM Log "
    '                    strSQL = strSQL + "WHERE "
    '                    bPrimaRiga = True
    '                    For Each dr As DataRow In ds.Tables(0).Rows
    '                        iLGLIID = GetLastStoredLGIDLogValue(dr.Item("LI_ID"), "", "", AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)
    '                        If iLGLIID <> 0 Then
    '                            If bPrimaRiga = True Then
    '                                bPrimaRiga = False
    '                                strSQL = strSQL + " LG_ID <> " + iLGLIID.ToString()
    '                            Else
    '                                strSQL = strSQL + " AND LG_ID <> " + iLGLIID.ToString()
    '                            End If
    '                        End If
    '                    Next dr
    '                    If bPrimaRiga = True Then
    '                        bPrimaRiga = False
    '                        strSQL = strSQL + " (LG_DataOra <= CONVERT(DATETIME, @LG_DataOra_Start, 105)) "
    '                    Else
    '                        strSQL = strSQL + " AND (LG_DataOra <= CONVERT(DATETIME, @LG_DataOra_Start, 105)) "
    '                    End If

    '                    ' Eseguo l'eliminazione
    '                    cmd.CommandText = strSQL
    '                    cmd.Parameters.Clear()
    '                    cmd.Parameters.AddWithValue("@LG_DataOra_Start", dtSTART)
    '                    cmd.Parameters.AddWithValue("@LG_DataOra_Start", dtSTART)

    '                    If cmd.ExecuteNonQuery() > 0 Then
    '                        ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Automatica dello storico del Log fino al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                    Else
    '                        ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_ERR, "Attenzione, i criteri impostati per la cancellazione del Log non coincidono con nessuna riga. Non e' stato possibile eliminare nessuna riga del Log prima del: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                    End If

    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '    End Try

    '    ds.Dispose()
    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()

    'End Sub

    Public Sub DeleteLogEventi(ByVal iLIID As Integer, ByVal dtSTART As Date)

        ' Prima prelevo tutti gli ultimi UDT Memorizzati con successo....
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim iLGLIID As Integer

        Try
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OPERAZIONE_IN_CORSO, "Eliminazione Dati Log Eventi Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

            ' Memorizzo tutti gli ID che NON devo cancellare
            strSQL = "DELETE FROM Log "
            strSQL = strSQL + "WHERE LG_LI_ID = @LG_LI_ID "
            iLGLIID = GetLastStoredLGIDLogValue(iLIID, "", "", AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)
            If iLGLIID <> 0 Then
                strSQL = strSQL + " AND LG_ID <> " + iLGLIID.ToString()
            End If
            strSQL = strSQL + " AND (LG_DataOra < CONVERT(DATETIME, @LG_DataOra_Start, 105)) "

            ' Eseguo l'eliminazione
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_DataOra_Start", dtSTART)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Dati Log Eventi Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_ERR, "Eliminazione Dati Log Eventi. Nessun Dato da Eliminare Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Function GetRowNrErrorInStackTrace(ByVal strStackTrace As String) As String

        Return Split(strStackTrace, vbCrLf)(UBound(Split(strStackTrace, vbCrLf)))

    End Function

    Public Function GENERICA_DESCRIZIONE(ByVal strColonnaDaCuiPrelevare As String, ByVal strTabella As String, ByVal strColonnaSelezione As String, ByVal strCodiceSelezione As String, ByVal iUID As Integer) As Object
        Dim obj As Object = Nothing

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " SELECT " + strColonnaDaCuiPrelevare + " FROM " + strTabella + " "
            strSQL = strSQL + " WHERE " + strColonnaSelezione + " = @CodSel "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@CodSel", strCodiceSelezione)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        obj = ds.Tables(0).Rows(0).Item(strColonnaDaCuiPrelevare)
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return obj

    End Function

    Public Function GENERICA_CANCELLAZIONE(ByVal strTabella As String, ByVal strColonnaSelezione As String, ByVal strCodiceSelezione As String, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " DELETE FROM " + strTabella + " "
            strSQL = strSQL + " WHERE " + strColonnaSelezione + " = @CodSel "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@CodSel", strCodiceSelezione)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", "", iUID, Nothing, False)
                bRes = True
            Else
                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR, "", "", "", iUID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetMinLevelReq(ByVal strFormName As String) As Integer
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim iFLivello As Integer

        iFLivello = 8
        Try

            strSQL = " SELECT TOP 1 F_Livello FROM [Form] "
            strSQL = strSQL + " WHERE F_Nome = @F_Nome "
            strSQL = strSQL + " ORDER BY F_Livello "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@F_Nome", strFormName)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iFLivello = ds.Tables(0).Rows(0).Item("F_Livello")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iFLivello

    End Function

    Public Function GetUID(ByVal strUserName As String, ByVal strPassword As String) As Integer
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim iUID As Integer

        Try

            strSQL = " SELECT * FROM [Utente] "
            strSQL = strSQL + " WHERE U_UserName = @U_UserName AND U_Password = @U_Password AND U_Disabilitato = 0 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@U_UserName", strUserName)
            cmd.Parameters.AddWithValue("@U_Password", strPassword)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iUID = ds.Tables(0).Rows(0).Item("U_ID")
                    End If
                Else
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iUID

    End Function

    Public Function GetUNomeCognome(ByVal iUID As Integer) As String
        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim strUNomeCognome As String = ""

        Try

            strSQL = " SELECT * FROM [Utente] "
            strSQL = strSQL + " WHERE U_ID = @U_ID AND U_Disabilitato = 0 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@U_ID", iUID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strUNomeCognome = ds.Tables(0).Rows(0).Item("U_NomeCognome")
                    End If
                Else
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strUNomeCognome

    End Function

    'Public Function InizializzaDatabase(ByVal iUID As Integer) As Boolean
    '    Dim strSQL As String
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand

    '    Try
    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn

    '        'strSQL = "DELETE FROM [StringAntiTheftInst_X_PannelloFotovString_X_Valore] "
    '        'cmd.CommandText = strSQL
    '        'cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringAntiTheftInst_X_PannelloFotovString_X_Config] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        'strSQL = "DELETE FROM [InverterTesterInst_X_InverterFotovInst_X_Valore] "
    '        'cmd.CommandText = strSQL
    '        'cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [InverterTesterInst_X_InverterFotovInst_X_Config] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        'strSQL = "DELETE FROM [LoggerInst_X_ImpiantoDiProduzione_X_Valore] "
    '        'cmd.CommandText = strSQL
    '        'cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringTesterInst_X_PannelloFotovString_X_Valore] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringTesterInst_X_PannelloFotovString_X_Config] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringTesterInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [InverterTesterInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringAntiTheftInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [StringTester] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [LoggerInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [Logger] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [PannelloFotovInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [PannelloFotov] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [PannelloFotovString] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [InverterFotovInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [InverterFotov] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [ContatoreDiProduzioneInst] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [ContatoreDiProduzione] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [ImpiantoDiProduzione] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [Cliente] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()
    '        strSQL = "DELETE FROM [Log] "
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()

    '        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "", "", iUID, Nothing)

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message+ " "+ex.InnerException.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
    '    End Try

    'End Function

    Public Sub CompattaDB(ByVal iLIID As Integer)

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        ScriviLogEventi(iLIID, 0, AZIONE_COMPATTA_DATABASE, RISULTATO_OPERAZIONE_IN_CORSO, "Dimensione prima della compattazione: " + DimensioneDatabase(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " DBCC SHRINKDATABASE (@DB)"

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DB", cn.Database)

            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            ScriviLogEventi(iLIID, 0, AZIONE_COMPATTA_DATABASE, RISULTATO_OK, "Dimensione dopo della compattazione: " + DimensioneDatabase(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_COMPATTA_DATABASE, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Function DimensioneDatabase() As String

        Dim strDBSize As String = "-"

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " EXEC Sp_helpdb '" + cn.Database + "' "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strDBSize = ds.Tables(0).Rows(0).Item("db_size")
                    End If
                End If
            End If


        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strDBSize

    End Function

    Public Function SetDGI(ByVal DGIID As Integer, ByVal DGIValore As String, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try

            strSQL = " UPDATE [DatiGeneraliImpianto] "
            strSQL = strSQL + " SET DGI_Valore = @DGI_Valore, DGI_DataOra = @DGI_DataOra, DGI_U_ID = @DGI_U_ID "
            strSQL = strSQL + " WHERE DGI_ID = @DGI_ID " 'AND DGI_CC = @DGI_CC "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DGI_ID", DGIID)
            cmd.Parameters.AddWithValue("@DGI_Valore", DGIValore)
            cmd.Parameters.AddWithValue("@DGI_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@DGI_U_ID", iUID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes
    End Function

    Public Function GetDGI(ByRef DGIID As Integer, ByRef objDGIValore As Object) As Boolean
        Dim bRes As Boolean

        Dim objIn As Object
        Dim stTypeIn As System.Type

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            objIn = objDGIValore
            stTypeIn = objDGIValore.GetType()

            strSQL = " SELECT * FROM [DatiGeneraliImpianto] "
            strSQL = strSQL + " WHERE DGI_ID = @DGI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DGI_ID", DGIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        objDGIValore = ds.Tables(0).Rows(0).Item("DGI_Valore")
                        bRes = True
                    End If
                End If
            End If

            Try
                objDGIValore = Convert.ChangeType(objDGIValore, stTypeIn)
            Catch ex As Exception
                objDGIValore = objIn
            End Try

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function StoreStringTesterValue(ByVal iSTILIID As Integer, ByVal iUDT As Integer, ByVal ushSTIndirizzoModbus As UShort, ByVal iSTIPFSCITID As Integer, ByVal sngSTIPFSVValore As Single, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean
        Dim bRes As Boolean

        Dim iSTIPFSCSTIID As Integer
        Dim iSTIPFSVSTIPFSCID As Integer

        ' In Base all'ID del Data Logger ed all'Indirizzo Modbus della Scheda,
        ' Ricavo L'ID della Scheda String Tester
        ' Ricavo L'ID del Valore di configurazione String Tester Installato/Pannello Fotovoltaico Stringa
        ' Tale valore e' diverso per ogni diverso ingresso a cui la stringa e' collegata allo String Tester
        ' Se tale valore e' 0 significa che l'ingresso non e' utilizzato e non lo memorizzo

        ' Verifico anche che lo stesso dato non sia gia' stato memorizzato
        bRes = False

        iSTIPFSCSTIID = GetSTIID(iSTILIID, ushSTIndirizzoModbus)
        iSTIPFSVSTIPFSCID = GetSTIPFSCID(iSTIPFSCSTIID, iSTIPFSCITID)


        If iSTIPFSCSTIID > 0 Then

            If iSTIPFSVSTIPFSCID > 0 Then

                Dim strSQL As String
                Dim ds As New DataSet
                Dim cn As New SqlConnection(My.Settings.ConnectionString)
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter

                Try
                    strSQL = "INSERT INTO [StringTesterInst_X_PannelloFotovString_X_Valore] "
                    strSQL = strSQL + "  (STIPFSV_STIPFSC_ID,  STIPFSV_Valore,  STIPFSV_UDT,  STIPFSV_DataOra) VALUES "
                    strSQL = strSQL + "  (@STIPFSV_STIPFSC_ID, @STIPFSV_Valore, @STIPFSV_UDT, @STIPFSV_DataOra) "

                    CustomSQLConnectionOpen(cn, cmd)
                    'cmd.Connection = cn
                    cmd.CommandText = strSQL

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@STIPFSV_STIPFSC_ID", iSTIPFSVSTIPFSCID)
                    cmd.Parameters.AddWithValue("@STIPFSV_Valore", Math.Round(sngSTIPFSVValore, 3))
                    cmd.Parameters.AddWithValue("@STIPFSV_UDT", iUDT)
                    cmd.Parameters.AddWithValue("@STIPFSV_DataOra", dt)

                    cmd.ExecuteNonQuery()

                    bRes = True

                Catch ex As Exception

                    ScriviLogEventi(iSTILIID, iUDT, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                End Try

                da.Dispose()
                cmd.Dispose()
                cn.Close()
                cn.Dispose()

            End If

        End If

        Return bRes

    End Function

    Public Function GetSTIID(ByVal iSTILIID As Integer, ByVal iSTushIndirizzoModbus As Integer) As Integer
        Dim iSTIID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT STI_ID FROM [StringTesterInst] "
            strSQL = strSQL + " WHERE STI_LI_ID = @STI_LI_ID AND STI_Indirizzo_Modbus = @STI_Indirizzo_Modbus "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STI_LI_ID", iSTILIID)
            cmd.Parameters.AddWithValue("@STI_Indirizzo_Modbus", iSTushIndirizzoModbus)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iSTIID = ds.Tables(0).Rows(0).Item("STI_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iSTILIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iSTIID

    End Function

    Public Function GetSTIPFSCID(ByVal iSTIPFSCSTIID As Integer, ByVal iSTIPFSCITID As Integer) As Integer
        Dim iSTIPFSCID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT STIPFSC_ID FROM [StringTesterInst_X_PannelloFotovString_X_Config] "
            strSQL = strSQL + " WHERE STIPFSC_STI_ID = @STIPFSC_STI_ID AND STIPFSC_IT_ID = @STIPFSC_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STIPFSC_STI_ID", iSTIPFSCSTIID)
            cmd.Parameters.AddWithValue("@STIPFSC_IT_ID", iSTIPFSCITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iSTIPFSCID = ds.Tables(0).Rows(0).Item("STIPFSC_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iSTIPFSCID

    End Function

    Public Function StoreInverterTesterValue(ByVal iITILIID As Integer, ByVal iUDT As Integer, ByVal iITushIndirizzoModbus As Integer, ByVal iITIIFICITID As Integer, ByVal sngITIIFIVValore As Single, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean
        Dim bRes As Boolean

        Dim iITIIFICITIID As Integer
        Dim iITIIFIVITIIFICID As Integer

        ' In Base all'ID del Data Logger ed all'Indirizzo Modbus della Scheda,
        ' Ricavo L'ID della Scheda Inverter Tester
        ' Ricavo L'ID del Valore di configurazione Inverter Tester Installato/Inverter Fotovoltaico
        ' Tale valore e' diverso per ogni diverso ingresso a cui la stringa e' collegata allo String Tester
        ' Se tale valore e' 0 significa che l'ingresso non e' utilizzato e non lo memorizzo
        ' Verifico anche che lo stesso dato non sia gia' stato memorizzato
        bRes = False

        iITIIFICITIID = GetITIID(iITILIID, iITushIndirizzoModbus)
        If iITIIFICITIID > 0 Then

            iITIIFIVITIIFICID = GetITIIFICID(iITIIFICITIID, iITIIFICITID)
            If iITIIFIVITIIFICID > 0 Then

                Dim strSQL As String
                Dim ds As New DataSet
                Dim cn As New SqlConnection(My.Settings.ConnectionString)
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter

                Try
                    strSQL = "INSERT INTO [InverterTesterInst_X_InverterFotovInst_X_Valore] "
                    strSQL = strSQL + "  (ITIIFIV_ITIIFIC_ID,  ITIIFIV_Valore,  ITIIFIV_UDT,  ITIIFIV_DataOra) VALUES "
                    strSQL = strSQL + "  (@ITIIFIV_ITIIFIC_ID, @ITIIFIV_Valore, @ITIIFIV_UDT, @ITIIFIV_DataOra) "

                    CustomSQLConnectionOpen(cn, cmd)
                    'cmd.Connection = cn
                    cmd.CommandText = strSQL

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@ITIIFIV_ITIIFIC_ID", iITIIFIVITIIFICID)
                    cmd.Parameters.AddWithValue("@ITIIFIV_Valore", Math.Round(sngITIIFIVValore, 3))
                    cmd.Parameters.AddWithValue("@ITIIFIV_UDT", iUDT)
                    cmd.Parameters.AddWithValue("@ITIIFIV_DataOra", dt)

                    cmd.ExecuteNonQuery()

                    bRes = True

                Catch ex As Exception

                    ScriviLogEventi(iITILIID, iUDT, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                End Try

                da.Dispose()
                cmd.Dispose()
                cn.Close()
                cn.Dispose()
                ds.Dispose()
            End If

        End If

        Return bRes

    End Function

    Public Function GetITIID(ByVal iITILIID As Integer, ByVal iITushIndirizzoModbus As Integer) As Integer
        Dim iITIID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT ITI_ID FROM [InverterTesterInst] "
            strSQL = strSQL + " WHERE ITI_LI_ID = @ITI_LI_ID AND ITI_Indirizzo_Modbus = @ITI_Indirizzo_Modbus "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITI_LI_ID", iITILIID)
            cmd.Parameters.AddWithValue("@ITI_Indirizzo_Modbus", iITushIndirizzoModbus)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iITIID = ds.Tables(0).Rows(0).Item("ITI_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iITILIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iITIID

    End Function

    Public Function GetITIIFICID(ByVal iITIIFICITIID As Integer, ByVal iITIIFICITID As Integer) As Integer
        Dim iITIIFICID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT ITIIFIC_ID FROM [InverterTesterInst_X_InverterFotovInst_X_Config] "
            strSQL = strSQL + " WHERE ITIIFIC_ITI_ID = @ITIIFIC_ITI_ID AND ITIIFIC_IT_ID = @ITIIFIC_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITIIFIC_ITI_ID", iITIIFICITIID)
            cmd.Parameters.AddWithValue("@ITIIFIC_IT_ID", iITIIFICITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iITIIFICID = ds.Tables(0).Rows(0).Item("ITIIFIC_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iITIIFICID

    End Function

    Public Function StoreDataLoggerValue(ByVal iLIID As Integer, ByVal iUDT As Integer, ByVal iLushIndirizzoModbus As Integer, ByVal iiLIIDPCITID As Integer, ByVal sngLIIDPVValore As Single, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean
        Dim bRes As Boolean
        Dim iLIIDPCID As Integer

        ' In Base all'ID del Data Logger ed all'Indirizzo Modbus della Scheda,
        ' Ricavo L'ID della Scheda Inverter Tester
        ' Ricavo L'ID del Valore di configurazione Inverter Tester Installato/Inverter Fotovoltaico
        ' Tale valore e' diverso per ogni diverso ingresso a cui la stringa e' collegata allo String Tester
        ' Se tale valore e' 0 significa che l'ingresso non e' utilizzato e non lo memorizzo
        bRes = False
        If iLIID > 0 Then

            iLIIDPCID = GetLIIDPCID(iLIID, iiLIIDPCITID, strLocalAddress, strRemoteAddress)
            If iLIIDPCID > 0 Then

                Dim strSQL As String
                Dim ds As New DataSet
                Dim cn As New SqlConnection(My.Settings.ConnectionString)
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter

                Try
                    strSQL = "INSERT INTO [LoggerInst_X_ImpiantoDiProduzione_X_Valore] "
                    strSQL = strSQL + "  (LIIDPV_LIIDPC_ID,  LIIDPV_Valore,  LIIDPV_UDT,  LIIDPV_DataOra) VALUES "
                    strSQL = strSQL + "  (@LIIDPV_LIIDPC_ID, @LIIDPV_Valore, @LIIDPV_UDT, @LIIDPV_DataOra) "

                    CustomSQLConnectionOpen(cn, cmd)
                    'cmd.Connection = cn
                    cmd.CommandText = strSQL

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@LIIDPV_LIIDPC_ID", iLIIDPCID)
                    cmd.Parameters.AddWithValue("@LIIDPV_Valore", Math.Round(sngLIIDPVValore, 3))
                    cmd.Parameters.AddWithValue("@LIIDPV_UDT", iUDT)
                    cmd.Parameters.AddWithValue("@LIIDPV_DataOra", dt)

                    cmd.ExecuteNonQuery()

                    bRes = True

                Catch ex As Exception

                    ScriviLogEventi(iLIID, iUDT, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                End Try

                da.Dispose()
                cmd.Dispose()
                cn.Close()
                cn.Dispose()
                ds.Dispose()

            End If

        End If

        Return bRes

    End Function

    Public Function GetLIIDPCID(ByVal iLIIDPCLIID As Integer, ByVal iLIIDPCITID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Integer
        Dim iLIIDPCID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LIIDPC_ID FROM [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
            strSQL = strSQL + " WHERE LIIDPC_LI_ID = @LIIDPC_LI_ID AND LIIDPC_IT_ID = @LIIDPC_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", iLIIDPCLIID)
            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", iLIIDPCITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLIIDPCID = ds.Tables(0).Rows(0).Item("LIIDPC_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIIDPCLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iLIIDPCID

    End Function

    'Public Function DEFAULT_OPERATOR_ID As Integer
    '    'prende il primo operatore disponibile
    '    Dim iID As Integer

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter
    '    Try

    '        strSQL = "SELECT U_ID FROM Utente"

    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        da.SelectCommand = cmd
    '        da.Fill(ds)
    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    iID = ds.Tables(0).Rows(0).Item("U_ID")
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message+ " "+ex.InnerException.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
    '    End Try

    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()
    '    ds.Dispose()

    '    Return iID

    'End Function

    Public Function GetLastStoredUDTLogValue(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal ushAzione As UShort) As UShort

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

        Dim bRes As Boolean
        Dim ushLILCUltimaUDTMem As UShort

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(ushAzione))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ushLILCUltimaUDTMem = ds.Tables(0).Rows(0).Item("LG_UltimoUDTMem")

                        bRes = True
                    End If
                End If
            End If
            If bRes = True Then
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_OK, "Ultimo UDT Memorizzato nel Database: " + ushLILCUltimaUDTMem.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "Non e' stato possibile prelevare l'ultimo UDT memorizzato correttamente nel Database.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return ushLILCUltimaUDTMem

    End Function

    Public Function GetLastStoredLGIDLogValue(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal ushAzione As UShort) As Integer

        Dim iLGID As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(ushAzione))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLGID = ds.Tables(0).Rows(0).Item("LG_ID")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iLGID

    End Function

    Public Sub SetLastStoredUDTValue(ByVal iLIID As Integer, ByVal ushLUltimoUDTMem As UShort, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal ushAzione As UShort)

        ScriviLogEventi(iLIID, ushLUltimoUDTMem, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_OK, "ATTENZIONE, questa impostazione e' stata fatta dal ciclo di sincronizzazione. Nessun dato e' stato richiesto dal Datalogger e quindi nessun dato e' stato salvato nel Database con questa UDT.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Function GetLastStoredDateAndTimeLogValue(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Date

        Dim dt As Date

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(AZIONE_MODBUS_TI_GET_ALL_STORED_DATA))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0).Rows(0).Item("LG_DataOra")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return dt

    End Function

    Public Function GetLastStoredDateAndTimeDLValue(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Date

        Dim dt As Date
        Dim iLGLIID As Integer
        Dim iLGUltimoUDTMem As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(AZIONE_MODBUS_TI_GET_ALL_STORED_DATA))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLGLIID = ds.Tables(0).Rows(0).Item("LG_LI_ID")
                        iLGUltimoUDTMem = ds.Tables(0).Rows(0).Item("LG_UltimoUDTMem")
                    End If
                End If
            End If

            If iLGLIID > 0 Then
                strSQL = " Select DISTINCT LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra, LoggerInst.LI_ID "
                strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
                strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
                strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
                strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT = @LIIDPV_UDT) AND (LoggerInst.LI_ID = @LI_ID) "
                strSQL = strSQL + " ORDER BY LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra DESC "

                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIIDPV_UDT", iLGUltimoUDTMem)
                cmd.Parameters.AddWithValue("@LI_ID", iLGLIID)

                da.SelectCommand = cmd
                ds.Clear()
                da.Fill(ds)

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            dt = ds.Tables(0).Rows(0).Item("LIIDPV_DataOra")
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return dt

    End Function

    Public Function GetLastStoredUDTDLValue(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As UShort

        Dim usUDT As UShort

        Dim iLGLIID As Integer
        Dim iLGUltimoUDTMem As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(AZIONE_MODBUS_TI_GET_ALL_STORED_DATA))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLGLIID = ds.Tables(0).Rows(0).Item("LG_LI_ID")
                        iLGUltimoUDTMem = ds.Tables(0).Rows(0).Item("LG_UltimoUDTMem")
                    End If
                End If
            End If

            If iLGLIID > 0 Then
                strSQL = " Select DISTINCT LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra, LoggerInst.LI_ID "
                strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
                strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
                strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
                strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT = @LIIDPV_UDT) AND (LoggerInst.LI_ID = @LI_ID) "
                strSQL = strSQL + " ORDER BY LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra DESC "

                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIIDPV_UDT", iLGUltimoUDTMem)
                cmd.Parameters.AddWithValue("@LI_ID", iLGLIID)

                da.SelectCommand = cmd
                ds.Clear()
                da.Fill(ds)

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            usUDT = ds.Tables(0).Rows(0).Item("LIIDPV_UDT")
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return usUDT

    End Function

    Public Function GetLastStoredITIDDLValue(ByVal iLIID As Integer, ByVal iITID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Double

        Dim dbITIDValue As Double

        Dim iLGLIID As Integer
        Dim iLGUltimoUDTMem As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT TOP 1 * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_A_ID = @LG_A_ID AND LG_R_ID = @LG_R_ID "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LG_A_ID", CInt(AZIONE_MODBUS_TI_GET_ALL_STORED_DATA))
            cmd.Parameters.AddWithValue("@LG_R_ID", CInt(RISULTATO_MODBUS_OK))

            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLGLIID = ds.Tables(0).Rows(0).Item("LG_LI_ID")
                        iLGUltimoUDTMem = ds.Tables(0).Rows(0).Item("LG_UltimoUDTMem")
                    End If
                End If
            End If

            If iLGLIID > 0 Then
                strSQL = " Select DISTINCT LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT, LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra, LoggerInst.LI_ID "
                strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
                strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
                strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
                strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_UDT = @LIIDPV_UDT) AND (LoggerInst.LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = @LIIDPC_IT_ID) "
                strSQL = strSQL + " ORDER BY LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra DESC "

                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIIDPV_UDT", iLGUltimoUDTMem)
                cmd.Parameters.AddWithValue("@LI_ID", iLGLIID)
                cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", iITID)

                da.SelectCommand = cmd
                ds.Clear()
                da.Fill(ds)

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            dbITIDValue = ds.Tables(0).Rows(0).Item("LIIDPV_Valore")
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return Math.Round(dbITIDValue, 0)

    End Function

    Public Sub ModbusReqTotalsDataLogger(ByRef lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim abyteStartingAddress() As Byte
        Dim abyteQuantityofRegisters() As Byte

        ' Compilo la richiesta
        ' 03 (0x03) Read Holding Registers
        ' Function Code
        lbyteBufferSend.Add(CByte(3))
        ' Starting Address (2 Bytes)
        abyteStartingAddress = BitConverter.GetBytes(CUShort(320))
        lbyteBufferSend.Add(abyteStartingAddress(1)) ' Hi
        lbyteBufferSend.Add(abyteStartingAddress(0)) ' Lo
        ' Quantity of Registers
        abyteQuantityofRegisters = BitConverter.GetBytes(CUShort(16))
        lbyteBufferSend.Add(abyteQuantityofRegisters(1)) ' Hi
        lbyteBufferSend.Add(abyteQuantityofRegisters(0)) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Sub ModbusGetTotalsDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim ushFunctionErrorCode As UShort
        Dim ushExceptionCode As UShort
        Dim ushByteCount As UShort
        Dim dblP1 As Double
        Dim dblP2 As Double
        Dim dblP3 As Double
        Dim dblP4 As Double
        Dim ba8(7) As Byte
        Dim sngLIPCKP1 As Single
        Dim sngLIPCKP2 As Single
        Dim sngLIPCKP3 As Single
        Dim sngLIPCKP4 As Single

        Dim ushUDTNumber As UShort
        Dim dtUDTDateAndTime As Date


        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        ushFunctionErrorCode = lbyteBufferReceived(0)
        If ushFunctionErrorCode = 3 Then
            ' Byte Count
            ushByteCount = lbyteBufferReceived(1)
            If ushByteCount = 32 Then
                ' Value
                Array.Copy(lbyteBufferReceived.ToArray(), 2, ba8, 0, 8)
                Array.Reverse(ba8)
                dblP1 = CDbl(BitConverter.ToUInt64(ba8, 0))
                Array.Copy(lbyteBufferReceived.ToArray(), 10, ba8, 0, 8)
                Array.Reverse(ba8)
                dblP2 = CDbl(BitConverter.ToUInt64(ba8, 0))
                Array.Copy(lbyteBufferReceived.ToArray(), 18, ba8, 0, 8)
                Array.Reverse(ba8)
                dblP3 = CDbl(BitConverter.ToUInt64(ba8, 0))
                Array.Copy(lbyteBufferReceived.ToArray(), 26, ba8, 0, 8)
                Array.Reverse(ba8)
                dblP4 = CDbl(BitConverter.ToUInt64(ba8, 0))

                ' Prendo come riferimento il contatore di produzione 1. Ovviamente in caso di 2 contatori di produzione deveno avere entrambi lo stesso K senno'
                ' Il sistema non funziona!!! Lo stesso dicasi con il contatore di consumo!!!
                sngLIPCKP1 = GetLIPCK(iLIID, 134)
                sngLIPCKP2 = GetLIPCK(iLIID, 134)
                sngLIPCKP3 = GetLIPCK(iLIID, 134)
                sngLIPCKP4 = GetLIPCK(iLIID, 134)

                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OK, "Energia Prodotta: " + Math.Round((dblP1 * sngLIPCKP1), 1).ToString() + " Kwh - CO Risparmiato: " + Math.Round((dblP2 * sngLIPCKP2), 1).ToString() + " Kg - Energia Prodotta - Energia Consumata: " + Math.Round((dblP3 * sngLIPCKP3), 1).ToString() + " Kwh - Energia In Produzione: " + Math.Round((dblP4 * sngLIPCKP4), 1).ToString() + " Kwh", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                ' Prelevo l'ultimo UDT Memorizzato nel DataLogger
                ushUDTNumber = GetLastStoredUDTDLValue(iLIID, strLocalAddress, strRemoteAddress)
                dtUDTDateAndTime = GetLastStoredDateAndTimeDLValue(iLIID, strLocalAddress, strRemoteAddress)
                ' Il DL al momento della codifica di questa routine, ha sempre indirizzo 0, sebbene nell'acquisizione dei dati
                ' non vi sia questo limite. Nel caso di cambiamenti a questa regola, la seguente acquisizione potrebbe non funzionare
                If IsDataLoggerDataAlreadyStored(iLIID, 0, 141, dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = False Then
                    If StoreDataLoggerValue(iLIID, ushUDTNumber, 0, 141, Math.Round((dblP1 * sngLIPCKP1), 1), dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = True Then
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OK, "Energia Prodotta Memorizzata", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
                If IsDataLoggerDataAlreadyStored(iLIID, 0, 142, dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = False Then
                    If StoreDataLoggerValue(iLIID, ushUDTNumber, 0, 142, Math.Round((dblP2 * sngLIPCKP1), 1), dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = True Then
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OK, "CO2 Risparmiata Memorizzata", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
                If IsDataLoggerDataAlreadyStored(iLIID, 0, 143, dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = False Then
                    If StoreDataLoggerValue(iLIID, ushUDTNumber, 0, 143, Math.Round((dblP3 * sngLIPCKP1), 1), dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = True Then
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OK, "Energia Prodotta - Energia Consumata Memorizzata", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
                If IsDataLoggerDataAlreadyStored(iLIID, 0, 144, dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = False Then
                    If StoreDataLoggerValue(iLIID, ushUDTNumber, 0, 144, Math.Round((dblP4 * sngLIPCKP1), 1), dtUDTDateAndTime, strLocalAddress, strRemoteAddress) = True Then
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_OK, "Energia In Produzione Memorizzata", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 32 : " + ushByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            End If
        Else
            ' Errore
            If ushFunctionErrorCode = &H83 Then
                ' Codice Eccezione
                ushExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ushFunctionErrorCode.ToString() + " - " + "Codice Errore: " + ushExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_TOTALS, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ushFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            End If
        End If

    End Sub

    Public Sub ModbusReqIDDataLogger(ByRef m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        ' Compilo la richiesta
        ' 03 (0x03) Read Holding Registers
        ' Function Code
        m_lbyteBufferSend.Add(CByte(3))
        ' Starting Address (2 Bytes)
        m_lbyteBufferSend.Add(CByte(0)) ' Hi
        m_lbyteBufferSend.Add(CByte(0)) ' Lo
        ' Quantity of Registers
        m_lbyteBufferSend.Add(CByte(0)) ' Hi
        m_lbyteBufferSend.Add(CByte(2)) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Function ModbusGetIDDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Integer

        Dim ui16FunctionErrorCode As UInt16
        Dim ui16ExceptionCode As UInt16
        Dim ui16ByteCount As UInt16
        Dim strValoreDato As String
        Dim ba4(3) As Byte
        Dim strSep() As Char = (":")
        Dim strRemoteAddressAndPort() As String
        Dim bLIAutoGetTCPIPIndPort As Boolean
        Dim iLIID As Integer

        strValoreDato = "0"

        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        ui16FunctionErrorCode = lbyteBufferReceived(0)
        If ui16FunctionErrorCode = 3 Then
            ' Byte Count
            ui16ByteCount = lbyteBufferReceived(1)
            If ui16ByteCount = 4 Then
                ' Value
                Array.Copy(lbyteBufferReceived.ToArray(), 2, ba4, 0, 4)
                Array.Reverse(ba4)

                ' Questo valore e' un UIint32 ma per memorizzarlo o utilizzarlo su SQL devo trasformarlo in un Integer
                strValoreDato = BitConverter.ToUInt32(ba4, 0).ToString()
                ' Modifica del 01/05/2012
                Try
                    iLIID = CInt(strValoreDato)
                Catch ex As Exception
                    iLIID = 0
                    ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_ERR, "L' ID del Data Logger deve essere compreso fra 1 e 9999999. Valore letto dal Datalogger: " + strValoreDato + ". Verra' impostato come se fosse 0.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End Try

                ' Verifico che l'ID sia maggiore di 0
                If iLIID > 0 Then

                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_OK, GetPlantDescription(iLIID), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    bLIAutoGetTCPIPIndPort = GENERICA_DESCRIZIONE("LI_Auto_Get_TCPIP_Ind_Port", "LoggerInst", "LI_ID", iLIID, DEFAULT_OPERATOR_ID)
                    ' Verifico che per questo Data Logger voglia memorizzare l'IP Automaticamente
                    If bLIAutoGetTCPIPIndPort = True Then

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "Aggiornamento Indirizzo TCP/IP Nei dati del Datalogger.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                        strRemoteAddressAndPort = strRemoteAddress.Split(strSep)
                        ' Ottenuto il valore dell'ID, salvo l'Indirizzo IP nel database
                        If strRemoteAddressAndPort.Count >= 2 Then

                            Dim strSQL As String
                            Dim ds As New DataSet
                            Dim cn As New SqlConnection(My.Settings.ConnectionString)
                            Dim cmd As New SqlCommand
                            Dim da As New SqlDataAdapter

                            Try

                                strSQL = " UPDATE LoggerInst "
                                strSQL = strSQL + " SET LI_TCPIP_Get_Ind = @LI_TCPIP_Get_Ind, LI_TCPIP_Get_Port = @LI_TCPIP_Get_Port, LI_DataOra = @LI_DataOra "
                                strSQL = strSQL + " WHERE LI_ID = @LI_ID "

                                CustomSQLConnectionOpen(cn, cmd)
                                'cmd.Connection = cn
                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@LI_ID", iLIID)
                                cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Ind", strRemoteAddressAndPort(0))
                                cmd.Parameters.AddWithValue("@LI_TCPIP_Get_Port", strRemoteAddressAndPort(1))
                                cmd.Parameters.AddWithValue("@LI_DataOra", Date.Now)

                                cmd.ExecuteNonQuery()

                                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_OK, "Aggiornamento Indirizzo TCP/IP Nei dati del Datalogger.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                            Catch ex As Exception

                                ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

                            End Try

                            da.Dispose()
                            cmd.Dispose()
                            cn.Close()
                            cn.Dispose()
                            ds.Dispose()

                        Else
                            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_ERR, "Formattazione Indirizzo TCP/IP e Porta non conforme a quello previsto, 'TCP/IP:Porta'.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    End If
                Else
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_ERR, "L' ID del Data Logger non puo' essere 0.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 4 : " + ui16ByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            If ui16FunctionErrorCode = &H83 Then
                ' Codice Eccezione
                ui16ExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ui16FunctionErrorCode.ToString() + " - " + "Codice Errore: " + ui16ExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(0, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ui16FunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        Return iLIID

    End Function

    Public Sub ModbusReqActualUDTDataLogger(ByRef m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Dim abyteStartingAddress() As Byte

        ' Compilo la richiesta
        ' 03 (0x03) Read Holding Registers
        ' Function Code
        m_lbyteBufferSend.Add(CByte(3))
        ' Starting Address (2 Bytes)
        abyteStartingAddress = BitConverter.GetBytes(CUShort(3000))
        m_lbyteBufferSend.Add(abyteStartingAddress(1)) ' Hi
        m_lbyteBufferSend.Add(abyteStartingAddress(0)) ' Lo
        ' Quantity of Registers
        m_lbyteBufferSend.Add(CByte(0)) ' Hi
        m_lbyteBufferSend.Add(CByte(1)) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Function ModbusGetActualUDTDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As UShort

        Dim ui16FunctionErrorCode As UInt16
        Dim ui16ExceptionCode As UInt16
        Dim ui16ByteCount As UInt16
        Dim strValoreDato As String
        Dim ba2(1) As Byte

        strValoreDato = "0"

        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        ui16FunctionErrorCode = lbyteBufferReceived(0)
        If ui16FunctionErrorCode = 3 Then
            ' Byte Count
            ui16ByteCount = lbyteBufferReceived(1)
            If ui16ByteCount = 2 Then
                ' Value
                Array.Copy(lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
                Array.Reverse(ba2)
                strValoreDato = BitConverter.ToUInt16(ba2, 0).ToString()

                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_OK, "Ultimo UDT Memorizzato nel Datalogger: " + CUShort(strValoreDato).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 2 : " + ui16ByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            End If
        Else
            ' Errore
            If ui16FunctionErrorCode = &H83 Then
                ' Codice Eccezione
                ui16ExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ui16FunctionErrorCode.ToString() + " - " + "Codice Errore: " + ui16ExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ui16FunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
            End If
        End If

        Return CUShort(strValoreDato)

    End Function

    Public Sub ModbusGetHWTestLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        'Dim ushFunctionErrorCode As UShort
        'Dim ushExceptionCode As UShort
        'Dim ushByteCount As UShort
        'Dim ushValue As UShort
        'Dim ba2(1) As Byte

        '' 03 (0x03) Read Holding Registers
        '' Function Code deve essere 3
        'ushFunctionErrorCode = lbyteBufferReceived(0)
        'If ushFunctionErrorCode = 3 Then
        '    ' Byte Count
        '    ushByteCount = lbyteBufferReceived(1)
        '    If ushByteCount = 2 Then
        '        ' Value
        '        Array.Copy(lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
        '        Array.Reverse(ba2)
        '        ushValue = BitConverter.ToUInt16(ba2, 0)
        '        'finire qui, utilizzare l'ultimo udt richiesto.
        '        'domani procedere come segue: memorizzare una variabile a livello globale
        '        'ElseIf memorizzarla, passandola alla funzione, quando vengono memorizzati i dati dei vari udt,
        '        'iLIID risultato sara' ovviamente una ripetizione della memorizzazione per ogni udt, ma cmq va bene lo stesso.
        '        ' Gli allarmi li aggiungo solo se scatenati
        '        For iIndice_1 As Integer = 0 To 15
        '            If TestBitInWORD16(ushValue, iIndice_1) > 0.0 Then
        '                If IsDataLoggerDataAlreadyStored(iLIID, ushIndirizzo, (161 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
        '                    If StoreDataLoggerValue(iLIID, ushUDTRichiesto, ushIndirizzo, (161 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
        '                        bRecordAcquisito = True
        '                    End If
        '                Else
        '                    bAlmenoUnRecordGiaPresente = True
        '                End If
        '            End If
        '        Next

        '        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_HW_TEST, RISULTATO_MODBUS_OK, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        '    Else
        '        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_HW_TEST, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 2 : " + ushByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
        '    End If
        'Else
        '    ' Errore
        '    If ushFunctionErrorCode = &H83 Then
        '        ' Codice Eccezione
        '        ushExceptionCode = lbyteBufferReceived(1)
        '        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ui16FunctionErrorCode.ToString() + " - " + "Codice Errore: " + ui16ExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
        '    Else
        '        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ui16FunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID)
        '    End If
        'End If

    End Sub

    Public Sub ModbusReqConnWayDataLogger(ByRef m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Dim abyteStartingAddress() As Byte

        ' Compilo la richiesta
        ' 03 (0x03) Read Holding Registers
        ' Function Code
        m_lbyteBufferSend.Add(CByte(3))
        ' Starting Address (2 Bytes)
        abyteStartingAddress = BitConverter.GetBytes(CUShort(411))
        m_lbyteBufferSend.Add(abyteStartingAddress(1)) ' Hi
        m_lbyteBufferSend.Add(abyteStartingAddress(0)) ' Lo
        ' Quantity of Registers
        m_lbyteBufferSend.Add(CByte(0)) ' Hi
        m_lbyteBufferSend.Add(CByte(1)) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Function ModbusGetConnWayDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As UShort

        Dim ushFunctionErrorCode As UShort
        Dim ushExceptionCode As UShort
        Dim ushByteCount As UShort
        Dim ushConnWay As UShort
        Dim ba2(1) As Byte


        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        ushFunctionErrorCode = lbyteBufferReceived(0)
        If ushFunctionErrorCode = 3 Then
            ' Byte Count
            ushByteCount = lbyteBufferReceived(1)
            If ushByteCount = 2 Then
                ' Value
                Array.Copy(lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
                Array.Reverse(ba2)
                ushConnWay = BitConverter.ToUInt16(ba2, 0)
                If ushConnWay = 0 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_OK, "Il Datalogger ha instaurato una connessione per Richiesta dati manuali GPRS.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                ElseIf ushConnWay = 1 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_OK, "Il Datalogger ha instaurato una connessione per Aggiornamento dati.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                ElseIf ushConnWay = 2 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_OK, "Il Datalogger ha instaurato una connessione per Sopraggiunto errore.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                Else
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_OK, "Il Datalogger ha instaurato una connessione per Motivo Sconosciuto. Codice Connessione: " + ushConnWay.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 2 : " + ushByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            If ushFunctionErrorCode = &H83 Then
                ' Codice Eccezione
                ushExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ushFunctionErrorCode.ToString() + " - " + "Codice Errore: " + ushExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONN_WAY, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ushFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        Return ushConnWay

    End Function

    Public Sub ModbusReqNrMaxUDTDataLogger(ByRef m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Dim abyteStartingAddress() As Byte

        ' Compilo la richiesta
        ' 03 (0x03) Read Holding Registers
        ' Function Code
        m_lbyteBufferSend.Add(CByte(3))
        ' Starting Address (2 Bytes)
        abyteStartingAddress = BitConverter.GetBytes(CUShort(125))
        m_lbyteBufferSend.Add(abyteStartingAddress(1)) ' Hi
        m_lbyteBufferSend.Add(abyteStartingAddress(0)) ' Lo
        ' Quantity of Registers
        m_lbyteBufferSend.Add(CByte(0)) ' Hi
        m_lbyteBufferSend.Add(CByte(1)) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_NR_MAX_UDT, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

    End Sub

    Public Function ModbusGetNrMaxUDTDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As UShort

        Dim ushFunctionErrorCode As UShort
        Dim ushExceptionCode As UShort
        Dim ushByteCount As UShort
        Dim ushNrMaxUDT As UShort
        Dim ba2(1) As Byte

        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        ushFunctionErrorCode = lbyteBufferReceived(0)
        If ushFunctionErrorCode = 3 Then
            ' Byte Count
            ushByteCount = lbyteBufferReceived(1)
            If ushByteCount = 2 Then
                ' Value
                Array.Copy(lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
                Array.Reverse(ba2)
                ushNrMaxUDT = BitConverter.ToUInt16(ba2, 0)

                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_NR_MAX_UDT, RISULTATO_MODBUS_OK, "Nr MAX UDT memorizzabili sul Datalogger impostato: " + ushNrMaxUDT.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_NR_MAX_UDT, RISULTATO_MODBUS_ERR, "Numero Byte Ricevuti <> 2 : " + ushByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            If ushFunctionErrorCode = &H83 Then
                ' Codice Eccezione
                ushExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_NR_MAX_UDT, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + ushFunctionErrorCode.ToString() + " - " + "Codice Errore: " + ushExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_NR_MAX_UDT, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ushFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        Return ushNrMaxUDT

    End Function

    Public Function GetConfigValueToRead(ByVal iLIID As Integer, ByVal bAll As Boolean) As DataTable
        Dim dt As DataTable = Nothing

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LPC_ID, LPC_Indirizzo_Registro_H, LPC_Indirizzo_Registro_L, LIPC_LI_ID, LIPC_LPC_VAL_MEMO_DB, LIPC_LPC_VAL_MEMO_DL "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            If bAll = True Then
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID "
            Else
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_Letto = 'False' "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return dt

    End Function

    Public Function GetLIPCK(ByVal iLIID As Integer, ByVal iLPCID As Integer) As Single
        Dim sngLIPCK As Single

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        ' Default = 1.0
        sngLIPCK = 1.0

        Try

            strSQL = " SELECT LIPC_K "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        sngLIPCK = ds.Tables(0).Rows(0).Item("LIPC_K")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return sngLIPCK

    End Function

    Public Function GetLIPCLPCVALMEMODB(ByVal iLIID As Integer, ByVal iLPCID As Integer) As Single
        Dim sngLIPCLPCVALMEMODB As Single

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LIPC_LPC_VAL_MEMO_DB "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        sngLIPCLPCVALMEMODB = ds.Tables(0).Rows(0).Item("LIPC_LPC_VAL_MEMO_DB")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return sngLIPCLPCVALMEMODB

    End Function

    Public Function GetLIPCLPCDESCRMEMODB(ByVal iLIID As Integer, ByVal iLPCID As Integer) As String
        Dim strLIPCLPCDESCRMEMODB As String = ""

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LIPC_Descrizione "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strLIPCLPCDESCRMEMODB = ds.Tables(0).Rows(0).Item("LIPC_Descrizione")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strLIPCLPCDESCRMEMODB

    End Function

    Public Function GetConfigValueToWrite(ByVal iLIID As Integer, ByVal bAll As Boolean) As DataTable
        Dim dt As DataTable = Nothing

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LPC_ID, LPC_Indirizzo_Registro_H, LPC_Indirizzo_Registro_L, LIPC_LI_ID, LIPC_LPC_VAL_MEMO_DB, LIPC_LPC_VAL_MEMO_DL "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            If bAll = True Then
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LPC_ReadOnly = 'False' "
            Else
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LPC_ReadOnly = 'False' AND LIPC_Inviato = 'False' "
            End If

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return dt

    End Function

    Public Sub ModbusGetConfigValue(ByVal m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal iLIPCLPCID As Integer, ByVal usIndirizzoRegistroH As UShort, ByVal usIndirizzoRegistroL As UShort, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim abyteStartingAddress() As Byte
        Dim usQuantityOfRegisters As UShort
        Dim abyteQuantityOfRegisters() As Byte

        If Not m_lbyteBufferSend Is Nothing Then
            If usIndirizzoRegistroL >= MODBUS_HOLDING_REGISTERS_START And usIndirizzoRegistroL <= MODBUS_HOLDING_REGISTERS_STOP And usIndirizzoRegistroH >= MODBUS_HOLDING_REGISTERS_START And usIndirizzoRegistroH <= MODBUS_HOLDING_REGISTERS_STOP Then
                ' Controllo quanti registri devo leggere
                usQuantityOfRegisters = (usIndirizzoRegistroH - usIndirizzoRegistroL) + 1
                If usQuantityOfRegisters >= 0 Then
                    ' 03 (0x03) Read Holding Registers
                    ' Function Code
                    m_lbyteBufferSend.Add(CByte(3))
                    ' Starting Address (2 Bytes)
                    abyteStartingAddress = BitConverter.GetBytes(CUShort(usIndirizzoRegistroL - MODBUS_HOLDING_REGISTERS_START))
                    m_lbyteBufferSend.Add(CByte(abyteStartingAddress(1))) ' Hi
                    m_lbyteBufferSend.Add(CByte(abyteStartingAddress(0))) ' Lo
                    ' Quantity of Registers
                    abyteQuantityOfRegisters = BitConverter.GetBytes(CUShort(usQuantityOfRegisters))
                    m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(1))) ' Hi
                    m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(0))) ' Lo

                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                Else
                    ' Errore
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Gli Indirizzi dei Registri Modbus sono incongruenti. Registro H: " + usIndirizzoRegistroH.ToString() + " - Registro L: " + usIndirizzoRegistroL.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ' Errore
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Gli Indirizzi dei Registri Modbus sono errati. Registro H: " + usIndirizzoRegistroH.ToString() + " - Registro L: " + usIndirizzoRegistroL.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

    End Sub

    Public Sub ModbusStoreGetConfigValue(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal iLIPCLPCID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim bRes As Boolean
        Dim iFunctionErrorCode As Integer
        Dim iExceptionCode As Integer
        Dim iByteCount As Integer
        Dim iTipoDato As Integer
        Dim strValoreDato As String
        Dim ba2(1) As Byte
        Dim ba4(3) As Byte
        Dim bastr() As Byte

        strValoreDato = ""

        ' 03 (0x03) Read Holding Registers
        ' Function Code deve essere 3
        iFunctionErrorCode = lbyteBufferReceived(0)
        If iFunctionErrorCode = 3 Then
            ' Tipo dato
            iTipoDato = GENERICA_DESCRIZIONE("LPC_Tipo", "LoggerParamConfig", "LPC_ID", iLIPCLPCID, DEFAULT_OPERATOR_ID)
            Select Case iTipoDato
                Case TIPO_DATO_UI4
                    ' Byte Count
                    iByteCount = lbyteBufferReceived(1)
                    If iByteCount = 4 Then
                        ' Value
                        Array.Copy(lbyteBufferReceived.ToArray(), 2, ba4, 0, 4)
                        Array.Reverse(ba4)
                        strValoreDato = BitConverter.ToUInt32(ba4, 0).ToString()

                        bRes = True

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_OK, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    Else
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Numero Byte Ricevuti <> 4 : " + iByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                Case TIPO_DATO_UI2
                    ' Byte Count
                    iByteCount = lbyteBufferReceived(1)
                    If iByteCount = 2 Then
                        Array.Copy(lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
                        Array.Reverse(ba2)
                        strValoreDato = BitConverter.ToUInt16(ba2, 0).ToString()

                        bRes = True

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_OK, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    Else
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Numero Byte Ricevuti <> 2 : " + iByteCount.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                Case TIPO_DATO_BSTR
                    ' Byte Count
                    iByteCount = lbyteBufferReceived(1)
                    bastr = Array.CreateInstance(GetType(Byte), iByteCount)
                    Array.Copy(lbyteBufferReceived.ToArray(), 2, bastr, 0, iByteCount)
                    For Each by As Byte In bastr
                        If by >= 32 And by <= 126 Then
                            strValoreDato = strValoreDato + Chr(by)
                        End If
                    Next

                    bRes = True

                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_OK, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                Case Else
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Tipo Dato non riconosciuto : " + iTipoDato.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

            End Select

        Else
            ' Errore
            If iFunctionErrorCode = &H83 Then
                ' Codice Eccezione
                iExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_EXCEPT, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Numero Errore: " + iFunctionErrorCode.ToString() + " - " + "Codice Errore: " + iExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Codice Funzione: " + iFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        If bRes = True Then
            ' Memorizzo il dato letto 
            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter

            Try

                strSQL = " UPDATE LoggerInstParamConfig "
                strSQL = strSQL + " SET LIPC_LPC_VAL_MEMO_DL = @LIPC_LPC_VAL_MEMO_DL, LIPC_Letto = @LIPC_Letto, LIPC_DataOra_Lettura = @LIPC_DataOra_Lettura "
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID "

                CustomSQLConnectionOpen(cn, cmd)
                'cmd.Connection = cn
                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
                cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLIPCLPCID)
                cmd.Parameters.AddWithValue("@LIPC_LPC_VAL_MEMO_DL", strValoreDato)
                cmd.Parameters.AddWithValue("@LIPC_Letto", True)
                cmd.Parameters.AddWithValue("@LIPC_DataOra_Lettura", Date.Now)

                cmd.ExecuteNonQuery()

            Catch ex As Exception
                ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()
            ds.Dispose()
        End If

    End Sub

    Public Sub ModbusSetConfigValue(ByVal m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal iLIPCLPCID As Integer, ByVal strValoreDato As String, ByVal usIndirizzoRegistroH As UShort, ByVal usIndirizzoRegistroL As UShort, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim abyteStartingAddress() As Byte
        Dim usQuantityOfRegisters As UShort
        Dim abyteQuantityOfRegisters() As Byte
        Dim iTipoDato As Integer
        Dim ba2(1) As Byte
        Dim ba4(3) As Byte
        Dim bastr() As Byte

        If usIndirizzoRegistroL >= MODBUS_HOLDING_REGISTERS_START And usIndirizzoRegistroL <= MODBUS_HOLDING_REGISTERS_STOP And usIndirizzoRegistroH >= MODBUS_HOLDING_REGISTERS_START And usIndirizzoRegistroH <= MODBUS_HOLDING_REGISTERS_STOP Then
            ' Controllo quanti registri devo leggere
            usQuantityOfRegisters = (usIndirizzoRegistroH - usIndirizzoRegistroL) + 1
            If usQuantityOfRegisters >= 0 Then
                ' 16 (0x10) Write Multiple registers
                ' Function Code
                m_lbyteBufferSend.Add(CByte(16))
                ' Starting Address (2 Bytes)
                abyteStartingAddress = BitConverter.GetBytes(CUShort(usIndirizzoRegistroL - MODBUS_HOLDING_REGISTERS_START))
                m_lbyteBufferSend.Add(CByte(abyteStartingAddress(1))) ' Hi
                m_lbyteBufferSend.Add(CByte(abyteStartingAddress(0))) ' Lo
                ' Quantity of Registers
                abyteQuantityOfRegisters = BitConverter.GetBytes(CUShort(usQuantityOfRegisters))
                m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(1))) ' Hi
                m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(0))) ' Lo
                ' Byte Count
                m_lbyteBufferSend.Add(CByte(usQuantityOfRegisters * 2))
                ' Registers Value
                iTipoDato = GENERICA_DESCRIZIONE("LPC_Tipo", "LoggerParamConfig", "LPC_ID", iLIPCLPCID, DEFAULT_OPERATOR_ID)
                Select Case iTipoDato
                    Case TIPO_DATO_UI4
                        ba4 = BitConverter.GetBytes(CUInt(strValoreDato))
                        m_lbyteBufferSend.Add(CByte(ba4(3))) ' Hi
                        m_lbyteBufferSend.Add(CByte(ba4(2))) ' Lo
                        m_lbyteBufferSend.Add(CByte(ba4(1))) ' Hi
                        m_lbyteBufferSend.Add(CByte(ba4(0))) ' Lo

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    Case TIPO_DATO_UI2
                        ba2 = BitConverter.GetBytes(CUShort(strValoreDato))
                        m_lbyteBufferSend.Add(CByte(ba2(1))) ' Hi
                        m_lbyteBufferSend.Add(CByte(ba2(0))) ' Lo

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    Case TIPO_DATO_BSTR
                        ' Byte Count
                        bastr = Array.CreateInstance(GetType(Byte), usQuantityOfRegisters * 2)
                        For iIndice As Integer = 0 To (usQuantityOfRegisters * 2 - 1)
                            If iIndice < strValoreDato.Count Then
                                bastr(iIndice) = Asc(strValoreDato.Chars(iIndice))
                            End If
                        Next

                        For iIndice As Integer = 0 To (usQuantityOfRegisters - 1)
                            m_lbyteBufferSend.Add(bastr((iIndice * 2) + 0)) ' Hi
                            m_lbyteBufferSend.Add(bastr((iIndice * 2) + 1)) ' Lo
                        Next

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    Case Else
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Tipo Dato non riconosciuto : " + iTipoDato.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                End Select

            Else
                ' Errore
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Gli Indirizzi dei Registri Modbus sono incongruenti. Registro H: " + usIndirizzoRegistroH.ToString() + " - Registro L: " + usIndirizzoRegistroL.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Gli Indirizzi dei Registri Modbus sono errati. Registro H: " + usIndirizzoRegistroH.ToString() + " - Registro L: " + usIndirizzoRegistroL.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End If

    End Sub

    Public Sub ModbusStoreSetConfigValue(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal iLIPCLPCID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim bRes As Boolean
        Dim ui16FunctionErrorCode As UInt16
        Dim ui16ExceptionCode As UInt16
        Dim ui16StartingAddress As UInt16
        Dim ui16ByteCount As UInt16
        Dim iTipoDato As Integer
        Dim ui16IndirizzoRegistroL As UInt16
        Dim ui16IndirizzoRegistroH As UInt16
        Dim ui16QuantityOfRegisters As UInt16
        Dim ba2(1) As Byte
        Dim ba4(3) As Byte

        ' Tipo dato
        iTipoDato = GENERICA_DESCRIZIONE("LPC_Tipo", "LoggerParamConfig", "LPC_ID", iLIPCLPCID, DEFAULT_OPERATOR_ID)
        ui16IndirizzoRegistroL = GENERICA_DESCRIZIONE("LPC_Indirizzo_Registro_L", "LoggerParamConfig", "LPC_ID", iLIPCLPCID, DEFAULT_OPERATOR_ID)
        ui16IndirizzoRegistroH = GENERICA_DESCRIZIONE("LPC_Indirizzo_Registro_H", "LoggerParamConfig", "LPC_ID", iLIPCLPCID, DEFAULT_OPERATOR_ID)
        ' Controllo quanti registri devo aver scritto
        ui16QuantityOfRegisters = (ui16IndirizzoRegistroH - ui16IndirizzoRegistroL) + 1

        ui16FunctionErrorCode = lbyteBufferReceived(0)
        If ui16FunctionErrorCode = 16 Then
            Array.Copy(lbyteBufferReceived.ToArray(), 1, ba2, 0, 2)
            Array.Reverse(ba2)
            ui16StartingAddress = BitConverter.ToUInt16(ba2, 0).ToString()
            If ui16StartingAddress = (ui16IndirizzoRegistroL - MODBUS_HOLDING_REGISTERS_START) Then
                Array.Copy(lbyteBufferReceived.ToArray(), 3, ba2, 0, 2)
                Array.Reverse(ba2)
                ui16ByteCount = BitConverter.ToUInt16(ba2, 0).ToString()
                If ui16ByteCount = ui16QuantityOfRegisters Then
                    bRes = True

                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_OK, "Codice Parametro: " + iLIPCLPCID.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                Else
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Quantity Of Register: " + ui16ByteCount.ToString() + " Errati.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Starting Address: " + ui16StartingAddress.ToString() + " Errato.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            If ui16FunctionErrorCode = &H90 Then
                ' Codice Eccezione
                ui16ExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_EXCEPT, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Function Code: " + ui16FunctionErrorCode.ToString() + " - " + "Codice Errore: " + ui16ExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG, RISULTATO_MODBUS_ERR, "Codice Parametro: " + iLIPCLPCID.ToString() + " - Codice Funzione: " + ui16FunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

        If bRes = True Then
            ' Memorizzo il dato letto 
            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter

            Try

                strSQL = " UPDATE LoggerInstParamConfig "
                strSQL = strSQL + " SET LIPC_Inviato = @LIPC_Inviato, LIPC_DataOra_Invio = @LIPC_DataOra_Invio "
                strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LIPC_LPC_ID = @LIPC_LPC_ID "

                CustomSQLConnectionOpen(cn, cmd)
                'cmd.Connection = cn
                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
                cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLIPCLPCID)
                cmd.Parameters.AddWithValue("@LIPC_Inviato", True)
                cmd.Parameters.AddWithValue("@LIPC_DataOra_Invio", Date.Now)

                cmd.ExecuteNonQuery()

            Catch ex As Exception
                ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()
            ds.Dispose()
        End If

    End Sub

    Public Sub ModbusReqStoredDataDataLogger(ByRef m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal ushUDT As UShort, ByVal ushRecordStart As UShort, ByVal ushRecordNr As UShort, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal shAzione As Short)
        Dim abyteUDT() As Byte
        Dim abyteCardOffset() As Byte
        Dim abyteNrRegister() As Byte
        Dim ushRecordLength As UShort

        If Not m_lbyteBufferSend Is Nothing Then
            ' Compilo la richiesta
            ' 20 (0x14) Read File Record
            ' Function Code
            m_lbyteBufferSend.Add(CByte(20))
            ' Byte Count
            m_lbyteBufferSend.Add(CByte(14))
            ' Data ed ora
            ' Sub-Req 1. x, Reference Type
            m_lbyteBufferSend.Add(CByte(6))
            ' Sub-Req 1. x, File Number
            abyteUDT = BitConverter.GetBytes(ushUDT)
            m_lbyteBufferSend.Add(CByte(abyteUDT(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteUDT(0))) ' Lo
            ' Sub-Req 1. x, Record Number (Start Reading Address)
            abyteCardOffset = BitConverter.GetBytes(CUShort(0))
            m_lbyteBufferSend.Add(CByte(abyteCardOffset(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteCardOffset(0))) ' Lo
            ' Sub-Req 1. x, Record Length.
            abyteNrRegister = BitConverter.GetBytes(CUShort(16))
            m_lbyteBufferSend.Add(CByte(abyteNrRegister(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteNrRegister(0))) ' Lo 
            ' Dati
            ' Sub-Req 2. x, Reference Type
            m_lbyteBufferSend.Add(CByte(6))
            ' Sub-Req 2. x, File Number
            abyteUDT = BitConverter.GetBytes(ushUDT)
            m_lbyteBufferSend.Add(CByte(abyteUDT(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteUDT(0))) ' Lo
            ' Sub-Req 2. x, Record Number (Start Reading Address)
            abyteCardOffset = BitConverter.GetBytes(ushRecordStart)
            m_lbyteBufferSend.Add(CByte(abyteCardOffset(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteCardOffset(0))) ' Lo
            ' Sub-Req 2. x, Record Length. 16(Registri sono un'intera scheda) * 2(byte ogni registro) * 6(Numero di schede che richiediamo ad ogni richiesta) = 224 byte
            ushRecordLength = (ushRecordNr * 16)
            abyteNrRegister = BitConverter.GetBytes(CShort(ushRecordLength))
            m_lbyteBufferSend.Add(CByte(abyteNrRegister(1))) ' Hi
            m_lbyteBufferSend.Add(CByte(abyteNrRegister(0))) ' Lo 

            ScriviLogEventi(iLIID, ushUDT, shAzione, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End If
    End Sub

    Public Sub ModbusGetStoredDataDataLogger(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal ushUDTRichiesto As UShort, ByRef bIncrementaUDT As Boolean, ByRef bIncrementaRecord As Boolean, ByRef bRecordAcquisito As Boolean, ByRef bAlmenoUnRecordGiaPresente As Boolean, ByVal strLocalAddress As String, ByVal strRemoteAddress As String, ByVal shAzione As Short)

        Dim iIndice_1 As Integer
        Dim iIndice_2 As Integer
        Dim iIndice_3 As Integer
        Dim iIndice_4 As Integer

        Dim iFunctionErrorCode As Integer
        Dim iExceptionCode As Integer

        Dim iFRL As Integer
        Dim iIndexData As Integer
        Dim iNrOfCardDataReq As Integer

        Dim ushUDTDiRifNr As UShort
        Dim ushUDTDiRifAnno As UShort
        Dim ushUDTDiRifMese As UShort
        Dim ushUDTDiRifGiorno As UShort
        Dim ushUDTDiRifOre As UShort
        Dim ushUDTDiRifMinuti As UShort
        Dim ushUDTDiRifSecondi As UShort

        Dim ushIndirizzo As UShort
        Dim ushTipoScheda As UShort
        Dim ushUDTRicevuto As UShort
        Dim ush_DI As UShort
        Dim sngITK As Single
        Dim sngLIPCK As Single
        Dim ui_A(7) As UInt16
        Dim sng_1 As Single

        Dim ush_RA1 As UShort
        Dim ush_RA2 As UShort

        Dim ush_T1 As UShort

        Dim bFineRecords As Boolean

        Dim bUDTDateOk As Boolean

        Dim ba2(1) As Byte
        Dim ba4(3) As Byte

        bUDTDateOk = False

        bIncrementaUDT = False
        bIncrementaRecord = False

        If Not lbyteBufferReceived Is Nothing Then
            ' Fisso l'indice
            iIndexData = 0
            ' 20 (0x14) Read File Record
            ' Function Code deve essere 20
            iFunctionErrorCode = lbyteBufferReceived(iIndexData)
            If iFunctionErrorCode = 20 Then
                iIndexData = iIndexData + 1

                ' Resp. Data length deve essere 228
                If lbyteBufferReceived(iIndexData) = 228 Then
                    iIndexData = iIndexData + 1

                    ' Sub-Req. 1, File resp. length deve essere 33
                    If lbyteBufferReceived(iIndexData) = 33 Then
                        iIndexData = iIndexData + 1
                        ' Sub-Req. 1, Ref. Type deve essere 6
                        If lbyteBufferReceived(iIndexData) = 6 Then
                            iIndexData = iIndexData + 1

                            ' Disp 1
                            iIndexData = iIndexData + 2

                            ' Disp 2
                            iIndexData = iIndexData + 2

                            ' Ok, prelevo UDT
                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifNr = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            ' Disp 3
                            iIndexData = iIndexData + 2

                            ' Ok, prelevo data ed ora
                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifAnno = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifMese = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifGiorno = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifOre = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifMinuti = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                            Array.Reverse(ba2)
                            ushUDTDiRifSecondi = BitConverter.ToUInt16(ba2, 0)
                            iIndexData = iIndexData + 2

                            ' Disp 4
                            iIndexData = iIndexData + 2

                            ' Disp 5
                            iIndexData = iIndexData + 2

                            ' Disp 6
                            iIndexData = iIndexData + 2

                            ' Disp 7
                            iIndexData = iIndexData + 2

                            ' Disp 8
                            iIndexData = iIndexData + 2

                            ' Disp 9
                            iIndexData = iIndexData + 2

                            ' Uso come riferimento l'anno per stabilire se l'SD card, nella posizione di UDT corrente
                            ' non sia mai stata scritta, nel qual caso esco.
                            If ushUDTDiRifAnno = 0 Then
                                ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto non e' ancora stato inizializzato.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                ' Termino tutte le operazioni
                                bIncrementaUDT = False
                                bIncrementaRecord = False
                                Exit Sub
                            End If

                            If ushUDTRichiesto <> ushUDTDiRifNr Then
                                ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Data e Ora: " + ushUDTDiRifNr.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                ' Termino tutte le operazioni
                                bIncrementaUDT = False
                                bIncrementaRecord = False
                                Exit Sub
                            End If
                        Else
                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Dati Ricevuti Non Corretti. Sub-Req. 1, Ref. Type <> 6: " + lbyteBufferReceived(3).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    Else
                        ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Dati Ricevuti Non Corretti. Sub-Req. 1, File resp. length <> 33: " + lbyteBufferReceived(2).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                    ' Fisso a OK
                    bUDTDateOk = True

                    Dim dt As DateTime
                    Try
                        dt = New DateTime(ushUDTDiRifAnno, ushUDTDiRifMese, ushUDTDiRifGiorno, ushUDTDiRifOre, ushUDTDiRifMinuti, ushUDTDiRifSecondi)
                        'L'anno della data che ricevo deve essere superiore o uguale al 2010 ed inferiore al 2200(penso che avremo tempo di espandere il range!!!)
                        If (dt.Year < 2010) Or (dt.Year > 2200) Then
                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_ERR, "Data Ora Non Valida." + " Anno: " + ushUDTDiRifAnno.ToString() + " Mese: " + ushUDTDiRifMese.ToString() + " Giorno: " + ushUDTDiRifGiorno.ToString() + " Ore: " + ushUDTDiRifOre.ToString() + " Minuti: " + ushUDTDiRifMinuti.ToString() + " Secondi: " + ushUDTDiRifSecondi.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                            bUDTDateOk = False
                        End If
                    Catch ex As Exception
                        ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_ERR, "Data Ora Non Valida." + " Anno: " + ushUDTDiRifAnno.ToString() + " Mese: " + ushUDTDiRifMese.ToString() + " Giorno: " + ushUDTDiRifGiorno.ToString() + " Ore: " + ushUDTDiRifOre.ToString() + " Minuti: " + ushUDTDiRifMinuti.ToString() + " Secondi: " + ushUDTDiRifSecondi.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                        bUDTDateOk = False
                    End Try

                    If bUDTDateOk = True Then

                        ' Sub-Req. 2, File resp. length deve essere 193
                        If lbyteBufferReceived(iIndexData) = 193 Then
                            iFRL = lbyteBufferReceived(iIndexData)
                            iIndexData = iIndexData + 1
                            ' Sub-Req. 2, Ref. Type deve essere 6
                            If lbyteBufferReceived(iIndexData) = 6 Then
                                iIndexData = iIndexData + 1

                                ' Ok, prelevo i dati
                                ' Calcolo i dati di quante schede ho richiesto
                                iNrOfCardDataReq = (iFRL - 1) \ 32
                                For iIndice_1 = 0 To (iNrOfCardDataReq - 1)

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ushIndirizzo = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ushTipoScheda = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ushUDTRicevuto = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ush_DI = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(0) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(1) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(2) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(3) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(4) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(5) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(6) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ui_A(7) = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ush_RA1 = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ush_RA2 = BitConverter.ToUInt16(ba2, 0)
                                    iIndexData = iIndexData + 2

                                    Array.Copy(lbyteBufferReceived.ToArray(), iIndexData, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    ush_T1 = BitConverter.ToUInt16(lbyteBufferReceived.ToArray, iIndexData)
                                    iIndexData = iIndexData + 2

                                    ' Disp 1
                                    iIndexData = iIndexData + 2

                                    ' Se i dati sono coerenti, li memorizzo.
                                    ' In base al tipo di scheda, memorizzo i dati propriamente
                                    ' Data Loggerr
                                    If ushTipoScheda = 1 Then
                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' DL
                                        For iIndice_2 = 0 To 7
                                            If IsDataLoggerDataAlreadyStored(iLIID, ushIndirizzo, (151 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (151 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                If (151 + iIndice_2) = 151 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 133)
                                                ElseIf (151 + iIndice_2) = 152 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 134)
                                                ElseIf (151 + iIndice_2) = 153 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 135)
                                                ElseIf (151 + iIndice_2) = 155 Then
                                                    ' Mi riferisco sempre al solarimetro 1, perche' 
                                                    ' se correggo il solarimetro 1 devo correggere 
                                                    ' in egual misura anche gli altri
                                                    sngLIPCK = (GetLIPCK(iLIID, 52) * GetLIPCK(iLIID, 53))
                                                ElseIf (151 + iIndice_2) = 156 Then
                                                    sngLIPCK = (GetLIPCK(iLIID, 43) * GetLIPCK(iLIID, 44))
                                                ElseIf (151 + iIndice_2) = 157 Then
                                                    sngLIPCK = (GetLIPCK(iLIID, 31) * GetLIPCK(iLIID, 48))
                                                Else
                                                    sngLIPCK = 1.0
                                                End If
                                                sng_1 = CSng(ui_A(iIndice_2))
                                                If StoreDataLoggerValue(iLIID, ushUDTRichiesto, ushIndirizzo, (151 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                If IsDataLoggerDataAlreadyStored(iLIID, ushIndirizzo, (161 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (161 + iIndice_2))
                                                    If StoreDataLoggerValue(iLIID, ushUDTRichiesto, ushIndirizzo, (161 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsDataLoggerDataAlreadyStored(iLIID, ushIndirizzo, (181 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (181 + iIndice_2))
                                                    If StoreDataLoggerValue(iLIID, ushUDTRichiesto, ushIndirizzo, (181 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next

                                    End If

                                    ' String Tester
                                    If ushTipoScheda = 11 Then

                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' ST
                                        For iIndice_2 = 0 To 7
                                            If IsStringTesterDataAlreadyStored(iLIID, ushIndirizzo, (201 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (201 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                sngLIPCK = GetLIPCK(iLIID, 31)
                                                sng_1 = CSng(ui_A(iIndice_2))
                                                If StoreStringTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (201 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        iIndice_4 = 0
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                ' Memorizzo lo stesso allarme per tutti gli ingressi
                                                For iIndice_3 = iIndice_4 To 7 + iIndice_4
                                                    If IsStringTesterDataAlreadyStored(iLIID, ushIndirizzo, (211 + iIndice_3), dt, strLocalAddress, strRemoteAddress) = False Then
                                                        StoreDataLoggerAllarme(iLIID, (211 + iIndice_3))
                                                        If StoreStringTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (211 + iIndice_3), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                            bRecordAcquisito = True
                                                        End If
                                                    Else
                                                        bAlmenoUnRecordGiaPresente = True
                                                    End If
                                                Next
                                            End If
                                            iIndice_4 = iIndice_4 + 10
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsStringTesterDataAlreadyStored(iLIID, ushIndirizzo, (381 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (381 + iIndice_2))
                                                    If StoreStringTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (381 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    ' Inverter Tester
                                    If ushTipoScheda = 21 Then

                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' IT
                                        For iIndice_2 = 0 To 7
                                            If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (411 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (411 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                If (411 + iIndice_2) = 411 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 41)
                                                    sng_1 = CSng(ui_A(iIndice_2))
                                                ElseIf (411 + iIndice_2) = 412 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 42)
                                                    sng_1 = CSng(ui_A(iIndice_2))
                                                ElseIf (411 + iIndice_2) = 413 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                    sng_1 = CSng(ui_A(iIndice_2))
                                                ElseIf (411 + iIndice_2) = 414 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 44)
                                                    sng_1 = CSng(ui_A(iIndice_2))
                                                ElseIf (411 + iIndice_2) = 415 Then
                                                    ' Mi riferisco sempre al solarimetro 1, perche' 
                                                    ' se correggo il solarimetro 1 devo correggere 
                                                    ' in egual misura anche gli altri
                                                    sngLIPCK = GetLIPCK(iLIID, 52)
                                                    sng_1 = CSng(ui_A(iIndice_2))
                                                Else
                                                    sngLIPCK = 1.0
                                                    sng_1 = CSng(GetComplementTwo(ui_A(iIndice_2)))
                                                End If
                                                If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (411 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (421 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (421 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (421 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (441 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (441 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (441 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    If ushTipoScheda = 22 Then

                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' IT
                                        For iIndice_2 = 0 To 7
                                            If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (471 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (471 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                If (471 + iIndice_2) = 471 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (471 + iIndice_2) = 472 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (471 + iIndice_2) = 473 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (471 + iIndice_2) = 474 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 44)
                                                ElseIf (471 + iIndice_2) = 475 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 44)
                                                ElseIf (471 + iIndice_2) = 476 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 44)
                                                ElseIf (471 + iIndice_2) = 477 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 48)
                                                Else
                                                    sngLIPCK = 1.0
                                                End If
                                                sng_1 = CSng(ui_A(iIndice_2))
                                                If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (471 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (481 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (481 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (481 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (501 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (501 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (501 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    ' Inverter Tester
                                    If ushTipoScheda = 23 Then

                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' IT
                                        For iIndice_2 = 0 To 7
                                            If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (531 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (531 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                If (531 + iIndice_2) = 531 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (531 + iIndice_2) = 532 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (531 + iIndice_2) = 533 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (531 + iIndice_2) = 534 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (531 + iIndice_2) = 535 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (531 + iIndice_2) = 536 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                Else
                                                    sngLIPCK = 1.0
                                                End If
                                                sng_1 = CSng(ui_A(iIndice_2))
                                                If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (531 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (541 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (541 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (541 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (561 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (561 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (561 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    ' Inverter Tester
                                    If ushTipoScheda = 24 Then

                                        If ushUDTRichiesto <> ushUDTRicevuto Then
                                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "L'UDT Richiesto e' diverso dall'UDT Di Riferimento Dei Dati: " + ushUDTRicevuto.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Termino tutte le operazioni
                                            bIncrementaUDT = False
                                            bIncrementaRecord = False
                                            Exit Sub
                                        End If

                                        ' IT
                                        For iIndice_2 = 0 To 7
                                            If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (591 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                sngITK = GENERICA_DESCRIZIONE("IT_K", "IngressoTipo", "IT_ID", (591 + iIndice_2), DEFAULT_OPERATOR_ID)
                                                If (591 + iIndice_2) = 591 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 592 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 593 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 594 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 595 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 596 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 597 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                ElseIf (591 + iIndice_2) = 598 Then
                                                    sngLIPCK = GetLIPCK(iLIID, 43)
                                                Else
                                                    sngLIPCK = 1.0
                                                End If
                                                sng_1 = CSng(ui_A(iIndice_2))
                                                If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (591 + iIndice_2), ((sng_1 * sngITK) * sngLIPCK), dt, strLocalAddress, strRemoteAddress) = True Then
                                                    bRecordAcquisito = True
                                                End If
                                            Else
                                                bAlmenoUnRecordGiaPresente = True
                                            End If
                                        Next

                                        ' Gli allarmi li aggiungo solo se scatenati
                                        ' RA 1
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA1, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (601 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (601 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (601 + iIndice_2), TestBitInWORD16(ush_RA1, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                        ' RA 2
                                        For iIndice_2 = 0 To 15
                                            If TestBitInWORD16(ush_RA2, iIndice_2) > 0.0 Then
                                                If IsInverterTesterDataAlreadyStored(iLIID, ushIndirizzo, (621 + iIndice_2), dt, strLocalAddress, strRemoteAddress) = False Then
                                                    StoreDataLoggerAllarme(iLIID, (621 + iIndice_2))
                                                    If StoreInverterTesterValue(iLIID, ushUDTRichiesto, ushIndirizzo, (621 + iIndice_2), TestBitInWORD16(ush_RA2, iIndice_2), dt, strLocalAddress, strRemoteAddress) = True Then
                                                        bRecordAcquisito = True
                                                    End If
                                                Else
                                                    bAlmenoUnRecordGiaPresente = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    ' Se tipo scheda = 0 significa che da adesso in poi non ci sono piu' dati da raccogliere,
                                    ' In questa unita' di tempo, cmq continuo perche'
                                    ' puo' succedere che sia stato lasciato un record vuoto
                                    ' di riserva.
                                    If ushTipoScheda = 0 Then
                                        bFineRecords = True
                                        Exit For
                                    End If
                                Next
                                If bFineRecords = True Then
                                    If bRecordAcquisito = True Then
                                        ' Almeno un'operazione ha avuto successo e sono arrivata a prendere tutti i recoed, vado all'UDT successiva
                                        bIncrementaUDT = True
                                        bIncrementaRecord = False

                                        ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_OK, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    Else
                                        ' Nessuna operazione ha avuto successo.
                                        bIncrementaUDT = False
                                        bIncrementaRecord = False

                                        ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Nessun Record acquisito per l'UDT corrente.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    End If
                                Else
                                    ' Non sono presumibilmente a fine record, quindi ho altri records da prelevare,
                                    ' percio', cmq sia il risultato, li prelevo tutti fino in fondo
                                    bIncrementaUDT = False
                                    bIncrementaRecord = True
                                End If
                            Else
                                ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Dati Ricevuti Non Corretti. Sub-Req. 2, Ref. Type <> 6: " + lbyteBufferReceived(19).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                            End If

                        Else
                            ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Dati Ricevuti Non Corretti. Resp. Data length. length <> 193: " + lbyteBufferReceived(36).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                        End If

                    Else
                        ' Vado all'UDT successiva
                        bIncrementaUDT = True
                        bIncrementaRecord = False
                    End If

                Else
                    ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Dati Ricevuti Non Corretti. Resp. Data length. length <> 228: " + lbyteBufferReceived(1).ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If

            Else
                ' Errore
                If iFunctionErrorCode = &H95 Then
                    ' Codice Eccezione
                    iExceptionCode = lbyteBufferReceived(1)
                    ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_EXCEPT, "Numero Errore: " + iFunctionErrorCode.ToString() + " - " + "Codice Errore: " + iExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                Else
                    ScriviLogEventi(iLIID, ushUDTRichiesto, shAzione, RISULTATO_MODBUS_ERR, "Codice Funzione: " + iFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If

                ' Termino le operazioni
                bIncrementaUDT = False
                bIncrementaRecord = False
            End If
        End If

    End Sub

    Public Sub ModbusSetResetFlagValue(ByVal m_lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim abyteStartingAddress() As Byte
        Dim abyteQuantityOfRegisters() As Byte
        Dim ba2(1) As Byte

        ' 16 (0x10) Write Multiple registers
        ' Function Code
        m_lbyteBufferSend.Add(CByte(16))
        ' Starting Address (2 Bytes)
        ' 40410, 40411
        abyteStartingAddress = BitConverter.GetBytes(CUShort(409))
        m_lbyteBufferSend.Add(CByte(abyteStartingAddress(1))) ' Hi
        m_lbyteBufferSend.Add(CByte(abyteStartingAddress(0))) ' Lo
        ' Quantity of Registers
        abyteQuantityOfRegisters = BitConverter.GetBytes(CUShort(2))
        m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(1))) ' Hi
        m_lbyteBufferSend.Add(CByte(abyteQuantityOfRegisters(0))) ' Lo
        ' Byte Count
        m_lbyteBufferSend.Add(CByte(4))
        ' Registers Value
        ba2 = BitConverter.GetBytes(CUShort(0))
        m_lbyteBufferSend.Add(CByte(ba2(1))) ' Hi
        m_lbyteBufferSend.Add(CByte(ba2(0))) ' Lo
        ba2 = BitConverter.GetBytes(CUShort(0))
        m_lbyteBufferSend.Add(CByte(ba2(1))) ' Hi
        m_lbyteBufferSend.Add(CByte(ba2(0))) ' Lo

        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)


    End Sub

    Public Sub ModbusCheckSetResetFlagValue(ByVal lbyteBufferReceived As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim ushFunctionErrorCode As UShort
        Dim ushExceptionCode As UShort
        Dim ushStartingAddress As UShort
        Dim ushQuantityOfRegisters As UShort
        Dim ba2(1) As Byte

        ushFunctionErrorCode = lbyteBufferReceived(0)
        If ushFunctionErrorCode = 16 Then
            Array.Copy(lbyteBufferReceived.ToArray(), 1, ba2, 0, 2)
            Array.Reverse(ba2)
            ushStartingAddress = BitConverter.ToUInt16(ba2, 0).ToString()
            If ushStartingAddress = 409 Then
                Array.Copy(lbyteBufferReceived.ToArray(), 3, ba2, 0, 2)
                Array.Reverse(ba2)
                ushQuantityOfRegisters = BitConverter.ToUInt16(ba2, 0).ToString()
                If ushQuantityOfRegisters = 2 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_OK, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                Else
                    ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_ERR, "Quantity of registers <> 1: " + ushQuantityOfRegisters.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_ERR, "Starting Address <> 410: " + ushStartingAddress.ToString() + " Errato.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        Else
            ' Errore
            If ushFunctionErrorCode = &H90 Then
                ' Codice Eccezione
                ushExceptionCode = lbyteBufferReceived(1)
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_EXCEPT, "Function Code: " + ushFunctionErrorCode.ToString() + " - " + "Codice Errore: " + ushExceptionCode.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_RESET_CALL_FLAG, RISULTATO_MODBUS_ERR, "Codice Funzione: " + ushFunctionErrorCode.ToString() + " Non riconosciuto.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End If
        End If

    End Sub

    Public Sub WriteLogModbusData(ByRef lbyteBufferSend As List(Of Byte), ByVal iLIID As Integer, ByVal strDirectory As String, ByVal strFileName As String, ByVal strPrefix As String, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Dim str_1 As String
        Dim di As System.IO.DirectoryInfo
        Try
            str_1 = strPrefix + " - ID: " + iLIID.ToString() + " - "
            For Each byte_1 As Byte In lbyteBufferSend
                str_1 = str_1 + byte_1.ToString() + " "
            Next
            str_1 = str_1 + " - " + Date.Now.ToString() + "," + Date.Now.Millisecond.ToString() + " ms" + vbCrLf
            di = My.Computer.FileSystem.GetDirectoryInfo(strDirectory)
            If di.Exists = False Then
                My.Computer.FileSystem.CreateDirectory(strDirectory)
            End If
            My.Computer.FileSystem.WriteAllText(strDirectory + "\" + strFileName + ".txt", str_1, True)
        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try
    End Sub

    Public Sub DeleteLogModbusData(ByVal iLIID As Integer, ByVal strDirectory As String, ByVal strFileName As String, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Try
            Dim bRes As Boolean
            bRes = My.Computer.FileSystem.FileExists(strDirectory + "\" + strFileName + ".txt")
            If bRes = True Then
                My.Computer.FileSystem.DeleteFile(strDirectory + "\" + strFileName + ".txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try
    End Sub

    Public Sub RenameLogModbusData(ByVal iLIID As Integer, ByVal strDirectory As String, ByVal strFileNameOld As String, ByVal strFileNameNew As String, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Try
            Dim bRes As Boolean
            ' Se esiste, lo cancello
            bRes = My.Computer.FileSystem.FileExists(strDirectory + "\" + strFileNameNew + ".txt")
            If bRes = True Then
                My.Computer.FileSystem.DeleteFile(strDirectory + "\" + strFileNameNew + ".txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
            ' Quindi rinomino
            bRes = My.Computer.FileSystem.FileExists(strDirectory + "\" + strFileNameOld + ".txt")
            If bRes = True Then
                My.Computer.FileSystem.RenameFile(strDirectory + "\" + strFileNameOld + ".txt", strFileNameNew + ".txt")
            End If
        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try
    End Sub

    Public Sub CopyLogModbusData(ByVal iLIID As Integer, ByVal strDirectory As String, ByVal strFileName As String, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)
        Try
            Dim bRes As Boolean
            bRes = My.Computer.FileSystem.FileExists(strDirectory + "\" + strFileName + ".txt")
            If bRes = True Then
                My.Computer.FileSystem.CopyFile(strDirectory + "\" + strFileName + ".txt", strDirectory + "\" + strFileName + "_" + Date.Now.Year.ToString() + "_" + Date.Now.Month.ToString() + "_" + Date.Now.Day.ToString() + "_" + Date.Now.Hour.ToString() + "_" + Date.Now.Minute.ToString() + "_" + Date.Now.Second.ToString() + ".txt", True)
                My.Computer.FileSystem.DeleteFile(strDirectory + "\" + strFileName + ".txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try
    End Sub

    Public Function TestBitInWORD16(ByVal ush As UShort, ByVal iPos As Integer) As Single
        Dim ushMaskValue As UShort
        Dim ushRes As UShort
        If iPos >= 0 Then
            ushMaskValue = 2 ^ iPos
            ushRes = (ush And ushMaskValue)
            If ushRes = ushMaskValue Then
                ushRes = 1
            Else
                ushRes = 0
            End If
        End If
        Return CSng(ushRes)
    End Function

    Public Function IsDataLoggerDataAlreadyStored(ByVal iLIIDPCLIID As Integer, ByVal iLushIndirizzoModbus As Integer, ByVal iLIIDPCITID As Integer, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean

        Dim bRes As Boolean

        Dim strSQL As String

        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Try

            strSQL = " SELECT LIIDPV_DataOra FROM [LoggerInst_X_ImpiantoDiProduzione_X_Valore] "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = IngressoTipo.IT_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = @LIIDPC_LI_ID) AND (LoggerInst.LI_Indirizzo_Modbus = @LI_Indirizzo_Modbus) AND (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = @LIIDPC_IT_ID) AND (LIIDPV_DataOra = @LIIDPV_DataOra) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", iLIIDPCLIID)
            cmd.Parameters.AddWithValue("@LI_Indirizzo_Modbus", iLushIndirizzoModbus)
            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", iLIIDPCITID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra", dt)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIIDPCLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsStringTesterDataAlreadyStored(ByVal iSTILIID As Integer, ByVal iSTushIndirizzoModbus As Integer, ByVal iSTIPFSCITID As Integer, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean

        Dim bRes As Boolean

        Dim strSQL As String

        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Try

            strSQL = " SELECT STIPFSV_DataOra FROM [StringTesterInst_X_PannelloFotovString_X_Valore] "
            strSQL = strSQL + " INNER JOIN StringTesterInst_X_PannelloFotovString_X_Config ON StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_STIPFSC_ID = StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_ID "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_STI_ID = StringTesterInst.STI_ID "
            strSQL = strSQL + "INNER JOIN  IngressoTipo ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID = IngressoTipo.IT_ID "
            strSQL = strSQL + " WHERE (StringTesterInst.STI_LI_ID = @STI_LI_ID) AND (StringTesterInst.STI_Indirizzo_Modbus = @STI_Indirizzo_Modbus) AND (StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID = @STIPFSC_IT_ID) AND (STIPFSV_DataOra = @STIPFSV_DataOra) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STI_LI_ID", iSTILIID)
            cmd.Parameters.AddWithValue("@STI_Indirizzo_Modbus", iSTushIndirizzoModbus)
            cmd.Parameters.AddWithValue("@STIPFSC_IT_ID", iSTIPFSCITID)
            cmd.Parameters.AddWithValue("@STIPFSV_DataOra", dt)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iSTILIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsStringTesterConfigured(ByVal iLIID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim iValue As Integer

        Try

            strSQL = " SELECT COUNT(*) AS VALUE "
            strSQL = strSQL + " FROM StringTesterInst INNER JOIN LoggerInst ON StringTesterInst.STI_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE  (LI_ID = @LI_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iValue = ds.Tables(0).Rows(0).Item("VALUE")
                        If iValue > 0 Then
                            bRes = True
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsInverterTesterDataAlreadyStored(ByVal iITILIID As Integer, ByVal iITushIndirizzoModbus As Integer, ByVal iITIIFICITID As Integer, ByVal dt As Date, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean

        Dim bRes As Boolean

        Dim strSQL As String

        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Try

            strSQL = " SELECT ITIIFIV_DataOra FROM [InverterTesterInst_X_InverterFotovInst_X_Valore] "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @ITI_LI_ID) AND (InverterTesterInst.ITI_Indirizzo_Modbus = @ITI_Indirizzo_Modbus) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = @ITIIFIC_IT_ID) AND (ITIIFIV_DataOra = @ITIIFIV_DataOra) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITI_LI_ID", iITILIID)
            cmd.Parameters.AddWithValue("@ITI_Indirizzo_Modbus", iITushIndirizzoModbus)
            cmd.Parameters.AddWithValue("@ITIIFIC_IT_ID", iITIIFICITID)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra", dt)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iITILIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Sub DeleteLoggerInstData(ByVal iLIID As Integer, ByVal dtSTART As Date)

        Dim strSQL As String

        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OPERAZIONE_IN_CORSO, "Eliminazione Dati Logger Installati Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

            strSQL = " DELETE FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
            strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID  "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE (LoggerInst.LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 151 OR LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 155 OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID >= 141 AND LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID <= 144) OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID >= 161 AND LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID <= 186)) "
            strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Dati Logger Installati Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_ERR, "Eliminazione Dati Logger Installati. Nessun Dato da Eliminare Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Function IsInverterTesterConfigured(ByVal iLIID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim iValue As Integer

        Try

            strSQL = " SELECT COUNT(*) AS VALUE "
            strSQL = strSQL + " FROM InverterTesterInst INNER JOIN LoggerInst ON InverterTesterInst.ITI_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE  (LI_ID = @LI_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iValue = ds.Tables(0).Rows(0).Item("VALUE")
                        If iValue > 0 Then
                            bRes = True
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Sub DeleteInverterTesterData(ByVal iLIID As Integer, ByVal dtSTART As Date)

        Dim strSQL As String

        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OPERAZIONE_IN_CORSO, "Eliminazione Dati Inverter Tester Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

            strSQL = " DELETE FROM InverterTesterInst_X_InverterFotovInst_X_Valore "
            strSQL = strSQL + " FROM InverterTesterInst_X_InverterFotovInst_X_Valore "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @ITI_LI_ID) AND ((InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID >= 411 AND InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID <= 414) OR (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID >= 471 AND InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID <= 478) OR (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID >= 531 AND InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID <= 538) OR (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID >= 591 AND InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID <= 598)) "
            strSQL = strSQL + " AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra < CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITI_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Dati Inverter Tester Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_ERR, "Eliminazione Dati Inverter Tester. Nessun Dato da Eliminare Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Sub DeleteStringTesterData(ByVal iLIID As Integer, ByVal dtSTART As Date)

        Dim strSQL As String

        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OPERAZIONE_IN_CORSO, "Eliminazione Dati String Tester Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

            strSQL = " DELETE FROM StringTesterInst_X_PannelloFotovString_X_Valore "
            strSQL = strSQL + " FROM StringTesterInst_X_PannelloFotovString_X_Valore "
            strSQL = strSQL + " INNER JOIN StringTesterInst_X_PannelloFotovString_X_Config ON StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_STIPFSC_ID = StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_ID "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_STI_ID = StringTesterInst.STI_ID "
            strSQL = strSQL + " WHERE (StringTesterInst.STI_LI_ID = @STI_LI_ID) AND ((StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID >= 201 AND StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID <= 208)) "
            strSQL = strSQL + " AND StringTesterInst_X_PannelloFotovString_X_Valore.STIPFSV_DataOra < CONVERT(DATETIME, @STIPFSV_DataOra_Start, 105) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STI_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@STIPFSV_DataOra_Start", dtSTART)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Dati String Tester Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_ERR, "Eliminazione Dati String Tester. Nessun Dato da Eliminare Fino Al: " + dtSTART.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Function GetDefaultDataLoggerParameterValue(ByVal iPCID As Integer, ByVal iUID As Integer) As String
        Dim strPCDefaultValue As String = ""

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerParamConfig "
            strSQL = strSQL + " WHERE LPC_ID = @LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LPC_ID", iPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strPCDefaultValue = ds.Tables(0).Rows(0).Item("LPC_Default_Value")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strPCDefaultValue

    End Function

    Public Function IsParameterDataLoggerValueConfigured(ByVal iLIID As Integer, ByVal iPCID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID AND LPC_ID = @LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LPC_ID", iPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddParameterDataLoggerDefaultValue(ByVal iLIID As Integer, ByVal iLPCID As Integer, ByVal strLPCVAL As String, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [LoggerInstParamConfig] "
            strSQL = strSQL + "  (LIPC_LI_ID,  LIPC_LPC_ID,  LIPC_LPC_VAL_MEMO_DB,  LIPC_Inviato,  LIPC_DataOra,  LIPC_U_ID  ) VALUES "
            strSQL = strSQL + "  (@LIPC_LI_ID, @LIPC_LPC_ID, @LIPC_LPC_VAL_MEMO_DB, @LIPC_Inviato, @LIPC_DataOra, @LIPC_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIPC_LPC_ID", iLPCID)
            cmd.Parameters.AddWithValue("@LIPC_LPC_VAL_MEMO_DB", strLPCVAL)
            cmd.Parameters.AddWithValue("@LIPC_Inviato", False)
            cmd.Parameters.AddWithValue("@LIPC_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@LIPC_U_ID", iUID)

            If cmd.ExecuteNonQuery() > 0 Then
                bRes = True
                ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", iUID, Nothing, False)
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", iUID, Nothing)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function AddAllParameterDataLoggerDefaultValue(ByVal iLIID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        bRes = True
        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerParamConfig "
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If IsParameterDataLoggerValueConfigured(iLIID, dr("LPC_ID"), iUID) = False Then
                                If AddParameterDataLoggerDefaultValue(iLIID, dr("LPC_ID"), dr("LPC_Default_Value"), iUID) = False Then
                                    bRes = False
                                End If
                            End If
                        Next dr
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsParameterDataLoggerValueInRange(ByVal iLPCID As Integer, ByVal strVAL As String, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim iTipoDato As Integer

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerParamConfig "
            strSQL = strSQL + " WHERE (LPC_ID = " + iLPCID.ToString() + " )"
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ' Tipo dato
                        iTipoDato = GENERICA_DESCRIZIONE("LPC_Tipo", "LoggerParamConfig", "LPC_ID", iLPCID, DEFAULT_OPERATOR_ID)
                        If iTipoDato = TIPO_DATO_UI2 Then
                            ' Dati numerici
                            If CUShort(strVAL) >= CUShort(ds.Tables(0).Rows(0).Item("LPC_MIN_Value")) And CUShort(strVAL) <= CUShort(ds.Tables(0).Rows(0).Item("LPC_MAX_Value")) Then
                                bRes = True
                            End If
                        End If
                        If iTipoDato = TIPO_DATO_UI4 Then
                            ' Dati numerici
                            If CUInt(strVAL) >= CUInt(ds.Tables(0).Rows(0).Item("LPC_MIN_Value")) And CUInt(strVAL) <= CUInt(ds.Tables(0).Rows(0).Item("LPC_MAX_Value")) Then
                                bRes = True
                            End If
                        End If
                        If iTipoDato = TIPO_DATO_BSTR Then
                            ' Dati stringa, controllo il nr di caratteri
                            If strVAL.Count >= CUShort(ds.Tables(0).Rows(0).Item("LPC_MIN_Value")) And strVAL.Count <= CUShort(ds.Tables(0).Rows(0).Item("LPC_MAX_Value")) Then
                                bRes = True
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsParameterDataLoggerAllConfigured(ByVal iLIID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        bRes = True
        Try
            strSQL = " SELECT LPC_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO "
            strSQL = strSQL + " FROM LoggerParamConfig "
            strSQL = strSQL + " EXCEPT "
            strSQL = strSQL + " SELECT LPC_ID, LPC_Nome + '   Min: ' + CAST(LPC_Min_Value AS varchar(8)) + ' - Max: ' + CAST(LPC_Max_Value AS varchar(8)) as LPC_TIPO "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInstParamConfig.LIPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst.LI_ID = " + iLIID.ToString + ") "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = False
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes
    End Function

    Public Function AddAllInputStringTester(ByVal iNrInput As Integer, ByVal iSTIID As Integer, ByVal iPFSID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        bRes = True

        If IsInputStringTesterConfigured(iSTIID, (200 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (200 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If
        If IsInputStringTesterConfigured(iSTIID, (210 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (210 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If
        If IsInputStringTesterConfigured(iSTIID, (220 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (220 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If
        If IsInputStringTesterConfigured(iSTIID, (360 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (360 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If
        If IsInputStringTesterConfigured(iSTIID, (380 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (380 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If
        If IsInputStringTesterConfigured(iSTIID, (388 + iNrInput), iUID) = False Then
            If AddInputStringTester(iSTIID, (388 + iNrInput), iPFSID, iUID) = False Then
                bRes = False
            End If
        End If

        Return bRes

    End Function

    Public Function IsInputStringTesterConfigured(ByVal iSTIID As Integer, ByVal iITID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM StringTesterInst_X_PannelloFotovString_X_Config "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_STI_ID = StringTesterInst.STI_ID "
            strSQL = strSQL + " INNER JOIN IngressoTipo ON StringTesterInst_X_PannelloFotovString_X_Config.STIPFSC_IT_ID = IngressoTipo.IT_ID "
            strSQL = strSQL + " WHERE STI_ID = @STI_ID AND IT_ID = @IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STI_ID", iSTIID)
            cmd.Parameters.AddWithValue("@IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddInputStringTester(ByVal iSTIPFSCSTIID As Integer, ByVal iSTIPFSCITID As Integer, ByVal iSTIPFSCPFSID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [StringTesterInst_X_PannelloFotovString_X_Config] "
            strSQL = strSQL + "  (STIPFSC_STI_ID,  STIPFSC_IT_ID,  STIPFSC_PFS_ID,  STIPFSC_DataOra,  STIPFSC_U_ID  ) VALUES "
            strSQL = strSQL + "  (@STIPFSC_STI_ID, @STIPFSC_IT_ID, @STIPFSC_PFS_ID, @STIPFSC_DataOra, @STIPFSC_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@STIPFSC_STI_ID", iSTIPFSCSTIID)
            cmd.Parameters.AddWithValue("@STIPFSC_IT_ID", iSTIPFSCITID)
            cmd.Parameters.AddWithValue("@STIPFSC_PFS_ID", iSTIPFSCPFSID)
            cmd.Parameters.AddWithValue("@STIPFSC_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@STIPFSC_U_ID", iUID)

            If cmd.ExecuteNonQuery() > 0 Then
                bRes = True
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_OK, "", "", "", iUID, Nothing, False)
            Else
                ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Attenzione, la tabella e' stata ricaricata.", "", "", iUID, Nothing)
            End If


        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function AddAllInputInverterTester(ByVal iINTTipo As Integer, ByVal iITIID As Integer, ByVal iIFIID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        If iINTTipo <> 0 Then

            bRes = True

            Dim strSQL As String
            Dim ds As New DataSet
            Dim cn As New SqlConnection(My.Settings.ConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter

            Try

                strSQL = " SELECT * "
                strSQL = strSQL + " FROM IngressoTipo "
                If iINTTipo = 21 Then
                    strSQL = strSQL + " WHERE IT_ID >= 411 AND IT_ID <= 456 "
                End If
                If iINTTipo = 22 Then
                    strSQL = strSQL + " WHERE IT_ID >= 471 AND IT_ID <= 516 "
                End If
                If iINTTipo = 23 Then
                    strSQL = strSQL + " WHERE IT_ID >= 531 AND IT_ID <= 576 "
                End If
                If iINTTipo = 24 Then
                    strSQL = strSQL + " WHERE IT_ID >= 591 AND IT_ID <= 636 "
                End If

                CustomSQLConnectionOpen(cn, cmd)
                'cmd.Connection = cn
                cmd.CommandText = strSQL

                da.SelectCommand = cmd
                da.Fill(ds)

                If Not ds Is Nothing Then
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            For Each dr As DataRow In ds.Tables(0).Rows
                                If IsInputInverterTesterConfigured(iITIID, dr("IT_ID"), iUID) = False Then
                                    If AddInputInverterTester(iITIID, dr("IT_ID"), iIFIID, iUID) = False Then
                                        bRes = False
                                    End If
                                End If
                            Next dr
                        End If
                    End If
                End If

            Catch ex As Exception
                bRes = False
                ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
            End Try

            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()
            ds.Dispose()

        Else
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_ERR, "Tipo di scheda Inverter Tester non riconosciuto.", "", "", iUID)
        End If

        Return bRes

    End Function

    Public Function IsInputInverterTesterConfigured(ByVal iITIID As Integer, ByVal iITID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM IngressoTipo "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON IngressoTipo.IT_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
            strSQL = strSQL + " WHERE ITI_ID = @ITI_ID AND IT_ID = @IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITI_ID", iITIID)
            cmd.Parameters.AddWithValue("@IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddInputInverterTester(ByVal iITIID As Integer, ByVal iITID As Integer, ByVal iIFIID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [InverterTesterInst_X_InverterFotovInst_X_Config] "
            strSQL = strSQL + "  (ITIIFIC_ITI_ID,  ITIIFIC_IT_ID,  ITIIFIC_IFI_ID,  ITIIFIC_DataOra,  ITIIFIC_U_ID ) VALUES "
            strSQL = strSQL + "  (@ITIIFIC_ITI_ID, @ITIIFIC_IT_ID, @ITIIFIC_IFI_ID, @ITIIFIC_DataOra, @ITIIFIC_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ITIIFIC_ITI_ID", iITIID)
            cmd.Parameters.AddWithValue("@ITIIFIC_IT_ID", iITID)
            cmd.Parameters.AddWithValue("@ITIIFIC_IFI_ID", iIFIID)
            cmd.Parameters.AddWithValue("@ITIIFIC_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@ITIIFIC_U_ID", iUID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function AddAllPannelliFotovoltaici(ByVal iCID As Integer, ByVal iIDPID As Integer, ByVal iCDPIID As Integer, ByVal iIFIID As Integer, ByVal iPFINr As Integer, ByVal iPFID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        bRes = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT PFS_ID "
            strSQL = strSQL + " FROM PannelloFotovString INNER JOIN Utente ON PFS_U_ID = U_ID INNER JOIN InverterFotovInst ON PFS_IFI_ID = IFI_ID INNER JOIN ContatoreDiProduzioneInst ON IFI_CDPI_ID = CDPI_ID INNER JOIN ImpiantoDiProduzione ON CDPI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
            strSQL = strSQL + " WHERE " + " C_ID = " + iCID.ToString + " AND IDP_ID = " + iIDPID.ToString + " AND IFI_ID = " + iIFIID.ToString + " AND CDPI_ID = " + iCDPIID.ToString

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If IsPannelliFotovoltaiciConfigured(dr("PFS_ID"), iUID) = False Then
                                If AddPannelliFotovoltaici(iPFID, dr("PFS_ID"), iPFINr, iUID) = False Then
                                    bRes = False
                                End If
                            End If
                        Next dr
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function DelAllPannelliFotovoltaici(ByVal iCID As Integer, ByVal iIDPID As Integer, ByVal iCDPIID As Integer, ByVal iIFIID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        bRes = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT PFS_ID "
            strSQL = strSQL + " FROM PannelloFotovString INNER JOIN Utente ON PFS_U_ID = U_ID INNER JOIN InverterFotovInst ON PFS_IFI_ID = IFI_ID INNER JOIN ContatoreDiProduzioneInst ON IFI_CDPI_ID = CDPI_ID INNER JOIN ImpiantoDiProduzione ON CDPI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
            strSQL = strSQL + " WHERE " + " C_ID = " + iCID.ToString + " AND IDP_ID = " + iIDPID.ToString + " AND IFI_ID = " + iIFIID.ToString + " AND CDPI_ID = " + iCDPIID.ToString

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If DelPannelliFotovoltaici(dr("PFS_ID"), iUID) = False Then
                                bRes = False
                            End If
                        Next dr
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsPannelliFotovoltaiciConfigured(ByVal iPFIPFSID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM PannelloFotovInst "
            strSQL = strSQL + " WHERE PFI_PFS_ID = @PFI_PFS_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@PFI_PFS_ID", iPFIPFSID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddPannelliFotovoltaici(ByVal iPFID As Integer, ByVal iPFIPFSID As Integer, ByVal iPFINr As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [PannelloFotovInst] "
            strSQL = strSQL + "  (PFI_PF_ID,  PFI_PFS_ID,  PFI_Nr,  PFI_DataOra,  PFI_U_ID) VALUES "
            strSQL = strSQL + "  (@PFI_PF_ID, @PFI_PFS_ID, @PFI_Nr, @PFI_DataOra, @PFI_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@PFI_PF_ID", iPFID)
            cmd.Parameters.AddWithValue("@PFI_PFS_ID", iPFIPFSID)
            cmd.Parameters.AddWithValue("@PFI_Nr", iPFINr)
            cmd.Parameters.AddWithValue("@PFI_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@PFI_U_ID", iUID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function DelPannelliFotovoltaici(ByVal iPFIPFSID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = "DELETE FROM [PannelloFotovInst] "
            strSQL = strSQL + "WHERE PFI_PFS_ID = @PFI_PFS_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@PFI_PFS_ID", iPFIPFSID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        Return bRes

    End Function

    Public Function AddAllStringhePannelliFotovoltaici(ByVal iCID As Integer, ByVal iIDPID As Integer, ByVal iCDPIID As Integer, ByVal iIFIID As Integer, ByVal iPFSNrTotDaAgg As Integer, ByVal iUID As Integer) As Boolean
        Dim iIndice As Integer
        Dim bRes As Boolean

        bRes = True

        For iIndice = 1 To iPFSNrTotDaAgg
            If IsStringaPannelliFotovoltaiciConfigured(iCID, iIDPID, iCDPIID, iIFIID, iIndice, iUID) = False Then
                If AddStringaPannelliFotovoltaici(iIFIID, iIndice, iUID) = False Then
                    bRes = False
                End If
            End If
        Next

        Return bRes

    End Function

    Public Function IsStringaPannelliFotovoltaiciConfigured(ByVal iCID As Integer, ByVal iIDPID As Integer, ByVal iCDPIID As Integer, ByVal iIFIID As Integer, ByVal iPFSNr As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, CDPI_ID, CDPI_Nr, IFI_ID, IFI_Nr, PannelloFotovString.*, U_NomeCognome "
            strSQL = strSQL + " FROM PannelloFotovString INNER JOIN Utente ON PFS_U_ID = U_ID INNER JOIN InverterFotovInst ON PFS_IFI_ID = IFI_ID INNER JOIN ContatoreDiProduzioneInst ON IFI_CDPI_ID = CDPI_ID INNER JOIN ImpiantoDiProduzione ON CDPI_IDP_ID = IDP_ID INNER JOIN Cliente ON IDP_C_ID = C_ID "
            strSQL = strSQL + " WHERE  C_ID = " + iCID.ToString + " AND IDP_ID = " + iIDPID.ToString + " AND IFI_ID = " + iIFIID.ToString + " AND CDPI_ID = " + iCDPIID.ToString + " AND PFS_Nr = " + iPFSNr.ToString()

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddStringaPannelliFotovoltaici(ByVal iPFIPFSID As Integer, ByVal iPFINr As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [PannelloFotovString] "
            strSQL = strSQL + "  (PFS_IFI_ID,  PFS_Nr,  PFS_DataOra,  PFS_U_ID) VALUES "
            strSQL = strSQL + "  (@PFS_IFI_ID, @PFS_Nr, @PFS_DataOra, @PFS_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@PFS_IFI_ID", iPFIPFSID)
            cmd.Parameters.AddWithValue("@PFS_Nr", iPFINr)
            cmd.Parameters.AddWithValue("@PFS_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@PFS_U_ID", iUID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function AddAllInputDatalogger(ByVal iLIID As Integer, ByVal iIDPID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        bRes = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM IngressoTipo "
            strSQL = strSQL + " WHERE IT_ID >= 101 AND IT_ID <= 196 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If IsInputDataloggerConfigured(iLIID, dr("IT_ID"), iUID) = False Then
                                If AddInputDatalogger(iLIID, dr("IT_ID"), iIDPID, iUID) = False Then
                                    bRes = False
                                End If
                            End If
                        Next dr
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function IsInputDataloggerConfigured(ByVal iLIID As Integer, ByVal iITID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM IngressoTipo "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON IngressoTipo.IT_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID AND IT_ID = @IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function AddInputDatalogger(ByVal iLIID As Integer, ByVal iITID As Integer, ByVal iIDPID As Integer, ByVal iUID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [LoggerInst_X_ImpiantoDiProduzione_X_Config] "
            strSQL = strSQL + "  (LIIDPC_LI_ID,  LIIDPC_IT_ID,  LIIDPC_IDP_ID,  LIIDPC_DataOra,  LIIDPC_U_ID ) VALUES "
            strSQL = strSQL + "  (@LIIDPC_LI_ID, @LIIDPC_IT_ID, @LIIDPC_IDP_ID, @LIIDPC_DataOra, @LIIDPC_U_ID) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID", iITID)
            cmd.Parameters.AddWithValue("@LIIDPC_IDP_ID", iIDPID)
            cmd.Parameters.AddWithValue("@LIIDPC_DataOra", Date.Now)
            cmd.Parameters.AddWithValue("@LIIDPC_U_ID", iUID)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", iUID, Nothing)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function IsModbusAddressAlreadyPresent(ByVal iLIID As Integer, ByVal ushModbusAddress As UShort) As Boolean
        Dim bAddressAlreadyPresent As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT StringTesterInst.STI_Indirizzo_Modbus, InverterTesterInst.ITI_Indirizzo_Modbus "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN InverterTesterInst ON LoggerInst.LI_ID = InverterTesterInst.ITI_LI_ID "
            strSQL = strSQL + " INNER JOIN StringTesterInst ON LoggerInst.LI_ID = StringTesterInst.STI_LI_ID "
            strSQL = strSQL + " WHERE (LI_ID = @LI_ID) AND ((StringTesterInst.STI_Indirizzo_Modbus = @IM) OR (InverterTesterInst.ITI_Indirizzo_Modbus = @IM)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@IM", CInt(ushModbusAddress))

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bAddressAlreadyPresent = True
                    End If
                Else
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bAddressAlreadyPresent

    End Function

    Public Function GetEnergia(ByVal iIDPID As Integer, ByVal iLIID As Integer, ByVal bDisableParameterLIID As Boolean, ByVal iITID_1 As Integer, ByVal iITID_2 As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal strType As String = "SUM", Optional ByVal iDigits As Integer = 1) As Double
        Dim dbValue As Double

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT " + strType + "(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID AND LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            strSQL = strSQL + " WHERE (ImpiantoDiProduzione.IDP_ID = @IDP_ID) AND ((LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = @LIIDPC_IT_ID_1) OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = @LIIDPC_IT_ID_2)) "
            If bDisableParameterLIID = False Then
                strSQL = strSQL + " AND (LoggerInst.LI_ID = " + iLIID.ToString() + ") "
            End If
            strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@IDP_ID", iIDPID)
            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID_1", iITID_1)
            cmd.Parameters.AddWithValue("@LIIDPC_IT_ID_2", iITID_2)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dbValue = ds.Tables(0).Rows(0).Item("VALUE")
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return Math.Round(dbValue, iDigits)

    End Function

    Public Function GetDataEnergiaMaxProdottaUDT(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date) As String
        Dim dblMaxValueKwhValue As Double
        Dim strMaxValueKwhData As String = ""

        Dim strSQL As String
        Dim ds_1 As New DataSet
        Dim ds_2 As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT MAX(ValueKwh) AS MaxValueKwh "
            strSQL = strSQL + " FROM ( "
            strSQL = strSQL + " SELECT SUM(LIIDPV_Valore) As ValueKwh, LIIDPV_DataOra "
            strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = @LI_ID) AND ((LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 152) OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 153)) "
            strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "
            strSQL = strSQL + " GROUP BY LIIDPV_DataOra "
            strSQL = strSQL + " ) AS TBL_Temp "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds_1)

            If Not ds_1 Is Nothing Then
                If ds_1.Tables.Count > 0 Then
                    If ds_1.Tables(0).Rows.Count > 0 Then
                        If Not ds_1.Tables(0).Rows(0).Item("MaxValueKwh") Is DBNull.Value Then
                            dblMaxValueKwhValue = ds_1.Tables(0).Rows(0).Item("MaxValueKwh")

                            strSQL = " SELECT DISTINCT LIIDPV_DataOra "
                            strSQL = strSQL + " FROM ( "
                            strSQL = strSQL + " SELECT SUM(LIIDPV_Valore) As ValueKwh, LIIDPV_DataOra "
                            strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
                            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
                            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = @LI_ID) AND ((LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 152) OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 153)) "
                            strSQL = strSQL + " AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "
                            strSQL = strSQL + " GROUP BY LIIDPV_DataOra "
                            strSQL = strSQL + " ) AS TBL_Temp "
                            strSQL = strSQL + " WHERE(ValueKwh = @ValueKwh) "

                            cmd.CommandText = strSQL

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
                            cmd.Parameters.AddWithValue("@ValueKwh", dblMaxValueKwhValue)
                            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
                            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

                            da.SelectCommand = cmd
                            da.Fill(ds_2)

                            If Not ds_2 Is Nothing Then
                                If ds_2.Tables.Count > 0 Then
                                    If ds_2.Tables(0).Rows.Count > 0 Then
                                        strMaxValueKwhData = strMaxValueKwhData + dblMaxValueKwhValue.ToString() + " Kwh, il: "
                                        For Each dr As DataRow In ds_2.Tables(0).Rows
                                            strMaxValueKwhData = strMaxValueKwhData + dr.Item("LIIDPV_DataOra").ToString() + " - "
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds_1.Dispose()
        ds_2.Dispose()

        Return strMaxValueKwhData

    End Function

    Public Function GetDataEnergiaMaxProdottaGiorno(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date) As String

        Dim dblMaxValueKwhValue As Double
        Dim strMaxValueKwhValue As String = ""
        Dim strMaxValueKwhData As String = ""
        Dim strResult As String = ""

        Dim strSQL As String
        Dim ds_1 As New DataSet
        Dim ds_2 As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT MAX(LISM_CDP_1 + LISM_CDP_2) AS MaxValueKwh "
            strSQL = strSQL + " FROM LoggerInstStatMemoDay "
            strSQL = strSQL + " WHERE (LISM_LI_ID = @LI_ID) AND (LISM_Data >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LISM_Data < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds_1)

            If Not ds_1 Is Nothing Then
                If ds_1.Tables.Count > 0 Then
                    If ds_1.Tables(0).Rows.Count > 0 Then
                        If Not ds_1.Tables(0).Rows(0).Item("MaxValueKwh") Is DBNull.Value Then
                            dblMaxValueKwhValue = ds_1.Tables(0).Rows(0).Item("MaxValueKwh")
                            strMaxValueKwhValue = dblMaxValueKwhValue.ToString()

                            strSQL = " SELECT LISM_Data "
                            strSQL = strSQL + " FROM LoggerInstStatMemoDay "
                            strSQL = strSQL + " WHERE (LISM_LI_ID = @LI_ID) AND (LISM_Data >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LISM_Data < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) AND ((LISM_CDP_1 + LISM_CDP_2) = @ValueKwh) "

                            cmd.CommandText = strSQL

                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
                            cmd.Parameters.AddWithValue("@ValueKwh", dblMaxValueKwhValue)
                            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
                            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

                            da.SelectCommand = cmd
                            da.Fill(ds_2)

                            If Not ds_2 Is Nothing Then
                                If ds_2.Tables.Count > 0 Then
                                    If ds_2.Tables(0).Rows.Count > 0 Then
                                        strResult = strResult + strMaxValueKwhValue + " Kwh, il: "
                                        For Each dr As DataRow In ds_2.Tables(0).Rows
                                            strMaxValueKwhData = dr.Item("LISM_Data").ToString() + " - "
                                            strResult = strResult + strMaxValueKwhData
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds_1.Dispose()
        ds_2.Dispose()

        Return strResult

    End Function

    'Public Function GetIrraggiamento(ByVal iIDPID As Integer, ByVal iLIID As Integer, ByVal iITID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal strType As String = "SUM", Optional ByVal iDigits As Integer = 0) As Double
    '    Dim dbValue As Double

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter

    '    Try

    '        strSQL = " SELECT " + strType + "(InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_Valore) AS VALUE "
    '        strSQL = strSQL + " FROM InverterTesterInst_X_InverterFotovInst_X_Valore "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID "
    '        strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTester ON InverterTesterInst.ITI_INT_ID = InverterTester.INT_ID "
    '        strSQL = strSQL + " INNER JOIN LoggerInst ON InverterTesterInst.ITI_LI_ID = LoggerInst.LI_ID "
    '        strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
    '        strSQL = strSQL + " WHERE (ImpiantoDiProduzione.IDP_ID = @IDP_ID) AND (LoggerInst.LI_ID = @LI_ID) AND (IngressoTipo.IT_ID = @IT_ID) "
    '        strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@IDP_ID", iIDPID)
    '        cmd.Parameters.AddWithValue("@LI_ID", iLIID)
    '        cmd.Parameters.AddWithValue("@IT_ID", iITID)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
    '                        dbValue = ds.Tables(0).Rows(0).Item("VALUE")
    '                    End If
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '    End Try

    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()
    '    ds.Dispose()

    '    Return Math.Round(dbValue, iDigits)

    'End Function

    Public Function GetIrraggiamentoMedio(ByVal iLIID As Integer, ByVal dblNrSolarimetri As Double, ByVal dblSoglia As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " SELECT AVG(VALUE_1) AS VALUE_2 "
            strSQL = strSQL + " FROM ( "

            strSQL = strSQL + " SELECT SUM( (ITIIFIV_Valore / @dblNrSolarimetri) ) AS VALUE_1 "
            strSQL = strSQL + " FROM InverterTesterInst "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
            strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra < CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
            strSQL = strSQL + " GROUP BY ITIIFIV_UDT "
            strSQL = strSQL + " HAVING (AVG (ITIIFIV_Valore) >= @dblSoglia) "

            strSQL = strSQL + " ) AS TBL_Temp "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@dblNrSolarimetri", dblNrSolarimetri)
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@dblSoglia", dblSoglia)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE_2") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE_2"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetTemperaturaMedia(ByVal iLIID As Integer, ByVal dblSoglia As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim dsUDT As New DataSet
        Dim dsTMP As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strUDT As String = ""

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " SELECT ITIIFIV_UDT "
            strSQL = strSQL + " FROM InverterTesterInst "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
            strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra < CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
            strSQL = strSQL + " GROUP BY ITIIFIV_UDT "
            strSQL = strSQL + " HAVING (AVG (ITIIFIV_Valore) >= @dblSoglia) "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@dblSoglia", dblSoglia)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(dsUDT)

            If Not dsUDT Is Nothing Then
                If dsUDT.Tables.Count > 0 Then
                    For Each dr As DataRow In dsUDT.Tables(0).Rows
                        If strUDT.Length = 0 Then
                            strUDT = strUDT + "ITIIFIV_UDT = "
                        Else
                            strUDT = strUDT + " OR ITIIFIV_UDT = "
                        End If
                        strUDT = strUDT + dr.Item(0).ToString
                    Next
                End If
            End If

            If strUDT.Length > 0 Then
                strSQL = " SELECT AVG( ITIIFIV_Valore ) AS VALUE_1 "
                strSQL = strSQL + " FROM InverterTesterInst "
                strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
                strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
                strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 417) "
                strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
                strSQL = strSQL + " AND (" + strUDT + ") "
                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LI_ID", iLIID)
                cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
                cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

                da.SelectCommand = cmd
                da.Fill(dsTMP)

                If Not dsTMP Is Nothing Then
                    If dsTMP.Tables.Count > 0 Then
                        If dsTMP.Tables(0).Rows.Count > 0 Then
                            If Not dsTMP.Tables(0).Rows(0).Item("VALUE_1") Is DBNull.Value Then
                                dblValueResult = Math.Round(dsTMP.Tables(0).Rows(0).Item("VALUE_1"), 1)
                            End If
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        dsUDT.Dispose()
        dsTMP.Dispose()

        Return bRes

    End Function

    'Public Function GetTemperaturaMedia(ByVal iIDPID As Integer, ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal iDigits As Integer = 0) As Double
    '    Dim dbValue As Double

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter

    '    Try

    '        strSQL = " SELECT AVG(InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_Valore) AS VALUE "
    '        strSQL = strSQL + " FROM InverterTesterInst_X_InverterFotovInst_X_Valore "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID "
    '        strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID = InverterTesterInst.ITI_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTester ON InverterTesterInst.ITI_INT_ID = InverterTester.INT_ID "
    '        strSQL = strSQL + " INNER JOIN LoggerInst ON InverterTesterInst.ITI_LI_ID = LoggerInst.LI_ID "
    '        strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
    '        strSQL = strSQL + " WHERE (ImpiantoDiProduzione.IDP_ID = @IDP_ID) AND (LoggerInst.LI_ID = @LI_ID) AND (IngressoTipo.IT_ID = 417) "
    '        strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@IDP_ID", iIDPID)
    '        cmd.Parameters.AddWithValue("@LI_ID", iLIID)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
    '                        dbValue = ds.Tables(0).Rows(0).Item("VALUE")
    '                    End If
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
    '    End Try

    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()
    '    ds.Dispose()

    '    Return Math.Round(dbValue, iDigits)

    'End Function

    Public Function GetHj(ByVal iLIID As Integer, ByVal dblNrSolarimetri As Double, ByVal dblValoreUDTMinuti As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " SELECT SUM( (ITIIFIV_Valore * @dblValoreUDTMinuti) / (60000.0 * @dblNrSolarimetri) ) AS VALUE_Hj "
            strSQL = strSQL + " FROM InverterTesterInst "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
            strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra < CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@dblNrSolarimetri", dblNrSolarimetri)
            cmd.Parameters.AddWithValue("@dblValoreUDTMinuti", dblValoreUDTMinuti)
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE_Hj") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE_Hj"), 3)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetHG(ByVal iLIID As Integer, ByVal dblNrSolarimetri As Double, ByVal dblValoreUDTMinuti As Double, ByVal dblSoglia As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " SELECT SUM(VALUE_HG_TEMP) AS VALUE_HG "
            strSQL = strSQL + " FROM ( "

            strSQL = strSQL + " SELECT SUM( (ITIIFIV_Valore * @dblValoreUDTMinuti) / (60000.0 * @dblNrSolarimetri) ) AS VALUE_HG_TEMP "
            strSQL = strSQL + " FROM InverterTesterInst "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
            strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
            strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
            strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra < CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
            strSQL = strSQL + " GROUP BY ITIIFIV_UDT "
            strSQL = strSQL + " HAVING (AVG (ITIIFIV_Valore) >= @dblSoglia) "

            strSQL = strSQL + " ) AS TBL_Temp "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@dblNrSolarimetri", dblNrSolarimetri)
            cmd.Parameters.AddWithValue("@dblValoreUDTMinuti", dblValoreUDTMinuti)
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@dblSoglia", dblSoglia)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE_HG") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE_HG"), 3)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetLIInFunzione() As DataTable
        Dim dt As DataTable = Nothing

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " WHERE LI_In_Funzione = 'true' "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return dt

    End Function

    Public Function SetLIAutoAggInCorso(ByVal iLIID As Integer, ByVal bStatus As Boolean) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_AutoAggInCorso = @LI_AutoAggInCorso "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LI_AutoAggInCorso", bStatus)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function SetLIAllarmeOfflineInviato(ByVal iLIID As Integer, ByVal bStatus As Boolean) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_AllarmeOfflineInviato = @LI_AllarmeOfflineInviato "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LI_AllarmeOfflineInviato", bStatus)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetLIAutoAggInCorso(ByVal iLIID As Integer) As Boolean
        Dim bLIAutoAggInCorso As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LI_AutoAggInCorso "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bLIAutoAggInCorso = ds.Tables(0).Rows(0).Item("LI_AutoAggInCorso")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bLIAutoAggInCorso

    End Function

    Public Function SetLIAzioneModbusDaEseguireAllaProssConn(ByVal iLIID As Integer, ByVal iAzioneModbus As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_AzioneModbusDaEseguireAllaProssConn = @LI_AzioneModbusDaEseguireAllaProssConn "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LI_AzioneModbusDaEseguireAllaProssConn", iAzioneModbus)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetLIAzioneModbusDaEseguireAllaProssConn(ByVal iLIID As Integer) As Integer
        Dim iLIAMDEAPC As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LI_AzioneModbusDaEseguireAllaProssConn FROM [LoggerInst] "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iLIAMDEAPC = ds.Tables(0).Rows(0).Item("LI_AzioneModbusDaEseguireAllaProssConn")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iLIAMDEAPC

    End Function

    Public Function TestDatabaseConnection() As Boolean
        Dim bREs As Boolean
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try

            CustomSQLConnectionOpen(cn, cmd)

            bREs = True

        Catch ex As Exception

            ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)

        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bREs

    End Function

    Public Function GetPlantDescription(ByVal iLIID As Integer) As String
        Dim strPlantDescription As String

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        strPlantDescription = ""
        Try

            strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, Cliente.C_Societa, ImpiantoDiProduzione.IDP_Nome, LoggerInst.LI_Nr "
            strSQL = strSQL + " FROM Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = LoggerInst.LI_IDP_ID "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        strPlantDescription = ds.Tables(0).Rows(0).Item("C_Nome") + " - " + ds.Tables(0).Rows(0).Item("C_Cognome") + " - " + ds.Tables(0).Rows(0).Item("C_Societa") + " - " + ds.Tables(0).Rows(0).Item("IDP_Nome") + " - " + ds.Tables(0).Rows(0).Item("LI_Nr").ToString()
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strPlantDescription

    End Function

    Public Sub CopyDLValueInDBValue(ByVal iLIID As Integer)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " UPDATE LoggerInstParamConfig "
            strSQL = strSQL + " SET LIPC_LPC_VAL_MEMO_DB = LIPC_LPC_VAL_MEMO_DL "
            strSQL = strSQL + " WHERE LIPC_LI_ID = @LIPC_LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)

            cmd.ExecuteNonQuery()

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_OK, "", "", "", DEFAULT_OPERATOR_ID, Nothing)

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Function CheckIfDataExistForLIID(ByVal iLIID As Integer, ByVal dtStart As Date, ByVal dtStop As Date) As Boolean

        Dim bDataExist As Boolean
        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, Cliente.C_Societa, ImpiantoDiProduzione.IDP_Nome, ImpiantoDiProduzione.IDP_Indirizzo, ImpiantoDiProduzione.IDP_Indirizzo, LoggerInst.LI_Nr "
            strSQL = strSQL + " FROM Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = LoggerInst.LI_IDP_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON ImpiantoDiProduzione.IDP_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105) AND LoggerInst.LI_ID = @LI_ID) "
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtStart)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtStop)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bDataExist = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bDataExist

    End Function

    Public Function GetComplementTwo(ByVal ui As UInt16) As Int16
        Dim iRes As Int16
        Dim ba2(1) As Byte
        Try
            ba2 = BitConverter.GetBytes(ui)
            iRes = BitConverter.ToInt16(ba2, 0)
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        Return iRes
    End Function

    Public Function GetDataLoggerAllarmi(ByVal iLIID As Integer) As String
        Dim strTestoAllarmeTotale As String = ""

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strTestoAllarme As String = ""

        Try
            ' Elimino, se necessario i Trigger per questo Data Logger e lo segnalo.
            DeleteDataLoggerAllarmeInviato(iLIID)

            strSQL = " SELECT DISTINCT LIA_LI_ID, LIA_IT_ID "
            strSQL = strSQL + " FROM LoggerInst_X_Allarmi "
            strSQL = strSQL + " WHERE LIA_LI_ID = @LIA_LI_ID "
            strSQL = strSQL + " ORDER BY LIA_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIA_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        ' Anche se ho piu' allarmi dello stesso tipo per lo stesso DL,
                        ' avro' solo 1 riga per ogni tipo di allarme
                        If GetDataLoggerAllarmeInviato(iLIID, dr("LIA_IT_ID")) = False Then
                            StoreDataLoggerAllarmeInviato(iLIID, dr("LIA_IT_ID"))
                            strTestoAllarme = GENERICA_DESCRIZIONE("IT_Nome", "IngressoTipo", "IT_ID", dr("LIA_IT_ID"), DEFAULT_OPERATOR_ID)
                            strTestoAllarmeTotale = strTestoAllarmeTotale + dr("LIA_IT_ID").ToString() + " - " + strTestoAllarme + vbCrLf
                            ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OK, "Aggiunto Codice Allarme: " + dr("LIA_IT_ID").ToString() + ", Aggiunta Stringa di Allarme: " + strTestoAllarme + ", nel testo dell'email.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                        DeleteDataLoggerAllarmi(iLIID, dr("LIA_IT_ID"))
                    Next
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strTestoAllarmeTotale

    End Function

    'Public Sub SendEMailByLIID(ByVal iLIID As Integer, ByVal strPrefissoOggetto As String, ByVal strBody As String)
    '    Dim email As New MailMessage
    '    Dim smtp As New SmtpClient
    '    Dim strSMTPHost As String = ""
    '    Dim iSMTPPort As Integer
    '    Dim strSMTPUserName As String = ""
    '    Dim strSMTPPassword As String = ""

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter

    '    Try

    '        strSQL = " SELECT * "
    '        strSQL = strSQL + " FROM LoggerInst_X_IndEMailSuAll "
    '        strSQL = strSQL + " WHERE LIIEMSA_LI_ID = @LIIEMSA_LI_ID "

    '        CustomSQLConnectionOpen(cn, cmd)
    '        'cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@LIIEMSA_LI_ID", iLIID)

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then

    '                    ' Prelevo i dati delle email da inviare
    '                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Corpo: " + strBody + " - Totale Caratteri Spediti per Ciascuna EMail Nr:" + strBody.Count.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

    '                    If GetDGI(61, strSMTPHost) = True Then
    '                        If GetDGI(62, iSMTPPort) = True Then
    '                            If GetDGI(63, strSMTPUserName) = True Then
    '                                If GetDGI(64, strSMTPPassword) = True Then
    '                                    Try
    '                                        smtp.Host = strSMTPHost
    '                                        smtp.Port = iSMTPPort
    '                                        smtp.Credentials = New System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword)
    '                                        For Each dr As DataRow In ds.Tables(0).Rows
    '                                            smtp.Send(dr("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE"), dr("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO"), strPrefissoOggetto + " " + dr("LIIEMSA_EMAIL_OGGETTO"), dr("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME") + " " + vbCrLf + strBody + vbCrLf + dr("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME"))
    '                                        Next
    '                                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Corpo: " + strBody + " - Totale Caratteri Spediti per Ciascuna EMail Nr:" + strBody.Count.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                    Catch ex As Exception
    '                                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                    End Try
    '                                Else
    '                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Password non impostata", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                End If
    '                            Else
    '                                ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP User Name non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                            End If
    '                        Else
    '                            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Port non impostata", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                        End If
    '                    Else
    '                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Host non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '    End Try

    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()
    '    ds.Dispose()

    'End Sub

    Public Sub SendEMailReportAllarmiDL(ByVal iLIID As Integer, ByVal strPrefissoOggetto As String, ByVal strBody As String, ByVal strAttachmentFileName As String, ByVal bAllarme As Boolean, ByVal bReport As Boolean)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerInst_X_IndEMailSuAll "
            strSQL = strSQL + " WHERE LIIEMSA_LI_ID = @LIIEMSA_LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIEMSA_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If (CBool(dr("LIIEMSA_InviaAllarme")) = True And bAllarme = True) Or (CBool(dr("LIIEMSA_InviaReport")) = True And bReport = True) Then
                                If bAllarme = True Then
                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Email Allarme inviata a: " + dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                                If bReport = True Then
                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Email Report inviata a: " + dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If

                                SendEMail(iLIID, dr.Item("LIIEMSA_EMAIL_INDIRIZZO_MITTENTE").ToString(), dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").ToString(), strPrefissoOggetto + " " + dr.Item("LIIEMSA_EMAIL_OGGETTO").ToString(), dr.Item("LIIEMSA_EMAIL_TESTO_PRECEDENTE_ALLARME").ToString() + " " + vbCrLf + strBody + vbCrLf + dr.Item("LIIEMSA_EMAIL_TESTO_SUCCESSIVO_ALLARME").ToString(), strAttachmentFileName)

                                If bAllarme = True Then
                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Email Allarme inviata a: " + dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                                If bReport = True Then
                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Email Report inviata a: " + dr.Item("LIIEMSA_EMAIL_INDIRIZZO_DESTINATARIO").ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    'Public Sub SendEMailReport(ByVal strAttachmentFileName As String, ByVal strBody As String, ByVal dtSTART As Date, ByVal dtSTOP As Date)
    '    Dim email As New MailMessage
    '    Dim smtp As New SmtpClient

    '    Dim strMittente As String = ""
    '    Dim strDestinatario As String = ""
    '    Dim strOggetto As String = ""

    '    Dim strSMTPHost As String = ""
    '    Dim iSMTPPort As Integer
    '    Dim strSMTPUserName As String = ""
    '    Dim strSMTPPassword As String = ""
    '    Dim mmMailMessage As System.Net.Mail.MailMessage

    '    Try
    '        If GetDGI(61, strSMTPHost) = True Then
    '            If GetDGI(62, iSMTPPort) = True Then
    '                If GetDGI(63, strSMTPUserName) = True Then
    '                    If GetDGI(64, strSMTPPassword) = True Then
    '                        If GetDGI(81, strMittente) = True Then
    '                            If strMittente.Count > 0 Then
    '                                If GetDGI(82, strDestinatario) = True Then
    '                                    If strDestinatario.Count > 0 Then
    '                                        If GetDGI(86, strOggetto) = True Then
    '                                            Try
    '                                                ' Prelevo i dati delle email da inviare
    '                                                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Invio Report Semplificato DI Tutti Gli Impianti. Dal: " + dtSTART.ToString() + " Al: " + dtSTOP.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

    '                                                smtp.Host = strSMTPHost
    '                                                smtp.Port = iSMTPPort
    '                                                smtp.Credentials = New System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword)

    '                                                mmMailMessage = New System.Net.Mail.MailMessage(strMittente, strDestinatario, strOggetto, strBody)

    '                                                If strAttachmentFileName.Length > 0 Then
    '                                                    ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Inserimento Allegato: " + strAttachmentFileName, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                                    mmMailMessage.Attachments.Add(New System.Net.Mail.Attachment(strAttachmentFileName))
    '                                                    ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Inserimento Allegato: " + strAttachmentFileName, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                                End If

    '                                                smtp.Send(mmMailMessage)
    '                                                'smtp.Send(strMittente, strDestinatario, strOggetto, strBody)

    '                                                mmMailMessage.Dispose()

    '                                                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Invio Report Semplificato DI Tutti Gli Impianti. Dal: " + dtSTART.ToString() + " Al: " + dtSTOP.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                            Catch ex As Exception
    '                                                Dim strInnerException As String = ""
    '                                                If Not ex.InnerException Is Nothing Then
    '                                                    strInnerException = ex.InnerException.Message
    '                                                End If
    '                                                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_EXCEPT, ex.Message + " " + strInnerException + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                            End Try
    '                                        Else
    '                                            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Oggetto non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                        End If
    '                                    End If
    '                                Else
    '                                    ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Destinatario non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                                End If
    '                            End If
    '                        Else
    '                            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Mittente non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                        End If
    '                    Else
    '                        ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Password non impostata", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                    End If
    '                Else
    '                    ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP User Name non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '                End If
    '            Else
    '                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Port non impostata", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '            End If
    '        Else
    '            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "SMTP Host non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '    End Try

    'End Sub

    'Public Sub SendEMailReport(ByVal strOggetto As String, ByVal strBody As String, ByVal strAttachmentFileName As String, ByVal dtSTART As Date, ByVal dtSTOP As Date)

    '    Dim strMittente As String = ""
    '    Dim strDestinatario As String = ""

    '    Try
    '        If GetDGI(81, strMittente) = True Then
    '            If GetDGI(82, strDestinatario) = True Then
    '                SendEMail(0, strMittente, strDestinatario, strOggetto, strBody, strAttachmentFileName)
    '            Else
    '                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 82, Destinatario Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '            End If
    '        Else
    '            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 81, Mittente Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '        End If

    '    Catch ex As Exception
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
    '    End Try

    'End Sub

    Public Sub SendEMail(ByVal iLIID As Integer, ByVal strMittente As String, ByVal strDestinatario As String, ByVal strOggetto As String, ByVal strBody As String, ByVal strAttachmentFileName As String)

        Dim strSMTPHost As String = ""
        Dim iSMTPPort As Integer
        Dim strSMTPUserName As String = ""
        Dim strSMTPPassword As String = ""
        Dim smtp As SmtpClient
        Dim mmMailMessage As System.Net.Mail.MailMessage

        ' Prelevo i dati delle email da inviare
        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Oggetto: " + strOggetto, "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        Try
            If GetDGI(61, strSMTPHost) = True Then
                If GetDGI(62, iSMTPPort) = True Then
                    If GetDGI(63, strSMTPUserName) = True Then
                        If GetDGI(64, strSMTPPassword) = True Then
                            If strMittente.Count > 0 Then
                                If strDestinatario.Count > 0 Then
                                    Try
                                        smtp = New SmtpClient
                                        smtp.Host = strSMTPHost
                                        smtp.Port = iSMTPPort
                                        smtp.Credentials = New System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword)

                                        mmMailMessage = New System.Net.Mail.MailMessage(strMittente, strDestinatario, strOggetto, strBody)

                                        If strAttachmentFileName.Length > 0 Then
                                            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OPERAZIONE_IN_CORSO, "Inserimento Allegato: " + strAttachmentFileName, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                            mmMailMessage.Attachments.Add(New System.Net.Mail.Attachment(strAttachmentFileName))
                                            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Inserimento Allegato: " + strAttachmentFileName, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                        End If

                                        smtp.Send(mmMailMessage)

                                        mmMailMessage.Dispose()

                                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_OK, "Oggetto: " + strOggetto, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                    Catch ex As Exception
                                        Dim strInnerException As String = ""
                                        If Not ex.InnerException Is Nothing Then
                                            strInnerException = ex.InnerException.Message
                                        End If
                                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_EXCEPT, ex.Message + " " + strInnerException + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                    End Try
                                Else
                                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Destinatario non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            Else
                                ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Mittente non impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                            End If
                        Else
                            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 64, SMTP Password, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    Else
                        ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 63, SMTP User Name, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                Else
                    ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 62, SMTP Port, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            Else
                ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 61, SMTP Server, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

    End Sub

    Public Sub ResetAllLIAllarmeOfflineInviato()

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_AllarmeOfflineInviato = @LI_AllarmeOfflineInviato "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_AllarmeOfflineInviato", False)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "Tutti i Trigger di Offline di Tutti i Data Logger Cancellati.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If

        Catch ex As Exception

            ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Function SetLIStatMemoDayEseguito(ByVal iLIID As Integer, ByVal bStatus As Boolean) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_StatMemoDayEseguito = @LI_StatMemoDayEseguito "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LI_StatMemoDayEseguito", bStatus)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function SetAllLIStatMemoDayEseguito(ByVal bStatus As Boolean) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_StatMemoDayEseguito = @LI_StatMemoDayEseguito "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_StatMemoDayEseguito", bStatus)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(0, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function SetLIReportDelMeseInviatoData(ByVal iLIID As Integer, ByVal dtLIReportDelMeseInviatoData As Date) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            ' Aggiorno la Data ed Ora dell'invio, solo quando setto il flag
            strSQL = " UPDATE LoggerInst "
            strSQL = strSQL + " SET LI_ReportDelMeseInviatoData = @LI_ReportDelMeseInviatoData "
            strSQL = strSQL + " WHERE LI_ID = @LI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LI_ReportDelMeseInviatoData", dtLIReportDelMeseInviatoData)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception

            ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaCDPByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date) As Double
        Dim dbValue As Double

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID AND LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            strSQL = strSQL + " WHERE ((LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 152) OR (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 153)) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra <= CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dbValue = ds.Tables(0).Rows(0).Item("VALUE")
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return Math.Round(dbValue, 1)

    End Function

    Public Sub DeleteDataLoggerAllarmi(ByVal iLIID As Integer, ByVal iITID As Integer)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = "DELETE FROM [LoggerInst_X_Allarmi] "
            strSQL = strSQL + "WHERE LIA_LI_ID = @LIA_LI_ID AND LIA_IT_ID = @LIA_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIA_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIA_IT_ID", iITID)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Codice Allarme: " + iITID.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try


        ds.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Sub StoreDataLoggerAllarme(ByVal iLIID As Integer, ByVal iITID As Integer)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim bDataLoggerAllarmePresente As Boolean

        Try
            'Apro la connessione
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            ' Prima verifico che l'allarme non sia gia' stato inserito...
            strSQL = " SELECT TOP 1 LIA_LI_ID, LIA_IT_ID "
            strSQL = strSQL + " FROM LoggerInst_X_Allarmi "
            strSQL = strSQL + " WHERE LIA_LI_ID = @LIA_LI_ID AND LIA_IT_ID = @LIA_IT_ID "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIA_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIA_IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bDataLoggerAllarmePresente = True
                    End If
                End If
            End If

            ' Se non e' stato ancora inserito, lo inserisco...
            If bDataLoggerAllarmePresente = False Then

                ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OPERAZIONE_IN_CORSO, "Codice Allarme: " + iITID.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

                strSQL = " INSERT INTO [LoggerInst_X_Allarmi] "
                strSQL = strSQL + "  (LIA_LI_ID,  LIA_IT_ID ) VALUES "
                strSQL = strSQL + "  (@LIA_LI_ID, @LIA_IT_ID) "

                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIA_LI_ID", iLIID)
                cmd.Parameters.AddWithValue("@LIA_IT_ID", iITID)

                If cmd.ExecuteNonQuery() > 0 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OK, "Codice Allarme: " + iITID.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Sub StoreDataLoggerAllarmeInviato(ByVal iLIID As Integer, ByVal iITID As Integer)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim bDataLoggerAllarmeInviatoPresente As Boolean

        Try
            'Apro la connessione
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            ' Prima verifico che l'allarme non sia gia' stato inserito...
            strSQL = " SELECT TOP 1 LIAI_LI_ID, LIAI_IT_ID "
            strSQL = strSQL + " FROM LoggerInst_X_AllarmiInviati "
            strSQL = strSQL + " WHERE LIAI_LI_ID = @LIAI_LI_ID AND LIAI_IT_ID = @LIAI_IT_ID "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIAI_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIAI_IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bDataLoggerAllarmeInviatoPresente = True
                    End If
                End If
            End If

            ' Se non e' stato ancora inserito, lo inserisco...
            If bDataLoggerAllarmeInviatoPresente = False Then

                ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OPERAZIONE_IN_CORSO, "Trigger Codice Allarme Inviato: " + iITID.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

                strSQL = " INSERT INTO [LoggerInst_X_AllarmiInviati] "
                strSQL = strSQL + "  (LIAI_LI_ID,  LIAI_IT_ID ) VALUES "
                strSQL = strSQL + "  (@LIAI_LI_ID, @LIAI_IT_ID) "

                cmd.CommandText = strSQL

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@LIAI_LI_ID", iLIID)
                cmd.Parameters.AddWithValue("@LIAI_IT_ID", iITID)

                If cmd.ExecuteNonQuery() > 0 Then
                    ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_OK, "Trigger Codice Allarme Inviato: " + iITID.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                End If

            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_ADD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Function GetDataLoggerAllarmeInviato(ByVal iLIID As Integer, ByVal iITID As Integer) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerInst_X_AllarmiInviati "
            strSQL = strSQL + " WHERE LIAI_LI_ID = @LIAI_LI_ID AND LIAI_IT_ID = @LIAI_IT_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIAI_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIAI_IT_ID", iITID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        bRes = True
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Sub DeleteDataLoggerAllarmeInviato(ByVal iLIID As Integer)

        Dim strSQL As String
        Dim strSQLITID As String = ""
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            ' Apro la connessione
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            ' Verifico gli allarmi presenti nella tabella "LoggerInst_X_Allarmi" riferita all' ID del Data Logger.
            strSQL = " SELECT * "
            strSQL = strSQL + " FROM LoggerInst_X_Allarmi "
            strSQL = strSQL + " WHERE LIA_LI_ID = @LIA_LI_ID "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIA_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        strSQLITID = strSQLITID + "AND LIAI_IT_ID <> " + dr.Item("LIA_IT_ID").ToString() + " "
                    Next
                End If
            End If

            ' Poi cancello tutto, tranne quelle voci. Pero' prima memorizzo quello che andro' a cancellare per inviare una segnalazione...
            strSQL = "DELETE FROM [LoggerInst_X_AllarmiInviati] "
            strSQL = strSQL + "WHERE LIAI_LI_ID = @LIAI_LI_ID " + strSQLITID

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIAI_LI_ID", iLIID)

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(iLIID, 0, AZIONE_DEL, RISULTATO_OK, "Trigger Codici Allarmi Inviati.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try


        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Sub DeleteAllDataLoggerAllarmeInviato()

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            ' Apro la connessione

            strSQL = "DELETE FROM [LoggerInst_X_AllarmiInviati] "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            If cmd.ExecuteNonQuery() > 0 Then
                ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "Tutti i Trigger Allarmi Inviati di Tutti i Data Logger Cancellati.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If


        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try


        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Public Function PrelevaDatiDiControlloSemplificatiDiTuttiGliImpiantiAsDT(ByVal dtSTART As Date, ByVal dtSTOP As Date) As DataTable

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Dim dtRep As New DataTable
        Dim drRep As DataRow

        Dim dtLastStoredLIValue As Date
        Dim dblLIPotenzaGestitaKw As Double
        Dim dbl_CDC As Double
        Dim dbl_CDP_1 As Double
        Dim dbl_CDP_2 As Double
        Dim dbl_SI As Double
        Dim dbl_STR As Double
        Dim dbl_INV As Double
        Dim dblPR As Double
        Dim dblHG As Double
        Dim dblMS As Double
        Dim dblMT As Double
        Dim dblS As Double

        Dim bRes As Boolean

        Try
            strSQL = " SELECT Cliente.C_Nome, Cliente.C_Cognome, Cliente.C_Societa, ImpiantoDiProduzione.IDP_ID, ImpiantoDiProduzione.IDP_Nome, ImpiantoDiProduzione.IDP_Indirizzo, LoggerInst.LI_ID, LoggerInst.LI_PotenzaGestitaKw, LoggerInst.LI_Nr, LoggerInst.LI_SogliaCalcoloHG, LoggerInst.LI_Kd "
            strSQL = strSQL + " FROM Cliente "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON Cliente.C_ID = ImpiantoDiProduzione.IDP_C_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON ImpiantoDiProduzione.IDP_ID = LoggerInst.LI_IDP_ID "
            strSQL = strSQL + " WHERE LI_In_Funzione = 'true' "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then

                    dtRep.Columns.Add("N Note", GetType(String))
                    dtRep.Columns("N Note").Caption = "Note"
                    dtRep.Columns.Add("N Cliente", GetType(String))
                    dtRep.Columns("N Cliente").Caption = "Cliente"
                    dtRep.Columns.Add("N Impianto Di Produzione", GetType(String))
                    dtRep.Columns("N Impianto Di Produzione").Caption = "Impianto Di Produzione"
                    dtRep.Columns.Add("N Nr DL Installato", GetType(Integer))
                    dtRep.Columns("N Nr DL Installato").Caption = "Nr DL Installato"
                    dtRep.Columns.Add("N Dal", GetType(Date))
                    dtRep.Columns("N Dal").Caption = "Dal"
                    dtRep.Columns.Add("N Al", GetType(Date))
                    dtRep.Columns("N Al").Caption = "Al"
                    dtRep.Columns.Add("N Produzione Nell'intervallo di ricerca Kwh", GetType(Double))
                    dtRep.Columns("N Produzione Nell'intervallo di ricerca Kwh").Caption = "Produzione Nell' Intervallo di Ricerca Kwh"
                    dtRep.Columns.Add("N Potenza Gestita Kw", GetType(Double))
                    dtRep.Columns("N Potenza Gestita Kw").Caption = "Potenza Gestita Kw"
                    dtRep.Columns.Add("N Performance Ratio (%)", GetType(Double))
                    dtRep.Columns("N Performance Ratio (%)").Caption = "Performance Ratio (%)"
                    dtRep.Columns.Add("N HG", GetType(Double))
                    dtRep.Columns("N HG").Caption = "HG"
                    dtRep.Columns.Add("N Media Solarimetri(W/mq)", GetType(Double))
                    dtRep.Columns("N Media Solarimetri(W/mq)").Caption = "Media Solarimetri(W/mq)"
                    dtRep.Columns.Add("N Media Temp. Pann.(°C)", GetType(Double))
                    dtRep.Columns("N Media Temp. Pann.(°C)").Caption = "Media Temp. Pann.(°C)"
                    dtRep.Columns.Add("N Soglia W/mq Calcolo Medie", GetType(Double))
                    dtRep.Columns("N Soglia W/mq Calcolo Medie").Caption = "Soglia W/mq Calcolo Medie"

                    For Each dr As DataRow In ds.Tables(0).Rows
                        bRes = GetStatMemoDay(dr.Item("LI_ID"), dbl_CDC, dbl_CDP_1, dbl_CDP_2, dbl_SI, dbl_STR, dbl_INV, dblPR, dblHG, dblMS, dblMT, dblS, dtSTART, dtSTOP)

                        dtLastStoredLIValue = GetLastStoredDateAndTimeLogValue(dr.Item("LI_ID"), "", "")

                        dblLIPotenzaGestitaKw = dr.Item("LI_PotenzaGestitaKw")

                        If bRes = False Then
                            drRep = dtRep.Rows.Add()
                            drRep.Item("N Note") = " ATTENZIONE, Non esistono Dati Di Produzione per questo impianto "
                            drRep.Item("N Cliente") = dr("C_Nome")
                            drRep.Item("N Impianto Di Produzione") = dr("IDP_Nome")
                            drRep.Item("N Nr DL Installato") = dr("LI_Nr")
                            drRep.Item("N Dal") = dtSTART
                            drRep.Item("N Al") = dtSTOP
                            drRep.Item("N Produzione Nell'intervallo di ricerca Kwh") = Math.Round((dbl_CDP_1 + dbl_CDP_2), 1)
                            drRep.Item("N Potenza Gestita Kw") = dblLIPotenzaGestitaKw
                            drRep.Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                            drRep.Item("N HG") = Math.Round(dblHG, 1)
                            drRep.Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
                            drRep.Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
                            drRep.Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblS, 1)
                        ElseIf (dbl_CDP_1 + dbl_CDP_2) <= dblLIPotenzaGestitaKw Then
                            drRep = dtRep.Rows.Add()
                            drRep.Item("N Note") = " ATTENZIONE, Produzione dell'impianto inferiore alla Soglia minima "
                            drRep.Item("N Cliente") = dr("C_Nome")
                            drRep.Item("N Impianto Di Produzione") = dr("IDP_Nome")
                            drRep.Item("N Nr DL Installato") = dr("LI_Nr")
                            drRep.Item("N Dal") = dtSTART
                            drRep.Item("N Al") = dtSTOP
                            drRep.Item("N Produzione Nell'intervallo di ricerca Kwh") = Math.Round((dbl_CDP_1 + dbl_CDP_2), 1)
                            drRep.Item("N Potenza Gestita Kw") = dblLIPotenzaGestitaKw
                            drRep.Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                            drRep.Item("N HG") = Math.Round(dblHG, 1)
                            drRep.Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
                            drRep.Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
                            drRep.Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblS, 1)
                        Else
                            drRep = dtRep.Rows.Add()
                            drRep.Item("N Note") = " Dati Di Produzione "
                            drRep.Item("N Cliente") = dr("C_Nome")
                            drRep.Item("N Impianto Di Produzione") = dr("IDP_Nome")
                            drRep.Item("N Nr DL Installato") = dr("LI_Nr")
                            drRep.Item("N Dal") = dtSTART
                            drRep.Item("N Al") = dtSTOP
                            drRep.Item("N Produzione Nell'intervallo di ricerca Kwh") = Math.Round((dbl_CDP_1 + dbl_CDP_2), 1)
                            drRep.Item("N Potenza Gestita Kw") = dblLIPotenzaGestitaKw
                            drRep.Item("N Performance Ratio (%)") = Math.Round(dblPR, 1)
                            drRep.Item("N HG") = Math.Round(dblHG, 1)
                            drRep.Item("N Media Solarimetri(W/mq)") = Math.Round(dblMS, 1)
                            drRep.Item("N Media Temp. Pann.(°C)") = Math.Round(dblMT, 1)
                            drRep.Item("N Soglia W/mq Calcolo Medie") = Math.Round(dblS, 1)
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        ds.Dispose()
        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return dtRep

    End Function

    Public Function PrelevaDatiDiControlloSemplificatiDiTuttiGliImpiantiAsString(ByVal dtSTART As Date, ByVal dtSTOP As Date) As String

        Dim strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti As String = ""

        Dim dtRep As New DataTable

        Try
            dtRep = PrelevaDatiDiControlloSemplificatiDiTuttiGliImpiantiAsDT(dtSTART, dtSTOP)
            For Each dr As DataRow In dtRep.Select("", dtRep.Columns(0).ColumnName + "," + dtRep.Columns(8).ColumnName)

                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(0).Caption() + ": " + dr.Item(0).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(1).Caption() + ": " + dr.Item(1).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(2).Caption() + ": " + dr.Item(2).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(3).Caption() + ": " + dr.Item(3).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(4).Caption() + ": " + dr.Item(4).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(5).Caption() + ": " + dr.Item(5).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(6).Caption() + ": " + dr.Item(6).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(7).Caption() + ": " + dr.Item(7).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(8).Caption() + ": " + dr.Item(8).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(9).Caption() + ": " + dr.Item(9).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(10).Caption() + ": " + dr.Item(10).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + dtRep.Columns(11).Caption() + ": " + dr.Item(11).ToString + "; "
                strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + vbCrLf + vbCrLf

            Next dr

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + vbCrLf + vbCrLf
        strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + vbCrLf + vbCrLf
        strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + "Nr totale di impianti in questo Report: " + dtRep.Rows.Count().ToString()
        strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti = strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti + vbCrLf + vbCrLf

        Return strPrelevaDatiDiControlloSemplificatiDiTuttiGliImpianti

    End Function

    Public Sub SetLIIDLogByRemoteAddressValueAndTime(ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String)

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim dt As Date = Date.Now
        Dim dtStop As Date
        Dim ushTimeoutSecs As UShort
        Dim iNrDiRigheAggiornate As Integer

        Try

            'Apro la connessione
            CustomSQLConnectionOpen(cn, cmd)

            ' Ricavo Data ed ora dalla quale ricercare i valori
            ushTimeoutSecs = GetLIIDConfigValue(iLIID, 409)
            dtStop = dt.AddSeconds(-ushTimeoutSecs)

            strSQL = " SELECT * FROM [Log] "
            strSQL = strSQL + " WHERE LG_LI_ID = @LG_LI_ID AND LG_RemoteAddress = @LG_RemoteAddress  AND (LG_DataOra >= CONVERT(DATETIME, @LG_DataOra, 105)) "
            strSQL = strSQL + " ORDER BY LG_ID DESC "

            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_LI_ID", 0)
            cmd.Parameters.AddWithValue("@LG_RemoteAddress", strRemoteAddress)
            cmd.Parameters.AddWithValue("@LG_DataOra", dtStop)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each dr As DataRow In ds.Tables(0).Rows
                            Try

                                strSQL = " UPDATE Log "
                                strSQL = strSQL + " SET LG_LI_ID = @LG_LI_ID "
                                strSQL = strSQL + " WHERE LG_ID = @LG_ID "

                                'cmd.Connection = cn
                                cmd.CommandText = strSQL

                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@LG_LI_ID", iLIID)
                                cmd.Parameters.AddWithValue("@LG_ID", dr.Item("LG_ID"))

                                cmd.ExecuteNonQuery()

                                iNrDiRigheAggiornate = iNrDiRigheAggiornate + 1

                            Catch ex As Exception

                                ScriviLogEventi(iLIID, 0, AZIONE_MOD, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

                            End Try
                        Next

                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_OK, "L'aggiornamento dell' ID del Data Logger nel Log e' avvenuto con successo in Nr: " + iNrDiRigheAggiornate.ToString() + " Righe.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    Else
                        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_TI_GET_ID, RISULTATO_MODBUS_ERR, "Nessuna corrispondenza trovata per l'aggiornamento dell'ID del Data Logger Nel Log. ID Data Logger: " + iLIID.ToString() + " IP Remoto: " + strRemoteAddress + " Data ed Ora di Inizio Ricerca: " + dtStop.ToString(), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

    End Sub

    Public Function GetLIIDConfigValue(ByVal iLIID As Integer, ByVal iLPCID As Integer) As UShort
        Dim ushValue As UShort

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT LIPC_LPC_VAL_MEMO_DB "
            strSQL = strSQL + " FROM LoggerInstParamConfig "
            strSQL = strSQL + " INNER JOIN LoggerParamConfig ON LoggerInstParamConfig.LIPC_LPC_ID = LoggerParamConfig.LPC_ID "
            strSQL = strSQL + " WHERE LPC_ID = @LPC_ID "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIPC_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LPC_ID", iLPCID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ushValue = ds.Tables(0).Rows(0).Item("LIPC_LPC_VAL_MEMO_DB")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return ushValue

    End Function

    Public Function GetTotalLIIDConnectedFromLog(ByVal dtSTART As Date, ByVal dtSTOP As Date) As Integer
        Dim iValue As Integer

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT COUNT(DISTINCT LG_LI_ID) as NrTotal "
            strSQL = strSQL + " FROM Log "
            strSQL = strSQL + " WHERE (LG_DataOra >= CONVERT(DATETIME, @LG_DataOra_Start, 105) AND LG_DataOra <= CONVERT(DATETIME, @LG_DataOra_Stop, 105)) AND LG_LI_ID <> 0 AND LG_A_ID = 512 AND LG_R_ID = 11 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LG_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LG_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iValue = ds.Tables(0).Rows(0).Item("NrTotal")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return iValue

    End Function

    Public Function GetReportDettagli(ByVal iLIIDPCLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date) As String
        Dim strReturn As String = ""

        Dim strSQL As String
        Dim dsColumn As New DataSet
        Dim dsData As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim dtRepDett As New DataTable
        Dim iIndice_1 As Integer
        Dim strIngressoTipo() As String
        Dim drTemp As DataRow
        Dim dcTemp As DataColumn
        Dim str_1 As String

        Try
            ' Apro la connessione
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            ' Prima ricavo le colonne
            ' Indice Colonne:  0       1       2       3        4      5          6(Variabile)                  7
            strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, LIIDPC_IT_ID, LIIDPC_DataOra AS DataOra "
            strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Config "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID = ImpiantoDiProduzione.IDP_ID AND LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
            strSQL = strSQL + " INNER JOIN Cliente ON ImpiantoDiProduzione.IDP_C_ID = Cliente.C_ID "
            strSQL = strSQL + " WHERE LIIDPC_LI_ID = @LIIDPC_LI_ID AND LIIDPC_InserisciDettaglioNelReport = 1 "
            strSQL = strSQL + " ORDER BY LIIDPC_IT_ID "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", iLIIDPCLIID)

            da.SelectCommand = cmd
            da.Fill(dsColumn)
            If Not dsColumn Is Nothing Then
                If dsColumn.Tables.Count > 0 Then
                    If dsColumn.Tables(0).Rows.Count > 0 Then

                        ' Valori Fissi
                        dtRepDett.Columns.Add("C_ID")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("C_ID").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("C_ID").Caption = str_1
                        End If

                        dtRepDett.Columns.Add("C_Nome")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("C_Nome").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("C_Nome").Caption = str_1
                        End If

                        dtRepDett.Columns.Add("IDP_ID")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("IDP_ID").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("IDP_ID").Caption = str_1
                        End If

                        dtRepDett.Columns.Add("IDP_Nome")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("IDP_Nome").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("IDP_Nome").Caption = str_1
                        End If

                        dtRepDett.Columns.Add("LI_ID")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("LI_ID").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("LI_ID").Caption = str_1
                        End If

                        dtRepDett.Columns.Add("LI_Nr")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("LI_Nr").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("LI_Nr").Caption = str_1
                        End If

                        ' Valori Variabili
                        For Each dr As DataRow In dsColumn.Tables(0).Rows
                            If dtRepDett.Columns.Contains("ITID_" + dr.Item(6).ToString() + "_") = False Then
                                dcTemp = dtRepDett.Columns.Add("ITID_" + dr.Item(6).ToString() + "_")
                                str_1 = GENERICA_DESCRIZIONE("IT_Nome", "IngressoTipo", "IT_ID", dr.Item(6).ToString(), DEFAULT_OPERATOR_ID)
                                If Not str_1 Is Nothing Then
                                    dtRepDett.Columns("ITID_" + dr.Item(6).ToString() + "_").Caption = str_1
                                End If
                                dcTemp.DefaultValue() = 0.0
                            End If
                        Next

                        ' Valori Fissi
                        dtRepDett.Columns.Add("DataOra")
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("DataOra").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("DataOra").Caption = str_1
                        End If

                        dcTemp = dtRepDett.Columns.Add("SeparatoreDecimale_Format")
                        dcTemp.DefaultValue() = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("SeparatoreDecimale_Format").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("SeparatoreDecimale_Format").Caption = str_1
                        End If

                        dcTemp = dtRepDett.Columns.Add("DataOra_Format")
                        dcTemp.DefaultValue() = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + System.Globalization.DateTimeFormatInfo.CurrentInfo.LongTimePattern
                        str_1 = GENERICA_DESCRIZIONE("DCC_ColonnaNome", "DatabaseCampoColonna", "DCC_CampoNome", dtRepDett.Columns("DataOra_Format").ColumnName.ToString(), DEFAULT_OPERATOR_ID)
                        If Not str_1 Is Nothing Then
                            dtRepDett.Columns("DataOra_Format").Caption = str_1
                        End If

                        ' Poi inserisco i dati...
                        ' Indice Colonne:  0       1       2       3        4      5          6             7              8
                        strSQL = " SELECT C_ID, C_Nome, IDP_ID, IDP_Nome, LI_ID, LI_Nr, LIIDPC_IT_ID, LIIDPV_Valore, LIIDPV_DataOra AS DataOra "
                        strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
                        strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
                        strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
                        strSQL = strSQL + " INNER JOIN ImpiantoDiProduzione ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IDP_ID = ImpiantoDiProduzione.IDP_ID AND LoggerInst.LI_IDP_ID = ImpiantoDiProduzione.IDP_ID "
                        strSQL = strSQL + " INNER JOIN Cliente ON ImpiantoDiProduzione.IDP_C_ID = Cliente.C_ID "
                        strSQL = strSQL + " WHERE LIIDPC_LI_ID = @LIIDPC_LI_ID AND LIIDPC_InserisciDettaglioNelReport = 1 "
                        strSQL = strSQL + " AND (LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "
                        strSQL = strSQL + " ORDER BY LIIDPV_DataOra, LIIDPC_IT_ID "

                        cmd.CommandText = strSQL

                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@LIIDPC_LI_ID", iLIIDPCLIID)
                        cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
                        cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

                        da.SelectCommand = cmd
                        da.Fill(dsData)
                        If Not dsData Is Nothing Then
                            If dsData.Tables.Count > 0 Then
                                iIndice_1 = 0
                                strIngressoTipo = Array.CreateInstance(GetType(String), dtRepDett.Columns.Count)
                                For Each dr As DataRow In dsData.Tables(0).Rows
                                    ' Appena un qualsiasi valore si ripete, significa che la riga e' completa
                                    If strIngressoTipo.Contains(dr.Item(6).ToString()) = True Then
                                        Array.Clear(strIngressoTipo, 0, strIngressoTipo.Length)
                                        iIndice_1 = 0
                                    End If
                                    If iIndice_1 = 0 Then
                                        drTemp = dtRepDett.Rows.Add()
                                        drTemp.Item("C_ID") = dr.Item("C_ID")
                                        strIngressoTipo(iIndice_1) = "C_ID"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("C_Nome") = dr.Item("C_Nome")
                                        strIngressoTipo(iIndice_1) = "C_Nome"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("IDP_ID") = dr.Item("IDP_ID")
                                        strIngressoTipo(iIndice_1) = "IDP_ID"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("IDP_Nome") = dr.Item("IDP_Nome")
                                        strIngressoTipo(iIndice_1) = "IDP_Nome"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("LI_ID") = dr.Item("LI_ID")
                                        strIngressoTipo(iIndice_1) = "LI_ID"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("LI_Nr") = dr.Item("LI_Nr")
                                        strIngressoTipo(iIndice_1) = "LI_Nr"
                                        iIndice_1 = iIndice_1 + 1
                                        drTemp.Item("DataOra") = dr.Item("DataOra")
                                        strIngressoTipo(iIndice_1) = "DataOra"
                                        iIndice_1 = iIndice_1 + 1
                                    End If

                                    If drTemp.Table.Columns.Contains("ITID_" + dr.Item(6).ToString() + "_") = True Then
                                        drTemp.Item("ITID_" + dr.Item(6).ToString() + "_") = dr.Item(7)
                                    End If

                                    strIngressoTipo(iIndice_1) = dr.Item(6).ToString()
                                    iIndice_1 = iIndice_1 + 1
                                Next

                                ' Prendo i Dati e li formatto in una stringa
                                strReturn = ""
                                strReturn = strReturn + "HEADER_START"
                                strReturn = strReturn + vbCrLf
                                For Each dc As DataColumn In dtRepDett.Columns
                                    strReturn = strReturn + dc.ColumnName.ToString + ";"
                                Next
                                strReturn = strReturn + vbCrLf
                                For Each dc As DataColumn In dtRepDett.Columns
                                    strReturn = strReturn + dc.Caption.ToString + ";"
                                Next
                                strReturn = strReturn + vbCrLf
                                strReturn = strReturn + "HEADER_END"
                                strReturn = strReturn + vbCrLf
                                strReturn = strReturn + "DATA_START"
                                strReturn = strReturn + vbCrLf
                                For Each dc As DataColumn In dtRepDett.Columns
                                    strReturn = strReturn + dc.ColumnName.ToString + ";"
                                Next
                                strReturn = strReturn + vbCrLf
                                For Each dr As DataRow In dtRepDett.Rows
                                    For Each obj As Object In dr.ItemArray
                                        strReturn = strReturn + obj.ToString + ";"
                                    Next
                                    strReturn = strReturn + vbCrLf
                                Next
                                strReturn = strReturn + "DATA_END"

                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIIDPCLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        dsColumn.Dispose()
        dsData.Dispose()

        Return strReturn

    End Function

    Public Function GetEnergiaCDC_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 151) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaCDP_1_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 152) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaCDP_2_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 153) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaSI_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 155) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaSTR_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 157) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function GetEnergiaINV_ByLIID(ByVal iLIID As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, ByRef dblValueResult As Double) As Boolean
        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT SUM(LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_Valore) AS VALUE "
            strSQL = strSQL + " FROM LoggerInst "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst.LI_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Valore ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID "
            strSQL = strSQL + " WHERE (LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_IT_ID = 156) AND (LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105) AND LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra < CONVERT(DATETIME, @LIIDPV_DataOra_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtSTART)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Stop", dtSTOP)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("VALUE") Is DBNull.Value Then
                            dblValueResult = Math.Round(ds.Tables(0).Rows(0).Item("VALUE"), 1)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function PrelevaDataUltimiValoriStatisticiMemorizzati(ByVal iLIID As Integer, ByRef dt As Date) As Boolean

        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try

            strSQL = " SELECT TOP 1 LISM_Data FROM [LoggerInstStatMemoDay] "
            strSQL = strSQL + " WHERE LISM_LI_ID = @LISM_LI_ID "
            strSQL = strSQL + " ORDER BY LISM_Data DESC "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LISM_LI_ID", iLIID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0).Rows(0).Item("LISM_Data")
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function SetStatMemoDay(ByVal iLIID As Integer, ByVal dblCDC As Double, ByVal dblCDP_1 As Double, ByVal dblCDP_2 As Double, ByVal dblSI As Double, ByVal dblSTR As Double, ByVal dblINV As Double, ByVal dblPR As Double, ByVal dblHG As Double, ByVal dblMS As Double, ByVal dblMT As Double, ByVal dblS As Double, ByVal dt As Date) As Boolean
        Dim bRes As Boolean

        Dim strSQL As String
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand

        Try
            strSQL = " INSERT INTO [LoggerInstStatMemoDay] "
            strSQL = strSQL + "  (LISM_LI_ID,  LISM_CDC,  LISM_CDP_1,  LISM_CDP_2,  LISM_SI,  LISM_STR,  LISM_INV,  LISM_PR,  LISM_HG,  LISM_MS,  LISM_MT,  LISM_S,  LISM_Data ) VALUES "
            strSQL = strSQL + "  (@LISM_LI_ID, @LISM_CDC, @LISM_CDP_1, @LISM_CDP_2, @LISM_SI, @LISM_STR, @LISM_INV, @LISM_PR, @LISM_HG, @LISM_MS, @LISM_MT, @LISM_S, @LISM_Data) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LISM_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LISM_CDC", dblCDC)
            cmd.Parameters.AddWithValue("@LISM_CDP_1", dblCDP_1)
            cmd.Parameters.AddWithValue("@LISM_CDP_2", dblCDP_2)
            cmd.Parameters.AddWithValue("@LISM_SI", dblSI)
            cmd.Parameters.AddWithValue("@LISM_STR", dblSTR)
            cmd.Parameters.AddWithValue("@LISM_INV", dblINV)
            cmd.Parameters.AddWithValue("@LISM_PR", dblPR)
            cmd.Parameters.AddWithValue("@LISM_HG", dblHG)
            cmd.Parameters.AddWithValue("@LISM_MS", dblMS)
            cmd.Parameters.AddWithValue("@LISM_MT", dblMT)
            cmd.Parameters.AddWithValue("@LISM_S", dblS)
            cmd.Parameters.AddWithValue("@LISM_Data", dt)

            cmd.ExecuteNonQuery()

            bRes = True

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        Return bRes

    End Function

    Public Function GetStatMemoDay(ByVal iLIID As Integer, ByRef dblCDC As Double, ByRef dblCDP_1 As Double, ByRef dblCDP_2 As Double, ByRef dblSI As Double, ByRef dblSTR As Double, ByRef dblINV As Double, ByRef dblPR As Double, ByRef dblHG As Double, ByRef dblMS As Double, ByRef dblMT As Double, ByRef dblS As Double, ByVal dtStart As Date, ByVal dtStop As Date) As Boolean

        Dim bRes As Boolean = False

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            ' Inizializzo i dati
            dblCDC = 0.0
            dblCDP_1 = 0.0
            dblCDP_2 = 0.0
            dblSI = 0.0
            dblSTR = 0.0
            dblINV = 0.0
            dblPR = 0.0
            dblHG = 0.0
            dblMS = 0.0
            dblMT = 0.0
            dblS = 0.0

            strSQL = " SELECT SUM(LISM_CDC) AS dblCDC, SUM(LISM_CDP_1) AS dblCDP_1, SUM(LISM_CDP_2) AS dblCDP_2, SUM(LISM_SI) AS dblSI, SUM(LISM_STR) AS dblSTR, SUM(LISM_INV) AS dblINV, AVG(LISM_PR) AS dblPR, AVG(LISM_HG) AS dblHG, AVG(LISM_MS) AS dblMS, AVG(LISM_MT) AS dblMT, AVG(LISM_S) AS dblS "
            strSQL = strSQL + " FROM [LoggerInstStatMemoDay] "
            strSQL = strSQL + " WHERE (LISM_LI_ID = @LISM_LI_ID) AND (LISM_Data >= CONVERT(DATETIME, @LISM_Data_Start, 105) AND LISM_Data < CONVERT(DATETIME, @LISM_Data_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LISM_LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LISM_Data_Start", dtStart)
            cmd.Parameters.AddWithValue("@LISM_Data_Stop", dtStop)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0).Item("dblCDC") Is DBNull.Value Then
                            ' Prendo come riferimento il primo. Se il primo e' NULL, lo sono anche gli altri, ovvero non c'e' nessun dato da prelevare
                            bRes = True

                            dblCDC = ds.Tables(0).Rows(0).Item("dblCDC")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblCDP_1") Is DBNull.Value Then
                            dblCDP_1 = ds.Tables(0).Rows(0).Item("dblCDP_1")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblCDP_2") Is DBNull.Value Then
                            dblCDP_2 = ds.Tables(0).Rows(0).Item("dblCDP_2")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblSI") Is DBNull.Value Then
                            dblSI = ds.Tables(0).Rows(0).Item("dblSI")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblSTR") Is DBNull.Value Then
                            dblSTR = ds.Tables(0).Rows(0).Item("dblSTR")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblINV") Is DBNull.Value Then
                            dblINV = ds.Tables(0).Rows(0).Item("dblINV")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblPR") Is DBNull.Value Then
                            dblPR = ds.Tables(0).Rows(0).Item("dblPR")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblHG") Is DBNull.Value Then
                            dblHG = ds.Tables(0).Rows(0).Item("dblHG")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblMS") Is DBNull.Value Then
                            dblMS = ds.Tables(0).Rows(0).Item("dblMS")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblMT") Is DBNull.Value Then
                            dblMT = ds.Tables(0).Rows(0).Item("dblMT")
                        End If
                        If Not ds.Tables(0).Rows(0).Item("dblS") Is DBNull.Value Then
                            dblS = ds.Tables(0).Rows(0).Item("dblS")
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    Public Function PrelevaDataPrimiValoriDataloggerMemorizzati(ByVal iLIID As Integer, ByVal dtStartInizioRicerca As Date, ByRef dt As Date) As Boolean

        Dim bRes As Boolean = True

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn

            strSQL = " Select TOP 1 LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra "
            strSQL = strSQL + " FROM LoggerInst_X_ImpiantoDiProduzione_X_Valore "
            strSQL = strSQL + " INNER JOIN LoggerInst_X_ImpiantoDiProduzione_X_Config ON LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_LIIDPC_ID = LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_ID "
            strSQL = strSQL + " INNER JOIN LoggerInst ON LoggerInst_X_ImpiantoDiProduzione_X_Config.LIIDPC_LI_ID = LoggerInst.LI_ID "
            strSQL = strSQL + " WHERE (LoggerInst.LI_ID = @LI_ID) AND (LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra >= CONVERT(DATETIME, @LIIDPV_DataOra_Start, 105)) "
            strSQL = strSQL + " ORDER BY LoggerInst_X_ImpiantoDiProduzione_X_Valore.LIIDPV_DataOra ASC "

            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@LI_ID", iLIID)
            cmd.Parameters.AddWithValue("@LIIDPV_DataOra_Start", dtStartInizioRicerca)

            da.SelectCommand = cmd
            ds.Clear()
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0).Rows(0).Item("LIIDPV_DataOra")
                    End If
                End If
            End If

        Catch ex As Exception
            bRes = False
            ScriviLogEventi(iLIID, 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

    'Public Sub ModbusSetCRC16(ByRef m_lbyteBuffer As List(Of Byte))
    '    Static abyteCRCHi() As Byte = {&H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, _
    '                                  &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
    '                                  &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40}

    '    Static abyteCRCLo() As Byte = {&H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4, _
    '                                  &H4, &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, _
    '                                  &H8, &HC8, &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, _
    '                                  &H1D, &H1C, &HDC, &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, _
    '                                  &H11, &HD1, &HD0, &H10, &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, _
    '                                  &H37, &HF5, &H35, &H34, &HF4, &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, _
    '                                  &H3B, &HFB, &H39, &HF9, &HF8, &H38, &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, _
    '                                  &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C, &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, _
    '                                  &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0, &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, _
    '                                  &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4, &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, _
    '                                  &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68, &H78, &HB8, &HB9, &H79, &HBB, _
    '                                  &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C, &HB4, &H74, &H75, &HB5, _
    '                                  &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0, &H50, &H90, &H91, _
    '                                  &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54, &H9C, &H5C, _
    '                                  &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98, &H88, _
    '                                  &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C, _
    '                                  &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, _
    '                                  &H40}
    '    Dim byteCRCHi As Byte = &HFF
    '    Dim byteCRCLo As Byte = &HFF
    '    Dim iIndiceCRC As Integer
    '    Dim iIndice_1 As Integer
    '    Dim iCount As Integer

    '    iIndice_1 = 0
    '    For iCount = m_lbyteBuffer.Count() - 1 To 0 Step -1
    '        iIndiceCRC = byteCRCLo Xor m_lbyteBuffer(iIndice_1)
    '        byteCRCLo = byteCRCHi Xor abyteCRCHi(iIndiceCRC)
    '        byteCRCHi = abyteCRCLo(iIndiceCRC)
    '        iIndice_1 = iIndice_1 + 1
    '    Next
    '    m_lbyteBuffer.Add(byteCRCLo)
    '    m_lbyteBuffer.Add(byteCRCHi)

    'End Sub

    'Public Function ModbusCheckCRC16(ByRef m_lbyteBuffer As List(Of Byte), ByVal iLIID As Integer, ByVal strLocalAddress As String, ByVal strRemoteAddress As String) As Boolean
    '    Dim bCRCOk As Boolean

    '    Static abyteCRCHi() As Byte = {&H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, _
    '                                  &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
    '                                  &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
    '                                  &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
    '                                  &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
    '                                  &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
    '                                  &H40}

    '    Static abyteCRCLo() As Byte = {&H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4, _
    '                                  &H4, &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, _
    '                                  &H8, &HC8, &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, _
    '                                  &H1D, &H1C, &HDC, &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, _
    '                                  &H11, &HD1, &HD0, &H10, &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, _
    '                                  &H37, &HF5, &H35, &H34, &HF4, &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, _
    '                                  &H3B, &HFB, &H39, &HF9, &HF8, &H38, &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, _
    '                                  &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C, &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, _
    '                                  &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0, &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, _
    '                                  &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4, &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, _
    '                                  &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68, &H78, &HB8, &HB9, &H79, &HBB, _
    '                                  &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C, &HB4, &H74, &H75, &HB5, _
    '                                  &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0, &H50, &H90, &H91, _
    '                                  &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54, &H9C, &H5C, _
    '                                  &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98, &H88, _
    '                                  &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C, _
    '                                  &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, _
    '                                  &H40}
    '    Dim byteCRCHi As Byte = &HFF
    '    Dim byteCRCLo As Byte = &HFF
    '    Dim iIndiceCRC As Integer
    '    Dim iIndice_1 As Integer
    '    Dim iCount As Integer

    '    If m_lbyteBuffer.Count() > 2 Then
    '        iIndice_1 = 0
    '        For iCount = (m_lbyteBuffer.Count() - 1 - 2) To 0 Step -1
    '            iIndiceCRC = byteCRCLo Xor m_lbyteBuffer(iIndice_1)
    '            byteCRCLo = byteCRCHi Xor abyteCRCHi(iIndiceCRC)
    '            byteCRCHi = abyteCRCLo(iIndiceCRC)
    '            iIndice_1 = iIndice_1 + 1
    '        Next
    '        If m_lbyteBuffer(m_lbyteBuffer.Count() - 1) = byteCRCHi And m_lbyteBuffer(m_lbyteBuffer.Count() - 2) = byteCRCLo Then
    '            bCRCOk = True
    '            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_CHECK_CRC16, RISULTATO_MODBUS_OK, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
    '        Else
    '            ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_CHECK_CRC16, RISULTATO_MODBUS_ERR, "", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
    '        End If
    '    Else
    '        ScriviLogEventi(iLIID, 0, AZIONE_MODBUS_CHECK_CRC16, RISULTATO_MODBUS_ERR, "Numero caratteri ricevuti troppo basso.", strLocalAddress, strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
    '    End If

    '    Return bCRCOk
    'End Function

    'Public Sub ApriDataSet(ByVal iTID As Integer, ByRef cmd As SqlCommand, ByRef ds As DataSet, ByVal iUID As Integer)
    '    If Not ds Is Nothing Then
    '        Try
    '            Dim da As New SqlDataAdapter(cmd)
    '            If ds.Tables.Count > 0 Then
    '                ds.Tables.Clear()
    '            End If
    '            da.Fill(ds)
    '            da.Dispose()
    '        Catch ex As Exception
    '            ScriviLogEventi(iTID, AZIONE_READ, RISULTATO_ERR, ex.Message+ " "+ex.InnerException.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), iUID)
    '        End Try
    '    End If
    'End Sub

    'Public Sub ApriDataSet(ByVal strSQL As String, ByRef ds As DataSet, ByRef cn As SqlConnection, ByRef txn As SqlTransaction, ByVal strConnectionString As String)
    '    If Not ds Is Nothing Then
    '        Try
    '            Dim cmd As New SqlCommand(strSQL, cn, txn)
    '            Dim da As New SqlDataAdapter(cmd)
    '            If ds.Tables.Count > 0 Then
    '                ds.Tables.Clear()
    '            End If
    '            da.Fill(ds)
    '            da.Dispose()
    '            cmd.Dispose()
    '        Catch ex As Exception
    '            ScriviLogEventi(ex.Message+ " "+ex.InnerException.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), LOG_EVENTI_VB, strConnectionString)
    '        End Try
    '    End If
    'End Sub

    'Public Sub WriteWindowsEventLog(ByVal strLogValue As String)
    '    Static bEventLogCreated As Boolean
    '    Try

    '        If Not EventLog.SourceExists("CepiPLCService") Then
    '            ' Create the source, if it does not already exist.
    '            ' An event log source should not be created and immediately used.
    '            ' There is a latency time to enable the source, it should be created
    '            ' prior to executing the application that uses the source.
    '            ' Execute this sample a second time to use the new source.
    '            EventLog.CreateEventSource("CepiPLCService", "")
    '            'The source is created.  Exit the application to allow it to be registered.
    '            bEventLogCreated = True
    '            Return
    '        End If

    '        If EventLog.SourceExists("CepiPLCService") Then
    '            If bEventLogCreated = True Then
    '                bEventLogCreated = False
    '                Dim el As New EventLog()
    '                el.Source = "CepiPLCService"
    '                el.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1)
    '            End If


    '            ' Create an EventLog instance and assign its source.
    '            Dim myLog As New EventLog()
    '            myLog.Source = "CepiPLCService"
    '            ' Write an informational entry to the event log.    
    '            myLog.WriteEntry(strLogValue)
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

End Module
