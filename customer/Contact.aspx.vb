Imports System.Data.SqlClient

Partial Class customer_Contact
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
                    companytb.Text = appcode.GetCompany(reader.Item("companyID"))
                    c_nametb.Text = reader.Item("name").ToString
                    c_admincb.Checked = reader.Item("administrator")
                    If Session("admin") = False Then
                        c_admincb.Enabled = False
                    End If
                    c_titletb.Text = reader.Item("title").ToString
                    c_phonetb.Text = reader.Item("c_phone").ToString
                    c_faxtb.Text = reader.Item("c_fax").ToString
                    c_usernametb.Text = reader.Item("username").ToString
                    c_emailtb.Text = reader.Item("email").ToString
                    c_passwordtb.Text = reader.Item("password").ToString
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
        If Session("selected_companyID") <> "0" Then
            companyID = Session("selected_companyID")
        Else
            companyID = Session("companyID")
        End If
        If appcode.IsUsernameAvailable(Trim(c_emailtb.Text), Session("selected_userID").ToString) = True Then
            If Session("selected_userID").ToString <> "0" Then
                appcode.UpdateUser(Session("selected_userID"), c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, c_admincb.Checked, c_usernametb.Text)
            Else
                Session("selected_userID") = appcode.InsertUser(c_nametb.Text, c_titletb.Text, c_phonetb.Text, c_faxtb.Text, c_emailtb.Text, c_passwordtb.Text, companyID, True, c_usernametb.Text)
                Response.Redirect("Contact.aspx")
            End If
            Response.Redirect("Contacts.aspx")
        Else
            emaillbl.Visible = True
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteUser(Session("selected_userID"))
        Response.Redirect("Contacts.aspx")
    End Sub

End Class
