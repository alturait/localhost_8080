Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class MyEquipmentList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("equipmentID") = ""
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub addequipmentbtn_Click(sender As Object, e As EventArgs) Handles addequipmentbtn.Click
        Response.Redirect("FindEquipment.aspx")
    End Sub

    Protected Sub pbookbtn_Click(sender As Object, e As EventArgs) Handles pbookbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/EquipmentBook_" & Session("selected_companyID") & ".pdf"
        Dim imagefile As String = ""
        If File.Exists(Server.MapPath("~/Images/Banners/" & Session("selected_companyID").ToString & ".jpg")) = True Then
            imagefile = "Images/Banners/" & Session("selected_companyID").ToString & ".jpg"
        Else
            imagefile = ""
        End If
        appcode.EquipmentBookPDF(Session("selected_companyID"), locationdd.SelectedValue, Session("userID"), True, 0, applicationpath, filename, imagefile)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub sbookbtn_Click(sender As Object, e As EventArgs) Handles sbookbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/ServiceKits_" & Session("selected_companyID") & ".pdf"
        appcode.KitBookPDF(Session("selected_companyID"), locationdd.SelectedValue, applicationpath, filename, "Images/DFO_LOGO_v3.jpg", False)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub assetlistbtn_Click(sender As Object, e As EventArgs) Handles assetlistbtn.Click
        Response.Redirect("AssetList.aspx")
    End Sub

    Protected Sub assetdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assetdd.SelectedIndexChanged
        Session("equipmentID") = assetdd.SelectedValue
        Response.Redirect("EquipmentProfile.aspx")
    End Sub
End Class
