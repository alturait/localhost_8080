Imports System.Data.SqlClient
Imports System.IO

Partial Class EST_Invoice
    Inherits System.Web.UI.Page
    Private shipping As Decimal = 0
    Private salestax As Decimal = 0
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private surcharge As Decimal = 0
    Private kitID As Array

    Protected Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/Invoice_" & Session("shipmentID") & ".pdf"
        Dim cimagefile As String = ""
        If File.Exists(Server.MapPath("~/Images/Banners/" & companyIDlbl.Value & ".jpg")) = True Then
            cimagefile = "Images/Banners/" & companyIDlbl.Value & ".jpg"
        Else
            cimagefile = "Images/bannerlogo.jpg"
        End If
        appcode.InvoicePDF(Session("shipmentID"), applicationpath, "Images/emailbanner.jpg", cimagefile, filename)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                qbobtn.Visible = False
                editbtn.Visible = False
            End If
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from v_shipments where shipmentID=@shipmentID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipmentID", Session("shipmentID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                If reader.Item("qbo") = True Then
                    qbobtn.Text = "Entered"
                    qbobtn.Enabled = False
                End If
                surcharge = reader.Item("surcharge")
                companyIDlbl.Value = reader.Item("companyID")
                vendorIDlbl.Value = reader.Item("vendorID")
                shipping = reader.Item("ship_charge")
                salestax = reader.Item("sales_tax")
                invoiceIDtb.Text = reader.Item("invoiceID").ToString
                shipmentIDlbl.Text = reader.Item("shipmentID")
                pickdatelbl.Text = reader.Item("pick_date")
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                polbl.Text = reader.Item("purchaseorder").ToString
                If reader.Item("pick_userID").ToString <> "" Then
                    pick_userIDlbl.Text = appcode.GetUserName(reader.Item("pick_userID"))
                End If
                If reader.Item("ship_userID").ToString <> "" Then
                    ship_userIDlbl.Text = appcode.GetUserName(reader.Item("ship_userID"))
                End If
                If reader.Item("invoice_userID").ToString <> "" Then
                    invoice_userIDlbl.Text = appcode.GetUserName(reader.Item("invoice_userID"))
                End If

                If reader.Item("needspo") = True Then
                        purchaseorderlbl.Text = "Needs PO"
                        purchaseorderlbl.ForeColor = Drawing.Color.Red
                        Panel1.Visible = True
                        Panel2.Visible = False
                    Else
                        Panel1.Visible = False
                        Panel2.Visible = True
                    End If
                    orderdatelbl.Text = reader.Item("order_date").ToString
                    deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                    vendorlbl.Text = reader.Item("vendor").ToString
                    vphonelbl.Text = reader.Item("v_phone").ToString
                    vfaxlbl.Text = reader.Item("v_fax").ToString
                    If reader.Item("serviceprofileID").ToString <> "0" Then
                        kitID = Split(reader.Item("serviceprofileID"), ",")
                        Dim numkits As Integer = kitID.GetLength(0)
                        Dim y As Integer = 0
                        For x As Integer = 0 To numkits - 1
                            If y > 0 Then
                                assetIDlbl.Text &= ","
                            End If
                            assetIDlbl.Text &= appcode.getAssetID(kitID(x))
                            y += 1
                        Next
                    Else
                        assetIDlbl.Text = "NA"
                    End If
                    Dim r_address As String = ""
                    If reader.Item("r_address1").ToString <> "" Then
                        r_address = reader.Item("r_address1") & "<br/>"
                        If reader.Item("r_address2").ToString <> "" Then
                            r_address &= reader.Item("r_address2") & "<br/>"
                        End If
                        r_address &= reader.Item("r_city") & ", " & reader.Item("r_state") & " " & reader.Item("r_zipcode")
                    End If
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
                        If reader.Item("r_address2").ToString <> "" Then
                            s_address &= reader.Item("s_address2") & "<br/>"
                        End If
                        s_address &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
                    End If
                    chargetaxcb.Checked = reader.Item("chargetax")
                    shiptolbl.Text = s_address
                    contactlbl.Text = reader.Item("c_contact")
                    phonelbl.Text = reader.Item("c_phone").ToString
                    vfaxlbl.Text = reader.Item("c_fax").ToString
                    notestb.Text = reader.Item("notes").ToString
                    carriertb.Text = reader.Item("carrier")
                    shipchargetb.Text = reader.Item("ship_charge")
                    trackingtb.Text = reader.Item("tracking")
                    If Session("company_role") = "Customer" Then
                        editbtn.Visible = False
                    End If
                End If
                reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
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
            Dim shippinglbl As Label = DirectCast(e.Row.FindControl("shippinglbl"), Label)
            Dim surchargelbl As Label = DirectCast(e.Row.FindControl("surchargelbl"), Label)
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            If chargetaxcb.Checked = True Then
                Session("salestax") = (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value))
                salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value)).ToString("c")
            Else
                Session("salestax") = "0"
                salestaxlbl.Text = 0.ToString("c")
            End If
            shippinglbl.Text = shipping.ToString("c")
            Dim invoice_total As Double = subtotal + CDbl(salestaxlbl.Text) + shipping
            surchargelbl.Text = surcharge.ToString("c")
            grandtotallbl.Text = (subtotal + salestaxlbl.Text + shipping + surcharge).ToString("c")
        End If
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs) Handles editbtn.Click
        Response.Redirect("EditInvoice.aspx")
    End Sub

    Protected Sub qbobtn_Click(sender As Object, e As EventArgs) Handles qbobtn.Click
        appcode.UpdateQBO(Session("shipmentID"), True)
        Response.Redirect("Billing.aspx")
        appcode.UpdateInvoiceUserID(Session("shipmentID"), Session("userID"))
    End Sub

    Protected Sub pickticketbtn_Click(sender As Object, e As EventArgs) Handles pickticketbtn.Click
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/PickTicket_" & Session("orderID") & ".pdf"
        appcode.OriginalPickTicketPDF(orderIDlbl.Text, assetIDlbl.Text, applicationpath, "Images/bannerlogo.jpg", filename)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        'update order po
        appcode.UpdateOrderPO(orderIDlbl.Text, potb.Text)
        appcode.UpdateNeedsPO(orderIDlbl.Text, False)
        Response.Redirect("Invoice.aspx")
    End Sub
    Protected Sub orderbtn_Click(sender As Object, e As EventArgs) Handles orderbtn.Click
        Session("orderID") = orderIDlbl.Text
        Response.Redirect("EditOrder.aspx")
    End Sub
End Class
