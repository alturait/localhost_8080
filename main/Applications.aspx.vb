Imports System.Data.SqlClient

Partial Class Applications
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("oem") <> "" Then
                If Request.QueryString("model") <> "" Then
                    oemdd.SelectedValue = Request.QueryString("oem")
                    modeldd.SelectedValue = Request.QueryString("model")
                    GridView3.DataSourceID = "SqlEquipmentByModel"
                Else
                    oemdd.SelectedValue = Request.QueryString("oem")
                    GridView3.DataSourceID = "SqlEquipmentByOEM"
                End If
            Else
                GridView3.Visible = False
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub oemdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles oemdd.SelectedIndexChanged
        Response.Redirect("Applications.aspx?oem=" & oemdd.SelectedValue)
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "Select" Then
            Dim response_string = "ApplicationProfile.aspx?"
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipment_oemlbl As Label = GridView3.Rows(index).FindControl("equipment_oemlbl")
            Dim equipment_modellbl As Label = GridView3.Rows(index).FindControl("equipment_modellbl")
            Dim engine_oemlbl As Label = GridView3.Rows(index).FindControl("engine_oemlbl")
            Dim engine_modellbl As Label = GridView3.Rows(index).FindControl("engine_modellbl")
            Dim optionslbl As Label = GridView3.Rows(index).FindControl("optionslbl")
            Dim yearlbl As Label = GridView3.Rows(index).FindControl("yearlbl")
            Dim vinlbl As Label = GridView3.Rows(index).FindControl("vinlbl")
            response_string &= "oem=" & equipment_oemlbl.Text
            response_string &= "&model=" & equipment_modellbl.Text
            response_string &= "&eoem=" & engine_oemlbl.Text
            response_string &= "&emodel=" & engine_modellbl.Text
            response_string &= "&year=" & yearlbl.Text
            response_string &= "&options=" & yearlbl.Text
            response_string &= "&vin=" & vinlbl.Text
            Response.Redirect(response_string)
        End If
    End Sub

    Protected Sub modeldd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles modeldd.SelectedIndexChanged
        Response.Redirect("Applications.aspx?oem=" & oemdd.SelectedValue & "&model=" & modeldd.SelectedValue)
    End Sub

End Class
