Public Class clsPlcCommunication
    Inherits System.ComponentModel.BackgroundWorker

#Region "Attribute"
    Private m_blnIsConnected As Boolean

    Private m_clsCurrentState As clsStateBase
    Public m_clsStateWaiting As clsStateBase
    Public m_clsStateProcessing As clsStateBase
    Public m_clsStateComplete As clsStateBase
    Public m_clsStateNoData As clsStateBase
    'Public m_clsStateError As clsStateBase
    Public m_clsStateWaitEnd As clsStateBase
    Public m_clsStateCompleteEnd As clsStateBase
    'Public m_clsStateErrorEnd As clsStateBase

    Private m_objLogger As clsLogger
    Private m_intSleepTime As Integer

    Private m_strLotNo As String

    Private m_plcObject As clsPlcWrapper
    'Private m_objLineSetting As clsLineSetting
    Private m_datNextSyncTime As DateTime

    Private m_blnStarted As Boolean
    Private m_blnStarted_Normal As Boolean
    Private m_objLotData As clsLotData

    Private m_intCurrentQTY As Integer

#End Region

    Public Class clsLotData
        Public LotNo As String
        Public LotQty As Integer
    End Class

#Region "Constructor"

    Public Sub New()

        'm_objLineSetting = objLine

        m_strLotNo = String.Empty
        'm_objLogger = New clsLogger(m_objLineSetting.LineName)
        m_objLogger = New clsLogger()
        m_plcObject = New clsPlcWrapper
        m_plcObject.InitializeAndConnect(My.Settings.PLC_Net, My.Settings.PLC_Node, My.Settings.PLC_Unit)

        m_intSleepTime = My.Settings.SleepInterval

        m_blnIsConnected = False

        Me.m_clsStateWaiting = New clsStateWaiting(Me)
        Me.m_clsStateProcessing = New clsStateProcessing(Me)
        Me.m_clsStateComplete = New clsStateComplete(Me)
        'Me.m_clsStateError = New clsStateError(Me)
        Me.m_clsStateWaitEnd = New clsStateWaitEnd(Me)
        Me.m_clsStateCompleteEnd = New clsStateCompleteEnd(Me)
        'Me.m_clsStateErrorEnd = New clsStateErrorEnd(Me)

        'Me.LotData = New clsLotData
        'Me.LotData.LotNo = ""
        'Me.LotData.LotQty = 0
        'Me.SetState(Me.m_clsStateWaiting, False)
    End Sub

#End Region

#Region "Property"

    Public ReadOnly Property PlcObject As clsPlcWrapper
        Get
            Return m_plcObject
        End Get
    End Property

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return m_blnIsConnected
        End Get
    End Property

    Public Property LotNo As String
        Get
            Return m_strLotNo
        End Get
        Set(ByVal value As String)
            If value.Trim.Length > 0 Then
                m_strLotNo = value
            End If
        End Set
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return m_clsCurrentState.GetErrorMessage
        End Get
    End Property

    Public ReadOnly Property LineName As String
        Get
            Return m_clsCurrentState.LineName
        End Get
    End Property

    Public ReadOnly Property LineNo As Integer
        Get
            Return m_clsCurrentState.LineNo
        End Get
    End Property

    Public ReadOnly Property Logger As clsLogger
        Get
            Return m_objLogger
        End Get
    End Property

    Public Property Started As Boolean
        Get
            Return m_blnStarted
        End Get
        Set(value As Boolean)
            m_blnStarted = value
        End Set
    End Property

    Public Property Started_Normal As Boolean
        Get
            Return m_blnStarted_Normal
        End Get
        Set(value As Boolean)
            m_blnStarted_Normal = value
        End Set
    End Property

    Public Property LotData As clsLotData
        Get
            Return m_objLotData
        End Get
        Set(value As clsLotData)
            m_objLotData = value
        End Set
    End Property

#End Region

