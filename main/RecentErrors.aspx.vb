
Partial Class main_RecentErrors
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
