Public Class clsStateProcessing

    Inherits clsStateBase

    Private m_objMachineData As clsMachineData

    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        MyBase.New(plcLineThread)
        m_objMachineData = New clsMachineData(plcLineThread.LineSetting, m_clsLogger)
        'm_objBkgImport = New clsImportDataFile
    End Sub

    Public Overrides Sub Initial(Optional inObj As Object = Nothing)
        'TODO: Implement
    End Sub

    Public Overrides Sub Process()
        'TODO: Implement
        If Not m_blnWorkDone Then
            Me.Reply1ToPlc()
            Me.m_blnWorkDone = True
        Else
            Dim intReadPlcStatus As Integer = Me.ReadPlcDataAndWriteToFile

            '20160524-16_00 UNCOMMENT STARTED
            Select Case intReadPlcStatus
                Case PLC_READ_STATUS_READ_FAILED
                    'DO NOTIHNG
                Case PLC_READ_STATUS_COMPLETE
                    m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Complete, Go to complete state", "Info")
                    m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateComplete)
                    m_objPlcLineThread.ForceToProcessCurrentState()
                Case Else
                    m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Error, Go to error state", "Info")
                    m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateError)
                    m_objPlcLineThread.ForceToProcessCurrentState()
            End Select
            'UNCOMMENT END
        End If
    End Sub

    Private Sub Reply1ToPlc()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
                                                , m_objLineSetting.WriteStatusAddress _
                                                , {PLC_WRITE_STATUS_PROCESSING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
                blnSent = True
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
        End While

        m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "End Function", "Info")
    End Sub

    Private Function ReadPlcStatus() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadStatusMemoryType _
                                                           , m_objLineSetting.ReadStatusAddress _
                                                           , m_objLineSetting.ReadStatusLength _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        If Not IsNewPlcMessage(intReadStatus) Then
            Return PLC_READ_STATUS_SAME_STATUS
        End If

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "End Function", "Info")
        Return intReadStatus
    End Function

    Private Function ReadPlcDataAndWriteToFile() As Integer
        Try
            Dim strData1 As String = m_objPlc.ReadMemoryString(m_objLineSetting.ReadDataMemoryType _
                                                               , m_objLineSetting.ReadDataAddress _
                                                               , m_objLineSetting.ReadDataAsciiLength)
            Dim bytData1() As Byte = System.Text.Encoding.ASCII.GetBytes(strData1)
            Dim strHex As String = BitConverter.ToString(bytData1)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "strData1 = " & strData1, "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "hexData1 = " & strHex, "Info")
            Dim intData2() As Integer = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadDataMemoryType _
                                                               , m_objLineSetting.ReadDataAddress + m_objLineSetting.ReadDataAsciiLength _
                                                               , m_objLineSetting.ReadDataBcdLength _
                                                               , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BIN)
            Dim strHexData2(intData2.Length - 1) As String
            For i As Integer = 0 To intData2.Length - 1
                strHexData2(i) = Hex$(intData2(i)).PadLeft(4, "0")
            Next
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "intData2 = " & String.Join(",", strHexData2), "Info")

            m_objMachineData.Init()
            m_objMachineData.ExtractMachineData(strData1, intData2)
            If m_objLineSetting.DoCopyFile Then
                Dim blnHasImage As Boolean = intData2(15) = 1
                m_objMachineData.CopyImage(blnHasImage)
            End If
            If m_objLineSetting.UseCsvMode Then
                m_objMachineData.WriteDataToFile()
            End If
            If m_objLineSetting.UseXlsMode Then
                m_objMachineData.WriteDataToExcel()
            End If

            'TODO Export I/F for import
            m_objMachineData.ExportTextFileForImport()

            m_blnWorkDone = True
            Return PLC_READ_STATUS_COMPLETE
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
            Return PLC_READ_STATUS_ERROR
        End Try
    End Function
End Class
