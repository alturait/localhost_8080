Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class EST_EditInvoice
    Inherits System.Web.UI.Page
    Private shipping As Decimal = 0
    Private subtotal As Decimal = 0
    Private salestax As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private surcharge As Decimal = 0

    Sub SendEmail(ByVal subject As String, ByVal message As String, ByVal from As String, ByVal recipient As String, ByVal cc As String, ByVal bcc As String)
        Dim msg As New EmailMessage()
        msg.Server = "localhost"
        msg.FromAddress = from
        msg.ValidateAddress = False
        msg.AddTo(recipient)
        msg.Subject = subject
        If cc <> "" Then
            msg.AddCc(cc)
        End If
        If bcc <> "" Then
            msg.AddBcc(bcc)
        End If
        msg.BodyFormat = MailFormat.Html
        msg.Body = Server.HtmlDecode(message)
        msg.ThrowException = False
        msg.Logging = False
        msg.LogBody = False
        msg.Send()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from v_shipments where shipmentID=@shipmentID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipmentID", Session("shipmentID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                invdatetb.Text = FormatDateTime(Now(), DateFormat.ShortDate)
                serviceprofileIDlbl.Value = reader.Item("serviceprofileID")
                companyIDlbl.Value = reader.Item("companyID")
                vendorIDlbl.Value = reader.Item("vendorID")
                shipping = reader.Item("ship_charge")
                shipmentIDlbl.Text = reader.Item("shipmentID")
                pickdatelbl.Text = reader.Item("pick_date")
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = reader.Item("order_date").ToString
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                vendorlbl.Text = reader.Item("vendor").ToString
                vphonelbl.Text = reader.Item("v_phone").ToString
                vfaxlbl.Text = reader.Item("v_fax").ToString
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
                shiptolbl.Text = s_address
                userIDlbl.Value = reader.Item("userID").ToString
                contactlbl.Text = reader.Item("c_contact")
                phonelbl.Text = reader.Item("c_phone").ToString
                vfaxlbl.Text = reader.Item("c_fax").ToString
                notestb.Text = reader.Item("notes").ToString
                carriertb.Text = reader.Item("carrier").ToString
                shipchargetb.Text = reader.Item("ship_charge").ToString
                trackingtb.Text = reader.Item("tracking").ToString
                chargetaxcb.Checked = reader.Item("chargetax")
                chargetaxcb.Enabled = False
                invoiceIDtb.Text = CStr(reader.Item("shipmentID") + 10000)
                emailtotb.Text = appcode.GetCompanyBillingEmail(reader.Item("companyID"))
                emailcctb.Text = appcode.GetUserEmail(reader.Item("userID"))
                emailmessagetb.Text = "Here's your invoice. We appreciate your prompt payment and thank you for your business! Desert Fleet Outfitters, LLC"
                sendcb.Checked = False
                If reader.Item("invoiced") = True Then
                    invoicebtn.Visible = False
                End If
                If Session("surcharge") = True Then
                    surchargecb.Checked = True
                Else
                    surchargecb.Checked = False
                End If
            End If
            reader.Close()
            conn.Close()
        End If
        EmailPanel.Visible = False
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub invoicebtn_Click(sender As Object, e As EventArgs) Handles invoicebtn.Click
        If invoiceIDtb.Text <> "" Then
            appcode.UpdateInvoice(Session("shipmentID"), invoiceIDtb.Text, invdatetb.Text, Session("salestax"), True, surcharge)
            appcode.UpdateShipUserID(Session("shipmentID"), Session("userID"))
            'check customer stock room and add items if not already in there
            For Each row In GridView1.Rows
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim companyID As Integer = companyIDlbl.Value
                'is item in stock room if not then add it
                If appcode.IsWarehouseItem(manufacturerlbl.Text, partnumberlbl.Text, companyID) = False Then
                    Dim warehouseID As Integer = appcode.InsertWarehouseItem("Main", manufacturerlbl.Text, partnumberlbl.Text, companyID, 0, 1, 0)
                End If
            Next
            If sendcb.Checked = True Then
                SendInvoice(Session("shipmentID"))
            End If
            'check for backorders
            If appcode.IsOrderBO(orderIDlbl.Text) = True Then
                'SendPickTicketToPrinter(orderIDlbl.Text)
            End If
            'send packing slip to printer
            SendPackingSlipToPrinter(Session("shipmentID"))
            Response.Redirect("Invoice.aspx")
        End If
    End Sub

    Sub SendPickTicketToPrinter(ByVal orderID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim fromemail As String = "dfofilters@mc.internetmailserver.net"
        Dim filename As String = "PDF/PickTicket_" & orderID & ".pdf"
        appcode.PickTicketPDF(orderID, appcode.getAssetID(serviceprofileIDlbl.Value), applicationpath, "Images/bannerlogo.jpg", filename)
        SendEmailWithAttachments("BACKORDER", "", fromemail, "dfo-packingslips@hpeprint.com", "picktickets@dfofilters.com", "", filename, "")
    End Sub

    Sub SendPackingSlipToPrinter(ByVal shipmentID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim fromemail As String = "dfofilters@mc.internetmailserver.net"
        Dim filename As String = "PDF/Invoice_" & shipmentID & ".pdf"
        Dim cimagefile As String = ""
        appcode.InvoicePDF(shipmentID, applicationpath, "Images/dfo_banner3.jpg", cimagefile, filename)
        SendEmailWithAttachments("ORDER FOR " & appcode.GetCompany(companyIDlbl.Value) & " PICKED", "", fromemail, "dfo-packingslips@hpeprint.com", "packingslips@dfofilters.com", "", filename, "")
        ' SEND ASN

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
            surcharge = 0
            If surchargecb.Checked = True Then
                Dim invoice_total As Double = subtotal + CDbl(salestaxlbl.Text) + shipping
                surcharge = invoice_total / 0.96 - invoice_total
            End If
            surchargelbl.Text = surcharge.ToString("c")
            grandtotallbl.Text = subtotal + salestaxlbl.Text + shipping + surcharge.ToString("c")
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteShipment(Session("shipmentID"))
        Session("orderID") = orderIDlbl.Text
        Response.Redirect("Order.aspx")
    End Sub

    Sub SendInvoice(ByVal shipmentID As Integer)
        appcode.UpdateInvoice(Session("shipmentID"), invoiceIDtb.Text, invdatetb.Text, salestax, True, surcharge)
        Dim filename1 As String = "PDF/Invoice_" & shipmentID & ".pdf"
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "Invoice From " & appcode.GetCompany(Session("companyID"))
        Dim message As String = appcode.InvoiceHTML(shipmentID, emailmessagetb.Text)
        Dim fromemail As String = appcode.GetCompanyContactEmail(Session("companyID"))
        Dim bcc As String = appcode.GetCompanyBillingEmail(Session("companyID"))
        Dim cimagefile As String = ""
        If File.Exists(Server.MapPath("~/Images/Banners/" & companyIDlbl.Value & ".jpg")) = True Then
            cimagefile = "Images/Banners/" & companyIDlbl.Value & ".jpg"
        Else
            cimagefile = "Images/bannerlogo.jpg"
        End If
        appcode.InvoicePDF(shipmentID, applicationpath, "Images/dfo_banner1.jpg", cimagefile, filename1)
        appcode.UpdateBillingEmail(Session("selected_companyID"), emailtotb.Text)
        SendEmailWithAttachments(subject, message, fromemail, emailtotb.Text, emailcctb.Text, "", filename1, "")
        File.Delete(applicationpath & filename1)
    End Sub

    Sub SendEmailWithAttachments(ByVal subject As String, ByVal message As String, ByVal from As String, ByVal recipient As String, ByVal cc As String, ByVal bcc As String, ByVal filename1 As String, ByVal filename2 As String)
        Dim msg As New EmailMessage()
        msg.Server = "localhost"
        msg.FromAddress = from
        msg.ValidateAddress = False
        msg.AddTo(recipient)
        msg.Subject = subject
        If cc <> "" Then
            msg.AddCc(cc)
        End If
        If bcc <> "" Then
            msg.AddBcc(bcc)
        End If
        msg.BodyFormat = MailFormat.Html
        msg.Body = Server.HtmlDecode(message)
        msg.AddAttachment(Server.MapPath("~/" & filename1))
        If filename2 <> "" Then
            msg.AddAttachment(Server.MapPath("~/" & filename2))
        End If
        msg.ThrowException = False
        msg.Logging = False
        msg.LogBody = False
        msg.Send()
    End Sub

    Protected Sub sendcb_CheckedChanged(sender As Object, e As EventArgs) Handles sendcb.CheckedChanged
        EmailPanel.Visible = sendcb.Checked
    End Sub

    Protected Sub surchargecb_CheckedChanged(sender As Object, e As EventArgs) Handles surchargecb.CheckedChanged
        If surchargecb.Checked = True Then
            Session("surcharge") = True
        Else
            Session("surcharge") = False
        End If
        Response.Redirect("EditInvoice.aspx")
    End Sub

    Protected Sub chargetaxcb_CheckedChanged(sender As Object, e As EventArgs) Handles chargetaxcb.CheckedChanged

    End Sub
End Class
