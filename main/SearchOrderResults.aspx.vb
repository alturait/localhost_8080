
Partial Class main_SearchOrderResults
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        End If
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        Session("selected_partnumber") = searchorderstb.Text
        If matchcb.Checked = True Then
            Session("match") = "match"
        Else
            Session("match") = ""
        End If
        Response.Redirect("SearchOrderResults.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = "Searching Orders for " & Session("selected_partnumber")
            If Session("match").ToString <> "" Then
                matchcb.Checked = True
                GridView1.DataSourceID = "SqlOrdersMatch"
            End If
        End If
    End Sub
End Class
