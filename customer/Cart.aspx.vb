Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class customer_Cart
    Inherits System.Web.UI.Page

    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Update" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim cartIDlbl As Label = GridView1.Rows(index).FindControl("cartIDlbl")
            Dim qtytb As TextBox = GridView1.Rows(index).FindControl("qtytb")
            appcode.UpdateCartItem(cartIDlbl.Text, qtytb.Text)
            Response.Redirect("Cart.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim cartIDlbl As Label = GridView1.Rows(index).FindControl("cartIDlbl")
            appcode.DeleteCartItem(cartIDlbl.Text)
            Response.Redirect("Cart.aspx")
        End If
    End Sub

    Protected Sub emptybtn_Click(sender As Object, e As EventArgs) Handles emptybtn.Click
        For Each row In GridView1.Rows
            Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
            appcode.DeleteCartItem(cartIDlbl.Text)
        Next
        Session("selected_assetID") = "0"
        Session("hours_miles") = "0"
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        Dim x As Integer = 0
        For Each row In GridView1.Rows
            x += 1
        Next
        If x > 0 Then
            Response.Redirect("CheckOut.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
            If Session("selected_assetID") <> "0" Then
                assetlbl.Text = "Asset: " & appcode.getAssetID(appcode.GetServiceProfileID(Session("selected_companyID"), Session("userID")))
            End If
            If appcode.IsActive(Session("selected_companyID")) = True Then
                submitbtn.Visible = True
            Else
                submitbtn.Visible = False
            End If
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim productID As Integer = DataBinder.Eval(e.Row.DataItem, "productID")
            Dim corelbl As Label = DirectCast(e.Row.FindControl("corelbl"), Label)
            Dim quantity As Double = DataBinder.Eval(e.Row.DataItem, "quantity")
            Dim linetotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extended"))
            Dim coretotal As Decimal = 0
            Dim coreID As Integer = appcode.GetCoreID(productID)
            If coreID <> 0 Then
                coretotal = appcode.GetCoreCharge(coreID) * quantity
                corelbl.Text = " * " & FormatCurrency(coretotal, 2) & " Core Charge"
            End If
            subtotal = subtotal + linetotal + coretotal
            If appcode.IsTaxable(DataBinder.Eval(e.Row.DataItem, "manufacturer"), DataBinder.Eval(e.Row.DataItem, "partnumber")) = True Then
                tax_subtotal = tax_subtotal + linetotal
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            Dim salestaxlbl As Label = DirectCast(e.Row.FindControl("salestaxlbl"), Label)
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID"))).ToString("c")
            grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID")))).ToString("c")
        End If
    End Sub

End Class
