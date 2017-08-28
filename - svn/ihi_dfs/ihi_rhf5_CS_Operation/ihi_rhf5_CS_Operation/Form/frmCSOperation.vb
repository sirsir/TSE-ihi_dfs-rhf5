Imports System.IO
Imports System.Text.RegularExpressions
Imports System.ServiceProcess

Public Class frmCSOperation

#Region "Attribute"
    Private m_dsIHI_RHF5 As ihi_rhf5

    Private m_taManager As ihi_rhf5TableAdapters.TableAdapterManager
    'read only
    Private m_taLine_Master As ihi_rhf5TableAdapters.LINE_MASTERTableAdapter
    Private m_taGlobal_Variables As ihi_rhf5TableAdapters.GLOBAL_VARIABLESTableAdapter
    Private m_taLOT As ihi_rhf5TableAdapters.LOTTableAdapter
    Private m_taLOT_PART As ihi_rhf5TableAdapters.LOT_PARTTableAdapter
    Private m_taBom As ihi_rhf5TableAdapters.BOMTableAdapter
    Private m_taSerial As ihi_rhf5TableAdapters.SERIALTableAdapter

    'insert update delete
    Private drLot As ihi_rhf5.LOTRow
    Private drLotPart As ihi_rhf5.LOT_PARTRow
    Private drGlobal_Variables As ihi_rhf5.GLOBAL_VARIABLESRow

    Private m_objLogger As clsLogger

    Private m_strFilePath As String = ""
    Private m_strExport As String = ""
    Private m_blnCheckDuplicate As Boolean
    'Private blnCheckFistStep As Boolean

    Private m_astrFileNameList As ArrayList
    Private m_strBackupPath As String = "D:\OMRON_DATA_FILING\temp\exportCSOperation_backup\"
    Private m_strBackupErrorPath As String = "D:\OMRON_DATA_FILING\temp\exportCSOperation_error\"
    Private arrtemp() As String
    'Friend WithEvents m_objPlc As System.ComponentModel.BackgroundWorker
    Friend WithEvents m_objPlc As clsPlcCommunication
    'Friend WithEvents m_objPlcLotData As clsPlcCommunication.clsLotData

#End Region

#Region "Constant"
    Private Const COLUMN_INDEX_NO As Integer = 0
    Private Const COLUMN_INDEX_PART_NO As Integer = 1
    Private Const COLUMN_INDEX_PART_NAME As Integer = 2
    Private Const COLUMN_INDEX_LOT1 As Integer = 3
    Private Const COLUMN_INDEX_QTY1 As Integer = 4
    Private Const COLUMN_INDEX_LOT2 As Integer = 5
    Private Const COLUMN_INDEX_QTY2 As Integer = 6
    Private Const COLUMN_INDEX_LOT3 As Integer = 7
    Private Const COLUMN_INDEX_QTY3 As Integer = 8
    Private Const COLUMN_INDEX_DATE As Integer = 9
    Private Const COLUMN_INDEX_TIME As Integer = 10

    Private Const IMPORT_HEADER_INDEX_LOT_CS As Integer = 0
    Private Const IMPORT_HEADER_INDEX_LOT_ITA As Integer = 0
    Private Const IMPORT_HEADER_INDEX_LOT_QTY As Integer = 0
    Private Const IMPORT_HEADER_INDEX_START_LOT_WHEN As Integer = 0

    Private Const IMPORT_DETAIL_INDEX_SEQ As Integer = 0
    Private Const IMPORT_DETAIL_INDEX_PART_NO As Integer = 1
    Private Const IMPORT_DETAIL_INDEX_PART_NAME As Integer = 2
    Private Const IMPORT_DETAIL_INDEX_LOT1 As Integer = 3
    Private Const IMPORT_DETAIL_INDEX_QTY1 As Integer = 4
    Private Const IMPORT_DETAIL_INDEX_LOT2 As Integer = 5
    Private Const IMPORT_DETAIL_INDEX_QTY2 As Integer = 6
    Private Const IMPORT_DETAIL_INDEX_LOT3 As Integer = 7
    Private Const IMPORT_DETAIL_INDEX_QTY3 As Integer = 8
    Private Const IMPORT_DETAIL_INDEX_DATE As Integer = 9
    Private Const IMPORT_DETAIL_INDEX_TIME As Integer = 10

    'Public Shared p_blnEndLotCompl
