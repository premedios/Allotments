Imports MySql.Data.MySqlClient

Public Class ResultsReporter
    Private LastCompany = ""
    Private LastYear = 0, LastMonth = 0

    Enum ResultBreaks
        Company
        Year
        Month
    End Enum

    Public Sub New()
        Me.LastCompany = ""
        Me.LastYear = 0
        Me.LastMonth = 0
    End Sub

    Public Event DataChange(Break As ResultBreaks, Record As Allotment)

    Public Event DataRow(Record As Allotment)

    Public Sub ShowResults(IdContrato As Integer, Ano As Integer, Mes As Integer)
        Using mySqlConnection As New MySqlConnection(ConfigurationManager.ConnectionStrings("VTAConnectionString").ConnectionString)
            Dim sqlWhere = ""

            mySqlConnection.Open()

            If IdContrato <> 0 Then
                sqlWhere += " and allotment.idcontrato = @idcontrato"
            End If

            If Ano <> 0 Then
                sqlWhere += " and allotment.ano = @ano"
            End If

            If Mes <> 0 Then
                sqlWhere += " and allotment.mes = @mes"
            End If

            Dim sqlQuery = $"select hotel.nome, ano, mes, dia, detalhe.unit_name, count(inicial) from vta_contrato_hotel_allotment allotment inner join vta_contrato_hotel_detalhe detalhe on allotment.idpreco = detalhe.id and allotment.inicial > 0 {sqlWhere} inner join vta_contrato_hotel hotel on hotel.id = allotment.idcontrato group by hotel.nome, ano, mes, dia ORDER BY hotel.nome"

            Using cmd As New MySqlCommand()
                cmd.Connection = mySqlConnection
                cmd.CommandText = sqlQuery
                cmd.Prepare()
                cmd.Parameters.AddWithValue("@idcontrato", IdContrato)
                cmd.Parameters.AddWithValue("@ano", Ano)
                cmd.Parameters.AddWithValue("@mes", Mes)
                Dim result = cmd.ExecuteReader()
                If result.HasRows Then
                    While result.Read
                        If LastCompany = "" Or LastCompany <> result("nome") Then
                            LastCompany = result("nome")
                            LastYear = result("ano")
                            LastMonth = result("mes")
                            RaiseEvent DataChange(ResultBreaks.Company, New Allotment(result))
                        ElseIf LastYear <> result("ano") Then
                            LastYear = result("ano")
                            RaiseEvent DataChange(ResultBreaks.Year, New Allotment(result))
                        ElseIf LastMonth <> result("mes") Then
                            LastMonth = result("mes")
                            RaiseEvent DataChange(ResultBreaks.Month, New Allotment(result))
                        Else
                            RaiseEvent DataRow(New Allotment(result))
                        End If
                    End While
                End If
            End Using
        End Using
    End Sub
End Class
