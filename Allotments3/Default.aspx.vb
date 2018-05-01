Imports MySql.Data.MySqlClient

Public Class _Default
    Inherits Page

    Dim LastCompany = ""
    Dim LastYear = 0, LastMonth = 0

    Dim Container, CompanyCard, YearCard As New HtmlGenericControl
    Dim CompanyCardBody, YearCardBody As HtmlGenericControl
    Dim DataTable As Table

    Dim MonthNames As String() = {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"}

    Dim filterContent As ContentPlaceHolder
    Dim mainContent As UpdatePanel
    Dim testContent As HtmlGenericControl
    Dim CompanyDropDownList, YearDropDownList, MonthDropDownList As DropDownList

    Dim results As ResultsFormatter


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim tHead As New TableHeaderRow
        Dim tHeaderCell As New TableHeaderCell
        Dim tRow As New TableRow
        Dim tCell As New TableCell

        mainContent = Me.Master.FindControl("MainContent").FindControl("ResultsUpdatePanel")
        filterContent = Me.Master.FindControl("FilterContent")
        testContent = filterContent.FindControl("Test")
        CompanyDropDownList = filterContent.FindControl("companyDropDown")
        YearDropDownList = filterContent.FindControl("yearDropDown")
        MonthDropDownList = filterContent.FindControl("monthDropDown")

        'results = New ResultsFormatter(mainContent)

        If Not Me.IsPostBack Then
            CompanyDropDownList.Items.Clear()
            CompanyDropDownList.Items.Add(New ListItem("-- All Sites --", 0))

            YearDropDownList.Items.Clear()
            YearDropDownList.Items.Add(New ListItem("-- All Years --", 0))

            MonthDropDownList.Items.Clear()
            MonthDropDownList.Items.Add(New ListItem("-- All Months --", 0))

            Using mySqlConnection As New MySqlConnection(ConfigurationManager.ConnectionStrings("VTAConnectionString").ConnectionString)
                mySqlConnection.Open()

                FillDropDownList(mySqlConnection, CompanyDropDownList, {"hotel.nome", "allotment.idcontrato"}, "hotel.nome", {"nome", "idcontrato"})
                FillDropDownList(mySqlConnection, YearDropDownList, {"allotment.ano"}, "allotment.ano", {"ano", "ano"})
                FillDropDownList(mySqlConnection, MonthDropDownList, {"allotment.mes"}, "allotment.mes", {"mes", "mes"})

                mySqlConnection.Close()
            End Using
        End If

        'results.ShowResults(CompanyDropDownList.SelectedValue, MonthDropDownList.SelectedValue, YearDropDownList.SelectedValue)

        LastCompany = ""
        LastYear = 0
        LastMonth = 0

        Using mySqlConnection As New MySqlConnection(ConfigurationManager.ConnectionStrings("VTAConnectionString").ConnectionString)
            Dim sqlWhere = ""

            mySqlConnection.Open()

            If CompanyDropDownList.SelectedIndex <> 0 Then
                sqlWhere += " and allotment.idcontrato = @idcontrato"
            End If

            If YearDropDownList.SelectedIndex <> 0 Then
                sqlWhere += " and allotment.ano = @ano"
            End If

            If MonthDropDownList.SelectedIndex <> 0 Then
                sqlWhere += " and allotment.mes = @mes"
            End If

            Dim sqlQuery = $"select hotel.nome, ano, mes, dia, detalhe.unit_name, count(inicial) from vta_contrato_hotel_allotment allotment inner join vta_contrato_hotel_detalhe detalhe on allotment.idpreco = detalhe.id and allotment.inicial > 0 {sqlWhere} inner join vta_contrato_hotel hotel on hotel.id = allotment.idcontrato group by hotel.nome, ano, mes, dia ORDER BY hotel.nome"

            Using cmd As New MySqlCommand()
                cmd.Connection = mySqlConnection
                cmd.CommandText = sqlQuery
                cmd.Prepare()
                cmd.Parameters.AddWithValue("@idcontrato", CompanyDropDownList.SelectedValue)
                cmd.Parameters.AddWithValue("@ano", YearDropDownList.SelectedValue)
                cmd.Parameters.AddWithValue("@mes", MonthDropDownList.SelectedValue)
                Dim result = cmd.ExecuteReader()
                Debug.WriteLine(result.HasRows.ToString)
                If result.HasRows Then
                    While result.Read
                        HandleChange(result)
                        mainContent.ContentTemplateContainer.Controls.Add(Container)
                    End While
                End If
            End Using
        End Using

    End Sub

    Protected Sub FillDropDownList(conn As MySqlConnection, DropDownControl As DropDownList, ByVal FieldsList() As String, OrderField As String, ByVal DropDownListValues() As String)
        Using cmd = New MySqlCommand($"select distinct {String.Join(",", FieldsList)} from vta_contrato_hotel_allotment allotment inner join vta_contrato_hotel_detalhe detalhe on allotment.idpreco = detalhe.id and allotment.inicial > 0 inner join vta_contrato_hotel hotel on hotel.id = allotment.idcontrato group by hotel.nome, ano, mes, dia ORDER BY {OrderField}")
            Debug.WriteLine(DropDownControl)
            cmd.Connection = conn
            Dim result = cmd.ExecuteReader()
            If result.HasRows Then
                While result.Read()
                    If TypeOf result(DropDownListValues(0)) Is Int32 And DropDownControl.ID = "monthDropDown" Then
                        DropDownControl.Items.Add(New ListItem(MonthNames(result(DropDownListValues(0)) - 1), result(DropDownListValues(1))))
                    Else
                        DropDownControl.Items.Add(New ListItem(result(DropDownListValues(0)), result(DropDownListValues(1))))
                    End If
                End While
            End If
        End Using
    End Sub

    Private Sub HandleChange(result As MySqlDataReader)
        Dim MonthCard, CardHeader, MonthCardBody, HeaderContent As HtmlGenericControl
        Dim DataTableHeader As TableHeaderRow
        Dim DataTableRow As TableRow

        If LastCompany = "" Or LastCompany <> result("nome") Then
            LastCompany = result("nome")
            Container = New HtmlGenericControl("div")
            Container.Attributes("class") = "container pt-4"
            CompanyCard = New HtmlGenericControl("div")
            CompanyCard.Attributes("class") = "card"
            CardHeader = New HtmlGenericControl("div")
            CardHeader.Attributes("class") = "card-header"
            HeaderContent = New HtmlGenericControl("h3")
            HeaderContent.InnerText = result("nome")
            CardHeader.Controls.Add(HeaderContent)
            CompanyCard.Controls.Add(CardHeader)
            CompanyCardBody = New HtmlGenericControl("div")
            CompanyCardBody.Attributes("class") = "card-body p-3"
            CompanyCard.Controls.Add(CompanyCardBody)
            Container.Controls.Add(CompanyCard)

            LastYear = result("ano")
            YearCard = New HtmlGenericControl("div")
            YearCard.Attributes("class") = "card mb-4"
            CardHeader = New HtmlGenericControl("div")
            CardHeader.Attributes("class") = "card-header"
            HeaderContent = New HtmlGenericControl("h5")
            HeaderContent.InnerText = result("ano").ToString
            CardHeader.Controls.Add(HeaderContent)
            YearCard.Controls.Add(CardHeader)
            YearCardBody = New HtmlGenericControl("div")
            YearCardBody.Attributes("class") = "card-body p-3"
            YearCard.Controls.Add(YearCardBody)
            CompanyCardBody.Controls.Add(YearCard)


            LastMonth = result("mes")
            MonthCard = New HtmlGenericControl("div")
            MonthCard.Attributes("class") = "card mb-4"
            CardHeader = New HtmlGenericControl("div")
            CardHeader.Attributes("class") = "card-header"
            HeaderContent = New HtmlGenericControl("h5")
            HeaderContent.InnerText = MonthNames(result("mes") - 1)
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
            DataTableRow = New TableRow
            DataTableRow.Cells.Add(NewTableCell(Convert.ToDateTime(result("ano").ToString + "/" + result("mes").ToString + "/" + result("dia").ToString).ToShortDateString))
            DataTableRow.Cells.Add(NewTableCell(result("unit_name")))
            DataTableRow.Cells.Add(NewTableCell(result("count(inicial)")))
            DataTable.Rows.Add(DataTableRow)
            MonthCardBody.Controls.Add(DataTable)

        ElseIf result("ano") <> 0 And (LastYear = 0 Or LastYear <> result("ano")) Then
            LastYear = result("ano")
            YearCard = New HtmlGenericControl("div")
            YearCard.Attributes("class") = "card mb-4"
            CardHeader = New HtmlGenericControl("div")
            CardHeader.Attributes("class") = "card-header"
            HeaderContent = New HtmlGenericControl("h5")
            HeaderContent.InnerText = result("ano").ToString
            CardHeader.Controls.Add(HeaderContent)
            YearCard.Controls.Add(CardHeader)
            YearCardBody = New HtmlGenericControl("div")
            YearCardBody.Attributes("class") = "card-body p-3"
            YearCard.Controls.Add(YearCardBody)
            CompanyCardBody.Controls.Add(YearCard)

        ElseIf result("mes") <> 0 And (LastMonth = 0 Or LastMonth <> result("mes")) Then
            LastMonth = result("mes")
            MonthCard = New HtmlGenericControl("div")
            MonthCard.Attributes("class") = "card mb-4"
            CardHeader = New HtmlGenericControl("div")
            CardHeader.Attributes("class") = "card-header"
            HeaderContent = New HtmlGenericControl("h5")
            HeaderContent.InnerText = MonthNames(result("mes") - 1)
            CardHeader.Controls.Add(HeaderContent)
            MonthCard.Controls.Add(CardHeader)
            MonthCardBody = New HtmlGenericControl("div")
            MonthCardBody.Attributes("class") = "card-body"
            'MonthCard.Controls.Add(MonthCardBody)
            'YearCardBody.Controls.Add(MonthCard)
            'YearCard.Controls.Add(YearCardBody)
            'CompanyCardBody.Controls.Add(YearCard)

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
            'YearCard.Controls.Add(YearCardBody)
            'CompanyCardBody.Controls.Add(YearCard)
        Else
            DataTable.Rows.Add(NewTableRow(result))
        End If

    End Sub

    Protected Function NewTableHeaderCell(cellText As String) As TableHeaderCell
        Dim cell As New TableHeaderCell
        cell.Text = cellText
        Return cell
    End Function

    Protected Function NewTableRow(resultRow As MySqlDataReader) As TableRow
        Dim tableRow = New TableRow()

        tableRow.Cells.Add(NewTableCell(Convert.ToDateTime(resultRow("ano").ToString + "/" + resultRow("mes").ToString + "/" + resultRow("dia").ToString).ToShortDateString))
        tableRow.Cells.Add(NewTableCell(resultRow("unit_name")))
        tableRow.Cells.Add(NewTableCell(resultRow("count(inicial)")))

        Return tableRow
    End Function

    Protected Function NewTableCell(contents As Object)
        Dim tableCell = New TableCell()

        tableCell.Text = contents
        tableCell.Style.Add("width", "1px")
        Return tableCell
    End Function
End Class