#End Region

    Private Enum nImportResult
        Success = 0
        SkipForRetry = 1
        SkipError = 2
    End Enum


    Private Sub frmCSOperation_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            BackupForm()
            StopService()

            m_objPlc.CancelAsync()

            'While m_objPlc.IsBusy 
            '    Threading.Thread.Sleep(500)
            'End While

            m_objPlc.Dispose()

            System.GC.Collect()
            Me.Dispose()
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "frmCSOperation_FormClosed", ex)
        End Try

    End Sub

    Private Sub frmCSOperation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If btnEndLot.Enabled Or btnEndRework.Enabled Then
            MsgBox("Must end lot before close program.")
            e.Cancel = True

            'If MsgBox("Do you want to force End Lot?", MsgBoxStyle.YesNo + vbQuestion) <> MsgBoxResult.Yes Then
            '    'user answer no
            '    e.Cancel = True
            'Else
            '    EndLotProcess()
            '    While Not btnStartLot.Enabled   'Wait until end lot complete
            '        Me.Refresh()
            '        Threading.Thread.Sleep(500)
            '    End While
            'End If
            'ElseIf (Not btnStartLot.Enabled) Or (btnEndLot.Enabled) Then
        ElseIf Not btnStartLot.Enabled Then

            e.Cancel = True
        Else
            If MsgBox("Are you sure to exit program", MsgBoxStyle.YesNo + vbQuestion) <> MsgBoxResult.Yes Then
                'user answer no
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub frmCSOperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyValue = Keys.F1 Then
                If e.Modifiers = Keys.Control + Keys.Alt + Keys.Shift Then
                    GenPartLot()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmCSOperation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            My.Application.ChangeCulture("en-US")

            'MsgBox((12.123).ToString("10.0000"))
            My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
            My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

            Me.BOMTableAdapter.Fill(Me.Ihi_rhf5.BOM)
            'Me.LOT_PARTTableAdapter.Fill(Me.Ihi_rhf5.LOT_PART)
            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = m_taBom.GetData()

            m_taGlobal_Variables.FillBy_ID(m_dsIHI_RHF5.GLOBAL_VARIABLES, "strCustomerLot")
            If m_dsIHI_RHF5.GLOBAL_VARIABLES IsNot Nothing AndAlso m_dsIHI_RHF5.GLOBAL_VARIABLES.Count > 0 Then
                drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES(0)
                txtCustomerLot.Text = drGlobal_Variables.TEXT_VALUE
                txtCustomerLot.Enabled = False
            Else
                txtCustomerLot.Text = ""
                txtCustomerLot.Enabled = True
            End If

            SetEnableButton(btnEndLot, False)
            SetEnableButton(btnEndRework, False)



            '== Set form control from file
            RestoreForm()

            StartService()



            m_objPlc = New clsPlcCommunication
            m_objPlc.LotData = New clsPlcCommunication.clsLotData
            m_objPlc.WorkerReportsProgress = True
            m_objPlc.WorkerSupportsCancellation = True
            m_objPlc.RunWorkerAsync()
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "frmCSOperation_Load", ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            m_objLogger = New clsLogger
            m_dsIHI_RHF5 = New ihi_rhf5
            m_taManager = New ihi_rhf5TableAdapters.TableAdapterManager
            m_taGlobal_Variables = New ihi_rhf5TableAdapters.GLOBAL_VARIABLESTableAdapter
            m_taLOT = New ihi_rhf5TableAdapters.LOTTableAdapter
            m_taLOT_PART = New ihi_rhf5TableAdapters.LOT_PARTTableAdapter
            m_taBom = New ihi_rhf5TableAdapters.BOMTableAdapter
            m_taLine_Master = New ihi_rhf5TableAdapters.LINE_MASTERTableAdapter

            'm_dsIHI_RHF5.Clear()
            'm_taBom.Fill(m_dsIHI_RHF5.BOM)
            'm_taGlobal_Variables.Fill(m_dsIHI_RHF5.GLOBAL_VARIABLES)
            m_taManager.GLOBAL_VARIABLESTableAdapter = m_taGlobal_Variables
            m_taManager.LOTTableAdapter = m_taLOT
            m_taManager.LOT_PARTTableAdapter = m_taLOT_PART

            m_astrFileNameList = New ArrayList

        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "LoadMaster", ex)
        End Try
    End Sub

    Private Sub txtLotQTY_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLotQTY.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtLotQTY.Text) Then
                If CInt(txtLotQTY.Text) = 0 Then
                    lbResult.Text = Now.ToString() & ": Invalid Lot Qty = " & txtLotQTY.Text
                    txtLotQTY.Focus()
                    txtLotQTY.SelectAll()
                Else
                    lbResult.Text = ""
                    txtPartBarcode.Focus()
                End If
            Else
                lbResult.Text = Now.ToString() & ": Invalid Lot Qty = " & txtLotQTY.Text
                txtLotQTY.Focus()
                txtLotQTY.SelectAll()
            End If
        End If
    End Sub

    Private Sub txtLotQTY_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtLotQTY.KeyPress
        'key the number only.
        If (Char.IsNumber(e.KeyChar) = False) And (e.KeyChar <> ""c) Then
            e.Handled = True
            Return
        End If
    End Sub

    Private Sub txtCurrentQTY_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrentQTY.KeyPress
        'key the number only.
        If (Char.IsNumber(e.KeyChar) = False) And (e.KeyChar <> ""c) Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating

        Try
            ' several possible format styles
            'Dim formats() As String = {"d-MM-yyyy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy"}
            Dim formats As String = "dd/MM/yyyy"
            Dim thisDt As DateTime

            Dim regexpTemp As Regex = New Regex("^([0-2]{1}[0-9]{1}[:]{1}[0-5]{1}[0-9]{1}[:]{1}[0-5]{1}[0-9]{1})$")
            Select Case e.ColumnIndex
                Case 2
                Case COLUMN_INDEX_QTY1
                    If Not (IsNumeric(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue) _
                    Or DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue = "") Then
                        MsgBox("Input number only.")
                        e.Cancel = True
                    End If
                Case COLUMN_INDEX_DATE

                    If Not ((DateTime.TryParseExact(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue, formats,
                                              Globalization.CultureInfo.InvariantCulture,
                                              Globalization.DateTimeStyles.None, thisDt)) Or _
                            (DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue = "")) Then

                        MsgBox("Input date format dd/MM/yyyy only.")
                        e.Cancel = True
                    End If
                Case COLUMN_INDEX_TIME
                    If Not (regexpTemp.IsMatch(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue) Or _
                            (DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue = "")) Then
                        MsgBox("Input date format HH:mm:ss only.")
                        e.Cancel = True
                    End If
            End Select
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "CellValidating", ex)
        End Try

    End Sub

    Private Sub btnStartLot_Click(sender As System.Object, e As System.EventArgs) Handles btnStartLot.Click
        Try
            If MsgBox("Warning! After starting new lot, you can not modify any information.", MsgBoxStyle.YesNo + vbExclamation, "Do you want to start new lot?") <> MsgBoxResult.Yes Then
                'user answer no
                Exit Sub
            End If



            BackupForm()

            'CHECK CAN START
            If m_objPlc.Started Then
                Return
            End If

            If txtProductionLot.Text.Trim = "" Then
                MsgBox("Please input Production Lot.")
                txtProductionLot.Select()
                Exit Sub
            End If

            If txtCustomerLot.Text.Trim = "" Then
                MsgBox("Please input Customer Lot.")
                txtCustomerLot.Select()
                Exit Sub
            End If

            If (txtLotQTY.Text.Trim = "") Then
                MsgBox("Please input Lot QTY.")
                txtLotQTY.Select()
                Exit Sub
            End If

            If (CInt(txtLotQTY.Text) = 0) Then
                MsgBox("Please input Lot QTY > 0.")
                txtLotQTY.Select()
                Exit Sub
            End If

            If Not Me.GenerateExportString() Then
                Exit Sub
            End If

            ' find rootPath if cannot connect to DB, program create rootpath by define 
            If Not findRootPathInDatabase() Then
                findRootPathByDefine()
            End If

            If ExportTextFileForImport("Normal") Then
                CountTextFile()
                'MsgBox("Save complete.")
            Else
                MsgBox("Save not success.")
            End If

            If Not m_blnCheckDuplicate Then
                m_objPlc.Started = True
                m_objPlc.Started_Normal = True
                SetEnableButton(btnStartLot, False)
                SetEnableButton(btnClear, False)
                txtPlcReady.BackColor = Color.Red
                txtPartBarcode.Enabled = False
                SetEnableButton(btnRework, False)
                SetEnableButton(btnEndLot, True)
                SetEnableButton(btnEndRework, False)
                'blnCheckFistStep = True
                SetEnableButton(btnEdit, False)
                BlockEditData()
            End If


        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Public Sub SetEnableButton(ByVal btn As System.Windows.Forms.Button, ByVal blnEnable As Boolean)
        If blnEnable Then
            btn.BackColor = Color.Lime
        Else
            btn.BackColor = Color.LightGray
        End If
        btn.Enabled = blnEnable
    End Sub

    Private Sub CountTextFile()
        Try
            'TODO: Modify add support for 3 lot
            Dim di As IO.DirectoryInfo

            If Not My.Computer.FileSystem.DirectoryExists(m_strFilePath & "\exportCSOperation") Then
                MsgBox("Path ""{0}"" not exist, please recheck configuration and restart program (" & m_strFilePath & "\exportCSOperation" & " )")
                Return
            End If
            di = New IO.DirectoryInfo(m_strFilePath & "\exportCSOperation")

            Dim arrFile = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly).OrderBy(Function(x) x.LastWriteTime)
            If arrFile.Count > 0 And m_astrFileNameList.Count = 0 Then
                For Each inf As FileInfo In arrFile
                    Me.m_astrFileNameList.Add(inf.FullName)
                Next
                m_objLogger.AppendLog("New Manual Import File Found", "Info")

                While Me.m_astrFileNameList.Count > 0
                    Me.ImportDataFile()
                End While
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub ImportDataFile()
        If Me.m_astrFileNameList.Count > 0 Then
            Dim tempfilePath As String = Me.m_astrFileNameList(0).ToString
            Try
                If Not My.Computer.FileSystem.DirectoryExists(m_strBackupPath) Then
                    My.Computer.FileSystem.CreateDirectory(m_strBackupPath)
                End If

                If Not My.Computer.FileSystem.DirectoryExists(m_strBackupErrorPath) Then
                    My.Computer.FileSystem.CreateDirectory(m_strBackupErrorPath)
                End If

                Dim i As Integer = 0
                Dim nResult As nImportResult
                While i < 5
                    Using theFile As New IO.FileStream(tempfilePath, FileMode.Open, FileAccess.Read)
                        If Not theFile.CanRead Then
                            m_objLogger.AppendLog(Me.GetType.Name, "ImportDataFile", "Cannot read the file: " & m_strFilePath, "Error")
                            Threading.Thread.Sleep(500)
                            Continue While
                        End If
                    End Using

                    nResult = Me.ImportToDB(tempfilePath)
                    If nResult = nImportResult.Success Then
                        'Import complete, move file to backup
                        Me.m_astrFileNameList.RemoveAt(0)
                        arrtemp = Split(tempfilePath, "\")
                        MoveFile(tempfilePath, m_strBackupPath & arrtemp(arrtemp.Count - 1))
                        Exit While
                    ElseIf nResult = nImportResult.SkipForRetry Then
                        i += 1
                    ElseIf nResult = nImportResult.SkipError Then
                        Me.m_astrFileNameList.RemoveAt(0)
                        arrtemp = Split(tempfilePath, "\")
                        MoveFile(tempfilePath, m_strBackupErrorPath & arrtemp(arrtemp.Count - 1))
                        Exit While
                    End If
                End While

                If i >= 5 Then
                    Me.m_astrFileNameList.RemoveAt(0)
                End If

            Catch ex As Exception
                HandleError(ex)
                Me.m_astrFileNameList.RemoveAt(0)
                'MoveFile(tempfilePath, m_strBackupPath & arrtemp(arrtemp.Count - 1))
            End Try
        End If
    End Sub

    Private Function ImportToDB(ByVal filePath As String) As nImportResult
        ImportToDB = 0
        m_objLogger.AppendLog("Start Import " & filePath, "Info")
        m_dsIHI_RHF5.GLOBAL_VARIABLES.Clear()
        m_dsIHI_RHF5.LOT_PART.Clear()
        m_dsIHI_RHF5.LOT.Clear()
        Dim blnResult As Boolean = False

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePath)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.TrimWhiteSpace = False
            MyReader.Delimiters = New String() {"|"}
            Dim strCurrentRow As String()
            Dim blnCheck As Boolean
            Dim intNumRows As Integer = 0

            Dim arrFilePath() As String = Nothing
            'Loop through all of the fields in the file. 
            'If any lines are corrupt, report an error and continue parsing. 

            'skip 1st line
            'strCurrentRow = MyReader.ReadFields()
            blnCheck = True
            m_blnCheckDuplicate = False

            arrFilePath = Split(filePath.ToString, "\")
            If arrFilePath(arrFilePath.Count - 1).Substring(0, 1) = "N" Then ' Normal Mode: Check from filename 1st digit
                While Not MyReader.EndOfData
                    Try
                        strCurrentRow = MyReader.ReadFields()
                        'Head
                        If blnCheck Then
                            blnCheck = False

                            m_taLOT.FillBy_LOT_CS(m_dsIHI_RHF5.LOT, strCurrentRow(0).Trim)
                            If m_dsIHI_RHF5.LOT IsNot Nothing AndAlso m_dsIHI_RHF5.LOT.Count > 0 Then
                                'MsgBox("Duplicate Production Lot.")
                                lbResult.Text = Now.ToString() & ": " & "Duplicate Production Lot - " & strCurrentRow(0)
                                m_blnCheckDuplicate = True
                                Return nImportResult.SkipError
                            End If

                            m_taGlobal_Variables.FillBy_ID(m_dsIHI_RHF5.GLOBAL_VARIABLES, "strCustomerLot")
                            If m_dsIHI_RHF5.GLOBAL_VARIABLES IsNot Nothing AndAlso m_dsIHI_RHF5.GLOBAL_VARIABLES.Count > 0 Then
                                'Update Customer_LOT
                                drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES(0)
                                drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                                m_objLogger.AppendLog("Update Global Variables [" & strCurrentRow(1) & "]", "Info")
                            Else
                                'Add Customer_LOT
                                drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES.NewGLOBAL_VARIABLESRow
                                drGlobal_Variables.ID = "strCustomerLot"
                                drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                                m_dsIHI_RHF5.GLOBAL_VARIABLES.AddGLOBAL_VARIABLESRow(drGlobal_Variables)
                                m_objLogger.AppendLog("Add Global Variables [" & strCurrentRow(1) & "]", "Info")
                            End If

                            drLot = m_dsIHI_RHF5.LOT.NewLOTRow
                            drLot.LOT_CS = strCurrentRow(0).Trim
                            drLot.LOT_ITA = strCurrentRow(1).Trim
                            drLot.LOT_QTY = CInt(strCurrentRow(2).Trim)
                            drLot.CURRENT_QTY = 0
                            Dim datTemp As DateTime
                            DateTime.TryParseExact(strCurrentRow(3).Trim, "dd/MM/yyyy HH:mm:ss", Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.None, datTemp)
                            drLot.START_LOT_WHEN = datTemp
                            drLot.START_MODE = "Normal"
                            drLot.CREATED_WHEN = Now
                            drLot.SetLAST_MODIFIED_WHENNull()
                            m_dsIHI_RHF5.LOT.AddLOTRow(drLot)
                            m_objLogger.AppendLog("Add LOT [" & strCurrentRow(0) & "] - Normal Mode", "Info")

                            'set data for PLC
                            'Dim AAA As New clsPlcCommunication.sLotData
                            'AAA.LotNo = ""

                            m_objPlc.LotData.LotNo = strCurrentRow(0).Trim
                            m_objPlc.LotData.LotQty = CInt(strCurrentRow(2).Trim)

                        Else

                            'Details
                            drLotPart = m_dsIHI_RHF5.LOT_PART.NewLOT_PARTRow
                            drLotPart.LOTRow = drLot
                            drLotPart.PART_NO = strCurrentRow(IMPORT_DETAIL_INDEX_PART_NO).Trim

                            If strCurrentRow(IMPORT_DETAIL_INDEX_PART_NAME).Trim = "" Then
                                drLotPart.SetPART_NAMENull()
                            Else
                                drLotPart.PART_NAME = strCurrentRow(IMPORT_DETAIL_INDEX_PART_NAME).Trim
                            End If

                            drLotPart.SEQ = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_SEQ).Trim)
                            drLotPart.PART_LOT_NO = strCurrentRow(IMPORT_DETAIL_INDEX_LOT1).Trim
                            drLotPart.QTY = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY1).Trim)

                            If strCurrentRow(IMPORT_DETAIL_INDEX_LOT2).Trim = "" Then
                                drLotPart.SetPART_LOT_NO_2Null()
                            Else
                                drLotPart.PART_LOT_NO_2 = strCurrentRow(IMPORT_DETAIL_INDEX_LOT2).Trim
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_QTY2).Trim = "" Then
                                drLotPart.SetQTY_2Null()
                            Else
                                drLotPart.QTY_2 = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY2).Trim)
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_LOT3).Trim = "" Then
                                drLotPart.SetPART_LOT_NO_3Null()
                            Else
                                drLotPart.PART_LOT_NO_3 = strCurrentRow(IMPORT_DETAIL_INDEX_LOT3).Trim
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_QTY3).Trim = "" Then
                                drLotPart.SetQTY_3Null()
                            Else
                                drLotPart.QTY_3 = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY3).Trim)
                            End If

                            Dim datTemp As DateTime
                            DateTime.TryParseExact(strCurrentRow(IMPORT_DETAIL_INDEX_DATE).Trim & " " & strCurrentRow(IMPORT_DETAIL_INDEX_TIME).Trim, "dd/MM/yyyy HH:mm:ss", Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.None, datTemp)
                            drLotPart.LOT_DATE_TIME = datTemp
                            drLotPart.CREATED_WHEN = Now
                            m_dsIHI_RHF5.LOT_PART.AddLOT_PARTRow(drLotPart)
                            m_objLogger.AppendLog("Add LOT_PART [" & strCurrentRow(0) & "] - Normal Mode", "Info")
                        End If

                        blnResult = True
                    Catch ex As Exception
                        m_objLogger.AppendLog(Me.GetType.Name, "ReadFields Normal Case", ex)
                        blnResult = False
                        Exit While
                    End Try
                End While
                If blnResult Then
                    m_taManager.UpdateAll(m_dsIHI_RHF5)
                    Return nImportResult.Success
                Else
                    m_dsIHI_RHF5.RejectChanges()
                    Return nImportResult.SkipForRetry
                End If

            Else 'Rework
                While Not MyReader.EndOfData
                    Try
                        strCurrentRow = MyReader.ReadFields()

                        'Head
                        If blnCheck Then
                            blnCheck = False

                            m_taLOT.FillBy_LOT_CS(m_dsIHI_RHF5.LOT, strCurrentRow(0).Trim)
                            If Not (m_dsIHI_RHF5.LOT.Count = 1) Then
                                lbResult.Text = Now.ToString() & ": " & "Duplicate Production Lot - " & strCurrentRow(0)
                                m_blnCheckDuplicate = True
                                Return nImportResult.SkipError
                            End If


                            m_taGlobal_Variables.FillBy_ID(m_dsIHI_RHF5.GLOBAL_VARIABLES, "strCustomerLot")
                            If m_dsIHI_RHF5.GLOBAL_VARIABLES IsNot Nothing AndAlso m_dsIHI_RHF5.GLOBAL_VARIABLES.Count > 0 Then
                                'Update Customer_LOT
                                drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES(0)
                                drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                                m_objLogger.AppendLog("Update Global Variables [" & strCurrentRow(1) & "]", "Info")
                            Else
                                'Add Customer_LOT
                                drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES.NewGLOBAL_VARIABLESRow
                                drGlobal_Variables.ID = "strCustomerLot"
                                drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                                m_dsIHI_RHF5.GLOBAL_VARIABLES.AddGLOBAL_VARIABLESRow(drGlobal_Variables)
                                m_objLogger.AppendLog("Add Global Variables [" & strCurrentRow(1) & "]", "Info")
                            End If

                            'm_taGlobal_Variables.FillBy_ID(m_dsIHI_RHF5.GLOBAL_VARIABLES, "strCustomerLot")
                            'If m_dsIHI_RHF5.GLOBAL_VARIABLES IsNot Nothing AndAlso m_dsIHI_RHF5.GLOBAL_VARIABLES.Count > 0 Then
                            '    'Update Customer_LOT
                            '    drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES(0)
                            '    drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                            '    m_objLogger.AppendLog("Update Global Variables [" & strCurrentRow(1) & "]", "Info")
                            'Else
                            '    'Add Customer_LOT
                            '    drGlobal_Variables = m_dsIHI_RHF5.GLOBAL_VARIABLES.NewGLOBAL_VARIABLESRow
                            '    drGlobal_Variables.ID = "strCustomerLot"
                            '    drGlobal_Variables.TEXT_VALUE = strCurrentRow(1).Trim
                            '    m_dsIHI_RHF5.GLOBAL_VARIABLES.AddGLOBAL_VARIABLESRow(drGlobal_Variables)
                            '    m_objLogger.AppendLog("Add Global Variables [" & strCurrentRow(1) & "]", "Info")
                            'End If

                            'drLot = m_dsIHI_RHF5.LOT.NewLOTRow
                            'drLot.LOT_CS = strCurrentRow(0).Trim
                            'drLot.LOT_ITA = strCurrentRow(1).Trim
                            'drLot.LOT_QTY = CInt(strCurrentRow(2).Trim)
                            'drLot.CURRENT_QTY = 0
                            'drLot.START_LOT_WHEN = Now ' Format(Now, "dd/MM/yyyy H:mm:ss")
                            'drLot.START_MODE = "Rework"
                            'drLot.CREATED_WHEN = Now
                            'drLot.LAST_MODIFIED_WHEN = Now
                            'm_dsIHI_RHF5.LOT.AddLOTRow(drLot)
                            'm_objLogger.AppendLog("Add LOT [" & strCurrentRow(0) & "] - Normal Mode", "Info")

                            ''set data for PLC
                            ''Dim AAA As New clsPlcCommunication.sLotData
                            ''AAA.LotNo = ""

                            'm_objPlc.LotData.LotNo = strCurrentRow(0).Trim
                            'm_objPlc.LotData.LotQty = CInt(strCurrentRow(2).Trim)

                            'drLot = m_dsIHI_RHF5.LOT.NewLOTRow
                            drLot = m_dsIHI_RHF5.LOT(0)
                            drLot.LOT_CS = strCurrentRow(0).Trim
                            drLot.LOT_ITA = strCurrentRow(1).Trim
                            drLot.LOT_QTY = CInt(strCurrentRow(2).Trim)
                            drLot.CURRENT_QTY = 0
                            Dim datTemp As DateTime
                            DateTime.TryParseExact(strCurrentRow(3).Trim, "dd/MM/yyyy HH:mm:ss", Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.None, datTemp)
                            drLot.START_LOT_WHEN = datTemp
                            drLot.START_MODE = "Rework"
                            'drLot.CREATED_WHEN = Now
                            drLot.LAST_MODIFIED_WHEN = Now
                            'm_dsIHI_RHF5.LOT.AddLOTRow(drLot)
                            m_objLogger.AppendLog("Update LOT [" & strCurrentRow(0) & "] - Rework Mode", "Info")

                            m_taLOT_PART.DeleteQuery(drLot.LOT_ID)

                            'set data for PLC
                            'Dim AAA As New clsPlcCommunication.sLotData
                            'AAA.LotNo = ""
                            m_objPlc.LotData.LotNo = strCurrentRow(0).Trim
                            m_objPlc.LotData.LotQty = CInt(strCurrentRow(2).Trim)

                        Else

                            'Details
                            drLotPart = m_dsIHI_RHF5.LOT_PART.NewLOT_PARTRow
                            drLotPart.LOTRow = drLot
                            drLotPart.PART_NO = strCurrentRow(IMPORT_DETAIL_INDEX_PART_NO).Trim

                            If strCurrentRow(IMPORT_DETAIL_INDEX_PART_NAME).Trim = "" Then
                                drLotPart.SetPART_NAMENull()
                            Else
                                drLotPart.PART_NAME = strCurrentRow(IMPORT_DETAIL_INDEX_PART_NAME).Trim
                            End If

                            drLotPart.SEQ = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_SEQ).Trim)
                            drLotPart.PART_LOT_NO = strCurrentRow(IMPORT_DETAIL_INDEX_LOT1).Trim
                            drLotPart.QTY = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY1).Trim)

                            If strCurrentRow(IMPORT_DETAIL_INDEX_LOT2).Trim = "" Then
                                drLotPart.SetPART_LOT_NO_2Null()
                            Else
                                drLotPart.PART_LOT_NO_2 = strCurrentRow(IMPORT_DETAIL_INDEX_LOT2).Trim
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_QTY2).Trim = "" Then
                                drLotPart.SetQTY_2Null()
                            Else
                                drLotPart.QTY_2 = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY2).Trim)
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_LOT3).Trim = "" Then
                                drLotPart.SetPART_LOT_NO_3Null()
                            Else
                                drLotPart.PART_LOT_NO_3 = strCurrentRow(IMPORT_DETAIL_INDEX_LOT3).Trim
                            End If

                            If strCurrentRow(IMPORT_DETAIL_INDEX_QTY3).Trim = "" Then
                                drLotPart.SetQTY_3Null()
                            Else
                                drLotPart.QTY_3 = CInt(strCurrentRow(IMPORT_DETAIL_INDEX_QTY3).Trim)
                            End If

                            Dim datTemp As DateTime
                            DateTime.TryParseExact(strCurrentRow(IMPORT_DETAIL_INDEX_DATE).Trim & " " & strCurrentRow(IMPORT_DETAIL_INDEX_TIME).Trim, "dd/MM/yyyy HH:mm:ss", Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.None, datTemp)
                            drLotPart.LOT_DATE_TIME = datTemp
                            drLotPart.CREATED_WHEN = Now
                            m_dsIHI_RHF5.LOT_PART.AddLOT_PARTRow(drLotPart)
                            m_objLogger.AppendLog("Add LOT_PART [" & strCurrentRow(0) & "] - Rework Mode", "Info")
                        End If

                        blnResult = True
                    Catch ex As Exception
                        m_objLogger.AppendLog(Me.GetType.Name, "ReadFields Rework Case", ex)
                        blnResult = False
                        Exit While
                    End Try
                End While


                If blnResult Then
                    m_taManager.UpdateAll(m_dsIHI_RHF5)
                    Return nImportResult.Success
                Else
                    m_dsIHI_RHF5.RejectChanges()
                    Return nImportResult.SkipForRetry
                End If
            End If

        End Using
    End Function

    Private Sub MoveFile(ByVal fromFile As String, ByVal toFile As String)
        Try
            If My.Computer.FileSystem.FileExists(fromFile) = True Then
                My.Computer.FileSystem.MoveFile(fromFile, toFile, False)
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Function findRootPathInDatabase()

        Try
            'My.Settings.RunningLineName = CS
            Dim dtLineMas As ihi_rhf5.LINE_MASTERDataTable = m_taLine_Master.GetDataBy_LINE_NAME("CS")

            For i = 0 To dtLineMas.Count - 1
                m_strFilePath = dtLineMas.Item(i).ROOT_TEMP
            Next

            Return True
        Catch ex As Exception
            HandleError(ex)
            Return False
        End Try

    End Function

    Private Function findRootPathByDefine()

        Try
            'strFilePath = "D:\OMRON_DATA_FILING\temp"
            m_strFilePath = My.Settings.exportCSOperationPath
            Return True
        Catch ex As Exception
            HandleError(ex)
            Return False
        End Try

    End Function

    Private Sub BlockEditData()
        'ByVal blnTextInputEnable As Boolean, blnReadOnly As Boolean
        'lbResult.Text = Now.ToString() & ": Lot " & txtProductionLot.Text & " Ended"

        'txtProductionLot.Text = ""
        'txtCustomerLot.Text = ""
        'txtLotQTY.Text = 0
        'txtCurrentQTY.Text = 0

        txtProductionLot.Enabled = False
        txtCustomerLot.Enabled = False
        txtLotQTY.Enabled = False
        txtCurrentQTY.Enabled = False

        For x = 0 To DataGridView1.Rows.Count - 1
            For y = 0 To DataGridView1.Columns.Count - 1
                If y >= COLUMN_INDEX_LOT1 Then
                    DataGridView1.Rows(x).Cells(y).ReadOnly = True
                    'DataGridView1.Rows(x).Cells(y).Value = ""
                End If
            Next
        Next
    End Sub

    Private Sub SaveComplete()
        'ByVal blnTextInputEnable As Boolean, blnReadOnly As Boolean
        lbResult.Text = Now.ToString() & ": Lot " & txtProductionLot.Text & " Ended"

        txtProductionLot.Text = ""
        'txtCustomerLot.Text = ""
        txtLotQTY.Text = 0
        txtCurrentQTY.Text = 0

        txtProductionLot.Enabled = True
        'txtCustomerLot.Enabled = False
        txtLotQTY.Enabled = True
        'txtCurrentQTY.Enabled = False

        For x = 0 To DataGridView1.Rows.Count - 1
            For y = 0 To DataGridView1.Columns.Count - 1
                If y >= COLUMN_INDEX_LOT1 Then
                    DataGridView1.Rows(x).Cells(y).ReadOnly = False
                    DataGridView1.Rows(x).Cells(y).Value = ""
                End If
            Next
        Next
    End Sub

    Private Sub ClearData()
        'ByVal blnTextInputEnable As Boolean, blnReadOnly As Boolean
        
        txtProductionLot.Text = ""
        'txtCustomerLot.Text = ""
        txtLotQTY.Text = 0
        txtCurrentQTY.Text = 0

        For x = 0 To DataGridView1.Rows.Count - 1
            For y = 0 To DataGridView1.Columns.Count - 1
                If y >= COLUMN_INDEX_LOT1 Then
                    DataGridView1.Rows(x).Cells(y).ReadOnly = False
                    DataGridView1.Rows(x).Cells(y).Value = ""
                End If
            Next
        Next
    End Sub

    Private Function ExportTextFileForImport(ByVal strMode As String)
        Try
            'TODO: Modify add support for 3 lot
            Dim timeForImport As DateTime = DateTime.Now
            Dim formatForImport As String = "yyyyMMddHHmmssfff"

            If Not My.Computer.FileSystem.DirectoryExists(m_strFilePath & "\exportCSOperation") Then
                My.Computer.FileSystem.CreateDirectory(m_strFilePath & "\exportCSOperation")
            End If

            Using stWrite As New IO.StreamWriter(m_strFilePath & "\exportCSOperation\" & _
                                                    strMode & "CSOperation" & timeForImport.ToString(formatForImport) & ".txt", True)
                stWrite.WriteLine(m_strExport)
            End Using
            Return True
        Catch ex As Exception
            HandleError(ex)
            Return False
        End Try
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        GenPartLot()
    End Sub

    Private Sub GenPartLot()
        For x = 0 To DataGridView1.Rows.Count - 1
            For y = 0 To DataGridView1.Columns.Count - 1

                Select Case y
                    Case COLUMN_INDEX_NO
                    Case COLUMN_INDEX_PART_NO
                    Case COLUMN_INDEX_LOT1
                        DataGridView1.Rows(x).Cells(y).Value = "SP1" + x.ToString("D2")
                    Case COLUMN_INDEX_QTY1
                        DataGridView1.Rows(x).Cells(y).Value = (x + 1) * 10
                    Case COLUMN_INDEX_LOT2
                        DataGridView1.Rows(x).Cells(y).Value = "SP2" + x.ToString("D2")
                    Case COLUMN_INDEX_QTY2
                        DataGridView1.Rows(x).Cells(y).Value = (x + 1) * 15
                    Case COLUMN_INDEX_LOT3
                        DataGridView1.Rows(x).Cells(y).Value = "SP3" + x.ToString("D2")
                    Case COLUMN_INDEX_QTY3
                        DataGridView1.Rows(x).Cells(y).Value = (x + 1) * 20
                    Case COLUMN_INDEX_DATE
                        DataGridView1.Rows(x).Cells(y).Value = Now.ToString("dd/MM/yyyy")
                    Case COLUMN_INDEX_TIME
                        DataGridView1.Rows(x).Cells(y).Value = Now.ToString("HH:mm:ss")
                End Select

            Next
        Next
    End Sub

    Private Sub btnRework_Click(sender As System.Object, e As System.EventArgs) Handles btnRework.Click
        Try
            If MsgBox("Warning! After starting new lot, you can not modify any information.", MsgBoxStyle.YesNo + vbExclamation, "Do you want to start new lot?") <> MsgBoxResult.Yes Then
                'user answer no
                Exit Sub
            End If

            If txtProductionLot.Text.Trim = "" Then
                MsgBox("Please input Production Lot.")
                txtProductionLot.Select()
                Exit Sub
            End If

            If txtCustomerLot.Text.Trim = "" Then
                MsgBox("Please input Customer Lot.")
                txtCustomerLot.Select()
                Exit Sub
            End If

            If txtLotQTY.Text.Trim = "" Then
                MsgBox("Please input Lot QTY.")
                txtLotQTY.Select()
                Exit Sub
            End If

            If (CInt(txtLotQTY.Text) = 0) Then
                MsgBox("Please input Lot QTY > 0.")
                txtLotQTY.Select()
                Exit Sub
            End If

            If Not Me.GenerateExportString() Then
                Exit Sub
            End If

            ' find rootPath if cannot connect to DB, program create rootpath by define 
            If Not findRootPathInDatabase() Then
                findRootPathByDefine()
            End If

            If ExportTextFileForImport("Rework") Then
                CountTextFile()
                'MsgBox("Save complete.")
            Else
                MsgBox("Save not success.")
            End If

            If Not m_blnCheckDuplicate Then
                m_objPlc.Started = True
                m_objPlc.Started_Normal = False
                SetEnableButton(btnStartLot, False)
                SetEnableButton(btnClear, False)
                txtPlcReady.BackColor = Color.Red
                txtPartBarcode.Enabled = False
                SetEnableButton(btnRework, False)
                SetEnableButton(btnEndLot, False)
                SetEnableButton(btnEndRework, True)
                'blnCheckFistStep = True
                SetEnableButton(btnEdit, False)
                BlockEditData()
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Function GenerateExportString() As Boolean
        m_strExport = ""
        m_strExport &= txtProductionLot.Text & "|" & txtCustomerLot.Text & _
                        "|" & txtLotQTY.Text & "|" & Now.ToString("dd/MM/yyyy HH:mm:ss") & vbCrLf

        'TODO: Modify add support for 3 lot
        For x = 0 To DataGridView1.Rows.Count - 1
            If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT1).EditedFormattedValue) = "" Then
                MsgBox("Cannot input empty data in LOT1.")
                Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_LOT1, x)
                Return False
            ElseIf Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY1).EditedFormattedValue) = "0" Then
                MsgBox("Please input QTY1 > 0.")
                Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_QTY1, x)
                Return False
            End If

            If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT2).EditedFormattedValue) <> "" Then
                If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).EditedFormattedValue) = "0" _
                    OrElse Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).EditedFormattedValue) = "" Then
                    MsgBox("Please input QTY2 > 0.")
                    Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_QTY2, x)
                    Return False
                End If

                If Not IsNumeric(Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).EditedFormattedValue)) Then
                    MsgBox("Input number only in QTY2.")
                    Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_QTY2, x)
                    Return False
                End If
            Else
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).Value() = ""
            End If

            If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT3).EditedFormattedValue) <> "" Then
                If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).EditedFormattedValue) = "0" _
                    OrElse Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).EditedFormattedValue) = "" Then
                    MsgBox("Please input QTY3 > 0.")
                    Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_QTY3, x)
                    Return False
                End If

                If Not IsNumeric(Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).EditedFormattedValue)) Then
                    MsgBox("Input number only in QTY3.")
                    Me.DataGridView1.CurrentCell = DataGridView1.Item(COLUMN_INDEX_QTY3, x)
                    Return False
                End If
            Else
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).Value() = ""
            End If
            If Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).EditedFormattedValue) = "" _
                Or Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).EditedFormattedValue) = "" Then
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).Value = Now.ToString("dd/MM/yyyy")
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).Value = Now.ToString("HH:mm:ss")
            End If
            Dim datTemp As DateTime
            Dim strDate As String = Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).EditedFormattedValue)
            Dim strTime As String = Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).EditedFormattedValue)
            DateTime.TryParseExact(strDate & " " & strTime, "dd/MM/yyyy HH:mm:ss", Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.None, datTemp)

            If datTemp.Equals(System.DateTime.MinValue) Then
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).Value = Now.ToString("dd/MM/yyyy")
                DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).Value = Now.ToString("HH:mm:ss")
            End If

            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_NO).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_PART_NO).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_PART_NAME).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT1).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY1).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT2).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT3).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).EditedFormattedValue) & "|"


            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).EditedFormattedValue) & "|"
            m_strExport &= Trim(DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).EditedFormattedValue) & vbCrLf
        Next
        Return True
    End Function

    Private Sub ShowDataGridView(ByVal intCurrentQTY As Integer)
        'TODO : Recheck about 3 lot support

        m_dsIHI_RHF5.GLOBAL_VARIABLES.Clear()
        m_dsIHI_RHF5.LOT_PART.Clear()
        m_dsIHI_RHF5.LOT.Clear()

        m_taLOT.FillBy_LOT_CS(m_dsIHI_RHF5.LOT, m_objPlc.LotData.LotNo.Trim)

        If m_dsIHI_RHF5.LOT.Count = 1 Then
            txtProductionLot.Text = m_dsIHI_RHF5.LOT(0).LOT_CS.Trim
            txtCustomerLot.Text = m_dsIHI_RHF5.LOT(0).LOT_ITA.Trim
            txtLotQTY.Text = CStr(m_dsIHI_RHF5.LOT(0).LOT_QTY)
            txtCurrentQTY.Text = intCurrentQTY
            m_taLOT_PART.FillBy_LOT_ID(m_dsIHI_RHF5.LOT_PART, m_dsIHI_RHF5.LOT(0).LOT_ID)

            If m_dsIHI_RHF5.LOT_PART.Count > 0 Then
                For x = 0 To m_dsIHI_RHF5.LOT_PART.Count - 1

                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_NO).Value = m_dsIHI_RHF5.LOT_PART(x).SEQ
                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_PART_NO).Value = m_dsIHI_RHF5.LOT_PART(x).PART_NO
                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT1).Value = m_dsIHI_RHF5.LOT_PART(x).PART_LOT_NO
                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY1).Value = m_dsIHI_RHF5.LOT_PART(x).QTY

                    If Not m_dsIHI_RHF5.LOT_PART(x).IsPART_LOT_NO_2Null Then
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT2).Value = m_dsIHI_RHF5.LOT_PART(x).PART_LOT_NO_2
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).Value = m_dsIHI_RHF5.LOT_PART(x).QTY_2
                    Else
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT2).Value = ""
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY2).Value = ""
                    End If
                    If Not m_dsIHI_RHF5.LOT_PART(x).IsPART_LOT_NO_3Null Then
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT3).Value = m_dsIHI_RHF5.LOT_PART(x).PART_LOT_NO_3
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).Value = m_dsIHI_RHF5.LOT_PART(x).QTY_3
                    Else
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_LOT3).Value = ""
                        DataGridView1.Rows(x).Cells(COLUMN_INDEX_QTY3).Value = ""
                    End If
                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_DATE).Value = m_dsIHI_RHF5.LOT_PART(x).LOT_DATE_TIME
                    'DataGridView1.Rows(x).Cells(4).FormattedValue = "d"
                    DataGridView1.Rows(x).Cells(COLUMN_INDEX_TIME).Value = m_dsIHI_RHF5.LOT_PART(x).LOT_DATE_TIME

                Next
            End If
        End If

    End Sub

    Private Sub m_objPlc_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles m_objPlc.ProgressChanged
        Try
            Select Case e.ProgressPercentage
                Case REPORT_PROGRESS_READY_START_LOT
                    'PLC Ready for start lot
                    If CBool(e.UserState) Then
                        SetEnableButton(btnStartLot, False)
                        SetEnableButton(btnClear, False)
                        SetEnableButton(btnEdit, False)
                        txtPlcReady.BackColor = Color.Red
                        txtPartBarcode.Enabled = False
                        SetEnableButton(btnRework, False)
                        'txtPlcReady.BackColor = Color.Red
                    Else
                        SetEnableButton(btnStartLot, True)
                        SetEnableButton(btnClear, True)
                        SetEnableButton(btnEdit, True)
                        txtPlcReady.BackColor = Color.Lime
                        txtPartBarcode.Enabled = True
                        SetEnableButton(btnRework, True)
                        SetEnableButton(btnEndLot, False)
                        SetEnableButton(btnEndRework, False)
                        'txtPlcReady.BackColor = Color.LightGreen
                        'txtPlcReady.Enabled = CBool(e.UserState)
                    End If
                Case REPORT_PROGRESS_LOT_START_NORMAL
                    'Control Normal Lot Button
                    ' m_objPlc.Started = True
                    ' m_objPlc.Started_Normal = True
                    SetEnableButton(btnStartLot, False)
                    SetEnableButton(btnClear, False)
                    txtPlcReady.BackColor = Color.Red
                    txtPartBarcode.Enabled = False
                    SetEnableButton(btnRework, False)
                    SetEnableButton(btnEndLot, True)
                    SetEnableButton(btnEndRework, False)
                    SetEnableButton(btnEdit, False)
                    ShowDataGridView(CInt(e.UserState))
                    BlockEditData()

                Case REPORT_PROGRESS_LOT_START_REWORK
                    'Control Rework Lot Button
                    ' m_objPlc.Started = True
                    ' m_objPlc.Started_Normal = False
                    SetEnableButton(btnStartLot, False)
                    SetEnableButton(btnClear, False)
                    txtPlcReady.BackColor = Color.Red
                    txtPartBarcode.Enabled = False
                    SetEnableButton(btnRework, False)
                    SetEnableButton(btnEndLot, False)
                    SetEnableButton(btnEndRework, True)
                    SetEnableButton(btnEdit, False)
                    ShowDataGridView(CInt(e.UserState))
                    BlockEditData()

                Case REPORT_PROGRESS_PLC_REQUEST_END_LOT
                    'PLC Request End Lot
                Case REPORT_PROGRESS_REFRESH_CURRENT_QTY
                    'Refresh Curr Qty
                    txtCurrentQTY.Text = CInt(e.UserState).ToString
                    'If txtCurrentQTY.Text <> "" AndAlso txtCurrentQTY.Text = txtLotQTY.Text Then
                    '    If blnCheckFistStep Then
                    '        blnCheckFistStep = False
                    '        EndLotProcess()
                    '    End If
                    'End If
                Case REPORT_PROGRESS_CALL_END_LOT
                    'Button Enable = True
                    ' txtPlcReady.BackColor = Color.LightGreen
                    EndLotProcess()
                Case REPORT_PROGRESS_CALL_CLEAR_CURRENT_QTY
                    'reset Current QTY
                    ResetCurrentQTY()
            End Select
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub ResetCurrentQTY()
        txtCurrentQTY.Text = 0
    End Sub

    Private Function EndLotProcess() As Boolean
        'CHECK CAN End
        If Not m_objPlc.Started Then
            Return False
        Else
            'Dim dlrResult As DialogResult = MessageBox.Show("You want to reset new data?", _
            '"Save Completed.", _
            'MessageBoxButtons.YesNo)
            'If dlrResult = Windows.Forms.DialogResult.Yes Then

            'Else
            '    SaveComplete(True, False)
            'End If
            SaveComplete()
            m_objPlc.Started = False
            SetEnableButton(btnStartLot, False)
            SetEnableButton(btnClear, False)
            txtPlcReady.BackColor = Color.Red
            txtPartBarcode.Enabled = False
            SetEnableButton(btnEndLot, False)
            SetEnableButton(btnRework, False)
            SetEnableButton(btnEndRework, False)
            SetEnableButton(btnEdit, False)
            'txtCurrentQTY.Text = ""
            'txtPlcReady.BackColor = Color.LightGreen
            Return True
        End If
    End Function

    Private Sub btnEndLot_Click(sender As Object, e As System.EventArgs) Handles btnEndLot.Click
        Try
            Dim dlrResult As DialogResult = MessageBox.Show("Warning! After ending lot, production line will stop.", _
            "Do you want to end lot?", _
            MessageBoxButtons.YesNo)
            If dlrResult = Windows.Forms.DialogResult.Yes Then
                EndLotProcess()
                DeleteForm()
            Else
                Return
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub btnEndRework_Click(sender As Object, e As System.EventArgs) Handles btnEndRework.Click
        Try
            'Dim dlrResult As DialogResult = MessageBox.Show("You want to End Lot?", _
            '"End Lot", _
            'MessageBoxButtons.YesNo)
            Dim dlrResult As DialogResult = MessageBox.Show("Warning! After ending lot, production line will stop.", _
            "Do you want to end lot?", _
            MessageBoxButtons.YesNo)
            If dlrResult = Windows.Forms.DialogResult.Yes Then
                EndLotProcess()
            Else
                Return
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtCustomerLot.Enabled Then
                txtCustomerLot.Enabled = False
            Else
                txtCustomerLot.Enabled = True
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub

    Private Sub tmrButton_Tick(sender As System.Object, e As System.EventArgs) Handles tmrButton.Tick
        Try
            If m_objPlc.Started Then
                If m_objPlc.Started_Normal Then
                    If btnEndLot.BackColor = Color.Yellow Then
                        btnEndLot.BackColor = Color.LightGray
                    Else
                        btnEndLot.BackColor = Color.Yellow
                    End If
                Else
                    If btnEndRework.BackColor = Color.Yellow Then
                        btnEndRework.BackColor = Color.LightGray
                    Else
                        btnEndRework.BackColor = Color.Yellow
                    End If
                End If
            Else
                btnEndLot.BackColor = Color.LightGray
                btnEndRework.BackColor = Color.LightGray
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub txtPartBarcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPartBarcode.KeyDown
        Try

            Dim astrPartBarcode As String()
            Dim strPartNo As String = ""
            Dim strLot As String = ""
            Dim strQty As String = ""

            If e.KeyCode = Keys.Enter Then
                astrPartBarcode = txtPartBarcode.Text.Split(My.Settings.BarcodeSplitChar)
                If astrPartBarcode.Length = My.Settings.BarcodeSplitLength Then
                    strPartNo = astrPartBarcode(My.Settings.BarcodePartNoIndex)
                    strLot = astrPartBarcode(My.Settings.BarcodeLotIndex)
                    strQty = astrPartBarcode(My.Settings.BarcodeQtyIndex)
                    If IsNumeric(strQty) Then
                        AddScanDataToGrid(strPartNo, strLot, CInt(strQty))
                    Else
                        lbResult.Text = Now.ToString() & ": Invalid Qty = " & strQty
                    End If
                Else
                    lbResult.Text = Now.ToString() & ": Invalid Barcode Format [" & txtPartBarcode.Text & "]"
                End If


            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub


    Private Function GetLogPath() As String
        Dim strDataPath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)
        Dim strCompany As String = IIf(My.Application.Info.CompanyName <> "", _
                                        My.Application.Info.CompanyName, _
                                        My.Application.Info.ProductName)
        'Dim strProduct As String = System.Diagnostics.Process.GetCurrentProcess.ProcessName.Split(".")(0)
        Dim strProduct As String = My.Application.Info.ProductName
        Dim strVersion As String = My.Application.Info.Version.ToString

        'Copy from clsLogger
        Dim LOG_PATH As Object = ".\log"

        Dim strDefaultPath As String = My.Computer.FileSystem.CombinePath(strDataPath & "\" & strCompany & "\" & strProduct & "\" & strVersion, LOG_PATH)
        If My.Settings.LogPath = "" Then
            Return strDefaultPath
        ElseIf Strings.Left(My.Settings.LogPath, 1) = "." Then
            Return My.Computer.FileSystem.CombinePath(strDataPath & "\" & strCompany & "\" & strProduct & "\" & strVersion, My.Settings.LogPath)
        Else
            Return My.Settings.LogPath
        End If
    End Function

    Private Sub BackupForm()
        Try

            'Modify from clsLogger
            Dim sLogType As String = "form"
            Dim strLogText As String = ""
            Dim strFileNameOnly As String = "NotEndFormValues.txt"

            'strLogText As String



            Dim strActualPath As String
            'Dim strActualFileName As String
            'Dim strActualLogText As String

            'If Not My.Settings.IsDebugMode AndAlso (sLogType.ToLower = "info" Or sLogType.ToLower = "debug") Then
            '    Return
            'End If

            strActualPath = Me.GetLogPath
            If My.Settings.LogPath IsNot Nothing AndAlso My.Settings.LogPath <> "" Then
                If Not IO.Directory.Exists(Me.GetLogPath) Then
                    Try
                        IO.Directory.CreateDirectory(Me.GetLogPath)
                        strActualPath = Me.GetLogPath
                    Catch ex As Exception

                    End Try
                End If
            Else
                strActualPath = Me.GetLogPath
            End If


            'Dim strActualPath As String = strActualFileName
            If Not IO.Directory.Exists(strActualPath) Then
                My.Computer.FileSystem.CreateDirectory(strActualPath)
            End If

            If strFileNameOnly <> String.Empty Then
                strActualPath &= "\" & strFileNameOnly
            End If



            'Me.DeleteOldLog(strActualPath)

            'strActualFileName &= "\" & m_strSubSystem & Now.ToString("yyyyMMdd") & "_" & sLogType & ".log"

            'strActualLogText = "[" & Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") & "] " & strLogText & vbCrLf

            'Using writer As New IO.StreamWriter(strActualFileName, True)
            '    writer.WriteLine(strActualLogText)
            'End Using

            If My.Computer.FileSystem.FileExists(strActualPath) Then
                My.Computer.FileSystem.DeleteFile(strActualPath)
            End If




            Using writer As New IO.StreamWriter(strActualPath, True)
                Dim strLineTemp As String


                'Line 1
                strLineTemp = txtProductionLot.Text
                strLineTemp &= "|" & txtCustomerLot.Text
                strLineTemp &= "|" & txtLotQTY.Text

                writer.WriteLine(strLineTemp)


                'Line 2
                strLineTemp = ""
                For Each ctrlButton As Control In Me.Controls
                    If TypeOf (ctrlButton) Is Button AndAlso ctrlButton.Enabled Then
                        strLineTemp &= "|" & ctrlButton.Name
                    End If
                Next
                strLineTemp = strLineTemp.TrimStart("|")
                writer.WriteLine(strLineTemp)

                'Line 3
                strLineTemp = ""
                For Each ctrlButton As Control In Me.Controls
                    If TypeOf (ctrlButton) Is Button AndAlso Not ctrlButton.Enabled Then
                        strLineTemp &= "|" & ctrlButton.Name
                    End If
                Next
                strLineTemp = strLineTemp.TrimStart("|")
                writer.WriteLine(strLineTemp)


                'Line 4-End
                For Each row As DataGridViewRow In DataGridView1.Rows
                    strLineTemp = ""
                    For i = 3 To row.Cells.Count - 1
                        strLineTemp &= "|" & row.Cells(i).Value


                    Next
                    strLineTemp = strLineTemp.TrimStart("|")
                    writer.WriteLine(strLineTemp)

                Next




            End Using




        Catch ex As Exception

        End Try


    End Sub

    Private Sub DeleteForm()
        Try

            'Modify from clsLogger
            Dim sLogType As String = "form"
            Dim strLogText As String = ""
            Dim strFileNameOnly As String = "NotEndFormValues.txt"

            'strLogText As String



            Dim strActualPath As String
            'Dim strActualFileName As String
            'Dim strActualLogText As String

            'If Not My.Settings.IsDebugMode AndAlso (sLogType.ToLower = "info" Or sLogType.ToLower = "debug") Then
            '    Return
            'End If

            strActualPath = Me.GetLogPath
            If My.Settings.LogPath IsNot Nothing AndAlso My.Settings.LogPath <> "" Then
                If Not IO.Directory.Exists(Me.GetLogPath) Then
                    Try
                        IO.Directory.CreateDirectory(Me.GetLogPath)
                        strActualPath = Me.GetLogPath
                    Catch ex As Exception

                    End Try
                End If
            Else
                strActualPath = Me.GetLogPath
            End If


            'Dim strActualPath As String = strActualFileName
            If Not IO.Directory.Exists(strActualPath) Then
                My.Computer.FileSystem.CreateDirectory(strActualPath)
            End If

            If strFileNameOnly <> String.Empty Then
                strActualPath &= "\" & strFileNameOnly
            End If



            'Me.DeleteOldLog(strActualPath)

            'strActualFileName &= "\" & m_strSubSystem & Now.ToString("yyyyMMdd") & "_" & sLogType & ".log"

            'strActualLogText = "[" & Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") & "] " & strLogText & vbCrLf

            'Using writer As New IO.StreamWriter(strActualFileName, True)
            '    writer.WriteLine(strActualLogText)
            'End Using

            If My.Computer.FileSystem.FileExists(strActualPath) Then
                My.Computer.FileSystem.DeleteFile(strActualPath)
            End If








        Catch ex As Exception

        End Try


    End Sub

    Private Sub RestoreForm()
        Try

            'Modify from clsLogger
            Dim sLogType As String = "form"
            Dim strLogText As String = ""
            Dim strFileNameOnly As String = "NotEndFormValues.txt"

            'strLogText As String




            Dim strActualPath As String
            'Dim strActualLogText As String

            'If Not My.Settings.IsDebugMode AndAlso (sLogType.ToLower = "info" Or sLogType.ToLower = "debug") Then
            '    Return
            'End If

            strActualPath = Me.GetLogPath
            If My.Settings.LogPath IsNot Nothing AndAlso My.Settings.LogPath <> "" Then
                If Not IO.Directory.Exists(Me.GetLogPath) Then
                    Try
                        IO.Directory.CreateDirectory(Me.GetLogPath)
                        strActualPath = Me.GetLogPath
                    Catch ex As Exception

                    End Try
                End If
            Else
                strActualPath = Me.GetLogPath
            End If


            'Dim strActualPath As String = strActualFileName
            If Not IO.Directory.Exists(strActualPath) Then
                My.Computer.FileSystem.CreateDirectory(strActualPath)
            End If

            If strFileNameOnly <> String.Empty Then
                strActualPath &= "\" & strFileNameOnly
            End If



            'Me.DeleteOldLog(strActualPath)

            'strActualFileName &= "\" & m_strSubSystem & Now.ToString("yyyyMMdd") & "_" & sLogType & ".log"

            'strActualLogText = "[" & Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") & "] " & strLogText & vbCrLf

            'Using writer As New IO.StreamWriter(strActualFileName, True)
            '    writer.WriteLine(strActualLogText)
            'End Using

            'If My.Computer.FileSystem.FileExists(strActualPath) Then
            '    Using writer As New IO.StreamReader(strActualFileName, True)
            '        writer.WriteLine(strActualLogText)
            '    End Using

            'End If

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(strActualPath)
                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.TrimWhiteSpace = False
                MyReader.Delimiters = New String() {"|"}
                Dim strCurrentRow As String()
                'Dim blnCheck As Boolean
                'Loop through all of the fields in the file. 
                'If any lines are corrupt, report an error and continue parsing. 


                '1st line
                strCurrentRow = MyReader.ReadFields()
                txtProductionLot.Text = strCurrentRow(0).Trim()
                txtCustomerLot.Text = strCurrentRow(1).Trim()
                txtLotQTY.Text = strCurrentRow(2).Trim()


                'line 2
                strCurrentRow = MyReader.ReadFields()
                For i = 0 To strCurrentRow.Count - 1
                    Me.Controls(strCurrentRow(i)).Enabled = True
                Next

                'For Each col In strCurrentRow
                '    Me.Controls(col).Enabled = True
                'Next

                'line 3
                strCurrentRow = MyReader.ReadFields()
                For i = 0 To strCurrentRow.Count - 1
                    Me.Controls(strCurrentRow(i)).Enabled = False
                Next
                'For Each col In strCurrentRow
                '    Me.Controls(col).Enabled = False
                'Next

                'line 4 to end
                Dim intRow = 0
                While Not MyReader.EndOfData
                    Try
                        strCurrentRow = MyReader.ReadFields()

                        For i = 0 To strCurrentRow.Count - 1
                            DataGridView1.Rows(intRow).Cells(i + 3).Value = strCurrentRow(i).Trim()
                        Next




                        intRow += 1


                    Catch ex As Exception

                    End Try
                End While

            End Using
        Catch ex As Exception

        End Try


    End Sub

    Private Sub StopService()
        Try
            Dim myService As ServiceController = New ServiceController("IHITurboDataFilingSystemService")
            If myService.Status = ServiceControllerStatus.Running AndAlso myService.CanStop Then
                myService.Stop()
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try


    End Sub

    Private Sub StartService()
        Try
            Dim myService As ServiceController = New ServiceController("IHITurboDataFilingSystemService")
            If myService.Status = ServiceControllerStatus.Stopped Or myService.Status = ServiceControllerStatus.StopPending Then
                myService.Start()
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try


    End Sub



    Private Sub AddScanDataToGrid(ByVal partNo As String, ByVal lotNo As String, ByVal qty As Integer)
        Dim intRow As Integer = 0
        While intRow < DataGridView1.Rows.Count
            'If DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_PART_NO).Value.ToLower = partNo.ToLower Then
            If DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_PART_NO).Value = partNo Then
                If Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT1).EditedFormattedValue) = "" _
                    Or Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT1).EditedFormattedValue).ToLower = lotNo.ToLower Then
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT1).Value = lotNo
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_QTY1).Value = qty
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_DATE).Value = Now.ToString("dd/MM/yyyy")
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_TIME).Value = Now.ToString("HH:mm:ss")
                    lbResult.Text = ""
                    txtPartBarcode.Text = ""
                    txtPartBarcode.Focus()
                    Return
                ElseIf Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT2).EditedFormattedValue) = "" _
                    Or Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT2).EditedFormattedValue).ToLower = lotNo.ToLower Then
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT2).Value = lotNo
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_QTY2).Value = qty
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_DATE).Value = Now.ToString("dd/MM/yyyy")
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_TIME).Value = Now.ToString("HH:mm:ss")
                    lbResult.Text = ""
                    txtPartBarcode.Text = ""
                    txtPartBarcode.Focus()
                    Return
                ElseIf Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT3).EditedFormattedValue) = "" _
                    Or Trim(DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT3).EditedFormattedValue).ToLower = lotNo.ToLower Then
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_LOT3).Value = lotNo
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_QTY3).Value = qty
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_DATE).Value = Now.ToString("dd/MM/yyyy")
                    DataGridView1.Rows(intRow).Cells(COLUMN_INDEX_TIME).Value = Now.ToString("HH:mm:ss")
                    lbResult.Text = ""
                    txtPartBarcode.Text = ""
                    txtPartBarcode.Focus()
                    Return
                Else
                    lbResult.Text = Now.ToString() & ": Max 3 Lot Per Part."
                    txtPartBarcode.Text = ""
                    txtPartBarcode.Focus()
                    Return
                End If
            Else
                intRow += 1
            End If
        End While

        If intRow = DataGridView1.Rows.Count Then
            lbResult.Text = Now.ToString() & ": Part No = " & partNo & " not found"
            txtPartBarcode.Text = ""
            txtPartBarcode.Focus()
        End If
    End Sub

    Private Sub txtProductionLot_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtProductionLot.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                lbResult.Text = ""
                txtLotQTY.Focus()
            End If
        Catch ex As Exception
            HandleError(ex)
        End Try
    End Sub

    Private Sub HandleError(ByVal ex As Exception)
        lbResult.Text = Now.ToString() & ": " & ex.Message
        m_objLogger.AppendLog(ex)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        Try
            BackupForm()
        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub


    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        If MsgBox("Do you want to clear input data on screen?", MsgBoxStyle.YesNo + vbExclamation, "Do you want to start new lot?") <> MsgBoxResult.Yes Then
            'user answer no
            Exit Sub
        End If

        Me.ClearData()
    End Sub
End Class