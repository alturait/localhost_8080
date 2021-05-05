
Partial Class EST_PurchaseOrders
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim poIDlbl As Label = GridView1.Rows(index).FindControl("poIDlbl")
            Session("poID") = poIDlbl.Text
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        pagelbl.Text = Page.Title
    End Sub

End Class
