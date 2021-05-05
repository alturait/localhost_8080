Imports System.Data.SqlClient

Partial Class main_SalesYTD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("this_locationID") = "0"
            If Session("selected_companyID") <> "0" Then
                Dim repID As Integer = appcode.GetRepID(Session("selected_companyID"))
                pagelbl.Text = appcode.GetCompany(Session("selected_companyID"))
            Else
                GridView1.DataSourceID = "SqlAllMonthlySales"
                GridView1.DataBind()
                GridView3.DataSourceID = "SqlAllTopMfrs"
                GridView3.DataBind()
                pagelbl.Text = "DFO Filters & Equipment"
            End If
            companyIDlbl.Text = Session("selected_companyID")
            yeardd.SelectedValue = Year(Now())
            'totalsaleslbl.Text = FormatCurrency(appcode.GetSalesYTD(Session("selected_companyID"), Year(Now())), 2)
            'totalorderslbl.Text = appcode.GetOrderCountYTD(Session("selected_companyID"), Year(Now()))
            'totalkitslbl.Text = appcode.GetTotalKitsSoldYTD(Year(Now()))
        End If
    End Sub

    Protected Sub yeardd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles yeardd.SelectedIndexChanged
        'totalsaleslbl.Text = FormatCurrency(appcode.GetSalesYTD(Session("selected_companyID"), yeardd.SelectedValue), 2)
        'totalorderslbl.Text = appcode.GetOrderCountYTD(Session("selected_companyID"), yeardd.SelectedValue)
        'totalkitslbl.Text = appcode.GetTotalKitsSoldYTD(yeardd.SelectedValue)
    End Sub

End Class