#Region "Event"

    Private Sub clsPlcCommunication_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Me.DoWork

        My.Application.ChangeCulture("en-US")
        My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
        My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

        Dim dat As New Date()
        Dim intThreadCount As Integer = 0
        Dim intTimeOutCount As Integer = 0

        m_datNextSyncTime = GetNextSyncTime()

        While Not CheckCurrentState()
        End While

        While Not CancellationPending
            Try
                intThreadCount += 1

                If m_clsCurrentState.IsPlcConnect() Then
                    m_blnIsConnected = True

                    If m_clsCurrentState.SendKeepAlive() Then
                        Me.ReportProgress(100)
                        If Now >= m_datNextSyncTime Then
                            If m_clsCurrentState.SendSyncTime() Then
                                m_datNextSyncTime = Me.GetNextSyncTime
                            End If
                        End If
                        m_clsCurrentState.Process()
                    Else
                        m_blnIsConnected = False
                        m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "Send KPAL Failed", "Error")
                        Me.ReportProgress(0)
                    End If
                Else
                    intTimeOutCount += 1
                    m_blnIsConnected = False
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "PLC Connect Failed", "Error")
                    Me.ReportProgress(0)
                End If
            Catch ex As Exception
                Me.m_blnIsConnected = False
                Me.m_clsCurrentState.SetErrorMessage = "Process is terminated Because exception happen"
                Me.ReportProgress(0)
                m_objLogger.AppendLog(Me.GetType.Name, "DoWork", ex)
            Finally
                If intThreadCount > 1000 Then
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "Thread Still Alive", "Info")
                    intThreadCount = 0
                End If

                If intTimeOutCount > 100 Then
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "PLC Timeout Exceeds 30 Times", "Error")
                    intTimeOutCount = 0
                End If
            End Try
            Threading.Thread.Sleep(My.Settings.SleepInterval)
        End While
    End Sub

#End Region

