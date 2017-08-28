Public Class clsStartAllWorker
#Region "Attribute"
    Private m_bkgList As List(Of clsPlcCommunication)
    Private m_objLogger As clsLogger
    Friend WithEvents m_objBkgImport As clsImportDataFile
#End Region

#Region "Constructor"
    Public Sub New()
        m_bkgList = New List(Of clsPlcCommunication)
        m_objLogger = New clsLogger
    End Sub
#End Region

#Region "Method"
    Public Sub StartAll()
        Try
            Dim aobjLine As List(Of clsLineSetting) = clsLineSetting.FindAll
            For Each objLine As clsLineSetting In aobjLine
                Dim objPlc As New clsPlcCommunication(objLine)
                With objPlc
                    .WorkerReportsProgress = True
                    .WorkerSupportsCancellation = True
                    '.RunWorkerAsync()
                End With
                m_bkgList.Add(objPlc)
            Next

            For Each objPlc As clsPlcCommunication In m_bkgList
                objPlc.RunWorkerAsync()
            Next

            m_objBkgImport = New clsImportDataFile()
            m_objBkgImport.WorkerReportsProgress = True
            m_objBkgImport.WorkerSupportsCancellation = True
            m_objBkgImport.RunWorkerAsync()
        Catch ex As Exception
            m_objLogger.AppendLog(ex)
        End Try
    End Sub

    Public Sub StopAll()

        For Each objPlc As clsPlcCommunication In m_bkgList
            Try
                If objPlc.IsBusy AndAlso Not objPlc.CancellationPending Then objPlc.CancelAsync()
            Catch ex As Exception
                m_objLogger.AppendLog(ex)
            End Try
        Next
        If m_objBkgImport.IsBusy AndAlso Not m_objBkgImport.CancellationPending Then m_objBkgImport.CancelAsync()

    End Sub
#End Region

End Class
