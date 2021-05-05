Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class customer_Order
    Inherits System.Web.UI.Page

    Private subtotal As Decimal = 0
    Private tax_subtotal As Decimal = 0
    Private kitID As Array
    Private serviceprofileID As String

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
        Dim subject As String = "Purchase Order for Order ID " & orderID & "is" & potb.Text
        Dim message As String = "Received from " & appcode.GetUserName(Session("userID"))
        Dim fromemail As String = appcode.GetUserEmail(Session("userID"))
        SendEmail(subject, message, fromemail, "sales@dfofilters.com", "", "")
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
                If reader.Item("complete") = True Then
                    Response.Redirect("CompleteOrder.aspx")
                End If
                If reader.Item("needspo") = False Then
                    potb.Enabled = False
                    updatebtn.Visible = False
                End If
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = FormatDateTime(reader.Item("order_date").ToString, DateFormat.ShortDate)
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                companyIDlbl.Value = reader.Item("companyID")
                vendorIDlbl.Value = reader.Item("vendorID")
                If reader.Item("serviceprofileID") > 0 Then
                    assetIDlbl.Text = appcode.getAssetID(reader.Item("serviceprofileID"))
                    equipmentlbl.Text = appcode.GetEquipment(appcode.GetEquipmentIDFromKitID(reader.Item("serviceprofileID")))
                    kitlbl.Text = appcode.GetKitName(reader.Item("serviceprofileID"))
                    hourslbl.Text = reader.Item("hours_miles")
                    Panel1.Visible = True
                End If
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
                notestb.Text = reader.Item("notes").ToString
                shipmethoddd.SelectedValue = reader.Item("ship_method").ToString
                shipotionsdd.SelectedValue = reader.Item("ship_options").ToString
                shipmethoddd.Enabled = False
                If reader.Item("complete") = True Then
                    shipotionsdd.Enabled = False
                End If
                serviceprofileID = reader.Item("serviceprofileID")
                serviceIDlbl.Value = reader.Item("serviceID")
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
            salestaxlbl.Text = (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value)).ToString("c")
            grandtotallbl.Text = (subtotal + (tax_subtotal * appcode.GetSalesTax(companyIDlbl.Value, vendorIDlbl.Value))).ToString("c")
        End If
    End Sub

    Protected Sub shipotionsdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shipotionsdd.SelectedIndexChanged
        appcode.UpdateShipOptions(Session("orderID"), shipotionsdd.SelectedValue)
        Response.Redirect("Order.aspx")
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteOrder(Session("orderID"))
        Response.Redirect("OpenOrders.aspx")
    End Sub
    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        appcode.UpdateOrderPO(Session("orderID"), potb.Text)
        appcode.UpdateNeedsPO(Session("orderID"), False)
        SendOrderConfirmation(Session("orderID"))
        Response.Redirect("NeedsPO.aspx")
    End Sub
End Class
