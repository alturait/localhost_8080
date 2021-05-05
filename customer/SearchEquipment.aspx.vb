Imports System.Data.SqlClient
Imports System.IO

Partial Class customer_SearchEquipment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("this_locationID") = "0"
            If Request.QueryString("searchterm") <> "" Then
                searchtermlbl.Text = Request.QueryString("searchterm")
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("Asset.aspx")
        End If
    End Sub

End Class
