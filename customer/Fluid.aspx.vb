Imports System.Data.SqlClient

Partial Class customer_Fluid
    Inherits System.Web.UI.Page

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If fluidtb.Text <> "" Then
            If Session("selected_fluidID") <> "" Then
                appcode.UpdateCompanyFluid(Session("selected_fluidID"), fluidtb.Text)
            Else
                appcode.InsertCompanyFluid(Session("selected_companyID"), fluidtb.Text)
            End If
            Response.Redirect("FluidList.aspx")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("selected_fluidID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                Dim comm As New SqlCommand
                Dim reader As SqlDataReader
                commandString = "select * from t_fluid where fluidID=@fluidID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@fluidID", Session("selected_fluidID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    fluidtb.Text = reader.Item("fluid")
                End If
                reader.Close()
                conn.Close()
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub
End Class
