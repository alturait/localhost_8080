
Partial Class main_ModifyAllKits
    Inherits System.Web.UI.Page

    Protected Sub replacebtn_Click(sender As Object, e As EventArgs) Handles replacebtn.Click
        'is it a valid part number
        Dim validpn As Boolean = False
        validpn = appcode.IsPartnumber(rmanufactuertb.Text, rpartnumbnertb.Text)
        If validpn = True Then
            'replace the part number in all kits
            errmsg1.Visible = False
            For Each row In GridView1.Rows
                Dim equipmentIDlbl As Label = row.findControl("equipmentIDlbl")
                Dim selectcb As CheckBox = row.findControl("selectcb")
                If selectcb.Checked = True Then
                    appcode.ReplacePartNumbers(Session("selected_companyID"), rmanufactuertb.Text, rpartnumbnertb.Text, mfrdd.SelectedValue, partnumberdd.SelectedValue, equipmentIDlbl.Text)
                End If
            Next
            rmanufactuertb.Text = "MFR"
            rpartnumbnertb.Text = "PART NUMBER"
        Else
            'show an error message
            errmsg1.Visible = True
        End If
    End Sub

    Protected Sub rmanufactuertb_TextChanged(sender As Object, e As EventArgs) Handles rmanufactuertb.TextChanged
        rmanufactuertb.Text = rmanufactuertb.Text.ToUpper()
        rpartnumbnertb.Focus()
    End Sub

    Protected Sub rpartnumbnertb_TextChanged(sender As Object, e As EventArgs) Handles rpartnumbnertb.TextChanged
        rpartnumbnertb.Text = rpartnumbnertb.Text.ToUpper
        replacebtn.Focus()
    End Sub

    Protected Sub mfrdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mfrdd.SelectedIndexChanged

    End Sub

    Protected Sub partnumberdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles partnumberdd.SelectedIndexChanged
        GridView1.DataBind()
    End Sub

    Protected Sub selectallcb_CheckedChanged(sender As Object, e As EventArgs) Handles selectallcb.CheckedChanged
        If selectallcb.Checked = True Then
            For Each row In GridView1.Rows
                Dim selectcb As CheckBox = row.findControl("selectcb")
                selectcb.Checked = True
            Next
        Else
            For Each row In GridView1.Rows
                Dim selectcb As CheckBox = row.findControl("selectcb")
                selectcb.Checked = False
            Next
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView1.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub
End Class
