Imports System.Data.SqlClient
Imports System.IO

Partial Class Product
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                Response.Redirect("VCatalogPage.aspx?productID=" & Request.QueryString("productID"))
            End If
            If Request.QueryString("productID") <> "" Then
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
                    If File.Exists(Server.MapPath("~/Images/Catalog/" & reader.Item("partnumber").ToString & ".jpg")) = True Then
                        Image1.ImageUrl = "~/Images/Catalog/" & reader.Item("partnumber").ToString & ".jpg"
                        picturelbl.Text = reader.Item("partnumber").ToString & ".jpg"
                    Else
                        Image1.ImageUrl = ""
                        picturelbl.Text = ""
                    End If
                    clearancecb.Checked = reader.Item("clearance")
                    rebatecb.Checked = reader.Item("rebate")
                    featurecb.Checked = reader.Item("featured")
                    categorydd.SelectedValue = reader.Item("category")
                    categorylbl.Text = appcode.GetCategoryName(reader.Item("category"))
                    manufacturerdd.SelectedValue = reader.Item("manufacturer").ToString
                    partnumbertb.Text = reader.Item("partnumber").ToString
                    upctb.Text = reader.Item("upc").ToString
                    itemtb.Text = reader.Item("item").ToString
                    descriptiontb.Text = reader.Item("description").ToString
                    packagetb.Text = reader.Item("package").ToString
                    weighttb.Text = FormatNumber(reader.Item("weight").ToString, 2)
                    categorytb.Text = reader.Item("price_category").ToString
                    If reader.Item("coreID").ToString <> "0" And reader.Item("coreID").ToString <> "" Then
                        coredd.SelectedValue = reader.Item("coreID")
                    End If
                    costtb.Text = FormatNumber(reader.Item("cost").ToString, 2)
                    msrptb.Text = FormatNumber(reader.Item("msrp").ToString, 2)
                    salepricetb.Text = FormatNumber(reader.Item("saleprice").ToString, 2)
                    uomtb.Text = reader.Item("uom").ToString
                    taxablecb.Checked = reader.Item("is_tax")
                    nstockcb.Checked = reader.Item("nstock")
                    onhandtb.Text = reader.Item("onhand")
                    mintb.Text = reader.Item("min")
                    maxtb.Text = reader.Item("max")
                End If
                reader.Close()
                conn.Close()
            Else
                deletebtn.Visible = False
                taxablecb.Checked = True
                Panel2.Visible = False
            End If
        End If
        savebtn.Focus()
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub partnumbertb_TextChanged(sender As Object, e As EventArgs) Handles partnumbertb.TextChanged
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        Dim comm As New SqlCommand
        Dim reader As SqlDataReader
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturerdd.SelectedValue)
        comm.Parameters.AddWithValue("@partnumber", partnumbertb.Text)
        reader = comm.ExecuteReader
        If reader.Read Then
            Response.Redirect("Product.aspx?productID=" & reader.Item("productID").ToString)
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim productID As Integer = 0
        If Request.QueryString("productID") <> "" Then
            appcode.UpdateProduct(Request.QueryString("productID"), categorydd.SelectedValue, manufacturerdd.SelectedValue, partnumbertb.Text, itemtb.Text, descriptiontb.Text, coredd.SelectedValue, costtb.Text, msrptb.Text, salepricetb.Text, uomtb.Text, packagetb.Text, rebatecb.Checked, categorytb.Text, weighttb.Text, upctb.Text, taxablecb.Checked, featurecb.Checked, clearancecb.Checked, onhandtb.Text, nstockcb.Checked, mintb.Text, maxtb.Text)
            productID = Request.QueryString("productID")
        Else
            If packagetb.Text = "" Then
                packagetb.Text = "1"
            End If
            If weighttb.Text = "" Then
                weighttb.Text = "0"
            End If
            If categorytb.Text = "" Then
                categorytb.Text = "NONE"
            End If
            If costtb.Text = "" Then
                costtb.Text = "0"
            End If
            If msrptb.Text = "" Then
                msrptb.Text = "0"
            End If
            If uomtb.Text = "" Then
                uomtb.Text = "EACH"
            End If
            productID = appcode.InsertProduct(Session("companyID"), categorydd.SelectedValue, manufacturerdd.SelectedValue, partnumbertb.Text, itemtb.Text, descriptiontb.Text, coredd.SelectedValue, costtb.Text, msrptb.Text, salepricetb.Text, uomtb.Text, packagetb.Text, rebatecb.Checked, categorytb.Text, weighttb.Text, upctb.Text, taxablecb.Checked, featurecb.Checked, clearancecb.Checked, onhandtb.Text, nstockcb.Checked, mintb.Text, maxtb.Text)
        End If
        Response.Redirect("VCatalogPage.aspx?productID=" & productID)
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteProduct(Request.QueryString("productID"))
        Response.Redirect("VCatalogPage.aspx?productID=" & Request.QueryString("productID"))
    End Sub

    Protected Sub uploadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uploadButton.Click
        If FileUpload1.HasFile Then
            Dim picture As String = partnumbertb.Text & ".jpg"
            FileUpload1.SaveAs(Server.MapPath("~/Images/Catalog/" & picture))
            msglbl.Text = "File " & picture & " uploaded."
        Else
            msglbl.Text = "No File Uploaded."
        End If
        Response.Redirect("Product.aspx?productID=" & Request.QueryString("productID"))
    End Sub

End Class
