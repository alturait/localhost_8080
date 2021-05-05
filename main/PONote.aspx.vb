Imports System.Data.SqlClient

Partial Class PONote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            datelbl.Text = FormatDateTime(Now(), DateFormat.ShortDate)
            authorlbl.Text = appcode.GetUserName(Session("userID"))
            notetb.Focus()
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If notetb.Text <> "" Then
            Dim noteID As Integer = appcode.InsertPONote(Session("poID"), datelbl.Text, authorlbl.Text, notetb.Text)
            Response.Redirect("PurchaseOrder.aspx")
        End If
    End Sub

End Class
