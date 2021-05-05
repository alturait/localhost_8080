Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class Cart
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
            If Session("chargetax") = True Then
                salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID"))).ToString("c")
                grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID")))).ToString("c")
            Else
                salestaxlbl.Text = (tax_subtotal * 0.00).ToString("c")
                grandtotallbl.Text = (subtotal + (tax_subtotal * 0)).ToString("c")
            End If
        End If
    End Sub

    Protected Sub emptybtn_Click(sender As Object, e As EventArgs) Handles emptybtn.Click
        For Each row In GridView1.Rows
            Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
            appcode.DeleteCartItem(cartIDlbl.Text)
        Next
        Session("hours_miles") = "0"
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Dim manufacturer As String
        Dim partnumber As String = Trim(partnumbertb.Text)
        Dim item As String
        Dim quantity As Integer = 1
        Dim saleprice As String
        Dim uom As String
        Dim weight As String
        Dim cartID As Integer
        If appcode.ispn(partnumber) = True Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_product where partnumber=@partnumber"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@partnumber", partnumber)
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                manufacturer = reader.Item("manufacturer")
                partnumber = reader.Item("partnumber")
                item = reader.Item("item")
                saleprice = appcode.GetCompanyPrice(Session("selected_companyID"), manufacturer, partnumber)
                uom = reader.Item("uom")
                weight = reader.Item("weight")
                cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), reader.Item("productID"), manufacturer, partnumber, item, quantity, saleprice, uom, weight, 0)
            End If
            reader.Close()
            conn.Close()
            Response.Redirect("Cart.aspx")
        End If
    End Sub

    Private Sub Cart_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        partnumbertb.Focus()
    End Sub
End Class
