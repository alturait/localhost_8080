
Partial Class Requirements
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub filtersbtn_Click(sender As Object, e As EventArgs) Handles filtersbtn.Click
        Dim qstring As String = ""
        Dim x As Integer = 0
        For Each row In GridView1.Rows
            Dim companyIDlbl As Label = row.FindControl("companyIDlbl")
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            If selectcb.Checked = True Then
                If x > 0 Then
                    qstring &= "|" & companyIDlbl.Text
                Else
                    qstring &= companyIDlbl.Text
                End If
                x += 1
            End If
        Next
        Session("qstring") = qstring
        Response.Redirect("FilterList.aspx")
    End Sub
End Class
