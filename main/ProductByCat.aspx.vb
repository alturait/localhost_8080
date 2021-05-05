
Partial Class main_ProductByCat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                categorydd.Visible = False
                addbtn.Visible = False
            End If
        End If
        If categorydd.SelectedValue <> "0" Then
            pagelbl.Text = categorydd.SelectedItem.Text
        Else
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub categorydd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles categorydd.SelectedIndexChanged

    End Sub
End Class
