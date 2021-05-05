
Partial Class customer_FluidByAssetDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            yeardd.SelectedValue = Year(Now())
        End If
        pagelbl.Text = Page.Title
    End Sub

End Class
