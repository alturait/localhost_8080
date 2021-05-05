
Partial Class customer_Warehouse
    Inherits System.Web.UI.Page
    Dim sales_total As Double = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("warehousemfr").ToString <> "" Then
                manufacturerdd.SelectedValue = Session("warehousemfr")
            Else
                savebtn.Visible = False
            End If
            'warehouselbl.Text = " (" & appcode.GetWarehouseCount(Session("selected_companyID")) & " Items / " & FormatCurrency(appcode.GetWarehouseValue(Session("selected_companyID")), 2) & ")"
            pagelbl.Text = Page.Title
        End If
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

    Protected Sub UpdateInventory()
        appcode.DeleteAllPricing(Session("selected_companyID"))
        Dim pricingID As Integer
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim warehouseIDlbl As Label = row.FindControl("warehouseIDlbl")
            Dim mintb As TextBox = row.FindControl("mintb")
            Dim maxtb As TextBox = row.FindControl("maxtb")
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            Dim costlbl As Label = row.FindControl("costlbl")
            appcode.UpdateWarehouseItem1(warehouseIDlbl.Text, onhandtb.Text, mintb.Text, maxtb.Text)
            pricingID = appcode.InsertPricing(Session("selected_companyID"), manufacturerlbl.Text, partnumberlbl.Text, costlbl.Text)
        Next
    End Sub
    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        UpdateInventory()
        Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Response.Redirect("CatalogPage.aspx?productID=" & productID.ToString)
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim warehouseIDlbl As Label = GridView1.Rows(index).FindControl("warehouseIDlbl")
            appcode.DeleteWarehouseItem(warehouseIDlbl.Text)
            Response.Redirect("Warehouse.aspx")
        End If
    End Sub

    Protected Sub resetbtn_Click(sender As Object, e As EventArgs) Handles resetbtn.Click
        appcode.DeleteAllWarehouseItems(Session("selected_companyID"))
        Response.Redirect("Warehouse.aspx")
    End Sub

    Protected Sub orderbtn_Click(sender As Object, e As EventArgs) Handles orderbtn.Click
        
    End Sub

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        Session("warehousemfr") = manufacturerdd.SelectedValue
        If manufacturerdd.SelectedValue = "0" Then
            Session("warehousemfr") = ""
        End If
        Response.Redirect("Warehouse.aspx")
    End Sub
End Class
