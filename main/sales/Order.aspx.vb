﻿Imports System.Data.SqlClient
Imports System.IO

Partial Class EST_Order
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private totalgp As Decimal = 0
    Private kitID As Array
    Private serviceprofileID As String
    Dim assets As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("orderID") <> "" Then
                Session("orderID") = Request.QueryString("orderID")
            End If
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_order where orderID=@orderID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", Session("orderID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                'test for backorder
                If appcode.IsBO(Session("orderID")) = True Then
                    bolbl.Visible = True
                End If
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = reader.Item("order_date").ToString
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                vendorlbl.Text = reader.Item("vendor").ToString
                vphonelbl.Text = reader.Item("v_phone").ToString
                vfaxlbl.Text = reader.Item("v_fax").ToString
                companyIDlbl.Value = reader.Item("companyID")
                vendorIDlbl.Value = reader.Item("vendorID")
                serviceIDlbl.Value = reader.Item("serviceID")
                Dim r_address As String = ""
                If reader.Item("r_address1").ToString <> "" Then
                    r_address = reader.Item("r_address1") & "<br/>"
                    If reader.Item("r_address2").ToString <> "" Then
                        r_address &= reader.Item("r_address2") & "<br/>"
                    End If
                    r_address &= reader.Item("r_city") & ", " & reader.Item("r_state") & " " & reader.Item("r_zipcode")
                End If
                replbl.Text = appcode.GetUserName(reader.Item("repID"))
                customerlbl.Text = reader.Item("company").ToString
                Dim b_address As String = ""
                If reader.Item("b_address1").ToString <> "" Then
                    b_address = reader.Item("b_address1") & "<br/>"
                    If reader.Item("b_address2").ToString <> "" Then
                        b_address &= reader.Item("b_address2") & "<br/>"
                    End If
                    b_address &= reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
                End If
                billtolbl.Text = b_address
                Dim s_address As String = ""
                If reader.Item("s_address1").ToString <> "" Then
                    s_address &= reader.Item("shipto") & "<br/>"
                    s_address &= reader.Item("s_address1") & "<br/>"
                    If reader.Item("s_address2").ToString <> "" Then
                        s_address &= reader.Item("s_address2") & "<br/>"
                    End If
                    If reader.Item("s_address3").ToString <> "" Then
                        s_address &= reader.Item("s_address3") & "<br/>"
                    End If
                    s_address &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
                End If
                shiptolbl.Text = s_address
                userIDlbl.Value = reader.Item("userID").ToString
                totb.Text = appcode.GetUserEmail(reader.Item("userID"))
                contactlbl.Text = reader.Item("c_contact")
                phonelbl.Text = reader.Item("c_phone").ToString
                vfaxlbl.Text = reader.Item("c_fax").ToString
                notestb.Text = reader.Item("notes").ToString
                shipmethoddd.SelectedValue = reader.Item("ship_method").ToString
                shipotionsdd.SelectedValue = reader.Item("ship_options").ToString
                kitcb.Checked = reader.Item("is_kit")
                serviceprofileID = reader.Item("serviceprofileID")
                kitID = Split(reader.Item("serviceprofileID"), ",")
                Dim numkits As Integer = kitID.GetLength(0)
                Dim y As Integer = 0
                For x As Integer = 0 To numkits - 1
                    If y > 0 Then
                        kitIDlbl.Text &= ","
                        assetlbl.Text &= ","
                    End If
                    kitIDlbl.Text &= "<a href='ViewPDF.aspx?serviceprofileID=" & kitID(x) & "&companyID=" & reader.Item("companyID") & "'>" & kitID(x) & "</a>"
                    assetlbl.Text &= appcode.getAssetID(kitID(x))
                    y += 1
                Next
                serviceIDlbl.Value = reader.Item("serviceID")
                shipmethoddd.Enabled = False
                kitcb.Enabled = False
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim shipmentIDlbl As Label = GridView2.Rows(index).FindControl("shipmentIDlbl")
            Session("shipmentID") = shipmentIDlbl.Text
            Response.Redirect("Invoice.aspx")
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim linetotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extended"))
            Dim linecost As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extendedcost"))            
            totalgp = totalgp + (linetotal - linecost)
            subtotal = subtotal + linetotal
            If appcode.IsTaxable(DataBinder.Eval(e.Row.DataItem, "manufacturer"), DataBinder.Eval(e.Row.DataItem, "partnumber")) = True Then
                tax_subtotal = tax_subtotal + linetotal
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            Dim salestaxlbl As Label = DirectCast(e.Row.FindControl("salestaxlbl"), Label)
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            Dim totalgplbl As Label = DirectCast(e.Row.FindControl("totalgplbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            totalgplbl.Text = totalgp.ToString("c")
            Dim chargetax As Boolean = appcode.getOrderChargeTax(Session("orderID"))
            If chargetax = True Then
                salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value)).ToString("c")
                grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value))).ToString("c")
            Else
                Dim notax As Double = 0.0
                salestaxlbl.Text = notax.ToString("c")
                grandtotallbl.Text = subtotal.ToString("c")
            End If
        End If
    End Sub

    Protected Sub shipotionsdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shipotionsdd.SelectedIndexChanged
        appcode.UpdateShipOptions(Session("orderID"), shipotionsdd.SelectedValue)
        Response.Redirect("Order.aspx")
    End Sub

End Class
