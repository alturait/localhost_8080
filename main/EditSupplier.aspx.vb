Imports System.Data.SqlClient

Partial Class EST_EditSupplier
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_company where companyID=@companyID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", Session("selected_supplierID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                companytb.Text = reader.Item("company").ToString
                address1tb.Text = reader.Item("address1").ToString
                address2tb.Text = reader.Item("address2").ToString
                citytb.Text = reader.Item("city").ToString
                statetb.Text = reader.Item("state").ToString
                zipcodetb.Text = reader.Item("zipcode").ToString
                phonetb.Text = reader.Item("c_phone").ToString
                faxtb.Text = reader.Item("c_fax").ToString
                contact_emailtb.Text = reader.Item("contact_email").ToString
                billing_emailtb.Text = reader.Item("billing_email").ToString
                warehouse_emailtb.Text = reader.Item("warehouse_email").ToString
                accountinglbl.Text = "Receivables"
                warehouselbl.Text = "Shipping"
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim vendor, customer, supplier As Boolean
        accountinglbl.Text = "Payables"
        customer = True
        vendor = False
        supplier = False
        'appcode.UpdateCompany(Session("selected_supplierID"), companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, phonetb.Text, faxtb.Text, "", 0, 0, False, contact_emailtb.Text, billing_emailtb.Text, warehouse_emailtb.Text, 3266, True)
        appcode.UpdateCompany(Session("selected_supplierID"), companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, phonetb.Text, faxtb.Text, "", 0, 0, False, equipment_emailtb.Text, contact_emailtb.Text, billing_emailtb.Text, warehouse_emailtb.Text, 3266, True)
        Response.Redirect("MySuppliers.aspx")
    End Sub

    Protected Sub contactsbtn_Click(sender As Object, e As EventArgs) Handles contactsbtn.Click
        Response.Redirect("SupplierContacts.aspx")
    End Sub

End Class
