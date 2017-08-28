Public Class clsPlcCommunication
    Inherits System.ComponentModel.BackgroundWorker

#Region "Attribute"
    Private m_blnIsConnected As Boolean

    Private m_clsCurrentState As clsStateBase
    Public m_clsStateWaiting As clsStateBase
    Public m_clsStateProcessing As clsStateBase
    Public m_clsStateComplete As clsStateBase
    Public m_clsStateNoData As clsStateBase
    Public m_clsStateError As clsStateBase

    Private m_objLogger As clsLogger
    Private m_intSleepTime As Integer

    Private m_strLotNo As String

    Private m_plcObject As clsPlcWrapper
    Private m_objLineSetting As clsLineSetting
    Private m_datNextSyncTime As DateTime
#End Region

#Region "Constructor"

    Public Sub New(ByVal objLine As clsLineSetting)

        m_objLineSetting = objLine

        m_strLotNo = String.Empty
        m_objLogger = New clsLogger(m_objLineSetting.LineName)
        m_plcObject = New clsPlcWrapper
        m_plcObject.InitializeAndConnect(m_objLineSetting.Net, m_objLineSetting.Node, m_objLineSetting.Unit)

        m_intSleepTime = m_objLineSetting.SleepInterval

        m_blnIsConnected = False

        Me.m_clsStateWaiting = New clsStateWaiting(Me)
        Me.m_clsStateProcessing = New clsStateProcessing(Me)
        Me.m_clsStateComplete = New clsStateComplete(Me)
        Me.m_clsStateError = New clsStateError(Me)

        Me.SetState(Me.m_clsStateWaiting)
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

    Public ReadOnly Property LineSetting As clsLineSetting
        Get
            Return m_objLineSetting
        End Get
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
            Threading.Thread.Sleep(m_objLineSetting.SleepInterval)
        End While
    End Sub

#End Region

#Region "Method"

    Public Sub SetState(ByRef state As clsStateBase)
        If Me.m_clsCurrentState Is Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State To :" & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = -1
        Else
            state.SetErrorMessage = Me.m_clsCurrentState.GetErrorMessage
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State From :" & m_clsCurrentState.GetType.Name & " To " & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = Me.m_clsCurrentState.PreviousPlcStatusCode
        End If

        state.WorkDone = False
        Me.m_clsCurrentState = state
    End Sub

    Public Sub ForceToProcessCurrentState()
        If Me.m_clsCurrentState IsNot Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "ForceToProcessCurrentState", "Current Process is forced to start", "Info")
            m_clsCurrentState.Process()
        End If
    End Sub

    Private Function GetNextSyncTime() As DateTime
        Dim strHour As String = m_objLineSetting.SyncTimeWhen.Split(":")(0)
        Dim strMinute As String = m_objLineSetting.SyncTimeWhen.Split(":")(1)
        Dim datTemp As DateTime = New DateTime(Now.Year, Now.Month, Now.Day, CInt(strHour), CInt(strMinute), 0)

        If DateTime.Compare(datTemp, Now) <= 0 Then
            datTemp = datTemp.AddDays(1)
        End If

        Return datTemp
    End Function

#End Region

End Class
