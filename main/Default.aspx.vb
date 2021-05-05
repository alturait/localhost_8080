Imports System.Data.SqlClient

Partial Class LT_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = appcode.GetCompany(Session("selected_companyID"))
    End Sub

    Protected Sub salesytdbtn_Click(sender As Object, e As EventArgs) Handles salesytdbtn.Click
        Response.Redirect("SalesYTD.aspx")
    End Sub
End Class
