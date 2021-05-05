Imports System.Data.SqlClient

Partial Class main_SalesHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            neworderslbl.Text = appcode.GetNewOrdersCount(0)
            totalsaleslbl.Text = FormatCurrency(appcode.GetSalesMTD(0, Month(Now()), Year(Now())), 2)
            torderslbl.Text = FormatCurrency(appcode.GetOrdersMTD(0, Month(Now()), Year(Now())), 2)
            totalorderslbl.Text = appcode.GetOrderCountMTD(0, Month(Now()), Year(Now()))
            totalshipmentslbl.Text = appcode.GetShipmentCountMTD(0, Month(Now()), Year(Now()))
            backorderslbl.Text = appcode.GetBackOrdersCount(0)
            openorderslbl.Text = appcode.GetOpenOrdersCount(0)
            needspolbl.Text = appcode.GetNeedsPO()
            monthdd.SelectedValue = Month(Now())
            yeardd.SelectedValue = Year(Now())
        End If
        pagelbl.Text = "Sales - " & MonthName(monthdd.SelectedValue)
    End Sub

    Protected Sub yeardd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles yeardd.SelectedIndexChanged
        neworderslbl.Text = appcode.GetNewOrdersCount(0)
        totalsaleslbl.Text = FormatCurrency(appcode.GetSalesMTD(0, monthdd.SelectedValue, yeardd.SelectedValue), 2)
        totalorderslbl.Text = appcode.GetOrderCountMTD(0, monthdd.SelectedValue, yeardd.SelectedValue)
    End Sub

    Protected Sub monthdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles monthdd.SelectedIndexChanged
        neworderslbl.Text = appcode.GetNewOrdersCount(0)
        totalsaleslbl.Text = FormatCurrency(appcode.GetSalesMTD(0, monthdd.SelectedValue, yeardd.SelectedValue), 2)
        totalorderslbl.Text = appcode.GetOrderCountMTD(0, monthdd.SelectedValue, yeardd.SelectedValue)
    End Sub

End Class
