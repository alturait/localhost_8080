
Partial Class CreateAccount
    Inherits System.Web.UI.Page

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        Dim companyID As Integer = appcode.InsertCompany(companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, c_phonetb.Text, c_faxtb.Text, True, "", 98, 7307)
        Dim shipID As Integer = appcode.InsertShipTo(companyID, shiptotb.Text, s_address1tb.Text, s_address2tb.Text, s_address3tb.Text, s_citytb.Text, s_statetb.Text, s_zipcodetb.Text, s_phonetb.Text)
        'Dim userID As Integer = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, True)
        Dim userID As Integer = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, True, c_emailtb.Text)
        Dim usercompanyID As Integer = appcode.InsertUserCompany(3266, companyID)
        If appcode.IsEmailAvailable(c_emailtb.Text, userID) Then
            Session("userID") = userID.ToString
            Session("admin") = True
            Session("companyID") = companyID
            Session("selected_companyID") = companyID
            Session("vendorID") = 7307
            If appcode.IsGuestCart(Session("ssID")) = True Then
                'main.ConvertGuestCart(Session("ssID"), Session("vendorID"), Session("companyID"), Session("userID"))
                'above line removed main and ConvertGuestCart are not found
                Response.Redirect("customer/Cart.aspx")
            End If
            Response.Redirect("customer/Default.aspx")
        Else
            'main.DeleteCompany(companyID)
            'main.DeleteUserCompany(usercompanyID)
            ' the above two lines were removed, main is not defined
            errormsglbl.Visible = True
        End If
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
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
