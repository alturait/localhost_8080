
Partial Class CustomerMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "../Images/lubetrackermobile.jpg"
            logobtn.PostBackUrl = "Order.aspx"
            namelbl.Text = Session("mobile_user")
            companylbl.Text = Session("mobile_company")
        End If
    End Sub

    Protected Sub logoffbtn_Click(sender As Object, e As EventArgs) Handles logoffbtn.Click
        Response.Redirect("../customer/Default.aspx")
    End Sub

End Class

