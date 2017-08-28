﻿Public Class frmFilter
    'Private Sub AddLabelTextbox_to_Table(tbl1 As TableLayoutPanel, strLable As String, strName As String, intRow As Integer, intCol As Integer)
    '    tbl1.Controls.Add(New Label() With {.Text = strLable, .Anchor = AnchorStyles.Right, .AutoSize = True}, intRow, intCol)
    '    'tbl1.Controls.Add(New ComboBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)
    '    tbl1.Controls.Add(New TextBox() With {.Name = strName, .Dock = DockStyle.Fill, .Anchor = AnchorStyles.Left}, intRow + 1, intCol)
    'End Sub

    'Private Sub AddLabelRadiobutton_to_Table(tbl1 As TableLayoutPanel, strLable As String, strName As String, strChoices As String, intRow As Integer, intCol As Integer)
    '    tbl1.Controls.Add(New Label() With {.Text = strLable, .Anchor = AnchorStyles.Right, .AutoSize = True}, intRow, intCol)
    '    'tbl1.Controls.Add(New ComboBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)

    '    'tbl1.RowStyles.Add(New RowStyle(SizeType.AutoSize))
    '    'tbl1.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

    '    Dim strButtons As String() = strChoices.Split("|")

    '    Dim newTable As TableLayoutPanel = New TableLayoutPanel() With {.Name = strName, .Dock = DockStyle.Fill, .Anchor = AnchorStyles.Left}
    '    'Dim newTable As TableLayoutPanel = New TableLayoutPanel() With {.Anchor = AnchorStyles.Left}

    '    tbl1.Controls.Add(newTable, intRow + 1, intCol)

    '    newTable.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

    '    'newTable.RowStyles.Add(New RowStyle(SizeType.AutoSize))



    '    For Each strBtn In strButtons
    '        'newTable.RowCount += 1
    '        newTable.ColumnCount += 1
    '        newTable.Controls.Add(New RadioButton() With {.Name = strBtn, .Text = strBtn, .Size = New System.Drawing.Size(50, 30), .Dock = DockStyle.Fill, .Anchor = AnchorStyles.Left, .AutoSize = True}, newTable.RowCount - 1, 0)
    '        'newTable.Controls.Add(New RadioButton() With {.Text = strBtn, .Size = New System.Drawing.Size(50, 30), .Dock = DockStyle.Fill, .Anchor = AnchorStyles.Left, .AutoSize = True}, 0, newTable.RowCount - 1)
    '    Next

    '    'tbl1.AutoSize = True
    '    'tbl1.RowStyles.Clear()

    '    'newTable.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

    '    'newTable.RowStyles.Add(New RowStyle(SizeType.AutoSize))

    '    newTable.AutoSize = True
    '    'tbl1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset

    'End Sub

    'Public currentLine As String = ""

    Private Sub AddControls()
        'Dim textbox1 As New TextBox
        'textbox1.Name = "Textbox1"
        'textbox1.Size = New Size(170, 20)
        'textbox1.Location = New Point(167, 32)
        'Me.TableLayoutPanel2.Controls.Add(textbox1, 0, 0)

        'Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        'Me.TableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset

        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Line:", "Line", 0, 0)
        'AddLabelRadiobutton_to_Table(Me.TableLayoutPanel2, "Line:", "Line", "B/H|C/S", 0, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Machine:", "Machine", 2, 0)
        'AddLabelRadiobutton_to_Table(Me.TableLayoutPanel2, "Search By:", "SearchBy", "B/H|C/S|ITA", 4, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Serial:", "Serial", 6, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Lot No:", "LotNo", 8, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Date From:", "DateFrom", 10, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Date To:", "DateTo", 12, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel2, "Result:", "Result", 14, 0)

    End Sub

    Private Sub SetControls()


        If dicSearch("Date From") = "" Then
            'dicSearch("Date From") = DateTime.Now.ToString("yyyyMMdd")
            dicSearch("Date From") = DateTime.Now.ToString(SCREEN_DATE_FORMAT)
        End If

        If dicSearch("Date To") = "" Then
            dicSearch("Date To") = DateTime.Now.ToString(SCREEN_DATE_FORMAT)

        End If


        'Me.TableLayoutPanel2.Controls("Line").Text = dicSearch("Line")
        'Me.TableLayoutPanel2.Controls("Machine").Text = dicSearch("Machine")
        ''Me.TableLayoutPanel2.Controls("SearchBy").Text = dicSearch("Search By")
        'Me.TableLayoutPanel2.Controls("Serial").Text = dicSearch("Serial")
        'Me.TableLayoutPanel2.Controls("LotNo").Text = dicSearch("Lot No")
        'Me.TableLayoutPanel2.Controls("DateFrom").Text = dicSearch("Date From")
        'Me.TableLayoutPanel2.Controls("DateTo").Text = dicSearch("Date To")
        'Me.TableLayoutPanel2.Controls("Result").Text = dicSearch("Result")

        'TextBoxLine.Text = dicSearch("Line")
        'TextBoxMachine.Text = dicSearch("Machine")
        ComboBoxMachine.Text = dicSearch("Machine")



        ComboBoxSerial.Text = dicSearch("Serial")
        TextBoxLotNo.Text = dicSearch("Lot No")

        'TO DO: set to last value
        'DateTimePickerDateFrom.Text = dicSearch("Date From")
        'DateTimePickerDateTo.Text = dicSearch("Date To")


        DateTimePickerDateFrom.Value = DateTime.ParseExact(dicSearch("Date From"), SCREEN_DATE_FORMAT, _
                                           System.Globalization.CultureInfo.InvariantCulture)
        'DateTimePickerDateFrom.Value = Now.AddDays(-5)
        'DateTimePickerDateFrom.Value = DateTimePickerDateFrom.Value.AddDays(-6)
        DateTimePickerDateTo.Value = DateTime.ParseExact(dicSearch("Date To"), SCREEN_DATE_FORMAT, _
                                           System.Globalization.CultureInfo.InvariantCulture)

        'DateTimePicker.da()

        'DateTimePickerDateFrom.Select()


        DateTimePickerDateFrom.Text = DateTime.ParseExact(dicSearch("Date From"), SCREEN_DATE_FORMAT, _
                                           System.Globalization.CultureInfo.InvariantCulture)
        DateTimePickerDateTo.Text = DateTime.ParseExact(dicSearch("Date To"), SCREEN_DATE_FORMAT, _
                                           System.Globalization.CultureInfo.InvariantCulture)

        DateTimePickerDateFrom.Update()
        DateTimePickerDateTo.Update()



        DateTimePickerDateFrom.Refresh()
        DateTimePickerDateTo.Refresh()




        'Set combobox
        ComboBoxResult.ResetText()





        ComboBoxResult.Items.Clear()
        ComboBoxResult.Items.Add("ALL")
        ComboBoxResult.Items.Add("OK")
        ComboBoxResult.Items.Add("NG")

        'For Each r As String In "ALL|OK|NG".Split("|")
        '    ComboBoxResult.Items.Add(r)
        'Next

        ComboBoxResult.Refresh()


        If ComboBoxResult.Items.Contains(dicSearch("Result")) Then
            ComboBoxResult.Text = dicSearch("Result")
        Else
            ComboBoxResult.Text = "ALL"
        End If




        'Dim checkedRadioButton1 As RadioButton = Me.TableLayoutPanel2.Controls("Line").Controls(dicSearch("Line"))


        For Each ctrl In TableLayoutPanelLine.Controls
            If ctrl.Text = dicSearch("Line") Then
                ctrl.Checked = True
                Exit For
            End If
        Next

        For Each ctrl In TableLayoutPanelSearchBy.Controls
            If ctrl.Text = dicSearch("Search By") Then
                ctrl.Checked = True
                Exit For
            End If
        Next

        'Dim checkedRadioButton1 As RadioButton = TableLayoutPanelLine.Controls(dicSearch("Line"))
        'If checkedRadioButton1 IsNot Nothing Then
        '    checkedRadioButton1.Checked = True
        'End If

        'Dim checkedRadioButton2 As RadioButton = Me.TableLayoutPanel2.Controls("SearchBy").Controls(dicSearch("Search By"))
        'Dim checkedRadioButton2 As RadioButton = TableLayoutPanelSearchBy.Controls(dicSearch("Search By"))
        'If checkedRadioButton2 IsNot Nothing Then
        '    checkedRadioButton2.Checked = True
        'End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DateTimePickerDateFrom.CustomFormat = SCREEN_DATE_FORMAT
        DateTimePickerDateFrom.Format = DateTimePickerFormat.Custom

        DateTimePickerDateTo.CustomFormat = SCREEN_DATE_FORMAT
        DateTimePickerDateTo.Format = DateTimePickerFormat.Custom

        AddControls()
        'SetupDataObj()
        SetControls()


    End Sub

    Private Sub ButtonOK_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOK.Click
        Try


            Dim checkedRadioButton1 As RadioButton
            'checkedRadioButton1 = Me.TableLayoutPanel2.Controls("Line").Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)
            checkedRadioButton1 = Me.TableLayoutPanelLine.Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)

            Dim checkedRadioButton2 As RadioButton
            'checkedRadioButton2 = Me.TableLayoutPanel2.Controls("SearchBy").Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)
            checkedRadioButton2 = Me.TableLayoutPanelSearchBy.Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)


            If checkedRadioButton1 Is Nothing Then
                MsgBox("Please select ""Line""")
                Exit Sub
            End If


            'If Me.TableLayoutPanel2.Controls("DateFrom").Text = "" Then
            '    MsgBox("Please select ""Date From""")
            '    Exit Sub
            'End If

            'If Me.TableLayoutPanel2.Controls("DateTo").Text = "" Then
            '    MsgBox("Please select ""Date To""")
            '    Exit Sub
            'End If

            If Me.DateTimePickerDateFrom.Text = "" Then
                MsgBox("Please select ""Date From""")
                Exit Sub
            End If

            If Me.DateTimePickerDateTo.Text = "" Then
                MsgBox("Please select ""Date To""")
                Exit Sub
            End If

            If (Me.ComboBoxSerial.Text <> "" Or Me.TextBoxLotNo.Text <> "") AndAlso checkedRadioButton2 Is Nothing Then
                MsgBox("Please select ""Search By""")
                Exit Sub
            End If

            ''dicSearch("Line") = Me.TableLayoutPanel2.Controls("Line").Text
            'dicSearch("Line") = checkedRadioButton1.Text
            'dicSearch("Machine") = Me.TableLayoutPanel2.Controls("Machine").Text
            ''dicSearch("Search By") = Me.TableLayoutPanel2.Controls("Search By").Text
            'dicSearch("Serial") = Me.TableLayoutPanel2.Controls("Serial").Text
            'dicSearch("Lot No") = Me.TableLayoutPanel2.Controls("LotNo").Text
            'dicSearch("Date From") = Me.TableLayoutPanel2.Controls("DateFrom").Text
            'dicSearch("Date To") = Me.TableLayoutPanel2.Controls("DateTo").Text
            'dicSearch("Result") = Me.TableLayoutPanel2.Controls("Result").Text

            dicSearch("Line") = checkedRadioButton1.Text

            'dicSearch("Machine") = Me.TextBoxMachine.Text
            dicSearch("Machine") = Me.ComboBoxMachine.Text


            dicSearch("Serial") = Me.ComboBoxSerial.Text
            dicSearch("Lot No") = Me.TextBoxLotNo.Text
            'dicSearch("Date From") = Me.DateTimePickerDateFrom.Text
            'dicSearch("Date To") = Me.DateTimePickerDateTo.Text

            Me.DateTimePickerDateFrom.Update()

            Me.DateTimePickerDateTo.Update()

            If Me.DateTimePickerDateFrom.Enabled Then
                dicSearch("Date From") = Me.DateTimePickerDateFrom.Value.ToString(SCREEN_DATE_FORMAT)
            Else
                dicSearch("Date From") = ""
            End If

            If Me.DateTimePickerDateTo.Enabled Then
                dicSearch("Date To") = Me.DateTimePickerDateTo.Value.ToString(SCREEN_DATE_FORMAT)
            Else
                dicSearch("Date To") = ""
            End If

            dicSearch("Times") = Me.ComboBoxTimes.Text
            dicSearch("Result") = Me.ComboBoxResult.Text

            If checkedRadioButton2 IsNot Nothing Then
                dicSearch("Search By") = checkedRadioButton2.Text
            Else
                dicSearch("Search By") = ""
            End If




            'Me.ParentForm.TableLayoutPanel2.Controls("")
            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonCANCEL_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCANCEL.Click
        Try
            Me.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    

    Private Sub SetListbox(ByVal intLineId As Integer)

        'Dim checkedRadioButton1 As RadioButton
        ''checkedRadioButton1 = Me.TableLayoutPanel2.Controls("Line").Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)
        'checkedRadioButton1 = Me.TableLayoutPanelLine.Controls.OfType(Of RadioButton)().FirstOrDefault(Function(r) r.Checked = True)

        'If currentLine = checkedRadioButton1.Text Then
        '    Exit Sub
        'End If

        'currentLine = checkedRadioButton1.Text

        Dim dt As ihi_rhf5_developmentDataSet.MACHINEDataTable = New ihi_rhf5_developmentDataSet.MACHINEDataTable

        ComboBoxMachine.ResetText()


        'ComboBoxMachine.DataSource = Nothing

        'ComboBoxMachine.DataSource = dt
        'ComboBoxMachine.DisplayMember = "MACHINE_NAME"
        'ComboBoxMachine.ValueMember = "MACHINE_NAME"


        ComboBoxMachine.Items.Clear()
        ComboBoxMachine.Items.Add("ALL")

        If intLineId <> -1 Then
            da_MACHINE.FillByLineId(dt, intLineId)
            For Each r As ihi_rhf5_developmentDataSet.MACHINERow In dt.Rows
                ComboBoxMachine.Items.Add(r.MACHINE_NAME)
            Next
        End If


        

        

        ComboBoxMachine.Refresh()


        If ComboBoxMachine.Items.Contains(dicSearch("Machine")) Then
            ComboBoxMachine.Text = dicSearch("Machine")
        Else
            ComboBoxMachine.Text = "ALL"
        End If



        '-- Set ComboBoxTimes
        ComboBoxTimes.Items.Clear()
        ComboBoxTimes.Items.Add("ALL")
        ComboBoxTimes.Items.Add("08:01 - 12:00")
        ComboBoxTimes.Items.Add("12:01 - 17:00")
        ComboBoxTimes.Items.Add("17:01 - 22:00")
        ComboBoxTimes.Items.Add("22:01 - 08:00")

        ComboBoxMachine.Refresh()


        If ComboBoxTimes.Items.Contains(dicSearch("Times")) Then
            ComboBoxTimes.Text = dicSearch("Times")
        Else
            ComboBoxTimes.Text = "ALL"
        End If



    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonLineCS.CheckedChanged
        Try
            If RadioButtonLineCS.Checked Then
                SetListbox(dicMapLineId(RadioButtonLineCS.Text))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonLineBH.CheckedChanged
        Try
            If RadioButtonLineBH.Checked Then
                SetListbox(dicMapLineId(RadioButtonLineBH.Text))
            End If

        Catch ex As Exception

        End Try



    End Sub

    'Private Sub RadioButtonSearchByBH_Click(sender As System.Object, e As System.EventArgs) Handles RadioButtonSearchByBH.Click
    '    Try
    '        'SetComboBoxSerial(1)
    '    Catch ex As Exception

    '    End Try

    'End Sub



    'Private Sub RadioButtonSearchByCS_Click(sender As System.Object, e As System.EventArgs) Handles RadioButtonSearchByCS.Click
    '    Try
    '        'SetComboBoxSerial(3)
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub RadioButtonSearchByITA_Click(sender As System.Object, e As System.EventArgs) Handles RadioButtonSearchByITA.Click
    '    Try
    '        ' SetComboBoxSerial(4)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub RadioButtonSearchByHB_Click(sender As System.Object, e As System.EventArgs) Handles RadioButtonSearchByHB.Click
    '    Try
    '        'SetComboBoxSerial(6)
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Sub SetComboBoxSerial(ByVal intColumnIndexInDb As Integer)
        Dim dt As ihi_rhf5_developmentDataSet.SERIALDataTable = New ihi_rhf5_developmentDataSet.SERIALDataTable


        da_SERIAL.Fill(dt)

        ComboBoxSerial.ResetText()
        ComboBoxSerial.Items.Clear()
        ComboBoxSerial.Items.Add("ALL")

        For Each r As ihi_rhf5_developmentDataSet.SERIALRow In dt.Rows
            'ComboBoxSerial.Items.Add(r.SERIAL_BH)
            If r.Item(intColumnIndexInDb) <> "" Then
                ComboBoxSerial.Items.Add(r.Item(intColumnIndexInDb))
            End If

        Next

        ComboBoxSerial.Refresh()


        If ComboBoxSerial.Items.Contains(dicSearch("Machine")) Then
            ComboBoxSerial.Text = dicSearch("Machine")
        Else
            ComboBoxSerial.Text = "ALL"
        End If
    End Sub

    Private Sub ComboBoxMachine_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxMachine.SelectedIndexChanged
        MachineSelected()
    End Sub

    Private Sub MachineSelected()
        'If Not RadioLineAll.Checked Then
        If ComboBoxMachine.Text = "" Or (ComboBoxMachine.Text.ToLower = "all") Then
            Me.DateTimePickerDateFrom.Enabled = False
            Me.DateTimePickerDateTo.Enabled = False
            Me.ComboBoxResult.Enabled = False
            Me.ComboBoxTimes.Enabled = False
        Else
            Me.DateTimePickerDateFrom.Enabled = True
            Me.DateTimePickerDateTo.Enabled = True
            Me.ComboBoxResult.Enabled = True
            Me.ComboBoxTimes.Enabled = True
        End If
        'Else
        '    ComboBoxMachine.Items.Clear()
        '    ComboBoxMachine.Items.Add("ALL")
        '    ComboBoxMachine.Text = "ALL"
        '    Me.DateTimePickerDateFrom.Enabled = False
        '    Me.DateTimePickerDateTo.Enabled = False
        '    Me.ComboBoxResult.Enabled = False
        '    Me.ComboBoxTimes.Enabled = False
        'End If

    End Sub

    Private Sub frmFilter_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'RadioLineAll.Checked = True
        MachineSelected()
    End Sub

    Private Sub RadioButtonLineAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButtonLineAll.CheckedChanged
        Try
            If RadioButtonLineAll.Checked Then
                SetListbox(-1)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class