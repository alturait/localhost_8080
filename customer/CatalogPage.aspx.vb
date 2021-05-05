Imports System.Data.SqlClient
Imports System.IO

Partial Class customer_CatalogPage
    Inherits System.Web.UI.Page

    Protected Function GetPicture(ByVal partnumber As String) As String
        GetPicture = "blank"
        If File.Exists(Server.MapPath("../Images/Catalog/" & partnumber & ".jpg")) = True Then
            GetPicture = partnumber
        ElseIf File.Exists(Server.MapPath("../Images/Catalog/catID" & categoryIDlbl.Value & ".jpg")) = True Then
            GetPicture = "catID" & categoryIDlbl.Value
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("productID") <> "" Then
                Session("productID") = Request.QueryString("productID")
            End If
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_product where productID=@productID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@productID", Session("productID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                If appcode.isCore(reader.Item("partnumber")) = True Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    Response.Redirect(previousPage)
                End If
                productImage.ImageUrl = "../Images/Catalog/" & GetPicture(reader.Item("partnumber")) & ".jpg"
                categoryIDlbl.Value = reader.Item("category")
                manufacturertb.Text = reader.Item("manufacturer").ToString
                pnlbl.Text = reader.Item("item").ToString
                partnumbertb.Text = reader.Item("partnumber").ToString
                itemtb.Text = reader.Item("item").ToString
                descriptiontb.Text = reader.Item("description").ToString
                packagetb.Text = reader.Item("package").ToString
                stocklbl.Text = reader.Item("onhand")
                Dim coreID As Integer = appcode.GetCoreID(reader.Item("productID"))
                corelbl.Text = FormatCurrency(appcode.GetCoreCharge(coreID), 2)
                If reader.Item("saleprice") <> 0 Then
                    salepricelbl.Text = FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber")), 2)
                Else
                    salepricelbl.Text = "None"
                End If
                If reader.Item("msrp") = 0 Then
                    salepricelbl.Text = "Call for Price"
                End If
                uomtb.Text = reader.Item("uom").ToString
                qtytb.Text = "1"
                Dim onpo As Boolean = appcode.IsOnPO(reader.Item("manufacturer"), reader.Item("partnumber"))
                If onpo = True Then
                    purchasinglbl.Text = appcode.GetOnPO(reader.Item("manufacturer"), reader.Item("partnumber")) & " of these are on incoming purchase orders. " & appcode.GetOnOrderAmount(reader.Item("manufacturer"), reader.Item("partnumber")) & " are allocated for sales orders."
                Else
                    purchasinglbl.Text = "This item is not on an incoming order"
                End If
            End If
            reader.Close()
            conn.Close()
        End If
    End Sub

    Protected Sub warehousebtn_Click(sender As Object, e As EventArgs) Handles warehousebtn.Click
        If appcode.IsWarehouseItem(manufacturertb.Text, partnumbertb.Text, Session("selected_companyID")) = False Then
            Dim warehouseID As Integer = appcode.InsertWarehouseItem("Main", manufacturertb.Text, partnumbertb.Text, Session("selected_companyID"), 0, 1, 0)
        End If
        Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Catalog.aspx?categoryID=" & categoryIDlbl.Value)
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Dim cartID As Integer = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), Session("productID"), manufacturertb.Text, partnumbertb.Text, itemtb.Text, qtytb.Text, salepricelbl.Text, uomtb.Text, 0, 0)
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub favoritebtn_Click(sender As Object, e As EventArgs) Handles favoritebtn.Click
        If appcode.IsFavorite(Request.QueryString("productID"), Session("selected_companyID")) = False Then
            Dim favoriteID As Integer = appcode.InsertFavorite(Request.QueryString("productID"), Session("userID"), Session("selected_companyID"))
            Response.Redirect("Favorites.aspx")
        End If
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim xmanufacturerlbl As Label = GridView3.Rows(index).FindControl("xmanufacturerlbl")
            Dim xpartnumberlbl As Label = GridView3.Rows(index).FindControl("xpartnumberlbl")
            appcode.RemoveXref(manufacturertb.Text, partnumbertb.Text, xmanufacturerlbl.Text, xpartnumberlbl.Text)
            appcode.RemoveXref(xmanufacturerlbl.Text, xpartnumberlbl.Text, manufacturertb.Text, partnumbertb.Text)
            Response.Redirect("VCatalogPage.aspx?productID=" & Request.QueryString("productID"))
        End If
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView2.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        End If
    End Sub
End Class
