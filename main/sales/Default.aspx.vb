Imports System.Data.SqlClient

Partial Class LT_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("this_locationID") = "0"
            yeardd.SelectedValue = Year(Now())
            If Session("selected_companyID") <> "0" Then
                Dim repID As Integer = appcode.GetRepID(Session("selected_companyID"))
                pagelbl.Text = appcode.GetCompany(Session("selected_companyID"))
                totalsaleslbl.Text = FormatCurrency(appcode.GetSalesYTD(Session("selected_companyID"), yeardd.SelectedValue), 2)
            Else
                GridView1.DataSourceID = "SqlAllMonthlySales"
                GridView1.DataBind()
                GridView2.DataSourceID = "SqlAllTopItems"
                GridView2.DataBind()
                GridView3.DataSourceID = "SqlAllTopMfrs"
                GridView3.DataBind()
                pagelbl.Text = "DFO Filters & Equipment"
                totalsaleslbl.Text = FormatCurrency(appcode.GetRepSalesYTD(Session("userID"), yeardd.SelectedValue), 2)
            End If
        End If
    End Sub

    Protected Sub yeardd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles yeardd.SelectedIndexChanged
        totalsaleslbl.Text = FormatCurrency(appcode.GetRepSalesYTD(Session("userID"), yeardd.SelectedValue), 2)
    End Sub
End Class
