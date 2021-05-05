
Partial Class OpenOrders
    Inherits System.Web.UI.Page
    Dim sales_total As Double = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        ElseIf e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("EditOrder.aspx")
        ElseIf e.CommandName = "Print" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim filename As String = "PDF/PickTicket_" & orderIDlbl.Text & ".pdf"
            Dim assetID As String = appcode.getAssetID(appcode.GetServiceProfileIDFromOrderID(orderIDlbl.Text))
            appcode.PickTicketPDF(orderIDlbl.Text, assetID, applicationpath, "Images/bannerlogo.jpg", filename)
            Response.Redirect("../" & filename)
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim subtotallbl As Label = e.Row.FindControl("subtotallbl")
            sales_total += subtotallbl.Text
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim salestotallbl As Label = DirectCast(e.Row.FindControl("salestotallbl"), Label)
            salestotallbl.Text = sales_total.ToString("c")
        End If
    End Sub

End Class
