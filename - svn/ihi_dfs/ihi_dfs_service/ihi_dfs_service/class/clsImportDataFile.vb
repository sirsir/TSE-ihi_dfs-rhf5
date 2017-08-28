Imports System.IO
Imports System.Text.RegularExpressions

Public Class clsImportDataFile
    Inherits System.ComponentModel.BackgroundWorker


#Region "Attribute"

    Private m_dsIHI_RHF5 As ihi_rhf5

    Private m_taManager As ihi_rhf5TableAdapters.TableAdapterManager

    Private m_taLine_Master As ihi_rhf5TableAdapters.LINE_MASTERTableAdapter
    Private m_taMachine As ihi_rhf5TableAdapters.MACHINETableAdapter
    Private m_taMachine_Columns As ihi_rhf5TableAdapters.MACHINE_COLUMNSTableAdapter
    Private m_taSerial As ihi_rhf5TableAdapters.SERIALTableAdapter
    Private m_taResult As ihi_rhf5TableAdapters.RESULTTableAdapter
    Private m_taMachine_Data_STR As ihi_rhf5TableAdapters.MACHINE_DATA_STRTableAdapter
    Private m_taFindMachine As ihi_rhf5TableAdapters.FindMachineTableAdapter
    Private m_taCheckMyMachineAndMaxSEQ As ihi_rhf5TableAdapters.CheckMyMachineAndMaxSEQTableAdapter

    Private drSerial As ihi_rhf5.SERIALRow
    Private drResult As ihi_rhf5.RESULTRow
    Private drMachine_Data_STR As ihi_rhf5.MACHINE_DATA_STRRow

    Private m_astrFileNameList As ArrayList
    Private m_objLogger As clsLogger

    Private m_intCountTotal As Integer
    Private m_strPathFiles As String

#End Region

#Region "Event"
    Private Sub clsImportDataFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Me.DoWork

        My.Application.ChangeCulture("en-US")
        My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
        My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

        Dim di As IO.DirectoryInfo

        If Not My.Computer.FileSystem.DirectoryExists(My.Settings.WatchPath) Then
            MsgBox("Path ""{0}"" not exist, please recheck configuration and restart program (" & My.Settings.WatchPath & " )")
            Return
        End If
        di = New IO.DirectoryInfo(My.Settings.WatchPath)

        While Not Me.CancellationPending
            Try
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
                m_objLogger.AppendLog(Me.GetType.Name, "CountTextFile", ex)
            Finally
                Threading.Thread.Sleep(1000)
            End Try

        End While

    End Sub
#End Region

#Region "Constructor/Destructor"
    Public Sub New()
        Me.WorkerSupportsCancellation = True
        Me.WorkerReportsProgress = True

        m_dsIHI_RHF5 = New ihi_rhf5

        m_taManager = New ihi_rhf5TableAdapters.TableAdapterManager

        m_taLine_Master = New ihi_rhf5TableAdapters.LINE_MASTERTableAdapter
        m_taMachine = New ihi_rhf5TableAdapters.MACHINETableAdapter
        m_taMachine_Columns = New ihi_rhf5TableAdapters.MACHINE_COLUMNSTableAdapter
        m_taSerial = New ihi_rhf5TableAdapters.SERIALTableAdapter
        m_taFindMachine = New ihi_rhf5TableAdapters.FindMachineTableAdapter
        m_taCheckMyMachineAndMaxSEQ = New ihi_rhf5TableAdapters.CheckMyMachineAndMaxSEQTableAdapter

        m_taResult = New ihi_rhf5TableAdapters.RESULTTableAdapter
        m_taMachine_Data_STR = New ihi_rhf5TableAdapters.MACHINE_DATA_STRTableAdapter

        m_taManager.SERIALTableAdapter = m_taSerial
        m_taManager.RESULTTableAdapter = m_taResult
        m_taManager.MACHINE_DATA_STRTableAdapter = m_taMachine_Data_STR

        m_astrFileNameList = New ArrayList
        m_objLogger = New clsLogger
        LoadMaster()
    End Sub
#End Region

