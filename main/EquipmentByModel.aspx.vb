
Partial Class main_EquipmentByModel
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Profile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView1.Rows(index).FindControl("equipmentIDlbl")
            Dim companyIDlbl As Label = GridView1.Rows(index).FindControl("companyIDlbl")
            Session("selected_companyID") = companyIDlbl.Text
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Request.QueryString("oem") & " " & Request.QueryString("model")
    End Sub
End Class
