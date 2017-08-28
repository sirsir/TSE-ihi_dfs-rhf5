Public Class frmShowParts

    Private dtStatic As DataTable
    

    Private strCsLot As String
    Private strItaLot As String


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub




    Private Sub frmShowParts_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        'strCsLot = drSelectedRow.Item("Serial_CS").ToString
        'If strCsLot.Split(" ").Count > 1 Then
        '    strCsLot = strCsLot.Split(" ")(0)
        'End If
        'Me.TextBoxCsLot.Text = strCsLot


        'strItaLot = drSelectedRow.Item("Serial_ITA").ToString
        'If strItaLot.Split(" ").Count > 1 Then
        '    strItaLot = strItaLot.Split(" ")(0)

        'End If
        'Me.TextBoxItaLot.Text = strItaLot

        



        strCsLot = drSelectedRow.Item("Serial_CS").ToString
        If strCsLot.Length < 8 Then
            'Me.TextBoxCsLot.Text = strCsLot
        Else
            strCsLot = strCsLot.Substring(0, 8)

        End If


        strItaLot = drSelectedRow.Item("Serial_ITA").ToString
        If strItaLot.Length < 12 Then
            'Me.TextBoxItaLot.Text = strItaLot
        Else
            strItaLot = strItaLot.Substring(0, 12)

        End If


        Me.TextBoxCsLot.Text = strCsLot

        Me.TextBoxItaLot.Text = strItaLot


        Me.FilterData()



    End Sub


    Private Sub FilterData()
        
        'Dim dv1 As DataView

        'dtStatic = da_LOT_PART_w_LOT.GetDataByLotCs_LotIta(strCsLot, strItaLot)

        If strCsLot = "" And strItaLot = "" Then
            MsgBox("Lot C/S and ITA can not be both Empty String")
            Exit Sub
        ElseIf strCsLot <> "" And strItaLot = "" Then
            dtStatic = da_LOT_PART_w_LOT.GetDataByLotCs(strCsLot & "%")
        ElseIf strCsLot = "" And strItaLot = "" Then
            dtStatic = da_LOT_PART_w_LOT.GetDataByLotCs_LotIta(strCsLot & "%", strItaLot & "%")
        Else
            MsgBox("Lot C/S can not be Empty String")
            Exit Sub
        End If



        'dtStatic = da_LOT_PART_w_LOT.GetData()

        'dv1 = New DataView(dtStatic)

        dtStatic = dtStatic.DefaultView.ToTable(False, "PART_NO", "PART_NAME", "PART_LOT_NO", "QTY", "PART_LOT_NO_2", "QTY_2", "PART_LOT_NO_3", "QTY_3", "DATE")

        dtStatic.Columns("PART_NO").ColumnName = "PART*"
        dtStatic.Columns("PART_LOT_NO").ColumnName = "LOT"




        DataGridView1.DataSource = dtStatic





    End Sub


End Class