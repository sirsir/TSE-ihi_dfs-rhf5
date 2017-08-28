Module modDAO
    'Public da1 As fct_gmi700_pbsDataSetTableAdapters.v_Result_HistoryTableAdapter = New fct_gmi700_pbsDataSetTableAdapters.v_Result_HistoryTableAdapter()

    'Public dv As DataView
    'Public dt1 As fct_gmi700_pbsDataSet.v_Result_HistoryDataTable = New fct_gmi700_pbsDataSet.v_Result_HistoryDataTable




    'TODO: This line of code loads data into the 'Fct_gmi700_pbsDataSet.V_RESULT_HISTORY' table. You can move, or remove it, as needed.
    'Me.V_RESULT_HISTORYTableAdapter.Fill(Me.Fct_gmi700_pbsDataSet.V_RESULT_HISTORY)


    'Private Sub SetupDataObj()

    '    'dt1 = New fct_gmi700_pbsDataSet.V_RESULT_HISTORYDataTable()
    '    da1.Fill(dt1)
    '    'fct_gmi700_pbsDataSetTableAdapters.V_RESULT_HISTORYTableAdapter()
    '    'fct_gmi700_pbsDataSetTableAdapters.V_RESULT_HISTORYTableAdapter.

    '    'dv = New DataView(ds.Tables(0), "type = 'business' ", "type Desc", DataViewRowState.CurrentRows)

    '    dv = New DataView(dt1)

    '    CtrlPivotGrid1.DataSource = dv
    'End Sub

    Public da_SERIAL As ihi_rhf5_developmentDataSetTableAdapters.SERIALTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.SERIALTableAdapter()
    Public da_RESULT As ihi_rhf5_developmentDataSetTableAdapters.RESULTTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.RESULTTableAdapter()

    Public da_MACHINE_DATA_STR As ihi_rhf5_developmentDataSetTableAdapters.MACHINE_DATA_STRTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.MACHINE_DATA_STRTableAdapter()

    Public da_MACHINE As ihi_rhf5_developmentDataSetTableAdapters.MACHINETableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.MACHINETableAdapter()

    Public da_LINE_MASTER As ihi_rhf5_developmentDataSetTableAdapters.LINE_MASTERTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.LINE_MASTERTableAdapter()

    Public da_V_SETTING_COLUMNS As ihi_rhf5_developmentDataSetTableAdapters.V_SETTING_COLUMNSTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.V_SETTING_COLUMNSTableAdapter()
    Public da_MACHINE_DATA_STR_w_SERIAL As ihi_rhf5_developmentDataSetTableAdapters.MACHINE_DATA_STR_w_SERIALTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.MACHINE_DATA_STR_w_SERIALTableAdapter()
    Public da_LOT_PART_w_LOT As ihi_rhf5_developmentDataSetTableAdapters.LOT_PART_w_LOTTableAdapter = New ihi_rhf5_developmentDataSetTableAdapters.LOT_PART_w_LOTTableAdapter()

    Private Sub dataRefresh()
        'CtrlPivotGrid1.DataSource = Nothing

        'dt1.Clear()
        'da1.Fill(dt1)
        'dv = New DataView(dt1)

        'CtrlPivotGrid1.DataSource = dv

        ''dv.RowFilter = [String].Empty
    End Sub


    Private Sub dataFilter()
        ''dv.RowFilter = "[Code No]='14000002'"

        'Dim strFilter = ""

        'If Me.TableLayoutPanel3.Controls("CodeNo").Text <> String.Empty Then
        '    strFilter = String.Format("[Code No]LIKE'{0}'", Me.TableLayoutPanel3.Controls("CodeNo").Text)
        '    'dv.RowFilter = String.Format("[Code No]LIKE'{0}' AND [Type]='RH'", Me.TableLayoutPanel3.Controls("CodeNo").Text)

        'End If

        'If Me.TableLayoutPanel3.Controls("ModelType").Text <> String.Empty Then

        '    If strFilter = String.Empty Then
        '        strFilter = String.Format("[Type]LIKE'{0}'", Me.TableLayoutPanel3.Controls("ModelType").Text)

        '    Else
        '        strFilter = String.Format("{1} AND [Type]LIKE'{0}'", Me.TableLayoutPanel3.Controls("ModelType").Text, strFilter)
        '    End If


        'End If

        'If Me.TableLayoutPanel3.Controls("Result").Text <> String.Empty Then

        '    If strFilter = String.Empty Then
        '        strFilter = String.Format("([Injection|Result]LIKE'{0}' OR [Tear Cut|Result]LIKE'{0}' OR [Vibration|Result]LIKE'{0}' OR [Assy|Result]LIKE'{0}')", Me.TableLayoutPanel3.Controls("Result").Text)

        '    Else
        '        strFilter = String.Format("{1} AND ([Injection|Result]LIKE'{0}' OR [Tear Cut|Result]LIKE'{0}' OR [Vibration|Result]LIKE'{0}' OR [Assy|Result]LIKE'{0}')", Me.TableLayoutPanel3.Controls("Result").Text, strFilter)
        '    End If


        'End If


        'If Me.TableLayoutPanel3.Controls("DateFrom").Text <> String.Empty Then

        '    If strFilter = String.Empty Then
        '        strFilter = String.Format("([Injection|When]>='{0}' OR [Tear Cut|When]>='{0}' OR [Vibration|When]>='{0}' OR [Assy|When]>='{0}')", Me.TableLayoutPanel3.Controls("DateFrom").Text)

        '    Else
        '        strFilter = String.Format("{1} AND ([Injection|When]>='{0}' OR [Tear Cut|When]>='{0}' OR [Vibration|When]>='{0}' OR [Assy|When]>='{0}')", Me.TableLayoutPanel3.Controls("DateFrom").Text, strFilter)
        '    End If


        'End If

        'If Me.TableLayoutPanel3.Controls("DateTo").Text <> String.Empty Then

        '    If strFilter = String.Empty Then
        '        strFilter = String.Format("([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", Me.TableLayoutPanel3.Controls("DateTo").Text)

        '    Else
        '        strFilter = String.Format("{1} AND ([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", Me.TableLayoutPanel3.Controls("DateTo").Text, strFilter)
        '    End If


        'End If



        ''MsgBox(dv.Table.Columns.Item(0).ToString)
        ''dv.RowFilter = "Type='RH'"

        'dv.RowFilter = strFilter
    End Sub
End Module
