﻿Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_CheckOut
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private surcharge_cartID = 0

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

    Sub SendOrderRequest(ByVal orderID As Integer, ByVal companyID As Integer, ByVal repemail As String)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "Order Request From " & appcode.GetCompany(companyID)
        Dim message As String = appcode.OrderConfirmationHTML(Session("orderID"), "", Application("banner_order"), Application("site_address"))
        Dim fromemail As String = Application("orders_email")
        SendEmail(subject, message, fromemail, repemail, "", "")
    End Sub

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
        If HasShipTo() = True Then
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
            shipto = shiptotb.Text
            s_address1 = saddress1tb.Text
            s_address2 = saddress2tb.Text
            s_address3 = saddress3tb.Text
            s_city = scitytb.Text
            s_state = sstatetb.Text
            s_zipcode = szipcodetb.Text
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
            Dim serviceprofileID As Integer = appcode.GetServiceProfileIDFromCart(Session("userID"), Session("selected_companyID"))
            Dim assetID As String = appcode.getAssetID(serviceprofileID)
            Dim isKit As Boolean = False
            If serviceprofileID <> 0 Then
                isKit = True
            End If
            Dim repID As Integer = appcode.GetRepID(Session("selected_companyID"))
            Dim orderID As Integer = appcode.InsertOrder(potb.Text, Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(contactdd.SelectedValue), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, notestb.Text, shipmethoddd.SelectedValue, shipotionsdd.SelectedValue, isKit, 0, serviceprofileID, referencetb.Text, rushcb.Checked, repID)
            appcode.UpdateOrderChargeTax(orderID, chargetaxcb.Checked)
            If Session("hours_miles").ToString <> "0" Then
                appcode.UpdateOrderKitHours(Session("hours_miles"), orderID)
            End If
            For Each row In GridView1.Rows
                Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemnamelbl As Label = row.FindControl("itemnamelbl")
                Dim qtytb As Label = row.FindControl("qtytb")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim kitIDlbl As Label = row.FindControl("kitIDlbl")
                If qtytb.Text <> "" Then
                    Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, assetID, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", True, kitIDlbl.Text)
                    'if line has core charge, add line
                    Dim coreID As Integer = appcode.GetCoreID(productIDlbl.Text)
                    If coreID > 0 Then
                        commandString = "select * from t_product where productID=@productID"
                        comm = New SqlCommand(commandString, conn)
                        comm.Parameters.AddWithValue("@productID", coreID)
                        reader = comm.ExecuteReader
                        If reader.Read Then
                            lineID = appcode.InsertOrderLine(orderID, 0, 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), qtytb.Text, reader.Item("msrp"), reader.Item("uom"), "", True, "0")
                        End If
                        reader.Close()
                    End If

                End If
            Next
            conn.Close()
            appcode.UpdateNeedsPO(orderID, needspocb.Checked)
            appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), 0)
            If serviceprofileID > 0 Then
                Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, assetID, "DFO", "KC1", "Kit Charge", 1, Application("kitcharge"), "EACH", "", False, serviceprofileID)
            End If
            Session("orderID") = orderID
            'email order request
            Dim repemail As String = appcode.GetUserEmail(repID)
            SendOrderRequest(orderID, Session("selected_companyID"), repemail)
            Response.Redirect("EditOrder.aspx")
        End If
    End Sub

    Protected Sub contactdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles contactdd.SelectedIndexChanged
        Session("selected_userID") = contactdd.SelectedValue.ToString
        emailtb.Text = appcode.GetUserEmail(contactdd.SelectedValue)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            shipotionsdd.SelectedValue = "Ship Complete"
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_company where companyID=@companyID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("selected_companyID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                customerlbl.Text = reader.Item("company").ToString
                Dim b_address As String = ""
                If reader.Item("address1").ToString <> "" Then
                    b_address = reader.Item("address1") & "<br/>"
                    If reader.Item("address2").ToString <> "" Then
                        b_address &= reader.Item("address2") & "<br/>"
                    End If
                    b_address &= reader.Item("city") & ", " & reader.Item("state") & " " & reader.Item("zipcode")
                End If
                billtolbl.Text = b_address
                chargetaxcb.Checked = Session("chargetax")
            End If
            reader.Close()
            If Session("selected_shipID").ToString = "" Then
                Session("selected_shipID") = appcode.GetDefaultShipID(Session("selected_companyID"))
            End If
            If Session("selected_userID").ToString = "" Then
                Session("selected_userID") = appcode.GetDefaultUserID(Session("selected_companyID"))
            End If
            contactdd.SelectedValue = Session("selected_userID")
            If Session("selected_shipID").ToString <> "" Then
                commandString = "select * from t_ship where shipID=@shipID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@shipID", Session("selected_shipID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    shiptodd.SelectedValue = reader.Item("shipID")
                    shiptotb.Text &= reader.Item("shipto").ToString
                    saddress1tb.Text = reader.Item("s_address1").ToString
                    saddress2tb.Text = reader.Item("s_address2").ToString
                    saddress3tb.Text = reader.Item("s_address3").ToString
                    scitytb.Text = reader.Item("s_city").ToString
                    sstatetb.Text = reader.Item("s_state").ToString
                    szipcodetb.Text = reader.Item("s_zipcode").ToString
                End If
            End If
            emailtb.Text = appcode.GetUserEmail(contactdd.SelectedValue)
        End If
        pagelbl.Text = Page.Title
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
                corelbl.Text = " * " & FormatCurrency(coretotal, 2) & " Core Charge applies"
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
            Dim chargetax As Boolean = chargetaxcb.Checked
            If chargetax = True Then
                salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID"))).ToString("c")
                grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID")))).ToString("c")
            Else
                Dim notax As Double = 0.0
                salestaxlbl.Text = notax.ToString("c")
                grandtotallbl.Text = subtotal.ToString("c")
            End If
        End If
    End Sub

    Protected Sub shiptodd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shiptodd.SelectedIndexChanged
        Session("selected_shipID") = shiptodd.SelectedValue
        Response.Redirect("CheckOut.aspx")
    End Sub

    Protected Function HasShipTo() As Boolean
        HasShipTo = False
        If shiptotb.Text <> "" And saddress1tb.Text <> "" And scitytb.Text <> "" And sstatetb.Text <> "" And szipcodetb.Text <> "" Then
            HasShipTo = True
        End If
    End Function

    Protected Sub returnbtn_Click(sender As Object, e As EventArgs) Handles returnbtn.Click
        Dim x As Integer = 0
        For Each row In GridView1.Rows
            x += 1
        Next
        If x > 0 Then
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
            Dim orderID As Integer = appcode.InsertReturn(Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, notestb.Text, "Local Delivery", "Ship & BO", False, 0, 0)
            For Each row In GridView1.Rows
                Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemnamelbl As Label = row.FindControl("itemnamelbl")
                Dim qtytb As Label = row.FindControl("qtytb")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim kitIDlbl As Label = row.FindControl("kitIDlbl")
                If qtytb.Text <> "" Then
                    Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", True, kitIDlbl.Text)
                End If
            Next
            appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), 0)
            Session("orderID") = orderID
            Response.Redirect("EditReturn.aspx")
        End If
    End Sub

    Protected Sub quotebtn_Click(sender As Object, e As EventArgs) Handles quotebtn.Click
        Dim x As Integer = 0
        For Each row In GridView1.Rows
            x += 1
        Next
        If x > 0 Then
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
            Dim quoteID As Integer = appcode.InsertQuote(Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, True, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, False, 0, 0)
            For Each row In GridView1.Rows
                Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemnamelbl As Label = row.FindControl("itemnamelbl")
                Dim qtytb As Label = row.FindControl("qtytb")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim kitIDlbl As Label = row.FindControl("kitIDlbl")
                If qtytb.Text <> "" Then
                    Dim lineID As Integer = appcode.InsertQuoteLine(quoteID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", False, kitIDlbl.Text)
                End If
            Next
            appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), 0)
            Session("quoteID") = quoteID
            Response.Redirect("EditQuote.aspx")
        End If
    End Sub

    Protected Sub chargetaxcb_CheckedChanged(sender As Object, e As EventArgs) Handles chargetaxcb.CheckedChanged
        If chargetaxcb.Checked = True Then
            Session("chargetax") = True
        Else
            Session("chargetax") = False
        End If
        Response.Redirect("CheckOut.aspx")
    End Sub

End Class
