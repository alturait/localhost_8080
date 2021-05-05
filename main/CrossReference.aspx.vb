
Partial Class CrossReference
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
            pripartnumberlbl.Value = appcode.GetPriPartNumber(Request.QueryString("searchterm"))
        End If
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim productIDlbl As Label = GridView2.Rows(index).FindControl("productIDlbl")
            Response.Redirect("ProductDetail.aspx?productID=" & productIDlbl.Text)
        End If
    End Sub

    Protected Sub crossbtn_Click(sender As Object, e As EventArgs) Handles crossbtn.Click
        Response.Redirect("CrossReference.aspx?searchterm=" & Trim(searchterm.Text))
    End Sub

End Class
