
Partial Class main_FilterList
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mfrdd.SelectedValue = Request.QueryString("mfr")
        End If
        Dim customerlist As String = ""
        Dim commandstring1 As String = "SELECT manufacturer,partnumber,description,count(partnumber) as numlines FROM v_kitparts WHERE "
        Dim commandstring2 As String = "SELECT [manufacturer] FROM v_kitparts WHERE "
        Dim clist() As String = Split(Session("qstring"), "|")
        Dim x As Integer
        Dim y As Integer = 0
        For x = 0 To clist.Length - 1
            If y > 0 Then
                commandstring1 &= " or companyID=" & clist(x)
                commandstring2 &= " or companyID=" & clist(x)
                customerlist &= ", " & appcode.GetCompany(clist(x))
            Else
                commandstring1 &= "(companyID=" & clist(x)
                commandstring2 &= "(companyID=" & clist(x)
                customerlist &= appcode.GetCompany(clist(x))
            End If
            y += 1
        Next
        commandstring1 &= ") AND manufacturer=@manufacturer GROUP BY manufacturer,partnumber,description ORDER BY manufacturer, partnumber"
        commandstring2 &= ") GROUP by manufacturer ORDER BY manufacturer"
        SqlPartSummary.SelectCommand = commandstring1
        SqlMfrs.SelectCommand = commandstring2
        customerslbl.Text = customerlist
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            appcode.UpdateOnHand(manufacturerlbl.Text, partnumberlbl.Text, onhandtb.Text)
        Next
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim linetotal As Decimal = Convert.ToDecimal(appcode.GetExtended(DataBinder.Eval(e.Row.DataItem, "manufacturer"), DataBinder.Eval(e.Row.DataItem, "partnumber")))
            subtotal = subtotal + linetotal
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
        End If
    End Sub

    Protected Sub mfrdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mfrdd.SelectedIndexChanged
        Response.Redirect("FilterList.aspx?mfr=" & mfrdd.SelectedValue)
    End Sub

    Protected Sub addPObtn_Click(sender As Object, e As EventArgs) Handles addPObtn.Click
        Dim poID As Integer = podd.SelectedValue
        If podd.SelectedValue = "0" Then
            poID = appcode.InsertPO(Session("vendorID"), appcode.GetCompany(Session("vendorID")), FormatDateTime(Now(), DateFormat.ShortDate), Session("userID"), appcode.GetUserName(Session("userID")))
        End If
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim packagelbl As Label = row.FindControl("packagelbl")
            Dim costlbl As Label = row.FindControl("costlbl")
            If selectcb.Checked = True Then
                Dim polineID As Integer = appcode.InsertPOLine(poID, manufacturerlbl.Text, partnumberlbl.Text, packagelbl.Text, costlbl.Text, "EACH", False, 0)
            End If
        Next
        Session("poID") = poID
        Response.Redirect("PurchaseOrder.aspx")
    End Sub
End Class
