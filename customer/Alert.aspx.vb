
Partial Class customer_Alert
    Inherits System.Web.UI.Page

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If alerttb.Text <> "" Then
            Dim noteID As Integer = appcode.InsertAlert(Session("equipmentID"), Now(), appcode.GetUserName(Session("userID")), alerttb.Text)
            Response.Redirect("Asset.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = appcode.GetEquipment(Session("equipmentID"))
    End Sub
End Class
