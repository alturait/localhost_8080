
Partial Class main_Admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "../Images/DFOBanner.jpg"
            logobtn.PostBackUrl = "Default.aspx"
            searchdd.SelectedValue = Session("searchdd")
            If Session("userID").ToString <> "" Then
                logobtn.ImageUrl = "../Images/DFO_LOGO_v3.jpg"
                logobtn.PostBackUrl = "Default.aspx"
                namelbl.Text = appcode.GetUserName(Session("userID"))
                If Session("selected_companyID") <> "0" Then
                    cartcountlbl.Text = " (" & appcode.GetCartCount(Session("userID"), Session("selected_companyID")) & " Items)"
                End If
                If Session("admin") = False Then
                    companydd.DataSourceID = "SqlUserCompanies"
                    companydd.DataBind()
                    newcustomerbtn.Visible = False
                End If
                companydd.SelectedValue = Session("selected_companyID")
                If Session("selected_companyID") = "0" Then
                    companydd.Visible = False
                    cartbtn.Visible = False
                    newcustomerbtn.Visible = False
                End If
                If Request.QueryString("menu") <> "" Then
                    Session("menu") = Request.QueryString("menu")
                End If
                If Session("menu").ToString = "catalog" Then
                    submenulbl.Text = "<B>CATALOG</B> "
                    If Session("admin") = "true" Then
                        submenulbl.Text += "<a href='Categories.aspx?categoryID=0' style='color: #FFFFFF'>Categories</a>&nbsp;|&nbsp;<a href='FeaturedProducts.aspx' style='color: #FFFFFF'>Featured</a>&nbsp;|&nbsp;<a href='ClearanceProducts.aspx' style='color: #FFFFFF'>Clearance</a>&nbsp;|&nbsp;"
                    End If
                    submenulbl.Text += "<a href='ProductByMfr.aspx' style='color: #FFFFFF'>By Manufacturer</a>&nbsp;|&nbsp;<a href='Favorites.aspx' style='color: #FFFFFF'>Favorites</a>"
                ElseIf Session("menu").ToString = "web" Then
                    submenulbl.Text = "<b>MARKETING</b> <a href='RecentLogins.aspx' style='color: #FFFFFF'>Recent Activity</a>&nbsp;|&nbsp;<a href='RecentErrors.aspx' style='color: #FFFFFF'>Recent Errors</a>&nbsp;|&nbsp;<a href='MailingLists.aspx' style='color: #FFFFFF'>Mailing Lists</a>&nbsp;|&nbsp;<a href='Flyer.aspx' style='color: #FFFFFF'>Email Flyers</a>&nbsp;|&nbsp;<a href='PDF.aspx' style='color: #FFFFFF'>PDF</a>"
                ElseIf Session("menu").ToString = "sales" Then
                    submenulbl.Text = "<b><a href='SalesHome.aspx' style='color: #FFFFFF'>SALES</a></b> <a href='NewOrders.aspx' style='color: #FFFFFF'>New Orders</a>&nbsp;|&nbsp;<a href='AllDiscounts.aspx' style='color: #FFFFFF'>Discounts</a>&nbsp;|&nbsp;<a href='NeedsPO.aspx' style='color: #FFFFFF'>Needs PO</a>&nbsp;|&nbsp;<a href='ActiveQuotes.aspx' style='color: #FFFFFF'>Quotes</a>&nbsp;|&nbsp;<a href='AccountsByRep.aspx' style='color: #FFFFFF'>Sales By Customer</a>&nbsp;|&nbsp;<a href='InactiveAccounts.aspx' style='color: #FFFFFF'>New/Inactive Accounts</a>"
                ElseIf Session("menu").ToString = "lubetracker1" Then
                    submenulbl.Text = "<B>LUBETRACKER</B> <a href='MasterEquipmentList.aspx' style='color: #FFFFFF'>Master Equipment List</a>&nbsp;|&nbsp;<a href='OpenKits.aspx' style='color: #FFFFFF'>Pending Kits</a>"
                ElseIf Session("menu").ToString = "lubetracker2" Then
                    submenulbl.Text = "<B>LUBETRACKER</B> <a href='AssetList.aspx' style='color: #FFFFFF'>Asset List</a>&nbsp;|&nbsp;<a href='ServicesDue.aspx' style='color: #FFFFFF'>Services Due</a>&nbsp;|&nbsp;<a href='OpenKits.aspx' style='color: #FFFFFF'>Pending Kits</a>&nbsp;|&nbsp;<a href='PartSummary.aspx' style='color: #FFFFFF'>Part Summary</a>&nbsp;|&nbsp;<a href='ModifyAllKits.aspx' style='color: #FFFFFF'>Kit Tools</a>"
                ElseIf Session("menu").ToString = "workorders" Then
                    submenulbl.Text = "<B>WORK ORDERS</B> <a href='EditWorkOrder.aspx' style='color: #FFFFFF'>New Work Order</a>&nbsp;|&nbsp;<a href='OpenWorkOrders.aspx' style='color: #FFFFFF'>Open Work Orders</a>&nbsp;|&nbsp;"
                ElseIf Session("menu").ToString = "purchasing" Then
                    submenulbl.Text = "<a href='PurchasingHome.aspx' style='color: #FFFFFF'><B>PURCHASING</B></a> <a href='RequisitionDashboard.aspx' style='color: #FFFFFF'>Order Requirements</a>&nbsp;|&nbsp;<a href='InventoryDashboard.aspx' style='color: #FFFFFF'>Inventory</a>&nbsp;|&nbsp;<a href='MySuppliers.aspx' style='color: #FFFFFF'>Suppliers</a>&nbsp;|&nbsp;<a href='PurchaseOrders.aspx' style='color: #FFFFFF'>Open POs</a>&nbsp;|&nbsp;<a href='POHistory.aspx' style='color: #FFFFFF'>PO History</a>&nbsp;|&nbsp;<a href='UsageByMfr.aspx' style='color: #FFFFFF'>Usage</a>"
                ElseIf Session("menu").ToString = "receiving" Then
                    submenulbl.Text = "<B>RECEIVING</B> <a href='PurchaseOrders.aspx' style='color: #FFFFFF'>Purchase Orders</a>"
                ElseIf Session("menu").ToString = "shipping" Then
                    submenulbl.Text = "<B>SHIPPING</B> <a href='OpenOrders.aspx' style='color: #FFFFFF'>Open Orders</a>&nbsp;|&nbsp;<a href='OpenOrdersBO.aspx' style='color: #FFFFFF'>Back Orders</a>&nbsp;|&nbsp;<a href='OpenOrdersRTS.aspx' style='color: #FFFFFF'>Ready To Ship</a>&nbsp;|&nbsp;<a href='Shipments.aspx' style='color: #FFFFFF'>Picked Orders</a>&nbsp;|&nbsp;<a href='OpenReturns.aspx?categoryID=0' style='color: #FFFFFF'>Returns</a>&nbsp;|&nbsp;<a href='RequirementsByCustomer.aspx' style='color: #FFFFFF'>Order Requirements</a>"
                ElseIf Session("menu").ToString = "accounting" Then
                    submenulbl.Text = "<B>ACCOUNTING</B> <a href='Billing.aspx' style='color: #FFFFFF'>Ready to Invoice</a>"
                ElseIf Session("menu").ToString = "csettings" Then
                    submenulbl.Text = "<B>SETTINGS</B> <a href='Company.aspx' style='color: #FFFFFF'>Company Info</a>&nbsp;|&nbsp;<a href='Locations.aspx' style='color: #FFFFFF'>Locations</a>&nbsp;|&nbsp;<a href='Contacts.aspx' style='color: #FFFFFF'>Users</a>"
                    If Session("admin") = True Then
                        submenulbl.Text += "&nbsp;|&nbsp;<a href='Discounts.aspx' style='color: #FFFFFF'>Discounts</a>"
                    End If
                ElseIf Session("menu").ToString = "stockroom" Then
                    submenulbl.Text = "<B>STOCK ROOM</B> "

                ElseIf Session("menu").ToString = "corders" Then
                    customerddpanel.Visible = True
                    submenulbl.Text = "<B>CUSTOMER</B> <a href='Orders.aspx' style='color: #FFFFFF'>Open Orders</a>&nbsp;|&nbsp;<a href='OrderHistory.aspx' style='color: #FFFFFF'>Order History</a>&nbsp;|&nbsp;<a href='CustomerInvoices.aspx' style='color: #FFFFFF'>Invoices</a>&nbsp;|&nbsp;<a href='Quotes.aspx' style='color: #FFFFFF'>Quotes</a>&nbsp;|&nbsp;<a href='Returns.aspx' style='color: #FFFFFF'>Returns</a>&nbsp;|&nbsp;<a href='Usage.aspx' style='color: #FFFFFF'>Usage</a>"
                ElseIf Session("menu").ToString = "crequirements" Then
                    submenulbl.Text = "<B>RECEIVING</B> Requirements"
                Else
                    submenulbl.Text = "<a href='WebContacts.aspx' style='color: #FFFFFF'>Users</a>"
                End If
            Else
                Session.Abandon()
                Response.Redirect("Logon.aspx")
            End If
        End If
        searchterm.Focus()
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        If searchterm.Text <> "" Then
            Dim searchfor As String = Trim(searchterm.Text)
            If searchdd.SelectedValue = "1" Then
                Session("searchdd") = "1"
                Response.Redirect("VCatalog.aspx?search=" & searchfor)
            ElseIf searchdd.SelectedValue = "2" Then
                Session("searchdd") = "2"
                Response.Redirect("AdvancedSearch.aspx?searchterm=" & searchterm.Text)
            ElseIf searchdd.SelectedValue = "3" Then
                Session("searchdd") = "3"
                Response.Redirect("CustomerUsage.aspx?search=" & searchfor)
            ElseIf searchdd.SelectedValue = "4" Then
                Session("searchdd") = "4"
                If appcode.IsOrderID(searchterm.Text) = True Then
                    Session("orderID") = Trim(searchterm.Text)
                    Response.Redirect("Order.aspx")
                End If
            ElseIf searchdd.SelectedValue = "5" Then
                Session("searchdd") = "5"
                If appcode.IsInvoiceID(CInt(searchterm.Text) - 10000) = True Then
                    Session("shipmentID") = CInt(searchterm.Text) - 10000
                    Response.Redirect("Invoice.aspx")
                End If
            ElseIf searchdd.SelectedValue = "6" Then
                Session("searchdd") = "6"
                If appcode.IsPO(searchterm.Text) Then
                    Session("poID") = CInt(searchterm.Text)
                    Response.Redirect("PurchaseOrder.aspx")
                End If
            End If
        End If
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
            submenulbl.Text = ""
        End If
        Response.Redirect("Default.aspx?menu=corders")
    End Sub

    Protected Sub newcustomerbtn_Click(sender As Object, e As EventArgs) Handles newcustomerbtn.Click
        Response.Redirect("NewCustomer.aspx")
    End Sub

    Protected Sub logobtn_Click(sender As Object, e As ImageClickEventArgs) Handles logobtn.Click
        If Session("admin") = True Then
            If Session("selected_companyID") = "0" Then
                Session("selected_companyID") = appcode.GetDefaultCompanyID()
                Response.Redirect("Default.aspx?menu=corders")
            Else
                Session("selected_companyID") = "0"
                Response.Redirect("Default.aspx?menu=orders")
            End If
        Else
            Response.Redirect("Default.aspx")
        End If
    End Sub

    Protected Sub newproductbtn_Click(sender As Object, e As EventArgs) Handles newproductbtn1.Click
        Response.Redirect("Product.aspx")
    End Sub

End Class

