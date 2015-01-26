'Public Class TCPIPClientEventArgs
'    Inherits EventArgs

'    Private m_iLIID As Integer
'    Private m_sTCPIPAddrAndPort As String
'    Private m_iResult As Integer
'    Private m_strMessage As String

'    'Constructor.
'    Public Sub New(ByVal iLIID As Integer, ByVal strTCPIPAddrAndPort As String, ByVal iResult As Integer, ByVal strMessage As String)
'        Me.m_iLIID = iLIID
'        Me.m_sTCPIPAddrAndPort = strTCPIPAddrAndPort
'        Me.m_iResult = iResult
'        Me.m_strMessage = strMessage
'    End Sub

'    'Properties.
'    Public ReadOnly Property LIID() As String
'        Get
'            Return m_iLIID
'        End Get
'    End Property

'    Public ReadOnly Property TCPIPAddrAndPort() As String
'        Get
'            Return m_sTCPIPAddrAndPort
'        End Get
'    End Property

'    Public ReadOnly Property Result() As String
'        Get
'            Return m_iResult
'        End Get
'    End Property

'    Public ReadOnly Property Message() As String
'        Get
'            Return m_strMessage
'        End Get
'    End Property

'End Class

'Public Delegate Sub TCPIPClientEventHandler(ByVal sender As Object, ByVal e As TCPIPClientEventArgs)

