
Partial Class main_TaskList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = appcode.GetEquipment(appcode.GetEquipmentIDFromKitID((Session("serviceprofileID"))))
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            Dim descriptionIDlbl As Label = row.FindControl("descriptionIDlbl")
            Dim worklineIDlbl As Label = row.FindControl("worklineIDlbl")
            appcode.DeleteWorkListItem(worklineIDlbl.Text)
            If selectcb.Checked = True Then
                'add record
                Dim worklineID = appcode.InsertWorkListItem(Session("serviceprofileID"), descriptionIDlbl.Text)
            End If
        Next
        Response.Redirect("ServiceKit.aspx")

    End Sub


    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim descriptionIDlbl As Label = GridView1.Rows(index).FindControl("descriptionIDlbl")
            Response.Redirect("Task.aspx?workdescriptionID=" & descriptionIDlbl.Text)
        ElseIf e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim descriptionIDlbl As Label = GridView1.Rows(index).FindControl("descriptionIDlbl")
            appcode.DeleteWorkListItems(descriptionIDlbl.Text)
            appcode.DeleteWorkDescription(descriptionIDlbl.Text)
            Response.Redirect("TaskList.aspx")
        ElseIf e.CommandName = "EditComponent" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim componentIDlbl As Label = GridView1.Rows(index).FindControl("componentIDlbl")
            Response.Redirect("Component.aspx?componentID=" & componentIDlbl.Text)
        End If
    End Sub

    Protected Sub descriptionbtn_Click(sender As Object, e As EventArgs) Handles descriptionbtn.Click
        Response.Redirect("Task.aspx")
    End Sub
    Protected Sub componentbtn_Click(sender As Object, e As EventArgs) Handles componentbtn.Click
        Response.Redirect("Component.aspx")
    End Sub
End Class
