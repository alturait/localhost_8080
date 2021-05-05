Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class mobile_ConfirmOrder
    Inherits System.Web.UI.Page

    Protected Sub orderbtn_Click(sender As Object, e As EventArgs) Handles orderbtn.Click
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
        'GET VENDOR INFO
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
        'GET BILLTO INFO
        commandString = "select * from t_company where companyID=@companyID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", Session("companyID"))
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
        'GET SHIP TO ADDRESS
        commandString = "select * from t_ship where shipID=@shipID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shiptodd.SelectedValue)
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
        'GET USER INFO
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
        'GET REP INFO
        Dim repID As Integer = appcode.GetRepID(Session("companyID"))

        Dim orderID As Integer = appcode.InsertOrder(potb.Text, Session("companyID"), b_company, Session("vendorID"), v_company, v_phone, v_fax, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, appcode.GetUserName(Session("userID")), v_company, v_address1, v_address2, v_city, v_state, v_zipcode, b_address1, b_address2, b_city, b_state, b_zipcode, shiptodd.SelectedValue, shipto, s_address1, s_address2, s_address3, s_city, s_state, s_zipcode, Session("userID"), u_name, u_phone, u_fax, "", "Best Way", "Ship Complete", True, 0, Session("mobile_serviceprofileID"), "", False, repID)
        If IsNumeric(hourstb.Text) = False Then
            hourstb.Text = "0"
        End If
        appcode.UpdateOrderKitHours(hourstb.Text, orderID)
        appcode.UpdateMobileUserID(Session("mobile_userID"), orderID)

        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            If selectcb.Checked = True Then
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim itemlbl As Label = row.FindControl("itemlbl")
                Dim qtylbl As Label = row.FindControl("qtylbl")
                Dim price As Double = appcode.GetCompanyPrice(Session("mobile_companyID"), manufacturerlbl.Text, partnumberlbl.Text)
                Dim uom As String = appcode.GetUOM(manufacturerlbl.Text, partnumberlbl.Text)
                Dim kitID As Integer = Session("mobile_serviceprofileID")
                Dim lineID As Integer = appcode.InsertOrderLine(orderID, 0, assetIDlbl.Text, manufacturerlbl.Text, partnumberlbl.Text, itemlbl.Text, qtylbl.Text, price, uom, "", True, kitID)
            End If
        Next
        conn.Close()
        Dim kc As Integer = appcode.InsertOrderLine(orderID, 0, assetIDlbl.Text, "DFO", "KC1", "Kit Charge", 1, Application("kitcharge"), "EACH", "", False, Session("mobile_serviceprofileID"))
        SendOrderRequest(orderID)
        'appcode.UpdateMobileOrders(Session("mobile_userID"))
        Response.Redirect("Order.aspx")

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            equipmentlbl.Text = appcode.GetEquipment(Session("mobile_equipmentID"))
            kitlbl.Text = appcode.GetKitName(Session("mobile_kitID"))
            intervallbl.Text = Session("mobile_interval")
            assetIDlbl.Text = Session("mobile_assetID")
            hourstb.Text = ""
            potb.Text = Session("mobile_assetID")
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

    Sub SendOrderRequest(ByVal orderID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "Order Acknowledgement - " & appcode.GetCompany(Session("mobile_companyID")) & " - Placed By " & Session("mobile_user")
        Dim message As String = appcode.OrderConfirmationHTML(orderID, "", "http://www.dfofilters.com/Images/dfo_email_banner.jpg", Application("site_address"))
        Dim sender As String = Application("orders_email")
        Dim recipient As String = Session("mobile_user_email")
        Dim cc As String = ""
        Dim bcc As String = appcode.GetUserEmail(appcode.GetRepID(Session("mobile_companyID")))
        If recipient <> "" Then
            SendEmail(subject, message, sender, recipient, cc, bcc)
        End If
    End Sub

    Protected Sub BACK_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Order.aspx")
    End Sub
End Class
