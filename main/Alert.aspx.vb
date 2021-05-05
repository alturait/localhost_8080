
Partial Class main_Alert
    Inherits System.Web.UI.Page

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If alerttb.Text <> "" Then
            Dim noteID As Integer = appcode.InsertAlert(Session("equipmentID"), Now(), appcode.GetUserName(Session("userID")), alerttb.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub
End Class
