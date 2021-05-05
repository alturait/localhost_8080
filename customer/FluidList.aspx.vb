
Partial Class customer_FluidList
    Inherits System.Web.UI.Page

    Protected Sub fluidbtn_Click(sender As Object, e As EventArgs) Handles fluidbtn.Click
        Session("selected_fluidID") = ""
        Response.Redirect("Fluid.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim fluidIDlbl As Label = GridView1.Rows(index).FindControl("fluidIDlbl")
            Session("selected_fluidID") = fluidIDlbl.Text
            Response.Redirect("Fluid.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim fluidIDlbl As Label = GridView1.Rows(index).FindControl("fluidIDlbl")
            'remove fluid
            appcode.DeleteCompanyFluid(fluidIDlbl.Text)
            Response.Redirect("FluidList.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