#Region "Method"
    Public Sub Init()
        m_dsIHI_RHF5.Clear()
    End Sub

    Public Sub LoadMaster()
        Try
            With m_dsIHI_RHF5
                m_taSerial.Fill(.SERIAL)
                m_taResult.Fill(.RESULT)
                m_taMachine_Data_STR.Fill(.MACHINE_DATA_STR)
            End With
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "LoadMaster", ex)
        End Try
    End Sub

    Public Sub ExtractFile(ByVal filePath As String)

        m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ.Clear()
        m_dsIHI_RHF5.FindMachine.Clear()

        m_dsIHI_RHF5.MACHINE_DATA_STR.Clear()
        m_dsIHI_RHF5.RESULT.Clear()
        m_dsIHI_RHF5.SERIAL.Clear()

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePath)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.TrimWhiteSpace = False
            MyReader.Delimiters = New String() {"|"}
            Dim strCurrentRow As String()
            Dim blnCheck As Boolean
            'Loop through all of the fields in the file. 
            'If any lines are corrupt, report an error and continue parsing. 


            'BALL: Why skip?
            ''skip 1st line
            'strCurrentRow = MyReader.ReadFields()
            blnCheck = True

            While Not MyReader.EndOfData
                Try
                    strCurrentRow = MyReader.ReadFields()
                    m_intCountTotal += 1

                    If blnCheck Then
                        blnCheck = False
                        m_taSerial.FillBy_FindSerial_BH(m_dsIHI_RHF5.SERIAL, strCurrentRow(0).Trim)
                        If m_dsIHI_RHF5.SERIAL IsNot Nothing AndAlso m_dsIHI_RHF5.SERIAL.Count > 0 Then     'Serial Exist -> Check if need to seq + 1
                            'm_taCheckMyMachineAndMaxSEQ.Fill_CheckMyMachine(m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ, strCurrentRow(0).Trim)
                            m_taMachine_Columns.FillBy_ID(m_dsIHI_RHF5.MACHINE_COLUMNS, strCurrentRow(10))
                            If (m_dsIHI_RHF5.MACHINE_COLUMNS IsNot Nothing AndAlso m_dsIHI_RHF5.MACHINE_COLUMNS.Count > 0) Then
                                'TODO: Test new check create new serial seq logic
                                m_taCheckMyMachineAndMaxSEQ.FillByFromCurrentMachineHasResultInThisSeq(m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ, strCurrentRow(0).Trim, m_dsIHI_RHF5.MACHINE_COLUMNS(0).MACHINE_ID)

                                If (m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ IsNot Nothing AndAlso m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ.Count > 0) Then

                                    'New Serial Seq
                                    drSerial = m_dsIHI_RHF5.SERIAL.NewSERIALRow
                                    drSerial.SERIAL_BH = strCurrentRow(0).Trim
                                    drSerial.SERIAL_TS = strCurrentRow(1).Trim
                                    drSerial.SERIAL_CW = strCurrentRow(2).Trim
                                    drSerial.SERIAL_ITA = strCurrentRow(3).Trim
                                    drSerial.SERIAL_CS = strCurrentRow(4).Trim

                                    drSerial.SEQ_NO = m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ(0).MAX_SEQ_NO + 1
                                    drSerial.CREATED_WHEN = Now
                                    m_dsIHI_RHF5.SERIAL.AddSERIALRow(drSerial)
                                    m_objLogger.AppendLog("Add Serial [" & strCurrentRow(0) & "]", "Info")

                                    drResult = m_dsIHI_RHF5.RESULT.NewRESULTRow
                                    drResult.SERIALRow = drSerial
                                    drResult.MODE = strCurrentRow(5)
                                    drResult.MACHINE_ID = m_dsIHI_RHF5.MACHINE_COLUMNS(0).MACHINE_ID
                                    drResult.LOT_NO = strCurrentRow(6)
                                    drResult.FILE_NAME = strCurrentRow(7)
                                    drResult.RESULT_DATE_TIME = strCurrentRow(8)
                                    drResult.STATUS = strCurrentRow(9)
                                    drResult.CREATED_WHEN = Now
                                    m_dsIHI_RHF5.RESULT.AddRESULTRow(drResult)
                                    m_objLogger.AppendLog("Add Result [" & strCurrentRow(0) & "]", "Info")
                                Else
                                    drSerial = m_dsIHI_RHF5.SERIAL.Select("SEQ_NO = " & m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ(0).MAX_SEQ_NO)(0)
                                    drSerial.SERIAL_BH = strCurrentRow(0).Trim
                                    drSerial.SERIAL_TS = strCurrentRow(1).Trim
                                    drSerial.SERIAL_CW = strCurrentRow(2).Trim
                                    drSerial.SERIAL_ITA = strCurrentRow(3).Trim
                                    drSerial.SERIAL_CS = strCurrentRow(4).Trim
                                    'drSerial.SEQ_NO = m_dsIHI_RHF5.CheckMyMachineAndMaxSEQ(0).MAX_SEQ_NO
                                    m_objLogger.AppendLog("Update Serial [" & strCurrentRow(0) & "]", "Info")

                                    drResult = m_dsIHI_RHF5.RESULT.NewRESULTRow
                                    drResult.SERIALRow = drSerial
                                    drResult.MODE = strCurrentRow(5)
                                    drResult.MACHINE_ID = m_dsIHI_RHF5.MACHINE_COLUMNS(0).MACHINE_ID
                                    drResult.LOT_NO = strCurrentRow(6)
                                    drResult.FILE_NAME = strCurrentRow(7)
                                    drResult.RESULT_DATE_TIME = strCurrentRow(8)
                                    drResult.STATUS = strCurrentRow(9)
                                    drResult.CREATED_WHEN = Now
                                    m_dsIHI_RHF5.RESULT.AddRESULTRow(drResult)
                                    m_objLogger.AppendLog("Add Result [" & strCurrentRow(0) & "]", "Info")
                                End If

                            Else    'Machine Column Id not exist -> ABNORMAL!!!
                                m_objLogger.AppendLog(" Machine Column Id [" & strCurrentRow(0) & "] not exist -> ABNORMAL!!!", "Error")
                                Exit While
                            End If

                        Else             'Serial Not Exist -> Create new serial, seq = 1

                            drSerial = m_dsIHI_RHF5.SERIAL.NewSERIALRow
                            drSerial.SERIAL_BH = strCurrentRow(0).Trim
                            drSerial.SERIAL_TS = strCurrentRow(1).Trim
                            drSerial.SERIAL_CW = strCurrentRow(2).Trim
                            drSerial.SERIAL_ITA = strCurrentRow(3).Trim
                            drSerial.SERIAL_CS = strCurrentRow(4).Trim
                            drSerial.SEQ_NO = 1
                            drSerial.CREATED_WHEN = Now
                            m_dsIHI_RHF5.SERIAL.AddSERIALRow(drSerial)
                            m_objLogger.AppendLog("Add Serial [" & strCurrentRow(0) & "]", "Info")

                            m_taMachine_Columns.FillBy_ID(m_dsIHI_RHF5.MACHINE_COLUMNS, strCurrentRow(10))
                            drResult = m_dsIHI_RHF5.RESULT.NewRESULTRow
                            drResult.SERIALRow = drSerial
                            drResult.MODE = strCurrentRow(5)
                            drResult.MACHINE_ID = m_dsIHI_RHF5.MACHINE_COLUMNS(0).MACHINE_ID
                            drResult.LOT_NO = strCurrentRow(6)
                            drResult.FILE_NAME = strCurrentRow(7)
                            drResult.RESULT_DATE_TIME = strCurrentRow(8)
                            drResult.STATUS = strCurrentRow(9)
                            drResult.CREATED_WHEN = Now
                            m_dsIHI_RHF5.RESULT.AddRESULTRow(drResult)
                            m_objLogger.AppendLog("Add Result [" & strCurrentRow(0) & "]", "Info")
                        End If

                    End If

                    'Extract from Machine_Data_STR
                    If Not ExtractFromImportMachine_Data_STRMapping(strCurrentRow) Then
                        m_objLogger.AppendLog("Skip record [" & strCurrentRow(0) & "]", "Info")
                        Continue While
                    End If

                    m_taManager.UpdateAll(m_dsIHI_RHF5)

                Catch ex As Exception
                    m_objLogger.AppendLog(Me.GetType.Name, "ReadFields", ex)
                End Try
            End While

        End Using
    End Sub

    Private Function ExtractFromImportMachine_Data_STRMapping(ByVal astrRowData() As String) As Boolean
        Dim strData As String
        strData = ""

        Try


            For x = 0 To astrRowData.Count - 1
                strData &= astrRowData(x) & "|"
            Next

            strData = Mid(strData, 1, strData.Length - 1)

            drMachine_Data_STR = m_dsIHI_RHF5.MACHINE_DATA_STR.NewMACHINE_DATA_STRRow
            drMachine_Data_STR.RESULTRow = drResult
            drMachine_Data_STR.MACHINE_COLUMNS_ID = astrRowData(10)
            drMachine_Data_STR.DATA = astrRowData(11)

            'Add on 20160530-11_39 By Ball


            m_dsIHI_RHF5.MACHINE_DATA_STR.AddMACHINE_DATA_STRRow(drMachine_Data_STR)
            m_objLogger.AppendLog("Add MACHINE_DATA_STR [" & strData & "]", "Info")

            Return True
        Catch ex As Exception
            m_objLogger.AppendLog("CAN NOT Add MACHINE_DATA_STR [" & strData & "]", "Info")
            m_objLogger.AppendLog(Me.GetType.Name, "ExtractFromImportMachine_Data_STRMapping", ex)
            Return False
        End Try
    End Function

    Private Sub ImportDataFile()
        Dim intResult As Integer = 0
        m_intCountTotal = 0
        m_strPathFiles = ""

        If Me.m_astrFileNameList.Count > 0 Then

            Dim strFileNameWithPath As String = Me.m_astrFileNameList(0).ToString
            Dim strFileName As String = strFileNameWithPath.Substring(My.Settings.WatchPath.Length + 1)
            Dim strFileRename As String = Format(Now, "yyyyMMddHHmmss") & "_" & strFileName
            'Dim strFileRenameWithPath As String = My.Settings.WatchPath & "\" & strFileRename
            Dim strBackupName As String = My.Settings.BackupPath & "\" & Format(Now, "yyyyMMdd") & "\"
            If Not My.Computer.FileSystem.DirectoryExists(strBackupName) Then
                My.Computer.FileSystem.CreateDirectory(strBackupName)
            End If

            strBackupName += strFileRename

            Dim bCheckPass As Boolean = False
            Dim bImportPass As Boolean = False
            Dim blnCanRead As Boolean = False

            Try
                Using theFile As New IO.FileStream(strFileNameWithPath, FileMode.Open, FileAccess.Read)
                    If Not theFile.CanRead Then
                        m_objLogger.AppendLog(Me.GetType.Name, "ImportDataFile", "Cannot read the file: " & strFileName, "Error")
                    Else
                        blnCanRead = True
                    End If
                End Using

                'MoveFile(strFileNameWithPath, strFileRenameWithPath)

                If blnCanRead Then
                    Me.ExtractFile(strFileNameWithPath)
                End If

                'Import complete, move file to backup
                Me.m_astrFileNameList.RemoveAt(0)
                MoveFile(strFileNameWithPath, strBackupName)

            Catch ex As Exception
                m_objLogger.AppendLog(ex)
                Me.m_astrFileNameList.RemoveAt(0)
                MoveFile(strFileNameWithPath, strBackupName)
            End Try

        End If
    End Sub

    Private Sub MoveFile(ByVal fromFile As String, ByVal toFile As String)
        If My.Computer.FileSystem.FileExists(fromFile) = True Then
            My.Computer.FileSystem.MoveFile(fromFile, toFile, True)
        End If
    End Sub

    Public Function ValueForSQL(ByVal varValue As Object, _
                            Optional ByVal varDelimiter As Object = Nothing, _
                            Optional ByVal blnSupportThLang As Boolean = False) As String
        On Error Resume Next
        Dim strResult As String
        Dim strText As String
        Dim vtType As VariantType

        strResult = "NULL"

        If Not IsNothing(varValue) AndAlso Not IsDBNull(varValue) Then
            vtType = VarType(varValue)

            If Trim(varValue) <> "" Then
                Select Case vtType
                    Case VariantType.String     'Kong change from vbString --> VariantType.String
                        strText = RTrim(varValue)
                        If Asc(Strings.Right(strText, 1)) = 0 Then
                            strText = Strings.Left(strText, Len(strText) - 1)
                        End If
                        If Not IsNothing(varDelimiter) Then
                            strText = strText.Split(varDelimiter)(0)
                        End If

                        strResult = "'" & Replace(strText, "'", "''") & "'"

                        If blnSupportThLang Then
                            strResult = "N" & strResult
                        End If
                    Case VariantType.Date       'Kong change from vbDate --> VariantType.Date
                        strResult = GetDateValueForSQL(varValue)
                        'Debug.Assert(False)  'please use GetDateValueForSQL(dt,cnn) instead.
                    Case VariantType.Boolean    'Kong change from vbBoolean --> VariantType.Boolean
                        strResult = IIf(varValue, "1", "0")
                    Case VariantType.Double     'Kong add for handle Double.NaN
                        If Not Double.IsNaN(varValue) Then
                            strResult = CType(varValue, String)
                        End If
                    Case Else
                        strResult = CType(varValue, String)
                End Select
            End If
        End If

        Return strResult
    End Function

    Public Function GetDateValueForSQL(ByVal varDateTime As Object) As String

        Const cstrDateFormatForSQL As String = "/MM/dd"

        Dim strText As String
        Dim strResult As String

        strResult = ""

        'If IsDBNull(varDateTime) Then
        If IsDBNull(varDateTime) OrElse IsNothing(varDateTime) Then 'Kong 20091109 : add "OrElse IsNothing(varDateTime)"
            strResult = "NULL"
        Else
            strText = CDate(varDateTime).Year & Format(varDateTime, cstrDateFormatForSQL)
            'strText = Replace(strText, " 12:00:00", "")

            strResult = "CONVERT(datetime, '" & strText & "',111)"
        End If

        Return strResult
    End Function

#End Region
End Class
