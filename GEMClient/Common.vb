Imports System.Data.SqlClient

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

    Public Const AZIONE_MODBUS_TI_START_CONNECTION As UShort = 101
    Public Const AZIONE_MODBUS_TI_ACCEPT_CONNECTION As UShort = 105
    Public Const AZIONE_MODBUS_TI_CLOSE_CONNECTION As UShort = 111

    Public Const AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC As UShort = 201
    Public Const AZIONE_MODBUS_TI_GET_ID As UShort = 501
    Public Const AZIONE_MODBUS_TI_GET_LAST_UDT As UShort = 503
    Public Const AZIONE_MODBUS_TI_GET_NR_MAX_UDT As UShort = 504
    Public Const AZIONE_MODBUS_TI_GET_CONN_WAY As UShort = 507
    Public Const AZIONE_MODBUS_TI_GET_TOTALS As UShort = 510
    Public Const AZIONE_MODBUS_TI_GET_ALL_STORED_DATA As UShort = 512
    Public Const AZIONE_MODBUS_TI_RESET_CALL_FLAG As UShort = 515

    Public Const AZIONE_MODBUS_TI_GET_CONFIG_PROC As UShort = 521
    Public Const AZIONE_MODBUS_TI_GET_CONFIG As UShort = 1000
    Public Const AZIONE_MODBUS_TI_GET_CONFIG_CODE_MIN_VALUE As UShort = 1001
    Public Const AZIONE_MODBUS_TI_GET_CONFIG_CODE_MAX_VALUE As UShort = 1999

    Public Const AZIONE_MODBUS_TI_SET_CONFIG_PROC As UShort = 531
    Public Const AZIONE_MODBUS_TI_SET_CONFIG As UShort = 2000
    Public Const AZIONE_MODBUS_TI_SET_CONFIG_CODE_MIN_VALUE As UShort = 2001
    Public Const AZIONE_MODBUS_TI_SET_CONFIG_CODE_MAX_VALUE As UShort = 2999

    Public Const AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC As UShort = 541
    Public Const AZIONE_MODBUS_TI_GET_ID_SINCR_DB_DL_DATA As UShort = 551
    Public Const AZIONE_MODBUS_TI_GET_LAST_UDT_SINCR_DB_DL_DATA As UShort = 552
    Public Const AZIONE_MODBUS_TI_GET_STORED_DATA_SINCR_DB_DL_DATA As UShort = 553
    Public Const AZIONE_MODBUS_TI_RESET_CALL_FLAG_SINCR_DB_DL_DATA As UShort = 554

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

        If String.IsNullOrEmpty(strDescrizione) Then
            strDescrizione = " "
        End If

        Try
            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn

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

            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()
            ds.Dispose()

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
    End Sub

    Public Function GetRowNrErrorInStackTrace(ByVal strStackTrace As String) As String

        Return Split(strStackTrace, vbCrLf)(UBound(Split(strStackTrace, vbCrLf)))

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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return strLIPCLPCDESCRMEMODB

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
            cmd.Connection = cn
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
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return obj

    End Function


    'Public Function GetIrraggiamento(ByVal iIDPID As Integer, ByVal iLIID As Integer, ByVal bDisableParameterLIID As Boolean, ByVal iITID_1 As Integer, ByVal iITID_2 As Integer, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal strType As String = "SUM", Optional ByVal iDigits As Integer = 0) As Double
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
    '        strSQL = strSQL + " WHERE (ImpiantoDiProduzione.IDP_ID = @IDP_ID) AND ((IngressoTipo.IT_ID = @IT_ID_1) OR (IngressoTipo.IT_ID = @IT_ID_2)) "
    '        If bDisableParameterLIID = False Then
    '            strSQL = strSQL + " AND (LoggerInst.LI_ID = " + iLIID.ToString() + ") "
    '        End If
    '        strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "

    '        CustomSQLConnectionOpen(cn, cmd)
    '        cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@IDP_ID", iIDPID)
    '        cmd.Parameters.AddWithValue("@IT_ID_1", iITID_1)
    '        cmd.Parameters.AddWithValue("@IT_ID_2", iITID_2)
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
    '        ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
    '    End Try

    '    da.Dispose()
    '    cmd.Dispose()
    '    cn.Close()
    '    cn.Dispose()
    '    ds.Dispose()

    '    Return Math.Round(dbValue, iDigits)

    'End Function

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
    '        cmd.Connection = cn
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return Math.Round(dbValue, iDigits)

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
            cmd.Connection = cn

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

    Public Function GetDGI(ByRef DGIID As Integer, ByRef objDGIValore As Object) As Boolean
        Dim bRes As Boolean

        Dim objIn As Object
        Dim stTypeIn As System.Type

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        objIn = objDGIValore
        stTypeIn = objDGIValore.GetType()
        Try

            strSQL = " SELECT * FROM [DatiGeneraliImpianto] "
            strSQL = strSQL + " WHERE DGI_ID = @DGI_ID "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
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


        Catch ex As Exception
            bRes = False
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Try
            objDGIValore = Convert.ChangeType(objDGIValore, stTypeIn)
        Catch ex As Exception
            objDGIValore = objIn
        End Try

        Return bRes

    End Function

    'Public Function GetHj(ByVal iLIID As Integer, ByVal dblNrSolarimetri As Double, ByVal dblValoreUDTMinuti As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal iDigits As Integer = 3) As Double
    '    Dim dbValue As Double

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter

    '    Try
    '        strSQL = " SELECT SUM( (ITIIFIV_Valore * @dblValoreUDTMinuti) / (60000.0 * @dblNrSolarimetri) ) AS VALUE_Hj "
    '        'strSQL = strSQL + " FROM LoggerInst "
    '        'strSQL = strSQL + " INNER JOIN InverterTesterInst ON LoggerInst.LI_ID = InverterTesterInst.ITI_LI_ID "
    '        strSQL = strSQL + " FROM InverterTesterInst "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
    '        'strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
    '        'strSQL = strSQL + " WHERE (LoggerInst.LI_ID = @LI_ID) AND (IngressoTipo.IT_ID = 415) "
    '        strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
    '        strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
    '        CustomSQLConnectionOpen(cn, cmd)
    '        cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@dblNrSolarimetri", dblNrSolarimetri)
    '        cmd.Parameters.AddWithValue("@dblValoreUDTMinuti", dblValoreUDTMinuti)
    '        cmd.Parameters.AddWithValue("@LI_ID", iLIID)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    If Not ds.Tables(0).Rows(0).Item("VALUE_Hj") Is DBNull.Value Then
    '                        dbValue = ds.Tables(0).Rows(0).Item("VALUE_Hj")
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

    'Public Function GetHG(ByVal iLIID As Integer, ByVal dblNrSolarimetri As Double, ByVal dblValoreUDTMinuti As Double, ByVal dblSoglia As Double, ByVal dtSTART As Date, ByVal dtSTOP As Date, Optional ByVal iDigits As Integer = 3) As Double
    '    Dim dbValue As Double

    '    Dim strSQL As String
    '    Dim ds As New DataSet
    '    Dim cn As New SqlConnection(My.Settings.ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter

    '    Try
    '        strSQL = " SELECT SUM(VALUE_HG_TEMP) AS VALUE_HG "
    '        strSQL = strSQL + " FROM ( "

    '        strSQL = strSQL + " SELECT SUM( (ITIIFIV_Valore * @dblValoreUDTMinuti) / (60000.0 * @dblNrSolarimetri) ) AS VALUE_HG_TEMP "
    '        'strSQL = strSQL + " FROM LoggerInst "
    '        'strSQL = strSQL + " INNER JOIN InverterTesterInst ON LoggerInst.LI_ID = InverterTesterInst.ITI_LI_ID "
    '        strSQL = strSQL + " FROM InverterTesterInst "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Config ON InverterTesterInst.ITI_ID = InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ITI_ID "
    '        strSQL = strSQL + " INNER JOIN InverterTesterInst_X_InverterFotovInst_X_Valore ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_ID = InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_ITIIFIC_ID "
    '        'strSQL = strSQL + " INNER JOIN IngressoTipo ON InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = IngressoTipo.IT_ID "
    '        'strSQL = strSQL + " WHERE (LoggerInst.LI_ID = @LI_ID) AND (IngressoTipo.IT_ID = 415) "
    '        strSQL = strSQL + " WHERE (InverterTesterInst.ITI_LI_ID = @LI_ID) AND (InverterTesterInst_X_InverterFotovInst_X_Config.ITIIFIC_IT_ID = 415) "
    '        strSQL = strSQL + " AND (InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra >= CONVERT(DATETIME, @ITIIFIV_DataOra_Start, 105) AND InverterTesterInst_X_InverterFotovInst_X_Valore.ITIIFIV_DataOra <= CONVERT(DATETIME, @ITIIFIV_DataOra_Stop, 105)) "
    '        strSQL = strSQL + " GROUP BY ITIIFIV_UDT "
    '        strSQL = strSQL + " HAVING (SUM (ITIIFIV_Valore) > @dblSoglia) "

    '        strSQL = strSQL + " ) AS TBL_Temp "

    '        CustomSQLConnectionOpen(cn, cmd)
    '        cmd.Connection = cn
    '        cmd.CommandText = strSQL

    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@dblNrSolarimetri", dblNrSolarimetri)
    '        cmd.Parameters.AddWithValue("@dblValoreUDTMinuti", dblValoreUDTMinuti)
    '        cmd.Parameters.AddWithValue("@LI_ID", iLIID)
    '        cmd.Parameters.AddWithValue("@dblSoglia", dblSoglia)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Start", dtSTART)
    '        cmd.Parameters.AddWithValue("@ITIIFIV_DataOra_Stop", dtSTOP)

    '        da.SelectCommand = cmd
    '        da.Fill(ds)

    '        If Not ds Is Nothing Then
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    If Not ds.Tables(0).Rows(0).Item("VALUE_HG") Is DBNull.Value Then
    '                        dbValue = ds.Tables(0).Rows(0).Item("VALUE_HG")
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

    Public Function GetStatMemoDay(ByVal iLIID As Integer, ByRef dblCDC As Double, ByRef dblCDP_1 As Double, ByRef dblCDP_2 As Double, ByRef dblSI As Double, ByRef dblSTR As Double, ByRef dblINV As Double, ByRef dblPR As Double, ByRef dblHG As Double, ByRef dblMS As Double, ByRef dblMT As Double, ByRef dblS As Double, ByVal dtStart As Date, ByVal dtStop As Date) As Boolean

        Dim bRes As Boolean = False

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Try
            strSQL = " SELECT SUM(LISM_CDC) AS dblCDC, SUM(LISM_CDP_1) AS dblCDP_1, SUM(LISM_CDP_2) AS dblCDP_2, SUM(LISM_SI) AS dblSI, SUM(LISM_STR) AS dblSTR, SUM(LISM_INV) AS dblINV, AVG(LISM_PR) AS dblPR, AVG(LISM_HG) AS dblHG, AVG(LISM_MS) AS dblMS, AVG(LISM_MT) AS dblMT, AVG(LISM_S) AS dblS "
            strSQL = strSQL + " FROM [LoggerInstStatMemoDay] "
            strSQL = strSQL + " WHERE (LISM_LI_ID = @LISM_LI_ID) AND (LISM_Data >= CONVERT(DATETIME, @LISM_Data_Start, 105) AND LISM_Data < CONVERT(DATETIME, @LISM_Data_Stop, 105)) "

            CustomSQLConnectionOpen(cn, cmd)
            cmd.Connection = cn
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

                        bRes = True
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

        Return bRes

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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        ds.Dispose()

        Return bRes

    End Function

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
            cmd.Connection = cn
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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
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
            cmd.Connection = cn

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
            ScriviLogEventi(iLIID, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing)
        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()
        dsUDT.Dispose()
        dsTMP.Dispose()

        Return bRes

    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint:="OpenClipboard", SetLastError:=True)> _
          Public Function OpenClipboard(ByVal hwnd As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint:="EmptyClipboard", SetLastError:=True)> _
          Public Function EmptyClipboard() As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint:="CloseClipboard", SetLastError:=True)> _
        Public Function CloseClipboard() As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint:="IsClipboardFormatAvailable", SetLastError:=True)> _
       Public Function IsClipboardFormatAvailable(ByVal uint As UInteger) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint:="GetClipboardData", SetLastError:=True)> _
       Public Function GetClipboardData(ByVal uint As UInteger) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("Kernel32.dll", EntryPoint:="GetLastError", SetLastError:=True)> _
           Public Function GetLastError() As UInteger
    End Function

End Module
