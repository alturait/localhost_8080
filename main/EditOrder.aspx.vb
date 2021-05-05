Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class EditOrder
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private totalgp As Decimal = 0

    Protected Sub chargetaxcb_CheckedChanged(sender As Object, e As EventArgs) Handles chargetaxcb.CheckedChanged
        If chargetaxcb.Checked = True Then
            appcode.UpdateOrderChargeTax(Session("orderID"), True)
        Else
            appcode.UpdateOrderChargeTax(Session("orderID"), False)
        End If
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim linetotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extended"))
            Dim linecost As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extendedcost"))
            subtotal = subtotal + linetotal
            totalgp = totalgp + (linetotal - linecost)
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
            Dim chargetax As Boolean = chargetaxcb.Checked
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
            commandString = "select * from t_order where orderID=@orderID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", Session("orderID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                companyIDlbl.Value = reader.Item("companyID")
                orderIDlbl.Text = reader.Item("orderID").ToString
                vendorIDlbl.Value = reader.Item("vendorID")
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = reader.Item("order_date").ToString
                If reader.Item("deliverby_date").ToString <> "" Then
                    deliverby_datetb.Text = reader.Item("deliverby_date")
                Else
                    deliverby_datetb.Text = ""
                End If
                vendorlbl.Text = reader.Item("vendor").ToString
                vphonelbl.Text = reader.Item("v_phone").ToString
                vfaxlbl.Text = reader.Item("v_fax").ToString
                If reader.Item("serviceprofileID") > 0 Then
                    assetIDlbl.Text = appcode.getAssetID(reader.Item("serviceprofileID"))
                    equipmentlbl.Text = appcode.GetEquipment(appcode.GetEquipmentIDFromKitID(reader.Item("serviceprofileID")))
                    kitlbl.Text = appcode.GetKitName(reader.Item("serviceprofileID"))
                        hourslbl.Text = reader.Item("hours_miles")
                        Panel1.Visible = True
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
                shiptodd.SelectedValue = reader.Item("shipID")
                Dim s_address As String = ""
                If reader.Item("s_address1").ToString <> "" Then
                    s_address = reader.Item("s_address1") & "<br/>"
                    If reader.Item("r_address2").ToString <> "" Then
                        s_address &= reader.Item("s_address2") & "<br/>"
                    End If
                    s_address &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
                End If
                shiptolbl.Text = s_address
                contactdd.SelectedValue = reader.Item("userID")
                totb.Text = appcode.GetUserEmail(reader.Item("userID"))
                phonelbl.Text = reader.Item("c_phone").ToString
                notestb.Text = reader.Item("notes").ToString
                shipmethoddd.SelectedValue = reader.Item("ship_method").ToString
                shipotionsdd.SelectedValue = reader.Item("ship_options").ToString
                chargetaxcb.Checked = reader.Item("chargetax")
                serviceIDlbl.Value = reader.Item("serviceprofileID")
                placedlbl.Value = reader.Item("placed")
                If reader.Item("confirmed") = True Then
                    submitbtn.Text = "Save"
                    savebtn.Text = "Cancel"
                    sendcb.Checked = False
                Else
                    sendcb.Checked = True
                End If
            End If
            EmailPanel.Visible = sendcb.Checked
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub sendcb_CheckedChanged(sender As Object, e As EventArgs) Handles sendcb.CheckedChanged
        EmailPanel.Visible = sendcb.Checked
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        appcode.DeleteOrder(Session("orderID"))
        If Session("selected_companyID") <> "0" Then
            Response.Redirect("Orders.aspx")
        Else
            Response.Redirect("OpenOrders.aspx")
        End If
    End Sub

    Protected Sub shiptodd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shiptodd.SelectedIndexChanged
        shiptolbl.Text = appcode.GetCompanyShipToAddress(shiptodd.SelectedValue)
    End Sub

    Protected Sub contactdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles contactdd.SelectedIndexChanged
        phonelbl.Text = appcode.GetUserPhone(contactdd.SelectedValue)
        totb.Text = appcode.GetUserEmail(contactdd.SelectedValue)
    End Sub

    Protected Sub updatelinesbtn_Click(sender As Object, e As EventArgs) Handles updatelinesbtn.Click
        For Each row In GridView1.Rows
            Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
            Dim qtytb As TextBox = row.FindControl("qtytb")
            Dim pricetb As TextBox = row.FindControl("pricetb")
            Dim availabilitytb As TextBox = row.FindControl("availabilitytb")
            appcode.UpdateVendorOrderLine(lineIDlbl.Text, qtytb.Text, pricetb.Text, "")
        Next
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim lineIDlbl As Label = GridView1.Rows(index).FindControl("lineIDlbl")
            appcode.DeleteOrderLine(lineIDlbl.Text)
            Response.Redirect("EditOrder.aspx")
        ElseIf e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim lineIDlbl As Label = GridView1.Rows(index).FindControl("lineIDlbl")
            Session("lineID") = lineIDlbl.Text
            Response.Redirect("EditOrderLine.aspx")
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If IsDate(deliverby_datetb.Text) = True Then
            Dim v_company As String = ""
            Dim v_phone As String = ""
            Dim v_fax As String = ""
            Dim v_address1 As String = ""
            Dim v_address2 As String = ""
            Dim v_city As String = ""
            Dim v_state As String = ""
            Dim v_zipcode As String = ""
            Dim b_company As String = ""
            Dim b_phone As String = ""
            Dim b_fax As String = ""
            Dim b_address1 As String = ""
            Dim b_address2 As String = ""
            Dim b_city As String = ""
            Dim b_state As String = ""
            Dim b_zipcode As String = ""
            Dim shipto As String = ""
            Dim s_phone As String = ""
            Dim s_address1 As String = ""
            Dim s_address2 As String = ""
            Dim s_address3 As String = ""
            Dim s_city As String = ""
            Dim s_state As String = ""
            Dim s_zipcode As String = ""
            Dim u_name As String = ""
            Dim u_phone As String = ""
            Dim u_fax As String = ""
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_company where companyID=@companyID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("vendorID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                v_company = reader.Item("company").ToString
                v_phone = reader.Item("c_phone").ToString
                v_fax = reader.Item("c_fax").ToString
                v_address1 = reader.Item("address1").ToString
                v_address2 = reader.Item("address2").ToString
                v_city = reader.Item("city").ToString
                v_state = reader.Item("state").ToString
                v_zipcode = reader.Item("zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_company where companyID=@companyID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("selected_companyID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                b_company = reader.Item("company").ToString
                b_phone = reader.Item("c_phone").ToString
                b_fax = reader.Item("c_fax").ToString
                b_address1 = reader.Item("address1").ToString
                b_address2 = reader.Item("address2").ToString
                b_city = reader.Item("city").ToString
                b_state = reader.Item("state").ToString
                b_zipcode = reader.Item("zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_ship where shipID=@shipID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipID", shiptodd.SelectedValue)
            reader = comm.ExecuteReader
            If reader.Read Then
                shipto = reader.Item("shipto").ToString
                s_phone = reader.Item("s_phone").ToString
                s_address1 = reader.Item("s_address1").ToString
                s_address2 = reader.Item("s_address2").ToString
                s_address3 = reader.Item("s_address3").ToString
                s_city = reader.Item("s_city").ToString
                s_state = reader.Item("s_state").ToString
                s_zipcode = reader.Item("s_zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_user where userID=@userID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@userID", contactdd.SelectedValue)
            reader = comm.ExecuteReader
            If reader.Read Then
                u_name = reader.Item("name").ToString
                u_phone = reader.Item("c_phone").ToString
                u_fax = reader.Item("c_fax").ToString
            End If
            reader.Close()
            conn.Close()
            appcode.UpdateOrder(Session("orderID"), "", deliverby_datetb.Text, "", False, False, "", shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, contactdd.SelectedItem.Text, appcode.GetUserPhone(contactdd.SelectedValue), "", notestb.Text, shipmethoddd.SelectedValue, shipotionsdd.SelectedValue, False)
            For Each row In GridView1.Rows
                Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
                Dim qtytb As TextBox = row.FindControl("qtytb")
                Dim pricetb As TextBox = row.FindControl("pricetb")
                Dim availabilitytb As TextBox = row.FindControl("availabilitytb")
                appcode.UpdateVendorOrderLine(lineIDlbl.Text, qtytb.Text, pricetb.Text, "")
            Next
            Response.Redirect("Order.aspx")

        End If
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        If potb.Text <> "" And IsDate(deliverby_datetb.Text) = True Then
            Dim v_company As String = ""
            Dim v_phone As String = ""
            Dim v_fax As String = ""
            Dim v_address1 As String = ""
            Dim v_address2 As String = ""
            Dim v_city As String = ""
            Dim v_state As String = ""
            Dim v_zipcode As String = ""
            Dim b_company As String = ""
            Dim b_phone As String = ""
            Dim b_fax As String = ""
            Dim b_address1 As String = ""
            Dim b_address2 As String = ""
            Dim b_city As String = ""
            Dim b_state As String = ""
            Dim b_zipcode As String = ""
            Dim shipto As String = ""
            Dim s_phone As String = ""
            Dim s_address1 As String = ""
            Dim s_address2 As String = ""
            Dim s_address3 As String = ""
            Dim s_city As String = ""
            Dim s_state As String = ""
            Dim s_zipcode As String = ""
            Dim u_name As String = ""
            Dim u_phone As String = ""
            Dim u_fax As String = ""
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_company where companyID=@companyID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("vendorID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                v_company = reader.Item("company").ToString
                v_phone = reader.Item("c_phone").ToString
                v_fax = reader.Item("c_fax").ToString
                v_address1 = reader.Item("address1").ToString
                v_address2 = reader.Item("address2").ToString
                v_city = reader.Item("city").ToString
                v_state = reader.Item("state").ToString
                v_zipcode = reader.Item("zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_company where companyID=@companyID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("selected_companyID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                b_company = reader.Item("company").ToString
                b_phone = reader.Item("c_phone").ToString
                b_fax = reader.Item("c_fax").ToString
                b_address1 = reader.Item("address1").ToString
                b_address2 = reader.Item("address2").ToString
                b_city = reader.Item("city").ToString
                b_state = reader.Item("state").ToString
                b_zipcode = reader.Item("zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_ship where shipID=@shipID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipID", shiptodd.SelectedValue)
            reader = comm.ExecuteReader
            If reader.Read Then
                shipto = reader.Item("shipto").ToString
                s_phone = reader.Item("s_phone").ToString
                s_address1 = reader.Item("s_address1").ToString
                s_address2 = reader.Item("s_address2").ToString
                s_address3 = reader.Item("s_address3").ToString
                s_city = reader.Item("s_city").ToString
                s_state = reader.Item("s_state").ToString
                s_zipcode = reader.Item("s_zipcode").ToString
            End If
            reader.Close()
            commandString = "select * from t_user where userID=@userID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@userID", contactdd.SelectedValue)
            reader = comm.ExecuteReader
            If reader.Read Then
                u_name = reader.Item("name").ToString
                u_phone = reader.Item("c_phone").ToString
                u_fax = reader.Item("c_fax").ToString
            End If
            reader.Close()
            conn.Close()
            appcode.UpdateOrder(Session("orderID"), potb.Text, deliverby_datetb.Text, FormatDateTime(Now(), DateFormat.ShortDate), True, True, appcode.GetUserName(Session("userID")), shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, contactdd.SelectedItem.Text, appcode.GetUserPhone(contactdd.SelectedValue), "", notestb.Text, shipmethoddd.SelectedValue, shipotionsdd.SelectedValue, False)
            For Each row In GridView1.Rows
                Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
                Dim qtytb As TextBox = row.FindControl("qtytb")
                Dim pricetb As TextBox = row.FindControl("pricetb")
                Dim availabilitytb As TextBox = row.FindControl("availabilitytb")
                appcode.UpdateVendorOrderLine(lineIDlbl.Text, qtytb.Text, pricetb.Text, "")
            Next
            If sendcb.Checked = True Then
                SendOrderConfirmation(Session("orderID"))
            End If
            SendOrder(Session("orderID"))
            Response.Redirect("Order.aspx")
        End If
    End Sub

    Sub SendOrder(ByVal orderID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim fromemail As String = "dfofilters@mc.internetmailserver.net"
        Dim filename As String = "PDF/PickTicket_" & orderID & ".pdf"
        appcode.PickTicketPDF(orderID, appcode.getAssetID(serviceIDlbl.Value), applicationpath, "Images/bannerlogo.jpg", filename)
        'SendEmailWithAttachments("ORDER RECEIVED FROM" & appcode.GetCompany(companyIDlbl.Value), "", fromemail, "dfo-packingslips@hpeprint.com", "picktickets@dfofilters.com", "", filename, "")
        Dim subject As String = "Order Request From " & appcode.GetCompany(companyIDlbl.Value)
        Dim message As String = appcode.OrderConfirmationHTML(orderID, emailmessagetb.Text, Application("banner_order"), Application("site_address"))
        SendEmail(subject, message, fromemail, Application("orders_email"), "", "")
    End Sub

    Sub SendOrderConfirmation(ByVal orderID As Integer)
        Dim repemail As String = appcode.GetUserEmail(appcode.GetRepID(companyIDlbl.Value))
        Dim subject As String = "Order Confirmation From " & appcode.GetCompany(Session("companyID"))
        Dim message As String = appcode.OrderConfirmationHTML(orderID, emailmessagetb.Text, Application("banner_order"), Application("site_address"))
        Dim fromemail As String = Application("orders_email")
        SendEmail(subject, message, fromemail, totb.Text, cctb.Text, repemail)
    End Sub

    Protected Sub addlinebtn_Click(sender As Object, e As EventArgs) Handles addlinebtn.Click
        Response.Redirect("AddOrderLine.aspx")
    End Sub
End Class
