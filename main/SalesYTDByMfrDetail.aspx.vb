
Partial Class main_SalesYTDByMfrDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            pagelbl.Text = Request.QueryString("mfr") & " " & Request.QueryString("partnumber") & " " & Year(Now())
        End If
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("SalesYTDByMfr.aspx?mfr=" & Request.QueryString("mfr"))

    End Sub
End Class