Public Class TCPIPClient

    Private m_iLIID As Integer
    Dim m_strLocalAddress As String = "---.---.---.---:XXXXX"
    Dim m_strRemoteAddress As String = "---.---.---.---:XXXXX"

    Private m_iModoConnessione As Integer
    Private m_bConnessioneDaDatalogger As Boolean
    Private m_shCausaConnessioneDaDatalogger As UShort
    Private m_iIndiceParametro As Integer
    Private m_dtLoggerParamConfigValore As DataTable
    Private m_bSalvaLogModbus As Boolean = True

    Private m_ushNrMaxUDT As UShort
    Private m_ushUltimoUDTElaboratoNelDatalogger As UShort
    Private m_ushUltimoUDTMemorizzatoOk As UShort
    Private m_ushUDTDaRichiedere As UShort
    Private m_bIncrementaUDT As Boolean
    Private m_bIncrementaRecord As Boolean
    Private m_ushRecordStart As UShort
    Private m_ushRecordNr As UShort
    Private m_bRecordAcquisito As Boolean
    Private m_bAlmenoUnRecordGiaPresente As Boolean
    Private m_iRetryToGetNextUDT As Integer
    'Private m_strLogFileName As String = "ModbusLog_"
    Private m_strLogFileName As String = "ModbusLog_"
    Private m_strLogFileDirectoty As String = My.Application.Info.DirectoryPath.ToString() + "\" + "Logs_" + Date.Now.Year.ToString() + "_" + Date.Now.Month.ToString() + "_" + Date.Now.Day.ToString()

    Private m_tcTC As New Net.Sockets.TcpClient
    Private m_lbyteBufferSend As New List(Of Byte)
    Private m_lbyteBufferReceived As New List(Of Byte)
    Private WithEvents m_t As New Timer

    ' Variabili utilizzate dentro l'evento Timer
    Private m_iTickCount As Integer
    Private m_iTickCountTimeout As Integer = 120
    Private Const TIMER_INTERVAL As Integer = 500
    Private m_bTimeoutRetry As Boolean

    Private m_bClose As Boolean

    Property TC() As Net.Sockets.TcpClient
        Get
            Return m_tcTC
        End Get

        Set(ByVal TC As Net.Sockets.TcpClient)
            m_tcTC = TC

            ' Setto il timeout
            m_t.Interval = TIMER_INTERVAL
            m_t.Start()

        End Set
    End Property

    ' Sequenza Invio/lettura dati
    Private m_bStep_1_Req As Boolean = False
    Private m_bStep_1_Answ As Boolean = False
    Private m_bStep_2_Req As Boolean = False
    Private m_bStep_2_Answ As Boolean = False
    Private m_bStep_3_Req As Boolean = False
    Private m_bStep_3_Answ As Boolean = False
    Private m_bStep_4_Req As Boolean = False
    Private m_bStep_4_Answ As Boolean = False
    Private m_bStep_5_Req As Boolean = False
    Private m_bStep_5_Answ As Boolean = False
    Private m_bStep_6_Req As Boolean = False
    Private m_bStep_6_Answ As Boolean = False
    Private m_bStep_7_Req As Boolean = False
    Private m_bStep_7_Answ As Boolean = False
    Private m_bStep_8_Req As Boolean = False
    Private m_bStep_8_Answ As Boolean = False

    Private Sub InitData()
        m_shCausaConnessioneDaDatalogger = 0

        m_iIndiceParametro = 0

        m_ushNrMaxUDT = 0
        m_ushUltimoUDTElaboratoNelDatalogger = 0
        m_ushUltimoUDTMemorizzatoOk = 0
        m_ushUDTDaRichiedere = 0
        m_bIncrementaUDT = True
        m_bIncrementaRecord = False
        m_ushRecordStart = 1
        m_ushRecordNr = 6
        m_bRecordAcquisito = False
        m_bAlmenoUnRecordGiaPresente = False
        m_iRetryToGetNextUDT = 0

        m_iTickCount = 0
        m_bTimeoutRetry = False

        m_bStep_1_Req = False
        m_bStep_1_Answ = False
        m_bStep_2_Req = False
        m_bStep_2_Answ = False
        m_bStep_3_Req = False
        m_bStep_3_Answ = False
        m_bStep_4_Req = False
        m_bStep_4_Answ = False
        m_bStep_5_Req = False
        m_bStep_5_Answ = False
        m_bStep_6_Req = False
        m_bStep_6_Answ = False
        m_bStep_7_Req = False
        m_bStep_7_Answ = False
        m_bStep_8_Req = False
        m_bStep_8_Answ = False

    End Sub

    Public Sub BeginConnect(ByVal iLIID As Integer, ByVal strTCPIPHostAddr As String, ByVal iTCPIPHostPort As Integer, ByVal iModoConnessione As Integer, ByVal dtLoggerParamConfigValore As DataTable)

        m_iLIID = iLIID

        Try
            m_strRemoteAddress = strTCPIPHostAddr + ":" + iTCPIPHostPort.ToString()
        Catch ex As Exception
            m_strRemoteAddress = "---.---.---.---:XXXXX"
        End Try

        m_bConnessioneDaDatalogger = False

        m_iModoConnessione = iModoConnessione
        m_dtLoggerParamConfigValore = dtLoggerParamConfigValore

        ' Inizializzo
        InitData()

        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

        Try
            m_tcTC.Client.BeginConnect(strTCPIPHostAddr, iTCPIPHostPort, AddressOf OnBeginConnect, Nothing)
        Catch ex As Exception
            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, ex.Message, m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        m_t.Interval = TIMER_INTERVAL
        m_t.Start()

    End Sub

    Private Sub OnBeginConnect(ByVal result As IAsyncResult)
        Dim stra_1 As String()

        If Not m_tcTC Is Nothing Then

            Try

                m_tcTC.Client.EndConnect(result)

                Try
                    m_strLocalAddress = m_tcTC.Client.LocalEndPoint.ToString()
                Catch ex As Exception
                    m_strLocalAddress = "---.---.---.---:XXXXX"
                End Try

                Try
                    m_strRemoteAddress = m_tcTC.Client.RemoteEndPoint.ToString()
                Catch ex As Exception
                    m_strRemoteAddress = "---.---.---.---:XXXXX"
                End Try

                stra_1 = Split(m_strRemoteAddress, ":")
                m_strLogFileName = m_strLogFileName + "PC_DL_" + stra_1(0) + "_"

                ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_OK, "", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

            Catch ex As Exception

                'If Not m_tcTC Is Nothing Then
                '    m_tcTC.GetStream().Close()
                '    m_tcTC.Client.Close()
                '    m_tcTC.Close()
                '    'm_tcTC = Nothing
                'End If
                'Try
                '    If Not m_lbyteBufferSend Is Nothing Then
                '        m_lbyteBufferSend.Clear()
                '        m_lbyteBufferSend = Nothing
                '    End If
                'Catch ex_1 As Exception

                'End Try

                'Try
                '    If Not m_lbyteBufferReceived Is Nothing Then
                '        m_lbyteBufferReceived.Clear()
                '        m_lbyteBufferReceived = Nothing
                '    End If
                'Catch ex_1 As Exception

                'End Try

                ' Reset bit di aggiornamento in corso...
                If m_iLIID > 0 Then
                    If GetLIAutoAggInCorso(m_iLIID) = True Then
                        ' Devo aggiornare i dati ....
                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_AUTO_START_CONNECTION_RESET, RISULTATO_OK, "Operazione Eseguita Automaticamente.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                    SetLIAutoAggInCorso(m_iLIID, False)
                End If

                '' Per sicurezza provo ad eliminare tutto....
                'Try
                '    m_tcTC.GetStream().Close()
                'Catch ex_1 As Exception
                '    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " Stream -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                'End Try
                'Try
                '    m_tcTC.Client.Close()
                'Catch ex_1 As Exception
                '    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " Client -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                'End Try
                'Try
                '    m_tcTC.Close()
                'Catch ex_1 As Exception
                '    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " m_tcTC -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                'End Try
                ''Try
                ''    m_tcTC.GetStream().Dispose()
                ''Catch ex_1 As Exception
                ''    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " Stream -> Dispose: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                ''End Try
                'Try
                '    m_tcTC.Client = Nothing
                'Catch ex_1 As Exception
                '    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " Client -> Nothing: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                'End Try
                'Try
                '    m_tcTC = Nothing
                'Catch ex_1 As Exception
                '    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, " m_tcTC -> Nothing: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                'End Try

                ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_START_CONNECTION, RISULTATO_MODBUS_ERR, ex.Message, m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                m_bClose = True

            End Try

        End If

    End Sub

    Public Sub Accept(ByVal tcTCPIPWait As Net.Sockets.TcpClient)
        Dim stra_1 As String()

        m_bConnessioneDaDatalogger = True

        ' Leggo l'operazione da eseguire a questa connessione
        m_iModoConnessione = AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC

        ' Inizializzo
        InitData()

        m_tcTC = tcTCPIPWait

        Try
            m_strLocalAddress = m_tcTC.Client.LocalEndPoint.ToString()
        Catch ex As Exception
            m_strLocalAddress = "---.---.---.---:XXXXX"
        End Try

        Try
            m_strRemoteAddress = m_tcTC.Client.RemoteEndPoint.ToString()
        Catch ex As Exception
            m_strRemoteAddress = "---.---.---.---:XXXXX"
        End Try

        stra_1 = Split(m_strRemoteAddress, ":")
        m_strLogFileName = m_strLogFileName + "PC_DL_" + stra_1(0) + "_"
        
        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_ACCEPT_CONNECTION, RISULTATO_MODBUS_OK, "", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

        m_t.Interval = TIMER_INTERVAL + 5000
        m_t.Start()

    End Sub

    Public Sub Close()
        m_bClose = True
    End Sub

    Private Sub m_t_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_t.Tick
        Dim abyte_1() As Byte
        Dim sTI As Short
        Dim iPI As Integer
        Dim iLenght As Integer
        Dim iUI As Integer
        Dim iCodiceParametro As Integer
        Dim strValoreDato As String
        Dim usIndirizzoRegistroH As UShort
        Dim usIndirizzoRegistroL As UShort
        Dim strTestoAllarme As String = ""
        Dim strParam78 As String = ""

        Try

            m_t.Stop()
            m_t.Interval = TIMER_INTERVAL
            m_iTickCount = m_iTickCount + 1
            If Not m_tcTC Is Nothing Then
                If m_tcTC.Connected = True Then
                    If m_tcTC.GetStream().CanWrite = True Then

                        '------------------------------------------------------------
                        If m_iModoConnessione = AZIONE_MODBUS_TI_GET_CONFIG_PROC Then
                            If m_bStep_1_Req = False And m_bStep_1_Answ = False Then
                                m_bStep_1_Req = True

                                ' Reset Buffer
                                m_lbyteBufferSend.Clear()
                                m_lbyteBufferReceived.Clear()

                                If m_dtLoggerParamConfigValore.Rows.Count > m_iIndiceParametro Then

                                    m_iLIID = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LIPC_LI_ID")

                                    iCodiceParametro = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_ID")
                                    usIndirizzoRegistroH = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_Indirizzo_Registro_H")
                                    usIndirizzoRegistroL = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_Indirizzo_Registro_L")
                                    m_bSalvaLogModbus = GENERICA_DESCRIZIONE("LI_LogModbus", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID)

                                    ' Compilo MBAP
                                    ' Transaction Identifier
                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_CONFIG + iCodiceParametro))
                                    m_lbyteBufferSend.Add(abyte_1(1))
                                    m_lbyteBufferSend.Add(abyte_1(0))
                                    ' Protocol Identifier
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(0)
                                    ' Length
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(6)
                                    ' Unit Identifier
                                    m_lbyteBufferSend.Add(&HFF)
                                    ' Dati Richiesta
                                    ModbusGetConfigValue(m_lbyteBufferSend, m_iLIID, iCodiceParametro, usIndirizzoRegistroH, usIndirizzoRegistroL, m_strLocalAddress, m_strRemoteAddress)
                                    ' Scrivo nel log
                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                    ' Invio
                                    Try
                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                    Catch ex As Exception
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                        ' Chiudo collegamento
                                        m_bClose = True
                                    End Try
                                Else
                                    ' Operazione conclusa, chiudo il collegamento
                                    m_bClose = True

                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Parametro Di Configurazione Da Leggere.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = False And m_bStep_2_Answ = False Then
                                    m_bStep_2_Req = True

                                    ' Reset Buffer
                                    m_lbyteBufferSend.Clear()
                                    m_lbyteBufferReceived.Clear()

                                    ' Compilo MBAP
                                    ' Transaction Identifier
                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_RESET_CALL_FLAG_GET_CONFIG))
                                    m_lbyteBufferSend.Add(abyte_1(1))
                                    m_lbyteBufferSend.Add(abyte_1(0))
                                    ' Protocol Identifier
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(0)
                                    ' Length
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(6)
                                    ' Unit Identifier
                                    m_lbyteBufferSend.Add(&HFF)
                                    ' Dati Richiesta
                                    ModbusSetResetFlagValue(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                    ' Scrivo nel log
                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                    ' Invio
                                    Try
                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                    Catch ex As Exception
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                        ' Chiudo collegamento
                                        m_bClose = True
                                    End Try

                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = False And m_bStep_3_Answ = False Then
                                        m_bStep_3_Req = True

                                        ' Reset Buffer
                                        m_lbyteBufferSend.Clear()
                                        m_lbyteBufferReceived.Clear()

                                        ' Operazione conclusa, chiudo il collegamento
                                        m_bClose = True
                                    End If
                                End If
                            End If

                        End If
                        '------------------------------------------------------------

                        '------------------------------------------------------------
                        If m_iModoConnessione = AZIONE_MODBUS_TI_SET_CONFIG_PROC Then
                            If m_bStep_1_Req = False And m_bStep_1_Answ = False Then
                                m_bStep_1_Req = True

                                ' Reset Buffer
                                m_lbyteBufferSend.Clear()
                                m_lbyteBufferReceived.Clear()

                                If m_dtLoggerParamConfigValore.Rows.Count > m_iIndiceParametro Then

                                    m_iLIID = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LIPC_LI_ID")

                                    iCodiceParametro = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_ID")
                                    strValoreDato = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LIPC_LPC_VAL_MEMO_DB")
                                    usIndirizzoRegistroH = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_Indirizzo_Registro_H")
                                    usIndirizzoRegistroL = m_dtLoggerParamConfigValore.Rows(m_iIndiceParametro).Item("LPC_Indirizzo_Registro_L")
                                    m_bSalvaLogModbus = GENERICA_DESCRIZIONE("LI_LogModbus", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID)

                                    ' Compilo MBAP
                                    ' Transaction Identifier
                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_SET_CONFIG + iCodiceParametro))
                                    m_lbyteBufferSend.Add(abyte_1(1))
                                    m_lbyteBufferSend.Add(abyte_1(0))
                                    ' Protocol Identifier
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(0)
                                    ' Length
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(6)
                                    ' Unit Identifier
                                    m_lbyteBufferSend.Add(&HFF)
                                    ' Dati Richiesta
                                    ModbusSetConfigValue(m_lbyteBufferSend, m_iLIID, iCodiceParametro, strValoreDato, usIndirizzoRegistroH, usIndirizzoRegistroL, m_strLocalAddress, m_strRemoteAddress)
                                    ' Scrivo nel log
                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                    ' Invio
                                    Try
                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                    Catch ex As Exception
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                        ' Chiudo collegamento
                                        m_bClose = True
                                    End Try

                                Else
                                    ' Operazione conclusa, chiudo il collegamento
                                    m_bClose = True

                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_MODBUS_ERR, "Nessun Parametro Di Configurazione Da Spedire.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = False And m_bStep_2_Answ = False Then
                                    m_bStep_2_Req = True

                                    ' Reset Buffer
                                    m_lbyteBufferSend.Clear()
                                    m_lbyteBufferReceived.Clear()

                                    ' Compilo MBAP
                                    ' Transaction Identifier
                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_RESET_CALL_FLAG_SET_CONFIG))
                                    m_lbyteBufferSend.Add(abyte_1(1))
                                    m_lbyteBufferSend.Add(abyte_1(0))
                                    ' Protocol Identifier
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(0)
                                    ' Length
                                    m_lbyteBufferSend.Add(0)
                                    m_lbyteBufferSend.Add(6)
                                    ' Unit Identifier
                                    m_lbyteBufferSend.Add(&HFF)
                                    ' Dati Richiesta
                                    ModbusSetResetFlagValue(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                    ' Scrivo nel log
                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                    ' Invio
                                    Try
                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                    Catch ex As Exception
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                        ' Chiudo collegamento
                                        m_bClose = True
                                    End Try

                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = False And m_bStep_3_Answ = False Then
                                        m_bStep_3_Req = True

                                        ' Reset Buffer
                                        m_lbyteBufferSend.Clear()
                                        m_lbyteBufferReceived.Clear()

                                        ' Operazione conclusa, chiudo il collegamento
                                        m_bClose = True
                                    End If
                                End If
                            End If

                        End If
                        '------------------------------------------------------------

                        '------------------------------------------------------------
                        If m_iModoConnessione = AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC Then
                            If m_bStep_1_Req = False And m_bStep_1_Answ = False Then
                                m_bStep_1_Req = True

                                ' Per prima cosa svuoto il buffer di ricezione
                                If m_tcTC.GetStream().CanRead = True Then
                                    Dim strTemp As String = ""
                                    While m_tcTC.GetStream().DataAvailable = True
                                        Dim byteRec As Integer
                                        Try
                                            byteRec = m_tcTC.GetStream().ReadByte()
                                            If byteRec = -1 Then
                                                Exit While
                                            Else
                                                ' Preparo una stringa che visualizzero' nel log....
                                                strTemp = strTemp + byteRec.ToString()
                                            End If
                                        Catch ex As Exception
                                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                            ' Chiudo collegamento
                                            m_bClose = True
                                            Exit While
                                        End Try
                                    End While
                                    ' Scrivo nel log
                                    If strTemp.Count() > 0 Then
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_MODBUS_OK, "Il Buffer di ricezione e' stato svuotato, conteneva i seguenti dati: " + strTemp, m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    Else
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_MODBUS_OK, "Il Buffer di ricezione e' stato svuotato, non conteneva dati.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    End If
                                End If

                                ' Se la connessione non e' stata instaurata dal Datalogger, salto il passo 2 e vado al 3
                                If m_bConnessioneDaDatalogger = False Then
                                    m_bStep_2_Req = True
                                    m_bStep_2_Answ = True
                                End If

                                ' Reset Buffer
                                m_lbyteBufferSend.Clear()
                                m_lbyteBufferReceived.Clear()

                                ' Richiesta ID Logger
                                ' Compilo MBAP
                                ' Transaction Identifier
                                abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_ID))
                                m_lbyteBufferSend.Add(abyte_1(1))
                                m_lbyteBufferSend.Add(abyte_1(0))
                                ' Protocol Identifier
                                m_lbyteBufferSend.Add(0)
                                m_lbyteBufferSend.Add(0)
                                ' Length
                                m_lbyteBufferSend.Add(0)
                                m_lbyteBufferSend.Add(6)
                                ' Unit Identifier99
                                m_lbyteBufferSend.Add(&HFF)
                                ' Dati Richiesta
                                ModbusReqIDDataLogger(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                ' Scrivo nel log
                                WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                ' Invio
                                Try
                                    m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                Catch ex As Exception
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                    ' Chiudo collegamento
                                    m_bClose = True
                                End Try
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = False And m_bStep_2_Answ = False Then
                                    m_bStep_2_Req = True

                                    ' Reset Buffer
                                    m_lbyteBufferSend.Clear()
                                    m_lbyteBufferReceived.Clear()

                                    ' Verifico nel database qual'e' l'ultima unita' di tempo che ho richiesto
                                    If m_iLIID > 0 Then

                                        ' Compilo MBAP
                                        ' Transaction Identifier
                                        abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_CONN_WAY))
                                        m_lbyteBufferSend.Add(abyte_1(1))
                                        m_lbyteBufferSend.Add(abyte_1(0))
                                        ' Protocol Identifier
                                        m_lbyteBufferSend.Add(0)
                                        m_lbyteBufferSend.Add(0)
                                        ' Length
                                        m_lbyteBufferSend.Add(0)
                                        m_lbyteBufferSend.Add(6)
                                        ' Unit Identifier99
                                        m_lbyteBufferSend.Add(&HFF)
                                        ' Dati Richiesta
                                        ModbusReqConnWayDataLogger(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                        ' Scrivo nel log
                                        WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                        ' Invio
                                        Try
                                            m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                        Catch ex As Exception
                                            ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                            ' Chiudo collegamento
                                            m_bClose = True
                                        End Try
                                    Else
                                        ' Chiudo collegamento
                                        m_bClose = True

                                        ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_ERR, "L' ID del Data Logger non puo' essere 0.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    End If

                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = False And m_bStep_3_Answ = False Then
                                        m_bStep_3_Req = True

                                        ' Reset Buffer
                                        m_lbyteBufferSend.Clear()
                                        m_lbyteBufferReceived.Clear()

                                        ' Verifico nel database qual'e' l'ultima unita' di tempo che ho richiesto
                                        If m_iLIID > 0 Then

                                            If m_shCausaConnessioneDaDatalogger < 3 Or m_bConnessioneDaDatalogger = False Then

                                                ' Compilo MBAP
                                                ' Transaction Identifier
                                                abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_NR_MAX_UDT))
                                                m_lbyteBufferSend.Add(abyte_1(1))
                                                m_lbyteBufferSend.Add(abyte_1(0))
                                                ' Protocol Identifier
                                                m_lbyteBufferSend.Add(0)
                                                m_lbyteBufferSend.Add(0)
                                                ' Length
                                                m_lbyteBufferSend.Add(0)
                                                m_lbyteBufferSend.Add(6)
                                                ' Unit Identifier99
                                                m_lbyteBufferSend.Add(&HFF)
                                                ' Dati Richiesta
                                                ModbusReqNrMaxUDTDataLogger(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                ' Scrivo nel log
                                                WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                ' Invio
                                                Try
                                                    m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                Catch ex As Exception
                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                    ' Chiudo collegamento
                                                    m_bClose = True
                                                End Try
                                            Else
                                                ' Chiudo collegamento
                                                m_bClose = True

                                                ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_ERR, "Il Codice di Connessione non puo' essere superiore a 2.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
                                            End If

                                        Else
                                            ' Chiudo collegamento
                                            m_bClose = True

                                            ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_ERR, "L' ID del Data Logger deve essere superiore a 0.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
                                        End If

                                    End If

                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = True And m_bStep_3_Answ = True Then
                                        If m_bStep_4_Req = False And m_bStep_4_Answ = False Then
                                            m_bStep_4_Req = True

                                            ' Reset Buffer
                                            m_lbyteBufferSend.Clear()
                                            m_lbyteBufferReceived.Clear()

                                            If m_ushNrMaxUDT > 0 Then

                                                ' Compilo MBAP
                                                ' Transaction Identifier
                                                abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_LAST_UDT))
                                                m_lbyteBufferSend.Add(abyte_1(1))
                                                m_lbyteBufferSend.Add(abyte_1(0))
                                                ' Protocol Identifier
                                                m_lbyteBufferSend.Add(0)
                                                m_lbyteBufferSend.Add(0)
                                                ' Length
                                                m_lbyteBufferSend.Add(0)
                                                m_lbyteBufferSend.Add(6)
                                                ' Unit Identifier99
                                                m_lbyteBufferSend.Add(&HFF)
                                                ' Dati Richiesta
                                                ModbusReqActualUDTDataLogger(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                ' Scrivo nel log
                                                WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                ' Invio
                                                Try
                                                    m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                Catch ex As Exception
                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                    ' Chiudo collegamento
                                                    m_bClose = True
                                                End Try
                                            Else
                                                ' Chiudo collegamento
                                                m_bClose = True

                                                ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "Il Nr Max di UDT non puo' essere 0.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing)
                                            End If

                                        End If
                                    End If
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = True And m_bStep_3_Answ = True Then
                                        If m_bStep_4_Req = True And m_bStep_4_Answ = True Then
                                            If m_bStep_5_Req = False And m_bStep_5_Answ = False Then
                                                m_bStep_5_Req = True

                                                ' Reset Buffer
                                                m_lbyteBufferSend.Clear()
                                                m_lbyteBufferReceived.Clear()

                                                ' Verifico nel database qual'e' l'ultima unita' di tempo che ho richiesto
                                                ' Controllo che esista questo ID nel Database
                                                If GENERICA_DESCRIZIONE("LI_ID", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID) > 0 Then

                                                    ' Compilo MBAP
                                                    ' Transaction Identifie
                                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_ALL_STORED_DATA))
                                                    m_lbyteBufferSend.Add(abyte_1(1))
                                                    m_lbyteBufferSend.Add(abyte_1(0))
                                                    ' Protocol Identifier
                                                    m_lbyteBufferSend.Add(0)
                                                    m_lbyteBufferSend.Add(0)
                                                    ' Length
                                                    m_lbyteBufferSend.Add(0)
                                                    m_lbyteBufferSend.Add(17)
                                                    ' Unit Identifier
                                                    m_lbyteBufferSend.Add(&HFF)

                                                    ' Dati Richiesta
                                                    ModbusReqStoredDataDataLogger(m_lbyteBufferSend, m_iLIID, m_ushUDTDaRichiedere, m_ushRecordStart, m_ushRecordNr, m_strLocalAddress, m_strRemoteAddress, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)
                                                    ' Scrivo nel log
                                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                    ' Invio
                                                    Try
                                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                    Catch ex As Exception
                                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                        ' Chiudo collegamento
                                                        m_bClose = True
                                                    End Try
                                                Else
                                                    ' Chiudo collegamento
                                                    m_bClose = True

                                                    ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_ERR, "L' ID del Datalogger non e' presente nel database. ID Datalogger: " + m_iLIID.ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = True And m_bStep_3_Answ = True Then
                                        If m_bStep_4_Req = True And m_bStep_4_Answ = True Then
                                            If m_bStep_5_Req = True And m_bStep_5_Answ = True Then
                                                If m_bStep_6_Req = False And m_bStep_6_Answ = False Then
                                                    m_bStep_6_Req = True

                                                    ' Reset Buffer
                                                    m_lbyteBufferSend.Clear()
                                                    m_lbyteBufferReceived.Clear()

                                                    ' Compilo MBAP
                                                    ' Transaction Identifier
                                                    abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_GET_TOTALS))
                                                    m_lbyteBufferSend.Add(abyte_1(1))
                                                    m_lbyteBufferSend.Add(abyte_1(0))
                                                    ' Protocol Identifier
                                                    m_lbyteBufferSend.Add(0)
                                                    m_lbyteBufferSend.Add(0)
                                                    ' Length
                                                    m_lbyteBufferSend.Add(0)
                                                    m_lbyteBufferSend.Add(6)
                                                    ' Unit Identifier
                                                    m_lbyteBufferSend.Add(&HFF)
                                                    ' Dati Richiesta
                                                    ModbusReqTotalsDataLogger(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                    ' Scrivo nel log
                                                    WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                    ' Invio
                                                    Try
                                                        m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                    Catch ex As Exception
                                                        ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                        ' Chiudo collegamento
                                                        m_bClose = True
                                                    End Try
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = True And m_bStep_3_Answ = True Then
                                        If m_bStep_4_Req = True And m_bStep_4_Answ = True Then
                                            If m_bStep_5_Req = True And m_bStep_5_Answ = True Then
                                                If m_bStep_6_Req = True And m_bStep_6_Answ = True Then
                                                    If m_bStep_7_Req = False And m_bStep_7_Answ = False Then
                                                        m_bStep_7_Req = True

                                                        ' Reset Buffer
                                                        m_lbyteBufferSend.Clear()
                                                        m_lbyteBufferReceived.Clear()

                                                        ' Bene, a questo punto verifico se la connessione e' stata instaurata dal Datalogger
                                                        If m_bConnessioneDaDatalogger = True Then
                                                            ' Ok, controllo se devo eseguire un'operazione aggiuntiva prima di chiudere
                                                            Dim usLIAMDEAPC As UShort
                                                            usLIAMDEAPC = GetLIAzioneModbusDaEseguireAllaProssConn(m_iLIID)
                                                            If usLIAMDEAPC = AZIONE_MODBUS_TI_GET_CONFIG_PROC Then

                                                                ' Reset Valore
                                                                If SetLIAzioneModbusDaEseguireAllaProssConn(m_iLIID, 0) = True Then
                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_OK, "Reset Operazione Riuscita. Inizio Operazione.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                Else
                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_CONFIG_PROC, RISULTATO_ERR, "Reset Operazione Non Riuscita.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                End If

                                                                ' Imposto nuova operazione
                                                                m_iModoConnessione = AZIONE_MODBUS_TI_GET_CONFIG_PROC
                                                                m_dtLoggerParamConfigValore = GetConfigValueToRead(m_iLIID, True)

                                                                ' Inizializzo
                                                                InitData()

                                                            ElseIf usLIAMDEAPC = AZIONE_MODBUS_TI_SET_CONFIG_PROC Then

                                                                If SetLIAzioneModbusDaEseguireAllaProssConn(m_iLIID, 0) = True Then
                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_OK, "Reset Operazione Riuscita. Inizio Operazione.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                Else
                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_SET_CONFIG_PROC, RISULTATO_ERR, "Reset Operazione Non Riuscita.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                End If

                                                                ' Imposto nuova operazione
                                                                m_iModoConnessione = AZIONE_MODBUS_TI_SET_CONFIG_PROC
                                                                m_dtLoggerParamConfigValore = GetConfigValueToWrite(m_iLIID, False)

                                                                ' Inizializzo
                                                                InitData()

                                                            Else

                                                                ' Compilo MBAP
                                                                ' Transaction Identifier
                                                                abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_RESET_CALL_FLAG))
                                                                m_lbyteBufferSend.Add(abyte_1(1))
                                                                m_lbyteBufferSend.Add(abyte_1(0))
                                                                ' Protocol Identifier
                                                                m_lbyteBufferSend.Add(0)
                                                                m_lbyteBufferSend.Add(0)
                                                                ' Length
                                                                m_lbyteBufferSend.Add(0)
                                                                m_lbyteBufferSend.Add(6)
                                                                ' Unit Identifier
                                                                m_lbyteBufferSend.Add(&HFF)
                                                                ' Dati Richiesta
                                                                ModbusSetResetFlagValue(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                                ' Scrivo nel log
                                                                WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                                ' Invio
                                                                Try
                                                                    m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                                Catch ex As Exception
                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                                    ' Chiudo collegamento
                                                                    m_bClose = True
                                                                End Try

                                                            End If

                                                        Else

                                                            ' Compilo MBAP
                                                            ' Transaction Identifier
                                                            abyte_1 = BitConverter.GetBytes(CUShort(AZIONE_MODBUS_TI_RESET_CALL_FLAG))
                                                            m_lbyteBufferSend.Add(abyte_1(1))
                                                            m_lbyteBufferSend.Add(abyte_1(0))
                                                            ' Protocol Identifier
                                                            m_lbyteBufferSend.Add(0)
                                                            m_lbyteBufferSend.Add(0)
                                                            ' Length
                                                            m_lbyteBufferSend.Add(0)
                                                            m_lbyteBufferSend.Add(6)
                                                            ' Unit Identifier
                                                            m_lbyteBufferSend.Add(&HFF)
                                                            ' Dati Richiesta
                                                            ModbusSetResetFlagValue(m_lbyteBufferSend, m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                            ' Scrivo nel log
                                                            WriteLogModbusData(m_lbyteBufferSend, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " > " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                                                            ' Invio
                                                            Try
                                                                m_tcTC.GetStream().Write(m_lbyteBufferSend.ToArray, 0, m_lbyteBufferSend.Count())
                                                            Catch ex As Exception
                                                                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                                ' Chiudo collegamento
                                                                m_bClose = True
                                                            End Try

                                                        End If

                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            If m_bStep_1_Req = True And m_bStep_1_Answ = True Then
                                If m_bStep_2_Req = True And m_bStep_2_Answ = True Then
                                    If m_bStep_3_Req = True And m_bStep_3_Answ = True Then
                                        If m_bStep_4_Req = True And m_bStep_4_Answ = True Then
                                            If m_bStep_5_Req = True And m_bStep_5_Answ = True Then
                                                If m_bStep_6_Req = True And m_bStep_6_Answ = True Then
                                                    If m_bStep_7_Req = True And m_bStep_7_Answ = True Then
                                                        If m_bStep_8_Req = False And m_bStep_8_Answ = False Then
                                                            m_bStep_8_Req = True

                                                            ' Reset Buffer
                                                            m_lbyteBufferSend.Clear()
                                                            m_lbyteBufferReceived.Clear()

                                                            ' Operazione conclusa, chiudo il collegamento
                                                            m_bClose = True

                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                        End If
                        '------------------------------------------------------------

                    End If
                End If

                If m_tcTC.Connected = True Then
                    If m_tcTC.GetStream().CanRead = True Then
                        If Not m_lbyteBufferReceived Is Nothing Then
                            Dim bDataArrived As Boolean
                            While m_tcTC.GetStream().DataAvailable = True
                                Dim byteRec As Integer
                                Try
                                    byteRec = m_tcTC.GetStream().ReadByte()
                                    If byteRec = -1 Then
                                        Exit While
                                    End If
                                Catch ex As Exception
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                    ' Chiudo collegamento
                                    m_bClose = True
                                    Exit While
                                End Try
                                m_iTickCount = 0
                                m_lbyteBufferReceived.Add(byteRec)
                                bDataArrived = True
                            End While
                            ' Scrivo nel log
                            If bDataArrived = True Then
                                WriteLogModbusData(m_lbyteBufferReceived, m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), " - " + m_strLocalAddress + " < " + m_strRemoteAddress, m_strLocalAddress, m_strRemoteAddress)
                            End If

                            ' Elaboro il messaggio
                            Dim ba2(1) As Byte
                            If m_lbyteBufferReceived.Count() >= 2 Then
                                ' Controllo MBAP
                                ' Transaction Identifier
                                Array.Copy(m_lbyteBufferReceived.ToArray(), 0, ba2, 0, 2)
                                Array.Reverse(ba2)
                                sTI = BitConverter.ToInt16(ba2, 0)
                                If m_lbyteBufferReceived.Count() >= 4 Then
                                    ' Protocol Identifier
                                    Array.Copy(m_lbyteBufferReceived.ToArray(), 2, ba2, 0, 2)
                                    Array.Reverse(ba2)
                                    iPI = BitConverter.ToInt16(ba2, 0)
                                    If iPI = 0 Then
                                        If m_lbyteBufferReceived.Count() >= 6 Then
                                            ' Length
                                            Array.Copy(m_lbyteBufferReceived.ToArray(), 4, ba2, 0, 2)
                                            Array.Reverse(ba2)
                                            iLenght = BitConverter.ToInt16(ba2, 0)
                                            If iLenght <= 240 Then
                                                If m_lbyteBufferReceived.Count() >= 7 Then
                                                    ' Unit Identifier
                                                    iUI = m_lbyteBufferReceived(6)
                                                    If iUI = &HFF Then
                                                        'If m_lbyteBufferReceived.Count() >= 6 + iLenght Then
                                                        If m_lbyteBufferReceived.Count() = (6 + iLenght) Then
                                                            ' Il messaggio e' completo
                                                            Select Case sTI

                                                                '------------------------------------------------------------
                                                                Case AZIONE_MODBUS_TI_GET_CONFIG_CODE_MIN_VALUE To AZIONE_MODBUS_TI_GET_CONFIG_CODE_MAX_VALUE
                                                                    ' Lettura configurazione
                                                                    ModbusStoreGetConfigValue(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, (sTI - AZIONE_MODBUS_TI_GET_CONFIG), m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Incremento l'indice
                                                                    m_iIndiceParametro = m_iIndiceParametro + 1
                                                                    ' Vedo se ho terminato
                                                                    If m_dtLoggerParamConfigValore.Rows.Count > m_iIndiceParametro Then
                                                                        ' Nuova Richiesta
                                                                        m_bStep_1_Req = False
                                                                    Else
                                                                        ' Passo Completato
                                                                        m_bStep_1_Answ = True
                                                                    End If

                                                                Case AZIONE_MODBUS_TI_RESET_CALL_FLAG_GET_CONFIG
                                                                    ' Richiesta Reset Flag di chiamata
                                                                    ModbusCheckSetResetFlagValue(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_2_Answ = True
                                                                    '------------------------------------------------------------

                                                                    '------------------------------------------------------------
                                                                Case AZIONE_MODBUS_TI_SET_CONFIG_CODE_MIN_VALUE To AZIONE_MODBUS_TI_SET_CONFIG_CODE_MAX_VALUE
                                                                    ' Scrittura configurazione
                                                                    ModbusStoreSetConfigValue(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, (sTI - AZIONE_MODBUS_TI_SET_CONFIG), m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Incremento l'indice
                                                                    m_iIndiceParametro = m_iIndiceParametro + 1
                                                                    ' Vedo se ho terminato
                                                                    If m_dtLoggerParamConfigValore.Rows.Count > m_iIndiceParametro Then
                                                                        ' Nuova Richiesta
                                                                        m_bStep_1_Req = False
                                                                    Else
                                                                        ' Passo Completato
                                                                        m_bStep_1_Answ = True
                                                                    End If

                                                                Case AZIONE_MODBUS_TI_RESET_CALL_FLAG_SET_CONFIG
                                                                    ' Richiesta Reset Flag di chiamata
                                                                    ModbusCheckSetResetFlagValue(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_2_Answ = True
                                                                    '------------------------------------------------------------

                                                                    '------------------------------------------------------------
                                                                Case AZIONE_MODBUS_TI_GET_ID
                                                                    ' Richiesta ID Data Logger
                                                                    m_iLIID = ModbusGetIDDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_strLocalAddress, m_strRemoteAddress)

                                                                    m_bSalvaLogModbus = GENERICA_DESCRIZIONE("LI_LogModbus", "LoggerInst", "LI_ID", m_iLIID, DEFAULT_OPERATOR_ID)

                                                                    RenameLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + 0.ToString(), m_strLogFileName + m_iLIID.ToString(), m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Nel log dei dati, prendo il riferimento dell'indirizzo
                                                                    ' IP riferito all'intervallo del Timeout ed imposto l'ID del DL
                                                                    ' Se la connessione e' stata instaurata dal Datalogger, eseguo questa operazione
                                                                    If m_bConnessioneDaDatalogger = True Then
                                                                        SetLIIDLogByRemoteAddressValueAndTime(m_iLIID, m_strLocalAddress, m_strRemoteAddress)
                                                                    End If

                                                                    ' Imposto il timeout uguale a quello del Datalogger
                                                                    ' Moltiplico per 2 perche' il Tick e' di 0,5 sec(500 ms)
                                                                    m_iTickCountTimeout = (GetLIIDConfigValue(m_iLIID, 409) * 2)

                                                                    ' Passo Completato
                                                                    m_bStep_1_Answ = True

                                                                Case AZIONE_MODBUS_TI_GET_CONN_WAY
                                                                    ' Richiesta Causa Connessione
                                                                    m_shCausaConnessioneDaDatalogger = ModbusGetConnWayDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_2_Answ = True

                                                                Case AZIONE_MODBUS_TI_GET_NR_MAX_UDT
                                                                    ' Richiesta ID Data Logger
                                                                    m_ushNrMaxUDT = ModbusGetNrMaxUDTDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_3_Answ = True

                                                                Case AZIONE_MODBUS_TI_GET_LAST_UDT
                                                                    ' Richiesta Ultimo UDT Memorizzato nel Datalogger
                                                                    m_ushUltimoUDTElaboratoNelDatalogger = ModbusGetActualUDTDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    If AZIONE_MODBUS_TI_GET_SINCR_DB_DL_DATA_PROC = GetLIAzioneModbusDaEseguireAllaProssConn(m_iLIID) Then
                                                                        ' Nel Log setto come ultimo UDT salvato, quello attuale,
                                                                        ' alla prossima richiesta verra' richiesto quello successivo, che e' sicuramente
                                                                        ' giusto e coerente.
                                                                        SetLastStoredUDTValue(m_iLIID, m_ushUltimoUDTElaboratoNelDatalogger, m_strLocalAddress, m_strRemoteAddress, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)

                                                                        If SetLIAzioneModbusDaEseguireAllaProssConn(m_iLIID, 0) = True Then
                                                                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_OK, "Reset Operazione Sincronizzazione Dati dal Datalogger Al Database Riuscita.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                        Else
                                                                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_ERR, "Reset Operazione Sincronizzazione Dati dal Datalogger Al Database Non Riuscita.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                        End If

                                                                        ' Passo Completato
                                                                        m_bStep_4_Answ = True

                                                                        ' Non prelevo niente
                                                                        m_bStep_5_Req = True
                                                                        m_bStep_5_Answ = True

                                                                    Else
                                                                        ' In questo punto richiedo anche l'ultimo UDT memorizzato con successo
                                                                        ' Se l'ultimo UDT memorizzato e' superiore all'UDT attuale,
                                                                        ' puo' significare che il Datalogger e' stato resettato
                                                                        ' oppure e' passato troppo tempo dall'ultima volta che ho acquisito i dati, quindi,
                                                                        ' quando raggiungero' l'acquisizione dell'UDT attuale, continuo a prendere i
                                                                        ' dati anche di quelli dopo, fino a che la data ed ora non mi segnaleranno allarme
                                                                        ' perche' il dato e' gia' stato acquisito.
                                                                        m_ushUltimoUDTMemorizzatoOk = GetLastStoredUDTLogValue(m_iLIID, m_strLocalAddress, m_strRemoteAddress, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)

                                                                        ' Se e' lo stesso UDT, allora non richiedo niente
                                                                        If m_ushUltimoUDTElaboratoNelDatalogger = m_ushUltimoUDTMemorizzatoOk Then
                                                                            ' Passo Completato
                                                                            m_bStep_4_Answ = True

                                                                            ' Non prelevo niente
                                                                            m_bStep_5_Req = True
                                                                            m_bStep_5_Answ = True

                                                                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_GET_LAST_UDT, RISULTATO_MODBUS_ERR, "L'ultimo UDT elaborato nel Datalogger e' lo stesso memorizzato nel Database. Non verra' memorizzato nessun dato.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                                        Else
                                                                            If m_ushUltimoUDTMemorizzatoOk >= m_ushNrMaxUDT Then
                                                                                m_ushUltimoUDTMemorizzatoOk = 0
                                                                                m_ushUDTDaRichiedere = 0
                                                                            Else
                                                                                m_ushUDTDaRichiedere = m_ushUltimoUDTMemorizzatoOk + 1
                                                                            End If

                                                                            ' Passo Completato
                                                                            m_bStep_4_Answ = True
                                                                        End If
                                                                    End If

                                                                Case AZIONE_MODBUS_TI_GET_ALL_STORED_DATA
                                                                    ' Richiesta Dati 
                                                                    ModbusGetStoredDataDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_ushUDTDaRichiedere, m_bIncrementaUDT, m_bIncrementaRecord, m_bRecordAcquisito, m_bAlmenoUnRecordGiaPresente, m_strLocalAddress, m_strRemoteAddress, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA)
                                                                    If m_bIncrementaUDT = False And m_bIncrementaRecord = False Then
                                                                        m_iRetryToGetNextUDT = m_iRetryToGetNextUDT + 1
                                                                        If m_iRetryToGetNextUDT > 3 Then
                                                                            ' Ok, anche i 3 udt successivi erano gia' inseriti o non inseribili, chiudo.

                                                                            ' Prelevo gli allarmi generati e verifico se devo
                                                                            ' inviare una email di segnalazione
                                                                            strTestoAllarme = GetDataLoggerAllarmi(m_iLIID)
                                                                            If strTestoAllarme.Count > 0 Then
                                                                                If GetDGI(78, strParam78) = True Then
                                                                                    SendEMailReportAllarmiDL(m_iLIID, strParam78, strTestoAllarme, "", True, False)
                                                                                Else
                                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 78, Prefisso Oggetto In caso di Allarmi Impianto invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                                End If
                                                                            End If

                                                                            ' Passo Completato
                                                                            m_bStep_5_Answ = True
                                                                        Else
                                                                            ' Ritento ancora
                                                                            m_bIncrementaUDT = True
                                                                        End If

                                                                        If m_bAlmenoUnRecordGiaPresente = True Then
                                                                            m_bAlmenoUnRecordGiaPresente = False
                                                                            ScriviLogEventi(m_iLIID, m_ushUDTDaRichiedere, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA, RISULTATO_MODBUS_ERR, "Almeno un record, nella memorizzazione dell'UDT corrente, era gia' presente.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                                        End If
                                                                    End If

                                                                    If m_bIncrementaUDT = True Then
                                                                        If m_ushUDTDaRichiedere <> m_ushUltimoUDTElaboratoNelDatalogger Then
                                                                            ' Ok, richiesta regolare
                                                                            If m_ushUDTDaRichiedere >= m_ushNrMaxUDT Then
                                                                                m_ushUDTDaRichiedere = 0
                                                                            Else
                                                                                m_ushUDTDaRichiedere = m_ushUDTDaRichiedere + 1
                                                                            End If

                                                                            m_ushRecordStart = 1
                                                                            m_bRecordAcquisito = False

                                                                            ' Nuova richiesta
                                                                            m_bStep_5_Req = False
                                                                        Else
                                                                            ' Prelevo gli allarmi generati e verifico se devo
                                                                            ' inviare una email di segnalazione
                                                                            strTestoAllarme = GetDataLoggerAllarmi(m_iLIID)
                                                                            If strTestoAllarme.Count > 0 Then
                                                                                If GetDGI(78, strParam78) = True Then
                                                                                    SendEMailReportAllarmiDL(m_iLIID, strParam78, strTestoAllarme, "", True, False)
                                                                                Else
                                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 78, Prefisso Oggetto In caso di Allarmi Impianto invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                                End If
                                                                            End If

                                                                            ' Passo Completato
                                                                            m_bStep_5_Answ = True
                                                                        End If
                                                                    End If

                                                                    If m_bIncrementaRecord = True Then
                                                                        m_ushRecordStart = m_ushRecordStart + m_ushRecordNr

                                                                        ' Nuova richiesta
                                                                        m_bStep_5_Req = False
                                                                    End If

                                                                Case AZIONE_MODBUS_TI_GET_TOTALS
                                                                    ' Richiesta Totali
                                                                    ModbusGetTotalsDataLogger(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_6_Answ = True

                                                                Case AZIONE_MODBUS_TI_RESET_CALL_FLAG
                                                                    ' Richiesta Reset Flag di chiamata
                                                                    ModbusCheckSetResetFlagValue(m_lbyteBufferReceived.GetRange(7, (m_lbyteBufferReceived.Count() - 7)), m_iLIID, m_strLocalAddress, m_strRemoteAddress)

                                                                    ' Passo Completato
                                                                    m_bStep_7_Answ = True
                                                                    '------------------------------------------------------------

                                                                Case Else

                                                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_ERR, "Intestazione o Codice Modbus non riconosciuta: " + sTI.ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                                                                    ' Chiudo collegamento
                                                                    m_bClose = True

                                                            End Select

                                                            ' Reset Buffer Ricezione
                                                            m_lbyteBufferReceived.Clear()

                                                        ElseIf m_lbyteBufferReceived.Count() >= (6 + iLenght) Then
                                                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_ERR, "Numero di dati ricevuti troppo elevato. Attesi: " + (6 + iLenght).ToString() + ", Ricevuti: " + m_lbyteBufferReceived.Count().ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                            ' Chiudo
                                                            m_bClose = True
                                                        End If
                                                    Else
                                                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_ERR, "Unit Identifier non riconosciuto: " + iUI.ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                        ' Chiudo
                                                        m_bClose = True
                                                    End If
                                                End If
                                            Else
                                                ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_ERR, "Lunghezza stringa da ricevere > 240: " + iLenght.ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                                ' Chiudo
                                                m_bClose = True
                                            End If
                                        End If
                                    Else
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_ERR, "Protocol Identifier Modbus non riconosciuto: " + iPI.ToString(), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                        ' Chiudo
                                        m_bClose = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If m_bClose = True Then
                If m_iLIID > 0 Then
                    If GetLIAutoAggInCorso(m_iLIID) = True Then
                        ' Devo aggiornare i dati ....
                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_AUTO_START_CONNECTION_RESET, RISULTATO_OK, "Operazione Eseguita Automaticamente.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                    SetLIAutoAggInCorso(m_iLIID, False)
                End If
                If m_iLIID > 0 Then
                    If m_bSalvaLogModbus = True Then
                        CopyLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), "", "")
                    Else
                        DeleteLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), "", "")
                    End If
                Else
                    CopyLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), "", "")
                End If

                If Not m_tcTC Is Nothing Then

                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_CLOSE_CONNECTION, RISULTATO_MODBUS_OK, "", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    m_tcTC.GetStream().Close()
                    m_tcTC.Client.Close()
                    m_tcTC.Close()
                    'm_tcTC.GetStream().Dispose()
                    m_tcTC.Client = Nothing
                    m_tcTC = Nothing

                End If
                If Not m_lbyteBufferSend Is Nothing Then
                    m_lbyteBufferSend.Clear()
                    m_lbyteBufferSend = Nothing
                End If
                If Not m_lbyteBufferReceived Is Nothing Then
                    m_lbyteBufferReceived.Clear()
                    m_lbyteBufferReceived = Nothing
                End If
            ElseIf m_iTickCount >= m_iTickCountTimeout Then
                If m_iLIID > 0 Then
                    If GetLIAutoAggInCorso(m_iLIID) = True Then
                        If Not m_tcTC Is Nothing Then
                            ' Devo aggiornare i dati ....
                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_AUTO_START_CONNECTION_RESET, RISULTATO_OK, "Operazione Eseguita Automaticamente.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    End If
                    SetLIAutoAggInCorso(m_iLIID, False)
                End If
                If m_bSalvaLogModbus = True Then
                    CopyLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), "", "")
                Else
                    DeleteLogModbusData(m_iLIID, m_strLogFileDirectoty, m_strLogFileName + m_iLIID.ToString(), "", "")
                End If

                If Not m_tcTC Is Nothing Then

                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_TI_CLOSE_CONNECTION, RISULTATO_MODBUS_TIMEOUT, "", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                    m_tcTC.GetStream().Close()
                    m_tcTC.Client.Close()
                    m_tcTC.Close()
                    'm_tcTC.GetStream().Dispose()
                    m_tcTC.Client = Nothing
                    m_tcTC = Nothing

                End If
                If Not m_lbyteBufferSend Is Nothing Then
                    m_lbyteBufferSend.Clear()
                    m_lbyteBufferSend = Nothing
                End If
                If Not m_lbyteBufferReceived Is Nothing Then
                    m_lbyteBufferReceived.Clear()
                    m_lbyteBufferReceived = Nothing
                End If
            ElseIf m_iTickCount >= (m_iTickCountTimeout \ 2) Then
                ' A meta' del timeout, ritento ripetizione del comando
                If Not m_tcTC Is Nothing Then
                    If m_tcTC.Connected = True Then
                        If m_bTimeoutRetry = False Then
                            m_bTimeoutRetry = True

                            ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_GENERICA, RISULTATO_MODBUS_TIMEOUT, "Reinvio dell'ultimo comando in corso...", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

                            ' Per prima cosa svuoto il buffer di ricezione
                            If m_tcTC.GetStream().CanRead = True Then
                                Dim strTemp As String = ""
                                While m_tcTC.GetStream().DataAvailable = True
                                    Dim byteRec As Integer
                                    Try
                                        byteRec = m_tcTC.GetStream().ReadByte()
                                        If byteRec = -1 Then
                                            Exit While
                                        Else
                                            ' Preparo una stringa che visualizzero' nel log....
                                            strTemp = strTemp + byteRec.ToString()
                                        End If
                                    Catch ex As Exception
                                        ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                        ' Chiudo collegamento
                                        m_bClose = True
                                        Exit While
                                    End Try
                                End While
                                ' Scrivo nel log
                                If strTemp.Count() > 0 Then
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_MODBUS_OK, "Il Buffer di ricezione e' stato svuotato, conteneva i seguenti dati: " + strTemp, m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                Else
                                    ScriviLogEventi(m_iLIID, 0, AZIONE_MODBUS_READ, RISULTATO_MODBUS_OK, "Il Buffer di ricezione e' stato svuotato, non conteneva dati.", m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            End If

                            If m_bStep_8_Req = True And m_bStep_8_Answ = False Then
                                m_bStep_8_Req = False
                            ElseIf m_bStep_7_Req = True And m_bStep_7_Answ = False Then
                                m_bStep_7_Req = False
                            ElseIf m_bStep_6_Req = True And m_bStep_6_Answ = False Then
                                m_bStep_6_Req = False
                            ElseIf m_bStep_5_Req = True And m_bStep_5_Answ = False Then
                                m_bStep_5_Req = False
                            ElseIf m_bStep_4_Req = True And m_bStep_4_Answ = False Then
                                m_bStep_4_Req = False
                            ElseIf m_bStep_3_Req = True And m_bStep_3_Answ = False Then
                                m_bStep_3_Req = False
                            ElseIf m_bStep_2_Req = True And m_bStep_2_Answ = False Then
                                m_bStep_2_Req = False
                            ElseIf m_bStep_1_Req = True And m_bStep_1_Answ = False Then
                                m_bStep_1_Req = False
                            End If

                        End If
                    End If
                End If
                m_t.Start()
            Else
                m_t.Start()
            End If

        Catch ex As Exception

            ' Per sicurezza provo ad eliminare tutto....
            Try
                m_tcTC.GetStream().Close()
            Catch ex_1 As Exception
                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " Stream -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            Try
                m_tcTC.Client.Close()
            Catch ex_1 As Exception
                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " Client -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            Try
                m_tcTC.Close()
            Catch ex_1 As Exception
                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " m_tcTC -> Close: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            'Try
            '    m_tcTC.GetStream().Dispose()
            'Catch ex_1 As Exception
            '    ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " Stream -> Dispose: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            'End Try
            Try
                m_tcTC.Client = Nothing
            Catch ex_1 As Exception
                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " Client -> Nothing: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            Try
                m_tcTC = Nothing
            Catch ex_1 As Exception
                ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_MODBUS_ERR, " m_tcTC -> Nothing: " + ex_1.Message + vbCrLf + GetRowNrErrorInStackTrace(ex_1.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)
            End Try
            Try
                If Not m_lbyteBufferSend Is Nothing Then
                    m_lbyteBufferSend.Clear()
                    m_lbyteBufferSend = Nothing
                End If
            Catch ex_1 As Exception

            End Try

            Try
                If Not m_lbyteBufferReceived Is Nothing Then
                    m_lbyteBufferReceived.Clear()
                    m_lbyteBufferReceived = Nothing
                End If
            Catch ex_1 As Exception

            End Try

            ScriviLogEventi(m_iLIID, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), m_strLocalAddress, m_strRemoteAddress, DEFAULT_OPERATOR_ID, Nothing, False)

            m_bClose = True

        End Try

    End Sub

End Class
