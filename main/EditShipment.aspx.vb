Imports System.Data.SqlClient

Partial Class EST_EditShipment
    Inherits System.Web.UI.Page

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
                shipmentIDlbl.Text = reader.Item("shipmentID")
                pickdatelbl.Text = reader.Item("pick_date")
                orderIDlbl.Text = reader.Item("orderID").ToString
                potb.Text = reader.Item("purchaseorder").ToString
                orderdatelbl.Text = reader.Item("order_date").ToString
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                Dim b_address As String = reader.Item("company") & "<br/>"
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
                contactlbl.Text = reader.Item("c_contact").ToString
                phonelbl.Text = reader.Item("c_phone").ToString
                notestb.Text = reader.Item("notes").ToString
                carrierdd.SelectedValue = "Local Delivery"
                trackingtb.Text = "NA"
                shipchargetb.Text = "0.00"
                If reader.Item("shipped") = True Then
                    shipbtn.Visible = False
                End If
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub shipbtn_Click(sender As Object, e As EventArgs) Handles shipbtn.Click
        If IsNumeric(shipchargetb.Text) = False Then
            shipchargetb.Text = "0"
        End If
        appcode.UpdateShipment(Session("shipmentID"), carrierdd.SelectedValue, shipchargetb.Text, trackingtb.Text, True, FormatDateTime(Now(), DateFormat.ShortDate))
        Response.Redirect("EditInvoice.aspx")
    End Sub
End Class
