Imports System.Xml

Public Class clsLineSetting

#Region "Attribute"
    Private m_intLineNo As Integer
    Private m_strLineName As String
    Private m_intNet As Integer
    Private m_intNode As Integer
    Private m_intUnit As Integer
    Private m_strSyncTimeWhen As String

    Private m_mtpReadStatusMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intReadStatusAddress As Integer
    Private m_intReadStatusLength As Integer

    Private m_mtpReadDataMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intReadDataAddress As Integer
    Private m_intReadDataAsciiLength As Integer
    Private m_intReadDataBcdLength As Integer

    Private m_mtpWriteStatusMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteStatusAddress As Integer

    Private m_mtpWriteLifeMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteLifeAddress As Integer

    Private m_mtpWriteSyncMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteSyncAddress As Integer

    Private m_intSleepInterval As Integer
    Private m_strWritePath As String
    Private m_strRootFolder As String
    Private m_strRootTempFolder As String
    Private m_strFormat As String
    Private m_aWriteField As List(Of clsField)
    Private m_intUseCsvMode As Integer
    Private m_intUseXlsMode As Integer

    Private m_objFieldSerial_BH As clsField
    Private m_objFieldSerial_TS As clsField
    Private m_objFieldSerial_CW As clsField
    Private m_objFieldSerial_ITA As clsField
    Private m_objFieldSerial_CS As clsField
    Private m_objFieldMode As clsField
    Private m_objFieldMc As clsField
    Private m_objFieldLotNo As clsField
    Private m_objFieldFileName As clsField
    Private m_objFieldDateTime As clsField
    Private m_objFieldStatus As clsField
    Private m_objFieldMachineColumnsID As clsField

    Private m_intCopyFile As Integer
    Private m_strCopyPath As String
    Private m_strCopyWildCard As String
    Private m_strCopyPurgeOldPath As String
    Private m_intCopyPeriodMilliSec As Integer
    Private m_intCopyPurgePeriodDay As Integer

    Private m_taMachine As ihi_rhf5TableAdapters.MACHINETableAdapter

    Protected m_clsLogger As clsLogger
#End Region

