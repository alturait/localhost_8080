
Partial Class NewCustomer
    Inherits System.Web.UI.Page

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        Dim companyID As Integer = appcode.InsertCompany(companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, c_phonetb.Text, c_faxtb.Text, True, "", Session("vendorID"), branchdd.SelectedValue)
        Dim shipID As Integer = appcode.InsertShipTo(companyID, shiptotb.Text, s_address1tb.Text, s_address2tb.Text, s_address3tb.Text, s_citytb.Text, s_statetb.Text, s_zipcodetb.Text, s_phonetb.Text)
        Dim userID As Integer = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, True, c_usernametb.Text)
        Dim usercompanyID As Integer = appcode.InsertUserCompany(assigneddd.SelectedValue, companyID)
        If appcode.IsEmailAvailable(c_emailtb.Text, userID) Then
            Response.Redirect("Default.aspx")
        Else
            appcode.DeleteCompany(companyID)
            appcode.DeleteUserCompany(usercompanyID)
            errormsglbl.Visible = True
        End If
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            assigneddd.SelectedValue = Session("userID")
            If appcode.isCustomer(Session("companyID")) = False And Session("admin") = False Then
                assigneddd.Enabled = False
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub sameascb_CheckedChanged(sender As Object, e As EventArgs) Handles sameascb.CheckedChanged
        If sameascb.Checked = True Then
            shiptotb.Text = companytb.Text
            s_address1tb.Text = address1tb.Text
            s_address2tb.Text = address2tb.Text
            s_citytb.Text = citytb.Text
            s_statetb.Text = statetb.Text
            s_zipcodetb.Text = zipcodetb.Text
            s_phonetb.Text = phonetb.Text
        Else
            shiptotb.Text = ""
            s_address1tb.Text = ""
            s_address2tb.Text = ""
            s_citytb.Text = ""
            s_statetb.Text = ""
            s_zipcodetb.Text = ""
            s_phonetb.Text = ""
        End If
    End Sub
End Class
