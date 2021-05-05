
Partial Class EST_Anonymous
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        Response.Expires = -1500
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            logobtn.ImageUrl = "../Images/DFOBanner.jpg"
            logobtn.PostBackUrl = "../Default.aspx"
            If Session("userID").ToString = "0" Or Session("userID").ToString = "" Then
                Response.Redirect("Logon.aspx")
            End If
        End If
    End Sub

End Class

