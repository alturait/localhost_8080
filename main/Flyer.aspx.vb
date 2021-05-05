Imports System.Data.SqlClient
Imports System.IO
Imports aspNetEmail

Partial Class main_Flyer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("flyerID").ToString <> "0" Then
                flyerdd.SelectedValue = Session("flyerID")
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_flyer where flyerID=@flyerID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@flyerID", Session("flyerID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    flyerlbl.Text = reader.Item("html")
                    pdflbl.Text = reader.Item("pdf_attachment")
                End If
                reader.Close()
                conn.Close()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub flyerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles flyerdd.SelectedIndexChanged
        Session("flyerID") = flyerdd.SelectedValue
        Response.Redirect("Flyer.aspx")
    End Sub

    Protected Sub newbtn_Click(sender As Object, e As EventArgs) Handles newbtn.Click
        Session("flyerID") = "0"
        Response.Redirect("FlyerTemplate.aspx")
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

    Sub SendEmailWithAttachments(ByVal subject As String, ByVal message As String, ByVal from As String, ByVal recipient As String, ByVal cc As String, ByVal bcc As String, ByVal filename1 As String, ByVal filename2 As String)
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
        msg.AddAttachment(Server.MapPath("~/" & filename1))
        If filename2 <> "" Then
            msg.AddAttachment(Server.MapPath("~/" & filename2))
        End If
        msg.ThrowException = True
        msg.Logging = False
        msg.LogBody = False
        msg.Send()
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs) Handles editbtn.Click
        If flyerdd.SelectedValue <> "0" Then
            Session("flyerID") = flyerdd.SelectedValue
            Response.Redirect("FlyerTemplate.aspx")
        End If
    End Sub

    Protected Sub sendtestbtn_Click(sender As Object, e As EventArgs) Handles sendtestbtn.Click
        If flyerdd.SelectedValue <> "0" Then
            Dim filename1 As String = "PDF_Attachments/" & pdflbl.Text
            Dim subject As String = flyerdd.SelectedItem.Text
            Dim message As String = flyerlbl.Text
            Dim fromemail As String = "kgardner@dfofilters.com"
            If pdflbl.Text <> "None" Then
                SendEmailWithAttachments(subject, message, fromemail, emailtb.Text, "", "", filename1, "")
            Else
                SendEmail(subject, message, fromemail, emailtb.Text, "", "")
            End If
            Session("flyerID") = "0"
            Response.Redirect("Flyer.aspx")
        End If
    End Sub
End Class
