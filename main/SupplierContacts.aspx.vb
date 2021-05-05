
Partial Class main_SupplierContacts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim userIDlbl As Label = GridView1.Rows(index).FindControl("userIDlbl")
            Session("selected_userID") = userIDlbl.Text
            Response.Redirect("SupplierContact.aspx")
        End If
    End Sub

    Protected Sub newcontactbtn_Click(sender As Object, e As EventArgs) Handles newcontactbtn.Click
        Session("selected_userID") = "0"
        Response.Redirect("SupplierContact.aspx")
    End Sub

End Class
