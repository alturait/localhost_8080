Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_NeedsPO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView1.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
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

    Sub SendOrderConfirmation(ByVal orderID As Integer, ByVal userID As Integer)
        Dim applicationpath As String = Request.PhysicalApplicationPath
        Dim subject As String = "PO Request - Please Reply"
        Dim toemail As String = appcode.GetUserEmail(userID)
        Dim message As String = appcode.PORequestHTML(orderID, "", Application("banner_order"), Application("site_address"))
        Dim fromemail As String = Application("orders_email")
        SendEmail(subject, message, fromemail, toemail, "", Application("orders_email"))
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        For Each row In GridView1.Rows
            Dim resendcb As CheckBox = row.FindControl("resendcb")
            Dim needspocb As CheckBox = row.FindControl("needspocb")
            Dim orderIDlbl As Label = row.FindControl("orderIDlbl")
            If resendcb.Checked = True Then
                SendOrderConfirmation(orderIDlbl.Text, contactdd.SelectedValue)
            Else
                If needspocb.Checked = False Then
                    appcode.UpdateNeedsPO(orderIDlbl.Text, needspocb.Checked)
                End If
            End If
        Next
        Response.Redirect("NeedsPO.aspx")
    End Sub
    Protected Sub customerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles customerdd.SelectedIndexChanged
        If customerdd.SelectedValue <> "0" Then
            contactdd.Items.Clear()
            contactdd.DataBind()
            contactdd.ClearSelection()
        End If
    End Sub
    Protected Sub sendbtn_Click(sender As Object, e As EventArgs) Handles sendbtn.Click
        For Each row In GridView1.Rows
            Dim orderIDlbl As Label = row.FindControl("orderIDlbl")
            Dim resendcb As CheckBox = row.FindControl("resendcb")
            If resendcb.Checked = True Then
                SendOrderConfirmation(orderIDlbl.Text, contactdd.SelectedValue)
            End If
        Next
        Response.Redirect("NeedsPO.aspx")

    End Sub
End Class
