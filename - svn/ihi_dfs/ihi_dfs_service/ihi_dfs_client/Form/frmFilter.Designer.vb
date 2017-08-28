<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanelLine = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButtonLineBH = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLineCS = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLineAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxLotNo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanelSearchBy = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButtonSearchByBH = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSearchByCS = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSearchByITA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSearchByHB = New System.Windows.Forms.RadioButton()
        Me.ComboBoxMachine = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerDateTo = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBoxTimes = New System.Windows.Forms.ComboBox()
        Me.ComboBoxSerial = New System.Windows.Forms.ComboBox()
        Me.ComboBoxResult = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCANCEL = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanelLine.SuspendLayout()
        Me.TableLayoutPanelSearchBy.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(455, 535)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.YellowGreen
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(455, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IHI - RHF5 - Filter"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanelLine, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBoxLotNo, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanelSearchBy, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.ComboBoxMachine, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.DateTimePickerDateTo, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.DateTimePickerDateFrom, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label9, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.Label10, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.ComboBoxTimes, 1, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.ComboBoxSerial, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.ComboBoxResult, 1, 8)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 33)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 10
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(449, 416)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanelLine
        '
        Me.TableLayoutPanelLine.ColumnCount = 3
        Me.TableLayoutPanelLine.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelLine.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelLine.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelLine.Controls.Add(Me.RadioButtonLineBH, 0, 0)
        Me.TableLayoutPanelLine.Controls.Add(Me.RadioButtonLineCS, 1, 0)
        Me.TableLayoutPanelLine.Controls.Add(Me.RadioButtonLineAll, 2, 0)
        Me.TableLayoutPanelLine.Location = New System.Drawing.Point(137, 3)
        Me.TableLayoutPanelLine.Name = "TableLayoutPanelLine"
        Me.TableLayoutPanelLine.RowCount = 1
        Me.TableLayoutPanelLine.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelLine.Size = New System.Drawing.Size(213, 41)
        Me.TableLayoutPanelLine.TabIndex = 8
        '
        'RadioButtonLineBH
        '
        Me.RadioButtonLineBH.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonLineBH.AutoSize = True
        Me.RadioButtonLineBH.Location = New System.Drawing.Point(3, 12)
        Me.RadioButtonLineBH.Name = "RadioButtonLineBH"
        Me.RadioButtonLineBH.Size = New System.Drawing.Size(48, 17)
        Me.RadioButtonLineBH.TabIndex = 0
        Me.RadioButtonLineBH.TabStop = True
        Me.RadioButtonLineBH.Text = "B/H"
        Me.RadioButtonLineBH.UseVisualStyleBackColor = True
        '
        'RadioButtonLineCS
        '
        Me.RadioButtonLineCS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonLineCS.AutoSize = True
        Me.RadioButtonLineCS.Location = New System.Drawing.Point(74, 12)
        Me.RadioButtonLineCS.Name = "RadioButtonLineCS"
        Me.RadioButtonLineCS.Size = New System.Drawing.Size(47, 17)
        Me.RadioButtonLineCS.TabIndex = 0
        Me.RadioButtonLineCS.TabStop = True
        Me.RadioButtonLineCS.Text = "C/S"
        Me.RadioButtonLineCS.UseVisualStyleBackColor = True
        '
        'RadioButtonLineAll
        '
        Me.RadioButtonLineAll.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonLineAll.AutoSize = True
        Me.RadioButtonLineAll.Location = New System.Drawing.Point(145, 12)
        Me.RadioButtonLineAll.Name = "RadioButtonLineAll"
        Me.RadioButtonLineAll.Size = New System.Drawing.Size(47, 17)
        Me.RadioButtonLineAll.TabIndex = 0
        Me.RadioButtonLineAll.TabStop = True
        Me.RadioButtonLineAll.Text = "ALL"
        Me.RadioButtonLineAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(96, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Line:"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Machine:"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(62, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Search By:"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(88, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Serial:"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(82, 205)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Lot No:"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(62, 252)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Date From:"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(74, 299)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Date To:"
        '
        'TextBoxLotNo
        '
        Me.TextBoxLotNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBoxLotNo.Location = New System.Drawing.Point(137, 201)
        Me.TextBoxLotNo.Name = "TextBoxLotNo"
        Me.TextBoxLotNo.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxLotNo.TabIndex = 6
        '
        'TableLayoutPanelSearchBy
        '
        Me.TableLayoutPanelSearchBy.ColumnCount = 4
        Me.TableLayoutPanelSearchBy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelSearchBy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelSearchBy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelSearchBy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelSearchBy.Controls.Add(Me.RadioButtonSearchByBH, 0, 0)
        Me.TableLayoutPanelSearchBy.Controls.Add(Me.RadioButtonSearchByCS, 1, 0)
        Me.TableLayoutPanelSearchBy.Controls.Add(Me.RadioButtonSearchByITA, 2, 0)
        Me.TableLayoutPanelSearchBy.Controls.Add(Me.RadioButtonSearchByHB, 3, 0)
        Me.TableLayoutPanelSearchBy.Location = New System.Drawing.Point(137, 97)
        Me.TableLayoutPanelSearchBy.Name = "TableLayoutPanelSearchBy"
        Me.TableLayoutPanelSearchBy.RowCount = 1
        Me.TableLayoutPanelSearchBy.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelSearchBy.Size = New System.Drawing.Size(259, 41)
        Me.TableLayoutPanelSearchBy.TabIndex = 7
        '
        'RadioButtonSearchByBH
        '
        Me.RadioButtonSearchByBH.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSearchByBH.AutoSize = True
        Me.RadioButtonSearchByBH.Location = New System.Drawing.Point(3, 12)
        Me.RadioButtonSearchByBH.Name = "RadioButtonSearchByBH"
        Me.RadioButtonSearchByBH.Size = New System.Drawing.Size(48, 17)
        Me.RadioButtonSearchByBH.TabIndex = 0
        Me.RadioButtonSearchByBH.TabStop = True
        Me.RadioButtonSearchByBH.Text = "B/H"
        Me.RadioButtonSearchByBH.UseVisualStyleBackColor = True
        '
        'RadioButtonSearchByCS
        '
        Me.RadioButtonSearchByCS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSearchByCS.AutoSize = True
        Me.RadioButtonSearchByCS.Location = New System.Drawing.Point(67, 12)
        Me.RadioButtonSearchByCS.Name = "RadioButtonSearchByCS"
        Me.RadioButtonSearchByCS.Size = New System.Drawing.Size(47, 17)
        Me.RadioButtonSearchByCS.TabIndex = 0
        Me.RadioButtonSearchByCS.TabStop = True
        Me.RadioButtonSearchByCS.Text = "C/S"
        Me.RadioButtonSearchByCS.UseVisualStyleBackColor = True
        '
        'RadioButtonSearchByITA
        '
        Me.RadioButtonSearchByITA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSearchByITA.AutoSize = True
        Me.RadioButtonSearchByITA.Location = New System.Drawing.Point(131, 12)
        Me.RadioButtonSearchByITA.Name = "RadioButtonSearchByITA"
        Me.RadioButtonSearchByITA.Size = New System.Drawing.Size(45, 17)
        Me.RadioButtonSearchByITA.TabIndex = 0
        Me.RadioButtonSearchByITA.TabStop = True
        Me.RadioButtonSearchByITA.Text = "ITA"
        Me.RadioButtonSearchByITA.UseVisualStyleBackColor = True
        '
        'RadioButtonSearchByHB
        '
        Me.RadioButtonSearchByHB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadioButtonSearchByHB.AutoSize = True
        Me.RadioButtonSearchByHB.Location = New System.Drawing.Point(195, 12)
        Me.RadioButtonSearchByHB.Name = "RadioButtonSearchByHB"
        Me.RadioButtonSearchByHB.Size = New System.Drawing.Size(48, 17)
        Me.RadioButtonSearchByHB.TabIndex = 0
        Me.RadioButtonSearchByHB.TabStop = True
        Me.RadioButtonSearchByHB.Text = "H/B"
        Me.RadioButtonSearchByHB.UseVisualStyleBackColor = True
        Me.RadioButtonSearchByHB.Visible = False
        '
        'ComboBoxMachine
        '
        Me.ComboBoxMachine.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBoxMachine.FormattingEnabled = True
        Me.ComboBoxMachine.Location = New System.Drawing.Point(137, 60)
        Me.ComboBoxMachine.Name = "ComboBoxMachine"
        Me.ComboBoxMachine.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxMachine.TabIndex = 9
        '
        'DateTimePickerDateTo
        '
        Me.DateTimePickerDateTo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DateTimePickerDateTo.Checked = False
        Me.DateTimePickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerDateTo.Location = New System.Drawing.Point(137, 295)
        Me.DateTimePickerDateTo.Name = "DateTimePickerDateTo"
        Me.DateTimePickerDateTo.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePickerDateTo.TabIndex = 10
        '
        'DateTimePickerDateFrom
        '
        Me.DateTimePickerDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DateTimePickerDateFrom.Checked = False
        Me.DateTimePickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerDateFrom.Location = New System.Drawing.Point(137, 248)
        Me.DateTimePickerDateFrom.Name = "DateTimePickerDateFrom"
        Me.DateTimePickerDateFrom.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePickerDateFrom.TabIndex = 10
        Me.DateTimePickerDateFrom.Value = New Date(2016, 6, 10, 15, 17, 24, 0)
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(84, 379)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Result:"
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(87, 346)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Times:"
        '
        'ComboBoxTimes
        '
        Me.ComboBoxTimes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBoxTimes.FormattingEnabled = True
        Me.ComboBoxTimes.Location = New System.Drawing.Point(137, 342)
        Me.ComboBoxTimes.Name = "ComboBoxTimes"
        Me.ComboBoxTimes.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxTimes.TabIndex = 9
        '
        'ComboBoxSerial
        '
        Me.ComboBoxSerial.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBoxSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.ComboBoxSerial.FormattingEnabled = True
        Me.ComboBoxSerial.Location = New System.Drawing.Point(137, 154)
        Me.ComboBoxSerial.Name = "ComboBoxSerial"
        Me.ComboBoxSerial.Size = New System.Drawing.Size(200, 21)
        Me.ComboBoxSerial.TabIndex = 9
        '
        'ComboBoxResult
        '
        Me.ComboBoxResult.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ComboBoxResult.FormattingEnabled = True
        Me.ComboBoxResult.Location = New System.Drawing.Point(137, 379)
        Me.ComboBoxResult.Name = "ComboBoxResult"
        Me.ComboBoxResult.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxResult.TabIndex = 9
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonOK, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonCANCEL, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 455)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(449, 77)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonOK.Location = New System.Drawing.Point(72, 18)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(80, 40)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCANCEL
        '
        Me.ButtonCANCEL.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonCANCEL.Location = New System.Drawing.Point(295, 18)
        Me.ButtonCANCEL.Name = "ButtonCANCEL"
        Me.ButtonCANCEL.Size = New System.Drawing.Size(83, 40)
        Me.ButtonCANCEL.TabIndex = 0
        Me.ButtonCANCEL.Text = "CANCEL"
        Me.ButtonCANCEL.UseVisualStyleBackColor = True
        '
        'frmFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 535)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmFilter"
        Me.Text = "Filter Condition"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanelLine.ResumeLayout(False)
        Me.TableLayoutPanelLine.PerformLayout()
        Me.TableLayoutPanelSearchBy.ResumeLayout(False)
        Me.TableLayoutPanelSearchBy.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonCANCEL As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLotNo As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanelSearchBy As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButtonSearchByBH As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSearchByCS As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSearchByITA As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanelLine As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButtonLineBH As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonLineCS As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxMachine As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTimes As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxSerial As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonSearchByHB As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxResult As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonLineAll As System.Windows.Forms.RadioButton
End Class
