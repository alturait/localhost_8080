
Partial Class mobile_Pending
    Inherits System.Web.UI.Page

    Protected Sub pendingbtn_Click(sender As Object, e As EventArgs) Handles pendingbtn.Click
        Response.Redirect("Order.aspx")
    End Sub
End Class
