Public Class clsStateWaitEnd

    Inherits clsStateBase

    'Private m_objMachineData As clsMachineData

    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        MyBase.New(plcLineThread)
        'm_objMachineData = New clsMachineData(plcLineThread.LineSetting, m_clsLogger)
        'm_objBkgImport = New clsImportDataFile
    End Sub

    Public Overrides Sub Initial(Optional inObj As Object = Nothing)
        'TODO: Implement
    End Sub

    Public Overrides Sub Process()
        'TODO: Implement
        If Not m_blnWorkDone Then
            'TODO check Process type
            If m_objPlcLineThread.Started_Normal Then
                m_intWorkType = 0
            Else
                m_intWorkType = 4
            End If
            'Read current QTY
            'm_objPlcLineThread.LotData.LotQty()
            'Me.ReplyZeroToPLC()
            Me.Reply1ToPlc()
            Me.m_blnWorkDone = True
        Else
            'TODO check Process type
            If m_objPlcLineThread.Started_Normal Then
                m_intWorkType = 0
            Else
                m_intWorkType = 4
            End If

            'Read current QTY
            m_objPlcLineThread.ReportProgress(50, Me.ReadPlcCurrentQTY)

            If Not m_objPlcLineThread.Started Then
                m_objPlcLineThread.ReportProgress(70)
                m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Complete, Go to complete state", "Info")
                m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateCompleteEnd)
                m_objPlcLineThread.ForceToProcessCurrentState()
                Return
            End If

            Dim intReadPlcStatus As Integer = Me.ReadPlcStatus
            Select Case intReadPlcStatus
                Case PLC_READ_STATUS_READ_FAILED
                    'DO NOTIHNG
                Case PLC_READ_STATUS_SAME_STATUS
                    'DO NOTHING
                Case PLC_READ_STATUS_WAITING
                    'DO NOTIHNG
                Case PLC_READ_STATUS_REQUEST
                    Me.WriteErrorStatus(PLC_WRITE_STATUS_WAITING)
                    m_objPlcLineThread.ReportProgress(70)
                    m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Complete, Go to complete state", "Info")
                    m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateCompleteEnd)
                    m_objPlcLineThread.ForceToProcessCurrentState()
                Case Else
                    'm_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Error, Go to error state", "Info")
                    'm_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateErrorEnd)
                    'm_objPlcLineThread.ForceToProcessCurrentState()
                    Me.WriteErrorStatus(PLC_WRITE_STATUS_ERROR)
            End Select
        End If
    End Sub

    Private Function ReadPlcCurrentQTY() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadDataCurrentQTY _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", "intReadStatus = " & intReadStatus & " at DM" & My.Settings.ReadDataCurrentQTY, "Info")
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        'If Not IsNewPlcMessage(intReadStatus) Then
        '    Return PLC_READ_STATUS_SAME_STATUS
        'End If

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "End Function", "Info")
        Return intReadStatus
    End Function

    Private Sub ReplyZeroToPLC()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddress + m_intWorkType _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_objPlc.WriteMemoryWordString(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , "asdfghjkl;".Substring(0, 20).PadRight(20, " "))
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "write = " & PLC_WRITE_STATUS_WAITING & " at DM" & (My.Settings.WriteStatusAddress + m_intWorkType), "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While
        m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "End Function", "Info")
    End Sub

    Private Sub Reply1ToPlc()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddressPCEnd + m_intWorkType _
                                                , {PLC_WRITE_STATUS_PROCESSING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "write = " & PLC_WRITE_STATUS_PROCESSING & " at DM" & (My.Settings.WriteStatusAddressPCEnd + m_intWorkType), "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", ex)
            End Try

            'Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While

        m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "End Function", "Info")
    End Sub

    Private Function ReadPlcStatus() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddressEnd + m_intWorkType _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "intReadStatus = " & intReadStatus & " at DM" & (My.Settings.ReadStatusAddressEnd + m_intWorkType), "Info")
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        'If Not IsNewPlcMessage(intReadStatus) Then
        '    Return PLC_READ_STATUS_SAME_STATUS
        'End If

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "End Function", "Info")
        Return intReadStatus
    End Function

    Private Function ReadPlcDataAndWriteToFile() As Integer
        Try
            Dim strData1 As String = m_objPlc.ReadMemoryString(My.Settings.ReadDataMemoryType _
                                                               , My.Settings.ReadDataAddress _
                                                               , My.Settings.ReadDataAsciiLength)
            Dim bytData1() As Byte = System.Text.Encoding.ASCII.GetBytes(strData1)
            Dim strHex As String = BitConverter.ToString(bytData1)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "strData1 = " & strData1, "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "hexData1 = " & strHex, "Info")
            Dim intData2() As Integer = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadDataMemoryType _
                                                               , My.Settings.ReadDataAddress + My.Settings.ReadDataAsciiLength _
                                                               , My.Settings.ReadDataBcdLength _
                                                               , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BIN)
            Dim strHexData2(intData2.Length - 1) As String
            For i As Integer = 0 To intData2.Length - 1
                strHexData2(i) = Hex$(intData2(i)).PadLeft(4, "0")
            Next
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", "intData2 = " & String.Join(",", strHexData2), "Info")

            'm_objMachineData.Init()
            'm_objMachineData.ExtractMachineData(strData1, intData2)
            'If m_objLineSetting.DoCopyFile Then
            '    Dim blnHasImage As Boolean = intData2(15) = 1
            '    m_objMachineData.CopyImage(blnHasImage)
            'End If
            'If m_objLineSetting.UseCsvMode Then
            '    m_objMachineData.WriteDataToFile()
            'End If
            'If m_objLineSetting.UseXlsMode Then
            '    m_objMachineData.WriteDataToExcel()
            'End If

            ''TODO Export I/F for import
            'm_objMachineData.ExportTextFileForImport()

            m_blnWorkDone = True
            Return PLC_READ_STATUS_COMPLETE
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
            Return PLC_READ_STATUS_ERROR
        End Try
    End Function
End Class