#Region "Method"

    'Public Sub SetState(ByRef state As clsStateBase)
    '    If Me.m_clsCurrentState Is Nothing Then
    '        m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State To :" & state.GetType.Name, "Info")
    '        state.PreviousPlcStatusCode = -1
    '    Else
    '        state.SetErrorMessage = Me.m_clsCurrentState.GetErrorMessage
    '        m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State From :" & m_clsCurrentState.GetType.Name & " To " & state.GetType.Name, "Info")
    '        state.PreviousPlcStatusCode = Me.m_clsCurrentState.PreviousPlcStatusCode
    '    End If

    '    state.WorkDone = False
    '    Me.m_clsCurrentState = state
    'End Sub

    Public Sub SetState(ByRef state As clsStateBase, Optional blnWorkDone As Boolean = False)
        If Me.m_clsCurrentState Is Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State To :" & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = -1
        Else
            state.SetErrorMessage = Me.m_clsCurrentState.GetErrorMessage
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State From :" & m_clsCurrentState.GetType.Name & " To " & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = Me.m_clsCurrentState.PreviousPlcStatusCode
        End If

        state.WorkDone = blnWorkDone
        Me.m_clsCurrentState = state
    End Sub

    Public Sub ForceToProcessCurrentState()
        If Me.m_clsCurrentState IsNot Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "ForceToProcessCurrentState", "Current Process is forced to start", "Info")
            m_clsCurrentState.Process()
        End If
    End Sub

    Private Function GetNextSyncTime() As DateTime
        Dim strHour As String = My.Settings.SyncTimeWhen.Split(":")(0)
        Dim strMinute As String = My.Settings.SyncTimeWhen.Split(":")(1)
        Dim datTemp As DateTime = New DateTime(Now.Year, Now.Month, Now.Day, CInt(strHour), CInt(strMinute), 0)

        If DateTime.Compare(datTemp, Now) <= 0 Then
            datTemp = datTemp.AddDays(1)
        End If

        Return datTemp
    End Function

    Private Function CheckCurrentState() As Boolean
        Try
            'TODO: Check report progress
            Dim PlcNormalStart = Me.ReadPlcNormalStartStatus()
            m_objLogger.AppendLog(String.Format("PlcNormalStart = {0}", PlcNormalStart),"Info")
            Dim PlcNormalEnd = Me.ReadPlcNormalEndStatus()
            m_objLogger.AppendLog(String.Format("PlcNormalEnd = {0}", PlcNormalEnd), "Info")
            Dim PcNormalStart = Me.ReadPcNormalStartStatus()
            m_objLogger.AppendLog(String.Format("PcNormalStart = {0}", PcNormalStart), "Info")
            Dim PcNormalEnd = Me.ReadPcNormalEndStatus()
            m_objLogger.AppendLog(String.Format("PcNormalEnd = {0}", PcNormalEnd), "Info")

            Dim PlcReworkStart = Me.ReadPlcReworkStartStatus()
            m_objLogger.AppendLog(String.Format("PlcReworkStart = {0}", PlcReworkStart), "Info")
            Dim PlcReworkEnd = Me.ReadPlcReworkEndStatus()
            m_objLogger.AppendLog(String.Format("PlcReworkEnd = {0}", PlcReworkEnd), "Info")
            Dim PcReworkStart = Me.ReadPcReworkStartStatus()
            m_objLogger.AppendLog(String.Format("PcReworkStart = {0}", PcReworkStart), "Info")
            Dim PcReworkEnd = Me.ReadPcReworkEndStatus()
            m_objLogger.AppendLog(String.Format("PcReworkEnd = {0}", PcReworkEnd), "Info")

            Dim txtNormalCase As String = PlcNormalStart & "," & PlcNormalEnd & "," & _
                                            PcNormalStart & "," & PcNormalEnd
            Dim txtReworkCase As String = PlcReworkStart & "," & PlcReworkEnd & "," & _
                                            PcReworkStart & "," & PcReworkEnd

            Me.LotData.LotNo = Me.ReadPlcLotData()
            m_objLogger.AppendLog(String.Format("LotNo = {0}", Me.LotData.LotNo), "Info")
            Me.LotData.LotQty = Me.ReadPlcLotQTY()
            m_objLogger.AppendLog(String.Format("LotQty = {0}", Me.LotData.LotQty), "Info")

            If (txtNormalCase = "2,2,2,2") _
                Or (txtReworkCase = "2,2,2,2") _
                Or (txtNormalCase = "2,2,0,0") _
                Or (txtReworkCase = "2,2,0,0") _
                Or ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "0,0,0,0")) Then
                '0,0,0,0 - 0,0,0,0
                '2,2,2,2 - 2,2,2,2
                '2,2,0,0 - 2,2,0,0
                Me.SetState(Me.m_clsStateWaiting, False)
                Return True

            ElseIf ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "0,0,0,0")) Or _
                ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "1,0,0,0")) Or
                ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "1,0,0,0")) Then
                '1,0,0,0 - 1,0,0,0
                '1,0,0,0 - 0,0,0,0
                '0,0,0,0 - 1,0,0,0
                'If (Me.LotData.LotNo <> "") And (Me.LotData.LotQty <> "") Then 'Case  send data to PLC
                '    If (txtNormalCase = "1,0,0,0") Then ' Normal case
                '        Me.Started = True
                '        Me.Started_Normal = True
                '    Else
                '        Me.Started = True
                '        Me.Started_Normal = False
                '    End If
                'Else 'Case cannot send data to PLC
                '    If (txtReworkCase = "1,0,0,0") Then ' Rework case
                '        Me.Started = False
                '        Me.Started_Normal = False
                '    Else
                '        Me.Started = False
                '        Me.Started_Normal = True
                '    End If
                'End If
                Me.Started = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, False)
                Me.SetState(Me.m_clsStateProcessing, False)
                Return True

            ElseIf ((txtNormalCase = "1,0,1,0") And (txtReworkCase = "0,0,0,0")) _
                Or ((txtNormalCase = "1,0,1,0") And (txtReworkCase = "1,0,0,0")) Then
                '1,0,1,0 - 1,0,1,0
                Me.Started = True
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_NORMAL, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateProcessing, True)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "1,0,1,0")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "1,0,1,0")) Then
                '1,0,1,0 - 1,0,1,0
                Me.Started = True
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_REWORK, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateProcessing, True)
                Return True

            ElseIf ((txtNormalCase = "2,0,1,0") And (txtReworkCase = "0,0,0,0")) _
                 Or ((txtNormalCase = "2,0,1,0") And (txtReworkCase = "1,0,0,0")) Then
                '2,0,1,0 - 2,0,1,0
                Me.Started = True
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_NORMAL, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateComplete, False)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "2,0,1,0")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "2,0,1,0")) Then
                '2,0,1,0 - 2,0,1,0
                Me.Started = True
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_REWORK, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateComplete, False)
                Return True

            ElseIf ((txtNormalCase = "2,0,2,0") And (txtReworkCase = "0,0,0,0")) _
                Or ((txtNormalCase = "2,0,2,0") And (txtReworkCase = "1,0,0,0")) Then
                '2,0,2,0 - 2,0,2,0
                Me.Started = True
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_NORMAL, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateWaitEnd, False)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "2,0,2,0")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "2,0,2,0")) Then
                '2,0,2,0 - 2,0,2,0
                Me.Started = True
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_REWORK, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateWaitEnd, False)
                Return True

            ElseIf ((txtNormalCase = "2,0,2,1") And (txtReworkCase = "0,0,0,0")) _
                Or ((txtNormalCase = "2,0,2,1") And (txtReworkCase = "1,0,0,0")) Then
                '2,0,2,1 - 2,0,2,1
                Me.Started = True
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_NORMAL, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateWaitEnd, True)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "2,0,2,1")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "2,0,2,1")) Then
                '2,0,2,1 - 2,0,2,1
                Me.Started = True
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                Me.ReportProgress(REPORT_PROGRESS_LOT_START_REWORK, Me.ReadPlcCurrentQTY())
                Me.SetState(Me.m_clsStateWaitEnd, True)
                Return True

            ElseIf ((txtNormalCase = "2,1,2,1") And (txtReworkCase = "0,0,0,0")) _
                Or ((txtNormalCase = "2,1,2,1") And (txtReworkCase = "1,0,0,0")) Then
                '2,1,2,1 - 2,1,2,1
                Me.Started = False
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                'Me.ReportProgress(20, Me.ReadPlcCurrentQTY())
                Me.ReportProgress(REPORT_PROGRESS_CALL_END_LOT)
                Me.SetState(Me.m_clsStateCompleteEnd, False)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "2,1,2,1")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "2,1,2,1")) Then
                '2,1,2,1 - 2,1,2,1
                Me.Started = False
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                'Me.ReportProgress(30, Me.ReadPlcCurrentQTY())
                Me.ReportProgress(REPORT_PROGRESS_CALL_END_LOT)
                Me.SetState(Me.m_clsStateCompleteEnd, False)
                Return True

            ElseIf ((txtNormalCase = "2,1,2,2") And (txtReworkCase = "0,0,0,0")) _
                Or ((txtNormalCase = "2,1,2,2") And (txtReworkCase = "1,0,0,0")) Then
                '2,1,2,2 - 2,1,2,2
                Me.Started = False
                Me.Started_Normal = True
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                'Me.ReportProgress(20, Me.ReadPlcCurrentQTY())
                Me.ReportProgress(REPORT_PROGRESS_CALL_END_LOT)
                Me.SetState(Me.m_clsStateCompleteEnd, True)
                Return True

            ElseIf ((txtNormalCase = "0,0,0,0") And (txtReworkCase = "2,1,2,2")) _
                Or ((txtNormalCase = "1,0,0,0") And (txtReworkCase = "2,1,2,2")) Then
                '2,1,2,2 - 2,1,2,2
                Me.Started = False
                Me.Started_Normal = False
                Me.ReportProgress(REPORT_PROGRESS_READY_START_LOT, True)
                'Me.ReportProgress(30, Me.ReadPlcCurrentQTY())
                Me.ReportProgress(REPORT_PROGRESS_CALL_END_LOT)
                Me.SetState(Me.m_clsStateCompleteEnd, True)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "CheckCurrentState", ex)
            Return False
        End Try
    End Function

    Private Function ReadPlcNormalStartStatus() As Integer '5020
        Dim intNormalStartStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPlcNormalStartStatus", "Start Normal Function", "Info")
        intNormalStartStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalStartStatus

    End Function

    Private Function ReadPlcNormalEndStatus() As Integer '5022
        Dim intNormalEndStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPlcNormalEndStatus", "End Normal Function", "Info")
        intNormalEndStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddressEnd _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalEndStatus

    End Function
    Private Function ReadPcNormalStartStatus() As Integer '5021
        Dim intNormalStartStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPcNormalStartStatus", "Start Normal Function", "Info")
        intNormalStartStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.WriteStatusAddress _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalStartStatus

    End Function
    Private Function ReadPcNormalEndStatus() As Integer '5023
        Dim intNormalEndStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPcNormalEndStatus", "End Normal Function", "Info")
        intNormalEndStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.WriteStatusAddressPCEnd _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalEndStatus

    End Function


    Private Function ReadPlcReworkStartStatus() As Integer '5024
        Dim intNormalStartStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPlcReworkStartStatus", "Rework Start Normal Function", "Info")
        intNormalStartStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddress + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalStartStatus

    End Function
    Private Function ReadPlcReworkEndStatus() As Integer '5026
        Dim intNormalEndStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPlcReworkEndStatus", "Rework End Normal Function", "Info")
        intNormalEndStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadStatusAddressEnd + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalEndStatus

    End Function
    Private Function ReadPcReworkStartStatus() As Integer '5025
        Dim intNormalStartStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPcReworkStartStatus", "Rework Start Normal Function", "Info")
        intNormalStartStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.WriteStatusAddress + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalStartStatus

    End Function
    Private Function ReadPcReworkEndStatus() As Integer '5027
        Dim intNormalEndStatus As Integer
        'm_objLogger.AppendLog(Me.GetType.Name, "ReadPcReworkEndStatus", "Rework End Normal Function", "Info")
        intNormalEndStatus = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.WriteStatusAddressPCEnd + 4 _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
        Return intNormalEndStatus

    End Function

    Private Function ReadPlcCurrentQTY() As Integer
        Dim intReadCurrentQTY As Integer
        m_objLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", "Current QTY Function", "Info")
        intReadCurrentQTY = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                       , My.Settings.ReadDataCurrentQTY _
                                                       , 1 _
                                                       , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

        Return intReadCurrentQTY
    End Function

    Private Function ReadPlcLotData() As String
        Dim strReadLotData As String
        m_objLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", "Current QTY Function", "Info")
        strReadLotData = m_plcObject.ReadMemoryString(My.Settings.ReadStatusMemoryType _
                                                       , My.Settings.ReadDataLotData _
                                                       , 20)

        Return strReadLotData
    End Function

    Private Function ReadPlcLotQTY() As Integer
        Dim intReadLotQTY As Integer
        m_objLogger.AppendLog(Me.GetType.Name, "ReadPlcCurrentQTY", "Current QTY Function", "Info")
        intReadLotQTY = m_plcObject.ReadMemoryWordInteger(My.Settings.ReadStatusMemoryType _
                                                           , My.Settings.ReadDataLotQTY _
                                                           , 1 _
                                                           , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

        Return intReadLotQTY
    End Function

#End Region

End Class
