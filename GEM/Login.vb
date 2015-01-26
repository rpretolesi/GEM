Imports System.Data.SqlClient

Public Class Login

    Private m_iUID As Integer
    Private m_iULivello As Integer

    Property UID() As Integer
        Get
            Return m_iUID
        End Get

        Set(ByVal UID As Integer)
            m_iUID = UID
        End Set
    End Property

    Property ULivello() As Integer
        Get
            Return m_iULivello
        End Get

        Set(ByVal ULivello As Integer)
            m_iULivello = ULivello
        End Set
    End Property

    Overloads Function ShowDialog() As Windows.Forms.DialogResult

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim iULivello As Integer

        Me.TextBox_UserName.Text = ""
        Me.TextBox_Password.Text = ""
        Me.TextBox_Livello.Text = m_iULivello.ToString

        Try

            strSQL = " SELECT * FROM [Utente] "
            strSQL = strSQL + " WHERE U_ID = @U_ID AND U_Disabilitato = 0 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@U_ID", m_iUID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iULivello = ds.Tables(0).Rows(0).Item("U_Livello")
                    End If
                Else
                    ' Nessun database disponibile, assegno il livello massimo
                    iULivello = 9
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)

            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()

            Return Windows.Forms.DialogResult.Yes

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If iULivello >= m_iULivello Then
            Return Windows.Forms.DialogResult.Yes
        End If

        Return MyBase.ShowDialog()

    End Function

    Overloads Function ShowDialog(ByVal owner As IWin32Window) As Windows.Forms.DialogResult

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim iULivello As Integer

        Me.TextBox_UserName.Text = ""
        Me.TextBox_Password.Text = ""
        Me.TextBox_Livello.Text = m_iULivello.ToString

        Try

            strSQL = " SELECT * FROM [Utente] "
            strSQL = strSQL + " WHERE U_ID = @U_ID AND U_Disabilitato = 0 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@U_ID", m_iUID)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iULivello = ds.Tables(0).Rows(0).Item("U_Livello")
                    End If
                Else
                    ' Nessun database disponibile, assegno il livello massimo
                    iULivello = 9
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)

            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()

            Return Windows.Forms.DialogResult.Yes

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If iULivello >= m_iULivello Then
            Return Windows.Forms.DialogResult.Yes
        End If

        Return MyBase.ShowDialog(owner)

    End Function

    Private Sub Button_Login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Login.Click
        ' Aggiorno i dati immessi
        Me.Validate()

        Dim strSQL As String
        Dim ds As New DataSet
        Dim cn As New SqlConnection(My.Settings.ConnectionString)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        Dim iUID As Integer
        Dim iULivello As Integer

        Try

            strSQL = " SELECT * FROM [Utente] "
            strSQL = strSQL + " WHERE U_UserName = @U_UserName AND U_Password = @U_Password AND U_Disabilitato = 0 "

            CustomSQLConnectionOpen(cn, cmd)
            'cmd.Connection = cn
            cmd.CommandText = strSQL

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@U_UserName", Me.TextBox_UserName.Text)
            cmd.Parameters.AddWithValue("@U_Password", Me.TextBox_Password.Text)

            da.SelectCommand = cmd
            da.Fill(ds)

            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        iUID = ds.Tables(0).Rows(0).Item("U_ID")
                        iULivello = ds.Tables(0).Rows(0).Item("U_Livello")
                    End If
                End If
            End If

        Catch ex As Exception
            ScriviLogEventi(0, 0, AZIONE_READ, RISULTATO_EXCEPT, ex.Message + vbCrLf + GetRowNrErrorInStackTrace(ex.StackTrace), "", "", DEFAULT_OPERATOR_ID, Me)
            Me.DialogResult = Windows.Forms.DialogResult.Yes

            da.Dispose()
            cmd.Dispose()
            cn.Close()
            cn.Dispose()

            Exit Sub

        End Try

        da.Dispose()
        cmd.Dispose()
        cn.Close()
        cn.Dispose()

        If iULivello >= CInt(Me.TextBox_Livello.Text) Then
            m_iUID = iUID
            ScriviLogEventi(0, 0, AZIONE_LOGIN, RISULTATO_OK, "", "", "", m_iUID, Me)
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Else
            m_iUID = 0
            ScriviLogEventi(0, 0, AZIONE_LOGIN, RISULTATO_ERR, "Nome Utente/Password Errati, Livello insufficiente oppure Utente disabilitato", "", "", DEFAULT_OPERATOR_ID, Me)
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If

    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click

        Me.DialogResult = Windows.Forms.DialogResult.Cancel

    End Sub
End Class