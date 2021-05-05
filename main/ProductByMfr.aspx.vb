
Partial Class main_ProductByMfr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                categorydd.Visible = False
                addbtn.Visible = False
            End If
        End If
        If manufacturerdd.SelectedValue <> "0" Then
            pagelbl.Text = manufacturerdd.SelectedItem.Text
        Else
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Detail" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim productIDlbl As Label = GridView1.Rows(index).FindControl("productIDlbl")
            Response.Redirect("VCatalogPage.aspx?productID=" & productIDlbl.Text)
        End If
    End Sub

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        GridView1.DataBind()
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        If categorydd.SelectedValue <> "0" Then
            For Each row In GridView1.Rows
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim productIDlbl As Label = row.FindControl("productIDlbl")
                If selectcb.Checked = True Then
                    appcode.UpdateProductCategory(productIDlbl.Text, categorydd.SelectedValue)
                End If
            Next
            Response.Redirect("ProductByMfr.aspx")
        End If
    End Sub
End Class
