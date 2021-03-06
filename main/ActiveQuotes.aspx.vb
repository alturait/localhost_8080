
Partial Class EST_ActiveQuotes
    Inherits System.Web.UI.Page
    Dim sales_total As Double = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False And Session("company_role") = "Vendor" Then
                GridView1.DataSourceID = "SqlQuotesByVUser"
                GridView1.DataBind()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim quoteIDlbl As Label = GridView1.Rows(index).FindControl("quoteIDlbl")
            Session("quoteID") = quoteIDlbl.Text
            Response.Redirect("Quote.aspx")
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim subtotallbl As Label = e.Row.FindControl("subtotallbl")
            sales_total += subtotallbl.Text
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim salestotallbl As Label = DirectCast(e.Row.FindControl("salestotallbl"), Label)
            salestotallbl.Text = sales_total.ToString("c")
        End If
    End Sub

End Class
