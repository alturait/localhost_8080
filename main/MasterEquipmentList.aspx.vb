
Partial Class main_MasterEquipmentList
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Model" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim oemlbl As Label = GridView1.Rows(index).FindControl("oemlbl")
            Dim modellbl As Label = GridView1.Rows(index).FindControl("modellbl")
            Response.Redirect("EquipmentByModel.aspx?model=" & modellbl.Text)
        End If
    End Sub

    Protected Sub oemdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles oemdd.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
End Class
