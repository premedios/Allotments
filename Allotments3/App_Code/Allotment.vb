Imports MySql.Data.MySqlClient

Public Class Allotment
    Private Property Name As String
    Private Property Year As Integer
    Private Property Month As Integer
    Private Property Day As Integer
    Private Property Unit As String
    Private Property Quantity As Integer

    Public Sub New(AllotmentRecord As MySqlDataReader)
        Me.Name = AllotmentRecord("nome")
        Me.Year = AllotmentRecord("ano")
        Me.Month = AllotmentRecord("mes")
        Me.Day = AllotmentRecord("dia")
        Me.Unit = AllotmentRecord("unit_name")
        Me.Quantity = AllotmentRecord("count(inicial)")
    End Sub

    ReadOnly Property AllotmentName() As String
        Get
            Return Me.Name
        End Get
    End Property

    ReadOnly Property AllotmentYear() As Integer
        Get
            Return Me.Year
        End Get
    End Property

    ReadOnly Property AllotmentMonth() As Integer
        Get
            Return Me.Month
        End Get
    End Property

    ReadOnly Property AllotmentDay() As Integer
        Get
            Return Me.Day
        End Get
    End Property

    ReadOnly Property AllotmentUnit() As String
        Get
            Return Me.Unit
        End Get
    End Property

    ReadOnly Property AllotmentQuantity() As String
        Get
            Return Me.Quantity
        End Get
    End Property
End Class
