<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSOperation
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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtProductionLot = New System.Windows.Forms.TextBox()
        Me.txtCustomerLot = New System.Windows.Forms.TextBox()
        Me.txtLotQTY = New System.Windows.Forms.TextBox()
        Me.txtCurrentQTY = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnRework = New System.Windows.Forms.Button()
        Me.btnStartLot = New System.Windows.Forms.Button()
        Me.btnEndLot = New System.Windows.Forms.Button()
        Me.btnEndRework = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PART = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PART_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOT1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOT2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOT3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOTPARTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Ihi_rhf5 = New ihi_rhf5_CS_Operation.ihi_rhf5()
        Me.LOT_PARTTableAdapter = New ihi_rhf5_CS_Operation.ihi_rhf5TableAdapters.LOT_PARTTableAdapter()
        Me.BOMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BOMTableAdapter = New ihi_rhf5_CS_Operation.ihi_rhf5TableAdapters.BOMTableAdapter()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtPlcReady = New System.Windows.Forms.TextBox()
        Me.tmrButton = New System.Windows.Forms.Timer(Me.components)
        Me.lbResult = New System.Windows.Forms.Label()
        Me.txtPartBarcode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LOTPARTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ihi_rhf5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BOMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(54, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "*Production Lot: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(61, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "*Customer Lot: "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(89, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "*Lot Qty: "
        '
        'txtProductionLot
        '
        Me.txtProductionLot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductionLot.Location = New System.Drawing.Point(146, 22)
        Me.txtProductionLot.MaxLength = 20
        Me.txtProductionLot.Name = "txtProductionLot"
        Me.txtProductionLot.Size = New System.Drawing.Size(173, 20)
        Me.txtProductionLot.TabIndex = 0
        '
        'txtCustomerLot
        '
        Me.txtCustomerLot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerLot.Location = New System.Drawing.Point(146, 48)
        Me.txtCustomerLot.Name = "txtCustomerLot"
        Me.txtCustomerLot.Size = New System.Drawing.Size(173, 20)
        Me.txtCustomerLot.TabIndex = 4
        '
        'txtLotQTY
        '
        Me.txtLotQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLotQTY.Location = New System.Drawing.Point(146, 74)
        Me.txtLotQTY.MaxLength = 4
        Me.txtLotQTY.Name = "txtLotQTY"
        Me.txtLotQTY.Size = New System.Drawing.Size(173, 20)
        Me.txtLotQTY.TabIndex = 1
        Me.txtLotQTY.Text = "0"
        Me.txtLotQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCurrentQTY
        '
        Me.txtCurrentQTY.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtCurrentQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrentQTY.Enabled = False
        Me.txtCurrentQTY.Location = New System.Drawing.Point(434, 74)
        Me.txtCurrentQTY.Name = "txtCurrentQTY"
        Me.txtCurrentQTY.Size = New System.Drawing.Size(84, 20)
        Me.txtCurrentQTY.TabIndex = 7
        Me.txtCurrentQTY.Text = "0"
        Me.txtCurrentQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(356, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Current QTY: "
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.LightGray
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(351, 46)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(154, 23)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "EDIT"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnRework
        '
        Me.btnRework.BackColor = System.Drawing.Color.LightGray
        Me.btnRework.Enabled = False
        Me.btnRework.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRework.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnRework.Location = New System.Drawing.Point(542, 45)
        Me.btnRework.Name = "btnRework"
        Me.btnRework.Size = New System.Drawing.Size(154, 23)
        Me.btnRework.TabIndex = 5
        Me.btnRework.Text = "REWORK"
        Me.btnRework.UseVisualStyleBackColor = False
        '
        'btnStartLot
        '
        Me.btnStartLot.BackColor = System.Drawing.Color.LightGray
        Me.btnStartLot.Enabled = False
        Me.btnStartLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnStartLot.Location = New System.Drawing.Point(542, 19)
        Me.btnStartLot.Name = "btnStartLot"
        Me.btnStartLot.Size = New System.Drawing.Size(154, 23)
        Me.btnStartLot.TabIndex = 3
        Me.btnStartLot.Text = "START LOT."
        Me.btnStartLot.UseVisualStyleBackColor = False
        '
        'btnEndLot
        '
        Me.btnEndLot.BackColor = System.Drawing.Color.LightGray
        Me.btnEndLot.Enabled = False
        Me.btnEndLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEndLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEndLot.Location = New System.Drawing.Point(732, 19)
        Me.btnEndLot.Name = "btnEndLot"
        Me.btnEndLot.Size = New System.Drawing.Size(154, 23)
        Me.btnEndLot.TabIndex = 4
        Me.btnEndLot.Text = "END LOT."
        Me.btnEndLot.UseVisualStyleBackColor = False
        '
        'btnEndRework
        '
        Me.btnEndRework.BackColor = System.Drawing.Color.LightGray
        Me.btnEndRework.Enabled = False
        Me.btnEndRework.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEndRework.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEndRework.Location = New System.Drawing.Point(732, 45)
        Me.btnEndRework.Name = "btnEndRework"
        Me.btnEndRework.Size = New System.Drawing.Size(154, 23)
        Me.btnEndRework.TabIndex = 6
        Me.btnEndRework.Text = "END REWORK"
        Me.btnEndRework.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NO, Me.PART, Me.PART_NAME, Me.LOT1, Me.QTY1, Me.LOT2, Me.QTY2, Me.LOT3, Me.QTY3, Me._DATE, Me.TIME})
        Me.DataGridView1.Location = New System.Drawing.Point(42, 135)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.DataGridView1.Size = New System.Drawing.Size(1277, 403)
        Me.DataGridView1.TabIndex = 14
        '
        'NO
        '
        Me.NO.DataPropertyName = "SEQ"
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NO.DefaultCellStyle = DataGridViewCellStyle12
        Me.NO.Frozen = True
        Me.NO.HeaderText = "No."
        Me.NO.Name = "NO"
        Me.NO.ReadOnly = True
        Me.NO.Width = 50
        '
        'PART
        '
        Me.PART.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PART.DataPropertyName = "PART_NO"
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PART.DefaultCellStyle = DataGridViewCellStyle13
        Me.PART.FillWeight = 40.0!
        Me.PART.HeaderText = "PART*"
        Me.PART.Name = "PART"
        Me.PART.ReadOnly = True
        '
        'PART_NAME
        '
        Me.PART_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PART_NAME.DataPropertyName = "PART_NAME"
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PART_NAME.DefaultCellStyle = DataGridViewCellStyle14
        Me.PART_NAME.HeaderText = "PART NAME"
        Me.PART_NAME.Name = "PART_NAME"
        Me.PART_NAME.ReadOnly = True
        '
        'LOT1
        '
        Me.LOT1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LOT1.FillWeight = 80.0!
        Me.LOT1.HeaderText = "LOT 1"
        Me.LOT1.Name = "LOT1"
        '
        'QTY1
        '
        Me.QTY1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N0"
        DataGridViewCellStyle15.NullValue = Nothing
        Me.QTY1.DefaultCellStyle = DataGridViewCellStyle15
        Me.QTY1.FillWeight = 25.0!
        Me.QTY1.HeaderText = "QTY 1"
        Me.QTY1.Name = "QTY1"
        '
        'LOT2
        '
        Me.LOT2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LOT2.FillWeight = 80.0!
        Me.LOT2.HeaderText = "LOT 2"
        Me.LOT2.Name = "LOT2"
        '
        'QTY2
        '
        Me.QTY2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N0"
        DataGridViewCellStyle16.NullValue = Nothing
        Me.QTY2.DefaultCellStyle = DataGridViewCellStyle16
        Me.QTY2.FillWeight = 25.0!
        Me.QTY2.HeaderText = "QTY 2"
        Me.QTY2.Name = "QTY2"
        '
        'LOT3
        '
        Me.LOT3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LOT3.FillWeight = 80.0!
        Me.LOT3.HeaderText = "LOT 3"
        Me.LOT3.Name = "LOT3"
        '
        'QTY3
        '
        Me.QTY3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N0"
        Me.QTY3.DefaultCellStyle = DataGridViewCellStyle17
        Me.QTY3.FillWeight = 25.0!
        Me.QTY3.HeaderText = "QTY 3"
        Me.QTY3.Name = "QTY3"
        '
        '_DATE
        '
        Me._DATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle18.Format = "dd/MM/yyyy"
        DataGridViewCellStyle18.NullValue = Nothing
        Me._DATE.DefaultCellStyle = DataGridViewCellStyle18
        Me._DATE.FillWeight = 30.0!
        Me._DATE.HeaderText = "Date"
        Me._DATE.Name = "_DATE"
        '
        'TIME
        '
        Me.TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle19.Format = "HH:mm:ss"
        DataGridViewCellStyle19.NullValue = Nothing
        Me.TIME.DefaultCellStyle = DataGridViewCellStyle19
        Me.TIME.FillWeight = 30.0!
        Me.TIME.HeaderText = "Time"
        Me.TIME.Name = "TIME"
        '
        'LOTPARTBindingSource
        '
        Me.LOTPARTBindingSource.DataMember = "LOT_PART"
        Me.LOTPARTBindingSource.DataSource = Me.Ihi_rhf5
        '
        'Ihi_rhf5
        '
        Me.Ihi_rhf5.DataSetName = "ihi_rhf5"
        Me.Ihi_rhf5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LOT_PARTTableAdapter
        '
        Me.LOT_PARTTableAdapter.ClearBeforeFill = True
        '
        'BOMBindingSource
        '
        Me.BOMBindingSource.DataMember = "BOM"
        Me.BOMBindingSource.DataSource = Me.Ihi_rhf5
        '
        'BOMTableAdapter
        '
        Me.BOMTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(321, -2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'txtPlcReady
        '
        Me.txtPlcReady.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlcReady.BackColor = System.Drawing.Color.Red
        Me.txtPlcReady.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtPlcReady.Location = New System.Drawing.Point(1226, 554)
        Me.txtPlcReady.Name = "txtPlcReady"
        Me.txtPlcReady.ReadOnly = True
        Me.txtPlcReady.Size = New System.Drawing.Size(93, 20)
        Me.txtPlcReady.TabIndex = 16
        Me.txtPlcReady.Text = "Input Ready"
        Me.txtPlcReady.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tmrButton
        '
        '
        'lbResult
        '
        Me.lbResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbResult.BackColor = System.Drawing.Color.White
        Me.lbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbResult.Location = New System.Drawing.Point(42, 554)
        Me.lbResult.Name = "lbResult"
        Me.lbResult.Size = New System.Drawing.Size(1178, 20)
        Me.lbResult.TabIndex = 17
        Me.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartBarcode
        '
        Me.txtPartBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartBarcode.Location = New System.Drawing.Point(147, 100)
        Me.txtPartBarcode.Name = "txtPartBarcode"
        Me.txtPartBarcode.Size = New System.Drawing.Size(423, 20)
        Me.txtPartBarcode.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(76, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "*Part Scan: "
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(402, -2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.LightGray
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClear.Location = New System.Drawing.Point(351, 20)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(154, 23)
        Me.btnClear.TabIndex = 20
        Me.btnClear.Text = "CLEAR ALL DATA"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'frmCSOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1360, 588)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPartBarcode)
        Me.Controls.Add(Me.lbResult)
        Me.Controls.Add(Me.txtPlcReady)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnEndLot)
        Me.Controls.Add(Me.btnEndRework)
        Me.Controls.Add(Me.btnStartLot)
        Me.Controls.Add(Me.btnRework)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.txtCurrentQTY)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtLotQTY)
        Me.Controls.Add(Me.txtCustomerLot)
        Me.Controls.Add(Me.txtProductionLot)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmCSOperation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IHI - RHF5 - C/S Operation"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LOTPARTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ihi_rhf5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BOMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProductionLot As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerLot As System.Windows.Forms.TextBox
    Friend WithEvents txtLotQTY As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrentQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnRework As System.Windows.Forms.Button
    Friend WithEvents btnStartLot As System.Windows.Forms.Button
    Friend WithEvents btnEndLot As System.Windows.Forms.Button
    Friend WithEvents btnEndRework As System.Windows.Forms.Button
    Friend WithEvents Ihi_rhf5 As ihi_rhf5_CS_Operation.ihi_rhf5
    Friend WithEvents LOTPARTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LOT_PARTTableAdapter As ihi_rhf5_CS_Operation.ihi_rhf5TableAdapters.LOT_PARTTableAdapter
    Friend WithEvents BOMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BOMTableAdapter As ihi_rhf5_CS_Operation.ihi_rhf5TableAdapters.BOMTableAdapter
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtPlcReady As System.Windows.Forms.TextBox
    Friend WithEvents tmrButton As System.Windows.Forms.Timer
    Friend WithEvents lbResult As System.Windows.Forms.Label
    Friend WithEvents txtPartBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PART As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PART_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOT1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOT2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOT3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
