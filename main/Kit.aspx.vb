Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class main_Kit
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Update" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim cartIDlbl As Label = GridView1.Rows(index).FindControl("cartIDlbl")
            Dim qtytb As TextBox = GridView1.Rows(index).FindControl("qtytb")
            appcode.UpdateCartItem(cartIDlbl.Text, qtytb.Text)
            Response.Redirect("Kit.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim cartIDlbl As Label = GridView1.Rows(index).FindControl("cartIDlbl")
            appcode.DeleteCartItem(cartIDlbl.Text)
            Response.Redirect("Kit.aspx")
        End If
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), kitIDlbl.Text)
        Response.Redirect("EditService.aspx")
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
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
            'Dim orderID As Integer = appcode.InsertOrder("", Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, "", "Local Delivery", "Ship & BO", True, Session("serviceprofileID"), kitIDlbl.Text, "", False)
            Dim orderID As Integer = appcode.InsertOrder("", Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, "", "Local Delivery", "Ship & BO", True, Session("serviceprofileID"), kitIDlbl.Text, "", False, 250)
            For Each row In GridView1.Rows
                Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemnamelbl As Label = row.FindControl("itemnamelbl")
                Dim qtytb As TextBox = row.FindControl("qtytb")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If qtytb.Text <> "" Then
                    'Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", True)
                    Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", True, kitIDlbl.Text)
                End If
            Next
            appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), kitIDlbl.Text)
            Session("orderID") = orderID
            Response.Redirect("EditOrder.aspx")
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
            Dim quoteID As Integer = appcode.InsertQuote(Session("selected_companyID"), customerlbl.Text, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, True, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, contactdd.SelectedValue, u_name, u_phone, u_fax, True, Session("serviceprofileID"), kitIDlbl.Text)
            For Each row In GridView1.Rows
                Dim cartIDlbl As Label = row.FindControl("cartIDlbl")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemnamelbl As Label = row.FindControl("itemnamelbl")
                Dim qtytb As TextBox = row.FindControl("qtytb")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If qtytb.Text <> "" Then
                    'Dim lineID As Integer = appcode.InsertQuoteLine(quoteID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", False)
                    Dim lineID As Integer = appcode.InsertQuoteLine(quoteID, 0, 0, manufacturerlbl.Text, partnumberlbl.Text, itemnamelbl.Text, qtytb.Text, pricelbl.Text, uomlbl.Text, "", False, kitIDlbl.Text)
                End If
            Next
            appcode.DeleteCartItems(Session("selected_companyID"), Session("vendorID"), Session("userID"), kitIDlbl.Text)
            Session("quoteID") = quoteID
            Response.Redirect("EditQuote.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            kitIDlbl.Text = Session("serviceprofileID")
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
            End If
            reader.Close()
            commandString = "select * from t_company where companyID=@vendorID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@vendorID", Session("vendorID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                vendorlbl.Text = reader.Item("company").ToString
                Dim r_address As String = ""
                If reader.Item("address1").ToString <> "" Then
                    r_address = reader.Item("address1") & "<br/>"
                    If reader.Item("address2").ToString <> "" Then
                        r_address &= reader.Item("address2") & "<br/>"
                    End If
                    r_address &= reader.Item("city") & ", " & reader.Item("state") & " " & reader.Item("zipcode")
                End If
                remittolbl.Text = r_address
                vphonelbl.Text = reader.Item("c_phone").ToString
                vfaxlbl.Text = reader.Item("c_fax").ToString
            End If
            reader.Close()
            If appcode.isCustomer(Session("companyID")) = True And Session("admin") = False Then
                shiptodd.DataSourceID = "SqlShipTosByUser"
                shiptodd.DataBind()
                If Session("selected_shipID").ToString = "" Then
                    Session("selected_shipID") = shiptodd.SelectedValue
                End If
            Else
                If Session("selected_shipID").ToString = "" Then
                    Session("selected_shipID") = appcode.GetDefaultShipID(Session("selected_companyID"))
                End If
            End If
            commandString = "select * from t_ship where shipID=@shipID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipID", Session("selected_shipID"))
            reader = comm.ExecuteReader
            Dim s_address As String = ""
            If reader.Read Then
                shiptodd.SelectedValue = reader.Item("shipID")
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
            End If
            reader.Close()
            If appcode.isCustomer(Session("companyID")) = True Then
                If Session("admin") = False Then
                    Session("selected_userID") = Session("userID")
                    contactdd.Enabled = False
                End If
            End If
            commandString = "select * from t_user where userID=@userID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@userID", Session("selected_userID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                contactdd.SelectedValue = reader.Item("userID")
                phonelbl.Text = reader.Item("c_phone").ToString
            Else
                phonelbl.Text = appcode.GetUserPhone(appcode.GetDefaultUserID(Session("selected_companyID")))
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
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID"))).ToString("c")
            grandtotallbl.Text = (subtotal + (subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("vendorID")))).ToString("c")
        End If
    End Sub

    Protected Sub shiptodd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shiptodd.SelectedIndexChanged
        Session("selected_shipID") = shiptodd.SelectedValue
        Response.Redirect("Kit.aspx")
    End Sub

    Protected Sub contactdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles contactdd.SelectedIndexChanged
        Session("selected_userID") = contactdd.SelectedValue
        Response.Redirect("Kit.aspx")
    End Sub

End Class
