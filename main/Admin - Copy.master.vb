
Partial Class main_Admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "../Images/DFOBanner.jpg"
            logobtn.PostBackUrl = "Default.aspx"
            'ssIDlbl.Text = Session("ssID")
            If Session("userID").ToString <> "" Then
                logobtn.ImageUrl = "../Images/DFO_LOGO_v3.jpg"
                logobtn.PostBackUrl = "Default.aspx0"
                If Session("admin") = False Then
                    companydd.DataSourceID = "SqlUserCompanies"
                    companydd.DataBind()
                End If
                If Session("selected_companyID") <> "0" Then
                    companydd.SelectedValue = Session("selected_companyID")
                    newcustomerbtn.Visible = False
                    cartcountlbl.Text = " (" & appcode.GetCartCount(Session("userID"), Session("selected_companyID")) & ")"
                    favcountlbl.Text = " (" & appcode.GetFavoriteCount(Session("selected_companyID")) & ")"
                    assetslbl.Text = "Assets (" & appcode.GetAssetCount(Session("selected_companyID")) & ")"
                    partslbl.Text = "Parts (" & appcode.GetPartsCount(Session("selected_companyID")) & ")"
                Else
                    cartbtn.Visible = False
                    cartcountlbl.Visible = False
                    favcountlbl.Visible = False
                    favbtn.Visible = False
                    Panel1.Visible = False
                End If
                openorderslbl.Text = "Open Orders (" & appcode.GetOpenOrdersCount(Session("selected_companyID")) & ")"
                neworderslbl.Text = "New Orders (" & appcode.GetNewOrdersCount(Session("selected_companyID")) & ")"
                returnslbl.Text = "Returns (" & appcode.GetReturnsCount(Session("selected_companyID")) & ")"
                shiplbl.Text = "Freight (" & appcode.GetShipmentsCount(Session("selected_companyID")) & ")"
                'shiplbl.Text = "Freight (" & appcode.GetShipmentCount(Session("selected_companyID")) & ")"
                invoicelbl.Text = "Ready To Ship (" & appcode.GetInvoicesCount(Session("selected_companyID")) & ")"
                billinglbl.Text = "Invoicing (" & appcode.GetBillingCount(Session("selected_companyID")) & ")"
                quoteslbl.Text = "Quotes (" & appcode.GetQuotesCount(Session("selected_companyID")) & ")"
                inventorylbl.Text = "Manage Inventory (" & appcode.GetInventoryCount(Session("selected_companyID")) & ")"
                requisitionlbl.Text = "Manage Requirements (" & appcode.GetRequisitionsCount("") & ")"
                poslbl.Text = "Purchase Orders (" & appcode.GetOpenPOs() & ")"
            Else
                Session.Abandon()
                Response.Redirect("Logon.aspx")
            End If
        End If
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        Dim searchfor As String = Trim(searchterm.Text)
        Response.Redirect("VCatalog.aspx?search=" & searchfor & "&categoryID=0")
    End Sub

    Protected Sub cartbtn_Click(sender As Object, e As EventArgs) Handles cartbtn.Click
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub logoffbtn_Click(sender As Object, e As EventArgs) Handles logoffbtn.Click
        Session.Abandon()
        Response.Redirect("../Default.aspx")
    End Sub

    Protected Sub companydd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles companydd.SelectedIndexChanged
        If companydd.SelectedValue <> "0" Then
            Session("selected_companyID") = companydd.SelectedValue
            Session("chargetax") = appcode.getChargeTax(Session("selected_companyID"))
            Session("selected_shipID") = ""
            Session("selected_userID") = ""
            Session("warehousemfr") = ""
            Session("selected_supplierID") = "0"
        Else
            Session("selected_companyID") = "0"
        End If
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub newcustomerbtn_Click(sender As Object, e As EventArgs) Handles newcustomerbtn.Click
        Response.Redirect("NewCustomer.aspx")
    End Sub

    Protected Sub favbtn_Click(sender As Object, e As EventArgs) Handles favbtn.Click
        Response.Redirect("Favorites.aspx")
    End Sub

    Protected Sub advsearchbtn_Click(sender As Object, e As EventArgs) Handles advsearchbtn.Click
        Response.Redirect("AdvancedSearch.aspx")
    End Sub
End Class

