
Partial Class customer_FluidByAsset
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            yeardd.SelectedValue = Year(Now())
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView7_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView7.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim fluidIDlbl As Label = GridView7.Rows(index).FindControl("fluidIDlbl")
            Dim equipmentIDlbl As Label = GridView7.Rows(index).FindControl("equipmentIDlbl")
            Session("fluidID") = fluidIDlbl.Text
            Session("selected_equipmentID") = equipmentIDlbl.Text
            Response.Redirect("FluidByAssetDetail.aspx")
        End If
    End Sub

End Class
