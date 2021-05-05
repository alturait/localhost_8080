Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class Contacts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim userIDlbl As Label = GridView1.Rows(index).FindControl("userIDlbl")
            Session("selected_userID") = userIDlbl.Text
            Response.Redirect("Contact.aspx")
        ElseIf e.CommandName = "Send" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim userIDlbl As Label = GridView1.Rows(index).FindControl("userIDlbl")
            Dim message As String = ""
            Dim pword As String = appcode.GetUserPassword(userIDlbl.Text)
            Dim recipient As String = appcode.GetUserEmail(userIDlbl.Text)
            Dim sentby As String = appcode.GetUserEmail(Session("userID"))
            Dim subject As String = "Desert Fleet Outfitters"
            message &= "<p>Welcome to Desert Fleet Outftters!</p>"
            message &= "<p>Web Site: <a href='http://www.desertfleetoutfitters.com'>www.desertfleetoutfitters.com</a>"
            message &= "<p>User Email: " & recipient & "<br/>"
            message &= "Password: " & pword & "</p>"
            message &= "<p>If you have questions or feedback, please contact Ken Gardner at 480-295-1676.</p>"
            SendEmail(sentby, message, recipient, subject, "", "kgardner@desertfleetoutfitters.com")
            Response.Redirect("Contacts.aspx")
        End If
    End Sub

    Sub SendEmail(ByVal from As String, ByVal message As String, ByVal recipient As String, ByVal subject As String, ByVal CC As String, ByVal bcc As String)
        Dim msg As New EmailMessage()
        msg.Server = "localhost"
        msg.FromAddress = from
        msg.ValidateAddress = False
        msg.AddTo(recipient)
        msg.Subject = subject
        If CC <> "" Then
            msg.AddCc(CC)
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

    Protected Sub newcontactbtn_Click(sender As Object, e As EventArgs) Handles newcontactbtn.Click
        Session("selected_userID") = "0"
        Response.Redirect("Contact.aspx")
    End Sub
End Class
