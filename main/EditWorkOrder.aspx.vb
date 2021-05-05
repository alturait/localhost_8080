Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_EditWorkOrder
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("repairID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_repair where repairID=@repairID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@repairID", Request.QueryString("repairID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    repairIDlbl.Text = reader.Item("repairID").ToString
                    orderdatetb.Text = reader.Item("date_received").ToString
                    est_deliverytb.Text = reader.Item("date_return_estimate").ToString
                    returnedontb.Text = reader.Item("date_return_actual").ToString
                    customerdd.SelectedValue = reader.Item("customer_companyID")
                    customeruserdd.SelectedValue = reader.Item("customer_userID")
                    manufacturertb.Text = reader.Item("manufacturer").ToString
                    modeltb.Text = reader.Item("model").ToString
                    serialnumbertb.Text = reader.Item("serial_number").ToString
                    partnumbertb.Text = reader.Item("part_number").ToString
                    descriptiontb.Text = reader.Item("description").ToString
                    estimatetb.Text = reader.Item("estimate").ToString
                    vendordd.SelectedValue = reader.Item("supplier_companyID")
                    vendoruserdd.SelectedValue = reader.Item("supplier_userID")
                    sentontb.Text = reader.Item("senton").ToString
                    returnedontb.Text = reader.Item("returnedon").ToString
                    hourstb.Text = reader.Item("hours").ToString
                    labortb.Text = reader.Item("labor").ToString
                    partstb.Text = reader.Item("parts").ToString
                    totaltb.Text = reader.Item("total").ToString
                    requestedtb.Text = reader.Item("work_requested").ToString
                    performedtb.Text = reader.Item("work_performed").ToString
                    approvedcb.Text = reader.Item("work_approved").ToString
                    approvedbytb.Text = reader.Item("work_approvedby").ToString
                    potb.Text = reader.Item("purchase_order").ToString
                    completecb.Checked = reader.Item("complete")
                    orderIDtb.Text = reader.Item("orderID").ToString
                    invoiceamounttb.Text = reader.Item("invoice_amount").ToString
                End If
                reader.Close()
                conn.Close()
            Else
                orderdatetb.Text = FormatDateTime(Now(), DateFormat.ShortDate)
                repairIDlbl.Text = "NEW"
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If IsNumeric(estimatetb.Text) = False Then
            estimatetb.Text = 0
        End If
        If IsNumeric(hourstb.Text) = False Then
            hourstb.Text = 0
        End If
        If IsNumeric(labortb.Text) = False Then
            labortb.Text = 0
        End If
        If IsNumeric(partstb.Text) = False Then
            partstb.Text = 0
        End If
        If IsNumeric(totaltb.Text) = False Then
            totaltb.Text = 0
        End If
        If IsNumeric(invoiceamounttb.Text) = False Then
            invoiceamounttb.Text = 0
        End If

        If Request.QueryString("repairID") <> "" Then
            appcode.UpdateWorkOrder(Request.QueryString("repairID"), Session("userID"), orderdatetb.Text, est_deliverytb.Text, deliveredtb.Text, customerdd.SelectedValue, customeruserdd.SelectedValue, manufacturertb.Text, modeltb.Text, partnumbertb.Text, serialnumbertb.Text, descriptiontb.Text, estimatetb.Text, vendordd.SelectedValue, vendoruserdd.SelectedValue, sentontb.Text, returnedontb.Text, hourstb.Text, labortb.Text, partstb.Text, totaltb.Text, requestedtb.Text, completecb.Checked, performedtb.Text, approvedcb.Checked, approvedbytb.Text, potb.Text, completecb.Text, orderIDtb.Text, invoiceamounttb.Text)
            Response.Redirect("OpenWorkOrders.aspx")
        Else
            'Dim repairID As Integer = appcode.InsertWorkOrder(Session("userID"), orderdatetb.Text, est_deliverytb.Text, deliveredtb.Text, customerdd.SelectedValue, customeruserdd.SelectedValue, manufacturertb.Text, modeltb.Text, partnumbertb.Text, serialnumbertb.Text, descriptiontb.Text, estimatetb.Text, vendordd.SelectedValue, vendoruserdd.SelectedValue, sentontb.Text, returnedontb.Text, hourstb.Text, labortb.Text, partstb.Text, totaltb.Text, requestedtb.Text, completecb.Checked, performedtb.Text, approvedcb.Checked, approvedbytb.Text, potb.Text, False, orderIDtb.Text, invoiceamounttb.Text)
            Dim repairID As Integer = appcode.InsertWorkOrder(Session("userID"), orderdatetb.Text, est_deliverytb.Text, deliveredtb.Text, customerdd.SelectedValue, customeruserdd.SelectedValue, manufacturertb.Text, modeltb.Text, partnumbertb.Text, serialnumbertb.Text, descriptiontb.Text, estimatetb.Text, vendordd.SelectedValue, vendoruserdd.SelectedValue, sentontb.Text, returnedontb.Text, hourstb.Text, labortb.Text, partstb.Text, totaltb.Text, requestedtb.Text, completecb.Checked, performedtb.Text, approvedcb.Checked, approvedbytb.Text, potb.Text, False, orderIDtb.Text, invoiceamounttb.Text)
            Response.Redirect("OpenWorkOrders.aspx")
        End If
    End Sub
    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        appcode.DeleteWorkOrder(Request.QueryString("repairID"))
        Response.Redirect("OpenWorkOrders.aspx")
    End Sub
End Class