#Region "Properties"
    Public ReadOnly Property LineNo As Integer
        Get
            Return m_intLineNo
        End Get
    End Property

    Public ReadOnly Property LineName As String
        Get
            Return m_strLineName
        End Get
    End Property

    Public ReadOnly Property Net As Integer
        Get
            Return m_intNet
        End Get
    End Property

    Public ReadOnly Property Node As Integer
        Get
            Return m_intNode
        End Get
    End Property

    Public ReadOnly Property Unit As Integer
        Get
            Return m_intUnit
        End Get
    End Property

    Public ReadOnly Property SyncTimeWhen As String
        Get
            Return m_strSyncTimeWhen
        End Get
    End Property

    Public WriteOnly Property ReadStatusMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intReadStatusAddress) Then
                m_intReadStatusAddress = 5000
            End If
        End Set
    End Property

    Public ReadOnly Property ReadStatusMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpReadStatusMemory
        End Get
    End Property

    Public ReadOnly Property ReadStatusAddress As Integer
        Get
            Return m_intReadStatusAddress
        End Get
    End Property

    Public ReadOnly Property ReadStatusLength As Integer
        Get
            Return m_intReadStatusLength
        End Get
    End Property

    Public WriteOnly Property ReadDataMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intReadDataAddress) Then
                m_intReadDataAddress = 32700
            End If
        End Set
    End Property

    Public ReadOnly Property ReadDataMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpReadDataMemory
        End Get
    End Property

    Public ReadOnly Property ReadDataAddress As Integer
        Get
            Return m_intReadDataAddress
        End Get
    End Property

    Public ReadOnly Property ReadDataAsciiLength As Integer
        Get
            Return m_intReadDataAsciiLength
        End Get
    End Property

    Public ReadOnly Property ReadDataBcdLength As Integer
        Get
            Return m_intReadDataBcdLength
        End Get
    End Property

    Public ReadOnly Property WriteStatusMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteStatusMemory
        End Get
    End Property

    Public WriteOnly Property WriteStatusMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteStatusAddress) Then
                m_intWriteStatusAddress = 5001
            End If
        End Set
    End Property

    Public ReadOnly Property WriteStatusAddress As Integer
        Get
            Return m_intWriteStatusAddress
        End Get
    End Property

    Public ReadOnly Property WriteLifeMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteLifeMemory
        End Get
    End Property

    Public WriteOnly Property WriteLifeMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteLifeAddress) Then
                m_intWriteLifeAddress = 5002
            End If
        End Set
    End Property

    Public ReadOnly Property WriteLifeAddress() As UInteger
        Get
            Return m_intWriteLifeAddress
        End Get
    End Property

    Public ReadOnly Property WriteSyncMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteSyncMemory
        End Get
    End Property

    Public WriteOnly Property WriteSyncMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteSyncAddress) Then
                m_intWriteSyncAddress = 5010
            End If
        End Set
    End Property

    Public ReadOnly Property WriteSyncAddress() As UInteger
        Get
            Return m_intWritesyncAddress
        End Get
    End Property

    Public ReadOnly Property WritePath As String
        Get
            Return m_strWritePath
        End Get
    End Property

    Public ReadOnly Property SleepInterval As Integer
        Get
            Return m_intSleepInterval
        End Get
    End Property

    Public ReadOnly Property RootFolder As String
        Get
            Return m_strRootFolder
        End Get
    End Property

    Public ReadOnly Property RootTempFolder As String
        Get
            Return m_strRootTempFolder
        End Get
    End Property

    Public ReadOnly Property Format As String
        Get
            Return m_strFormat
        End Get
    End Property

    Public ReadOnly Property Fields As List(Of clsField)
        Get
            Return m_aWriteField
        End Get
    End Property

    Public ReadOnly Property UseCsvMode As Boolean
        Get
            Return m_intUseCsvMode = 1
        End Get
    End Property

    Public ReadOnly Property UseXlsMode As Boolean
        Get
            Return m_intUseXlsMode = 1
        End Get
    End Property

    Public ReadOnly Property FieldSerial_BH As clsField
        Get
            Return m_objFieldSerial_BH
        End Get
    End Property

    Public ReadOnly Property FieldSerial_TS As clsField
        Get
            Return m_objFieldSerial_TS
        End Get
    End Property

    Public ReadOnly Property FieldSerial_CW As clsField
        Get
            Return m_objFieldSerial_CW
        End Get
    End Property

    Public ReadOnly Property FieldSerial_ITA As clsField
        Get
            Return m_objFieldSerial_ITA
        End Get
    End Property

    Public ReadOnly Property FieldSerial_CS As clsField
        Get
            Return m_objFieldSerial_CS
        End Get
    End Property

    Public ReadOnly Property FieldMode As clsField
        Get
            Return m_objFieldMode
        End Get
    End Property

    Public ReadOnly Property FieldMc As clsField
        Get
            Return m_objFieldMc
        End Get
    End Property

    Public ReadOnly Property FieldLotNo As clsField
        Get
            Return m_objFieldLotNo
        End Get
    End Property

    Public ReadOnly Property FieldFileName As clsField
        Get
            Return m_objFieldFileName
        End Get
    End Property

    Public ReadOnly Property FieldDateTime As clsField
        Get
            Return m_objFieldDateTime
        End Get
    End Property

    Public ReadOnly Property FieldStatus As clsField
        Get
            Return m_objFieldStatus
        End Get
    End Property

    Public ReadOnly Property FieldMachineColumnsID As clsField
        Get
            Return m_objFieldMachineColumnsID
        End Get
    End Property

    Public ReadOnly Property DoCopyFile As Boolean
        Get
            Return m_intCopyFile = 1
        End Get
    End Property

    Public ReadOnly Property CopyPath As String
        Get
            Return m_strCopyPath
        End Get
    End Property

    Public ReadOnly Property CopyWildCard As String
        Get
            Return m_strCopyWildCard
        End Get
    End Property

    Public ReadOnly Property CopyPeriodMilliSec As Integer
        Get
            Return m_intCopyPeriodMilliSec
        End Get
    End Property

    Public ReadOnly Property CopyPurgePeriodDay As Integer
        Get
            Return m_intCopyPurgePeriodDay
        End Get
    End Property

    Public ReadOnly Property CopyPurgeOldPath As String
        Get
            Return m_strCopyPurgeOldPath
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        Me.Init()
    End Sub
