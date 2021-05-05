Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class main_OpenKits
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("selected_companyID") <> "0" Then
                GridView2.DataSourceID = "SqlOpenKitsByCustomer"
                GridView2.DataBind()
            End If
            pagelbl.Text = Page.Title
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

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Order" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim orderIDlbl As Label = GridView2.Rows(index).FindControl("orderIDlbl")
            Session("orderID") = orderIDlbl.Text
            Response.Redirect("Order.aspx")
        ElseIf e.CommandName = "Kit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView2.Rows(index).FindControl("equipmentIDlbl")
            Dim serviceprofileIDlbl As Label = GridView2.Rows(index).FindControl("serviceprofileIDlbl")
            Dim companyIDlbl As Label = GridView2.Rows(index).FindControl("companyIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Session("serviceprofileID") = serviceprofileIDlbl.Text
            Session("selected_companyID") = companyIDlbl.Text
            Response.Redirect("ServiceKit.aspx")
        End If
    End Sub

    Protected Sub sendbtn_Click(sender As Object, e As EventArgs) Handles sendbtn.Click
        If Session("selected_companyID") <> "0" Then
            Dim equipment_email As String = appcode.GetEqupmentEmail(Session("selected_companyID"))
            Dim recipient As String = "ken@dfofilters.com"
            If equipment_email <> "" Then
                recipient = equipment_email
            End If
            Dim message As String = appcode.KitStatusReportHTML(Session("selected_companyID"), Application("banner"), Application("site_address"))
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim subject As String = "KIT STATUS REPORT"
            Dim fromemail As String = Application("orders_email")
            SendEmail(subject, message, fromemail, recipient, "ken@dfofilters.com", "")
            Response.Redirect("OpenKits.aspx")
        Else
            Dim lastcompany As String = ""
            For Each row In GridView2.Rows
                Dim companyIDlbl As Label = row.findControl("companyIDlbl")
                Dim companylbl As Label = row.findControl("companylbl")
                If companylbl.Text <> lastcompany Then
                    If appcode.isSendKitStatus(companyIDlbl.Text) Then
                        'send the report
                        Dim equipment_email As String = appcode.GetEqupmentEmail(companyIDlbl.Text)
                        Dim recipient As String = "ken@dfofilters.com"
                        If equipment_email <> "" Then
                            recipient = equipment_email
                        End If
                        Dim message As String = appcode.KitStatusReportHTML(companyIDlbl.Text, Application("banner"), Application("site_address"))
                        Dim applicationpath As String = Request.PhysicalApplicationPath
                        Dim subject As String = "KIT STATUS REPORT"
                        Dim fromemail As String = Application("orders_email")
                        SendEmail(subject, message, fromemail, recipient, "ken@dfofilters.com", "")
                    End If
                End If
                lastcompany = companylbl.Text
            Next
        End If

    End Sub
End Class
