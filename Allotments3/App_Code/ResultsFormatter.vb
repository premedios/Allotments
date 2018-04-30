Public Class ResultsFormatter
    Private ContainerUpdatePanel As UpdatePanel

    Private CompanyCard, YearCard As New HtmlGenericControl
    Private CompanyCardBody, YearCardBody As HtmlGenericControl

    Private DataTable As Table

    Private MonthCard, CardHeader, MonthCardBody, HeaderContent As HtmlGenericControl
    Private DataTableHeader As TableHeaderRow
    Private DataTableRow As TableRow

    Private WithEvents resultsReporter As New ResultsReporter

    Public Sub New(updatePanel As UpdatePanel)
        Me.ContainerUpdatePanel = updatePanel
    End Sub

    Public Sub ShowResults(IdContrato As Integer, Ano As Integer, Mes As Integer)
        resultsReporter.ShowResults(IdContrato, Ano, Mes)
    End Sub

    Private Sub DataChange(ChangeType As ResultsReporter.ResultBreaks, Record As Allotment) Handles resultsReporter.DataChange
        Dim ViewModel = New AllotmentViewModel()
        ViewModel.Model = Record
        Select Case ChangeType
            Case ResultsReporter.ResultBreaks.Company
                CompanyChange(ViewModel)

            Case ResultsReporter.ResultBreaks.Year
                YearChange(ViewModel)
            Case ResultsReporter.ResultBreaks.Month
                MonthChange(ViewModel)
        End Select
    End Sub

    Private Function NewTableHeaderCell(cellText As String) As TableHeaderCell
        Dim cell As New TableHeaderCell
        cell.Text = cellText
        cell.Scope = TableHeaderScope.Column
        Return cell
    End Function

    Private Function NewTableRow(Record As AllotmentViewModel) As TableRow
        Dim tableRow = New TableRow()

        tableRow.Cells.Add(NewTableCell(Record.AllotDate))
        tableRow.Cells.Add(NewTableCell(Record.AllotUnit))
        tableRow.Cells.Add(NewTableCell(Record.AllotQuantity))

        Return tableRow
    End Function

    Private Function NewTableCell(contents As Object) As TableCell
        Dim tableCell = New TableCell()

        tableCell.Text = contents
        tableCell.Style.Add("width", "1px")
        Return tableCell
    End Function

    Private Sub DataRow(Record As Allotment) Handles resultsReporter.DataRow
        Dim ViewModel As New AllotmentViewModel()
        ViewModel.Model = Record
        DataTable.Rows.Add(NewTableRow(ViewModel))
    End Sub

    Private Sub CompanyChange(ViewModel As AllotmentViewModel)
        Dim Container = New HtmlGenericControl("div")
        Container.Attributes("class") = "container pt-4"
        CompanyCard = New HtmlGenericControl("div")
        CompanyCard.Attributes("class") = "card"
        CardHeader = New HtmlGenericControl("div")
        CardHeader.Attributes("class") = "card-header"
        HeaderContent = New HtmlGenericControl("h3")
        HeaderContent.InnerText = ViewModel.Model.AllotmentName
        CardHeader.Controls.Add(HeaderContent)
        CompanyCard.Controls.Add(CardHeader)
        CompanyCardBody = New HtmlGenericControl("div")
        CompanyCardBody.Attributes("class") = "card-body p-3"
        CompanyCard.Controls.Add(CompanyCardBody)
        Container.Controls.Add(CompanyCard)

        YearCard = New HtmlGenericControl("div")
        YearCard.Attributes("class") = "card mb-4"
        CardHeader = New HtmlGenericControl("div")
        CardHeader.Attributes("class") = "card-header"
        HeaderContent = New HtmlGenericControl("h5")
        HeaderContent.InnerText = ViewModel.Model.AllotmentYear
        CardHeader.Controls.Add(HeaderContent)
        YearCard.Controls.Add(CardHeader)
        YearCardBody = New HtmlGenericControl("div")
        YearCardBody.Attributes("class") = "card-body p-3"
        YearCard.Controls.Add(YearCardBody)
        CompanyCardBody.Controls.Add(YearCard)

        MonthCard = New HtmlGenericControl("div")
        MonthCard.Attributes("class") = "card mb-4"
        CardHeader = New HtmlGenericControl("div")
        CardHeader.Attributes("class") = "card-header"
        HeaderContent = New HtmlGenericControl("h5")
        HeaderContent.InnerText = ViewModel.AllotMonth
        CardHeader.Controls.Add(HeaderContent)
        MonthCard.Controls.Add(CardHeader)
        MonthCardBody = New HtmlGenericControl("div")
        MonthCardBody.Attributes("class") = "card-body"
        MonthCard.Controls.Add(MonthCardBody)
        YearCardBody.Controls.Add(MonthCard)

        DataTable = New Table()
        DataTable.CssClass = "table"

        DataTableHeader = New TableHeaderRow()
        DataTableHeader.CssClass = "thead-dark"
        DataTableHeader.Cells.Add(NewTableHeaderCell("Date"))
        DataTableHeader.Cells.Add(NewTableHeaderCell("Unit"))
        DataTableHeader.Cells.Add(NewTableHeaderCell("Quantity"))
        DataTable.Rows.Add(DataTableHeader)
        DataTable.Rows.Add(NewTableRow(ViewModel))
        MonthCardBody.Controls.Add(DataTable)

        ContainerUpdatePanel.ContentTemplateContainer.Controls.Add(Container)
    End Sub

    Private Sub YearChange(ViewModel As AllotmentViewModel)
        YearCard = New HtmlGenericControl("div")
        YearCard.Attributes("class") = "card mb-4"
        CardHeader = New HtmlGenericControl("div")
        CardHeader.Attributes("class") = "card-header"
        HeaderContent = New HtmlGenericControl("h5")
        HeaderContent.InnerText = ViewModel.AllotYear
        CardHeader.Controls.Add(HeaderContent)
        YearCard.Controls.Add(CardHeader)
        YearCardBody = New HtmlGenericControl("div")
        YearCardBody.Attributes("class") = "card-body p-3"
        YearCard.Controls.Add(YearCardBody)
        CompanyCardBody.Controls.Add(YearCard)
    End Sub

    Private Sub MonthChange(ViewModel As AllotmentViewModel)
        MonthCard = New HtmlGenericControl("div")
        MonthCard.Attributes("class") = "card mb-4"
        CardHeader = New HtmlGenericControl("div")
        CardHeader.Attributes("class") = "card-header"
        HeaderContent = New HtmlGenericControl("h5")
        HeaderContent.InnerText = ViewModel.AllotMonth
        CardHeader.Controls.Add(HeaderContent)
        MonthCard.Controls.Add(CardHeader)
        MonthCardBody = New HtmlGenericControl("div")
        MonthCardBody.Attributes("class") = "card-body"

        DataTable = New Table()
        DataTable.CssClass = "table"
        DataTableHeader = New TableHeaderRow()
        DataTableHeader.CssClass = "thead-dark"
        DataTableHeader.Cells.Add(NewTableHeaderCell("Date"))
        DataTableHeader.Cells.Add(NewTableHeaderCell("Unit"))
        DataTableHeader.Cells.Add(NewTableHeaderCell("Quantity"))
        DataTable.Rows.Add(DataTableHeader)
        MonthCardBody.Controls.Add(DataTable)
        MonthCard.Controls.Add(MonthCardBody)
        YearCardBody.Controls.Add(MonthCard)
    End Sub
End Class
