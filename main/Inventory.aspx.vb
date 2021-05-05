
Partial Class main_Inventory
    Inherits System.Web.UI.Page
    Dim sales_total As Double = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("imfr").ToString <> "" Then
                manufacturerdd.SelectedValue = Session("imfr")
            End If
        End If
        Session("imfr") = ""
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        GridView1.DataSourceID = "SqlMfrInventory"
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Session("lastpage") = "Inventory"
            Response.Redirect("Product.aspx?productID=" & productID.ToString)
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim stockcb As CheckBox = row.FindControl("stockcb")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            Dim mintb As TextBox = row.FindControl("mintb")
            Dim maxtb As TextBox = row.FindControl("maxtb")
            appcode.UpdateInventory(manufacturerlbl.Text, partnumberlbl.Text, onhandtb.Text, mintb.Text, maxtb.Text, stockcb.Checked)
        Next
        Session("imfr") = manufacturerdd.SelectedValue
        Response.Redirect("Inventory.aspx")
    End Sub

    Protected Sub addPObtn_Click(sender As Object, e As EventArgs) Handles addPObtn.Click
        Dim poID As Integer = podd.SelectedValue
        Dim count As Integer = 0
        If podd.SelectedValue <> "0" Then
            'NEW PO
            For Each row In GridView1.Rows
                Dim polineID As Integer
                Dim orderamount As Double
                Dim onpoamount As Double = 0
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim stockcb As CheckBox = row.FindControl("stockcb")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim rebate As Boolean = appcode.isRebate(manufacturerlbl.Text, partnumberlbl.Text)
                Dim mintb As TextBox = row.FindControl("mintb")
                Dim maxtb As TextBox = row.FindControl("maxtb")
                Dim onhandtb As TextBox = row.FindControl("onhandtb")
                Dim ordertb As TextBox = row.FindControl("ordertb")
                Dim costlbl As Label = row.FindControl("costlbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If stockcb.Checked = True Then
                    orderamount = appcode.CalculateOrderQuantity(manufacturerlbl.Text, partnumberlbl.Text, mintb.Text, maxtb.Text, onhandtb.Text)
                    If orderamount > 0 Then
                        polineID = appcode.InsertPOLine(poID, manufacturerlbl.Text, partnumberlbl.Text, orderamount, costlbl.Text, uomlbl.Text, rebate, 0)
                    End If
                End If
            Next
        Else
            'ADD TO PO
            For Each row In GridView1.Rows
                Dim polineID As Integer
                Dim orderamount As Double
                Dim onpoamount As Double = 0
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim stockcb As CheckBox = row.FindControl("stockcb")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim rebate As Boolean = appcode.isRebate(manufacturerlbl.Text, partnumberlbl.Text)
                Dim mintb As TextBox = row.FindControl("mintb")
                Dim maxtb As TextBox = row.FindControl("maxtb")
                Dim onhandtb As TextBox = row.FindControl("onhandtb")
                Dim ordertb As TextBox = row.FindControl("ordertb")
                Dim costlbl As Label = row.FindControl("costlbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If stockcb.Checked = True Then
                    If count = 0 Then
                        poID = appcode.InsertPO(Session("vendorID"), appcode.GetCompany(Session("vendorID")), FormatDateTime(Now(), DateFormat.ShortDate), Session("userID"), appcode.GetUserName(Session("userID")))
                    End If
                    orderamount = appcode.CalculateOrderQuantity(manufacturerlbl.Text, partnumberlbl.Text, mintb.Text, maxtb.Text, onhandtb.Text)
                    If orderamount > 0 Then
                        polineID = appcode.InsertPOLine(poID, manufacturerlbl.Text, partnumberlbl.Text, orderamount, costlbl.Text, uomlbl.Text, rebate, 0)
                    End If
                    count += 1
                End If
            Next
        End If
        Session("poID") = poID
        Response.Redirect("PurchaseOrder.aspx")
    End Sub

End Class
