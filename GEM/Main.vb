Imports System.Management

Public Class Main

    Private m_iUID As Integer

    Dim m_ipTCPIPAddressWait As Net.IPAddress
    Dim m_ipTCPIPPortWait As Integer

    Dim m_tlListen As New List(Of Net.Sockets.TcpListener)

    Dim m_ltcpipClient As New List(Of TCPIPClient)

    Private m_bs As New BindingSource

    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.ToolStripStatusIndirizzoPortaTCPIPInAscolto.Text = "Server non abilitato."
        Me.ToolStripStatusIndirizzoPortaTCPIPConnesse.Text = "Server non abilitato."

        ' Notifico l'Aggiornamento.
        If My.Settings.NewRelease = True Then 'new version
            My.Settings.Upgrade() 'yes, reload previous settings
            My.Settings.NewRelease = False 'set new version to false
            My.Settings.Save() 'save the settings
            System.Windows.Forms.MessageBox.Show(Owner, "Il programma e' stato aggiornato alla versione: " + My.Application.Info.Version.ToString() + "." + vbCrLf + "Per info e dettagli potete scrivere una email a: service@fasenet.it" + vbCrLf + "Buon Lavoro da Fase Engineering." + vbCrLf + "www.fasenet.com" + vbCrLf + vbCrLf + vbCrLf + vbCrLf + "Sviluppato per Fase Engineering Da:" + vbCrLf + "Pretolesi Riccardo - www.consulenzeperizie.it - www.pretolesi.com", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Avvio il server solo se abilitato
        If My.Settings.Server = True Then
            AvviaServerTCPIP()

            AvviaServerComm()

            Dim iParam26 As Integer
            If GetDGI(26, iParam26) = True Then
                If iParam26 >= 1000 Then
                    TimerTCPIP.Interval = iParam26
                    TimerTCPIP.Start()
                Else
                    System.Windows.Forms.MessageBox.Show(Owner, "Nel Timer di controllo connessioni(Parametro Nr 26) e' stato impostato un tempo inferiore a 1000ms. Impostare un tempo uguale o superiore a 1000ms e riavviare il programma.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                System.Windows.Forms.MessageBox.Show(Owner, "Parametro Nr 26 Non Impostato. Impostarlo e riavviare il programma.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Dim iParam28 As Integer
            If GetDGI(28, iParam28) = True Then
                If iParam28 >= 5000 Then
                    TimerCheckDL.Interval = iParam28
                    TimerCheckDL.Start()
                Else
                    System.Windows.Forms.MessageBox.Show(Owner, "Nel Timer di controllo DL(Parametro Nr 28) e' stato impostato un tempo inferiore a 5000ms. Impostare un tempo uguale o superiore a 5000ms e riavviare il programma.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                System.Windows.Forms.MessageBox.Show(Owner, "Parametro Nr 28 Non Impostato. Impostarlo e riavviare il programma.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        If e.CloseReason = CloseReason.UserClosing Then
            lg.UID = 0
            lg.ULivello = GetMinLevelReq("ChiusuraGEM")
            dr = lg.ShowDialog(Me)
            If dr = Windows.Forms.DialogResult.Yes Then
                TimerTCPIP.Stop()
                TimerCheckDL.Stop()
                ArrestaServerTCPIP()
                ArrestaServerComm()
            Else
                e.Cancel = True
            End If
        Else
            TimerTCPIP.Stop()
            TimerCheckDL.Stop()
            ArrestaServerTCPIP()
            ArrestaServerComm()
        End If

        lg.Dispose()

    End Sub

    Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Dim byte_1 As Byte
        Dim lbyteBuffer As New List(Of Byte)
        Dim strPrefix As String = ""
        If e.EventType = IO.Ports.SerialData.Chars Then
            strPrefix = "_chars"
        End If
        If e.EventType = IO.Ports.SerialData.Eof Then
            strPrefix = "_eof"
        End If
        Try
            While byte_1 <> -1
                byte_1 = SerialPort.ReadByte()
                lbyteBuffer.Add(byte_1)
            End While
        Catch ex As Exception

        End Try
        WriteLogModbusData(lbyteBuffer, 0, My.Application.Info.DirectoryPath.ToString() + "\" + "Logs_" + Date.Now.Year.ToString() + "_" + Date.Now.Month.ToString() + "_" + Date.Now.Day.ToString(), "DataloggerDeviceCommDebug", strPrefix, "", "")
    End Sub

    Private Sub AvviaServerTCPIP()
        Dim iTotNrTCPITAddr As Integer
        Dim strTCPITAddr As String = ""
        Dim iTCPITPort As Integer
        Dim tl As Net.Sockets.TcpListener
        Try
            If m_tlListen.Count = 0 Then
                If GetDGI(30, iTotNrTCPITAddr) = True Then
                    If GetDGI(51, iTCPITPort) = True Then
                        For iIndice = 0 To iTotNrTCPITAddr - 1
                            If GetDGI((31 + iIndice), strTCPITAddr) = True Then
                                tl = New Net.Sockets.TcpListener(Net.IPAddress.Parse(strTCPITAddr), iTCPITPort)
                                tl.Start()
                                m_tlListen.Add(tl)
                                ScriviLogEventi(0, 0, AZIONE_SERVER_TCPIP_LISTEN, RISULTATO_OK, "", strTCPITAddr + ":" + iTCPITPort.ToString(), "", DEFAULT_OPERATOR_ID, Me, False)
                            End If
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_SERVER_TCPIP_LISTEN, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try

    End Sub

    Private Sub AvviaServerComm()
        Dim bCommDebugEnable As Boolean
        Dim strCommPortName As String
        Dim iReadTimeout As Integer
        ' Abilito la seriale per il debug
        Try
            bCommDebugEnable = False
            strCommPortName = ""
            iReadTimeout = 3
            GetDGI(10, bCommDebugEnable)
            If bCommDebugEnable = True Then
                GetDGI(11, strCommPortName)
                GetDGI(21, iReadTimeout)

                SerialPort.PortName = strCommPortName
                SerialPort.BaudRate = 38400
                SerialPort.DataBits = 8
                SerialPort.Parity = IO.Ports.Parity.None
                SerialPort.StopBits = 1
                SerialPort.RtsEnable = False
                SerialPort.ReadTimeout = iReadTimeout

                SerialPort.Open()
                ScriviLogEventi(0, 0, AZIONE_SERVER_COM_LISTEN, RISULTATO_OK, "Porta Di Comunicazione: " + strCommPortName + " Aperta per Debug Traffico Dati Tra Datalogger e Periferiche", "", "", DEFAULT_OPERATOR_ID, Me, False)
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_SERVER_COM_LISTEN, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try

    End Sub

    Private Sub ArrestaServerTCPIP()
        Try
            For Each tl As Net.Sockets.TcpListener In m_tlListen
                tl.Stop()
                tl.Server.Close()
            Next
            ' Rimuovo tutti gli elementi disconnessi
            m_tlListen.RemoveAll(AddressOf TCPIPListenDisconnected)

            ScriviLogEventi(0, 0, AZIONE_SERVER_TCPIP_STOP, RISULTATO_OK, "", "", "", DEFAULT_OPERATOR_ID, Me, False)

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_SERVER_TCPIP_STOP, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try

    End Sub

    Private Sub ArrestaServerComm()
        Try
            If SerialPort.IsOpen() = True Then
                SerialPort.Close()
                ScriviLogEventi(0, 0, AZIONE_SERVER_COM_STOP, RISULTATO_OK, "Porta Di Comunicazione Chiusa per Debug Traffico Dati Tra Datalogger e Periferiche", "", "", DEFAULT_OPERATOR_ID, Me, False)
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_SERVER_COM_STOP, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
        End Try
    End Sub

    Private Sub TimerTCPIP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTCPIP.Tick
        Dim str_1 As String
        Dim iParam50 As Integer
        Dim bMaxNrConnessioniDLRaggiunto As Boolean

        TimerTCPIP.Stop()

        Try
            ' TCP/IP
            If GetDGI(50, iParam50) = True Then
                If m_tlListen.Count > 0 Then
                    For Each tl As Net.Sockets.TcpListener In m_tlListen
                        If m_ltcpipClient.Count() < iParam50 Then
                            If tl.Pending() = True Then
                                Dim tcTC As Net.Sockets.TcpClient
                                Dim tcpipc As TCPIPClient
                                tcTC = tl.AcceptTcpClient()
                                ScriviLogEventi(0, 0, AZIONE_CLIENT_TCPIP_REQ_CONNECTION, RISULTATO_OK, "Dettagli Memoria: Fisica Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Virtuale Tot.: " + My.Computer.Info.TotalVirtualMemory.ToString + ". Disponibile Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Disponibile Virt.: " + My.Computer.Info.AvailablePhysicalMemory.ToString, tl.LocalEndpoint.ToString(), tcTC.Client.RemoteEndPoint.ToString(), DEFAULT_OPERATOR_ID, Me, False)
                                tcpipc = New TCPIPClient()
                                tcpipc.Accept(tcTC)
                                m_ltcpipClient.Add(tcpipc)
                            End If
                        Else
                            bMaxNrConnessioniDLRaggiunto = True
                            Exit For
                        End If
                    Next tl
                End If
            Else
                ScriviLogEventi(0, 0, AZIONE_CLIENT_TCPIP_REQ_CONNECTION, RISULTATO_ERR, "Parametro 50, Nr Max DL connessi, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
            End If

            ' Rimuovo tutti gli elementi disconnessi
            m_ltcpipClient.RemoveAll(AddressOf TCPIPClientDisconnected)

            ' Porte di Comunicazione - TCP/IP
            ' Conteggio Le TCP/IP in ascolto
            str_1 = ""
            For Each tl As Net.Sockets.TcpListener In m_tlListen
                str_1 = str_1 + tl.LocalEndpoint.ToString() + " - "
            Next
            ToolStripStatusIndirizzoPortaTCPIPInAscolto.Text = str_1

            ' Conteggio Le TCP/IP connesse
            'str_1 = ""
            'For Each tcpipClient As TCPIPClient In m_ltcpipClient
            '    If Not tcpipClient.TC.Client.RemoteEndPoint Is Nothing Then
            '        str_1 = str_1 + tcpipClient.TC.Client.RemoteEndPoint.ToString() + " - "
            '    End If
            'Next
            'ToolStripStatusIndirizzoPortaTCPIPConnesse.Text = str_1
            ToolStripStatusIndirizzoPortaTCPIPConnesse.Text = m_ltcpipClient.Count().ToString() + " / " + iParam50.ToString()
            If bMaxNrConnessioniDLRaggiunto = True Then
                ToolStripStatusIndirizzoPortaTCPIPConnesse.ForeColor = Color.Red
            Else
                ToolStripStatusIndirizzoPortaTCPIPConnesse.ForeColor = Color.Green
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me, False)
        End Try

        TimerTCPIP.Start()
    End Sub

    Private Sub TimerCheckDL_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckDL.Tick

        TimerCheckDL.Stop()

        ' Verifico, di ogni datalogger, la data dell'ultimo aggiornamento
        ' quindi, se supera una soglia di tempo impostata, tento l'aggiornamento.
        ' Datalogger configurati
        Dim dtLI As DataTable
        Dim dtLastStoredLIValue As Date
        Dim tsDiff As TimeSpan
        Dim iDiffTotalMinutes As Integer

        Static dtStatNowDay As Date = Date.Now
        Dim dtStartDay As Date
        Dim dtStopDay As Date
        Dim dtStartDeleteLogDB As Date
        Dim dtStartDeleteLogFiles As Date

        Static dtStatNowMounth As Date = Date.Now
        Dim dtStartMounth As Date
        Dim dtStopMounth As Date

        Dim bSetLIStatMemoDayEseguito As Boolean
        Dim strReportSemplificatoGiornoDiTuttiGliImpianti As String = ""
        Dim strReportSemplificatoMeseDiTuttiGliImpianti As String = ""
        Dim di As System.IO.DirectoryInfo


        Try

            ' Prelevo la lista di tutti i Datalogger
            dtLI = GetLIInFunzione()
            If Not dtLI Is Nothing Then
                For Each dr As DataRow In dtLI.Rows
                    ' Utilizzo come riferimento i Riferimenti nel Log

                    dtLastStoredLIValue = GetLastStoredDateAndTimeLogValue(dr.Item("LI_ID"), "", "")
                    tsDiff = (Date.Now - dtLastStoredLIValue)
                    ' Se questa operazione va in overflow, significa una differenza di ore maggiore di un intero
                    ' Quindi, in questo caso, lo imposto al massimo di un intero
                    Try
                        iDiffTotalMinutes = tsDiff.TotalMinutes \ 1
                    Catch ex As Exception
                        iDiffTotalMinutes = Integer.MaxValue
                    End Try

                    If dr.Item("LI_AutoAggDopoXOre") > 10 Then
                        If iDiffTotalMinutes >= dr.Item("LI_AutoAggDopoXOre") Then
                            If dr.Item("LI_AutoAggInCorso") = False Then
                                ' Prima di effettuare il tentativo di aggancio
                                ' mi assicuro di settare il bit, se l'operazione andasse male, richierei di andare in comunicazione continua...
                                If SetLIAutoAggInCorso(dr.Item("LI_ID"), True) = True Then
                                    ' Devo aggiornare i dati ....
                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, RISULTATO_MODBUS_OPERAZIONE_IN_CORSO, "E' Stata Avviata la procedura Automatica di Prelevamento dei Dati Dal Datalogger." + " Dettagli Memoria: Fisica Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Virtuale Tot.: " + My.Computer.Info.TotalVirtualMemory.ToString + ". Disponibile Tot.: " + My.Computer.Info.TotalPhysicalMemory.ToString + ". Disponibile Virt.: " + My.Computer.Info.AvailablePhysicalMemory.ToString, "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_MODBUS_TI_AUTO_START_CONNECTION_SET, RISULTATO_OK, "Operazione Eseguita Automaticamente. Data ultima connessione: " + dtLastStoredLIValue.ToString(), "", dr.Item("LI_TCPIP_Get_Ind").ToString() + ":" + dr.Item("LI_TCPIP_Set_Port").ToString(), DEFAULT_OPERATOR_ID, Nothing, False)

                                    Dim tcpipc As TCPIPClient
                                    tcpipc = New TCPIPClient
                                    tcpipc.BeginConnect(dr.Item("LI_ID"), dr.Item("LI_TCPIP_Get_Ind"), dr.Item("LI_TCPIP_Set_Port"), AZIONE_MODBUS_TI_GET_ALL_STORED_DATA_PROC, Nothing)
                                    m_ltcpipClient.Add(tcpipc)
                                End If
                            End If
                        End If
                    End If

                    ' Verifico se ho dei dati fino a XXX UDT in piu' di quelli previsti per l'aggiornamento
                    ' Prelevo ogni quanti UDT devo prelevare i dati...

                    Dim iValoreUDTMinuti As Integer
                    Dim iNrUDTPerConnEthernet As Integer
                    Dim iMinutiMinimiControlloPresenzaDatiDaOraAttuale As Integer
                    Dim strParam77 As String = ""
                    Dim strParam79 As String = ""
                    Dim strParam80 As String = ""
                    Dim iParam121 As Integer

                    iValoreUDTMinuti = GetLIPCLPCVALMEMODB(dr.Item("LI_ID"), 125)
                    iNrUDTPerConnEthernet = GetLIPCLPCVALMEMODB(dr.Item("LI_ID"), 401)
                    If GetDGI(121, iParam121) = True And iParam121 >= 3 Then
                        iMinutiMinimiControlloPresenzaDatiDaOraAttuale = iValoreUDTMinuti * (iNrUDTPerConnEthernet + iParam121)
                    Else
                        iMinutiMinimiControlloPresenzaDatiDaOraAttuale = iValoreUDTMinuti * (iNrUDTPerConnEthernet + 6)
                    End If

                    If iDiffTotalMinutes >= iMinutiMinimiControlloPresenzaDatiDaOraAttuale Then
                        If dr.Item("LI_AllarmeOfflineInviato") = False Then
                            If SetLIAllarmeOfflineInviato(dr.Item("LI_ID"), True) = True Then
                                ' Invio l'EMail per indicare che sono gia' passati 6 UDT in piu' di quelli previsti
                                ' per l'aggiornamento, senza ricevere dati
                                If GetDGI(77, strParam77) = True Then
                                    SendEMailReportAllarmiDL(dr.Item("LI_ID"), strParam77, "L'ultimo dato correttamente elaborato e' stato ricevuto il: " + dtLastStoredLIValue.ToString(), "", True, False)
                                Else
                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 77, Prefisso Oggetto In caso di allarme di Offline invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                End If
                            End If
                        End If
                    Else
                        If dr.Item("LI_AllarmeOfflineInviato") = True Then ' Per evitare continue scritture inutili
                            SetLIAllarmeOfflineInviato(dr.Item("LI_ID"), False)
                        End If
                    End If

                    ' Verifico se devo mandare i totali. Mando solo 1 email ogni giro...
                    If bSetLIStatMemoDayEseguito = False Then
                        If dr.Item("LI_StatMemoDayEseguito") = False Then
                            If SetLIStatMemoDayEseguito(dr.Item("LI_ID"), True) Then
                                bSetLIStatMemoDayEseguito = True
                            End If

                            Dim dtDataUltimiValoriStatisticiMemorizzati As Date = Date.Parse("01/01/2010 00:00:00")
                            Dim dtDataPrimiValoriDataloggerMemorizzati As Date = Date.Parse("01/01/2010 00:00:00")
                            Dim dtDataPrimiValoriDataloggerMemorizzatiGiornoSucc As Date = Date.Parse("01/01/2010 00:00:00")
                            Dim dtTemp_1 As Date
                            Dim dtTemp_2 As Date

                            ' Ricavo il periodo temporale su cui calcolare i dati statistici
                            ' Prelevo l'ultimo dato statistico memorizzato
                            If PrelevaDataUltimiValoriStatisticiMemorizzati(dr.Item("LI_ID"), dtDataUltimiValoriStatisticiMemorizzati) = True Then
                                dtTemp_1 = dtDataUltimiValoriStatisticiMemorizzati.Date.AddDays(1)
                                ' Verifico se esiste nei dati del Datalogger un valore successivo a questa data e lo prelevo
                                If PrelevaDataPrimiValoriDataloggerMemorizzati(dr.Item("LI_ID"), dtTemp_1, dtDataPrimiValoriDataloggerMemorizzati) = True Then
                                    dtTemp_2 = dtDataPrimiValoriDataloggerMemorizzati.Date.AddDays(1)
                                    ' Verifico se esiste nei dati del Datalogger un valore il giorno successivo, cio' significa che ho a disposizione tutti i dati del giorno precedente
                                    If PrelevaDataPrimiValoriDataloggerMemorizzati(dr.Item("LI_ID"), dtTemp_2, dtDataPrimiValoriDataloggerMemorizzatiGiornoSucc) = True Then
                                        If dtDataPrimiValoriDataloggerMemorizzatiGiornoSucc >= dtTemp_2 And Date.Now > dtTemp_2 Then

                                            Dim bRes As Boolean = False

                                            Dim dtValoriDelGiornoStart As Date
                                            Dim dtValoriDelGiornoStop As Date
                                            Dim iNrSolarimetri As Integer
                                            Dim dblLIPotenzaGestitaKw As Double
                                            Dim dblLIKd As Double
                                            Dim dblEgse As Double
                                            Dim dblHj As Double
                                            Dim dblLISogliaWMQ As Double
                                            Dim dblEref As Double
                                            Dim dbl_EuroTot As Double
                                            Dim dbl_CDC As Double
                                            Dim dbl_CDP_1 As Double
                                            Dim dbl_CDP_2 As Double
                                            Dim dbl_SI As Double
                                            Dim dbl_STR As Double
                                            Dim dbl_INV As Double
                                            Dim dbl_HG As Double
                                            Dim dbl_PR As Double
                                            Dim dbl_MS As Double
                                            Dim dbl_MT As Double

                                            dtValoriDelGiornoStart = dtDataPrimiValoriDataloggerMemorizzati.Date
                                            dtValoriDelGiornoStop = dtValoriDelGiornoStart.AddDays(1)

                                            ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_OPERAZIONE_IN_CORSO, "Del: " + dtValoriDelGiornoStart.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)

                                            ' Ricavo i valori
                                            If GetEnergiaCDC_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDC) = True Then
                                                If GetEnergiaCDP_1_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDP_1) = True Then
                                                    If GetEnergiaCDP_2_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_CDP_2) = True Then
                                                        If GetEnergiaSI_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_SI) = True Then
                                                            If GetEnergiaSTR_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_STR) = True Then
                                                                If GetEnergiaINV_ByLIID(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_INV) = True Then
                                                                    dblLISogliaWMQ = dr.Item("LI_SogliaCalcoloHG")
                                                                    dblLIKd = dr.Item("LI_Kd")
                                                                    dblEgse = (dbl_CDP_1 + dbl_CDP_2)
                                                                    iNrSolarimetri = GetLIPCLPCVALMEMODB(dr.Item("LI_ID"), 6)
                                                                    If GetHj(dr.Item("LI_ID"), iNrSolarimetri, iValoreUDTMinuti, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dblHj) = True Then
                                                                        If GetHG(dr.Item("LI_ID"), iNrSolarimetri, iValoreUDTMinuti, dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_HG) = True Then
                                                                            dblLIPotenzaGestitaKw = dr.Item("LI_PotenzaGestitaKw")
                                                                            dblEref = dblLIPotenzaGestitaKw * dblHj
                                                                            If (dblEref > 0.0) And (dblLIKd > 0.0) Then
                                                                                dbl_PR = Math.Round(((dblEgse / (dblEref * dblLIKd)) * 100.0), 1)
                                                                            Else
                                                                                dbl_PR = Math.Round(0.0, 1)
                                                                            End If
                                                                            ' Irraggiamento medio con soglia w/mq
                                                                            If GetIrraggiamentoMedio(dr.Item("LI_ID"), iNrSolarimetri, dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_MS) = True Then
                                                                                ' Temperatura media con soglia w/mq
                                                                                If GetTemperaturaMedia(dr.Item("LI_ID"), dblLISogliaWMQ, dtValoriDelGiornoStart, dtValoriDelGiornoStop, dbl_MT) = True Then
                                                                                    ' Calcolo la cifra in Euro del guadagno giornaliero
                                                                                    dbl_EuroTot = Math.Round((dblEgse * dr.Item("LI_EuroKwh")), 2)
                                                                                    ' Aggiungo i valori nella Tabella intermedia
                                                                                    If SetStatMemoDay(dr.Item("LI_ID"), dbl_CDC, dbl_CDP_1, dbl_CDP_2, dbl_SI, dbl_STR, dbl_INV, dbl_PR, dbl_HG, dbl_MS, dbl_MT, dblLISogliaWMQ, dtValoriDelGiornoStart) = True Then

                                                                                        ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_OK, "Del: " + dtValoriDelGiornoStart.ToString(), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                                        bRes = True

                                                                                        ' Ricavo i dettagli dei valori da inviare come allegato nell'email
                                                                                        Dim strReport As String
                                                                                        Dim strReportDettagli As String
                                                                                        Dim strReportDettagliFile As String
                                                                                        Dim strDirectoryReportDettagliFile As String
                                                                                        strReportDettagli = ""
                                                                                        strReportDettagliFile = ""
                                                                                        strDirectoryReportDettagliFile = ""
                                                                                        strReportDettagli = GetReportDettagli(dr.Item("LI_ID"), dtValoriDelGiornoStart, dtValoriDelGiornoStop)
                                                                                        ' Se ho dei dettagli da inviare, salvo il file da inviare nella propria directory
                                                                                        If strReportDettagli.Length > 0 Then
                                                                                            strReportDettagliFile = "\ReportDettagli_" + dr.Item("LI_ID").ToString() + "_.txt"
                                                                                            strDirectoryReportDettagliFile = My.Application.Info.DirectoryPath.ToString() + "\ReportDettagli"
                                                                                            di = My.Computer.FileSystem.GetDirectoryInfo(strDirectoryReportDettagliFile)
                                                                                            If di.Exists = False Then
                                                                                                Try
                                                                                                    My.Computer.FileSystem.CreateDirectory(strDirectoryReportDettagliFile)
                                                                                                    My.Computer.FileSystem.WriteAllText(strDirectoryReportDettagliFile + strReportDettagliFile, strReportDettagli, False)
                                                                                                Catch ex As Exception
                                                                                                    strReportDettagli = ""
                                                                                                    strReportDettagliFile = ""
                                                                                                    strDirectoryReportDettagliFile = ""
                                                                                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me, False)
                                                                                                End Try
                                                                                            Else
                                                                                                Try
                                                                                                    My.Computer.FileSystem.WriteAllText(strDirectoryReportDettagliFile + strReportDettagliFile, strReportDettagli, False)
                                                                                                Catch ex As Exception
                                                                                                    strReportDettagli = ""
                                                                                                    strReportDettagliFile = ""
                                                                                                    strDirectoryReportDettagliFile = ""
                                                                                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me, False)
                                                                                                End Try
                                                                                            End If
                                                                                        End If

                                                                                        ' Invio l'EMail con i totali del Giorno
                                                                                        If GetDGI(79, strParam79) = True Then
                                                                                            If dr.Item("LI_EuroKwh") > 0.0 Then
                                                                                                strReport = "Produzione Totale dell’impianto Del Giorno: " + dtValoriDelGiornoStart.Date.ToString() + " Kwh: " + (dbl_CDP_1 + dbl_CDP_2).ToString() + ", 1: Kwh: " + dbl_CDP_1.ToString() + ", 2: Kwh: " + dbl_CDP_2.ToString() + "; Irraggiamento Medio(W/mq) con soglia impostata a " + dblLISogliaWMQ.ToString() + "(W/mq): " + dbl_MS.ToString() + "; Totale € (" + dr.Item("LI_EuroKwh").ToString() + " €Kwh): " + dbl_EuroTot.ToString() + "."
                                                                                            Else
                                                                                                strReport = "Produzione Totale dell’impianto Del Giorno: " + dtValoriDelGiornoStart.Date.ToString() + " Kwh: " + (dbl_CDP_1 + dbl_CDP_2).ToString() + ", 1: Kwh: " + dbl_CDP_1.ToString() + ", 2: Kwh: " + dbl_CDP_2.ToString() + "; Irraggiamento Medio(W/mq) con soglia impostata a " + dblLISogliaWMQ.ToString() + "(W/mq): " + dbl_MS.ToString() + "."
                                                                                            End If
                                                                                            SendEMailReportAllarmiDL(dr.Item("LI_ID"), strParam79, strReport, strDirectoryReportDettagliFile + strReportDettagliFile, False, True)
                                                                                        Else
                                                                                            ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 79, Prefisso Oggetto Produzione Giornaliera invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                                        End If

                                                                                        ' Invio l'EMail con i totali del Mese
                                                                                        Dim dtLIReportDelMeseInviatoData As Date
                                                                                        Dim dtLIReportDelMeseInviatoDataStart As Date
                                                                                        Dim dtLIReportDelMeseInviatoDataStop As Date

                                                                                        dtLIReportDelMeseInviatoData = dr.Item("LI_ReportDelMeseInviatoData")

                                                                                        If (dtValoriDelGiornoStop.Date.Month > dtLIReportDelMeseInviatoData.Date.Month) Or (dtValoriDelGiornoStop.Date.Year > dtLIReportDelMeseInviatoData.Date.Year) Then

                                                                                            dtLIReportDelMeseInviatoDataStop = Date.Parse("01/" + dtValoriDelGiornoStop.Date.Month.ToString() + "/" + dtValoriDelGiornoStop.Date.Year.ToString())
                                                                                            dtLIReportDelMeseInviatoDataStart = dtLIReportDelMeseInviatoDataStop.Date.AddMonths(-1)

                                                                                            If SetLIReportDelMeseInviatoData(dr.Item("LI_ID"), dtLIReportDelMeseInviatoDataStart.Date) = True Then

                                                                                                GetStatMemoDay(dr.Item("LI_ID"), dbl_CDC, dbl_CDP_1, dbl_CDP_2, dbl_SI, dbl_STR, dbl_INV, dbl_PR, dbl_HG, dbl_MS, dbl_MT, dblLISogliaWMQ, dtLIReportDelMeseInviatoDataStart.Date, dtLIReportDelMeseInviatoDataStop.Date)

                                                                                                If GetDGI(80, strParam80) = True Then
                                                                                                    If dr.Item("LI_EuroKwh") > 0.0 Then
                                                                                                        strReport = "Produzione Totale dell’impianto Del Mese: " + dtLIReportDelMeseInviatoDataStart.Date.Month.ToString() + "/" + dtLIReportDelMeseInviatoDataStart.Date.Year.ToString() + " Kwh: " + (dbl_CDP_1 + dbl_CDP_2).ToString() + ", 1: Kwh: " + dbl_CDP_1.ToString() + ", 2: Kwh: " + dbl_CDP_2.ToString() + "; Irraggiamento Medio(W/mq) con soglia impostata a " + dblLISogliaWMQ.ToString() + "(W/mq): " + dbl_MS.ToString() + "; Totale € (" + dr.Item("LI_EuroKwh").ToString() + " €Kwh): " + dbl_EuroTot.ToString() + "."
                                                                                                    Else
                                                                                                        strReport = "Produzione Totale dell’impianto Del Mese: " + dtLIReportDelMeseInviatoDataStart.Date.Month.ToString() + "/" + dtLIReportDelMeseInviatoDataStart.Date.Year.ToString() + " Kwh: " + (dbl_CDP_1 + dbl_CDP_2).ToString() + ", 1: Kwh: " + dbl_CDP_1.ToString() + ", 2: Kwh: " + dbl_CDP_2.ToString() + "; Irraggiamento Medio(W/mq) con soglia impostata a " + dblLISogliaWMQ.ToString() + "(W/mq): " + dbl_MS.ToString() + "."
                                                                                                    End If
                                                                                                    SendEMailReportAllarmiDL(dr.Item("LI_ID"), strParam80, strReport, "", False, True)
                                                                                                Else
                                                                                                    ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 80, Prefisso Oggetto Produzione Mensile invio allarmi/report, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                                                                                End If

                                                                                            End If

                                                                                        End If

                                                                                        ' Eseguo La cancellazione del Log DB
                                                                                        Dim iParam101 As Integer
                                                                                        If GetDGI(101, iParam101) = True Then
                                                                                            dtStartDeleteLogDB = dtValoriDelGiornoStart.AddDays(-iParam101)
                                                                                            DeleteLogEventi(dr.Item("LI_ID"), dtStartDeleteLogDB)
                                                                                        End If

                                                                                        '' Eseguo La cancellazione dei dati storici 
                                                                                        Dim iParam111 As Integer
                                                                                        If GetDGI(111, iParam111) = True Then
                                                                                            Dim dtStartDelete As Date
                                                                                            dtStartDelete = dtValoriDelGiornoStart.AddMonths(-iParam111)
                                                                                            ' Logger Installati
                                                                                            DeleteLoggerInstData(dr.Item("LI_ID"), dtStartDelete)
                                                                                            ' Schede IT
                                                                                            If IsInverterTesterConfigured(dr.Item("LI_ID")) = True Then
                                                                                                DeleteInverterTesterData(dr.Item("LI_ID"), dtStartDelete)
                                                                                            End If
                                                                                            ' Schede ST
                                                                                            If IsStringTesterConfigured(dr.Item("LI_ID")) = True Then
                                                                                                DeleteStringTesterData(dr.Item("LI_ID"), dtStartDelete)
                                                                                            End If
                                                                                        End If

                                                                                        ' Compatto il Database
                                                                                        CompattaDB(dr.Item("LI_ID"))
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If bRes = False Then
                                                ScriviLogEventi(dr.Item("LI_ID"), 0, AZIONE_SERVER_INSERISCI_DATI_STATISTICI, RISULTATO_ERR, "Almeno una operazione non e' stata eseguita.", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                ' Ricomincio il giro
                If bSetLIStatMemoDayEseguito = False Then
                    SetAllLIStatMemoDayEseguito(False)
                End If

            End If

            ' Ogni volta che cambia data, eseguo queste operazioni:
            ' Devo pero' sincerami che siano stati generati tutti i dati statistici.
            ' Alle 6:00 dovrebbero essere stati eseguiti tutti i dati statistici
            If dtStatNowDay.DayOfYear <> Date.Now.DayOfYear Then
                If Date.Now.Hour >= 6 Then

                    ' Trigger
                    dtStatNowDay = Date.Now

                    ' Imposto l'intervallo temporale
                    dtStopDay = Date.Now.Date
                    dtStartDay = dtStopDay.AddDays(-1)

                    ' Reset Trigger per tutti i DL
                    DeleteAllDataLoggerAllarmeInviato()

                    ' Reset Trigger Offline per tutti i DL
                    ResetAllLIAllarmeOfflineInviato()

                    ' Invio resoconto giornaliero
                    Dim strParam81 As String = ""
                    Dim strParam82 As String = ""
                    Dim strParam86 As String = ""

                    If GetDGI(81, strParam81) = True Then
                        If GetDGI(82, strParam82) = True Then
                            If GetDGI(86, strParam86) = True Then
                                strReportSemplificatoGiornoDiTuttiGliImpianti = PrelevaDatiDiControlloSemplificatiDiTuttiGliImpiantiAsString(dtStartDay, dtStopDay)
                                SendEMail(0, strParam81, strParam82, strParam86, strReportSemplificatoGiornoDiTuttiGliImpianti, "")
                            Else
                                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 86, Oggetto Email per invio report automatico giornaliero semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                            End If
                        Else
                            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 82, Destinatario Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    Else
                        ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 81, Mittente Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                    ' Elimino il Log fino a xxx giorni prima
                    Dim iParam101 As Integer
                    If GetDGI(101, iParam101) = True Then
                        ' Eseguo La cancellazione del Log DB riferita all'ID 0
                        dtStartDeleteLogDB = Date.Now.AddDays(-iParam101)
                        DeleteLogEventi(0, dtStartDeleteLogDB)

                        ' Eseguo La cancellazione del Log Directory
                        dtStartDeleteLogFiles = Date.Now.AddDays(-iParam101)
                        For Each strSDPN As String In My.Computer.FileSystem.GetDirectories(My.Application.Info.DirectoryPath.ToString())
                            di = My.Computer.FileSystem.GetDirectoryInfo(strSDPN)
                            If di.Name.Contains("Logs_") Then
                                If di.CreationTime < dtStartDeleteLogFiles Then
                                    Try
                                        My.Computer.FileSystem.DeleteDirectory(strSDPN, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_OK, "Eliminazione Automatica delle Directory dello storico del Log fino al: " + dtStartDeleteLogFiles.ToString("G"), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                    Catch ex As Exception
                                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                                    End Try
                                End If
                            End If
                        Next
                    Else
                        ScriviLogEventi(0, 0, AZIONE_DEL, RISULTATO_ERR, "Parametro 101, Buffer Log Espresso in giorni, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    End If

                    ' Compatto il Database
                    CompattaDB(0)
                End If
            End If

            ' Ogni volta che cambia mese, eseguo queste operazioni:
            ' Dalle 7:00 in poi...
            If dtStatNowMounth.Month <> Date.Now.Month Then
                If Date.Now.Hour >= 7 Then
                    ' Trigger
                    dtStatNowMounth = Date.Now

                    ' Imposto l'intervallo temporale
                    dtStopMounth = Date.Now.Date
                    dtStartMounth = dtStopMounth.AddMonths(-1)

                    ' Invio resoconto mensile
                    Dim strParam81 As String = ""
                    Dim strParam82 As String = ""
                    Dim strParam88 As String = ""

                    If GetDGI(81, strParam81) = True Then
                        If GetDGI(82, strParam82) = True Then
                            If GetDGI(88, strParam88) = True Then
                                strReportSemplificatoMeseDiTuttiGliImpianti = PrelevaDatiDiControlloSemplificatiDiTuttiGliImpiantiAsString(dtStartMounth, dtStopMounth)
                                SendEMail(0, strParam81, strParam82, strParam88, strReportSemplificatoMeseDiTuttiGliImpianti, "")
                            Else
                                ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 88, Oggetto Email per invio report automatico mensile semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                            End If
                        Else
                            ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 82, Destinatario Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                        End If
                    Else
                        ScriviLogEventi(0, 0, AZIONE_SERVER_INVIO_EMAIL, RISULTATO_ERR, "Parametro 81, Mittente Email per invio report semplificato di tutti gli impianti, Non Impostato", "", "", DEFAULT_OPERATOR_ID, Nothing, False)
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Nothing, False)
        End Try

        TimerCheckDL.Start()

    End Sub

    Private Shared Function TCPIPListenDisconnected(ByVal tl As Net.Sockets.TcpListener) As Boolean
        If tl.Server.IsBound = False Then
            Return True
        End If
        Return False
    End Function

    Private Shared Function TCPIPClientDisconnected(ByVal tcpipc As TCPIPClient) As Boolean
        If Not tcpipc.TC Is Nothing Then
            'If tcpipc.TC.Connected = False Then
            '    tcpipc = Nothing

            '    Return True
            'End If
        Else
            tcpipc = Nothing

            Return True
        End If
        Return False
    End Function

    Private Sub SelezionaCollegaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelezionaCollegaToolStripMenuItem1.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("Seleziona/CollegaDatabase")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            Dim sdc As New StringaDiConnessione
            sdc.TextBox_Conn_String.Text = My.Settings.ConnectionString
            sdc.TextBox_SQLCmdTimeout.Text = My.Settings.SQLCmdTimeout
            dr = sdc.ShowDialog(Me)
            If dr = Windows.Forms.DialogResult.OK Then
                My.Settings.ConnectionString = sdc.TextBox_Conn_String.Text
                Try
                    My.Settings.SQLCmdTimeout = CInt(sdc.TextBox_SQLCmdTimeout.Text)
                Catch ex As Exception
                End Try
                My.Settings.Save()
            End If
            sdc.Dispose()
        End If

        lg.Dispose()
    End Sub

    Private Sub VisualizzaUtentiConnessiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisualizzaUtentiConnessiToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("VisualizzaUtentiCollegatiAlDB")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            VisualizzaUtentiCollegatiAlDB.Close()
            VisualizzaUtentiCollegatiAlDB.UID = m_iUID
            VisualizzaUtentiCollegatiAlDB.MdiParent = Me
            VisualizzaUtentiCollegatiAlDB.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub PannelloFotovoltaicoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PannelloFotovoltaicoToolStripMenuItem.Click

        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("PannelloFotov")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            PannelloFotov.Close()
            PannelloFotov.UID = m_iUID
            PannelloFotov.MdiParent = Me
            PannelloFotov.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub InverterFotovoltaicoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InverterFotovoltaicoToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("InverterFotov")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            InverterFotov.Close()
            InverterFotov.UID = m_iUID
            InverterFotov.MdiParent = Me
            InverterFotov.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ContatoreDiProduzioneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContatoreDiProduzioneToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("ContatoreDiProduzione")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            ContatoreDiProduzione.Close()
            ContatoreDiProduzione.UID = m_iUID
            ContatoreDiProduzione.MdiParent = Me
            ContatoreDiProduzione.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub LoggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoggerToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("Logger")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            Logger.Close()
            Logger.UID = m_iUID
            Logger.MdiParent = Me
            Logger.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub StringTesterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StringTesterToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("StringTester")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            StringTester.Close()
            StringTester.UID = m_iUID
            StringTester.MdiParent = Me
            StringTester.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub InverterTesterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InverterTesterToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("InverterTester")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            InverterTester.Close()
            InverterTester.UID = m_iUID
            InverterTester.MdiParent = Me
            InverterTester.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub IngressiDiMisuraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IngressiDiMisuraToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("IngressoTipo")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            IngressoTipo.Close()
            IngressoTipo.UID = m_iUID
            IngressoTipo.MdiParent = Me
            IngressoTipo.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ClienteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClienteToolStripMenuItem1.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("Cliente")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            Cliente.Close()
            Cliente.UID = m_iUID
            Cliente.MdiParent = Me
            Cliente.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ImpiantoDiProduzioneToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImpiantoDiProduzioneToolStripMenuItem1.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("ImpiantoDiProduzione")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            ImpiantoDiProduzione.Close()
            ImpiantoDiProduzione.UID = m_iUID
            ImpiantoDiProduzione.MdiParent = Me
            ImpiantoDiProduzione.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ContatoreDiProduzioneToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContatoreDiProduzioneToolStripMenuItem1.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("ContatoreDiProduzioneInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            ContatoreDiProduzioneInst.Close()
            ContatoreDiProduzioneInst.UID = m_iUID
            ContatoreDiProduzioneInst.MdiParent = Me
            ContatoreDiProduzioneInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub InverterFotovoltaiciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InverterFotovoltaiciToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("InverterFotovInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            InverterFotovInst.Close()
            InverterFotovInst.UID = m_iUID
            InverterFotovInst.MdiParent = Me
            InverterFotovInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub StringheDiPannelliFotovoltaiciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StringheDiPannelliFotovoltaiciToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("PannelloFotovString")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            PannelloFotovString.Close()
            PannelloFotovString.UID = m_iUID
            PannelloFotovString.MdiParent = Me
            PannelloFotovString.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub PannelliFotovoltaiciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PannelliFotovoltaiciToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("PannelloFotovInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            PannelloFotovInst.Close()
            PannelloFotovInst.UID = m_iUID
            PannelloFotovInst.MdiParent = Me
            PannelloFotovInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub LoggerInstallatiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoggerInstallatiToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("LoggerInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            LoggerInst.Close()
            LoggerInst.UID = m_iUID
            LoggerInst.MdiParent = Me
            LoggerInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub InverterTesterInstallatiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InverterTesterInstallatiToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("InverterTesterInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            InverterTesterInst.Close()
            InverterTesterInst.UID = m_iUID
            InverterTesterInst.MdiParent = Me
            InverterTesterInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub StringTesterInstallatiToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StringTesterInstallatiToolStripMenuItem1.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("StringTesterInst")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            StringTesterInst.Close()
            StringTesterInst.UID = m_iUID
            StringTesterInst.MdiParent = Me
            StringTesterInst.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ImpiantoDiProduzioneLoggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImpiantoDiProduzioneLoggerToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("LoggerInst_X_ImpiantoDiProduzione_X_Config")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            LoggerInst_X_ImpiantoDiProduzione_X_Config.Close()
            LoggerInst_X_ImpiantoDiProduzione_X_Config.UID = m_iUID
            LoggerInst_X_ImpiantoDiProduzione_X_Config.MdiParent = Me
            LoggerInst_X_ImpiantoDiProduzione_X_Config.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub InverterFotovoltaiciInverterTesterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InverterFotovoltaiciInverterTesterToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("InverterTesterInst_X_InverterFotovInst_X_Config")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            InverterTesterInst_X_InverterFotovInst_X_Config.Close()
            InverterTesterInst_X_InverterFotovInst_X_Config.UID = m_iUID
            InverterTesterInst_X_InverterFotovInst_X_Config.MdiParent = Me
            InverterTesterInst_X_InverterFotovInst_X_Config.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StringheDiPannelliFotovoltaiciStringTesterToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("StringTesterInst_X_PannelloFotovString_X_Config")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            StringTesterInst_X_PannelloFotovString_X_Config.Close()
            StringTesterInst_X_PannelloFotovString_X_Config.UID = m_iUID
            StringTesterInst_X_PannelloFotovString_X_Config.MdiParent = Me
            StringTesterInst_X_PannelloFotovString_X_Config.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub EMailLoggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EMailLoggerToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("LoggerInst_X_IndEMailSuAll")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            LoggerInst_X_IndEMailSuAll.Close()
            LoggerInst_X_IndEMailSuAll.UID = m_iUID
            LoggerInst_X_IndEMailSuAll.MdiParent = Me
            LoggerInst_X_IndEMailSuAll.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub CompattaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompattaToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = GetMinLevelReq("FUNC_CompattaDB(...)")
        dr = lg.ShowDialog(Me)
        m_iUID = lg.UID
        If dr = Windows.Forms.DialogResult.Yes Then
            CompattaDB(0)
        End If

        lg.Dispose()
    End Sub

    Private Sub DatiGeneraliImpiantoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatiGeneraliImpiantoToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = 0
        lg.ULivello = GetMinLevelReq("DatiGeneraliImpianto")
        dr = lg.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.Yes Then
            DatiGeneraliImpianto.Close()
            DatiGeneraliImpianto.UID = lg.UID
            DatiGeneraliImpianto.MdiParent = Me
            DatiGeneraliImpianto.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ConfiguraParametriLoggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfiguraParametriLoggerToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = 0
        lg.ULivello = GetMinLevelReq("LoggerParamConfig")
        dr = lg.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.Yes Then
            LoggerParamConfig.Close()
            LoggerParamConfig.UID = lg.UID
            LoggerParamConfig.MdiParent = Me
            LoggerParamConfig.Show()
        End If

        lg.Dispose()
    End Sub

    Private Sub ConfiguraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfiguraToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        Dim ce As New ConfigurazioneComunicazione
        ' Avvio il server solo se abilitato
        If My.Settings.Server = True Then

            lg.UID = m_iUID
            lg.ULivello = GetMinLevelReq("ConfigurazioneComunicazione")
            dr = lg.ShowDialog(Me)
            m_iUID = lg.UID
            If dr = Windows.Forms.DialogResult.Yes Then
                ce.UID = m_iUID
                dr = ce.ShowDialog(Me)
                If dr = Windows.Forms.DialogResult.OK Then
                    ArrestaServerTCPIP()
                    AvviaServerTCPIP()

                    ArrestaServerComm()
                    AvviaServerComm()
                End If
            End If
        Else
            System.Windows.Forms.MessageBox.Show(Owner, "Server non abilitato.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        lg.Dispose()

    End Sub

    Private Sub LoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        lg.UID = m_iUID
        lg.ULivello = 1
        dr = lg.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.Yes Then
            m_iUID = lg.UID
        End If
        lg.Dispose()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        m_iUID = 0
        ScriviLogEventi(0, 0, AZIONE_LOGOUT, RISULTATO_OK, "", "", "", m_iUID, Me)
    End Sub

    Private Sub VisualizzaLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisualizzaLogToolStripMenuItem.Click
        log.Close()
        log.UID = m_iUID
        log.MdiParent = Me
        log.Show()
    End Sub

    Private Sub AggiornaManualmenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AggiornaManualmenteToolStripMenuItem.Click
        Dim lg As New Login
        Dim dr As Windows.Forms.DialogResult
        ' Avvio il server solo se abilitato
        If My.Settings.Server = True Then

            lg.UID = m_iUID
            lg.ULivello = GetMinLevelReq("AggiornaManualmenteDati")
            dr = lg.ShowDialog(Me)
            m_iUID = lg.UID
            If dr = Windows.Forms.DialogResult.Yes Then
                AggiornaManualmenteDati.Close()
                AggiornaManualmenteDati.UID = m_iUID
                AggiornaManualmenteDati.TCPIPCL = m_ltcpipClient
                AggiornaManualmenteDati.MdiParent = Me
                AggiornaManualmenteDati.Show()
            End If
        Else
            System.Windows.Forms.MessageBox.Show(Owner, "Server non abilitato.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        lg.Dispose()

    End Sub

    Private Sub VisualizzaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisualizzaToolStripMenuItem.Click
        VisDatiDataLogger.Close()
        VisDatiDataLogger.UID = m_iUID
        VisDatiDataLogger.MdiParent = Me
        VisDatiDataLogger.Show()
    End Sub

    Private Sub ToolStripMenuItem_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_Info.Click
        Dim strDatabaseRel As String = ""
        GetDGI(1, strDatabaseRel)
        System.Windows.Forms.MessageBox.Show(Owner, "GEM Versione: " + My.Application.Info.Version.ToString() + "." + vbCrLf + "Database Versione: " + strDatabaseRel + "." + vbCrLf + "Database Dimensione: " + DimensioneDatabase() + "." + vbCrLf + "www.fasenet.com" + vbCrLf + vbCrLf + vbCrLf + vbCrLf + "Sviluppato per Fase Engineering Da:" + vbCrLf + "Pretolesi Riccardo - www.consulenzeperizie.it - www.pretolesi.com", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub FinestraToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownOpening

        For Each frm As System.Windows.Forms.Form In Me.MdiChildren
            Dim tsmi As New ToolStripMenuItem
            tsmi.Text = frm.Text
            tsmi.Name = frm.Name
            tsmi.Tag = frm
            Me.FinestraToolStripMenuItem.DropDownItems.Add(tsmi)
            AddHandler tsmi.Click, AddressOf FinestraToolStripMenuItem_Click
        Next
    End Sub

    Private Sub FinestraToolStripMenuItem_DropDownClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownClosed
        For Each frm As System.Windows.Forms.Form In Me.MdiChildren
            Me.FinestraToolStripMenuItem.DropDownItems.RemoveByKey(frm.Name)
        Next
    End Sub

    Private Sub FinestraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not sender Is Nothing Then
            If sender.GetType().Name = "ToolStripMenuItem" Then
                If Not sender.Tag Is Nothing Then
                    If Not sender.Tag.GetType().GetMethod("Focus") Is Nothing Then
                        sender.Tag.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SovrapponiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SovrapponiToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub AffiancaOrizzontalmenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AffiancaOrizzontalmenteToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub AffiancaVerticalmenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AffiancaVerticalmenteToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub VisualizzaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinestraToolStripMenuItem.DropDownOpening
        Dim bAllMinimized As Boolean

        bAllMinimized = True
        If Me.MdiChildren.Length = 0 Then
            SovrapponiToolStripMenuItem.Enabled = False
            AffiancaOrizzontalmenteToolStripMenuItem.Enabled = False
            AffiancaVerticalmenteToolStripMenuItem.Enabled = False
        Else
            For Each swf As Form In MdiChildren
                If swf.WindowState = FormWindowState.Maximized Or swf.WindowState = FormWindowState.Normal Then
                    bAllMinimized = False
                End If
            Next
            If bAllMinimized = False Then
                SovrapponiToolStripMenuItem.Enabled = True
                AffiancaOrizzontalmenteToolStripMenuItem.Enabled = True
                AffiancaVerticalmenteToolStripMenuItem.Enabled = True
            Else
                SovrapponiToolStripMenuItem.Enabled = False
                AffiancaOrizzontalmenteToolStripMenuItem.Enabled = False
                AffiancaVerticalmenteToolStripMenuItem.Enabled = False
            End If
        End If
    End Sub

End Class
