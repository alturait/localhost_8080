Imports System.Data.SqlClient
Imports System.IO

Partial Class customer_Favorites
    Inherits System.Web.UI.Page

    Protected Function GetPicture(ByVal partnumber As String) As String
        GetPicture = "blank"
        If File.Exists(Server.MapPath("~/Images/Catalog/" & partnumber & ".jpg")) = True Then
            GetPicture = partnumber
        End If
    End Function

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim productIDlbl As Label = GridView1.Rows(index).FindControl("productIDlbl")
            Response.Redirect("CatalogPage.aspx?productID=" & productIDlbl.Text)
        ElseIf e.CommandName = "Add" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim productIDlbl As Label = GridView1.Rows(index).FindControl("productIDlbl")
            Dim qtytb As TextBox = GridView1.Rows(index).FindControl("qtytb")
            appcode.AddToCart(productIDlbl.Text, qtytb.Text, Session("selected_companyID"), Session("userID"), Session("vendorID"), "0")
            Response.Redirect("Favorites.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim favoriteIDlbl As Label = GridView1.Rows(index).FindControl("favoriteIDlbl")
            appcode.DeleteFavorite(favoriteIDlbl.Text)
            Response.Redirect("Favorites.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub addselectedbtn_Click(sender As Object, e As EventArgs) Handles addselectedbtn.Click
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            Dim productIDlbl As Label = row.FindControl("productIDlbl")
            Dim qtytb As TextBox = row.FindControl("qtytb")
            If selectcb.Checked = True And IsNumeric(qtytb.Text) Then
                Dim cartID As Integer = appcode.AddToCart(productIDlbl.Text, qtytb.Text, Session("selected_companyID"), Session("userID"), Session("vendorID"), 0)
            End If
        Next
        Response.Redirect("Cart.aspx")
    End Sub

End Class
