Imports System.Data
Imports System.Drawing

Partial Class customer_EquipmentOrderHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("equipmentID") <> "0" Then
                GridView1.DataSourceID = "SqlKitOrdersByEquipment"
                GridView1.DataBind()
            End If
            If Request.QueryString("eid") = "0" Then
                GridView1.DataSourceID = "SqlKitOrders"
                GridView1.DataBind()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Order" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        ElseIf e.CommandName = "Asset" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView1.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

End Class
