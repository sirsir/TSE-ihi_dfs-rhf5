Public MustInherit Class clsStateBase

#Region "Constant"
    
#End Region

#Region "Attribute"
    Private Shared m_dictLatestSendLogMsg As Dictionary(Of Integer, String)
    Private Shared m_dictLatestRecvLogMsg As Dictionary(Of Integer, String)

    Protected m_intPreviousStateCode As Integer
    Protected m_intCurrentStateCode As Integer
    Protected m_intPreviousPlcStatusCode As Integer
    Protected m_strErrorMessage As String

    Protected m_intLineNo As String
    Protected m_strLineName As String
    Protected m_blnWorkDone As Boolean

    Protected m_clsLogger As clsLogger
    Protected m_objPlc As clsPlcWrapper
    Protected m_objLineSetting As clsLineSetting
    Protected m_objPlcLineThread As clsPlcCommunication

#End Region

#Region "Constructor"
    'TODO create new constructor
    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        Me.m_clsLogger = plcLineThread.Logger
        Me.m_objPlc = plcLineThread.PlcObject
        Me.m_objPlcLineThread = plcLineThread
        Me.m_objLineSetting = plcLineThread.LineSetting
        Me.m_intLineNo = plcLineThread.LineSetting.LineNo
        Me.m_strLineName = plcLineThread.LineSetting.LineName
        Me.m_blnWorkDone = False
    End Sub

#End Region

#Region "Property"

    Public ReadOnly Property LineNo() As Integer
        Get
            Return m_intLineNo
        End Get
    End Property

    Public ReadOnly Property LineName() As String
        Get
            Return m_strLineName
        End Get
    End Property

    Public ReadOnly Property GetErrorMessage() As String
        Get
            Return m_strErrorMessage
        End Get
    End Property

    Public WriteOnly Property SetErrorMessage()
        Set(ByVal value)
            m_strErrorMessage = value
        End Set
    End Property

    Public Property WorkDone() As Boolean
        Get
            Return m_blnWorkDone
        End Get
        Set(ByVal value As Boolean)
            m_blnWorkDone = value
        End Set
    End Property

    Public ReadOnly Property CurrentStateCode() As Integer
        Get
            Return m_intCurrentStateCode
        End Get
    End Property

    Public Property PreviousPlcStatusCode() As Integer
        Get
            Return m_intPreviousPlcStatusCode
        End Get
        Set(ByVal value As Integer)
            m_intPreviousPlcStatusCode = value
        End Set
    End Property

#End Region

#Region "Method"

    Public MustOverride Sub Process()

    Public MustOverride Sub Initial(Optional ByVal inObj As Object = Nothing)

    Public Function IsPlcConnect() As Boolean
        Try
            'Read for test plc connection
            Dim intTemp As Integer = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadDataMemoryType _
                                                                    , m_objLineSetting.ReadStatusAddress _
                                                                    , m_objLineSetting.ReadStatusLength _
                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
            Return True
        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
            m_strErrorMessage = "PLC Time Out"
            Return False
        Catch ex As Exception
            m_strErrorMessage = ex.Message
            Return False
        End Try
    End Function

    Public Function SendKeepAlive() As Boolean
        Try
            m_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteLifeMemoryType _
                                            , m_objLineSetting.WriteLifeAddress _
                                            , {1} _
                                            , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
            Return True
        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
            m_strErrorMessage = "PLC Time Out"
            Return False
        Catch ex As Exception
            m_strErrorMessage = ex.Message
            Return False
        End Try
    End Function

    Public Function SendSyncTime() As Boolean
        Try
            'Dim datSync As DateTime = Now
            'Dim aintSync(2) As Integer

            'aintSync(0) = CInt(datSync.ToString("yyMM"))
            'aintSync(1) = CInt(datSync.ToString("ddHH"))
            'aintSync(2) = CInt(datSync.ToString("mmss"))

            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteSyncMemoryType _
            '                               , m_objLineSetting.WriteSyncAddress _
            '                                , aintSync _
            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

            Dim astrSyncFormat(2) As String
            astrSyncFormat(0) = "mmss"
            astrSyncFormat(1) = "ddHH"
            astrSyncFormat(2) = "yyMM"
            m_objPlc.WriteMemoryWordSyncDateTime(m_objLineSetting.WriteSyncMemoryType _
                                           , m_objLineSetting.WriteSyncAddress _
                                            , astrSyncFormat _
                                            , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

            m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time: ", "Info")
            Return True
        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
            m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time Failed", "Error")
            Return False
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time Failed", "Error")
            Return False
        End Try
    End Function

    Protected Function IsNewPlcMessage(ByVal readPlcStatus As Integer) As Boolean
        If readPlcStatus <> Me.m_intPreviousPlcStatusCode Then
            Me.m_intPreviousPlcStatusCode = readPlcStatus
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
