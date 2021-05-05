Imports System.Data.SqlClient
Imports System.IO

Partial Class customer_CompleteOrder
    Inherits System.Web.UI.Page

    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_order where orderID=@orderID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", Session("orderID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                If reader.Item("needspo") = False Then
                    potb.Enabled = False
                    updatebtn.Visible = False
                End If
                orderdatelbl.Text = FormatDateTime(reader.Item("order_date").ToString, DateFormat.ShortDate)
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                If reader.Item("confirmed") = True Then
                    confirmlbl.Text = "YES"
                Else
                    confirmlbl.Text = "NO"
                End If
                vphonelbl.Text = reader.Item("v_phone").ToString
                vfaxlbl.Text = reader.Item("v_fax").ToString
                companyIDlbl.Value = reader.Item("companyID")
                vendorIDlbl.Value = reader.Item("vendorID")
                serviceIDlbl.Value = reader.Item("serviceID")
                Dim r_address As String = reader.Item("vendor") & "<br/>"
                r_address &= reader.Item("r_address1") & "<br/>"
                If reader.Item("r_address2").ToString <> "" Then
                    r_address &= reader.Item("r_address2") & "<br/>"
                End If
                r_address &= reader.Item("r_city") & ", " & reader.Item("r_state") & " " & reader.Item("r_zipcode")
                remittolbl.Text = r_address
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
                shipmethoddd.Enabled = False
                If reader.Item("complete") = True Then
                    shipotionsdd.Enabled = False
                End If
            End If
            reader.Close()
            conn.Close()
            If appcode.isCustomer(Session("companyID")) = True Then
                editbtn.Visible = False
                pickbtn.Visible = False
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub pickbtn_Click(sender As Object, e As EventArgs) Handles pickbtn.Click
        Response.Redirect("PickOrder.aspx")
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
            salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value)).ToString("c")
            grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value))).ToString("c")
        End If
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs) Handles editbtn.Click
        Session("selected_companyID") = companyIDlbl.Value
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub shipotionsdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shipotionsdd.SelectedIndexChanged
        appcode.UpdateShipOptions(Session("orderID"), shipotionsdd.SelectedValue)
        Response.Redirect("Order.aspx")
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        appcode.UpdateOrderPO(Session("orderID"), potb.Text)
        appcode.UpdateNeedsPO(Session("orderID"), False)
        Response.Redirect("NeedsPO.aspx")
    End Sub
End Class
