Imports System.Data.SqlClient

Partial Class Contact
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim companyID As Integer
            companyID = Session("selected_companyID")
            companytb.Text = appcode.GetCompany(companyID)
            If Session("selected_userID").ToString <> "0" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_user where userID=@userID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@userID", Session("selected_userID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    c_nametb.Text = reader.Item("name").ToString
                    c_titletb.Text = reader.Item("title").ToString
                    c_phonetb.Text = reader.Item("c_phone").ToString
                    c_faxtb.Text = reader.Item("c_fax").ToString
                    c_usernametb.Text = reader.Item("username").ToString
                    c_emailtb.Text = reader.Item("email").ToString
                    c_passwordtb.Text = reader.Item("password").ToString
                    admincb.Checked = reader.Item("administrator")
                End If
                reader.Close()
                conn.Close()
                If appcode.IsLastUser(companyID) = True Then
                    deletebtn.Visible = False
                End If
            Else
                companytb.Text = appcode.GetCompany(companyID)
                deletebtn.Visible = False
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim companyID As Integer
        companyID = Session("selected_companyID")
        If appcode.IsUsernameAvailable(Trim(c_usernametb.Text), Session("selected_userID")) = True Then
            If Session("selected_userID").ToString <> "0" Then
                appcode.UpdateUser(Session("selected_userID"), c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, admincb.Checked, c_usernametb.Text)
            Else
                Session("selected_userID") = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, admincb.Checked, c_usernametb.Text)
                Response.Redirect("Contact.aspx")
            End If
            If Session("selected_supplierID") <> "0" Then
                Response.Redirect("MySuppliers.aspx")
            Else
                Response.Redirect("Contacts.aspx")
            End If
        Else
            emaillbl.Visible = True
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteUser(Session("selected_userID"))
        Response.Redirect("Contacts.aspx")
    End Sub

End Class
