Imports System.Data.SqlClient

Partial Class mobile_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            accesstb.Focus()
            logoimg.ImageUrl = "../Images/lubetrackermobile.jpg"
            logoimg.Width = 800
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_user where userID=@userID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@userID", Session("userID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                Session("mobile_userID") = reader.Item("userID")
                Session("mobile_companyID") = reader.Item("companyID")
                Session("mobile_user") = reader.Item("name")
                Session("mobile_user_email") = reader.Item("email")

                'define equipment manager info
                'Session("mobile_company_userID") = reader.Item("website_userID")
                'Session("mobile_company_user_email") = appcode.GetUserEmail(reader.Item("website_userID"))

                Session("mobile_company") = appcode.GetCompany(Session("companyID"))
                Session("mobile_equipmentID") = appcode.getDefaultEquipmentID(Session("mobile_companyID"))
                Session("mobile_serviceprofileID") = appcode.GetDefaultServiceProfileID(Session("mobile_equipmentID"))
                Session("mobile_partID") = ""
                'appcode.UpdateEquipmentViews(Session("mobile_userID"))
                'appcode.UpdateLogins(Session("mobile_userID"))
                'appcode.UpdateLastLogin(Session("mobile_userID"))
                Response.Redirect("Order.aspx")
            End If
            reader.Close()
            conn.Close()
        End If
    End Sub

End Class
