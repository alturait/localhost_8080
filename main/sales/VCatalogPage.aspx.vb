Imports System.Data.SqlClient
Imports System.IO

Partial Class main_VCatalogPage
    Inherits System.Web.UI.Page

    Protected Function GetPicture(ByVal partnumber As String) As String
        GetPicture = "blank"
        If File.Exists(Server.MapPath("../Images/Catalog/" & partnumber & ".jpg")) = True Then
            GetPicture = partnumber
        End If
    End Function

    Protected Sub favoritebtn_Click(sender As Object, e As EventArgs) Handles favoritebtn.Click
        If appcode.IsFavorite(Request.QueryString("productID"), Session("userID")) = False Then
            Dim favoriteID As Integer = appcode.InsertFavorite(Request.QueryString("productID"), 0, Session("selected_companyID"))
            Response.Redirect("Favorites.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("warehousemfr") = ""
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_product where productID=@productID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@productID", Request.QueryString("productID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                productImage.ImageUrl = "~/Images/Catalog/" & GetPicture(reader.Item("partnumber")) & ".jpg"
                categoryIDlbl.Value = reader.Item("category")
                manufacturertb.Text = reader.Item("manufacturer").ToString
                pnlbl.Text = reader.Item("item").ToString
                partnumbertb.Text = reader.Item("partnumber").ToString
                upctb.Text = reader.Item("upc").ToString
                itemtb.Text = reader.Item("item").ToString
                descriptiontb.Text = reader.Item("description").ToString
                packagetb.Text = reader.Item("package").ToString
                weighttb.Text = FormatNumber(reader.Item("weight").ToString, 2)
                categorytb.Text = reader.Item("price_category").ToString
                msrptb.Text = FormatCurrency(reader.Item("msrp").ToString, 2)
                uomtb.Text = reader.Item("uom").ToString
                qtytb.Text = "1"
                costlbl.Value = FormatCurrency(reader.Item("cost"), 2)
                currentcostlbl.Text = FormatCurrency(reader.Item("cost"), 2)
                stocklbl.Text = reader.Item("onhand")
                corelbl.Text = FormatCurrency(appcode.GetCoreCharge(reader.Item("coreID")), 2)
                If reader.Item("saleprice") <> 0 Then
                    salepricelbl.Text = FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber")), 2)
                    saleprice2lbl.Text = salepricelbl.Text
                Else
                    salepricelbl.Text = "0"
                    saleprice2lbl.Text = FormatCurrency(reader.Item("saleprice"), 2)
                End If
                If CDbl(currentcostlbl.Text) <> 0 And CDbl(msrptb.Text <> 0) And CDbl(salepricelbl.Text <> 0) Then
                    msrpgplbl.Text = " (" & FormatPercent(1 - CDbl(currentcostlbl.Text) / CDbl(msrptb.Text), 2) & ")"
                    salegplbl.Text = " (" & FormatPercent(1 - CDbl(currentcostlbl.Text) / CDbl(salepricelbl.Text), 2) & ")"
                    salegp2lbl.Text = " (" & FormatPercent(1 - CDbl(currentcostlbl.Text) / CDbl(saleprice2lbl.Text), 2) & ")"
                End If
                If reader.Item("msrp") = 0 Then
                    msrptb.Text = "Call for Price"
                    salepricelbl.Text = "Call for Price"
                End If
                If appcode.isCustomer(Session("companyID")) = True Then
                    Panel2.Visible = False
                Else
                    If Session("selected_companyID") <> "0" Then

                    Else
                        Panel4.Visible = False
                        Panel5.Visible = False
                    End If
                End If
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("VCatalog.aspx?categoryID=" & categoryIDlbl.Value)
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Dim cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), Request.QueryString("productID"), manufacturertb.Text, partnumbertb.Text, itemtb.Text, qtytb.Text, salepricelbl.Text, uomtb.Text, weighttb.Text, 0)
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub warehousebtn_Click(sender As Object, e As EventArgs) Handles warehousebtn.Click
        If appcode.IsWarehouseItem(manufacturertb.Text, partnumbertb.Text, Session("selected_companyID")) = False Then
            'Dim warehouseID As Integer = appcode.InsertWarehouseItem(manufacturertb.Text, partnumbertb.Text, Session("selected_companyID"), 0, 1, 1)
            Dim warehouseID As Integer = appcode.InsertWarehouseItem("", manufacturertb.Text, partnumbertb.Text, Session("selected_companyID"), 0, 1, 1)
        End If
        Session("warehousemfr") = manufacturertb.Text.ToString
        Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim poIDlbl As LinkButton = GridView1.Rows(index).FindControl("poIDlbl")
            Session("poID") = poIDlbl.Text
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As LinkButton = GridView2.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        End If
    End Sub
End Class
