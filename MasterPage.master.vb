
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "Images/DFO_LOGO_v3.jpg"
            logobtn.PostBackUrl = "Default.aspx"
            phonelbl.Text = Application("site_phone")
            'ssIDlbl.Text = Session("ssID")
        End If
    End Sub

    Protected Sub lubetrackerbtn_Click(sender As Object, e As EventArgs) Handles lubetrackerbtn.Click
        Response.Redirect("Logon.aspx")
    End Sub

End Class

