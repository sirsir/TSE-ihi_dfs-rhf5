Imports System.Xml

Public Class frmResultHistory

    'Private dtStatic As DataTable
    Private dtStatic As ihi_rhf5_developmentDataSet.SERIALDataTable
    Private dtStatic2 As DataTable

    Private dtDynamicDataString As DataTable
    Private dtDynamicDataInteger As DataTable
    Private dtDynamicDataDateTime As DataTable
    'Private dtColumnHeader As ihi_rhf5_developmentDataSet.V_SETTING_COLUMNSDataTable
    Private dtColumnHeader As DataTable
    Private astrRowHeaderMappingDataMember As String()
    Private strRawDataColumnMappingDataMember As String
    Private astrRawDataRowMappingDataMember As String()
    Private strRawDataValueDataMember As String
    Private astrShowColumnNames
    Private blnReadOnly As Boolean

    Private intColumnMode As Integer = 0


    Private m_objLogger As clsLogger = New clsLogger

    Private m_datDateTime As DateTime

    Private m_strInsertValues As String

    Private m_strMachineName As String

    Private Sub frmResultHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            SetDao()
            TextBoxStatus.Text = "Program started with Database OK. Please use Filter data to show data."
        Catch ex As Exception
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        initialize_modGlobalVariable()
        AddControls()

        'SetupDataObj()
        'SetControls()

    End Sub


    Private Sub AddControls()
        'Dim textbox1 As New TextBox
        'textbox1.Name = "Textbox1"
        'textbox1.Size = New Size(170, 20)
        'textbox1.Location = New Point(167, 32)
        'Me.TableLayoutPanel3.Controls.Add(textbox1, 0, 0)

        'Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        'Me.TableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset

        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "*Line:", "Line", 0, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Machine:", "Machine", 2, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Serial:", "Serial", 4, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Search by:", "SearchBy", 6, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Date From:", "DateFrom", 0, 1)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Date To:", "DateTo", 2, 1)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Lot No:", "LotNo", 4, 1)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Result:", "Result", 6, 1)


        For Each ctrl As Control In Me.TableLayoutPanel3.Controls
            If TypeOf (ctrl) Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
        Next


    End Sub


    Private Sub AddLabelTextbox_to_Table(tbl1 As TableLayoutPanel, strLable As String, strName As String, intRow As Integer, intCol As Integer)
        tbl1.Controls.Add(New Label() With {.Text = strLable, .Anchor = AnchorStyles.Right, .AutoSize = True}, intRow, intCol)
        'tbl1.Controls.Add(New ComboBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)
        tbl1.Controls.Add(New TextBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)
    End Sub


    Private Sub FilterData()
        ' Do something here to handle data from dialog box.
        'Me.TableLayoutPanel3.Controls("Line").Text = dicSearch("Line")
        'Me.TableLayoutPanel3.Controls("Machine").Text = dicSearch("Machine")
        'Me.TableLayoutPanel3.Controls("SearchBy").Text = dicSearch("Search By")
        'Me.TableLayoutPanel3.Controls("Serial").Text = dicSearch("Serial")
        'Me.TableLayoutPanel3.Controls("LotNo").Text = dicSearch("Lot No")
        'Me.TableLayoutPanel3.Controls("DateFrom").Text = dicSearch("Date From")
        'Me.TableLayoutPanel3.Controls("DateTo").Text = dicSearch("Date To")
        'Me.TableLayoutPanel3.Controls("Result").Text = dicSearch("Result")

        Me.TextBoxLine.Text = dicSearch("Line")
        Me.TextBoxMachine.Text = IIf(dicSearch("Machine") = "", "ALL", dicSearch("Machine"))
        Me.TextBoxSearchBy.Text = dicSearch("Search By")
        Me.TextBoxSerial.Text = dicSearch("Serial")
        Me.TextBoxLotNo.Text = dicSearch("Lot No")
        Me.TextBoxDateFrom.Text = dicSearch("Date From")
        Me.TextBoxDateTo.Text = dicSearch("Date To")
        Me.TextBoxTimes.Text = dicSearch("Times")
        Me.TextBoxResult.Text = dicSearch("Result")


        'dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.ParseExact(dicSearch("Date From"), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
        '                                              DateTime.ParseExact(dicSearch("Date To"), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture))

        'dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(dicSearch("Date From"), dicSearch("Date To"))

        'If dicSearch("Times").ToLower = "all" Or dicSearch("Times").ToLower = "" Or dicSearch("Date From") <> dicSearch("Date To") Then
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")), DateTime.Parse(dicSearch("Date To")).AddDays(1))
        'ElseIf dicSearch("Times") = "08:01 - 12:00" Then
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(8), DateTime.Parse(dicSearch("Date To")).AddHours(12))
        'ElseIf dicSearch("Times") = "12:01 - 17:00" Then
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(12), DateTime.Parse(dicSearch("Date To")).AddHours(17))
        'ElseIf dicSearch("Times") = "17:01 - 22:00" Then
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(17), DateTime.Parse(dicSearch("Date To")).AddHours(22))
        'ElseIf dicSearch("Times") = "22:01 - 08:00" Then
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(22), DateTime.Parse(dicSearch("Date To")).AddHours(32))
        'Else
        '    dtStatic = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")), DateTime.Parse(dicSearch("Date To")).AddDays(1))
        'End If





        dtStatic2 = DirectCast(dtStatic, DataTable)

        If dicSearch("Serial") <> "" And (Not dicSearch("Serial").ToUpper = "ALL") Then
            Dim dv As DataView = dtStatic2.DefaultView
            dv.RowFilter = dicMapSearchBy(dicSearch("Search By")) & " LIKE '%" & dicSearch("Serial") & "%'"

            dtStatic2 = dv.ToTable
        End If


        If dicSearch("Lot No") <> "" And (Not dicSearch("Lot No").ToUpper = "ALL") Then
            Dim dv As DataView = dtStatic2.DefaultView
            dv.RowFilter = dicMapSearchBy(dicSearch("Search By")) & " LIKE '" & dicSearch("Lot No") & "%'"

            dtStatic2 = dv.ToTable
        End If






        Dim dtResult As ihi_rhf5_developmentDataSet.RESULTDataTable
        Dim datFrom As DateTime
        Dim datTo As DateTime
        If dicSearch("Date From") = "" Then
            datFrom = New Date(2000, 1, 1)
        Else
            datFrom = DateTime.ParseExact(dicSearch("Date From"), SCREEN_DATE_FORMAT, _
                                                       System.Globalization.CultureInfo.InvariantCulture)
        End If

        If dicSearch("Date To") = "" Then
            datTo = New Date(2100, 1, 1)
        Else
            datTo = DateTime.ParseExact(dicSearch("Date To"), SCREEN_DATE_FORMAT, _
                                                      System.Globalization.CultureInfo.InvariantCulture)
        End If


        'If dicSearch("Times").ToLower = "all" Or dicSearch("Times") = "" Or dicSearch("Date From") <> dicSearch("Date To") Then

        'If dicSearch("Machine").ToLower = "all" Then
        '    dtResult = da_RESULT.GetData


        'Else





        'If dicSearch("Times").ToLower = "all" Or dicSearch("Times") = "" Or dicSearch("Date From") <> dicSearch("Date To") Then
        If dicSearch("Times").ToLower = "all" Or dicSearch("Times") = "" Or dicSearch("Machine").ToLower = "all" Then

            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom, datTo.AddDays(1))
        ElseIf dicSearch("Times") = "08:01 - 12:00" Then
            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom.AddHours(8), datTo.AddHours(12))
        ElseIf dicSearch("Times") = "12:01 - 17:00" Then
            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom.AddHours(12), datTo.AddHours(17))
        ElseIf dicSearch("Times") = "17:01 - 22:00" Then
            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom.AddHours(17), datTo.AddHours(22))
        ElseIf dicSearch("Times") = "22:01 - 08:00" Then
            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom.AddHours(22), datTo.AddHours(32))
        Else
            dtResult = da_RESULT.GetDataByDateFrom_DateTo(datFrom, datTo.AddDays(1))

            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")), DateTime.Parse(dicSearch("Date To")).AddDays(1))
            'ElseIf dicSearch("Times") = "08:01 - 12:00" Then
            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(8), DateTime.Parse(dicSearch("Date To")).AddHours(12))
            'ElseIf dicSearch("Times") = "12:01 - 17:00" Then
            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(12), DateTime.Parse(dicSearch("Date To")).AddHours(17))
            'ElseIf dicSearch("Times") = "17:01 - 22:00" Then
            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(17), DateTime.Parse(dicSearch("Date To")).AddHours(22))
            'ElseIf dicSearch("Times") = "22:01 - 08:00" Then
            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")).AddHours(22), DateTime.Parse(dicSearch("Date To")).AddHours(32))
            'Else
            '    dtResult = da_RESULT.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")), DateTime.Parse(dicSearch("Date To")).AddDays(1))
            'End If

        End If
        'End If


        Dim intMachineId As Integer = -1
        Dim adrResult As ihi_rhf5_developmentDataSet.RESULTRow()
        If dicSearch("Machine") <> "" And (Not dicSearch("Machine").ToLower = "all") Then
            Dim dtMachine As ihi_rhf5_developmentDataSet.MACHINEDataTable
            dtMachine = da_MACHINE.GetDataByLineId(dicMapLineId(dicSearch("Line")))
            Dim adrMachine As ihi_rhf5_developmentDataSet.MACHINERow() = dtMachine.Select("MACHINE_NAME = '" & dicSearch("Machine") & "'")
            intMachineId = adrMachine(0).ID
            adrResult = dtResult.Select("MACHINE_ID = " & intMachineId)
        Else
            adrResult = dtResult.Select("MACHINE_ID > 0")
        End If

        Dim lstSerial As List(Of String) = New List(Of String)
        Dim lstTransactionId As List(Of String) = New List(Of String)

        For Each r As ihi_rhf5_developmentDataSet.RESULTRow In adrResult

            If dicSearch("Result").ToUpper = "OK" Then
                If r.STATUS = "OK" Then
                    lstSerial.Add(r.SERIAL_ID)
                    lstTransactionId.Add(r.TRAN_ID)
                End If

            ElseIf dicSearch("Result").ToUpper = "NG" Then
                If r.STATUS = "NG" Then
                    lstSerial.Add(r.SERIAL_ID)
                    lstTransactionId.Add(r.TRAN_ID)
                End If
            Else
                lstSerial.Add(r.SERIAL_ID)
                lstTransactionId.Add(r.TRAN_ID)
            End If

        Next





        lstSerial = lstSerial.Distinct.ToList

        'If dicSearch("Result").ToUpper = "OK" Then
        '    For Each strSerial In lstSerial.ToArray
        '        If strSerial Then
        '            lstSerial
        '        End If
        '    Next
        'End If


        Dim dv2 As DataView = dtStatic2.DefaultView
        If lstSerial.Count > 0 Then

            dv2.RowFilter = "SERIAL_ID In (" & String.Join(",", lstSerial.ToArray) & ")"

            'dtStatic = DirectCast(dv2.ToTable, ihi_rhf5_developmentDataSet.SERIALDataTable)
            dtStatic2 = dv2.ToTable
        Else
            'dtStatic = New ihi_rhf5_developmentDataSet.MACHINE_DATA_STR_w_SERIALDataTable()
            'dtStatic = New ihi_rhf5_developmentDataSet.SERIALDataTable()
            dtStatic2 = New ihi_rhf5_developmentDataSet.SERIALDataTable()
            dtStatic2.Clear()

        End If


        If dicSearch("Line").ToLower = "all" Then
            dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetDataByMachineName("%")
            dtColumnHeader = da_V_SETTING_COLUMNS.GetDataByMachineName("%")

        Else
            If dicSearch("Machine") <> "" And (Not dicSearch("Machine").ToLower = "all") And lstTransactionId.Count > 0 Then
                dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetDataByLineID_MachineName(dicMapLineId(dicSearch("Line")), dicSearch("Machine"))

                Dim dvDynamicDataString As DataView = dtDynamicDataString.DefaultView
                dvDynamicDataString.RowFilter = "RESULT_ID In (" & String.Join(",", lstTransactionId.ToArray) & ")"

                'dtStatic = DirectCast(dv2.ToTable, ihi_rhf5_developmentDataSet.SERIALDataTable)
                dtDynamicDataString = dvDynamicDataString.ToTable


                dtColumnHeader = da_V_SETTING_COLUMNS.GetDataByLineID_MachineName(dicMapLineId(dicSearch("Line")), dicSearch("Machine"))
            Else
                dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetDataByLineID_MachineName(dicMapLineId(dicSearch("Line")), "%")
                dtColumnHeader = da_V_SETTING_COLUMNS.GetDataByLineID_MachineName(dicMapLineId(dicSearch("Line")), "%")
            End If



            'If dicSearch("Machine") <> "" Then
            '    dtStatic = da_SERIAL.GetData()

            '    'dtDynamicDataString = da_MACHINE_DATA_STR.GetData()
            '    'dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetData()
            '    If dicSearch("DateFrom") = "" And dicSearch("DateFrom") = "" Then
            '        dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetDataByMachineName(dicSearch("Machine"))
            '    Else
            '        If dicSearch("DateFrom") = "" Then

            '        End If
            '    End If
        End If




        dtDynamicDataInteger = Nothing
        dtDynamicDataDateTime = Nothing
        'dtColumnHeader = da_V_SETTING_COLUMNS.GetDataByMachineName(dicSearch("Machine"))

        'dtColumnHeader = da_V_SETTING_COLUMNS.GetData()
        'astrRowHeaderMappingDataMember = {"ENGINE_ID", "REV_NO"}
        astrRowHeaderMappingDataMember = {"SERIAL_ID"}
        strRawDataColumnMappingDataMember = "MACHINE_COLUMNS_ID"
        'astrRawDataRowMappingDataMember = {"ENGINE_ID", "REV_NO"}
        astrRawDataRowMappingDataMember = {"SERIAL_ID"}
        strRawDataValueDataMember = "DATA"
        astrShowColumnNames = {"SERIAL_ID", "SERIAL_BH", "SERIAL_CS", "SERIAL_ITA"}



        DataGridView1.Columns.Clear()

        DirectCast(DataGridView1, ctrlPivotGrid).SetDataSource(dtStatic2 _
                                        , dtDynamicDataString _
                                        , dtDynamicDataInteger _
                                        , dtDynamicDataDateTime _
                                        , dtColumnHeader _
                                        , astrRowHeaderMappingDataMember _
                                        , strRawDataColumnMappingDataMember _
                                        , astrRawDataRowMappingDataMember _
                                        , strRawDataValueDataMember _
                                        , astrShowColumnNames _
                                         , blnReadOnly)

        DoColumns()

        'DataGridView1.Rows(1).Height = DataGridView1.Rows(1).Height * 1.5

        TextBoxStatus.Text = "Number of Filtered Data:=" & (DataGridView1.RowCount - 1).ToString

        'For Each col In DataGridView1.Columns
        '    col.
        'Next

    End Sub

    Private Sub ButtonFilter_Click(sender As Object, e As EventArgs) Handles ButtonFilter.Click
        'dataFilter()
        Try

            'Dim dlrResult As DialogResult = MessageBox.Show("You want to End Lot?", _
            '"End Lot", _
            'MessageBoxButtons.YesNo)

            Dim frm As New frmFilter
            'frm.ShowDialog()

            If frm.ShowDialog = DialogResult.OK Then
                Me.Cursor = Cursors.WaitCursor
                Me.FilterData()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            m_clsLogger.AppendLog(ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SetDao()
        'dtStatic = DirectCast(dtStatic, ihi_rhf5_developmentDataSetTableAdapters.SERIALTableAdapter).getdata()
        dtStatic = da_SERIAL.GetData()

        '------Comment out to show all TODAY result >> SLOW opening!
        ''dicSearch("Date From") = Date.Now
        ''dicSearch("Date To") = Date.Now
        'dicSearch("Date From") = Date.Today
        'dicSearch("Date To") = Date.Today


        'Me.TextBoxDateFrom.Text = dicSearch("Date From")
        'Me.TextBoxDateTo.Text = dicSearch("Date To")

        ''dtStatic2 = da_SERIAL.GetDataByDateFrom_DateTo(DateTime.Parse(dicSearch("Date From")), DateTime.Parse(dicSearch("Date To")).AddDays(1))
        ''dtStatic2 = da_SERIAL.GetDataByDateFrom_DateTo(Date.Now, Date.Now.AddDays(1)).DefaultView.ToTable
        ''dtStatic2 = da_SERIAL.GetDataByDateFrom_DateTo(Date.Parse(dicSearch("Date From")).AddDays(-1), Date.Parse(dicSearch("Date To")).AddDays(1))
        'dtStatic2 = da_SERIAL.GetDataByDateFrom_DateTo(Date.Parse(dicSearch("Date From")), Date.Parse(dicSearch("Date To")).AddDays(1))
        ''dtStatic2 = da_SERIAL.GetDataByDateFrom_DateTo(Date.Now, Date.Now.AddDays(1))



        'dtDynamicDataString = da_MACHINE_DATA_STR.GetData()
        dtDynamicDataString = da_MACHINE_DATA_STR_w_SERIAL.GetData()
        dtDynamicDataInteger = Nothing
        dtDynamicDataDateTime = Nothing
        dtColumnHeader = da_V_SETTING_COLUMNS.GetData()
        'astrRowHeaderMappingDataMember = {"ENGINE_ID", "REV_NO"}
        astrRowHeaderMappingDataMember = {"SERIAL_ID"}
        strRawDataColumnMappingDataMember = "MACHINE_COLUMNS_ID"
        'astrRawDataRowMappingDataMember = {"ENGINE_ID", "REV_NO"}
        astrRawDataRowMappingDataMember = {"SERIAL_ID"}
        strRawDataValueDataMember = "DATA"
        astrShowColumnNames = {"SERIAL_ID", "SERIAL_BH", "SERIAL_CS", "SERIAL_ITA", "SERIAL_TS", "SERIAL_CW"}





        'DirectCast(DataGridView1, ctrlPivotGrid).SetDataSource(dtStatic2 _
        '                                , dtDynamicDataString _
        '                                , dtDynamicDataInteger _
        '                                , dtDynamicDataDateTime _
        '                                , dtColumnHeader _
        '                                , astrRowHeaderMappingDataMember _
        '                                , strRawDataColumnMappingDataMember _
        '                                , astrRawDataRowMappingDataMember _
        '                                , strRawDataValueDataMember _
        '                                , astrShowColumnNames _
        '                                 , blnReadOnly)

        'HideColumns()


    End Sub


    Private Sub DoColumns()
        'If CheckBoxAutoResizeColumnIgnoreHead.Checked Then
        '    ' Me.DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        '    Me.DataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        'Else

        '    DataGridView1.ColumnHeadersHeight = 23
        '    Me.DataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False
        'End If


        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.HeaderText.IndexOf("Serial") > 0 Or col.HeaderText.IndexOf("Lot No") > 0 Or col.HeaderText.IndexOf("File Name") > 0 Or col.HeaderText.IndexOf("Setting Data") > 0 Or col.HeaderText.IndexOf("Marking Data") > 0 Or col.HeaderText.IndexOf("Read Data") > 0 Then
                'col.
                col.Visible = False
            End If

            'Set column size auto
            'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

            If CheckBoxAutoResizeColumnIgnoreHead.Checked Then
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            Else
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader

                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End If


        Next

        DataGridView1.Refresh()

        'For Each col As DataGridViewColumn In DataGridView1.Columns
        '    If CheckBoxAutoResizeColumnIgnoreHead.Checked Then
        '        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        '    End If

        'Next

        DataGridView1.AllowUserToResizeColumns = True

        ButtonResizeColumns.Text = "ENLARGE columns"
        intColumnMode = 1


    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles ButtonShowParts.Click
        If dicSearch("Lot No").ToLower = "all" Or dicSearch("Lot No") = "" Then
            MsgBox("""Lot No"" must be set. (By Filtering)")
            Exit Sub
        End If


        Try
            'Dim intSelectRow As Integer

            'intSelectRow = DataGridView1.CurrentCell.RowIndex

            'Dim dr As DataRow

            'dr = DataGridView1.Rows(intSelectRow).DataBoundItem


            'dr = DataGridView1.SelectedRows(0).DataBoundItem
            'dr = DataGridView1.CurrentRow.DataBoundItem

            'dr = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).DataBoundItem.Row


            If DataGridView1.CurrentCell Is Nothing Then
                drSelectedRow = DataGridView1.Rows(0).DataBoundItem.Row
            Else
                drSelectedRow = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).DataBoundItem.Row
            End If

            'If drSelectedRow Is Nothing Then
            '    drSelectedRow = DataGridView1.Rows(0).DataBoundItem
            'End If



            Dim frm As New frmShowParts
            'frm.ShowDialog()

            If frm.ShowDialog = DialogResult.OK Then

                'Me.FilterData()



            End If

        Catch ex As Exception
            MsgBox("Selected Row does not have supported format for this function")
            m_clsLogger.AppendLog(ex)
            'MsgBox(ex.StackTrace)
        End Try




    End Sub

    Private Sub ButtonRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRefresh.Click

        Try



            'da_MACHINE_DATA_STR_w_SERIAL = New ihi_rhf5_developmentDataSetTableAdapters.MACHINE_DATA_STR_w_SERIALTableAdapter()

            SetDao()

            FilterData()

        Catch ex As Exception
            m_clsLogger.AppendLog(ex)
        End Try
    End Sub

    Private Sub ButtonExport_XLS_Click(sender As System.Object, e As System.EventArgs) Handles ButtonExport_XLS.Click

        Try

            If dicSearch("Machine") = "" Or dicSearch("Machine").ToUpper = "ALL" Then
                MsgBox("Please select ""Machine"" by Filtering data!")
                Exit Sub
            Else


                Dim strExportPath As String
                SaveFileDialog1.Filter = "Excel|*.xls"
                SaveFileDialog1.FileName = ""
                SaveFileDialog1.AddExtension = True
                If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    strExportPath = SaveFileDialog1.FileName

                    'strExportPath = IIf(Right(strExportPath, 4) = ".xls", strExportPath, strExportPath & ".xls")
                    'strExportPath.Split(".").Last()
                    strExportPath = IIf(strExportPath.Split(".").Last = "xls", strExportPath, strExportPath & ".xls")
                Else
                    Exit Sub
                End If

                If My.Computer.FileSystem.FileExists(strExportPath) Then
                    My.Computer.FileSystem.DeleteFile(strExportPath)
                End If


                'Dim xmlFilePath As String = GetSettingPath("lineDBExport")

                'Dim blnExportXMLFile As Boolean

                'blnExportXMLFile = ExportXMLFile(xmlFilePath)

                m_strMachineName = dicSearch("Machine")

                Dim strInsertValues As String

                Dim dtMachineColumn As ihi_rhf5_developmentDataSet.MACHINE_COLUMNSDataTable = New ihi_rhf5_developmentDataSet.MACHINE_COLUMNSDataTable

                Dim daMachineColumn As ihi_rhf5_developmentDataSetTableAdapters.MACHINE_COLUMNSTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.MACHINE_COLUMNSTableAdapter()

                daMachineColumn.Fill(dtMachineColumn)


                'For Each rFromGrid In DataGridView1.Rows

                For Each rFromGrid As DataGridViewRow In DataGridView1.Rows
                    Try


                        'Dim intSerialId As Integer = rFromGrid.Item(0)
                        Dim intSerialId As Integer = rFromGrid.Cells.Item(0).Value

                        strInsertValues = Me.GetInsertValues("", "", "")

                        'For Each r As ihi_rhf5_developmentDataSet.MACHINE_DATA_STR_w_SERIALRow In dtDynamicDataString.Rows
                        For Each r As ihi_rhf5_developmentDataSet.MACHINE_DATA_STR_w_SERIALRow In da_MACHINE_DATA_STR_w_SERIAL.GetDataBySerialId(intSerialId).Rows



                            Dim strValue As String = r.DATA
                            Dim strReplacePattern As String = dtMachineColumn.FindByID(r.MACHINE_COLUMNS_ID).REPLACE_COLUMN_NAME.ToString

                            strInsertValues = Me.GetInsertValues(strValue, strReplacePattern, strInsertValues)

                        Next


                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId), "[SERIAL_CS]", strInsertValues)

                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.s).FindBySERIAL_ID(intSerialId).SERIAL_CS, "[SERIAL_CS]", strInsertValues)
                        'Dim strTemp As String = dtStatic.Select

                        'Dim intReturn As Integer = (From rows In DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable)
                        'Where (rows.SERIAL_ID = intSerialId)
                        '                         Select rows).First

                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_CS, "[SERIAL_CS]", strInsertValues)

                        'For Each r As ihi_rhf5_developmentDataSet.SERIALRow In da_SERIAL.GetDataBySerialId(intSerialId).Rows


                        'Next

                        strInsertValues = Me.GetInsertValues(dtStatic.FindBySERIAL_ID(intSerialId).SERIAL_CS, "[SERIAL_CS]", strInsertValues)
                        strInsertValues = Me.GetInsertValues(dtStatic.FindBySERIAL_ID(intSerialId).SERIAL_BH, "[SERIAL_BH]", strInsertValues)
                        strInsertValues = Me.GetInsertValues(dtStatic.FindBySERIAL_ID(intSerialId).SERIAL_ITA, "[SERIAL_ITA]", strInsertValues)

                        Try

                            strInsertValues = Me.GetInsertValues(dtStatic.FindBySERIAL_ID(intSerialId).SERIAL_TS, "[SERIAL_TS]", strInsertValues)
                            strInsertValues = Me.GetInsertValues(dtStatic.FindBySERIAL_ID(intSerialId).SERIAL_CW, "[SERIAL_CW]", strInsertValues)
                        Catch ex As Exception
                            MsgBox(ex.StackTrace)
                        End Try






                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic.Rows.Find(intSerialId), ihi_rhf5_developmentDataSet.SERIALRow).SERIAL_CS, "[SERIAL_CS]", strInsertValues)

                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_CS, "[SERIAL_CS]", strInsertValues)
                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_BH, "[SERIAL_BH]", strInsertValues)
                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_CW, "[SERIAL_CW]", strInsertValues)
                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_ITA, "[SERIAL_ITA]", strInsertValues)
                        'strInsertValues = Me.GetInsertValues(DirectCast(dtStatic, ihi_rhf5_developmentDataSet.SERIALDataTable).FindBySERIAL_ID(intSerialId).SERIAL_TS, "[SERIAL_TS]", strInsertValues)





                        m_strInsertValues = Me.SetDefaultRemainingInsertValues(strInsertValues)

                        'WriteDataToExcel(dicSearch("Machine"))

                        WriteDataToExcel(dicSearch("Machine"), strExportPath)
                    Catch ex As Exception

                    End Try


                Next






                MsgBox("Exporting data to """ & strExportPath & """ completed")

            End If


        Catch ex As Exception
            'm_clsLogger.AppendLog(ex)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    'Private Shared Function ExportXMLFile(ByVal xmlFilePath As String) As Boolean
    '    Dim objLogger = New clsLogger
    '    Try

    '        If System.IO.File.Exists(xmlFilePath) = True Then

    '            System.IO.File.Delete(xmlFilePath)

    '        End If

    '        Dim Doc As New XmlDocument()
    '        Dim newAtt As XmlAttribute
    '        Dim strFormat As String = ""

    '        ' Use the XmlDeclaration class to place the
    '        ' <?xml version="1.0"?> declaration at the top of our XML file
    '        Dim dec As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", "utf-8", "")
    '        Doc.AppendChild(dec)
    '        Dim elementSetting As XmlElement = Doc.CreateElement("settings")
    '        Doc.AppendChild(elementSetting)

    '        'Generate common Element
    '        Dim m_taLineMaster As New ihi_rhf5_developmentDataSetTableAdapters.LINE_MASTERTableAdapter
    '        Dim dtLineMas As ihi_rhf5_developmentDataSet.LINE_MASTERDataTable = m_taLineMaster.GetData
    '        Dim nodeCommon As XmlNode = Doc.CreateElement("common")

    '        If dtLineMas.Count > 0 Then
    '            For i = 0 To dtLineMas.Count - 1
    '                newAtt = Doc.CreateAttribute("root")
    '                newAtt.Value = dtLineMas.Item(i).ROOT_PATH
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("root_temp")
    '                newAtt.Value = dtLineMas.Item(i).ROOT_TEMP
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("sleep_interval")
    '                newAtt.Value = dtLineMas.Item(i).SLEEP_INTERVAL
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("readdataasciilength")
    '                newAtt.Value = dtLineMas.Item(i).READ_ASCII_LENGTH
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("readdatabcdlength")
    '                newAtt.Value = dtLineMas.Item(i).READ_BCD_LENGTH
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("synctimewhen")
    '                newAtt.Value = dtLineMas.Item(i).SYNC_TIME_WHEN
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("usecsvmode")
    '                newAtt.Value = Convert.ToInt32(dtLineMas.Item(i).USE_CSV_MODE).ToString
    '                nodeCommon.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("usexlsmode")
    '                newAtt.Value = Convert.ToInt32(dtLineMas.Item(i).USE_XLS_MODE).ToString
    '                nodeCommon.Attributes.Append(newAtt)

    '                elementSetting.AppendChild(nodeCommon)
    '            Next
    '        End If

    '        'Generate line Element
    '        Dim m_taMachine As New ihi_rhf5_developmentDataSetTableAdapters.MACHINETableAdapter
    '        Dim dtMachine As ihi_rhf5_developmentDataSet.MACHINEDataTable = m_taMachine.GetData
    '        Dim m_taMachine_Col As New ihi_rhf5_developmentDataSetTableAdapters.MACHINE_COLUMNSTableAdapter
    '        Dim dtMachine_Col As ihi_rhf5_developmentDataSet.MACHINE_COLUMNSDataTable = Nothing
    '        Dim astrSCREEN_COLUMN_NAME() As String

    '        If dtMachine.Count > 0 Then
    '            For x = 0 To dtMachine.Count - 1

    '                Dim nodeLine As XmlNode = Doc.CreateElement("line")

    '                newAtt = Doc.CreateAttribute("no")
    '                newAtt.Value = dtMachine.Item(x).MACHINE_NO
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("name")
    '                newAtt.Value = dtMachine.Item(x).MACHINE_NAME
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("path")
    '                newAtt.Value = dtMachine.Item(x).PATH
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("net")
    '                newAtt.Value = dtMachine.Item(x).PLC_NET
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("node")
    '                newAtt.Value = dtMachine.Item(x).PLC_NODE
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("unit")
    '                newAtt.Value = dtMachine.Item(x).PLC_UNIT
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("readstatusaddress")
    '                newAtt.Value = dtMachine.Item(x).READ_STATUS_ADDRESS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("readstatuslength")
    '                newAtt.Value = dtMachine.Item(x).READ_STATUS_LENGTH
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("readdataaddress")
    '                newAtt.Value = dtMachine.Item(x).READ_DATA_ADDRESS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("writestatusaddress")
    '                newAtt.Value = dtMachine.Item(x).WRITE_STATUS_ADDRESS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("writelifeaddress")
    '                newAtt.Value = dtMachine.Item(x).WRITE_LIFE_ADDRESS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("writesyncaddress")
    '                newAtt.Value = dtMachine.Item(x).WRITE_SYNC_ADDRESS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copyfile")
    '                newAtt.Value = If(dtMachine.Item(x).COPY_FILE, 1, 0) 'dtMachine.Item(x).COPY_FILE.ToString
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copypath")
    '                newAtt.Value = dtMachine.Item(x).COPY_PATH
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copywildcard")
    '                newAtt.Value = dtMachine.Item(x).COPY_WILDCARD
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copypurgeoldpath")
    '                newAtt.Value = dtMachine.Item(x).COPY_PURGE_OLD_PATH
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copyperiodmillisec")
    '                newAtt.Value = dtMachine.Item(x).COPY_PERIOD_MILLISEC
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("copypurgeperiodday")
    '                newAtt.Value = dtMachine.Item(x).COPY_PURGE_PERIOD_DAY
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("serial_BH")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_BH
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("serial_TS")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_TS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("serial_CW")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_CW
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("serial_ITA")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_ITA
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("serial_CS")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_SERIAL_CS
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("mode")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_MODE
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("mc")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_MC
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("lotno")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_LOT_NO
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("filename")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_FILE_NAME
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("datetime")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_DATE_TIME
    '                nodeLine.Attributes.Append(newAtt)

    '                newAtt = Doc.CreateAttribute("status")
    '                newAtt.Value = dtMachine.Item(x).FORMAT_STATUS
    '                nodeLine.Attributes.Append(newAtt)

    '                dtMachine_Col = m_taMachine_Col.GetDataBy_MACHINE_ID(dtMachine.Item(x).ID)
    '                strFormat = ""
    '                For mc = 0 To dtMachine_Col.Count - 1
    '                    astrSCREEN_COLUMN_NAME = dtMachine_Col.Item(mc).SCREEN_COLUMN_NAME.Split({"|"}, StringSplitOptions.None)
    '                    strFormat = strFormat & astrSCREEN_COLUMN_NAME(astrSCREEN_COLUMN_NAME.Count - 1) & "," & _
    '                                            dtMachine_Col.Item(mc).DATA_TYPE & "," & _
    '                                            dtMachine_Col.Item(mc).POSITION & "," & _
    '                                            dtMachine_Col.Item(mc).LENGTH & "," & _
    '                                            If(dtMachine_Col.Item(mc).IsMULTIPLIERNull, "", _
    '                                                dtMachine_Col.Item(mc).MULTIPLIER) & "," & _
    '                                            If(dtMachine_Col.Item(mc).IsFORMAT_STRINGNull, "", _
    '                                                dtMachine_Col.Item(mc).FORMAT_STRING) & "," & _
    '                                            dtMachine_Col.Item(mc).REPLACE_COLUMN_NAME & "," & _
    '                                            dtMachine_Col.Item(mc).ID.ToString.Trim & "|"
    '                Next

    '                strFormat = Mid(strFormat, 1, strFormat.Length - 1)
    '                newAtt = Doc.CreateAttribute("format")
    '                newAtt.Value = strFormat
    '                nodeLine.Attributes.Append(newAtt)

    '                elementSetting.AppendChild(nodeLine)
    '            Next
    '        End If

    '        Doc.Save(xmlFilePath)
    '        objLogger.AppendLog("Export XML file success.", "Info")
    '        Return True
    '    Catch ex As Exception
    '        objLogger.AppendLog("ExportXMLFile", "cannot export XML file.", ex)
    '        Return False
    '    End Try

    'End Function

    'Private Shared Function GetSettingPath(ByVal settingName As String) As String
    '    Dim strFileName As String = settingName & ".xml"
    '    Dim strProgramDataFileName As String = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & strFileName
    '    Dim strAppPathFileName As String = My.Application.Info.DirectoryPath & "\" & strFileName
    '    If My.Computer.FileSystem.FileExists(strProgramDataFileName) Then
    '        Return strProgramDataFileName
    '    Else
    '        Try
    '            My.Computer.FileSystem.CopyFile(strAppPathFileName, strProgramDataFileName, True)
    '            Return strProgramDataFileName
    '        Catch ex As Exception
    '            Return strAppPathFileName
    '        End Try
    '    End If
    'End Function


    Public Sub WriteDataToExcel(ByVal strMachineName As String, ByVal strFilePath As String)

        Dim strTemplatePath As String


        strTemplatePath = My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, ".\Template") _
                                    & "\" & strMachineName & ".xls"

        'strTemplatePath = strFilePath

        If Not My.Computer.FileSystem.FileExists(strFilePath) Then
            My.Computer.FileSystem.CopyFile(strTemplatePath, strFilePath)
        End If

        'If Not My.Computer.FileSystem.DirectoryExists(Me.GetFileOutputPath) Then
        '    My.Computer.FileSystem.CreateDirectory(Me.GetFileOutputPath)
        'End If

        'Dim strOutTempPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileTempOutputPath, m_strMc & "_" & m_strFileName & ".xls")
        'Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, m_strMc & "_" & m_strFileName & ".xls")

        'Dim strOutTempPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileTempOutputPath, strMachineName & ".xls")
        'Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, strMachineName & ".xls")

        Dim blnWriteHeader As Boolean = False
        'If Not My.Computer.FileSystem.FileExists(strOutTempPath) Then
        '    My.Computer.FileSystem.CopyFile(strTemplatePath, strOutTempPath)
        'End If

        Dim sql As String
        Using MyConnection As New System.Data.OleDb.OleDbConnection _
                                    ("provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & _
                                    "'" & strFilePath & "';" & _
                                    "Extended Properties=""Excel 8.0;HDR=YES""")
            MyConnection.Open()
            Using myCommand As New System.Data.OleDb.OleDbCommand()
                myCommand.Connection = MyConnection
                sql = GetInsertHeader() & m_strInsertValues
                myCommand.CommandText = sql
                m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Insert Excel = " & sql, "Info")
                myCommand.ExecuteNonQuery()
            End Using
        End Using
        'Try
        '    My.Computer.FileSystem.CopyFile(strOutTempPath, strOutFullPath, True)
        'Catch ex As Exception
        '    m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Copy to Output Folder Failed: " & ex.Message, "Error")
        'End Try
    End Sub

    Public Sub WriteDataToExcelOld(ByVal strMachineName As String)

        Dim strTemplatePath As String
        'strTemplatePath = My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, ".\Template") _
        '                            & "\" & m_objLineSetting.LineName & ".xls"

        'strTemplatePath = My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, ".\Template") _
        '                            & "\" & strMachineName & ".xls"

        strTemplatePath = strMachineName

        If Not My.Computer.FileSystem.FileExists(strTemplatePath) Then
            Return
        End If

        If Not My.Computer.FileSystem.DirectoryExists(Me.GetFileOutputPath) Then
            My.Computer.FileSystem.CreateDirectory(Me.GetFileOutputPath)
        End If

        'Dim strOutTempPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileTempOutputPath, m_strMc & "_" & m_strFileName & ".xls")
        'Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, m_strMc & "_" & m_strFileName & ".xls")

        Dim strOutTempPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileTempOutputPath, strMachineName & ".xls")
        Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, strMachineName & ".xls")

        Dim blnWriteHeader As Boolean = False
        If Not My.Computer.FileSystem.FileExists(strOutTempPath) Then
            My.Computer.FileSystem.CopyFile(strTemplatePath, strOutTempPath)
        End If

        Dim sql As String
        Using MyConnection As New System.Data.OleDb.OleDbConnection _
                                    ("provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & _
                                    "'" & strOutTempPath & "';" & _
                                    "Extended Properties=""Excel 8.0;HDR=YES""")
            MyConnection.Open()
            Using myCommand As New System.Data.OleDb.OleDbCommand()
                myCommand.Connection = MyConnection
                sql = GetInsertHeader() & m_strInsertValues
                myCommand.CommandText = sql
                m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Insert Excel = " & sql, "Info")
                myCommand.ExecuteNonQuery()
            End Using
        End Using
        Try
            My.Computer.FileSystem.CopyFile(strOutTempPath, strOutFullPath, True)
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Copy to Output Folder Failed: " & ex.Message, "Error")
        End Try
    End Sub

    Private Function GetFileOutputPath() As String
        'Dim strTemp As String = My.Computer.FileSystem.CombinePath(m_strWritePath, m_datDateTime.ToString("yyyyMMdd"))
        Dim strTemp As String = My.Computer.FileSystem.CombinePath(ExportPath(), m_datDateTime.ToString("yyyyMMdd"))


        'strTemp = My.Computer.FileSystem.CombinePath(strTemp, m_strLotNo)
        strTemp = My.Computer.FileSystem.CombinePath(strTemp, dicSearch("Lot No"))

        Return strTemp
    End Function

    Private Function GetFileTempOutputPath() As String
        'Dim strTemp As String = My.Computer.FileSystem.CombinePath(m_strTempPath, m_datDateTime.ToString("yyyyMMdd"))
        Dim strTemp As String = My.Computer.FileSystem.CombinePath(ExportPath(), m_datDateTime.ToString("yyyyMMdd"))

        'strTemp = My.Computer.FileSystem.CombinePath(strTemp, m_strLotNo)
        strTemp = My.Computer.FileSystem.CombinePath(strTemp, dicSearch("Lot No"))
        Return strTemp
    End Function

    Private Function GetInsertHeader() As String
        GetInsertHeader = "Insert into [DATA$] " _
                            & " ([ITEM],[SERIAL_CS], [MODE], [MC], [E], [F], [G], [H], [I], [J], [K], [L], [M], [N], [O], [P], [Q], [R], [S], [T], [U], [V], [DATE], [TIME], [STATUS], [SERIAL_BH], [SERIAL_TS], [SERIAL_CW], [SERIAL_ITA]) " _
                            & " values"
    End Function

    Private Function ExportPath() As String
        'If m_objLineSetting.WritePath.StartsWith(".\") Then
        '    Return My.Computer.FileSystem.CombinePath(m_objLineSetting.RootFolder, m_objLineSetting.WritePath)
        'Else
        '    Return m_objLineSetting.WritePath
        'End If
        If m_strMachineName.StartsWith(".\") Then
            'Return My.Computer.FileSystem.CombinePath(m_objLineSetting.RootFolder, m_strMachineName)
            'TO EDIT
            Return m_strMachineName
        Else
            Return m_strMachineName
        End If
    End Function

    Private Function SetDefaultRemainingInsertValues(ByVal strLastValuesString As String) As String
        strLastValuesString = strLastValuesString.Replace("[E]", "0")
        strLastValuesString = strLastValuesString.Replace("[F]", "0")
        strLastValuesString = strLastValuesString.Replace("[G]", "0")
        strLastValuesString = strLastValuesString.Replace("[H]", "0")
        strLastValuesString = strLastValuesString.Replace("[I]", "0")
        strLastValuesString = strLastValuesString.Replace("[J]", "0")
        strLastValuesString = strLastValuesString.Replace("[K]", "0")
        strLastValuesString = strLastValuesString.Replace("[L]", "0")
        strLastValuesString = strLastValuesString.Replace("[M]", "0")
        strLastValuesString = strLastValuesString.Replace("[N]", "0")
        strLastValuesString = strLastValuesString.Replace("[O]", "0")
        strLastValuesString = strLastValuesString.Replace("[P]", "0")
        strLastValuesString = strLastValuesString.Replace("[Q]", "0")
        strLastValuesString = strLastValuesString.Replace("[R]", "0")
        strLastValuesString = strLastValuesString.Replace("[S]", "0")
        strLastValuesString = strLastValuesString.Replace("[T]", "0")
        strLastValuesString = strLastValuesString.Replace("[U]", "0")
        strLastValuesString = strLastValuesString.Replace("[V]", "0")
        Return strLastValuesString
    End Function

    Private Function GetInsertValues(ByVal strValue As String, ByVal strReplacePattern As String, ByVal strLastValuesString As String) As String
        If strLastValuesString = "" Then
            strLastValuesString = "('','[SERIAL_CS]', '[MODE]', '[MC]', '[E]', '[F]', '[G]', '[H]', '[I]', '[J]', '[K]', '[L]', '[M]', '[N]', '[O]', '[P]', '[Q]', '[R]', '[S]', '[T]', '[U]', '[V]', '[DATE]', '[TIME]', '[STATUS]', '[SERIAL_BH]', '[SERIAL_TS]', '[SERIAL_CW]', '[SERIAL_ITA]')"
        End If
        If strReplacePattern = "" Then
            Return strLastValuesString
        End If

        strLastValuesString = strLastValuesString.Replace(strReplacePattern, strValue)
        Return strLastValuesString
    End Function

    Private Sub ButtonExit_Click(sender As System.Object, e As System.EventArgs) Handles ButtonExit.Click
        Me.Dispose()

    End Sub

    Private Sub ButtonResizeColumns_Click(sender As System.Object, e As System.EventArgs) Handles ButtonResizeColumns.Click
        If intColumnMode = 0 Then
            For Each col As DataGridViewColumn In DataGridView1.Columns
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader

            Next
            ButtonResizeColumns.Text = "ENLARGE columns"
            intColumnMode = 1
        ElseIf intColumnMode = 1 Then
            For Each col As DataGridViewColumn In DataGridView1.Columns
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader

            Next
            ButtonResizeColumns.Text = "MANUAL RESIZE columns"
            intColumnMode = 2

        ElseIf intColumnMode = 2 Then
            For Each col As DataGridViewColumn In DataGridView1.Columns
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            Next


            ButtonResizeColumns.Text = "MINIMIZE columns"
            intColumnMode = 0
        End If

        'If CheckBoxAutoResizeColumnIgnoreHead.Checked Then
        '    ' Me.DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        '    Me.DataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        'Else

        '    DataGridView1.ColumnHeadersHeight = 23
        '    Me.DataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False
        'End If


        

        DataGridView1.Refresh()

        'For Each col As DataGridViewColumn In DataGridView1.Columns
        '    If CheckBoxAutoResizeColumnIgnoreHead.Checked Then
        '        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        '    End If

        'Next

        DataGridView1.AllowUserToResizeColumns = True



    End Sub
End Class