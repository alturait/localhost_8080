Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class customer_Assets
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("equipment_type") <> "" Then
                typedd.SelectedValue = Request.QueryString("equipment_type")
                GridView3.DataSourceID = "SqlEquipmentByType"
                GridView3.DataBind()
            Else
                If Session("this_locationID") <> "0" And Session("this_locationID") <> "" Then
                    locationdd.SelectedValue = Session("this_locationID")
                    GridView3.DataSourceID = "SqlEquipmentByLocation"
                    GridView3.DataBind()
                End If
            End If
            Session("equipmentID") = ""
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("Asset.aspx")
        ElseIf e.CommandName = "Kits" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("Kits.aspx?equipmentID=" & equipmentIDlbl.Text)
        End If
    End Sub

    Protected Sub locationdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles locationdd.SelectedIndexChanged
        Session("this_locationID") = locationdd.SelectedValue
        If locationdd.SelectedValue = 0 Then
            If Session("admin") = False And Session("company_role") = "Customer" Then
                GridView3.DataSourceID = "SqlEquipmentByUser"
                GridView3.DataBind()
            Else
                GridView3.DataSourceID = "SqlEquipment"
                GridView3.DataBind()
            End If
        Else
            GridView3.DataSourceID = "SqlEquipmentByLocation"
            GridView3.DataBind()
        End If
    End Sub

    Protected Sub addequipmentbtn_Click(sender As Object, e As EventArgs) Handles addequipmentbtn.Click
        Response.Redirect("FindEquipment.aspx")
    End Sub

    Protected Sub typedd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles typedd.SelectedIndexChanged
        If typedd.SelectedValue <> "0" Then
            Response.Redirect("Assets.aspx?equipment_type=" & typedd.SelectedValue)
        Else
            Response.Redirect("Assets.aspx")
        End If
    End Sub

    Protected Sub assetdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assetdd.SelectedIndexChanged
        Session("equipmentID") = assetdd.SelectedValue
        Response.Redirect("Asset.aspx")
    End Sub

    Protected Sub selectall_CheckedChanged(sender As Object, e As EventArgs) Handles selectall.CheckedChanged
        For Each row In GridView3.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            selectcb.Checked = selectall.Checked
        Next
    End Sub

    Protected Sub changebtn_Click(sender As Object, e As EventArgs) Handles changebtn.Click
        If changetb.Text <> "" Then
            'update equipment descriptions
            For Each row In GridView3.Rows
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim equipmentIDlbl As Label = row.FindControl("equipmentIDlbl")
                If selectcb.Checked = True Then
                    appcode.UpdateEquipmentDescription(equipmentIDlbl.Text, changetb.Text)
                End If
            Next
            Response.Redirect("Assets.aspx")
        End If
    End Sub

    Protected Sub movebtn_Click(sender As Object, e As EventArgs) Handles movebtn.Click
        If newlocationdd.SelectedValue <> "0" Then
            'update equipment locations
            For Each row In GridView3.Rows
                Dim equipmentIDlbl As Label = row.FindControl("equipmentIDlbl")
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                If selectcb.Checked = True Then
                    appcode.UpdateEquipmentLocation(equipmentIDlbl.Text, newlocationdd.SelectedValue)
                End If
            Next
            Response.Redirect("Assets.aspx")
        End If
    End Sub
End Class