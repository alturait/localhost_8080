Imports System.IO

Partial Class EST_Vendor
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
        Response.Expires = -1500
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            If Session("userID").ToString <> "" Then
                logobtn.ImageUrl = "../Images/bannerlogo.jpg"
                logobtn.PostBackUrl = "../Catalog.aspx?categoryID=0"
                namelbl.Text = appcode.GetUserName(Session("userID"))
                mycompanybtn.Text = appcode.GetCompany(Session("companyID"))
                If Session("admin") = False Then
                    companydd.DataSourceID = "SqlUserCompanies"
                    companydd.DataBind()
                End If
                If Session("selected_companyID") <> "0" Then
                    companydd.SelectedValue = Session("selected_companyID")
                    pobtn.Visible = False
                    vordersbtn.Visible = False
                    shipmentsbtn.Visible = False
                    invoicesbtn.Visible = False
                    flyersbtn.Visible = False
                Else
                    cartbtn.Visible = False
                    ordersbtn.Visible = False
                    assetbtn.Visible = False
                    kitsbtn.Visible = False
                    favoritesbtn.Visible = False
                End If
            Else
                Session.Abandon()
                Response.Redirect("Logon.aspx")
            End If
        End If
        sessionlbl.Text = "VendorID: " & Session("vendorID") & " | CompanyID: " & Session("companyID") & " | Selected CompanyID: " & Session("selected_companyID") & " | LocationID: " & Session("this_locationID") & " | UserID: " & Session("userID") & " | Selected UserID: " & Session("selected_userID") & "<br/>EquipmentID: " & Session("equipmentID") & " | ServiceID: " & Session("serviceID") & " | ServiceProfileID: " & Session("serviceprofileID") & " | PartID: " & Session("partID")
    End Sub

    Protected Sub favoritesbtn_Click(sender As Object, e As EventArgs) Handles favoritesbtn.Click
        Response.Redirect("Favorites.aspx")
    End Sub

    Protected Sub assetbtn_Click(sender As Object, e As EventArgs) Handles assetbtn.Click
        Response.Redirect("MyEquipmentList.aspx")
    End Sub

    Protected Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        Session.Abandon()
        Response.Redirect("../Default.aspx")
    End Sub

    Protected Sub pobtn_Click(sender As Object, e As EventArgs) Handles pobtn.Click
        Response.Redirect("PurchaseOrders.aspx")
    End Sub

    Protected Sub vordersbtn_Click(sender As Object, e As EventArgs) Handles vordersbtn.Click
        Response.Redirect("OpenOrders.aspx")
    End Sub

    Protected Sub shipmentsbtn_Click(sender As Object, e As EventArgs) Handles shipmentsbtn.Click
        Response.Redirect("Shipments.aspx")
    End Sub

    Protected Sub invoicesbtn_Click(sender As Object, e As EventArgs) Handles invoicesbtn.Click
        Response.Redirect("Invoices.aspx")
    End Sub

    Protected Sub companydd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles companydd.SelectedIndexChanged
        If companydd.SelectedValue <> "0" Then
            Session("selected_companyID") = companydd.SelectedValue
            Session("selected_shipID") = ""
            Session("selected_userID") = ""
        Else
            Session("selected_companyID") = "0"
        End If
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub newcustomerbtn_Click(sender As Object, e As EventArgs) Handles newcustomerbtn.Click
        Session("selected_companyID") = "0"
        Response.Redirect("NewCustomer.aspx")
    End Sub

    Protected Sub homebtn_Click(sender As Object, e As EventArgs) Handles homebtn.Click
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub accountbtn_Click(sender As Object, e As EventArgs) Handles accountbtn.Click
        Response.Redirect("Company.aspx")
    End Sub

    Protected Sub catalogbtn_Click(sender As Object, e As EventArgs) Handles catalogbtn.Click
        Response.Redirect("VCatalog.aspx?categoryID=0")
    End Sub

    Protected Sub cartbtn_Click(sender As Object, e As EventArgs) Handles cartbtn.Click
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub ordersbtn_Click(sender As Object, e As EventArgs) Handles ordersbtn.Click
        Response.Redirect("Orders.aspx")
    End Sub

    Protected Sub kitsbtn_Click(sender As Object, e As EventArgs) Handles kitsbtn.Click
        Response.Redirect("KitList2.aspx")
    End Sub

    Protected Sub flyersbtn_Click(sender As Object, e As EventArgs) Handles flyersbtn.Click
        Response.Redirect("Flyer.aspx")
    End Sub
End Class

