
Partial Class customer_CustomerReports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            numunitslbl.Text = appcode.GetAssetCount(Session("selected_companyID"))
            sortbydd.SelectedValue = "1"
            Panel1.Visible = True
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub sortbydd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sortbydd.SelectedIndexChanged
        If sortbydd.SelectedValue = "1" Then
            Panel1.Visible = True
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
        ElseIf sortbydd.SelectedValue = "2" Then
            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False
            Panel4.Visible = False
        ElseIf sortbydd.SelectedValue = "3" Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = True
            Panel4.Visible = False
        ElseIf sortbydd.SelectedValue = "4" Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = True
        End If
    End Sub

End Class
