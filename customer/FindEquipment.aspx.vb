
Partial Class customer_FindEquipment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            searchtermlbl.Text = Request.QueryString("searchterm")
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("CopyEquipment.aspx")
        End If
    End Sub

    Protected Sub esearchbtn_Click(sender As Object, e As EventArgs) Handles esearchbtn.Click
        Response.Redirect("FindEquipment.aspx?searchterm=" & esearchterm.Text)
    End Sub

    Protected Sub newbtn_Click(sender As Object, e As EventArgs) Handles newbtn.Click
        Session("equipmentID") = ""
        Response.Redirect("EditEquipment.aspx")
    End Sub
End Class
