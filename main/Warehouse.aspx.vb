
Partial Class main_Warehouse
    Inherits System.Web.UI.Page
    Dim sales_total As Double = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("warehousemfr").ToString <> "" Then
                manufacturerdd.SelectedValue = Session("warehousemfr")
                allusagecb.Checked = Session("allusage")
            End If
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub UpdateInventory()
        'appcode.DeleteAllPricing(Session("selected_companyID"))
        Dim pricingID As Integer
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim warehouseIDlbl As Label = row.FindControl("warehouseIDlbl")
            Dim mintb As TextBox = row.FindControl("mintb")
            Dim maxtb As TextBox = row.FindControl("maxtb")
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            Dim costtb As TextBox = row.FindControl("costtb")
            Dim nStockcb As CheckBox = row.FindControl("nstockcb")
            appcode.UpdateWarehouseItem(warehouseIDlbl.Text, "Main", onhandtb.Text, mintb.Text, maxtb.Text, nStockcb.Checked)
            appcode.DeleteStockItemPricing(Session("selected_companyID"), manufacturerlbl.Text, partnumberlbl.Text)
            pricingID = appcode.InsertPricing(Session("selected_companyID"), manufacturerlbl.Text, partnumberlbl.Text, costtb.Text)
        Next
    End Sub
    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        UpdateInventory()
        Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim valuelbl As Label = e.Row.FindControl("valuelbl")
            sales_total += valuelbl.Text
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim salestotallbl As Label = DirectCast(e.Row.FindControl("salestotallbl"), Label)
            salestotallbl.Text = sales_total.ToString("c")
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Session("lastpage") = ""
            Response.Redirect("Product.aspx?productID=" & productID.ToString)
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim warehouseIDlbl As Label = GridView1.Rows(index).FindControl("warehouseIDlbl")
            appcode.DeleteWarehouseItem(warehouseIDlbl.Text)
            Response.Redirect("Warehouse.aspx")
        End If
    End Sub

    Protected Sub cartbtn_Click(sender As Object, e As EventArgs) Handles cartbtn.Click
        For Each row In GridView1.Rows
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            Dim mintb As TextBox = row.FindControl("mintb")
            Dim maxtb As TextBox = row.FindControl("maxtb")
            Dim onhand As Double = 0
            Dim min As Double = 0
            Dim max As Double = 0
            onhand = CDbl(onhandtb.Text)
            min = CDbl(mintb.Text)
            max = CDbl(maxtb.Text)
            If onhand < min Then
                Dim costtb As TextBox = row.FindControl("costtb")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim item As String = appcode.GetItemName(manufacturerlbl.Text, partnumberlbl.Text)
                Dim qty As Double = max - onhand
                'add item
                Dim cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text), manufacturerlbl.Text, partnumberlbl.Text, item, qty, costtb.Text, uomlbl.Text, 0, 0)
            End If
        Next
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub worksheetbtn_Click(sender As Object, e As EventArgs) Handles worksheetbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/Worksheet_" & Session("selected_companyID") & ".pdf"
        appcode.InventorySheetPDF(Session("selected_companyID"), applicationpath, filename, "Images/DFO_LOGO_v3.jpg")
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        Session("warehousemfr") = manufacturerdd.SelectedValue
    End Sub
    Protected Sub allusagecb_CheckedChanged(sender As Object, e As EventArgs) Handles allusagecb.CheckedChanged
        If allusagecb.Checked = True Then
            Session("usage") = True
            GridView1.DataSourceID = "SqlInventoryAll"
            GridView1.DataBind()
        Else
            Session("allusage") = False
            GridView1.DataSourceID = "SqlInventory"
            GridView1.DataBind()
        End If
        'Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub pricesheetbtn_Click(sender As Object, e As EventArgs) Handles pricesheetbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/Pricesheet_" & Session("selected_companyID") & ".pdf"
        appcode.PriceSheetPDF(Session("selected_companyID"), applicationpath, filename, "Images/DFO_LOGO_v3.jpg")
        Response.Redirect("../" & filename)
    End Sub
End Class
