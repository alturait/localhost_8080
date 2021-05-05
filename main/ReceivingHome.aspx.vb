
Partial Class main_ReceivingHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pendingreturnslbl.Text = appcode.GetReturnsCount(0)
            openposlbl.Text = appcode.GetOpenPOs()
            pagelbl.Text = Page.Title
        End If
    End Sub

End Class
