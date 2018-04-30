Public Class AllotmentViewModel
    Private Property AllotmentDate As String
    Private Property AllotmentContractName As String
    Private Property AllotmentUnit As String
    Private Property AllotmentQuantity As Integer

    Private Property AllotmentModel As Allotment

    Public Property Model() As Allotment
        Set(value As Allotment)
            Me.AllotmentModel = value
            Me.AllotmentDate = Convert.ToDateTime(value.AllotmentYear.ToString + "/" + value.AllotmentMonth.ToString + "/" + value.AllotmentDay.ToString).ToShortDateString
            Me.AllotmentContractName = value.AllotmentName
            Me.AllotmentUnit = value.AllotmentUnit
            Me.AllotmentQuantity = value.AllotmentQuantity
        End Set
        Get
            Return Me.AllotmentModel
        End Get
    End Property

    ReadOnly Property AllotContractName() As String
        Get
            Return Me.AllotmentContractName
        End Get
    End Property

    ReadOnly Property AllotDate() As String
        Get
            Return Me.AllotmentDate
        End Get
    End Property

    ReadOnly Property AllotUnit As String
        Get
            Return Me.AllotmentUnit
        End Get
    End Property

    ReadOnly Property AllotQuantity As String
        Get
            Return Me.AllotmentQuantity
        End Get
    End Property

    ReadOnly Property AllotYear As Integer
        Get
            Return Me.AllotmentModel.AllotmentYear
        End Get
    End Property

    ReadOnly Property AllotMonth As String
        Get
            Return {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"}(Me.AllotmentModel.AllotmentMonth - 1)
        End Get
    End Property
End Class
