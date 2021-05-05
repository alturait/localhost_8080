
Partial Class main_MailingLists
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim listIDlbl As Label = GridView1.Rows(index).FindControl("listIDlbl")
            Session("listID") = listIDlbl.Text
            Response.Redirect("MailingList.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
