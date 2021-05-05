
Partial Class main_InactiveAccounts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim companyIDlbl As Label = GridView1.Rows(index).FindControl("companyIDlbl")
            Session("selected_companyID") = companyIDlbl.Text
            Session("chargetax") = appcode.getChargeTax(Session("selected_companyID"))
            Session("selected_shipID") = ""
            Session("selected_userID") = ""
            Session("warehousemfr") = ""
            Session("selected_supplierID") = "0"
            Response.Redirect("Default.aspx?menu=corders")
        End If
    End Sub

End Class
