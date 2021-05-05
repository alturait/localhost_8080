Imports System.Data.SqlClient
Imports System.IO

Partial Class CatalogPage
    Inherits System.Web.UI.Page

    Protected Function GetPicture(ByVal partnumber As String) As String
        GetPicture = "blank"
        If File.Exists(Server.MapPath("Images/Catalog/" & partnumber & ".jpg")) = True Then
            GetPicture = partnumber
        ElseIf File.Exists(Server.MapPath("Images/Catalog/catID" & Request.QueryString("categoryID") & ".jpg")) = True Then
            GetPicture = "catID" & Request.QueryString("categoryID")
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
                If reader.Item("saleprice") <> 0 Then
                    salepricelbl.Text = FormatCurrency(reader.Item("saleprice"), 2)
                Else
                    salepricelbl.Text = "None"
                End If
                If reader.Item("msrp") = 0 Then
                    msrptb.Text = "Call for Price"
                    salepricelbl.Text = "Call for Price"
                End If
                uomtb.Text = reader.Item("uom").ToString
                qtytb.Text = "1"
            End If
            reader.Close()
            conn.Close()
            qtytb.Visible = False
            addbtn.Visible = False
        End If
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Catalog.aspx?categoryID=" & categoryIDlbl.Value)
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        If IsNumeric(qtytb.Text) = False Then
            qtytb.Text = "1"
        End If
        Dim cartID As Integer = appcode.AddToCart(Request.QueryString("productID"), qtytb.Text, 0, 0, 98, Session("ssID"))
        'Dim cartID As Integer = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), Session("productID"), manufacturertb.Text, partnumbertb.Text, itemtb.Text, qtytb.Text, salepricelbl.Text, uomtb.Text, weighttb.Text, Session("ssID"))
        Response.Redirect("GuestCart.aspx")
    End Sub
End Class
