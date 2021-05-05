Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class Locations
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("selected_companyID") <> "0" Then
                GridView2.DataSourceID = "SqlCustomerShipTos"
                GridView2.DataBind()
            ElseIf Session("selected_supplierID") <> "0" Then
                GridView2.DataSourceID = "SqlSupplierShipTos"
                GridView2.DataBind()
            End If
            Session("selected_locationID") = ""
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim shipIDlbl As Label = GridView2.Rows(index).FindControl("shipIDlbl")
            Session("selected_shipID") = shipIDlbl.Text
            Response.Redirect("Location.aspx")
        End If
    End Sub

    Protected Sub newlocationbtn_Click(sender As Object, e As EventArgs) Handles newlocationbtn.Click
        Session("selected_shipID") = ""
        Response.Redirect("Location.aspx")
    End Sub

End Class
