Imports System.Data.SqlClient

Partial Class main_AdvancedSearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("this_locationID") = "0"
            If Request.QueryString("searchterm") <> "" Then
                searchterm.Text = Request.QueryString("searchterm")
            End If
            If Session("selected_companyID") = "0" Then
                GridView3.DataSourceID = "SqlAllEquipment"
                GridView3.DataBind()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        Response.Redirect("AdvancedSearch.aspx?searchterm=" & searchterm.Text)
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

End Class
