
Partial Class SearchResults
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        Response.Redirect("SearchResults.aspx?searchterm=" & Trim(searchterm.Text))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            searchtermlbl.Text = Request.QueryString("searchterm")
            If Request.QueryString("manufacturer") <> "" Then
                GridView1.DataSourceID = "SqlProductsByMfr"
                GridView1.DataBind()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim productIDlbl As Label = GridView1.Rows(index).FindControl("productIDlbl")
            Response.Redirect("VCatalogPage.aspx?productID=" & productIDlbl.Text)
        End If
    End Sub

    Protected Sub productbtn_Click(sender As Object, e As EventArgs) Handles productbtn.Click
        Session("productID") = ""
        Response.Redirect("Product.aspx")
    End Sub

    Protected Sub crossbtn_Click(sender As Object, e As EventArgs) Handles crossbtn.Click
        Response.Redirect("CrossReference.aspx?searchterm=" & Trim(searchterm.Text))
    End Sub

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        If manufacturerdd.SelectedValue <> "0" Then
            Response.Redirect("SearchResults.aspx?manufacturer=" & manufacturerdd.SelectedValue & "&searchterm=" & Request.QueryString("searchterm"))
        End If
    End Sub
End Class
