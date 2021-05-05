Imports System.Data.SqlClient

Partial Class main_Xref
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("productID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_product where productID=@productID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@productID", Request.QueryString("productID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    mfrlbl.Text = reader.Item("manufacturer")
                    partnumberlbl.Text = reader.Item("partnumber")
                End If
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        If manufacturerdd.SelectedValue <> "0" And partnumberdd.SelectedValue <> "0" Then
            Dim xref1 = appcode.InsertXref(mfrlbl.Text, partnumberlbl.Text, manufacturerdd.SelectedValue, partnumberdd.SelectedValue)
            Dim xref2 = appcode.InsertXref(manufacturerdd.SelectedValue, partnumberdd.SelectedValue, mfrlbl.Text, partnumberlbl.Text)
        End If
        Response.Redirect("VCatalogPage.aspx?productID=" & Request.QueryString("productID"))
    End Sub
End Class
