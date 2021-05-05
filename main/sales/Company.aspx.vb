Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class Company
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim companyID As Integer
            If Session("selected_companyID") <> "0" Then
                companyID = Session("selected_companyID")
            Else
                companyID = Session("companyID")
                purchasinglbl.Text = "Sales"
                payableslbl.Text = "Receivables"
                receivinglbl.Text = "Shipping"
            End If
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_company where companyID=@companyID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@companyID", companyID)
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                If File.Exists(Server.MapPath("~/Images/Banners/" & reader.Item("companyID").ToString & ".jpg")) = True Then
                    Image1.ImageUrl = "~/Images/Banners/" & reader.Item("companyID").ToString & ".jpg"
                Else
                    Image1.ImageUrl = ""
                End If
                If reader.Item("repID").ToString <> "" Then
                    repdd.SelectedValue = reader.Item("repID")
                End If
                companytb.Text = reader.Item("company").ToString
                address1tb.Text = reader.Item("address1").ToString
                address2tb.Text = reader.Item("address2").ToString
                citytb.Text = reader.Item("city").ToString
                statetb.Text = reader.Item("state").ToString
                zipcodetb.Text = reader.Item("zipcode").ToString
                phonetb.Text = reader.Item("c_phone").ToString
                faxtb.Text = reader.Item("c_fax").ToString
                salestaxtb.Text = FormatNumber(reader.Item("salestax") * 100, 1)
                purchasingtb.Text = reader.Item("contact_email")
                payablestb.Text = reader.Item("billing_email")
                receivingtb.Text = reader.Item("warehouse_email")
                chargetaxcb.Checked = reader.Item("chargetax")
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim salestax As Double = salestaxtb.Text / 100
        Dim companyID As Integer
        If Session("selected_companyID") <> "0" Then
            companyID = Session("selected_companyID")
        Else
            companyID = Session("companyID")
        End If
        appcode.UpdateCompany(companyID, companytb.Text, address1tb.Text, address2tb.Text, citytb.Text, statetb.Text, zipcodetb.Text, phonetb.Text, faxtb.Text, "", 70.0, salestax, chargetaxcb.Checked, equipmenttb.Text, purchasingtb.Text, payablestb.Text, receivingtb.Text, repdd.SelectedValue, False
                              )
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub uploadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uploadButton.Click
        If FileUpload1.HasFile Then
            Dim picture As String = Session("selected_companyID").ToString & ".jpg"
            FileUpload1.SaveAs(Server.MapPath("~/Images/Banners/" & picture))
            msglbl.Text = "File " & picture & " uploaded."
        Else
            msglbl.Text = "No File Uploaded."
        End If
        Response.Redirect("Company.aspx")
    End Sub

End Class
