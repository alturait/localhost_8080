
Partial Class main_AllDiscounts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("companyID") <> "" Then
                Session("menu") = "csettings"
                Session("selected_companyID") = Request.QueryString("companyID")
                Response.Redirect("Discounts.aspx")
            End If
        End If
    End Sub
    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim multiplierIDlbl As Label = GridView2.Rows(index).FindControl("multiplierIDlbl")
            appcode.DeleteDiscount(multiplierIDlbl.Text)
            Response.Redirect("AllDiscounts.aspx")
        End If
    End Sub
End Class
