Imports System.Data.SqlClient

Partial Class main_Component
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("componentID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_component where componentID=@componentID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@componentID", Request.QueryString("componentID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    componenttb.Text = reader.Item("component")

                End If
                reader.Close()
                conn.Close()
            Else

            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If Request.QueryString("componentID") <> "" Then
            If componenttb.Text <> "" Then
                appcode.UpdateComponent(Request.QueryString("componentID"), componenttb.Text)
            End If
        Else
            If componenttb.Text <> "" Then
                Dim componentID = appcode.InsertComponent(componenttb.Text)
            End If
        End If
        Response.Redirect("TaskList.aspx")
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click

    End Sub
End Class
