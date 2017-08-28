<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResultHistory
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxSerial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonResizeColumns = New System.Windows.Forms.Button()
        Me.TextBoxMachine = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxLine = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxDateFrom = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxDateTo = New System.Windows.Forms.TextBox()
        Me.TextBoxLotNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxSearchBy = New System.Windows.Forms.TextBox()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.ButtonShowParts = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxTimes = New System.Windows.Forms.TextBox()
        Me.VResultHistoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New ihi_dfs_client.ctrlPivotGrid()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonFilter = New System.Windows.Forms.Button()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.ButtonExport_XLS = New System.Windows.Forms.Button()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.CheckBoxAutoResizeColumnIgnoreHead = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.VResultHistoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.YellowGreen
        Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1206, 29)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 9
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.88262!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.983655!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1206, 29)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "IHI - RHF5 - Result History"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 12
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.126055!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.270788!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.509759!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.343711!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxSerial, 5, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonResizeColumns, 11, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxMachine, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxLine, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxDateFrom, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label7, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label8, 4, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxDateTo, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxLotNo, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label9, 7, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label5, 7, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxSearchBy, 8, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxResult, 8, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ButtonShowParts, 6, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 9, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxTimes, 10, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 61)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1206, 64)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(461, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Serial:"
        '
        'TextBoxSerial
        '
        Me.TextBoxSerial.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxSerial.Location = New System.Drawing.Point(503, 6)
        Me.TextBoxSerial.Name = "TextBoxSerial"
        Me.TextBoxSerial.Size = New System.Drawing.Size(79, 20)
        Me.TextBoxSerial.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(246, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Machine:"
        '
        'ButtonResizeColumns
        '
        Me.ButtonResizeColumns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonResizeColumns.AutoSize = True
        Me.ButtonResizeColumns.Location = New System.Drawing.Point(1098, 0)
        Me.ButtonResizeColumns.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonResizeColumns.Name = "ButtonResizeColumns"
        Me.ButtonResizeColumns.Size = New System.Drawing.Size(108, 32)
        Me.ButtonResizeColumns.TabIndex = 96
        Me.ButtonResizeColumns.Text = "Resize Columns"
        Me.ButtonResizeColumns.UseVisualStyleBackColor = True
        '
        'TextBoxMachine
        '
        Me.TextBoxMachine.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxMachine.Location = New System.Drawing.Point(303, 6)
        Me.TextBoxMachine.Name = "TextBoxMachine"
        Me.TextBoxMachine.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxMachine.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(67, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Line:"
        '
        'TextBoxLine
        '
        Me.TextBoxLine.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxLine.Location = New System.Drawing.Point(103, 6)
        Me.TextBoxLine.Name = "TextBoxLine"
        Me.TextBoxLine.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxLine.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Date From:"
        '
        'TextBoxDateFrom
        '
        Me.TextBoxDateFrom.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxDateFrom.Location = New System.Drawing.Point(103, 38)
        Me.TextBoxDateFrom.Name = "TextBoxDateFrom"
        Me.TextBoxDateFrom.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxDateFrom.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(248, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Date To:"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(455, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Lot No:"
        '
        'TextBoxDateTo
        '
        Me.TextBoxDateTo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxDateTo.Location = New System.Drawing.Point(303, 38)
        Me.TextBoxDateTo.Name = "TextBoxDateTo"
        Me.TextBoxDateTo.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxDateTo.TabIndex = 3
        '
        'TextBoxLotNo
        '
        Me.TextBoxLotNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxLotNo.Location = New System.Drawing.Point(503, 38)
        Me.TextBoxLotNo.Name = "TextBoxLotNo"
        Me.TextBoxLotNo.Size = New System.Drawing.Size(79, 20)
        Me.TextBoxLotNo.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(755, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Result:"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(737, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Search by:"
        '
        'TextBoxSearchBy
        '
        Me.TextBoxSearchBy.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxSearchBy.Location = New System.Drawing.Point(801, 6)
        Me.TextBoxSearchBy.Name = "TextBoxSearchBy"
        Me.TextBoxSearchBy.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxSearchBy.TabIndex = 11
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxResult.Location = New System.Drawing.Point(801, 38)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxResult.TabIndex = 3
        '
        'ButtonShowParts
        '
        Me.ButtonShowParts.Location = New System.Drawing.Point(588, 35)
        Me.ButtonShowParts.Name = "ButtonShowParts"
        Me.ButtonShowParts.Size = New System.Drawing.Size(98, 23)
        Me.ButtonShowParts.TabIndex = 12
        Me.ButtonShowParts.Text = "Show Parts"
        Me.ButtonShowParts.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(957, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Times:"
        '
        'TextBoxTimes
        '
        Me.TextBoxTimes.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxTimes.Location = New System.Drawing.Point(1001, 38)
        Me.TextBoxTimes.Name = "TextBoxTimes"
        Me.TextBoxTimes.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxTimes.TabIndex = 3
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.90909!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.Controls.Add(Me.DataGridView1, 1, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 125)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.90909!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1206, 346)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.GroupedDuplicateCells = False
        Me.DataGridView1.HideDuplicateColumns = 0
        Me.DataGridView1.Location = New System.Drawing.Point(57, 18)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(1090, 308)
        Me.DataGridView1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonFilter, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonRefresh, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonExport_XLS, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonExit, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxStatus, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBoxAutoResizeColumnIgnoreHead, 4, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 29)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1206, 32)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'ButtonFilter
        '
        Me.ButtonFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFilter.AutoSize = True
        Me.ButtonFilter.Location = New System.Drawing.Point(0, 0)
        Me.ButtonFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonFilter.Name = "ButtonFilter"
        Me.ButtonFilter.Size = New System.Drawing.Size(160, 32)
        Me.ButtonFilter.TabIndex = 85
        Me.ButtonFilter.Text = "Filter"
        Me.ButtonFilter.UseVisualStyleBackColor = True
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresh.AutoSize = True
        Me.ButtonRefresh.Location = New System.Drawing.Point(160, 0)
        Me.ButtonRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(160, 32)
        Me.ButtonRefresh.TabIndex = 92
        Me.ButtonRefresh.Text = "Refresh"
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'ButtonExport_XLS
        '
        Me.ButtonExport_XLS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExport_XLS.AutoSize = True
        Me.ButtonExport_XLS.Location = New System.Drawing.Point(320, 0)
        Me.ButtonExport_XLS.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonExport_XLS.Name = "ButtonExport_XLS"
        Me.ButtonExport_XLS.Size = New System.Drawing.Size(160, 32)
        Me.ButtonExport_XLS.TabIndex = 95
        Me.ButtonExport_XLS.Text = "Export XLS"
        Me.ButtonExport_XLS.UseVisualStyleBackColor = True
        '
        'ButtonExit
        '
        Me.ButtonExit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExit.AutoSize = True
        Me.ButtonExit.Location = New System.Drawing.Point(480, 0)
        Me.ButtonExit.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(160, 32)
        Me.ButtonExit.TabIndex = 96
        Me.ButtonExit.Text = "Exit"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.Enabled = False
        Me.TextBoxStatus.Location = New System.Drawing.Point(803, 3)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.Size = New System.Drawing.Size(400, 20)
        Me.TextBoxStatus.TabIndex = 11
        Me.TextBoxStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxAutoResizeColumnIgnoreHead
        '
        Me.CheckBoxAutoResizeColumnIgnoreHead.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAutoResizeColumnIgnoreHead.AutoSize = True
        Me.CheckBoxAutoResizeColumnIgnoreHead.Checked = True
        Me.CheckBoxAutoResizeColumnIgnoreHead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAutoResizeColumnIgnoreHead.Location = New System.Drawing.Point(643, 3)
        Me.CheckBoxAutoResizeColumnIgnoreHead.Name = "CheckBoxAutoResizeColumnIgnoreHead"
        Me.CheckBoxAutoResizeColumnIgnoreHead.Size = New System.Drawing.Size(154, 26)
        Me.CheckBoxAutoResizeColumnIgnoreHead.TabIndex = 97
        Me.CheckBoxAutoResizeColumnIgnoreHead.Text = "Auto Resize Column"
        Me.CheckBoxAutoResizeColumnIgnoreHead.UseVisualStyleBackColor = True
        Me.CheckBoxAutoResizeColumnIgnoreHead.Visible = False
        '
        'frmResultHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1206, 471)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmResultHistory"
        Me.Text = "Result History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.VResultHistoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VResultHistoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents ButtonFilter As System.Windows.Forms.Button
    Friend WithEvents ButtonExport_XLS As System.Windows.Forms.Button
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As ihi_dfs_client.ctrlPivotGrid
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSearchBy As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMachine As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLine As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDateFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDateTo As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxResult As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLotNo As System.Windows.Forms.TextBox
    Friend WithEvents ButtonShowParts As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTimes As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxStatus As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxAutoResizeColumnIgnoreHead As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonResizeColumns As System.Windows.Forms.Button
End Class
