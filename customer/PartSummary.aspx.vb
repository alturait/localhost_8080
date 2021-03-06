
Partial Class customer_PartSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("equipmentID") = ""
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Equipment" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Response.Redirect("SearchEquipment.aspx?searchterm=" & partnumberlbl.Text)
        ElseIf e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Response.Redirect("CatalogPage.aspx?productID=" & productID)
        End If
    End Sub

    Protected Sub locationdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles locationdd.SelectedIndexChanged
        If locationdd.SelectedValue <> "0" Then
            Session("this_locationID") = locationdd.SelectedValue
            GridView1.DataSourceID = "SqlPartSummaryByLocation"
            GridView1.DataBind()
        Else
            Session("this_locationID") = ""
            GridView1.DataSourceID = "SqlPartSummaryByCustomer"
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub pdfbtn_Click(sender As Object, e As EventArgs) Handles pdfbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/InventorySheet_" & Session("selected_companyID") & ".pdf"
        appcode.InventorySheet2PDF(Session("selected_companyID"), locationdd.SelectedValue, applicationpath, filename, "Images/bannerlogo.jpg")
        Response.Redirect("../" & filename)
    End Sub

End Class
