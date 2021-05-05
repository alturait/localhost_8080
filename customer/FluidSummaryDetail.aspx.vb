
Partial Class customer_FluidSummaryDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("fluidID").ToString <> "" Then
                If Session("rmonth") <> "" And Session("ryear") <> "" Then
                    monthdd.SelectedValue = Session("rmonth")
                    yeardd.SelectedValue = Session("ryear")
                Else
                    monthdd.SelectedValue = Month(Now())
                    yeardd.SelectedValue = Year(Now())
                End If
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
