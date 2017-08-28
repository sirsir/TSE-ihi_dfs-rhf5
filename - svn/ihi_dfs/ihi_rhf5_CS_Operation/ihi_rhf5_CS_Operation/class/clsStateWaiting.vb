Public Class clsStateWaiting
    Inherits clsStateBase

    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        MyBase.New(plcLineThread)
    End Sub

    Public Overrides Sub Initial(Optional inObj As Object = Nothing)
        'TODO: Implement
    End Sub

    Public Overrides Sub Process()
        'TODO: Implement
        m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Start Function", "Info")
        If Not m_blnWorkDone Then
            Me.ReplyZeroToPLC()                 '5023
            Me.ReplyZeroToPLC_END()             '5021
            Me.ReplyZeroToPLC_ReWork_End()      '5027
            Me.ReplyZeroToPLC_ReWork_Start()    '5025
            'TODO : clear data Production Lot and QTY
            Me.ClearData()
            Me.m_blnWorkDone = True
            Me.m_objPlcLineThread.ReportProgress(10, True)
        Else
            m_clsLogger.AppendLog(Me.GetType.Name, "Process", "m_blnWorkDone = True", "Info")
            Dim intReadPlcStatus = Me.ReadPlcReadyStartStatus()
            'Dim intReadPlcStatus_Rework = Me.ReadPlcStatus_Rework()

            If (intReadPlcStatus = PLC_READ_STATUS_REQUEST) Then
                m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Read Request status", "Info")
                Me.WriteErrorStatus(PLC_WRITE_STATUS_WAITING)
                Me.m_objPlcLineThread.ReportProgress(10, False)
                Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateProcessing)
                m_objPlcLineThread.ForceToProcessCurrentState()
                m_blnWorkDone = True
            ElseIf intReadPlcStatus = PLC_READ_STATUS_SAME_STATUS Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_WAITING Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_READ_FAILED Then
                'DO NOTHING
            Else
                'm_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Other, Go to error state", "Error")
                'Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateError)
                'Me.m_objPlcLineThread.ForceToProcessCurrentState()
                Me.WriteErrorStatus(PLC_WRITE_STATUS_ERROR)
            End If
        End If
        m_clsLogger.AppendLog(Me.GetType.Name, "Process", "End Function", "Info")
    End Sub

    Private Function ReadPlcStatus_Rework() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus_Rework", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus_Rework", "intReadStatus = " & intReadStatus & " at DM" & (My.Settings.ReadStatusAddress + 4), "Info")
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus_Rework", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        'If Not IsNewPlcMessage(intReadStatus) Then
        '    Return PLC_READ_STATUS_SAME_STATUS
        'End If

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus_Rework", "End Function", "Info")
        Return intReadStatus
    End Function

    Private Function ReadPlcStatus() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "intReadStatus = " & intReadStatus & " at DM" & (My.Settings.ReadStatusAddress), "Info")
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

    Private Function ReadPlcReadyStartStatus() As Integer
        Dim intReadNormalStatus As Integer
        Dim intReadReworkStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcReadyStartStatus", "Start Function", "Info")

        Try
            intReadNormalStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcReadyStartStatus", "intReadNormalStatus = " & intReadNormalStatus & " at DM" & My.Settings.ReadStatusAddress, "Info")
            intReadReworkStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcReadyStartStatus", "intReadReworkStatus = " & intReadReworkStatus & " at DM" & (My.Settings.ReadStatusAddress + 4), "Info")
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcReadyStartStatus", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        'If Not IsNewPlcMessage(intReadStatus) Then
        '    Return PLC_READ_STATUS_SAME_STATUS
        'End If

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcReadyStartStatus", "End Function", "Info")
        If intReadReworkStatus = 1 AndAlso intReadNormalStatus = 1 Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Private Sub ReplyZeroToPLC_END()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddress _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_objPlc.WriteMemoryWordString(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , "asdfghjkl;".Substring(0, 20).PadRight(20, " "))
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_END", "write = " & PLC_WRITE_STATUS_WAITING & " at DM" & My.Settings.WriteStatusAddress, "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_END", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_END", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While
        m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "End Function", "Info")
    End Sub

    Private Sub ReplyZeroToPLC_ReWork_Start()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddress + 4 _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_objPlc.WriteMemoryWordString(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , "asdfghjkl;".Substring(0, 20).PadRight(20, " "))
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_Start", "write = " & PLC_WRITE_STATUS_WAITING & " at DM" & (My.Settings.WriteStatusAddress + 4), "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_Start", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_Start", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While
        m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_Start", "End Function", "Info")
    End Sub

    Private Sub ReplyZeroToPLC_ReWork_End()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddressPCEnd + 4 _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_objPlc.WriteMemoryWordString(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , "asdfghjkl;".Substring(0, 20).PadRight(20, " "))
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_End", "write = " & PLC_WRITE_STATUS_WAITING & " at DM" & (My.Settings.WriteStatusAddressPCEnd + 4), "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_End", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_End", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While
        m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC_ReWork_End", "End Function", "Info")
    End Sub

    Private Sub ReplyZeroToPLC()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddressPCEnd _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_objPlc.WriteMemoryWordString(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , "asdfghjkl;".Substring(0, 20).PadRight(20, " "))
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "write = " & PLC_WRITE_STATUS_WAITING & " at DM" & My.Settings.WriteStatusAddressPCEnd, "Info")
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

    Private Sub ClearData()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordString(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.ReadDataLotData _
                                                , "".PadRight(20, " "))

                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.ReadDataLotQTY _
                                                , {PLC_WRITE_STATUS_WAITING} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
                blnSent = True
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

End Class
