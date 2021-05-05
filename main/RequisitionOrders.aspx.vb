
Partial Class main_RequisitionOrders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            manufacturerlbl.Text = Request.QueryString("manufacturer").ToString
            partnumberlbl.Text = Request.QueryString("partnumber").ToString
        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
