Public Class ConfigurazioneComunicazione
    Private m_iUID As Integer

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Private Sub ConfigurazioneComunicazione_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim iTotNrTCPITAddr As Integer
        Dim strTCPITAddr As String = ""
        Dim bCommDebugEnable As Boolean
        Dim strCommPortName As String
        Dim iReadTimeout As Integer

        For Each nipa As Net.IPAddress In System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList
            ListBox_TCPIP_Addr_Disp.Items.Add(nipa.ToString())
        Next
        If GetDGI(30, iTotNrTCPITAddr) = True Then
            If iTotNrTCPITAddr > 0 Then
                For iIndice = 0 To iTotNrTCPITAddr - 1
                    If GetDGI((31 + iIndice), strTCPITAddr) = True Then
                        If ListBox_TCPIP_Addr_Disp.Items.Contains(strTCPITAddr) = True Then
                            ListBox_TCPIP_Addr_Disp.Items.Remove(strTCPITAddr)
                            ListBox_TCPIP_Addr_InUse.Items.Add(strTCPITAddr)
                        End If
                    End If
                Next
            End If
        End If

        If ListBox_TCPIP_Addr_Disp.Items.Count = 0 Then
            Button_From_Disp_To_InUse.Enabled = False
        Else
            Button_From_Disp_To_InUse.Enabled = True
        End If
        If ListBox_TCPIP_Addr_InUse.Items.Count = 0 Then
            Button_From_InUse_To_Disp.Enabled = False
        Else
            Button_From_InUse_To_Disp.Enabled = True
        End If

        GetDGI(51, TextBox_TCPIP_Port_InUse.Text)
        TextBox_TCPIP_Port_Disp.Text = TextBox_TCPIP_Port_InUse.Text

        Dim strPortsName() As String
        strPortsName = System.IO.Ports.SerialPort.GetPortNames()
        For Each str_1 As String In strPortsName
            ComboBox_Com_Port_Disp.Items.Add(str_1)
        Next
        If ComboBox_Com_Port_Disp.Items.Count > 0 Then
            ComboBox_Com_Port_Disp.SelectedIndex = 0
        End If

        ' Porta Seriale Per Debug Comunicazione Datalogger verso le periferiche
        bCommDebugEnable = False
        strCommPortName = ""
        GetDGI(10, bCommDebugEnable)
        GetDGI(11, strCommPortName)
        GetDGI(21, iReadTimeout)
        CheckBox_Abilita_Debug_DataloggerDevice.Checked = bCommDebugEnable
        If bCommDebugEnable = False Then
            ComboBox_Com_Port_Disp.Enabled = False
        Else
            ComboBox_Com_Port_Disp.Enabled = True
        End If
        ComboBox_Com_Port_Disp.Text = strCommPortName
        TextBox_ReadTimeout.Text = iReadTimeout.ToString()

    End Sub

    Private Sub CheckBox_Abilita_Debug_DataloggerDevice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Abilita_Debug_DataloggerDevice.CheckedChanged
        If CheckBox_Abilita_Debug_DataloggerDevice.Checked = False Then
            ComboBox_Com_Port_Disp.Enabled = False
            TextBox_ReadTimeout.Enabled = False
        Else
            ComboBox_Com_Port_Disp.Enabled = True
            TextBox_ReadTimeout.Enabled = True
        End If
    End Sub

    Private Sub Button_From_Disp_To_InUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_From_Disp_To_InUse.Click
        For Each obj As Object In ListBox_TCPIP_Addr_Disp.SelectedItems
            ListBox_TCPIP_Addr_InUse.Items.Add(obj)
        Next
        For Each obj As Object In ListBox_TCPIP_Addr_InUse.Items
            ListBox_TCPIP_Addr_Disp.Items.Remove(obj)
        Next
        If ListBox_TCPIP_Addr_Disp.Items.Count = 0 Then
            Button_From_Disp_To_InUse.Enabled = False
        Else
            Button_From_Disp_To_InUse.Enabled = True
        End If
        If ListBox_TCPIP_Addr_InUse.Items.Count = 0 Then
            Button_From_InUse_To_Disp.Enabled = False
        Else
            Button_From_InUse_To_Disp.Enabled = True
        End If
    End Sub

    Private Sub Button_From_InUse_To_Disp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_From_InUse_To_Disp.Click
        For Each obj As Object In ListBox_TCPIP_Addr_InUse.SelectedItems
            ListBox_TCPIP_Addr_Disp.Items.Add(obj)
        Next
        For Each obj As Object In ListBox_TCPIP_Addr_Disp.Items
            ListBox_TCPIP_Addr_InUse.Items.Remove(obj)
        Next
        If ListBox_TCPIP_Addr_Disp.Items.Count = 0 Then
            Button_From_Disp_To_InUse.Enabled = False
        Else
            Button_From_Disp_To_InUse.Enabled = True
        End If
        If ListBox_TCPIP_Addr_InUse.Items.Count = 0 Then
            Button_From_InUse_To_Disp.Enabled = False
        Else
            Button_From_InUse_To_Disp.Enabled = True
        End If
    End Sub

    Private Sub Button_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Ok.Click
        Dim iIndice As Integer
        ' Setto il nr di porte selezionate
        SetDGI(30, ListBox_TCPIP_Addr_InUse.Items.Count, m_iUID)
        iIndice = 0
        For Each obj As Object In ListBox_TCPIP_Addr_InUse.Items
            If SetDGI((31 + iIndice), obj, m_iUID) = False Then
                Exit For
            End If
            iIndice = iIndice + 1
        Next

        SetDGI(51, TextBox_TCPIP_Port_Disp.Text, m_iUID)

        ' Seriale per debug
        SetDGI(10, CheckBox_Abilita_Debug_DataloggerDevice.Checked, m_iUID)
        SetDGI(11, ComboBox_Com_Port_Disp.Text, m_iUID)
        If IsNumeric(TextBox_ReadTimeout.Text) = True Then
            SetDGI(21, TextBox_ReadTimeout.Text, m_iUID)
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class