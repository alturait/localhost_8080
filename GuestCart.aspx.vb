Imports System.Data.SqlClient

Partial Class GuestCart
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Session("ssID")
    End Sub

    Protected Sub updatebtn1_Click(sender As Object, e As EventArgs) Handles updatebtn1.Click
        For Each row In GridView1.Rows
            Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
            Dim qtytb As TextBox = row.FindControl("qtytb")
            appcode.UpdateCartItem(cartIDlbl.Text, qtytb.Text)
        Next
        Response.Redirect("GuestCart.aspx")
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim linetotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extended"))
            subtotal = subtotal + linetotal
            If appcode.IsTaxable(DataBinder.Eval(e.Row.DataItem, "manufacturer"), DataBinder.Eval(e.Row.DataItem, "partnumber")) = True Then
                tax_subtotal = tax_subtotal + linetotal
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            Dim salestaxlbl As Label = DirectCast(e.Row.FindControl("salestaxlbl"), Label)
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            salestaxlbl.Text = (tax_subtotal * 0.083).ToString("c")
            grandtotallbl.Text = (subtotal + (tax_subtotal * 0.083)).ToString("c")
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim cartIDlbl As Label = GridView1.Rows(index).FindControl("cartIDlbl")
            appcode.DeleteCartItem(cartIDlbl.Text)
            Response.Redirect("GuestCart.aspx")
        End If
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        appcode.DeleteGuestCartItems(Session("ssID"))
        Response.Redirect("Catalog.aspx?categoryID=0")
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        Response.Redirect("Logon.aspx")
    End Sub
End Class
