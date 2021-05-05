Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_AssetList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("equipment_type") <> "" Then
                typedd.SelectedValue = Request.QueryString("equipment_type")
                GridView3.DataSourceID = "SqlEquipmentByType"
                GridView3.DataBind()
                efficiencylbl.Text = FormatPercent(appcode.CheckEquipmentKitStockEfficiency(Session("selected_companyID"), Request.QueryString("equipment_type")), 0)
            Else
                If Session("this_locationID") <> "0" And Session("this_locationID") <> "" Then
                    locationdd.SelectedValue = Session("this_locationID")
                    GridView3.DataSourceID = "SqlEquipmentByLocation"
                    GridView3.DataBind()
                    efficiencylbl.Text = FormatPercent(appcode.CheckLocationKitStockEfficiency(Session("selected_companyID"), locationdd.SelectedValue), 0)
                Else
                    efficiencylbl.Text = FormatPercent(appcode.CheckTotalKitStockEfficiency(Session("selected_companyID")), 0)
                End If
                If Session("hidecb") = True Then
                    hidecb.Checked = True
                    GridView3.DataSourceID = "SqlEquipmentJobOnly"
                    GridView3.DataBind()
                Else
                    hidecb.Checked = False
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
            Response.Redirect("EquipmentProfile.aspx")
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
        Response.Redirect("AssetList.aspx?equipment_type=" & typedd.SelectedValue)
    End Sub

    Protected Sub pbookbtn_Click(sender As Object, e As EventArgs) Handles pbookbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/EquipmentBook_" & Session("selected_companyID") & ".pdf"
        Dim imagefile As String = ""
        If File.Exists(Server.MapPath("~/Images/Banners/" & Session("selected_companyID").ToString & ".jpg")) = True Then
            imagefile = "Images/Banners/" & Session("selected_companyID").ToString & ".jpg"
        Else
            imagefile = "Images/bannerlogo.jpg"
        End If
        appcode.EquipmentBookPDF(Session("selected_companyID"), locationdd.SelectedValue, Session("userID"), True, 0, applicationpath, filename, imagefile)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub sbookbtn_Click(sender As Object, e As EventArgs) Handles sbookbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/ServiceKits_" & Session("selected_companyID") & ".pdf"
        appcode.KitBookPDF(Session("selected_companyID"), locationdd.SelectedValue, applicationpath, filename, "Images/DFO_LOGO_v3.jpg", False)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub hidecb_CheckedChanged(sender As Object, e As EventArgs) Handles hidecb.CheckedChanged
        If hidecb.Checked = True Then
            Session("hidecb") = True
        Else
            Session("hidecb") = False
        End If
        Response.Redirect("Assets.aspx")
    End Sub

    Protected Sub updatehoursbtn_Click(sender As Object, e As EventArgs) Handles updatehoursbtn.Click
        For Each row In GridView3.Rows
            Dim equipmentIDlbl As Label = row.FindControl("equipmentIDlbl")
            Dim interval_rootlbl As Label = row.FindControl("interval_rootlbl")
            Dim lasthourstb As Label = row.FindControl("lasthourstb")
            Dim updatehourstb As TextBox = row.FindControl("updatehourstb")
            If IsNumeric(updatehourstb.Text) = True Then
                appcode.UpdateHoursMiles(equipmentIDlbl.Text, updatehourstb.Text)
                If IsNumeric(lasthourstb.Text) = True Then
                    If updatehourstb.Text > lasthourstb.Text Then
                        If (updatehourstb.Text - lasthourstb.Text) > interval_rootlbl.Text Then
                            appcode.UpdateServiceFlagA(equipmentIDlbl.Text, True)
                        End If
                    End If
                End If
            End If

        Next
        Response.Redirect("AssetList.aspx")
    End Sub
End Class
