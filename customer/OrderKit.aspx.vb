Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class customer_OrderKit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("equipmentID").ToString <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                Dim comm As New SqlCommand
                Dim reader As SqlDataReader
                commandString = "select * from t_equipment where equipmentID=@equipmentID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@equipmentID", Session("equipmentID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    Dim equipmentname As String = ""
                    If reader.Item("equipment_year").ToString <> "" Then
                        equipmentname &= reader.Item("equipment_year") & " "
                    End If
                    equipmentname &= reader.Item("equipment_oem")
                    If reader.Item("equipment_model").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_model")
                    End If
                    If reader.Item("equipment_description").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_description")
                    End If
                    equipmentlbl.Text = equipmentname
                    equipmentdd.SelectedValue = Session("equipmentID")
                    hourslbl.Text = reader.Item("interval_type")
                    datelbl.Text = appcode.GetLastKitDate(reader.Item("equipmentID")) & " (" & reader.Item("hours_miles") & " " & reader.Item("interval_type") & ")"
                    pmlbl.Text = appcode.GetLastKitName(reader.Item("equipmentID")) & "/" & appcode.GetLastKitInterval(reader.Item("equipmentID"))
                    If File.Exists(Server.MapPath("~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg")) = True Then
                        equipmentImage.ImageUrl = "~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg"
                    End If
                Else
                    ' Panel1.Visible = False
                    ' Panel2.Visible = False
                    profilebtn.Visible = False
                    historybtn.Visible = False
                End If
                reader.Close()
                conn.Close()
                hourstb.Focus()
            End If
        End If
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

    Sub SendOrderConfirmation(ByVal orderID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "Order Request From " & appcode.GetCompany(Session("companyID"))
        Dim message As String = appcode.WebOrderHTML(Session("orderID"), "", Application("dfo_banner_weborder"), Application("site_address"))
        Dim fromemail As String = "donotreply@dfofilters.com"
        SendEmail(subject, message, fromemail, Application("orders_email"), "", "")
    End Sub

    Function InsertCoreLine(ByVal productID As Integer, ByVal quantity As Double, ByVal orderID As Integer, ByVal kitID As Integer) As Integer
        InsertCoreLine = 0
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        Dim comm As New SqlCommand
        Dim reader As SqlDataReader
        commandString = "select * from t_product where productID=@productID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        reader = comm.ExecuteReader
        If reader.Read Then
            InsertCoreLine = appcode.InsertOrderLine(orderID, 0, 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), quantity, reader.Item("msrp"), reader.Item("uom"), "", True, kitID)
        End If
        reader.Close()
        conn.Close()
    End Function

    Protected Sub CheckOut(ByVal kitID As Integer, ByVal shipID As Integer, ByVal confirm As Boolean)
        Dim isKit As Boolean = True
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
        comm.Parameters.AddWithValue("@shipID", shipID)
        reader = comm.ExecuteReader
        If reader.Read Then
            shipto = reader.Item("shipto")
            s_address1 = reader.Item("s_address1")
            s_address2 = reader.Item("s_address2")
            s_address3 = reader.Item("s_address3")
            s_city = reader.Item("s_city")
            s_state = reader.Item("s_state")
            s_zipcode = reader.Item("s_zipcode")
        End If
        reader.Close()
        commandString = "select * from t_user where userID=@userID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", Session("userID"))
        reader = comm.ExecuteReader
        If reader.Read Then
            u_name = reader.Item("name").ToString
            u_phone = reader.Item("c_phone").ToString
            u_fax = reader.Item("c_fax").ToString
        End If
        reader.Close()
        Dim repID As Integer = appcode.GetRepID(Session("selected_companyID"))
        Dim purchaseorder As String = potb.Text
        Dim orderID As Integer = appcode.InsertOrder(potb.Text, Session("selected_companyID"), appcode.GetCompany(Session("selected_companyID")), Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, Session("userID"), u_name, u_phone, u_fax, "", "Best Way", "Ship Complete", isKit, 0, kitID, "", False, repID)
        appcode.UpdateOrderKitHours(hourstb.Text, orderID)

        commandString = "select * from t_cart where userID=@userID and companyID=@companyID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", Session("userID"))
        comm.Parameters.AddWithValue("@companyID", Session("selected_companyID"))
        reader = comm.ExecuteReader

        While reader.Read
            Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), reader.Item("quantity"), reader.Item("price"), reader.Item("uom"), "", True, kitID)
            'if line has core charge, add line
            Dim coreID As Integer = appcode.GetCoreID(reader.Item("productID"))
            If coreID > 0 Then
                InsertCoreLine(coreID, reader.Item("quantity"), orderID, kitID)
            End If
        End While
        reader.Close()
        appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), kitID)
        appcode.UpdateDeliverBy(orderID, deliverbytb.Text)
        Session("orderID") = orderID
        If confirm = True Then
            SendOrderConfirmation(Session("orderID"))
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "order" Then
            If equipmentdd.SelectedValue <> "0" Then
                If potb.Text <> "" Then
                    If IsDate(deliverbytb.Text) = True Then
                        Dim index = Convert.ToInt32(e.CommandArgument)
                        Dim serviceprofileIDlbl As Label = GridView1.Rows(index).FindControl("serviceprofileIDlbl")
                        Session("serviceprofileIDlbl") = serviceprofileIDlbl.Text
                        'add parts to cart
                        Dim productID As Integer
                        Dim conn As New SqlConnection(appcode.ConnectionString)
                        conn.Open()
                        Dim commandString As String = "select * from v_serviceparts where serviceprofileID=@serviceprofileID and selected=@selected"
                        Dim comm As New SqlCommand(commandString, conn)
                        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileIDlbl.Text)
                        comm.Parameters.AddWithValue("@selected", True)
                        Dim reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            productID = appcode.GetProductID(reader.Item("manufacturer"), reader.Item("partnumber"))
                            If productID = 0 Then
                                'insert part number
                                'productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, 0, False, 0, 0)
                                productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, 0, 0, False, 0, 0)

                            End If
                            Dim price As Double = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
                            Dim cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), productID, reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString, reader.Item("description").ToString, reader.Item("quantity"), price, reader.Item("uom"), 0, serviceprofileIDlbl.Text)
                        End While
                        reader.Close()
                        conn.Close()
                        If IsNumeric(hourstb.Text) = False Then
                            hourstb.Text = "0"
                        End If
                        appcode.UpdateHoursMiles(Session("equipmentID"), hourstb.Text)
                        CheckOut(serviceprofileIDlbl.Text, shiptodd.SelectedValue, confirmcb.Checked)
                        Response.Redirect("Order.aspx")
                    Else
                        errmsglbl.Text = "Please provide a valid delivery date."
                        errmsglbl.Visible = True
                    End If
                Else
                    errmsglbl.Text = "Please provide a PO."
                    errmsglbl.Visible = True
                End If
            Else
                errmsglbl.Text = "You must select an asset."
                errmsglbl.Visible = True
            End If
        ElseIf e.CommandName = "add" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim serviceprofileIDlbl As Label = GridView1.Rows(index).FindControl("serviceprofileIDlbl")
            Session("serviceprofileIDlbl") = serviceprofileIDlbl.Text
            'add parts to cart
            Dim productID As Integer
            Dim conn As New SqlConnection(appcode.ConnectionString)
            conn.Open()
            Dim commandString As String = "select * from v_serviceparts where serviceprofileID=@serviceprofileID and selected=@selected"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileIDlbl.Text)
            comm.Parameters.AddWithValue("@selected", True)
            Dim reader As SqlDataReader = comm.ExecuteReader
            While reader.Read
                productID = appcode.GetProductID(reader.Item("manufacturer"), reader.Item("partnumber"))
                If productID = 0 Then
                    'insert part number
                    'productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, 0, False, 0, 0)
                    productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, 0, 0, False, 0, 0)
                End If
                Dim price As Double = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
                Dim cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), productID, reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString, reader.Item("description").ToString, reader.Item("quantity"), price, reader.Item("uom"), 0, serviceprofileIDlbl.Text)
            End While
            reader.Close()
            conn.Close()
            If IsNumeric(hourstb.Text) = False Then
                hourstb.Text = "0"
            End If
            appcode.UpdateHoursMiles(Session("equipmentID"), hourstb.Text)
            Response.Redirect("Cart.aspx")
        ElseIf e.CommandName = "edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim serviceprofileIDlbl As Label = GridView1.Rows(index).FindControl("serviceprofileIDlbl")
            Session("serviceprofileID") = serviceprofileIDlbl.Text
            Response.Redirect("ServiceKit.aspx")
        End If
    End Sub

    Protected Sub equipmentdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles equipmentdd.SelectedIndexChanged
        If equipmentdd.SelectedValue <> "0" Then
            Session("equipmentID") = equipmentdd.SelectedValue
            Response.Redirect("OrderKit.aspx")
        End If
    End Sub

    Protected Sub profilebtn_Click(sender As Object, e As EventArgs) Handles profilebtn.Click
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub newkitbtn_Click(sender As Object, e As EventArgs) Handles newkitbtn.Click
        Session("serviceprofileID") = "0"
        Response.Redirect("ServiceKit.aspx")
    End Sub

    Protected Sub historybtn_Click(sender As Object, e As EventArgs) Handles historybtn.Click
        Response.Redirect("EquipmentOrderHistory.aspx")
    End Sub

End Class
