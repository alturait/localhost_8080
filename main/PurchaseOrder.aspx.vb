Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class EST_PurchaseOrder
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private submitted As Boolean = False

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
            commandString = "select * from v_po where poID=@poID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@poID", Session("poID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                poIDlbl.Text = reader.Item("poID")
                potb.Text = reader.Item("po").ToString
                podatelbl.Text = reader.Item("date_submitted")
                estimated_arrivaltb.Text = reader.Item("estimated_arrival").ToString
                submittedcb.Checked = reader.Item("submitted")
                suppliertb.Text = reader.Item("supplier").ToString
                sup_contacttb.Text = reader.Item("sup_contact").ToString
                sup_phonetb.Text = reader.Item("sup_phone").ToString
                sup_faxtb.Text = reader.Item("sup_fax").ToString
                sup_address1tb.Text = reader.Item("sup_address1").ToString
                sup_address2tb.Text = reader.Item("sup_address2").ToString
                sup_citytb.Text = reader.Item("sup_city").ToString
                sup_statetb.Text = reader.Item("sup_state").ToString
                sup_zipcodetb.Text = reader.Item("sup_zipcode").ToString
                email1tb.Text = reader.Item("email_1").ToString
                email2tb.Text = reader.Item("email_2").ToString
                shiptotb.Text = reader.Item("shipto").ToString
                usertb.Text = reader.Item("ship_contact").ToString
                vphonetb.Text = reader.Item("ship_phone").ToString
                vfaxtb.Text = reader.Item("ship_fax").ToString
                ship_address1tb.Text = reader.Item("ship_address1").ToString
                ship_address2tb.Text = reader.Item("ship_address2").ToString
                ship_citytb.Text = reader.Item("ship_city").ToString
                ship_statetb.Text = reader.Item("ship_state").ToString
                ship_zipcodetb.Text = reader.Item("ship_zipcode").ToString
                FillShipTo(7307)
                FillShipContact(Session("userID"))
            End If
            reader.Close()
            conn.Close()
            If submittedcb.Checked = True Then
                emailbtn.Visible = False
                Panel1.Visible = False
            End If
        End If
        partnumbertb.Focus()
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim extendedlbl As Label = e.Row.FindControl("extendedlbl")
            subtotal += extendedlbl.Text
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
        End If
    End Sub

    Protected Sub email_po()
        Dim subject As String = "PO Request From " & appcode.GetCompany(Session("companyID"))
        Dim message As String = appcode.POHTML(Session("poID"), emailmessagetb.Text, Application("banner_po"), Application("site_address"))
        Dim fromemail As String = Application("purchasing_email")
        Dim recipient As String = email1tb.Text
        Dim cc As String = email2tb.Text
        Dim bcc As String = Application("purchasing_email")
        SendEmail(subject, message, fromemail, recipient, cc, bcc)
    End Sub

    Protected Sub emailbtn_Click(sender As Object, e As EventArgs) Handles emailbtn.Click
        If poIDlbl.Text <> "" Then
            savepo()
            If submittedcb.Checked = False Then
                email_po()
            End If
            appcode.UpdatePOSubmitted(Session("poID"), True)
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeletePO(Session("poID"))
        Response.Redirect("PurchaseOrders.aspx")
    End Sub

    Sub FillSupplier(ByVal companyID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            suppliertb.Text = reader.Item("company").ToString
            sup_address1tb.Text = reader.Item("address1").ToString
            sup_address2tb.Text = reader.Item("address2").ToString
            sup_citytb.Text = reader.Item("city").ToString
            sup_statetb.Text = reader.Item("state").ToString
            sup_zipcodetb.Text = reader.Item("zipcode").ToString
        End If
        reader.Close()
        conn.Close()
    End Sub

    Sub FillSupplierContact(ByVal companyID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            sup_contacttb.Text = reader.Item("name").ToString
            sup_phonetb.Text = reader.Item("c_phone").ToString
            sup_faxtb.Text = reader.Item("c_fax").ToString
            email1tb.Text = reader.Item("email").ToString
        End If
        reader.Close()
        conn.Close()
    End Sub

    Sub FillShipContact(ByVal userID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            usertb.Text = reader.Item("name").ToString
            vphonetb.Text = reader.Item("c_phone").ToString
            vfaxtb.Text = reader.Item("c_fax").ToString
            email2tb.Text = reader.Item("email").ToString
        End If
        reader.Close()
        conn.Close()
    End Sub

    Sub FillShipTo(ByVal companyID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            shiptotb.Text = reader.Item("shipto").ToString
            ship_address1tb.Text = reader.Item("s_address1").ToString
            ship_address2tb.Text = reader.Item("s_address2").ToString
            ship_citytb.Text = reader.Item("s_city").ToString
            ship_statetb.Text = reader.Item("s_state").ToString
            ship_zipcodetb.Text = reader.Item("s_zipcode").ToString
        End If
        reader.Close()
        conn.Close()
    End Sub

    Protected Sub supplierdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles supplierdd.SelectedIndexChanged
        FillSupplier(supplierdd.SelectedValue)
        FillSupplierContact(supplierdd.SelectedValue)
        sup_contactdd.Items.Clear()
        sup_contactdd.DataBind()
        sup_contactdd.Items.Insert(0, "Select Contact")
    End Sub

    Protected Sub sup_contactdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sup_contactdd.SelectedIndexChanged
        If sup_contactdd.SelectedIndex <> 0 Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_user where userID=@userID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@userID", sup_contactdd.SelectedValue)
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                sup_contacttb.Text = reader.Item("name").ToString
                sup_phonetb.Text = reader.Item("c_phone").ToString
                sup_faxtb.Text = reader.Item("c_fax").ToString
                email1tb.Text = reader.Item("email").ToString
            End If
            reader.Close()
            conn.Close()
        Else
            sup_contacttb.Text = ""
            sup_phonetb.Text = ""
            sup_faxtb.Text = ""
            email1tb.Text = ""
        End If
    End Sub

    Protected Sub savepo()
        appcode.UpdatePO(Session("poID"), potb.Text, podatelbl.Text, estimated_arrivaltb.Text, email1tb.Text, email2tb.Text, suppliertb.Text, sup_address1tb.Text, sup_address2tb.Text, sup_citytb.Text, sup_statetb.Text, sup_zipcodetb.Text, sup_contacttb.Text, sup_phonetb.Text, sup_faxtb.Text, shiptotb.Text, ship_address1tb.Text, ship_address2tb.Text, ship_citytb.Text, ship_statetb.Text, ship_zipcodetb.Text, usertb.Text, vphonetb.Text, vfaxtb.Text, "", submittedcb.Checked)
        'update estimated delivery on all open order line items

        Dim POLineComplete As Boolean = True
        For Each row In GridView1.Rows
            Dim polineIDlbl As Label = row.FindControl("polineIDlbl")
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As LinkButton = row.FindControl("partnumberlbl")
            Dim qtytb As TextBox = row.FindControl("qtytb")
            Dim receivetb As TextBox = row.FindControl("receivetb")
            Dim costlbl As TextBox = row.FindControl("costlbl")
            appcode.UpdateProductCost(manufacturerlbl.Text, partnumberlbl.Text, costlbl.Text)
            appcode.UpdatePOLineCost(polineIDlbl.Text, costlbl.Text)
            If submittedcb.Checked = True Then
                If IsNumeric(receivetb.Text) = True Then
                    If CDbl(receivetb.Text) > 0 Then
                        appcode.InsertReceipt(Session("poID"), manufacturerlbl.Text, partnumberlbl.Text, receivetb.Text)
                        'add to onhand
                        Dim onhand As Double = appcode.GetOnHand(manufacturerlbl.Text, partnumberlbl.Text)
                        appcode.UpdateOnHand(manufacturerlbl.Text, partnumberlbl.Text, onhand + CDbl(receivetb.Text))
                    End If
                End If
                If appcode.IsNotPOLineComplete(Session("poID"), manufacturerlbl.Text, partnumberlbl.Text) = True Then
                    POLineComplete = False
                End If
            Else
                appcode.UpdatePOLineQty(polineIDlbl.Text, qtytb.Text)
                POLineComplete = False
            End If
        Next
        If POLineComplete = True Then
            appcode.UpdatePOComplete(Session("poID"), True)
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If potb.Text <> "" And estimated_arrivaltb.Text <> "" Then
            savepo()
            Response.Redirect("PurchaseOrders.aspx")
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As LinkButton = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Response.Redirect("VCatalogPage.aspx?productID=" & productID)
        ElseIf e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim polineIDlbl As Label = GridView1.Rows(index).FindControl("polineIDlbl")
            appcode.DeletePOLine(polineIDlbl.Text)
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        For Each row In GridView1.Rows
            Dim polineIDlbl As Label = row.FindControl("polineIDlbl")
            Dim qtytb As TextBox = row.FindControl("qtytb")
            Dim costlbl As TextBox = row.FindControl("costlbl")
            appcode.UpdatePOLineCost(polineIDlbl.Text, costlbl.Text)
            appcode.UpdatePOLineQty(polineIDlbl.Text, qtytb.Text)
        Next
        Response.Redirect("PurchaseOrder.aspx")
    End Sub

    Protected Sub submittedcb_CheckedChanged(sender As Object, e As EventArgs) Handles submittedcb.CheckedChanged
        If submittedcb.Checked = True Then
            appcode.UpdatePOSubmitted(Session("poID"), True)
        Else
            appcode.UpdatePOSubmitted(Session("poID"), False)
        End If
        Response.Redirect("PurchaseOrder.aspx")
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Session("noteID") = ""
        Response.Redirect("PONote.aspx")
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim noteIDlbl As Label = GridView2.Rows(index).FindControl("noteIDlbl")
            appcode.DeletePONote(noteIDlbl.Text)
            Response.Redirect("PurchaseOrder.aspx")
        End If

    End Sub

    Protected Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        savepo()
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim filename As String = "PDF/ReceivingTicket_" & Session("poID") & ".pdf"
        appcode.ReceivingTicketPDF(Session("poID"), applicationpath, Application("banner_receipt"), filename)
        Response.Redirect("../" & filename)
    End Sub

    Protected Sub removebtn_Click(sender As Object, e As EventArgs) Handles removebtn.Click
        For Each row In GridView1.Rows
            Dim polineIDlbl As Label = row.FindControl("polineIDlbl")
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            If selectcb.Checked = True Then
                appcode.DeletePOLine(polineIDlbl.Text)
            End If
        Next
        Response.Redirect("PurchaseOrder.aspx")
    End Sub
    Protected Sub addpnbtn_Click(sender As Object, e As EventArgs) Handles addpnbtn.Click
        Dim poID As Integer = Session("poID")
        Dim manufacturer As String
        Dim partnumber As String = partnumbertb.Text
        Dim quantity As Integer = 1
        Dim cost As Double
        Dim uom As String
        Dim polineID As Integer
        If appcode.ispn(partnumber) = True Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_product where partnumber=@partnumber"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@partnumber", partnumber)
            Dim reader As SqlDataReader = comm.ExecuteReader
            While reader.Read
                polineID = appcode.InsertPOLine(poID, reader.Item("manufacturer"), reader.Item("partnumber"), quantity, reader.Item("cost"), reader.Item("uom"), False, 0)
                Session("selected_companyID") = "0"
                Dim coreID As Integer = appcode.GetCoreID(reader.Item("productID"))
                If coreID <> 0 Then
                    AddCoreCharge(poID, reader.Item("productID"), quantity)
                End If
            End While
            reader.Close()
            conn.Close()
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

    Sub AddCoreCharge(ByVal poID As Integer, ByVal productID As Integer, ByVal quantity As Double)
        Dim coreID As Integer = appcode.GetCoreID(productID)
        If coreID <> 0 Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_product where productID=@productID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@productID", coreID)
            reader = comm.ExecuteReader
            If reader.Read Then
                Dim polineID As Integer = appcode.InsertPOLine(poID, reader.Item("manufacturer"), reader.Item("partnumber"), quantity, reader.Item("cost"), reader.Item("uom"), False, 0)
            End If
            reader.Close()
            conn.Close()
        End If

    End Sub
End Class
