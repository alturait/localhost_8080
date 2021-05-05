Imports System.Data.SqlClient
Imports aspNetEmail

Partial Class main_MailingList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("listID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_lists where listID=@listID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@listID", Session("listID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    pagelbl.Text = reader.Item("listname") & " " & Page.Title
                    nametb.Text = reader.Item("listname")
                End If
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim listitemIDlbl As Label = GridView1.Rows(index).FindControl("listitemIDlbl")
            Session("listitemID") = listitemIDlbl.Text
            Response.Redirect("ListItem.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim listitemIDlbl As Label = GridView1.Rows(index).FindControl("listitemIDlbl")            
            appcode.DeleteListItem(listitemIDlbl.Text)
            Response.Redirect("MailingList.aspx")
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each row In GridView1.Rows
            Dim selectedcb As CheckBox = row.FindControl("selectedcb")
            selectedcb.Checked = Not selectedcb.Checked
            If selectedcb.Checked = True Then
                Button1.Text = "Un-Select All"
            Else
                Button1.Text = "Select All"
            End If
        Next
    End Sub

    Function GetHTML(ByVal flyerID As Integer) As String
        GetHTML = 0
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_flyer where flyerID=@flyerID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@flyerID", flyerID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetHTML = reader.Item("html")
        End If
        reader.Close()
        conn.Close()
    End Function

    Protected Sub sendbtn_Click(sender As Object, e As EventArgs) Handles sendbtn.Click
        If flyerdd.SelectedValue <> "0" Then
            Dim html As String = GetHTML(flyerdd.SelectedValue)
            For Each row In GridView1.Rows
                Dim selectedcb As CheckBox = row.FindControl("selectedcb")
                Dim emaillbl As Label = row.FindControl("emaillbl")
                Dim pdf_attachment As String = appcode.GetPDFAttachment(flyerdd.SelectedValue)
                Dim filename1 As String = "PDF_Attachments/" & pdf_attachment
                If selectedcb.Checked = True Then
                    If pdf_attachment <> "None" Then
                        SendEmailWithAttachments(flyerdd.SelectedItem.Text, html, "dfofilters@mc.internetmailserver.net", emaillbl.Text, "", "", filename1, "")
                    Else
                        SendEmail(flyerdd.SelectedItem.Text, html, "dfofilters@mc.internetmailserver.net", emaillbl.Text, "", "")
                    End If
                End If
            Next
            Session("flyerID") = "0"
            Response.Redirect("MailingList.aspx")
        End If
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

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        Session("listitemID") = ""
        Response.Redirect("ListItem.aspx")
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        appcode.UpdateMailingList(Session("listID"), nametb.Text)
        Response.Redirect("MailingLists.aspx")
    End Sub
End Class
