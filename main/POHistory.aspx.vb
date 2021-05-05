
Partial Class EST_POHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            monthdd.SelectedValue = Month(Now())
            yeardd.SelectedValue = Year(Now())
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim poIDlbl As Label = GridView1.Rows(index).FindControl("poIDlbl")
            Session("poID") = poIDlbl.Text
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

End Class
