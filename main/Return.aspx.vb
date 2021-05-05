Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_Return
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                editbtn.Visible = False
            End If
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
                totb.Text = appcode.GetCompanyBillingEmail(reader.Item("companyID"))
                cctb.Text = appcode.GetUserEmail(reader.Item("userID"))
                contactlbl.Text = reader.Item("c_contact")
                phonelbl.Text = reader.Item("c_phone").ToString
                vfaxlbl.Text = reader.Item("c_fax").ToString
                notestb.Text = reader.Item("notes").ToString
                shipmethoddd.SelectedValue = reader.Item("ship_method").ToString
                shipotionsdd.SelectedValue = reader.Item("ship_options").ToString
                kitcb.Checked = reader.Item("is_kit")
                kitIDlbl.Text = reader.Item("serviceprofileID")
                serviceIDlbl.Value = reader.Item("serviceID")
                shipmethoddd.Enabled = False
                shipotionsdd.Enabled = False
                kitcb.Enabled = False
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
        Dim shipmentID As Integer = appcode.InsertShipment(Session("orderID"), FormatDateTime(Now(), DateFormat.ShortDate))
        For Each row In GridView1.Rows
            Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim qtytb As Label = row.FindControl("qtytb")
            If qtytb.Text > 0 Then
                Dim qty As Double = qtytb.Text * -1
                appcode.InsertShipmentLine(shipmentID, lineIDlbl.Text, qty)
                'reduce requisition qty by shipment qty
                Dim reqqty As Double = appcode.GetRequisitionQuantity(lineIDlbl.Text)
                appcode.UpdateRequisitionQty(lineIDlbl.Text, reqqty - qtytb.Text)
                Dim onhand As Double = appcode.GetOnHand(manufacturerlbl.Text, partnumberlbl.Text)
                appcode.UpdateOnHand(manufacturerlbl.Text, partnumberlbl.Text, onhand + CDbl(qtytb.Text))
            End If
            If appcode.IsOrderLineOpen(lineIDlbl.Text) = False Then
                appcode.DeleteRequisition(lineIDlbl.Text)
            End If
        Next
        appcode.UpdateOrderComplete(Session("orderID"), True)
        appcode.UpdateShipment(shipmentID, "Local Delivery", 0, "", True, FormatDateTime(Now(), DateFormat.ShortDate))
        appcode.UpdateInvoice(shipmentID, shipmentID + 10000, FormatDateTime(Now(), DateFormat.ShortDate), Session("salestax"), True, False)
        If sendcb.Checked = True Then
            SendInvoice(shipmentID)
        End If
        Session("shipmentID") = shipmentID
        Response.Redirect("Invoice.aspx")
    End Sub

    Sub SendInvoice(ByVal shipmentID As Integer)
        Dim filename1 As String = "PDF/CreditMemo_" & shipmentID & ".pdf"
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "Credit Memo From " & appcode.GetCompany(Session("companyID"))
        Dim message As String = appcode.CreditMemoHTML(shipmentID, emailmessagetb.Text)
        Dim fromemail As String = appcode.GetCompanyContactEmail(Session("companyID"))
        Dim bcc As String = appcode.GetCompanyBillingEmail(Session("companyID"))
        appcode.CreditMemoPDF(shipmentID, applicationpath, "Images/desertlogo.jpg", filename1)
        SendEmailWithAttachments(subject, message, fromemail, totb.Text, cctb.Text, "", filename1, "")
        File.Delete(applicationpath & filename1)
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
        Response.Redirect("EditReturn.aspx")
    End Sub

End Class
