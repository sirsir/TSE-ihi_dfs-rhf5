﻿Public Class clsStateComplete
    Inherits clsStateBase

    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        MyBase.New(plcLineThread)
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
            Me.Reply2ToPlc()
            Me.m_blnWorkDone = True
        Else
            'TODO check Process type
            If m_objPlcLineThread.Started_Normal Then
                m_intWorkType = 0
            Else
                m_intWorkType = 4
            End If
            m_clsLogger.AppendLog(Me.GetType.Name, "Process", "m_blnWorkDone = True", "Info")
            Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateWaitEnd)
            m_blnWorkDone = True
            'Dim intReadPlcStatus = Me.ReadPlcStatus()
            'If intReadPlcStatus = PLC_READ_STATUS_WAITING Then
            '    m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Read Waiting status", "Info")
            '    Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateWaitEnd)
            '    m_blnWorkDone = True
            'ElseIf intReadPlcStatus = PLC_READ_STATUS_SAME_STATUS Then
            '    'DO NOTHING
            'ElseIf intReadPlcStatus = PLC_READ_STATUS_REQUEST Then
            '    'DO NOTHING
            'ElseIf intReadPlcStatus = PLC_READ_STATUS_COMPLETE Then
            '    'DO NOTHING
            'ElseIf intReadPlcStatus = PLC_READ_STATUS_READ_FAILED Then
            '    'DO NOTHING
            'Else
            '    m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Other, Go to error state", "Error")
            '    Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateError)
            '    Me.m_objPlcLineThread.ForceToProcessCurrentState()
            'End If
        End If
    End Sub

    Private Sub Reply2ToPlc()
        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                m_objPlc.WriteMemoryWordInteger(My.Settings.WriteStatusMemoryType _
                                                , My.Settings.WriteStatusAddress + m_intWorkType _
                                                , {PLC_WRITE_STATUS_COMPLETE} _
                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
                blnSent = True
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", "intReadStatus = " & PLC_WRITE_STATUS_COMPLETE & " at DM" & (My.Settings.WriteStatusAddress + m_intWorkType), "Info")
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100))
        End While

        m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", "End Function", "Info")
    End Sub

    Private Function ReadPlcStatus() As Integer
        Dim intReadStatus As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

        Try
            intReadStatus = m_objPlc.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress + m_intWorkType _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "intReadStatus = " & intReadStatus & " at DM" & (My.Settings.ReadStatusAddress + m_intWorkType), "Info")
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

End Class