#End Region

#Region "Method"
    Public Sub Init()
        m_intLineNo = -1
        m_strLineName = ""
        m_intNet = -1
        m_intNode = -1
        m_intUnit = -1
        m_intReadStatusAddress = -1
        m_intReadStatusLength = -1
        m_intReadDataAddress = -1
        m_intReadDataAsciiLength = -1
        m_intReadDataBcdLength = -1
        m_intWriteStatusAddress = -1
        m_intWriteLifeAddress = -1
        m_intWriteSyncAddress = -1
        m_intUseCsvMode = 1
        m_intUseXlsMode = 1

        m_intSleepInterval = -1
        m_strWritePath = ""
        m_strRootFolder = ""
        m_strRootTempFolder = ""
        m_strFormat = ""
        m_aWriteField = Nothing

        m_objFieldSerial_BH = Nothing
        m_objFieldSerial_TS = Nothing
        m_objFieldSerial_CW = Nothing
        m_objFieldSerial_ITA = Nothing
        m_objFieldSerial_CS = Nothing
        m_objFieldMode = Nothing
        m_objFieldMc = Nothing
        m_objFieldLotNo = Nothing
        m_objFieldFileName = Nothing
        m_objFieldDateTime = Nothing
        m_objFieldStatus = Nothing
        m_objFieldMachineColumnsID = Nothing

        m_intCopyFile = 0
        m_strCopyPath = ""
        m_strCopyWildCard = ""
        m_strCopyPurgeOldPath = ""
        m_intCopyPeriodMilliSec = 5000
        m_intCopyPurgePeriodDay = 2

        m_strSyncTimeWhen = ""
        m_clsLogger = New clsLogger

    End Sub

    Public Shared Function FindAll() As List(Of clsLineSetting)

        'Dim xmlFilePath As String = GetSettingPath("line")
        Dim xmlFilePath As String = GetSettingPath("lineDBExport")

        Dim objDoc As New XmlDocument
        Dim lstLine As New List(Of clsLineSetting)
        Dim strFormat As String = ""

        Dim objLogger = New clsLogger
        Dim blnExportXMLFile As Boolean

        Dim astrSCREEN_COLUMN_NAME() As String

        Try

            Dim m_taLineMaster As New ihi_rhf5TableAdapters.LINE_MASTERTableAdapter
            Dim dtLineMas As ihi_rhf5.LINE_MASTERDataTable = m_taLineMaster.GetDataBy_LINE_NAME(My.Settings.RunningLineName)

            Dim m_taMachine As New ihi_rhf5TableAdapters.MACHINETableAdapter
            Dim dtMachine As ihi_rhf5.MACHINEDataTable = Nothing

            Dim m_taMachineColumns As New ihi_rhf5TableAdapters.MACHINE_COLUMNSTableAdapter
            Dim dtMacCol As ihi_rhf5.MACHINE_COLUMNSDataTable = Nothing

            For i = 0 To dtLineMas.Count - 1

                dtMachine = m_taMachine.GetDataBy_LINE_ID(dtLineMas.Item(i).ID)
                For j = 0 To dtMachine.Count - 1
                    strFormat = ""
                    Dim line As New clsLineSetting
                    line.m_intNet = dtMachine.Item(j).PLC_NET '1
                    line.m_intNode = dtMachine.Item(j).PLC_NODE
                    line.m_intUnit = dtMachine.Item(j).PLC_UNIT '0
                    line.m_intSleepInterval = dtLineMas.Item(i).SLEEP_INTERVAL '500
                    line.m_strRootFolder = dtLineMas.Item(i).ROOT_PATH 'D:\OMRON_DATA_FILING\output\
                    line.m_strRootTempFolder = dtLineMas.Item(i).ROOT_TEMP 'D:\OMRON_DATA_FILING\temp
                    line.m_strSyncTimeWhen = dtLineMas.Item(i).SYNC_TIME_WHEN '00:00
                    line.m_intUseCsvMode = dtLineMas.Item(i).USE_CSV_MODE '0
                    If dtLineMas.Item(i).USE_XLS_MODE Then
                        line.m_intUseXlsMode = 1
                    Else
                        line.m_intUseXlsMode = 0
                    End If

                    line.m_intLineNo = dtMachine.Item(j).MACHINE_NO
                    line.m_strLineName = dtMachine.Item(j).MACHINE_NAME
                    line.ReadStatusMemory = dtMachine.Item(j).READ_STATUS_ADDRESS
                    line.m_intReadStatusLength = dtMachine.Item(j).READ_STATUS_LENGTH
                    line.ReadDataMemory = dtMachine.Item(j).READ_DATA_ADDRESS
                    line.m_intReadDataAsciiLength = dtLineMas.Item(i).READ_ASCII_LENGTH
                    line.m_intReadDataBcdLength = dtLineMas.Item(i).READ_BCD_LENGTH
                    line.WriteStatusMemory = dtMachine.Item(j).WRITE_STATUS_ADDRESS
                    line.WriteLifeMemory = dtMachine.Item(j).WRITE_LIFE_ADDRESS
                    line.WriteSyncMemory = dtMachine.Item(j).WRITE_SYNC_ADDRESS
                    line.m_strWritePath = dtMachine.Item(j).PATH


                    line.m_intCopyFile = dtMachine.Item(j).COPY_FILE
                    line.m_strCopyPath = dtMachine.Item(j).COPY_PATH
                    line.m_strCopyWildCard = dtMachine.Item(j).COPY_WILDCARD
                    line.m_strCopyPurgeOldPath = dtMachine.Item(j).COPY_PURGE_OLD_PATH
                    line.m_intCopyPeriodMilliSec = dtMachine.Item(j).COPY_PERIOD_MILLISEC
                    line.m_intCopyPurgePeriodDay = dtMachine.Item(j).COPY_PURGE_PERIOD_DAY


                    line.m_objFieldSerial_BH = New clsField(dtMachine.Item(j).FORMAT_SERIAL_BH.Trim & ",")
                    line.m_objFieldSerial_TS = New clsField(dtMachine.Item(j).FORMAT_SERIAL_TS.Trim & ",")
                    line.m_objFieldSerial_CW = New clsField(dtMachine.Item(j).FORMAT_SERIAL_CW.Trim & ",")
                    line.m_objFieldSerial_ITA = New clsField(dtMachine.Item(j).FORMAT_SERIAL_ITA.Trim & ",")
                    line.m_objFieldSerial_CS = New clsField(dtMachine.Item(j).FORMAT_SERIAL_CS.Trim & ",")
                    line.m_objFieldMode = New clsField(dtMachine.Item(j).FORMAT_MODE.Trim & ",")
                    line.m_objFieldMc = New clsField(dtMachine.Item(j).FORMAT_MC.Trim & ",")
                    line.m_objFieldLotNo = New clsField(dtMachine.Item(j).FORMAT_LOT_NO.Trim & ",")
                    line.m_objFieldFileName = New clsField(dtMachine.Item(j).FORMAT_FILE_NAME.Trim & ",")
                    line.m_objFieldDateTime = New clsField(dtMachine.Item(j).FORMAT_DATE_TIME.Trim & ",")
                    line.m_objFieldStatus = New clsField(dtMachine.Item(j).FORMAT_STATUS.Trim & ",")

                    dtMacCol = m_taMachineColumns.GetDataBy_MACHINE_ID(dtMachine.Item(j).ID)
                    For x = 0 To dtMacCol.Count - 1
                        Dim strMultiplier As String = ""
                        If Not dtMacCol.Item(x).IsMULTIPLIERNull Then
                            strMultiplier = dtMacCol.Item(x).MULTIPLIER.ToString
                        End If

                        Dim strFormatString As String = ""
                        If Not dtMacCol.Item(x).IsFORMAT_STRINGNull Then
                            strFormatString = dtMacCol.Item(x).FORMAT_STRING
                        End If

                        astrSCREEN_COLUMN_NAME = dtMacCol.Item(x).SCREEN_COLUMN_NAME.Split({"|"}, StringSplitOptions.None)
                        strFormat += astrSCREEN_COLUMN_NAME(astrSCREEN_COLUMN_NAME.Count - 1) & "," & _
                                            dtMacCol.Item(x).DATA_TYPE & "," & _
                                            dtMacCol.Item(x).POSITION & "," & _
                                            dtMacCol.Item(x).LENGTH & "," & _
                                            strMultiplier & "," & _
                                            strFormatString & "," & _
                                            dtMacCol.Item(x).REPLACE_COLUMN_NAME & "," & dtMacCol.Item(x).ID.ToString.Trim & "|"

                        'add MachineColumnsID for ExportTextFileForImport()
                        line.m_objFieldMachineColumnsID = New clsField("MachineColumnsID,,0,0,0,,," & dtMacCol.Item(x).ID.ToString.Trim)

                    Next
                    strFormat = strFormat.Substring(0, strFormat.Length - 1)
                    line.m_strFormat = strFormat
                    line.m_aWriteField = clsField.GetFieldList(line.m_strFormat)
                    lstLine.Add(line)

                Next ' End j = 0 To dtMachine.Count - 1

            Next ' End For i = 0 To dtLineMas.Count - 1

            'ExportXMLFile()
            blnExportXMLFile = ExportXMLFile(xmlFilePath)

            Return lstLine
        Catch ex As Exception
            objLogger.AppendLog("clsLineSetting", "cannot connect DB.", ex)
            'Return lstLine
        End Try

        'Offline Mode
        Try

            objDoc.Load(xmlFilePath)
            Dim nodeCommonSetting As XmlNodeList = objDoc.GetElementsByTagName("common")
            If nodeCommonSetting.Count <> 1 Then
                Throw New Exception("Invalid common setting in line.xml format")
            End If

            Dim intSleepInterval As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("sleep_interval").Value
            Dim strRootFolder As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("root").Value
            Dim strRootTempFolder As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("root_temp").Value
            Dim intReadDataAsciiLength As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("readdataasciilength").Value
            Dim intReadDataBcdLength As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("readdatabcdlength").Value
            Dim strSyncTimeWhen As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("synctimewhen").Value
            Dim intUseCsvMode As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("usecsvmode").Value
            Dim intUseXlsMode As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("usexlsmode").Value

            Dim nodeLineSetting As XmlNodeList = objDoc.GetElementsByTagName("line")
            For i = 0 To nodeLineSetting.Count - 1

                Dim line As New clsLineSetting
                line.m_intNet = nodeLineSetting.Item(i).Attributes.GetNamedItem("net").Value
                line.m_intNode = nodeLineSetting.Item(i).Attributes.GetNamedItem("node").Value
                line.m_intUnit = nodeLineSetting.Item(i).Attributes.GetNamedItem("unit").Value
                line.m_intSleepInterval = intSleepInterval '500
                line.m_strRootFolder = strRootFolder 'D:\OMRON_DATA_FILING\output\
                line.m_strRootTempFolder = strRootTempFolder 'D:\OMRON_DATA_FILING\temp
                line.m_strSyncTimeWhen = strSyncTimeWhen '00:00
                line.m_intUseCsvMode = intUseCsvMode '0
                line.m_intUseXlsMode = intUseXlsMode '1


                line.m_intLineNo = nodeLineSetting.Item(i).Attributes.GetNamedItem("no").Value
                line.m_strLineName = nodeLineSetting.Item(i).Attributes.GetNamedItem("name").Value
                line.ReadStatusMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("readstatusaddress").Value
                line.m_intReadStatusLength = nodeLineSetting.Item(i).Attributes.GetNamedItem("readstatuslength").Value
                line.ReadDataMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("readdataaddress").Value
                line.m_intReadDataAsciiLength = intReadDataAsciiLength
                line.m_intReadDataBcdLength = intReadDataBcdLength
                line.WriteStatusMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writestatusaddress").Value
                line.WriteLifeMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writelifeaddress").Value
                line.WriteSyncMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writesyncaddress").Value
                line.m_strWritePath = nodeLineSetting.Item(i).Attributes.GetNamedItem("path").Value

                line.m_intCopyFile = nodeLineSetting.Item(i).Attributes.GetNamedItem("copyfile").Value
                line.m_strCopyPath = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypath").Value
                line.m_strCopyWildCard = nodeLineSetting.Item(i).Attributes.GetNamedItem("copywildcard").Value
                line.m_strCopyPurgeOldPath = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypurgeoldpath").Value
                line.m_intCopyPeriodMilliSec = nodeLineSetting.Item(i).Attributes.GetNamedItem("copyperiodmillisec").Value
                line.m_intCopyPurgePeriodDay = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypurgeperiodday").Value

                'line.m_objFieldSerial = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial").Value.Trim & ",")
                line.m_objFieldSerial_BH = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial_BH").Value.Trim & ",")
                line.m_objFieldSerial_TS = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial_TS").Value.Trim & ",")
                line.m_objFieldSerial_CW = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial_CW").Value.Trim & ",")
                line.m_objFieldSerial_ITA = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial_ITA").Value.Trim & ",")
                line.m_objFieldSerial_CS = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial_CS").Value.Trim & ",")
                line.m_objFieldMode = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("mode").Value.Trim & ",")
                line.m_objFieldMc = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("mc").Value.Trim & ",")
                line.m_objFieldLotNo = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("lotno").Value.Trim & ",")
                line.m_objFieldFileName = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("filename").Value.Trim & ",")
                line.m_objFieldDateTime = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("datetime").Value.Trim & ",")
                line.m_objFieldStatus = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("status").Value.Trim & ",")

                line.m_strFormat = nodeLineSetting.Item(i).Attributes.GetNamedItem("format").Value
                line.m_aWriteField = clsField.GetFieldList(line.m_strFormat)
                lstLine.Add(line)
            Next

            Return lstLine
        Catch ex As Exception
            objLogger.AppendLog("clsLineSetting", "cannot read XML file.", ex)
            Return lstLine
        End Try
    End Function

    Private Shared Function GetSettingPath(ByVal settingName As String) As String
        Dim strFileName As String = settingName & ".xml"
        Dim strProgramDataFileName As String = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & strFileName
        Dim strAppPathFileName As String = My.Application.Info.DirectoryPath & "\" & strFileName
        If My.Computer.FileSystem.FileExists(strProgramDataFileName) Then
            Return strProgramDataFileName
        Else
            Try
                My.Computer.FileSystem.CopyFile(strAppPathFileName, strProgramDataFileName, True)
                Return strProgramDataFileName
            Catch ex As Exception
                Return strAppPathFileName
            End Try
        End If
    End Function

    Private Shared Function ExportXMLFile(ByVal xmlFilePath As String) As Boolean
        Dim objLogger = New clsLogger
        Try

            If System.IO.File.Exists(xmlFilePath) = True Then

                System.IO.File.Delete(xmlFilePath)

            End If

            Dim Doc As New XmlDocument()
            Dim newAtt As XmlAttribute
            Dim strFormat As String = ""

            ' Use the XmlDeclaration class to place the
            ' <?xml version="1.0"?> declaration at the top of our XML file
            Dim dec As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", "utf-8", "")
            Doc.AppendChild(dec)
            Dim elementSetting As XmlElement = Doc.CreateElement("settings")
            Doc.AppendChild(elementSetting)

            'Generate common Element
            Dim m_taLineMaster As New ihi_rhf5TableAdapters.LINE_MASTERTableAdapter
            Dim dtLineMas As ihi_rhf5.LINE_MASTERDataTable = m_taLineMaster.GetData
            Dim nodeCommon As XmlNode = Doc.CreateElement("common")

            If dtLineMas.Count > 0 Then
                For i = 0 To dtLineMas.Count - 1
                    newAtt = Doc.CreateAttribute("root")
                    newAtt.Value = dtLineMas.Item(i).ROOT_PATH
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("root_temp")
                    newAtt.Value = dtLineMas.Item(i).ROOT_TEMP
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("sleep_interval")
                    newAtt.Value = dtLineMas.Item(i).SLEEP_INTERVAL
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("readdataasciilength")
                    newAtt.Value = dtLineMas.Item(i).READ_ASCII_LENGTH
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("readdatabcdlength")
                    newAtt.Value = dtLineMas.Item(i).READ_BCD_LENGTH
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("synctimewhen")
                    newAtt.Value = dtLineMas.Item(i).SYNC_TIME_WHEN
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("usecsvmode")
                    newAtt.Value = Convert.ToInt32(dtLineMas.Item(i).USE_CSV_MODE).ToString
                    nodeCommon.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("usexlsmode")
                    newAtt.Value = Convert.ToInt32(dtLineMas.Item(i).USE_XLS_MODE).ToString
                    nodeCommon.Attributes.Append(newAtt)

                    elementSetting.AppendChild(nodeCommon)
                Next
            End If

            'Generate line Element
            Dim m_taMachine As New ihi_rhf5TableAdapters.MACHINETableAdapter
            Dim dtMachine As ihi_rhf5.MACHINEDataTable = m_taMachine.GetData
            Dim m_taMachine_Col As New ihi_rhf5TableAdapters.MACHINE_COLUMNSTableAdapter
            Dim dtMachine_Col As ihi_rhf5.MACHINE_COLUMNSDataTable = Nothing
            Dim astrSCREEN_COLUMN_NAME() As String

            If dtMachine.Count > 0 Then
                For x = 0 To dtMachine.Count - 1

                    Dim nodeLine As XmlNode = Doc.CreateElement("line")

                    newAtt = Doc.CreateAttribute("no")
                    newAtt.Value = dtMachine.Item(x).MACHINE_NO
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("name")
                    newAtt.Value = dtMachine.Item(x).MACHINE_NAME
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("path")
                    newAtt.Value = dtMachine.Item(x).PATH
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("net")
                    newAtt.Value = dtMachine.Item(x).PLC_NET
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("node")
                    newAtt.Value = dtMachine.Item(x).PLC_NODE
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("unit")
                    newAtt.Value = dtMachine.Item(x).PLC_UNIT
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("readstatusaddress")
                    newAtt.Value = dtMachine.Item(x).READ_STATUS_ADDRESS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("readstatuslength")
                    newAtt.Value = dtMachine.Item(x).READ_STATUS_LENGTH
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("readdataaddress")
                    newAtt.Value = dtMachine.Item(x).READ_DATA_ADDRESS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("writestatusaddress")
                    newAtt.Value = dtMachine.Item(x).WRITE_STATUS_ADDRESS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("writelifeaddress")
                    newAtt.Value = dtMachine.Item(x).WRITE_LIFE_ADDRESS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("writesyncaddress")
                    newAtt.Value = dtMachine.Item(x).WRITE_SYNC_ADDRESS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copyfile")
                    newAtt.Value = If(dtMachine.Item(x).COPY_FILE, 1, 0) 'dtMachine.Item(x).COPY_FILE.ToString
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copypath")
                    newAtt.Value = dtMachine.Item(x).COPY_PATH
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copywildcard")
                    newAtt.Value = dtMachine.Item(x).COPY_WILDCARD
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copypurgeoldpath")
                    newAtt.Value = dtMachine.Item(x).COPY_PURGE_OLD_PATH
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copyperiodmillisec")
                    newAtt.Value = dtMachine.Item(x).COPY_PERIOD_MILLISEC
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("copypurgeperiodday")
                    newAtt.Value = dtMachine.Item(x).COPY_PURGE_PERIOD_DAY
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("serial_BH")
                    newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_BH
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("serial_TS")
                    newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_TS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("serial_CW")
                    newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_CW
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("serial_ITA")
                    newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_ITA
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("serial_CS")
                    newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_CS
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("mode")
                    newAtt.Value = dtMachine.Item(x).FORMAT_MODE
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("mc")
                    newAtt.Value = dtMachine.Item(x).FORMAT_MC
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("lotno")
                    newAtt.Value = dtMachine.Item(x).FORMAT_LOT_NO
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("filename")
                    newAtt.Value = dtMachine.Item(x).FORMAT_FILE_NAME
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("datetime")
                    newAtt.Value = dtMachine.Item(x).FORMAT_DATE_TIME
                    nodeLine.Attributes.Append(newAtt)

                    newAtt = Doc.CreateAttribute("status")
                    newAtt.Value = dtMachine.Item(x).FORMAT_STATUS
                    nodeLine.Attributes.Append(newAtt)

                    dtMachine_Col = m_taMachine_Col.GetDataBy_MACHINE_ID(dtMachine.Item(x).ID)
                    strFormat = ""
                    For mc = 0 To dtMachine_Col.Count - 1
                        astrSCREEN_COLUMN_NAME = dtMachine_Col.Item(mc).SCREEN_COLUMN_NAME.Split({"|"}, StringSplitOptions.None)
                        strFormat = strFormat & astrSCREEN_COLUMN_NAME(astrSCREEN_COLUMN_NAME.Count - 1) & "," & _
                                                dtMachine_Col.Item(mc).DATA_TYPE & "," & _
                                                dtMachine_Col.Item(mc).POSITION & "," & _
                                                dtMachine_Col.Item(mc).LENGTH & "," & _
                                                If(dtMachine_Col.Item(mc).IsMULTIPLIERNull, "", _
                                                    dtMachine_Col.Item(mc).MULTIPLIER) & "," & _
                                                If(dtMachine_Col.Item(mc).IsFORMAT_STRINGNull, "", _
                                                    dtMachine_Col.Item(mc).FORMAT_STRING) & "," & _
                                                dtMachine_Col.Item(mc).REPLACE_COLUMN_NAME & "," & _
                                                dtMachine_Col.Item(mc).ID.ToString.Trim & "|"
                    Next

                    strFormat = Mid(strFormat, 1, strFormat.Length - 1)
                    newAtt = Doc.CreateAttribute("format")
                    newAtt.Value = strFormat
                    nodeLine.Attributes.Append(newAtt)

                    elementSetting.AppendChild(nodeLine)
                Next
            End If

            Doc.Save(xmlFilePath)
            objLogger.AppendLog("Export XML file success.", "Info")
            Return True
        Catch ex As Exception
            objLogger.AppendLog("ExportXMLFile", "cannot export XML file.", ex)
            Return False
        End Try

    End Function

#End Region

End Class
