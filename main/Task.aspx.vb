Imports System.Data.SqlClient

Partial Class Task
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("workdescriptionID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_workdescription where descriptionID=@descriptionID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@descriptionID", Request.QueryString("workdescriptionID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    componentdd.SelectedValue = reader.Item("componentID")
                    descriptiontb.Text = reader.Item("description")
                    costtb.Text = reader.Item("cost")
                    pricetb.Text = reader.Item("price")
                End If
                reader.close()
                conn.close()
            Else

            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Request.QueryString("workdescriptionID") <> "" Then
            If descriptiontb.Text <> "" Then
                appcode.UpdateWorkDescription(Request.QueryString("workdescriptionID"), descriptiontb.Text, costtb.Text, pricetb.Text, componentdd.SelectedValue)
            End If
        Else
            If descriptiontb.Text <> "" Then
                Dim workdescriptionID = appcode.InsertWorkDescription(descriptiontb.Text, costtb.Text, pricetb.Text, componentdd.SelectedValue)
            End If
        End If
        Response.Redirect("TaskList.aspx")
    End Sub

End Class
