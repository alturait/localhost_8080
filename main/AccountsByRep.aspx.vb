
Partial Class main_AccountsByRep
    Inherits System.Web.UI.Page
    Dim sales_mtd As Double = 0
    Dim sales_ytd As Double = 0

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim companyIDlbl As Label = GridView1.Rows(index).FindControl("companyIDlbl")
            Session("selected_companyID") = companyIDlbl.Text
            Session("chargetax") = appcode.getChargeTax(Session("selected_companyID"))
            Session("selected_shipID") = ""
            Session("selected_userID") = ""
            Session("warehousemfr") = ""
            Session("selected_supplierID") = "0"
            Response.Redirect("Default.aspx?menu=corders")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If repdd.SelectedValue <> "All" Then
                GridView1.DataSourceID = "SqlCustomersByRep"
                GridView1.DataBind()
            End If
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub repdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles repdd.SelectedIndexChanged
        If repdd.SelectedValue <> "All" Then
            GridView1.DataSourceID = "SqlCustomersByRep"
            GridView1.DataBind()
        Else
            GridView1.DataSourceID = "SqlCustomers"
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim salesytdlbl As Label = e.Row.FindControl("salesytdlbl")
            sales_ytd += salesytdlbl.Text
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim salesytdtotallbl As Label = DirectCast(e.Row.FindControl("salesytdtotallbl"), Label)
            salesytdtotallbl.Text = sales_ytd.ToString("c")
        End If
    End Sub

End Class
