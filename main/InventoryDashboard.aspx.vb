Partial Class main_InventoryDashboard
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Session("imfr") = manufacturerlbl.Text
            Response.Redirect("Inventory.aspx")
        ElseIf e.CommandName = "Worksheet" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim filename As String = "PDF/Worksheet_" & manufacturerlbl.Text & ".pdf"
            appcode.DFOInventorySheetPDF(manufacturerlbl.Text, applicationpath, filename, "Images/DFO_LOGO_v3.jpg")
            Response.Redirect("../" & filename)
        End If
    End Sub
End Class
