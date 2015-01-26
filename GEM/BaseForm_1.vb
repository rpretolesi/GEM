Imports System.Data.SqlClient
Imports System.Globalization

Public Class BaseForm_1

    Protected m_iUID As Integer

    Private m_bs As New BindingSource
    Private m_strReportHeader As String

    Property BS() As BindingSource
        Get
            Return m_bs
        End Get

        Set(ByVal BS As BindingSource)
            m_bs = BS
            With Me.DataGridView
                .DataSource = Me.m_bs.DataSource
            End With

            ' Settaggio data grid
            SetDataGrid()

            ' Impostazione Tool Strip e Status Label
            Set_ToolStripButtonStatusLabel()

        End Set
    End Property

    Property ReportHeader() As String
        Get
            Return m_strReportHeader
        End Get

        Set(ByVal ReportHeader As String)
            m_strReportHeader = ReportHeader
        End Set
    End Property

    Protected Overridable Sub BaseForm_1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.DesignMode = False Then

                ' Settaggio data grid
                SetDataGrid()

                ' Impostazione Tool Strip e Status Label
                Set_ToolStripButtonStatusLabel()

            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try
    End Sub

    Private Sub SetDataGrid()
        Try
            With Me.DataGridView

                .DataSource = Me.m_bs.DataSource

                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .RowHeadersVisible = True
                .AutoGenerateColumns = True

                For iIndice_1 = 0 To .Columns.Count - 1
                    .Columns(iIndice_1).Visible = True
                Next iIndice_1

                If Not m_bs.DataSource Is Nothing Then
                    For iIndice_1 = 0 To .Columns.Count - 1
                        If m_bs.DataSource.Columns(iIndice_1).ColumnName.ToString = m_bs.DataSource.Columns(iIndice_1).Caption.ToString Then
                            .Columns(iIndice_1).Visible = False
                        Else
                            .Columns(iIndice_1).HeaderText = m_bs.DataSource.Columns(iIndice_1).Caption
                        End If
                    Next iIndice_1
                End If
            End With

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try
    End Sub

    Protected Overridable Sub BaseForm_1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub

    Protected Overridable Sub BaseForm_1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.DesignMode = False Then
                Static bOneShot As Boolean
                Static szFormOriginalSize As Size

                Static szDataGridViewOriginalSize As Size
                Static ptDataGridViewOriginalLocation As Point

                If bOneShot = False Then
                    bOneShot = True
                    szFormOriginalSize = Me.Size

                    szDataGridViewOriginalSize = Me.DataGridView.Size

                    ptDataGridViewOriginalLocation = Me.DataGridView.Location

                End If

                Me.DataGridView.Height = szDataGridViewOriginalSize.Height - (szFormOriginalSize.Height - Me.Height)
                Me.DataGridView.Width = szDataGridViewOriginalSize.Width - (szFormOriginalSize.Width - Me.Width)

            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try
    End Sub

    Protected Overridable Sub DataGridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick

    End Sub

    Protected Overridable Sub DataGridView_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView.RowHeaderMouseDoubleClick

    End Sub

    Private Sub DataGridView_ColumnHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView.ColumnHeaderMouseDoubleClick
        DataGridView.Columns(e.ColumnIndex).Visible = False
    End Sub

    Protected Overridable Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView.SelectionChanged
        Try
            If Me.DesignMode = False Then
                If DataGridView.SelectedRows.Count > 0 Then
                    If DataGridView.SelectedRows(0).Index >= 0 Then
                        If Not m_bs.DataSource Is Nothing Then
                            If m_bs.DataSource.Rows.Count > DataGridView.SelectedRows(0).Index Then
                                Me.m_bs.Position = DataGridView.SelectedRows(0).Index
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_GENERICA, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", m_iUID, Me)
        End Try
    End Sub

    ' Eventi binding navigator
    Protected Overridable Sub ToolStripButton_Nuovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Nuovo.Click
        If Me.DesignMode = False Then

            Me.ToolStripButton_Nuovo.Enabled = False
            Me.ToolStripButton_Elimina.Enabled = False
            Me.ToolStripButton_Modifica.Enabled = False
            Me.ToolStripButton_Salva.Enabled = True
            Me.ToolStripButton_Annulla.Enabled = True

            Me.DataGridView.Enabled = False

            If Not m_bs.DataSource Is Nothing Then
                ToolStripStatusLabel_Nr_Record.Text = m_bs.DataSource.Rows.Count.ToString() + " Righe"
            End If

        End If
    End Sub

    Protected Overridable Sub ToolStripButton_Elimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Elimina.Click
        If Me.DesignMode = False Then

            ' Impostazione Tool Strip e Status Label
            Set_ToolStripButtonStatusLabel()

        End If
    End Sub

    Protected Overridable Sub ToolStripButton_Modifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Modifica.Click
        If Me.DesignMode = False Then

            Me.ToolStripButton_Nuovo.Enabled = False
            Me.ToolStripButton_Elimina.Enabled = False
            Me.ToolStripButton_Modifica.Enabled = False
            Me.ToolStripButton_Salva.Enabled = True
            Me.ToolStripButton_Annulla.Enabled = True

            Me.DataGridView.Enabled = False
        End If
    End Sub

    Protected Overridable Sub ToolStripButton_Salva_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Salva.Click
        If Me.DesignMode = False Then

            ' Impostazione Tool Strip e Status Label
            Set_ToolStripButtonStatusLabel()

        End If
    End Sub

    Protected Overridable Sub ToolStripButton_Annulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Annulla.Click
        If Me.DesignMode = False Then

            ' Impostazione Tool Strip e Status Label
            Set_ToolStripButtonStatusLabel()

        End If
    End Sub

    Private Sub ToolStripButtonSelezionaTutto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonSelezionaTutto.Click
        If Me.DesignMode = False Then
            Me.DataGridView.SelectAll()
        End If
    End Sub

    Private Sub ToolStripButton_Stampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Stampa.Click
        Dim iIndice_1 As Integer
        Dim iIndice_2 As Integer
        Dim strHTML As String
        Dim strNomeFile As String
        Dim iPrintedRecord As Integer

        strHTML = ""
        strNomeFile = My.Application.Info.DirectoryPath.ToString() + "\" + Me.Text + ".html"
        Try
            My.Computer.FileSystem.WriteAllText(strNomeFile, strHTML, False)
            strHTML = ""

        Catch ex As Exception
            'AddLogEvent(LOG_ERROR, ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_ldLoginData, m_strFormTitle, m_strConnStringParamName)
            'System.Windows.Forms.MessageBox.Show(ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_strFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End Try

        strHTML = "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0//EN""" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + """http://www.w3.org/TR/REC-html140/strict.dtd"">" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<HTML>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<HEAD>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<TITLE>" + " Fase Engineering " + "</TITLE>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "</HEAD>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<BODY>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + " Fase Engineering " + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<IMG src=""" + "file:///" + My.Application.Info.DirectoryPath.ToString() + "\LogoFase.jpg"" align=""center""> " + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.fasenet.com/"">Fase Engineering - www.fasenet.com</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        If m_iUID = 0 Then
            m_iUID = DEFAULT_OPERATOR_ID
        End If
        strHTML = strHTML + "<P1>" + "Elaborato il: " + Date.Now.ToString + " - Da: " + GENERICA_DESCRIZIONE("U_NomeCognome", "Utente", "U_ID", m_iUID, m_iUID).ToString() + "</P1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf

        ' Se ho un'intestazione, la stampo
        If m_strReportHeader <> "" Then
            strHTML = strHTML + "<P1>" + "Dati Riferiti A: " + m_strReportHeader + "</P1>" + Microsoft.VisualBasic.vbCrLf
            strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        End If

        ' Verifico che ci sia qualcosa di selezionato
        iPrintedRecord = 0
        If Not Me.DataGridView.CurrentRow Is Nothing Then
            strHTML = strHTML + "<TABLE BORDER=1 WIDTH=100%>" + Microsoft.VisualBasic.vbCrLf

            strHTML = strHTML + "<TR>" + Microsoft.VisualBasic.vbCrLf
            For iIndice_1 = 0 To Me.DataGridView.ColumnCount - 1
                If Me.DataGridView.Columns(iIndice_1).Visible = True Then
                    strHTML = strHTML + "<TD>"
                    strHTML = strHTML + Me.DataGridView.Columns(iIndice_1).HeaderText.ToString
                    strHTML = strHTML + "</TD>" + Microsoft.VisualBasic.vbCrLf
                End If
            Next iIndice_1
            strHTML = strHTML + "</TR>" + Microsoft.VisualBasic.vbCrLf
            If Me.DataGridView.SelectedRows.Count > 0 Then
                For iIndice_1 = 0 To Me.DataGridView.SelectedRows.Count - 1
                    strHTML = strHTML + "<TR>" + Microsoft.VisualBasic.vbCrLf
                    For iIndice_2 = 0 To Me.DataGridView.ColumnCount - 1
                        If Me.DataGridView.Columns(iIndice_2).Visible = True Then
                            strHTML = strHTML + "<TD>"
                            strHTML = strHTML + Me.DataGridView.SelectedRows.Item((Me.DataGridView.SelectedRows.Count - 1) - iIndice_1).Cells(iIndice_2).Value.ToString
                            strHTML = strHTML + "</TD>" + Microsoft.VisualBasic.vbCrLf
                        End If
                    Next iIndice_2
                    strHTML = strHTML + "</TR>" + Microsoft.VisualBasic.vbCrLf

                    Try
                        My.Computer.FileSystem.WriteAllText(strNomeFile, strHTML, True)
                        strHTML = ""

                    Catch ex As Exception
                        'AddLogEvent(LOG_ERROR, ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_ldLoginData, m_strFormTitle, m_strConnStringParamName)
                        'System.Windows.Forms.MessageBox.Show(ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_strFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End Try

                Next iIndice_1
                iPrintedRecord = Me.DataGridView.SelectedRows.Count.ToString()
            End If

            strHTML = strHTML + "</TABLE>" + Microsoft.VisualBasic.vbCrLf
        End If

        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<P1>" + "Nr Record: " + iPrintedRecord.ToString() + "</P1>" + Microsoft.VisualBasic.vbCrLf

        strHTML = strHTML + "<DIV align=center>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<IMG src=""" + "file:///" + My.Application.Info.DirectoryPath.ToString() + "\LogoFase.jpg"" align=""center""> " + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<P1>" + "Fine Documento." + "</P1>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<DIV align=left>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<H1>" + "</H1>" + Microsoft.VisualBasic.vbCrLf
        'strHTML = strHTML + "<A href=""http://www.consulenzeperizie.it/"">Developed by Pretolesi Riccardo - www.consulenzeperizie.it</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A>Developed by Pretolesi Riccardo</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.consulenzeperizie.it/"">www.consulenzeperizie.it</A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A>  -  </A>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "<A href=""http://www.pretolesi.com/"">www.pretolesi.com</A>" + Microsoft.VisualBasic.vbCrLf

        strHTML = strHTML + "</BODY>" + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + Microsoft.VisualBasic.vbCrLf
        strHTML = strHTML + "</HTML>" + Microsoft.VisualBasic.vbCrLf

        Try
            My.Computer.FileSystem.WriteAllText(strNomeFile, strHTML, True)
            strHTML = ""

        Catch ex As Exception
            'AddLogEvent(LOG_ERROR, ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_ldLoginData, m_strFormTitle, m_strConnStringParamName)
            'System.Windows.Forms.MessageBox.Show(ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_strFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End Try

        Try
            Process.Start(strNomeFile)
        Catch ex As Exception
            'AddLogEvent(LOG_ERROR, ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_ldLoginData, m_strFormTitle, m_strConnStringParamName)
            'System.Windows.Forms.MessageBox.Show(ex.Message+ " "+ex.InnerException.Message + ex.StackTrace, m_strFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Protected Sub Set_ToolStripButtonStatusLabel()
        If Me.DesignMode = False Then

            Me.DataGridView.Enabled = True

            If Not m_bs.DataSource Is Nothing Then
                Me.ToolStripButton_Nuovo.Enabled = True
                If m_bs.DataSource.Rows.Count > 0 Then
                    Me.ToolStripButton_Elimina.Enabled = True
                    Me.ToolStripButton_Modifica.Enabled = True
                Else
                    Me.ToolStripButton_Elimina.Enabled = False
                    Me.ToolStripButton_Modifica.Enabled = False
                End If
                Me.ToolStripButton_Salva.Enabled = False
                Me.ToolStripButton_Annulla.Enabled = False
            Else
                Me.ToolStripButton_Nuovo.Enabled = False
                Me.ToolStripButton_Elimina.Enabled = False
                Me.ToolStripButton_Modifica.Enabled = False
                Me.ToolStripButton_Salva.Enabled = False
                Me.ToolStripButton_Annulla.Enabled = False
            End If

            If Not m_bs.DataSource Is Nothing Then
                ToolStripStatusLabel_Nr_Record.Text = m_bs.DataSource.Rows.Count.ToString() + " Righe"
            End If

        End If
    End Sub

    Protected Overrides Sub Finalize()

        m_bs.Dispose()

        MyBase.Finalize()
    End Sub

    Private Sub DataGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView.CellFormatting
        If e.Value.GetType().ToString() = GetType(Date).ToString() Then
            Me.DataGridView.Columns(e.ColumnIndex).DefaultCellStyle().Format = "G"
        End If
        'Me.DataGridView.Columns(e.ColumnIndex).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    End Sub
End Class