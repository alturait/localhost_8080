Imports System.Data.SqlClient

Partial Class customer_Location
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim companyID As Integer
            companyID = Session("selected_companyID")
            If Session("selected_shipID") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_ship where shipID=@shipID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@shipID", Session("selected_shipID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    companytb.Text = appcode.GetCompany(reader.Item("companyID"))
                    shiptotb.Text = reader.Item("shipto").ToString
                    s_address1tb.Text = reader.Item("s_address1").ToString
                    s_address2tb.Text = reader.Item("s_address2").ToString
                    s_address3tb.Text = reader.Item("s_address3").ToString
                    s_citytb.Text = reader.Item("s_city").ToString
                    s_statetb.Text = reader.Item("s_state").ToString
                    s_zipcodetb.Text = reader.Item("s_zipcode").ToString
                    s_phonetb.Text = reader.Item("s_phone").ToString
                End If
                reader.Close()
                conn.Close()
            Else
                deletebtn.Visible = False
            End If
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim companyID As Integer
        companyID = Session("selected_companyID")
        If Session("selected_shipID") <> "" Then
            appcode.UpdateShipTo(Session("selected_shipID"), shiptotb.Text, s_address1tb.Text, s_address2tb.Text, s_address3tb.Text, s_citytb.Text, s_statetb.Text, s_zipcodetb.Text, s_phonetb.Text)
        Else
            Dim shipID As Integer = appcode.InsertShipTo(companyID, shiptotb.Text, s_address1tb.Text, s_address2tb.Text, s_address3tb.Text, s_citytb.Text, s_statetb.Text, s_zipcodetb.Text, s_phonetb.Text)
            If appcode.isCustomer(companyID) = True And Session("admin") = False Then
                appcode.InsertUserLocation(Session("userID"), shipID)
            End If
        End If
        If Session("selected_supplierID") <> "0" Then
            Response.Redirect("MySuppliers.aspx")
        Else
            Response.Redirect("Locations.aspx")
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteShipTo(Session("selected_shipID"))
        Response.Redirect("Locations.aspx")
    End Sub

End Class
