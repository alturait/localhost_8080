
Partial Class main_Admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "../../Images/DFOBanner.jpg"
            logobtn.PostBackUrl = "Default.aspx"
            If Session("userID").ToString <> "" Then
                logobtn.ImageUrl = "../../Images/DFO_LOGO_v3.jpg"
                logobtn.PostBackUrl = "Default.aspx"
                companydd.SelectedValue = Session("selected_companyID")
                If Session("selected_companyID") = "0" Then
                    cartbtn.Visible = False
                Else

                End If
                If Request.QueryString("menu") <> "" Then
                    Session("menu") = Request.QueryString("menu")
                End If
                If Session("menu").ToString = "catalog" Then
                    pnsearchpanel.Visible = True
                    submenulbl.Text = ""
                ElseIf Session("menu").ToString = "sales" Then
                    submenulbl.Text = "<a href='NewOrders.aspx' style='color: #FFFFFF'>New Orders</a>&nbsp;|&nbsp;<a href='OpenOrdersBO.aspx' style='color: #FFFFFF'>Back Orders</a>&nbsp;|&nbsp;<a href='OpenOrdersRTS.aspx' style='color: #FFFFFF'>Ready To Ship</a>&nbsp;|&nbsp;<a href='ActiveQuotes.aspx' style='color: #FFFFFF'>Quotes</a>&nbsp;|&nbsp;<a href='AccountsByRep.aspx' style='color: #FFFFFF'>Customer Sales</a>"
                ElseIf Session("menu").ToString = "marketing" Then
                    submenulbl.Text = "<a href='MailingList.aspx' style='color: #FFFFFF'>Mailing List</a>&nbsp;|&nbsp;<a href='Flyer.aspx' style='color: #FFFFFF'>Sales Flyers</a>&nbsp;|&nbsp;<a href='FeaturedProducts.aspx' style='color: #FFFFFF'>Featured Products</a>&nbsp;|&nbsp;<a href='CatalogItems.aspx' style='color: #FFFFFF'>Catalog</a>"
                ElseIf Session("menu").ToString = "lubetracker" Then
                    lubetrackerpanel.Visible = True
                    submenulbl.Text = "<a href='OpenKits.aspx' style='color: #FFFFFF'>Pending Kits</a>&nbsp;|&nbsp;<a href='Assets.aspx' style='color: #FFFFFF'>Asset List</a>&nbsp;|&nbsp;<a href='PartSummary.aspx' style='color: #FFFFFF'>Part Summary</a>&nbsp;|&nbsp;<a href='ModifyAllKits.aspx' style='color: #FFFFFF'>Kit Tools</a>"
                ElseIf Session("menu").ToString = "purchasing" Then
                    submenulbl.Text = "<a href='InventoryDashboard.aspx' style='color: #FFFFFF'>Inventory</a>&nbsp;|&nbsp;<a href='RequisitionDashboard.aspx' style='color: #FFFFFF'>Requirements</a>&nbsp;|&nbsp;<a href='PurchaseOrders.aspx' style='color: #FFFFFF'>Purchase Orders</a>&nbsp;|&nbsp;<a href='POHistory.aspx' style='color: #FFFFFF'>History</a>&nbsp;|&nbsp;<a href='MySuppliers.aspx' style='color: #FFFFFF'>Suppliers</a>"
                ElseIf Session("menu").ToString = "receiving" Then
                    submenulbl.Text = "<a href='PurchaseOrders.aspx' style='color: #FFFFFF'>Purchase Orders</a>&nbsp;|&nbsp;<a href='OpenReturns.aspx?categoryID=0' style='color: #FFFFFF'>Product Returns</a>"
                ElseIf Session("menu").ToString = "shipping" Then
                    submenulbl.Text = "<a href='OpenOrders.aspx' style='color: #FFFFFF'>Open Orders</a>&nbsp;|&nbsp;<a href='OpenOrdersRTS.aspx' style='color: #FFFFFF'>Ready To Ship</a>&nbsp;|&nbsp;<a href='Shipments.aspx' style='color: #FFFFFF'>Picked Orders</a>"
                ElseIf Session("menu").ToString = "accounting" Then
                    submenulbl.Text = "<a href='Billing.aspx' style='color: #FFFFFF'>Ready to Invoice</a>"
                ElseIf Session("menu").ToString = "csettings" Then
                    submenulbl.Text = "<a href='Company.aspx' style='color: #FFFFFF'>Company Info</a>&nbsp;|&nbsp;<a href='Locations.aspx' style='color: #FFFFFF'>Locations</a>&nbsp;|&nbsp;<a href='Contacts.aspx' style='color: #FFFFFF'>Users</a>&nbsp;|&nbsp;<a href='Discounts.aspx' style='color: #FFFFFF'>Discounts</a>"
                ElseIf Session("menu").ToString = "stockroom" Then
                    submenulbl.Text = "Stock Room"
                ElseIf Session("menu").ToString = "corders" Then
                    customerddpanel.Visible = True
                    submenulbl.Text = "<a href='Orders.aspx' style='color: #FFFFFF'>Open Orders</a>&nbsp;|&nbsp;<a href='OrderHistory.aspx' style='color: #FFFFFF'>Order History</a>&nbsp;|&nbsp;<a href='CustomerInvoices.aspx' style='color: #FFFFFF'>Invoices</a>&nbsp;|&nbsp;<a href='Quotes.aspx' style='color: #FFFFFF'>Quotes</a>&nbsp;|&nbsp;<a href='Returns.aspx' style='color: #FFFFFF'>Returns</a>&nbsp;|&nbsp;<a href='Usage.aspx' style='color: #FFFFFF'>Usage</a>"
                ElseIf Session("menu").ToString = "crequirements" Then
                    submenulbl.Text = "Requirements"
                ElseIf Session("menu").ToString = "home" Then
                    submenulbl.Text = ""
                Else
                    submenulbl.Text = ""
                End If
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
        Session("selected_companyID") = companydd.SelectedValue
        If companydd.SelectedValue <> "0" Then
            Session("chargetax") = appcode.getChargeTax(Session("selected_companyID"))
            Session("selected_shipID") = ""
            Session("selected_userID") = ""
            Session("warehousemfr") = ""
            Session("selected_supplierID") = "0"
        Else
            pnsearchpanel.Visible = False
            lubetrackerpanel.Visible = False
            submenulbl.Text = ""
        End If
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub newcustomerbtn_Click(sender As Object, e As EventArgs) Handles newcustomerbtn.Click
        Response.Redirect("NewCustomer.aspx")
    End Sub

    Protected Sub advsearchbtn_Click(sender As Object, e As EventArgs) Handles advsearchbtn.Click
        Response.Redirect("AdvancedSearch.aspx")
    End Sub

    Protected Sub logobtn_Click(sender As Object, e As ImageClickEventArgs) Handles logobtn.Click
        Session("selected_companyID") = "0"
        Session("menu") = ""
        Session("chargetax") = True
        Session("selected_shipID") = ""
        Session("selected_userID") = ""
        Session("warehousemfr") = ""
        Session("selected_supplierID") = "0"
        pnsearchpanel.Visible = False
        lubetrackerpanel.Visible = False
        Response.Redirect("Default.aspx")
    End Sub
End Class

