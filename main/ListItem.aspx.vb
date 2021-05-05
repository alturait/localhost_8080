Imports System.Data.SqlClient

Partial Class main_ListItem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("listitemID").ToString <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_listitem where listitemID=@listitemID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@listitemID", Session("listitemID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    emailtb.Text = reader.Item("email").ToString
                    nametb.Text = reader.Item("name").ToString
                    companytb.Text = reader.Item("company").ToString
                End If
                reader.Close()
                conn.Close()
            End If
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If Session("listitemID").ToString <> "" Then
            appcode.UpdateList(emailtb.Text, nametb.Text, companytb.Text, Session("listitemID"))
        Else
            Dim listitemID As Integer = appcode.InsertList(emailtb.Text, nametb.Text, companytb.Text, Session("listID"))
        End If
        Response.Redirect("MailingList.aspx")
    End Sub

End Class
