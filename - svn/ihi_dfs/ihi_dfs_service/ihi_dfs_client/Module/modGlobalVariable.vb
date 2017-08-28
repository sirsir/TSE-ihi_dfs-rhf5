Module modGlobalVariable

    Public drSelectedRow As DataRow

    Public m_clsLogger As clsLogger = New clsLogger()

    Public dicSearch As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public dicMapLineId As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)

    Public dicMapSearchBy As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Const SCREEN_DATE_FORMAT As String = "dd-MM-yyyy"

    Public Sub initialize_modGlobalVariable()

        dicSearch.Add("Line", "")
        dicSearch.Add("Machine", "")
        dicSearch.Add("Search By", "")
        dicSearch.Add("Serial", "")
        dicSearch.Add("Lot No", "")
        dicSearch.Add("Date From", "")
        dicSearch.Add("Date To", "")
        dicSearch.Add("Times", "")
        dicSearch.Add("Result", "")


        Dim dt As ihi_rhf5_developmentDataSet.LINE_MASTERDataTable = New ihi_rhf5_developmentDataSet.LINE_MASTERDataTable

        da_LINE_MASTER.Fill(dt)


        Dim resultBh As ihi_rhf5_developmentDataSet.LINE_MASTERRow = dt.Select("LINE_NAME = 'BH'").First
        Dim resultCS As ihi_rhf5_developmentDataSet.LINE_MASTERRow = dt.Select("LINE_NAME = 'CS'").First

        dicMapLineId.Add("B/H", resultBh.ID)
        dicMapLineId.Add("C/S", resultCS.ID)

        dicMapSearchBy.Add("B/H", "SERIAL_BH")
        dicMapSearchBy.Add("C/S", "SERIAL_CS")
        dicMapSearchBy.Add("ITA", "SERIAL_ITA")

    End Sub




End Module
