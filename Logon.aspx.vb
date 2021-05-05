Imports System.Data.SqlClient
Imports System.IO
Partial Class Logon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            usernametb.Focus()
            pagelbl.Text = Page.Title
        End If
    End Sub

    Protected Sub CleanPDFs()
        Dim s As String
        For Each s In System.IO.Directory.GetFiles(MapPath("\PDF"))
            System.IO.File.Delete(s)
        Next
    End Sub

    Protected Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        If usernametb.Text <> "" Then
            Dim loginID As Integer
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from v_user where username=@username"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@username", usernametb.Text)
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                Session("nextpage") = ""
                Session("flyerID") = "0"
                Session("lastpage") = ""
                Session("imfr") = ""
                Session("warehousemfr") = ""
                Session("surcharge") = False
                Session("chargetax") = False
                Session("hidecb") = False
                Session("hours_miles") = "0"
                Session("menu") = ""
                Session("selected_assetID") = "0"
                Session("repairID") = ""
                Session("searchdd") = "1"
                If reader.Item("password") = Trim(passwordtb.Text) Then
                    Session("userID") = reader.Item("userID").ToString
                    Session("admin") = reader.Item("administrator")
                    Session("companyID") = reader.Item("companyID").ToString
                    If isCustomer(reader.Item("companyID")) = True Then
                        Session("selected_companyID") = Session("companyID")
                        Session("vendorID") = reader.Item("vendorID")
                        Session("logo") = "bannerlogo.jpg"
                        Session("menu") = "catalog"
                        'modify shopping cart if not empty
                        loginID = appcode.InsertLogin(Session("userID"))
                        Response.Redirect("customer/Favorites.aspx")
                    ElseIf Session("companyID") = 98 Then
                        Session("super") = reader.Item("super")
                        Session("webadmin") = reader.Item("administrator")
                        Session("selected_companyID") = appcode.GetDefaultCompanyID()
                        Session("vendorID") = Session("companyID")
                        Session("logo") = "bannerlogo.jpg"
                        CleanPDFs()
                        If reader.Item("administrator") = True Then
                            Session("menu") = "corders"
                            loginID = appcode.InsertLogin(Session("userID"))
                            Response.Redirect("main/Default.aspx?menu=corders")
                        Else
                            'sales rep - set selected_companyID
                            Session("selected_companyID") = appcode.GetRepDefaultCompanyID(reader.Item("userID"))
                            Response.Redirect("main/Default.aspx?menu=corders")
                            loginID = appcode.InsertLogin(Session("userID"))
                        End If
                    Else
                        errmsglbl.Text = "Please enter a valid User Name."
                        Panel3.Visible = True
                    End If
                Else
                    errmsglbl.Text = "Your password is incorrect."
                    Panel3.Visible = True
                End If
            Else
                errmsglbl.Text = "User Name not recognized."
                Panel3.Visible = True
            End If
            reader.Close()
            conn.Close()
        End If
    End Sub

    Protected Function isCustomer(ByVal companyID As Integer) As Boolean
        isCustomer = False
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            isCustomer = reader.Item("customer")
        End If
        reader.Close()
        conn.Close()
    End Function

End Class
