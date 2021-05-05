
Partial Class main_RequisitionDashboard
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Session("imfr") = manufacturerlbl.Text
            Response.Redirect("Requisitions.aspx")
        End If
    End Sub

End Class
