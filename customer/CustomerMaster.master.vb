
Partial Class CustomerMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            logobtn.ImageUrl = "../Images/DFO_LOGO_v3.jpg"
            logobtn.PostBackUrl = "Default.aspx"
            namelbl.Text = appcode.GetUserName(Session("userID"))
            companylbl.Text = appcode.GetCompany(Session("companyID"))
            cartcountlbl.Text = " (" & appcode.GetCartCount(Session("userID"), Session("selected_companyID")) & " Items)"
            'ssIDlbl.Text = Session("ssID")
            If Request.QueryString("menu") <> "" Then
                Session("menu") = Request.QueryString("menu")
            End If
            If Session("menu").ToString = "catalog" Then
                pnsearchpanel.Visible = True
                submenulbl.Text = "<B>CATALOG</B>&nbsp;-&nbsp;<a href='Favorites.aspx' style='color: #FFFFFF'>Favorites</a>"
            ElseIf Session("menu").ToString = "stockroom" Then
                submenulbl.Text = "<B>STOCK ROOM</B>"
            ElseIf Session("menu").ToString = "lubetracker" Then
                lubetrackerpanel.Visible = True
                If Session("admin") = True Then
                    submenulbl.Text = "<B>LUBETRACKER</B>&nbsp;-&nbsp;<a href='Assets.aspx' style='color: #FFFFFF'>Assets</a>&nbsp;|&nbsp;<a href='OpenKits.aspx' style='color: #FFFFFF'>Pending Kits</a>&nbsp;|&nbsp;<a href='EquipmentOrderHistory.aspx?eid=0' style='color: #FFFFFF'>History</a>&nbsp;|&nbsp;<a href='PartSummary.aspx' style='color: #FFFFFF'>Part Summary</a>&nbsp;|&nbsp;<a href='FluidList.aspx' style='color: #FFFFFF'>Fluids</a>&nbsp;|&nbsp;<a href='FluidSummary.aspx' style='color: #FFFFFF'>Fluid Summary</a>"
                Else
                    submenulbl.Text = "<B>LUBETRACKER</B>"
                End If
            ElseIf Session("menu").ToString = "orders" Then
                submenulbl.Text = "<B>ORDERS</B>&nbsp;-&nbsp;<a href='OpenOrders.aspx' style='color: #FFFFFF'>Open Orders</a>&nbsp;|&nbsp;<a href='OrderHistory.aspx?categoryID=0' style='color: #FFFFFF'>Order History</a>&nbsp;|&nbsp;<a href='NeedsPO.aspx' style='color: #FFFFFF'>Needs PO</a>"
            ElseIf Session("menu").ToString = "home" Then
                If Session("admin") = True Then
                    submenulbl.Text = "<B>HOME</B>&nbsp;-&nbsp;<a href='Company.aspx' style='color: #FFFFFF'>Company Info</a>&nbsp;|&nbsp;<a href='Locations.aspx' style='color: #FFFFFF'>Locations</a>&nbsp;|&nbsp;<a href='Contacts.aspx' style='color: #FFFFFF'>Users</a>"
                End If
            End If

        End If
    End Sub

    Protected Sub esearchbtn_Click(sender As Object, e As EventArgs) Handles esearchbtn.Click
        Response.Redirect("SearchEquipment.aspx?searchterm=" & esearchterm.Text)
    End Sub

    Protected Sub searchbtn_Click(sender As Object, e As EventArgs) Handles searchbtn.Click
        Response.Redirect("Catalog.aspx?search=" & Trim(searchterm.Text) & "&categoryID=0")
    End Sub

    Protected Sub cartbtn_Click(sender As Object, e As EventArgs) Handles cartbtn.Click
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub logoffbtn_Click(sender As Object, e As EventArgs) Handles logoffbtn.Click
        Session.Abandon()
        Response.Redirect("../Default.aspx")
    End Sub

    Protected Sub homebtn_Click(sender As Object, e As EventArgs) Handles homebtn.Click
        Session("menu") = "home"
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub stockroombtn_Click(sender As Object, e As ImageClickEventArgs) Handles stockroombtn.Click
        Session("menu") = "stockroom"
        Response.Redirect("Warehouse.aspx?categoryID=0")
    End Sub

    Protected Sub catalogbtn_Click(sender As Object, e As ImageClickEventArgs) Handles catalogbtn.Click
        Session("menu") = "catalog"
        Response.Redirect("Catalog.aspx?categoryID=0")
    End Sub
    Protected Sub ordersbtn_Click(sender As Object, e As ImageClickEventArgs) Handles ordersbtn.Click
        Session("menu") = "orders"
        Response.Redirect("OpenOrders.aspx")
    End Sub
    Protected Sub lubetrackerbtn_Click(sender As Object, e As ImageClickEventArgs) Handles lubetrackerbtn.Click
        Session("menu") = "lubetracker"
        If Session("admin") = True Then
            Response.Redirect("OpenKits.aspx")
        Else
            Response.Redirect("../mobile/Default.aspx")
        End If
    End Sub
End Class

