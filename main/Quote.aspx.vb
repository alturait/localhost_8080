Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class EST_Quote
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim linetotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "extended"))
            subtotal = subtotal + linetotal
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim subtotallbl As Label = DirectCast(e.Row.FindControl("subtotallbl"), Label)
            Dim salestaxlbl As Label = DirectCast(e.Row.FindControl("salestaxlbl"), Label)
            Dim grandtotallbl As Label = DirectCast(e.Row.FindControl("grandtotallbl"), Label)
            subtotallbl.Text = subtotal.ToString("c")
            salestaxlbl.Text = (subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("selected_vendorID"))).ToString("c")
            grandtotallbl.Text = (subtotal + (subtotal * appcode.GetSalesTax(Session("selected_companyID"), Session("selected_vendorID")))).ToString("c")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select * from t_quote where quoteID=@quoteID"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@quoteID", Session("quoteID"))
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                companyIDlbl.Value = reader.Item("companyID")
                quoteIDlbl.Text = reader.Item("quoteID").ToString
                quotedatelbl.Text = reader.Item("response_date").ToString
                deliverby_datetb.Text = reader.Item("deliverby_date").ToString
                vendorlbl.Text = reader.Item("vendor").ToString
                vphonelbl.Text = reader.Item("v_phone").ToString
                vfaxlbl.Text = reader.Item("v_fax").ToString
                Dim r_address As String = ""
                If reader.Item("r_address1").ToString <> "" Then
                    r_address = reader.Item("r_address1") & "<br/>"
                    If reader.Item("r_address2").ToString <> "" Then
                        r_address &= reader.Item("r_address2") & "<br/>"
                    End If
                    r_address &= reader.Item("r_city") & ", " & reader.Item("r_state") & " " & reader.Item("r_zipcode")
                End If
                remittolbl.Text = r_address
                customerlbl.Text = reader.Item("company").ToString
                Dim b_address As String = ""
                If reader.Item("b_address1").ToString <> "" Then
                    b_address = reader.Item("b_address1") & "<br/>"
                    If reader.Item("b_address2").ToString <> "" Then
                        b_address &= reader.Item("b_address2") & "<br/>"
                    End If
                    b_address &= reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
                End If
                billtolbl.Text = b_address
                Dim s_address As String = ""
                If reader.Item("s_address1").ToString <> "" Then
                    s_address &= reader.Item("shipto") & "<br/>"
                    s_address &= reader.Item("s_address1") & "<br/>"
                    If reader.Item("r_address2").ToString <> "" Then
                        s_address &= reader.Item("s_address2") & "<br/>"
                    End If
                    s_address &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
                End If
                kitcb.Checked = reader.Item("is_kit")
                kitIDlbl.Text = reader.Item("serviceID")
                shiptolbl.Text = s_address
                totb.Text = appcode.GetUserEmail(reader.Item("userID"))
                userIDlbl.Value = reader.Item("userID").ToString
                contactlbl.Text = reader.Item("c_contact").ToString
                phonelbl.Text = reader.Item("c_phone").ToString
                vfaxlbl.Text = reader.Item("c_fax").ToString
                notestb.Text = reader.Item("notes").ToString
            End If
            reader.Close()
            conn.Close()
            If appcode.isCustomer(Session("companyID")) = True Then
                editbtn.Visible = False
                copybtn.Visible = False
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub copybtn_Click(sender As Object, e As EventArgs) Handles copybtn.Click
        Session("orderID") = appcode.CopyQuoteToOrder(Session("quoteID"))
        appcode.UpdateOrderQuoteID(Session("orderID"), Session("quoteID"))
        appcode.DeleteQuote(Session("quoteID"))
        Session("selected_companyID") = companyIDlbl.Value
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs) Handles editbtn.Click
        Response.Redirect("EditQuote.aspx")
    End Sub
End Class
