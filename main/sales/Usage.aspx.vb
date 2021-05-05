
Partial Class main_Usage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("selected_companyID") <> "0" Then
                GridView1.DataSourceID = "SqlCustomerUsage"
                GridView1.DataBind()
                If Request.QueryString("manufacturer") <> "" Then
                    GridView1.DataSourceID = "SqlCustomerUsageByMfr"
                    GridView1.DataBind()
                End If
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Response.Redirect("UsageDetail.aspx?manufacturer=" & manufacturerlbl.Text & "&partnumber=" & partnumberlbl.Text)
        End If
    End Sub

    Protected Sub mfrdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mfrdd.SelectedIndexChanged
        If mfrdd.SelectedValue <> "0" Then
            Response.Redirect("Usage.aspx?manufacturer=" & mfrdd.SelectedValue)
        Else
            Response.Redirect("Usage.aspx")
        End If
    End Sub
End Class
