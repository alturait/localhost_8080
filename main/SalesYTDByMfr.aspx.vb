
Partial Class main_SalesByMfrDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            yearlbl.Text = Year(Now())
            pagelbl.Text = Request.QueryString("mfr")
        End If
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Default.aspx")

    End Sub

End Class
