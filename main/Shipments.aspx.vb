
Partial Class EST_Shipments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim shipmentIDlbl As Label = GridView1.Rows(index).FindControl("shipmentIDlbl")
            Session("shipmentID") = shipmentIDlbl.Text
            Response.Redirect("EditShipment.aspx")
        End If
    End Sub

End Class
