 <%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Application("purchasing_email") = "purchasing@dfofilters.com"
        Application("kitcharge") = 5.0
        Application("orders_email") = "orders@dfofilters.com"
        Application("logo") = "http://www.dfofilters.com/Images/DFO_LOGO_v3.jpg"
        Application("banner") = "http://www.dfofilters.com/Images/dfo_email_banner.jpg"
        Application("banner_po") = "http://www.dfofilters.com/Images/dfo_email_banner.jpg"
        Application("banner_receipt") = "http://www.dfofilters.com/Images/dfo_email_banner.jpg"
        Application("banner_order") = "http://www.dfofilters.com/Images/dfo_email_banner.jpg"
        Application("banner_weborder") = "http://www.dfofilters.com/Images/dfo_email_banner.jpg"
        Application("site_address") = "http://www.dfofilters.com"
        Application("site_phone") = "480-295-1676"
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        Dim objErr As Exception = Server.GetLastError().GetBaseException()
        Dim err As String = "Error Message: " & objErr.Message.ToString() & System.Environment.NewLine
        Session("err") = err
        Session("err_page") = Request.Url.ToString()
        Session("err_userID") = Session("userID")
        'Server.ClearError()
        If Session("userID").ToString = "" Then
            Session("userID") = "0"
        End If
        appcode.InsertError(Session("userID"), err, Session("err_page"))
        'Response.Redirect("Error.aspx")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        Session("parentID") = "0"
        Session("categoryID") = "0"
        Session("vendorID") = "98"
        Session("locationID") = "0"
        Session("companyID") = "0"
        Session("selected_locationID") = "0"
        Session("selected_companyID") = "0"
        Session("selected_supplierID") = "0"
        Session("selected_shipID") = "0"
        Session("selected_userID") = "0"
        Session("ssID") = Session.SessionID
        Response.Redirect("http://www.dfofilters.com/Default.aspx")
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

</script>