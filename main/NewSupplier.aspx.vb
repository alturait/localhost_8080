
Partial Class EST_NewSupplier
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        Dim companyID As Integer = appcode.InsertSupplier(companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, c_phonetb.Text, c_faxtb.Text, True, "", Session("vendorID"), 0)
        Dim shipID As Integer = appcode.InsertShipTo(companyID, shiptotb.Text, s_address1tb.Text, s_address2tb.Text, s_address3tb.Text, s_citytb.Text, s_statetb.Text, s_zipcodetb.Text, s_phonetb.Text)
        Dim userID As Integer = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, True, c_emailtb.Text)
        Dim vsID As Integer = appcode.InsertVendorSupplier(companyID, Session("selected_vendorID"))
        Response.Redirect("MySuppliers.aspx")
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        pagelbl.Text = Page.Title
    End Sub
End Class
