
Partial Class main_ShippingHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'rtplbl.Text = appcode.GetReadyToPickCount(0)
            'totalshipmentdollarslbl.Text = FormatCurrency(appcode.GetShipments(0, Month(Now()), Year(Now())), 2)
            'totalshipmentslbl.Text = appcode.GetShipmentCount(0, Month(Now()), Year(Now()))
            openorderslbl.Text = appcode.GetOpenOrdersCount(0)
            pagelbl.Text = "Shipping Home"
        End If
    End Sub

End Class
