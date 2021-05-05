
Partial Class customer_OpenKits
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Order" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView2.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        End If
    End Sub

End Class
