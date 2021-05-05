Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class EST_PO
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private submitted As Boolean = False

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
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
            commandString = "select * from v_po where poID=@poID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@poID", Session("poID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                poIDlbl.Text = reader.Item("poID")
                potb.Text = reader.Item("po").ToString
                podatelbl.Text = reader.Item("date_submitted")
                submittedcb.Checked = reader.Item("submitted")
                submittedcb.Enabled = False
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
            End If
            reader.Close()
            conn.Close()
        End If
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

    Protected Sub emailbtn_Click(sender As Object, e As EventArgs) Handles emailbtn.Click
        If poIDlbl.Text <> "" Then
            Dim subject As String = "PO Request From " & appcode.GetCompany(Session("companyID"))
            Dim message As String = appcode.POHTML(Session("poID"), "", Application("banner_po"), Application("site_address"))
            Dim from As String = appcode.GetUserEmail(Session("userID"))
            Dim recipient As String = email1tb.Text
            Dim cc As String = email2tb.Text
            SendEmail(subject, message, from, recipient, "", "")
            Response.Redirect("PurchaseOrders.aspx")
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeletePO(Session("poID"))
        Response.Redirect("PurchaseOrders.aspx")
    End Sub

End Class
