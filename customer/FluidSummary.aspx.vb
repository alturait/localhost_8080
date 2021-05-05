
Partial Class customer_FluidSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            monthdd.SelectedValue = Month(Now())
            yeardd.SelectedValue = Year(Now())
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView7_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView7.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim fluidIDlbl As Label = GridView7.Rows(index).FindControl("fluidIDlbl")
            Session("fluidID") = fluidIDlbl.Text
            Session("rmonth") = monthdd.SelectedValue
            Session("ryear") = yeardd.SelectedValue
            Response.Redirect("FluidSummaryDetail.aspx")
        ElseIf e.CommandName = "ByAsset" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim fluidIDlbl As Label = GridView7.Rows(index).FindControl("fluidIDlbl")
            Session("fluidID") = fluidIDlbl.Text
            Response.Redirect("FluidByAsset.aspx")
        End If
    End Sub
End Class
