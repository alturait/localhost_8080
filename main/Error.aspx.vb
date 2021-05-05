
Partial Class main_Error
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            errmsg.Text = Session("err")
            errorpagelbl.Text = Session("err_page")
            erroruserlbl.Text = Session("err_user")
        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
