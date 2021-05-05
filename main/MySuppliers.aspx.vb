
Partial Class EST_MySuppliers
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim companyIDlbl As Label = GridView1.Rows(index).FindControl("companyIDlbl")
            Session("selected_supplierID") = companyIDlbl.Text
            Response.Redirect("EditSupplier.aspx")
        ElseIf e.CommandName = "History" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim companylbl As Label = GridView1.Rows(index).FindControl("companylbl")
            Session("selected_supplier") = companylbl.Text
            Response.Redirect("SupplierHistory.aspx")
        End If
    End Sub

    Protected Sub newbtn_Click(sender As Object, e As EventArgs) Handles newbtn.Click
        Response.Redirect("NewSupplier.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub
End Class
