Imports System.Data.SqlClient

Partial Class EST_PickOrder
    Inherits System.Web.UI.Page

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
                companyIDlbl.Value = reader.Item("companyID")
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = reader.Item("order_date").ToString
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                vendorIDlbl.Value = reader.Item("vendorID")
                vendorlbl.Text = reader.Item("vendor")
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
                    s_address = reader.Item("s_address1") & "<br/>"
                    If reader.Item("r_address2").ToString <> "" Then
                        s_address &= reader.Item("s_address2") & "<br/>"
                    End If
                    s_address &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
                End If
                shiptolbl.Text = s_address
                contactlbl.Text = appcode.GetUserName(reader.Item("userID"))
                phonelbl.Text = reader.Item("c_phone").ToString
                faxlbl.Text = reader.Item("c_fax").ToString
                notestb.Text = reader.Item("notes").ToString
                shipmethoddd.SelectedValue = reader.Item("ship_method").ToString
                shipoptionsdd.SelectedValue = reader.Item("ship_options").ToString
                kitcb.Checked = reader.Item("is_kit")
                shipmethoddd.Enabled = False
                shipoptionsdd.Enabled = False
                kitcb.Enabled = False
            End If
            reader.Close()
            conn.Close()
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim line_count As Integer = 0
        For Each row In GridView1.Rows
            Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
            Dim pickqtytb As TextBox = row.FindControl("pickqtytb")
            If IsNumeric(pickqtytb.Text) = True Then
                If pickqtytb.Text > 0 Then
                    line_count += 1
                End If
            End If
        Next
        Dim shipmentID As Integer = appcode.InsertShipment(Session("orderID"), FormatDateTime(Now(), DateFormat.ShortDate))
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim lineIDlbl As Label = row.FindControl("lineIDlbl")
            Dim pickqtytb As TextBox = row.FindControl("pickqtytb")
            Dim qtytb As Label = row.FindControl("qtytb")
            If IsNumeric(pickqtytb.Text) = False Then
                pickqtytb.Text = 0
            End If
            'If pickqtytb.Text > 0 Then
            appcode.InsertShipmentLine(shipmentID, lineIDlbl.Text, pickqtytb.Text)
            appcode.UpdatePickUserID(shipmentID, Session("userID"))
            'reduce requisition qty by shipment qty
            Dim reqqty As Double = appcode.GetRequisitionQuantity(lineIDlbl.Text)
                appcode.UpdateRequisitionQty(lineIDlbl.Text, reqqty - pickqtytb.Text)
                Dim onhand As Double = appcode.GetOnHand(manufacturerlbl.Text, partnumberlbl.Text)
                appcode.UpdateOnHand(manufacturerlbl.Text, partnumberlbl.Text, onhand - CDbl(pickqtytb.Text))
            'End If
            If shipoptionsdd.SelectedValue = "Ship & Cancel BO" Then
                'reduce order quantity by BO qty
                'reduce requisition qty by BO qty
                If pickqtytb.Text = 0 Then
                    appcode.DeleteOrderLine(lineIDlbl.Text)
                    appcode.DeleteRequisition(lineIDlbl.Text)
                Else
                    appcode.UpdateOrderLineQuantity(lineIDlbl.Text, pickqtytb.Text)
                    appcode.UpdateRequisitionQty(lineIDlbl.Text, pickqtytb.Text)
                End If
            End If
            'if order line complete then delete requisition line
            If appcode.IsOrderLineOpen(lineIDlbl.Text) = False Then
                appcode.DeleteRequisition(lineIDlbl.Text)
            End If
        Next
        Dim ordercomplete As Boolean = appcode.IsOrderComplete(Session("orderID"))
        If ordercomplete = False Then
            'change ship options to ship complete
            appcode.UpdateShipOptions(Session("orderID"), "Ship Complete")
        End If
        appcode.UpdateOrderComplete(Session("orderID"), ordercomplete)
        Session("shipmentID") = shipmentID
        Response.Redirect("EditShipment.aspx")
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Response.Redirect("Order.aspx")
    End Sub
End Class
