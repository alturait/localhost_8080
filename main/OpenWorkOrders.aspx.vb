
Partial Class main_OpenWorkOrders
    Inherits System.Web.UI.Page


    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim repairIDlbl As Label = GridView1.Rows(index).FindControl("repairIDlbl")
            Session("repairID") = repairIDlbl.Text
            Response.Redirect("EditWorkOrder.aspx?repairID=" & repairIDlbl.Text)
        ElseIf e.CommandName = "Print" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim repairIDlbl As Label = GridView1.Rows(index).FindControl("repairIDlbl")
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim filename As String = "PDF/WorkOrder" & repairIDlbl.Text & ".pdf"
            'appcode.WorkOrderPDF(repairIDlbl.Text, applicationpath, "Images/bannerlogo.jpg", filename)
            'Response.Redirect("../" & filename)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

End Class
