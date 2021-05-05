
Partial Class EST_Customer
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        Response.Expires = -1500
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            If Session("userID").ToString <> "0" And Session("userID").ToString <> "" Then
                logobtn.ImageUrl = "../Images/" & Session("logo")
                logobtn.PostBackUrl = "../Catalog.aspx?categoryID=0"
                namelbl.Text = appcode.GetUserName(Session("userID"))
                myaccountbtn.Text = appcode.GetCompany(Session("companyID"))
                myaccountbtn.Enabled = Session("admin")
                If Session("admin") = False And appcode.isCustomer(Session("companyID")) = True Then
                    assetsdd.DataSourceID = "SqlAssetsByUser"
                    assetsdd.DataBind()
                End If
                If Session("equipmentID").ToString <> "" Then
                    assetsdd.SelectedValue = Session("equipmentID")
                End If
            Else
                Session("lastpage") = "main/Default.aspx"
                Response.Redirect("../Logon.aspx")
            End If
        End If
    End Sub

    Protected Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        Session.Abandon()
        Response.Redirect("Logon.aspx")
    End Sub

    Protected Sub homebtn_Click(sender As Object, e As EventArgs) Handles homebtn.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub equipmentbtn_Click(sender As Object, e As EventArgs) Handles equipmentbtn.Click
        Session("locationID") = ""
        Response.Redirect("MyEquipmentList.aspx")
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'sessionlbl.Text = "VendorID: " & Session("vendorID") & " | CompanyID: " & Session("selected_companyID") & " | LocationID: " & Session("this_locationID") & " | VendorID: " & Session("selected_vendorID") & " | UserID: " & Session("userID") & " | EquipmentID: " & Session("equipmentID") & " | ServiceID: " & Session("serviceID")
    End Sub

    Protected Sub myaccountbtn_Click(sender As Object, e As EventArgs) Handles myaccountbtn.Click
        Response.Redirect("Company.aspx")
    End Sub

    Protected Sub kitsbtn_Click(sender As Object, e As EventArgs) Handles kitsbtn.Click
        Response.Redirect("KitList2.aspx")
    End Sub

    Protected Sub assetsdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assetsdd.SelectedIndexChanged
        If assetsdd.SelectedValue <> "0" Then
            Session("equipmentID") = assetsdd.SelectedValue.ToString
            Response.Redirect("MyEquipment.aspx")
        End If
    End Sub

    Protected Sub addequipmentbtn_Click(sender As Object, e As EventArgs) Handles addequipmentbtn.Click
        Session("equipmentID") = ""
        Response.Redirect("MyEquipment.aspx")
    End Sub

    Protected Sub catalogbtn_Click(sender As Object, e As EventArgs) Handles catalogbtn.Click
        Response.Redirect("VCatalog.aspx?categoryID=0")
    End Sub

    Protected Sub favoritesbtn_Click(sender As Object, e As EventArgs) Handles favoritesbtn.Click
        Response.Redirect("Favorites.aspx")
    End Sub
End Class

