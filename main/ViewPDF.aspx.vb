Imports System.IO

Partial Class main_ViewPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/Kit_" & Request.QueryString("serviceprofileID") & ".pdf"
        Dim imagefile As String = ""
        If File.Exists(Server.MapPath("~/Images/Banners/" & Request.QueryString("companyID").ToString & ".jpg")) = True Then
            imagefile = "Images/Banners/" & Request.QueryString("companyID").ToString & ".jpg"
        Else
            imagefile = "Images/bannerlogo.jpg"
        End If
        Session("serviceprofileID") = Request.QueryString("serviceprofileID")
        appcode.ServiceKitPDF(Session("serviceprofileID"), applicationpath, filename, imagefile, True)
        Response.Redirect("../" & filename)
    End Sub
End Class
